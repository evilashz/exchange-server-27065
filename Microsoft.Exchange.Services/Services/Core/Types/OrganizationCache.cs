using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000834 RID: 2100
	internal sealed class OrganizationCache : LazyLookupTimeoutCache<OrganizationId, Organization>
	{
		// Token: 0x06003C86 RID: 15494 RVA: 0x000D5E1A File Offset: 0x000D401A
		private OrganizationCache() : base(1, 1000, false, TimeSpan.FromSeconds((double)Global.OrganizationWideAccessPolicyTimeoutInSeconds), TimeSpan.FromSeconds((double)Global.OrganizationWideAccessPolicyTimeoutInSeconds))
		{
		}

		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x06003C87 RID: 15495 RVA: 0x000D5E3F File Offset: 0x000D403F
		internal static OrganizationCache Singleton
		{
			get
			{
				return OrganizationCache.singleton;
			}
		}

		// Token: 0x06003C88 RID: 15496 RVA: 0x000D5E48 File Offset: 0x000D4048
		protected override Organization CreateOnCacheMiss(OrganizationId orgId, ref bool shouldAdd)
		{
			if (Global.OrganizationWideAccessPolicyTimeoutInSeconds == 0)
			{
				shouldAdd = false;
			}
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(orgId), 63, "CreateOnCacheMiss", "f:\\15.00.1497\\sources\\dev\\services\\src\\Core\\Types\\OrganizationCache.cs");
			Organization orgContainer = tenantOrTopologyConfigurationSession.GetOrgContainer();
			shouldAdd = orgContainer.IsValid;
			return orgContainer;
		}

		// Token: 0x0400216E RID: 8558
		private const int OrganizationCacheBuckets = 1;

		// Token: 0x0400216F RID: 8559
		private const int OrganizationCacheBucketSize = 1000;

		// Token: 0x04002170 RID: 8560
		private static OrganizationCache singleton = new OrganizationCache();
	}
}
