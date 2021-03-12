using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Autodiscover.ConfigurationCache
{
	// Token: 0x02000027 RID: 39
	internal sealed class OrganizationCache : LazyLookupTimeoutCache<OrganizationId, Organization>
	{
		// Token: 0x06000135 RID: 309 RVA: 0x00007454 File Offset: 0x00005654
		private OrganizationCache() : base(1, 1000, false, TimeSpan.FromSeconds(10800.0), TimeSpan.FromSeconds(10800.0))
		{
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000747F File Offset: 0x0000567F
		internal static OrganizationCache Singleton
		{
			get
			{
				return OrganizationCache.singleton;
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00007488 File Offset: 0x00005688
		protected override Organization CreateOnCacheMiss(OrganizationId orgId, ref bool shouldAdd)
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(orgId), 56, "CreateOnCacheMiss", "f:\\15.00.1497\\sources\\dev\\autodisc\\src\\ConfigurationCache\\OrganizationCache.cs");
			return tenantOrTopologyConfigurationSession.GetOrgContainer();
		}

		// Token: 0x04000157 RID: 343
		private const int OrganizationCacheBuckets = 1;

		// Token: 0x04000158 RID: 344
		private const int OrganizationCacheBucketSize = 1000;

		// Token: 0x04000159 RID: 345
		private const int CacheTimeoutInSeconds = 10800;

		// Token: 0x0400015A RID: 346
		private static OrganizationCache singleton = new OrganizationCache();
	}
}
