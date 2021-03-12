using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000F23 RID: 3875
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UpdatableCache<K, T> where K : class where T : IUpdatableItem
	{
		// Token: 0x06008543 RID: 34115 RVA: 0x002472CA File Offset: 0x002454CA
		public UpdatableCache(int cacheCapacity, double timeForUpdateInSeconds)
		{
			this.MaxCount = ((cacheCapacity < 1) ? 1 : cacheCapacity);
			this.TimeForUpdateInSeconds = timeForUpdateInSeconds;
			this.CachedItems = new Dictionary<K, UpdatableCacheEntry<T>>(this.MaxCount);
		}

		// Token: 0x06008544 RID: 34116 RVA: 0x00247314 File Offset: 0x00245514
		public bool UpdateCacheEntry(K key, ref T value, DateTime expirationUtc)
		{
			if (key == null || value == null)
			{
				return false;
			}
			lock (this.Lock)
			{
				UpdatableCacheEntry<T> updatableCacheEntry;
				if (this.CachedItems.TryGetValue(key, out updatableCacheEntry))
				{
					if (!updatableCacheEntry.UpdateCachedItem(value, expirationUtc))
					{
						T t;
						updatableCacheEntry.GetCachedItem(out t, DateTime.UtcNow);
						value = t;
						return false;
					}
				}
				else
				{
					if (this.CachedItems.Count == this.MaxCount)
					{
						K key2 = (from pair in this.CachedItems
						orderby pair.Value.UtcOfExpiration
						select pair).First<KeyValuePair<K, UpdatableCacheEntry<T>>>().Key;
						this.CachedItems.Remove(key2);
					}
					this.CachedItems[key] = new UpdatableCacheEntry<T>(value, expirationUtc, this.TimeForUpdateInSeconds);
				}
			}
			return true;
		}

		// Token: 0x06008545 RID: 34117 RVA: 0x00247420 File Offset: 0x00245620
		public bool GetCacheEntry(K key, out T value, out bool expired, DateTime currentUtcTime)
		{
			value = default(T);
			expired = false;
			if (key == null)
			{
				return false;
			}
			lock (this.Lock)
			{
				UpdatableCacheEntry<T> updatableCacheEntry;
				if (this.CachedItems.TryGetValue(key, out updatableCacheEntry))
				{
					expired = updatableCacheEntry.GetCachedItem(out value, currentUtcTime);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400594A RID: 22858
		protected readonly int MaxCount;

		// Token: 0x0400594B RID: 22859
		protected readonly double TimeForUpdateInSeconds;

		// Token: 0x0400594C RID: 22860
		protected object Lock = new object();

		// Token: 0x0400594D RID: 22861
		protected readonly Dictionary<K, UpdatableCacheEntry<T>> CachedItems;
	}
}
