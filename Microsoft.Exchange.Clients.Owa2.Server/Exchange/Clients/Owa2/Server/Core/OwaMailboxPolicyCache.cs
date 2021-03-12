using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000E5 RID: 229
	internal sealed class OwaMailboxPolicyCache : LazyLookupTimeoutCache<OrgIdADObjectWrapper, PolicyConfiguration>
	{
		// Token: 0x0600086D RID: 2157 RVA: 0x0001BA4A File Offset: 0x00019C4A
		private OwaMailboxPolicyCache() : base(5, 1000, false, TimeSpan.FromMinutes(15.0), TimeSpan.FromMinutes(60.0))
		{
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x0001BA75 File Offset: 0x00019C75
		internal static OwaMailboxPolicyCache Instance
		{
			get
			{
				return OwaMailboxPolicyCache.instance;
			}
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0001BA7C File Offset: 0x00019C7C
		protected override PolicyConfiguration CreateOnCacheMiss(OrgIdADObjectWrapper key, ref bool shouldAdd)
		{
			shouldAdd = true;
			return this.GetPolicyFromAD(key);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0001BA88 File Offset: 0x00019C88
		private PolicyConfiguration GetPolicyFromAD(OrgIdADObjectWrapper key)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(key.OrgId);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.FullyConsistent, sessionSettings, 89, "GetPolicyFromAD", "f:\\15.00.1497\\sources\\dev\\clients\\src\\Owa2\\Server\\Core\\configuration\\OwaMailboxPolicyCache.cs");
			return PolicyConfiguration.GetPolicyConfigurationFromAD(tenantOrTopologyConfigurationSession, key.AdObject);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0001BAC8 File Offset: 0x00019CC8
		internal static PolicyConfiguration GetPolicyConfiguration(ADObjectId owaMailboxPolicyId, OrganizationId organizationId)
		{
			PolicyConfiguration policyConfiguration = null;
			if (owaMailboxPolicyId != null)
			{
				policyConfiguration = OwaMailboxPolicyCache.Instance.Get(new OrgIdADObjectWrapper(owaMailboxPolicyId, organizationId));
			}
			if (policyConfiguration == null)
			{
				ADObjectId adobjectId = OwaMailboxPolicyIdCacheByOrganization.Instance.Get(organizationId);
				if (adobjectId != null)
				{
					policyConfiguration = OwaMailboxPolicyCache.Instance.Get(new OrgIdADObjectWrapper(adobjectId, organizationId));
				}
			}
			return policyConfiguration;
		}

		// Token: 0x04000523 RID: 1315
		private const int OwaMailboxPolicyCacheBucketSize = 1000;

		// Token: 0x04000524 RID: 1316
		private const int OwaMailboxPolicyCacheBuckets = 5;

		// Token: 0x04000525 RID: 1317
		private static OwaMailboxPolicyCache instance = new OwaMailboxPolicyCache();
	}
}
