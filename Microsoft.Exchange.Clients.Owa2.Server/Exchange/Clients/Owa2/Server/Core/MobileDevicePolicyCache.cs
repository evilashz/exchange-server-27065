using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Configuration;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000DD RID: 221
	internal class MobileDevicePolicyCache : LazyLookupExactTimeoutCache<OrgIdADObjectWrapper, MobileDevicePolicyData>
	{
		// Token: 0x06000848 RID: 2120 RVA: 0x0001B2C9 File Offset: 0x000194C9
		protected MobileDevicePolicyCache(int maxCount, TimeSpan slidingLiveTime, TimeSpan absoluteLiveTime) : base(maxCount, false, slidingLiveTime, absoluteLiveTime, CacheFullBehavior.ExpireExisting)
		{
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001B2D6 File Offset: 0x000194D6
		protected MobileDevicePolicyCache() : this(5000, TimeSpan.FromMinutes(15.0), TimeSpan.FromMinutes(60.0))
		{
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x0001B2FF File Offset: 0x000194FF
		internal static MobileDevicePolicyCache Instance
		{
			get
			{
				return MobileDevicePolicyCache.instance;
			}
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001B308 File Offset: 0x00019508
		protected override MobileDevicePolicyData CreateOnCacheMiss(OrgIdADObjectWrapper key, ref bool shouldAdd)
		{
			MobileDevicePolicyData policyFromAD = this.GetPolicyFromAD(key);
			shouldAdd = (policyFromAD != null);
			return policyFromAD;
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0001B327 File Offset: 0x00019527
		protected virtual IConfigurationSession GetConfigSession(ADSessionSettings settings)
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.FullyConsistent, settings, 101, "GetConfigSession", "f:\\15.00.1497\\sources\\dev\\clients\\src\\Owa2\\Server\\Core\\configuration\\MobileDevicePolicyCache.cs");
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0001B368 File Offset: 0x00019568
		private MobileDevicePolicyData GetPolicyFromAD(OrgIdADObjectWrapper key)
		{
			ExTraceGlobals.MobileDevicePolicyTracer.Information<OrgIdADObjectWrapper>(0L, "MobileDevicePolicyCache.GetPolicyFromAD({0})", key);
			ADSessionSettings settings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(key.OrgId);
			IConfigurationSession session = this.GetConfigSession(settings);
			MobileDevicePolicyData policyData = null;
			try
			{
				ADNotificationAdapter.RunADOperation(delegate()
				{
					policyData = MobileDevicePolicyDataFactory.GetMobileDevicePolicyDataFromAD(session, key.AdObject);
				});
			}
			catch (LocalizedException arg)
			{
				ExTraceGlobals.MobileDevicePolicyTracer.TraceError<OrgIdADObjectWrapper, LocalizedException>((long)this.GetHashCode(), "MobileDevicePolicyCache.GetPolicyFromAD({0}) threw exception: {1}", key, arg);
				throw;
			}
			ExTraceGlobals.MobileDevicePolicyTracer.Information<OrgIdADObjectWrapper, MobileDevicePolicyData>((long)this.GetHashCode(), "MobileDevicePolicyCache.GetPolicyFromAD({0}) returned: {1}", key, policyData);
			return policyData;
		}

		// Token: 0x0400050A RID: 1290
		private const int MobileDevicePolicyCacheCapacity = 5000;

		// Token: 0x0400050B RID: 1291
		private static MobileDevicePolicyCache instance = new MobileDevicePolicyCache();
	}
}
