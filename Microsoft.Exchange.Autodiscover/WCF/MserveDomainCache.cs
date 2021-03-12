using System;
using System.Threading;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Autodiscover;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000072 RID: 114
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MserveDomainCache : LazyLookupTimeoutCache<string, string>
	{
		// Token: 0x0600030F RID: 783 RVA: 0x000140A3 File Offset: 0x000122A3
		private MserveDomainCache() : base(1, MserveDomainCache.cacheSize.Value, false, MserveDomainCache.cacheTimeToLive.Value)
		{
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000310 RID: 784 RVA: 0x000140C1 File Offset: 0x000122C1
		public static MserveDomainCache Singleton
		{
			get
			{
				return MserveDomainCache.singleton;
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x000140C8 File Offset: 0x000122C8
		protected override string CreateOnCacheMiss(string key, ref bool shouldAdd)
		{
			int num = Interlocked.Increment(ref MserveDomainCache.concurrentMserveLookups);
			string redirectServer;
			try
			{
				if (num > MserveDomainCache.concurrentLookupMaximum.Value)
				{
					throw new OverBudgetException();
				}
				redirectServer = MServe.GetRedirectServer(string.Format("E5CB63F56E8B4b69A1F70C192276D6AD@{0}", key));
			}
			finally
			{
				Interlocked.Decrement(ref MserveDomainCache.concurrentMserveLookups);
			}
			return redirectServer;
		}

		// Token: 0x040002E2 RID: 738
		private static int concurrentMserveLookups = 0;

		// Token: 0x040002E3 RID: 739
		private static TimeSpanAppSettingsEntry cacheTimeToLive = new TimeSpanAppSettingsEntry("MserveDomainCacheTimeToLive", TimeSpanUnit.Seconds, TimeSpan.FromMinutes(30.0), ExTraceGlobals.FrameworkTracer);

		// Token: 0x040002E4 RID: 740
		private static IntAppSettingsEntry cacheSize = new IntAppSettingsEntry("MserveDomainCacheMaxItems", 4000, ExTraceGlobals.FrameworkTracer);

		// Token: 0x040002E5 RID: 741
		private static IntAppSettingsEntry concurrentLookupMaximum = new IntAppSettingsEntry("MserveDomainDomainConcurrentLookupMax", 10, ExTraceGlobals.FrameworkTracer);

		// Token: 0x040002E6 RID: 742
		private static MserveDomainCache singleton = new MserveDomainCache();
	}
}
