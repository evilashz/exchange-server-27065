using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Directory.ProvisioningCache
{
	// Token: 0x020007A5 RID: 1957
	[Serializable]
	internal class ExpiringCacheObject
	{
		// Token: 0x06006144 RID: 24900 RVA: 0x0014B24C File Offset: 0x0014944C
		public ExpiringCacheObject(TimeSpan expirationTime)
		{
			this.expirationTime = expirationTime;
		}

		// Token: 0x06006145 RID: 24901 RVA: 0x0014B271 File Offset: 0x00149471
		public ExpiringCacheObject(TimeSpan expirationTime, object data) : this(expirationTime)
		{
			this.Data = data;
		}

		// Token: 0x170022C5 RID: 8901
		// (get) Token: 0x06006146 RID: 24902 RVA: 0x0014B281 File Offset: 0x00149481
		// (set) Token: 0x06006147 RID: 24903 RVA: 0x0014B28C File Offset: 0x0014948C
		public object Data
		{
			get
			{
				return this.data;
			}
			set
			{
				IDisposable disposable = value as IDisposable;
				if (disposable != null)
				{
					throw new ArgumentException("IDisposable type object is not allowed to be cached in ProvisioningCache.");
				}
				this.data = value;
				this.hasValue = true;
				this.lastUpdateTime = ExDateTime.UtcNow;
			}
		}

		// Token: 0x170022C6 RID: 8902
		// (get) Token: 0x06006148 RID: 24904 RVA: 0x0014B2C8 File Offset: 0x001494C8
		public bool IsExpired
		{
			get
			{
				if (!this.hasValue)
				{
					return true;
				}
				ExDateTime utcNow = ExDateTime.UtcNow;
				return utcNow - this.lastUpdateTime > this.expirationTime;
			}
		}

		// Token: 0x04004150 RID: 16720
		private TimeSpan expirationTime = TimeSpan.MinValue;

		// Token: 0x04004151 RID: 16721
		private object data;

		// Token: 0x04004152 RID: 16722
		private bool hasValue;

		// Token: 0x04004153 RID: 16723
		private ExDateTime lastUpdateTime = ExDateTime.MinValue;
	}
}
