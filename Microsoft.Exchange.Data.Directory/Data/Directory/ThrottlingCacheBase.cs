using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009A3 RID: 2467
	internal abstract class ThrottlingCacheBase<K, T> : LazyLookupExactTimeoutCache<K, T>
	{
		// Token: 0x060071E8 RID: 29160 RVA: 0x001799AE File Offset: 0x00177BAE
		protected ThrottlingCacheBase(int maxCount, bool shouldCallbackOnDispose, TimeSpan absoluteLiveTime, CacheFullBehavior cacheFullBehavior) : base(maxCount, shouldCallbackOnDispose, absoluteLiveTime, cacheFullBehavior)
		{
		}

		// Token: 0x060071E9 RID: 29161 RVA: 0x001799BB File Offset: 0x00177BBB
		protected ThrottlingCacheBase(int maxCount, bool shouldCallbackOnDispose, TimeSpan slidingLiveTime, TimeSpan absoluteLiveTime, CacheFullBehavior cacheFullBehavior) : base(maxCount, shouldCallbackOnDispose, slidingLiveTime, absoluteLiveTime, cacheFullBehavior)
		{
		}

		// Token: 0x060071EA RID: 29162 RVA: 0x001799CC File Offset: 0x00177BCC
		protected override void BeforeGet(K key)
		{
			int num = 0;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<int>(2663787837U, ref num);
			if (num != 0 && num != this.lastClearCacheStamp)
			{
				this.lastClearCacheStamp = num;
				this.Clear();
			}
		}

		// Token: 0x060071EB RID: 29163 RVA: 0x00179A05 File Offset: 0x00177C05
		internal override void Clear()
		{
			base.Clear();
			ThrottlingPerfCounterWrapper.ClearCaches();
		}

		// Token: 0x040049C4 RID: 18884
		private const uint LidClearThrottlingCaches = 2663787837U;

		// Token: 0x040049C5 RID: 18885
		private int lastClearCacheStamp;
	}
}
