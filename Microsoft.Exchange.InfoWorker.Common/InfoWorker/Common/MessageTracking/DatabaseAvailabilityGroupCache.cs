using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x02000307 RID: 775
	internal sealed class DatabaseAvailabilityGroupCache : LazyLookupTimeoutCacheWithDiagnostics<ADObjectId, IList<string>>
	{
		// Token: 0x060016F8 RID: 5880 RVA: 0x0006AA9E File Offset: 0x00068C9E
		public DatabaseAvailabilityGroupCache() : base(2, 32, false, TimeSpan.FromHours(5.0))
		{
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x0006AAB8 File Offset: 0x00068CB8
		protected override IList<string> Create(ADObjectId databaseAvailabilityGroupId, ref bool shouldAdd)
		{
			TraceWrapper.SearchLibraryTracer.TraceDebug<ADObjectId>(this.GetHashCode(), "DatabaseAvailabilityGroupCache miss, searching for DAG by ADObjectId: {0}", databaseAvailabilityGroupId);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), OrganizationId.ForestWideOrgId, null, false), 53, "Create", "f:\\15.00.1497\\sources\\dev\\infoworker\\src\\common\\MessageTracking\\Caching\\DatabaseAvailabilityGroupCache.cs");
			shouldAdd = true;
			return DatabaseAvailabilityGroupCache.GetDagMembers(tenantOrTopologyConfigurationSession, databaseAvailabilityGroupId);
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x0006AB10 File Offset: 0x00068D10
		private static IList<string> GetDagMembers(IConfigurationSession globalConfigSession, ADObjectId databaseAvailabilityGroupId)
		{
			List<string> list = new List<string>(5);
			DatabaseAvailabilityGroup databaseAvailabilityGroup = globalConfigSession.Read<DatabaseAvailabilityGroup>(databaseAvailabilityGroupId);
			if (databaseAvailabilityGroup == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<ADObjectId>(0, "Null DAG object read for ADObjectId: {0}", databaseAvailabilityGroupId);
				return list;
			}
			if (databaseAvailabilityGroup.Servers == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<ADObjectId>(0, "Servers attribute set to null for DAG-Id: {0}", databaseAvailabilityGroupId);
				return list;
			}
			foreach (ADObjectId adobjectId in databaseAvailabilityGroup.Servers)
			{
				Server server = globalConfigSession.Read<Server>(adobjectId);
				if (server == null)
				{
					TraceWrapper.SearchLibraryTracer.TraceError<ADObjectId, ADObjectId>(0, "Null Server attribute was read for server-ID: {0} for DAG-Id: {1}", adobjectId, databaseAvailabilityGroupId);
					TrackingFatalException.RaiseED(ErrorCode.InvalidADData, "NonExistent Object: {0}", new object[]
					{
						adobjectId
					});
				}
				if (string.IsNullOrEmpty(server.Fqdn))
				{
					TraceWrapper.SearchLibraryTracer.TraceError<ADObjectId, ADObjectId>(0, "Null/Empty FQDN for server with ID: {0} for DAG-Id: {1}", adobjectId, databaseAvailabilityGroupId);
					TrackingFatalException.RaiseED(ErrorCode.InvalidADData, "Missing FQDN: {0}", new object[]
					{
						adobjectId
					});
				}
				TraceWrapper.SearchLibraryTracer.TraceDebug<string>(0, "Found server: {0}", server.Fqdn);
				list.Add(server.Fqdn);
			}
			return list;
		}
	}
}
