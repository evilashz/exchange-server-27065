using System;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.IsMemberOfProvider
{
	// Token: 0x020001C2 RID: 450
	internal class CachePerformanceCounters : ICachePerformanceCounters
	{
		// Token: 0x0600126F RID: 4719 RVA: 0x00059090 File Offset: 0x00057290
		public CachePerformanceCounters(ExPerformanceCounter hitCount, ExPerformanceCounter missCount, ExPerformanceCounter cacheSize, ExPerformanceCounter cacheSizePercentage, long maxCacheSizeInBytes)
		{
			this.hitCount = hitCount;
			this.missCount = missCount;
			this.cacheSize = cacheSize;
			this.cacheSizePercentage = cacheSizePercentage;
			this.maxCacheSize = maxCacheSizeInBytes;
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x000590C0 File Offset: 0x000572C0
		public void Accessed(AccessStatus accessStatus)
		{
			switch (accessStatus)
			{
			case AccessStatus.Hit:
				this.hitCount.Increment();
				return;
			case AccessStatus.Miss:
				this.missCount.Increment();
				return;
			default:
				return;
			}
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x000590F8 File Offset: 0x000572F8
		public void SizeUpdated(long cacheSize)
		{
			this.cacheSize.RawValue = cacheSize;
			if (this.maxCacheSize != 0L)
			{
				double num = (double)cacheSize / (double)this.maxCacheSize;
				this.cacheSizePercentage.RawValue = (long)(num * 100.0);
			}
		}

		// Token: 0x04000AA6 RID: 2726
		private readonly long maxCacheSize;

		// Token: 0x04000AA7 RID: 2727
		private ExPerformanceCounter hitCount;

		// Token: 0x04000AA8 RID: 2728
		private ExPerformanceCounter missCount;

		// Token: 0x04000AA9 RID: 2729
		private ExPerformanceCounter cacheSize;

		// Token: 0x04000AAA RID: 2730
		private ExPerformanceCounter cacheSizePercentage;
	}
}
