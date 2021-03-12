using System;

namespace Microsoft.Exchange.Collections.TimeoutCache
{
	// Token: 0x020006A0 RID: 1696
	internal class CacheEntryLimitedSliding<K, T> : CacheEntrySliding<K, T>
	{
		// Token: 0x06001F34 RID: 7988 RVA: 0x0003ADD8 File Offset: 0x00038FD8
		internal CacheEntryLimitedSliding(K key, T value, TimeSpan slidingLiveTime, TimeSpan maximumLiveTime) : base(key, value, slidingLiveTime)
		{
			if (maximumLiveTime < slidingLiveTime)
			{
				throw new ArgumentException("MaximumLiveTime must be greater than SlidingLiveTime.", "MaximumLiveTime");
			}
			this.maximumLiveTime = maximumLiveTime;
			this.maximumExpirationUtc = ((maximumLiveTime == TimeSpan.MaxValue) ? DateTime.MaxValue : DateTime.UtcNow.Add(maximumLiveTime));
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x0003AE3C File Offset: 0x0003903C
		internal override void OnForceExtend()
		{
			base.OnForceExtend();
			this.maximumExpirationUtc = ((this.maximumLiveTime == TimeSpan.MaxValue) ? DateTime.MaxValue : DateTime.UtcNow.Add(this.maximumLiveTime));
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x0003AE81 File Offset: 0x00039081
		internal override void OnTouch()
		{
			base.OnTouch();
			if (this.touchedExpirationUtc > this.maximumExpirationUtc)
			{
				this.touchedExpirationUtc = this.maximumExpirationUtc;
			}
		}

		// Token: 0x04001E94 RID: 7828
		private readonly TimeSpan maximumLiveTime;

		// Token: 0x04001E95 RID: 7829
		private DateTime maximumExpirationUtc;
	}
}
