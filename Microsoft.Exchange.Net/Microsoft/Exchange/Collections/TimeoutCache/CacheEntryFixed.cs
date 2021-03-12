using System;

namespace Microsoft.Exchange.Collections.TimeoutCache
{
	// Token: 0x0200069E RID: 1694
	internal class CacheEntryFixed<K, T> : CacheEntryBase<K, T>
	{
		// Token: 0x06001F2A RID: 7978 RVA: 0x0003AC08 File Offset: 0x00038E08
		internal CacheEntryFixed(K key, T value, TimeSpan absoluteLiveTime) : base(key, value)
		{
			if (absoluteLiveTime <= TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("absoluteLiveTime", value, "value must be greater than zero.");
			}
			this.absoluteLiveTime = absoluteLiveTime;
			this.expirationUtc = ((absoluteLiveTime == TimeSpan.MaxValue) ? DateTime.MaxValue : DateTime.UtcNow.Add(absoluteLiveTime));
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06001F2B RID: 7979 RVA: 0x0003AC6F File Offset: 0x00038E6F
		internal override DateTime ExpirationUtc
		{
			get
			{
				return this.expirationUtc;
			}
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x0003AC78 File Offset: 0x00038E78
		internal override void OnForceExtend()
		{
			this.expirationUtc = DateTime.UtcNow.Add(this.absoluteLiveTime);
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x0003AC9E File Offset: 0x00038E9E
		internal override void OnTouch()
		{
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x0003ACA0 File Offset: 0x00038EA0
		internal override bool OnBeforeExpire()
		{
			return true;
		}

		// Token: 0x04001E8F RID: 7823
		private readonly TimeSpan absoluteLiveTime;

		// Token: 0x04001E90 RID: 7824
		private DateTime expirationUtc;
	}
}
