using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Common.Cache
{
	// Token: 0x0200067E RID: 1662
	internal abstract class DefaultCachePerformanceCounters : ICachePerformanceCounters
	{
		// Token: 0x06001E1F RID: 7711 RVA: 0x00037443 File Offset: 0x00035643
		public DefaultCachePerformanceCounters()
		{
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x0003744C File Offset: 0x0003564C
		public virtual void Accessed(AccessStatus accessStatus)
		{
			if (this.requestsCounter != null)
			{
				this.requestsCounter.Increment();
				if (accessStatus.Equals(AccessStatus.Hit) && this.hitRatioCounter != null)
				{
					this.hitRatioCounter.Increment();
				}
				if (this.hitRatioBaseCounter != null)
				{
					this.hitRatioBaseCounter.Increment();
				}
			}
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x000374A8 File Offset: 0x000356A8
		public virtual void SizeUpdated(long cacheSize)
		{
			if (this.cacheSizeCounter != null)
			{
				this.cacheSizeCounter.RawValue = cacheSize;
			}
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x000374BE File Offset: 0x000356BE
		protected void InitializeCounters(ExPerformanceCounter requestsCounter, ExPerformanceCounter hitRatioCounter, ExPerformanceCounter hitRatioBaseCounter, ExPerformanceCounter cacheSizeCounter)
		{
			this.requestsCounter = requestsCounter;
			this.hitRatioCounter = hitRatioCounter;
			this.hitRatioBaseCounter = hitRatioBaseCounter;
			this.cacheSizeCounter = cacheSizeCounter;
		}

		// Token: 0x04001E2E RID: 7726
		private ExPerformanceCounter requestsCounter;

		// Token: 0x04001E2F RID: 7727
		private ExPerformanceCounter hitRatioCounter;

		// Token: 0x04001E30 RID: 7728
		private ExPerformanceCounter hitRatioBaseCounter;

		// Token: 0x04001E31 RID: 7729
		private ExPerformanceCounter cacheSizeCounter;
	}
}
