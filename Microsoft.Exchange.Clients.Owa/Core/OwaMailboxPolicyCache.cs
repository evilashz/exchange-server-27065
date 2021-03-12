using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001E2 RID: 482
	internal sealed class OwaMailboxPolicyCache : LazyLookupTimeoutCache<ADObjectId, PolicyConfiguration>
	{
		// Token: 0x06000F6B RID: 3947 RVA: 0x0005F8C0 File Offset: 0x0005DAC0
		private OwaMailboxPolicyCache() : base(5, 1000, false, TimeSpan.FromMinutes(15.0), TimeSpan.FromMinutes(60.0))
		{
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x0005F8EB File Offset: 0x0005DAEB
		protected override PolicyConfiguration CreateOnCacheMiss(ADObjectId key, ref bool shouldAdd)
		{
			shouldAdd = true;
			return this.GetPolicyFromAD(key);
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0005F8F8 File Offset: 0x0005DAF8
		private PolicyConfiguration GetPolicyFromAD(ADObjectId key)
		{
			IConfigurationSession configurationSession = Utilities.CreateConfigurationSessionScoped(true, ConsistencyMode.FullyConsistent, key);
			configurationSession.SessionSettings.IsSharedConfigChecked = true;
			return PolicyConfiguration.GetPolicyConfigurationFromAD(configurationSession, key);
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x0005F921 File Offset: 0x0005DB21
		internal static OwaMailboxPolicyCache Instance
		{
			get
			{
				return OwaMailboxPolicyCache.instance;
			}
		}

		// Token: 0x04000A5F RID: 2655
		private const int OwaMailboxPolicyCacheBucketSize = 1000;

		// Token: 0x04000A60 RID: 2656
		private const int OwaMailboxPolicyCacheBuckets = 5;

		// Token: 0x04000A61 RID: 2657
		private static OwaMailboxPolicyCache instance = new OwaMailboxPolicyCache();
	}
}
