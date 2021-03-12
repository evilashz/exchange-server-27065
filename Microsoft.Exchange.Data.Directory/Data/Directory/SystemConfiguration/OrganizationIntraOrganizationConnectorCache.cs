using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000640 RID: 1600
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OrganizationIntraOrganizationConnectorCache : OrganizationBaseCache
	{
		// Token: 0x06004B66 RID: 19302 RVA: 0x00115FEC File Offset: 0x001141EC
		public OrganizationIntraOrganizationConnectorCache(OrganizationId organizationId, IConfigurationSession session) : base(organizationId, session)
		{
			this.cache = new Dictionary<string, IntraOrganizationConnector>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06004B67 RID: 19303 RVA: 0x00116008 File Offset: 0x00114208
		public IntraOrganizationConnector Get(string domainName)
		{
			Dictionary<string, IntraOrganizationConnector> dictionary = this.cache;
			IntraOrganizationConnector intraOrganizationConnector = null;
			lock (dictionary)
			{
				if (!dictionary.TryGetValue(domainName, out intraOrganizationConnector))
				{
					OrganizationBaseCache.Tracer.TraceDebug<string, OrganizationId>((long)this.GetHashCode(), "OrganizationRelationship cache miss for: {0} in organization {1}", domainName, base.OrganizationId);
					QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, IntraOrganizationConnectorSchema.TargetAddressDomains, domainName);
					IntraOrganizationConnector[] array = base.Session.Find<IntraOrganizationConnector>(IntraOrganizationConnector.GetContainerId(base.Session), QueryScope.SubTree, filter, null, 1);
					if (array.Length == 1)
					{
						intraOrganizationConnector = array[0];
					}
					dictionary.Add(domainName, intraOrganizationConnector);
					OrganizationBaseCache.Tracer.TraceDebug((long)this.GetHashCode(), "Cached IntraOrganizationConnector {0}", new object[]
					{
						(intraOrganizationConnector == null) ? "<null>" : intraOrganizationConnector.Id
					});
				}
				else
				{
					OrganizationBaseCache.Tracer.TraceDebug<string, object>((long)this.GetHashCode(), "Found IntraOrganizationConnector in cache for domain {0}: {1}", domainName, (intraOrganizationConnector == null) ? "<null>" : intraOrganizationConnector.Id);
				}
			}
			return intraOrganizationConnector;
		}

		// Token: 0x040033CC RID: 13260
		private Dictionary<string, IntraOrganizationConnector> cache;
	}
}
