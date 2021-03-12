using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Collections.TimeoutCache;

namespace Microsoft.Exchange.SharedCache
{
	// Token: 0x02000009 RID: 9
	internal sealed class SharedTimeoutCache : SharedCacheBase
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002A1B File Offset: 0x00000C1B
		public SharedTimeoutCache(CacheSettings cacheSettings) : base(cacheSettings)
		{
			this.internalCache = new ExactTimeoutCache<string, CacheEntry>(null, null, null, cacheSettings.Size, false);
			this.syncLock = new ReaderWriterLockSlim();
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002A44 File Offset: 0x00000C44
		public override long Count
		{
			get
			{
				return (long)this.internalCache.Count;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002A52 File Offset: 0x00000C52
		public override void Dispose()
		{
			base.Dispose();
			if (this.internalCache != null)
			{
				this.internalCache.Dispose();
				this.internalCache = null;
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002A94 File Offset: 0x00000C94
		internal override KeyValuePair<string, CacheEntry>[] InternalSearch(string keyWildcard)
		{
			KeyValuePair<string, CacheEntry>[] array2;
			try
			{
				this.syncLock.EnterReadLock();
				string[] array = (from k in this.internalCache.Keys
				where k.IndexOf(keyWildcard, StringComparison.OrdinalIgnoreCase) >= 0
				select k).ToArray<string>();
				array2 = new KeyValuePair<string, CacheEntry>[array.Length];
				int num = 0;
				foreach (string key in array)
				{
					CacheEntry value = null;
					if (this.internalCache.TryGetValue(key, out value))
					{
						array2[num++] = new KeyValuePair<string, CacheEntry>(key, value);
					}
				}
			}
			finally
			{
				try
				{
					this.syncLock.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			return array2;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002B6C File Offset: 0x00000D6C
		internal override CacheEntry InternalGet(string key)
		{
			CacheEntry result = null;
			try
			{
				this.syncLock.EnterReadLock();
				this.internalCache.TryGetValue(key, out result);
			}
			finally
			{
				try
				{
					this.syncLock.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			return result;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002BC8 File Offset: 0x00000DC8
		internal override void InternalInsert(string key, CacheEntry cacheEntryToInsert)
		{
			this.syncLock.EnterWriteLock();
			try
			{
				this.internalCache.TryInsertAbsolute(key, cacheEntryToInsert, base.Settings.EntryTimeout);
			}
			finally
			{
				try
				{
					this.syncLock.ExitWriteLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002C28 File Offset: 0x00000E28
		internal override CacheEntry InternalDelete(string key)
		{
			CacheEntry cacheEntry = null;
			try
			{
				this.syncLock.EnterUpgradeableReadLock();
				this.internalCache.TryGetValue(key, out cacheEntry);
				if (cacheEntry != null)
				{
					try
					{
						this.syncLock.EnterWriteLock();
						this.internalCache.Remove(key);
					}
					finally
					{
						try
						{
							this.syncLock.ExitWriteLock();
						}
						catch (SynchronizationLockException)
						{
						}
					}
				}
			}
			finally
			{
				try
				{
					this.syncLock.ExitUpgradeableReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			return cacheEntry;
		}

		// Token: 0x0400001B RID: 27
		private ExactTimeoutCache<string, CacheEntry> internalCache;

		// Token: 0x0400001C RID: 28
		private ReaderWriterLockSlim syncLock;
	}
}
