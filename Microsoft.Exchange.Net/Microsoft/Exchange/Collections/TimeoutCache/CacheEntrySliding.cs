using System;

namespace Microsoft.Exchange.Collections.TimeoutCache
{
	// Token: 0x0200069F RID: 1695
	internal class CacheEntrySliding<K, T> : CacheEntryBase<K, T>
	{
		// Token: 0x06001F2F RID: 7983 RVA: 0x0003ACA4 File Offset: 0x00038EA4
		internal CacheEntrySliding(K key, T value, TimeSpan slidingLiveTime) : base(key, value)
		{
			if (slidingLiveTime <= TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("slidingLiveTime", slidingLiveTime, "value must be positive.");
			}
			this.slidingLiveTime = slidingLiveTime;
			this.expirationUtc = ((slidingLiveTime == TimeSpan.MaxValue) ? DateTime.MaxValue : DateTime.UtcNow.Add(slidingLiveTime));
			this.touchedExpirationUtc = this.expirationUtc;
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06001F30 RID: 7984 RVA: 0x0003AD17 File Offset: 0x00038F17
		internal override DateTime ExpirationUtc
		{
			get
			{
				return this.expirationUtc;
			}
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x0003AD20 File Offset: 0x00038F20
		internal override void OnForceExtend()
		{
			this.expirationUtc = ((this.slidingLiveTime == TimeSpan.MaxValue) ? DateTime.MaxValue : DateTime.UtcNow.Add(this.slidingLiveTime));
			this.touchedExpirationUtc = this.expirationUtc;
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x0003AD6C File Offset: 0x00038F6C
		internal override void OnTouch()
		{
			if (!base.InShouldRemoveCycle)
			{
				this.touchedExpirationUtc = ((this.slidingLiveTime == TimeSpan.MaxValue) ? DateTime.MaxValue : DateTime.UtcNow.Add(this.slidingLiveTime));
			}
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x0003ADB3 File Offset: 0x00038FB3
		internal override bool OnBeforeExpire()
		{
			if (this.touchedExpirationUtc > this.expirationUtc)
			{
				this.expirationUtc = this.touchedExpirationUtc;
				return false;
			}
			return true;
		}

		// Token: 0x04001E91 RID: 7825
		private readonly TimeSpan slidingLiveTime;

		// Token: 0x04001E92 RID: 7826
		private DateTime expirationUtc;

		// Token: 0x04001E93 RID: 7827
		protected DateTime touchedExpirationUtc;
	}
}
