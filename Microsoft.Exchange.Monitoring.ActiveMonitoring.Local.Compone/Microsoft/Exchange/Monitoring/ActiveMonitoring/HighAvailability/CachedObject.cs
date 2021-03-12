using System;
using System.Threading;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability
{
	// Token: 0x0200018E RID: 398
	public class CachedObject<T>
	{
		// Token: 0x06000B8D RID: 2957 RVA: 0x00049B8C File Offset: 0x00047D8C
		public CachedObject(CachedObject<T>.UpdateMethod<T> updateMethod, TimeSpan expirationTime, bool treatNullAsInvalid = false)
		{
			this.updateMethod = updateMethod;
			this.expirationTime = expirationTime;
			this.lastUpdate = DateTime.MinValue;
			this.treatNullAsInvalid = treatNullAsInvalid;
			this.rwLock = new ReaderWriterLockSlim();
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000B8E RID: 2958 RVA: 0x00049BC0 File Offset: 0x00047DC0
		public T GetValue
		{
			get
			{
				this.rwLock.EnterUpgradeableReadLock();
				T result;
				try
				{
					if (DateTime.UtcNow - this.lastUpdate > this.expirationTime || (this.cachedValue == null && this.treatNullAsInvalid))
					{
						this.rwLock.EnterWriteLock();
						try
						{
							this.cachedValue = this.updateMethod();
							this.lastUpdate = DateTime.UtcNow;
						}
						finally
						{
							this.rwLock.ExitWriteLock();
						}
					}
					result = this.cachedValue;
				}
				finally
				{
					this.rwLock.ExitUpgradeableReadLock();
				}
				return result;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x00049C74 File Offset: 0x00047E74
		public DateTime LastUpdate
		{
			get
			{
				this.rwLock.EnterReadLock();
				DateTime result;
				try
				{
					result = this.lastUpdate;
				}
				finally
				{
					this.rwLock.ExitReadLock();
				}
				return result;
			}
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00049CB4 File Offset: 0x00047EB4
		internal void ForceUpdate()
		{
			this.rwLock.EnterWriteLock();
			try
			{
				this.cachedValue = this.updateMethod();
				this.lastUpdate = DateTime.UtcNow;
			}
			finally
			{
				this.rwLock.ExitWriteLock();
			}
		}

		// Token: 0x040008C5 RID: 2245
		private readonly TimeSpan expirationTime;

		// Token: 0x040008C6 RID: 2246
		private readonly bool treatNullAsInvalid;

		// Token: 0x040008C7 RID: 2247
		private CachedObject<T>.UpdateMethod<T> updateMethod;

		// Token: 0x040008C8 RID: 2248
		private DateTime lastUpdate;

		// Token: 0x040008C9 RID: 2249
		private ReaderWriterLockSlim rwLock;

		// Token: 0x040008CA RID: 2250
		private T cachedValue;

		// Token: 0x0200018F RID: 399
		// (Invoke) Token: 0x06000B92 RID: 2962
		public delegate U UpdateMethod<U>();
	}
}
