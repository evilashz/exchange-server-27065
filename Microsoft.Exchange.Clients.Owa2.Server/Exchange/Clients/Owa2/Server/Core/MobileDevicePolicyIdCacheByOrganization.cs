using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Configuration;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000DF RID: 223
	internal class MobileDevicePolicyIdCacheByOrganization : LazyLookupExactTimeoutCache<OrganizationId, ADObjectId>
	{
		// Token: 0x06000856 RID: 2134 RVA: 0x0001B6F1 File Offset: 0x000198F1
		protected MobileDevicePolicyIdCacheByOrganization(int maxCount, TimeSpan slidingLiveTime, TimeSpan absoluteLiveTime) : base(maxCount, false, slidingLiveTime, absoluteLiveTime, CacheFullBehavior.ExpireExisting)
		{
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0001B6FE File Offset: 0x000198FE
		protected MobileDevicePolicyIdCacheByOrganization() : this(5000, TimeSpan.FromMinutes(15.0), TimeSpan.FromMinutes(60.0))
		{
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x0001B727 File Offset: 0x00019927
		internal static MobileDevicePolicyIdCacheByOrganization Instance
		{
			get
			{
				return MobileDevicePolicyIdCacheByOrganization.instance;
			}
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0001B730 File Offset: 0x00019930
		protected override ADObjectId CreateOnCacheMiss(OrganizationId key, ref bool shouldAdd)
		{
			ADObjectId policyIdFromAD = this.GetPolicyIdFromAD(key);
			shouldAdd = (policyIdFromAD != null);
			return policyIdFromAD;
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001B74F File Offset: 0x0001994F
		protected virtual IConfigurationSession GetConfigSession(ADSessionSettings settings)
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.FullyConsistent, settings, 103, "GetConfigSession", "f:\\15.00.1497\\sources\\dev\\clients\\src\\Owa2\\Server\\Core\\configuration\\MobileDevicePolicyIdCacheByOrganization.cs");
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0001B7F0 File Offset: 0x000199F0
		private ADObjectId GetPolicyIdFromAD(OrganizationId key)
		{
			ExTraceGlobals.MobileDevicePolicyTracer.Information<OrganizationId>((long)this.GetHashCode(), "MobileDevicePolicyIdCacheByOrganization.GetPolicyFromAD({0})", key);
			ADSessionSettings settings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(key);
			IConfigurationSession session = this.GetConfigSession(settings);
			ADObjectId rootId = session.GetOrgContainerId();
			QueryFilter filter = new BitMaskAndFilter(MobileMailboxPolicySchema.MobileFlags, 4096UL);
			SortBy sortBy = new SortBy(ADObjectSchema.WhenChanged, SortOrder.Descending);
			ADObjectId policyId = null;
			try
			{
				ADNotificationAdapter.RunADOperation(delegate()
				{
					MobileMailboxPolicy[] array = session.Find<MobileMailboxPolicy>(rootId, QueryScope.SubTree, filter, sortBy, 1);
					if (array != null && array.Length > 0)
					{
						policyId = array[0].Id;
						OrgIdADObjectWrapper key2 = new OrgIdADObjectWrapper(policyId, key);
						if (!MobileDevicePolicyCache.Instance.Contains(key2))
						{
							MobileDevicePolicyData mobileDevicePolicyDataFromMobileMailboxPolicy = MobileDevicePolicyDataFactory.GetMobileDevicePolicyDataFromMobileMailboxPolicy(array[0]);
							MobileDevicePolicyCache.Instance.TryAdd(key2, ref mobileDevicePolicyDataFromMobileMailboxPolicy);
						}
					}
				});
			}
			catch (LocalizedException arg)
			{
				ExTraceGlobals.MobileDevicePolicyTracer.TraceError<OrganizationId, LocalizedException>((long)this.GetHashCode(), "MobileDevicePolicyIdCacheByOrganization.GetPolicyIdFromAD({0}) threw exception: {1}", key, arg);
				throw;
			}
			ExTraceGlobals.MobileDevicePolicyTracer.Information<OrganizationId, ADObjectId>((long)this.GetHashCode(), "MobileDevicePolicyIdCacheByOrganization.GetPolicyFromAD({0}) returned: {1}", key, policyId);
			return policyId;
		}

		// Token: 0x0400050C RID: 1292
		private const int MobileDevicePolicyIdCacheCapacity = 5000;

		// Token: 0x0400050D RID: 1293
		private static MobileDevicePolicyIdCacheByOrganization instance = new MobileDevicePolicyIdCacheByOrganization();
	}
}
