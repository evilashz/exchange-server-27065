using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x0200001A RID: 26
	internal class SimpleCache<TKey, TValue>
	{
		// Token: 0x06000099 RID: 153 RVA: 0x0000A654 File Offset: 0x00008854
		internal bool TryGetValue(TKey key, out TValue value)
		{
			bool result;
			try
			{
				this.rwLock.AcquireReaderLock(-1);
				this.FlushIfNeeded();
				result = this.cacheDictionary.TryGetValue(key, out value);
			}
			finally
			{
				this.rwLock.ReleaseReaderLock();
			}
			return result;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000A6A0 File Offset: 0x000088A0
		internal void AddIgnoringDups(TKey key, TValue value)
		{
			try
			{
				this.rwLock.AcquireWriterLock(-1);
				if (this.cacheDictionary.Count == 0)
				{
					this.nextCacheFlushTime = DateTime.UtcNow.Add(SimpleCache<TKey, TValue>.CacheFlushInterval);
				}
				if (!this.cacheDictionary.ContainsKey(key))
				{
					this.cacheDictionary.Add(key, value);
				}
			}
			finally
			{
				this.rwLock.ReleaseWriterLock();
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000A718 File Offset: 0x00008918
		private void FlushIfNeeded()
		{
			if (DateTime.UtcNow > this.nextCacheFlushTime)
			{
				this.nextCacheFlushTime = DateTime.UtcNow.Add(SimpleCache<TKey, TValue>.CacheFlushInterval);
				LockCookie lockCookie = this.rwLock.UpgradeToWriterLock(-1);
				this.cacheDictionary.Clear();
				this.rwLock.DowngradeFromWriterLock(ref lockCookie);
			}
		}

		// Token: 0x040000AB RID: 171
		private static readonly TimeSpan CacheFlushInterval = new TimeSpan(0, 10, 0);

		// Token: 0x040000AC RID: 172
		private Dictionary<TKey, TValue> cacheDictionary = new Dictionary<TKey, TValue>();

		// Token: 0x040000AD RID: 173
		private ReaderWriterLock rwLock = new ReaderWriterLock();

		// Token: 0x040000AE RID: 174
		private DateTime nextCacheFlushTime = DateTime.UtcNow;
	}
}
