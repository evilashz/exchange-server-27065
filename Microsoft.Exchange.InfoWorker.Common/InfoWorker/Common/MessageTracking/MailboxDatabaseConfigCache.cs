﻿using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x02000308 RID: 776
	internal sealed class MailboxDatabaseConfigCache : LazyLookupTimeoutCacheWithDiagnostics<ADObjectId, string>
	{
		// Token: 0x060016FB RID: 5883 RVA: 0x0006AC3C File Offset: 0x00068E3C
		public MailboxDatabaseConfigCache() : base(2, 100, false, TimeSpan.FromHours(5.0))
		{
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x0006AC58 File Offset: 0x00068E58
		protected override string Create(ADObjectId dataBaseId, ref bool shouldAdd)
		{
			TraceWrapper.SearchLibraryTracer.TraceDebug<ADObjectId>(this.GetHashCode(), "MailboxDatabaseConfigCache miss, searching for {0}", dataBaseId);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), OrganizationId.ForestWideOrgId, null, false), 53, "Create", "f:\\15.00.1497\\sources\\dev\\infoworker\\src\\common\\MessageTracking\\Caching\\MailboxDatabaseConfigCache.cs");
			shouldAdd = true;
			return MailboxDatabaseConfigCache.GetServerFromDatabase(tenantOrTopologyConfigurationSession, dataBaseId);
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x0006ACB0 File Offset: 0x00068EB0
		public static string GetServerFromDatabase(IConfigurationSession globalConfigSession, ADObjectId dataBaseId)
		{
			Database database = globalConfigSession.Read<Database>(dataBaseId);
			if (database == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<ADObjectId>(0, "Null Database read for DataBase ID: {0}", dataBaseId);
				TrackingFatalException.RaiseED(ErrorCode.InvalidADData, "No object: {0}", new object[]
				{
					dataBaseId
				});
			}
			ADObjectId server = database.Server;
			if (server == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<ADObjectId>(0, "Null Server ID returned for DataBase ID: {0}", dataBaseId);
				TrackingFatalException.RaiseED(ErrorCode.InvalidADData, "No msExchOwningServer attribute for DataBase-Id {0}", new object[]
				{
					dataBaseId
				});
			}
			Server server2 = globalConfigSession.Read<Server>(server);
			if (server2 == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<ADObjectId>(0, "Null Server read for DataBase ID: {0}", dataBaseId);
				TrackingFatalException.RaiseED(ErrorCode.InvalidADData, "No server object for {0} for DataBase-Id {1}", new object[]
				{
					server,
					dataBaseId
				});
			}
			if (string.IsNullOrEmpty(server2.Fqdn))
			{
				TraceWrapper.SearchLibraryTracer.TraceError<ADObjectId>(0, "Null/Empty FQDN for server with ID: {0} for user", server);
				TrackingFatalException.RaiseED(ErrorCode.InvalidADData, "No FQDN found for server {0}", new object[]
				{
					server
				});
			}
			return server2.Fqdn;
		}
	}
}
