using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Common.Cache
{
	// Token: 0x02000673 RID: 1651
	internal class Cache<K, V> : IDisposable where V : CachableItem
	{
		// Token: 0x06001DE1 RID: 7649 RVA: 0x00036824 File Offset: 0x00034A24
		static Cache()
		{
			if (typeof(V).GetInterface(typeof(IDisposable).FullName) != null)
			{
				throw new ArgumentException("Disposable cached item are not supported", "V");
			}
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x0003687A File Offset: 0x00034A7A
		public Cache(long cacheSizeInBytes, TimeSpan cacheExpirationInterval, TimeSpan cacheCleanupInterval) : this(cacheSizeInBytes, cacheExpirationInterval, cacheCleanupInterval, null, null)
		{
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x00036887 File Offset: 0x00034A87
		public Cache(long cacheSizeInBytes, TimeSpan cacheExpirationInterval, TimeSpan cacheCleanupInterval, ICacheTracer<K> tracer, ICachePerformanceCounters perfCounters) : this(cacheSizeInBytes, cacheExpirationInterval, cacheCleanupInterval, Cache<K, V>.DefaultPurgeInterval, tracer, perfCounters)
		{
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x0003689C File Offset: 0x00034A9C
		public Cache(long cacheSizeInBytes, TimeSpan cacheExpirationInterval, TimeSpan cacheCleanupInterval, TimeSpan cachePurgeInterval, ICacheTracer<K> tracer, ICachePerformanceCounters perfCounters)
		{
			if (cacheSizeInBytes < 0L)
			{
				throw new ArgumentOutOfRangeException("cacheSizeInBytes", cacheSizeInBytes, "cacheSizeInBytes must be greater than or equal to 0 bytes");
			}
			if (cacheExpirationInterval.TotalSeconds < 0.0)
			{
				throw new ArgumentOutOfRangeException("cacheExpirationInterval", cacheExpirationInterval, "Expire time must be greater than or equal to 0 seconds");
			}
			if (cacheCleanupInterval.TotalSeconds < 0.0)
			{
				throw new ArgumentOutOfRangeException("cacheCleanupInterval", cacheCleanupInterval, "Cleanup time must be greater than or equal to 0 seconds");
			}
			if (cachePurgeInterval.TotalSeconds <= 0.0)
			{
				throw new ArgumentOutOfRangeException("cachePurgeInterval", cachePurgeInterval, "Purge time must be greater than 0 seconds");
			}
			this.cacheTracer = (tracer ?? new Cache<K, V>.NoopCacheTracer());
			this.cachePerfCounters = (perfCounters ?? new Cache<K, V>.NoopCachePerformanceCounters());
			this.maxCacheSize = cacheSizeInBytes;
			this.cacheExpirationInterval = cacheExpirationInterval;
			this.cleanupInterval = cacheCleanupInterval + cacheExpirationInterval;
			this.currentSize = 0L;
			this.memoryCache = new Dictionary<K, Cache<K, V>.CacheItemWrapper>();
			this.mruList = new Cache<K, V>.MruList();
			this.cleanupTimer = new GuardedTimer(new TimerCallback(this.HandleCleanUp), null, cachePurgeInterval);
		}

		// Token: 0x1400007E RID: 126
		// (add) Token: 0x06001DE5 RID: 7653 RVA: 0x000369C4 File Offset: 0x00034BC4
		// (remove) Token: 0x06001DE6 RID: 7654 RVA: 0x000369FC File Offset: 0x00034BFC
		public event Cache<K, V>.OnRemovedEventHandler OnRemoved;

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06001DE7 RID: 7655 RVA: 0x00036A31 File Offset: 0x00034C31
		public int Count
		{
			get
			{
				return this.memoryCache.Count;
			}
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x00036A40 File Offset: 0x00034C40
		public virtual bool TryAdd(K key, V value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			long itemSize = value.ItemSize;
			if (itemSize > this.maxCacheSize || this.maxCacheSize == 0L)
			{
				return false;
			}
			DateTime utcNow;
			lock (this.syncObject)
			{
				utcNow = DateTime.UtcNow;
				if (this.memoryCache.ContainsKey(key))
				{
					this.Remove(key, CacheItemRemovedReason.OverWritten);
				}
				Cache<K, V>.CacheItemWrapper cacheItemWrapper;
				while (this.currentSize + itemSize > this.maxCacheSize)
				{
					cacheItemWrapper = this.mruList.Oldest;
					this.Remove(cacheItemWrapper.ItemKey, CacheItemRemovedReason.Scavenged);
				}
				cacheItemWrapper = new Cache<K, V>.CacheItemWrapper(key, value);
				this.mruList.Add(cacheItemWrapper);
				this.memoryCache[key] = cacheItemWrapper;
				this.currentSize += itemSize;
			}
			this.cacheTracer.ItemAdded(key, value, utcNow);
			this.cachePerfCounters.SizeUpdated(this.currentSize);
			return true;
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x00036B60 File Offset: 0x00034D60
		public void Add(K key, V value)
		{
			if (!this.TryAdd(key, value))
			{
				throw new ArgumentException("Value cannot be added for given key");
			}
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x00036B77 File Offset: 0x00034D77
		public void Remove(K key)
		{
			this.Remove(key, CacheItemRemovedReason.Removed);
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x00036B84 File Offset: 0x00034D84
		public bool GetAllValues(out ICollection<V> values)
		{
			values = null;
			bool flag = false;
			List<V> list = new List<V>();
			if (!this.disposed)
			{
				lock (this.syncObject)
				{
					foreach (K key in new List<K>(this.memoryCache.Keys))
					{
						V item;
						bool flag3;
						if (this.TryGetValue(key, out item, out flag3))
						{
							list.Add(item);
						}
						else
						{
							flag3 = true;
						}
						if (flag3 && !flag)
						{
							flag = true;
						}
					}
				}
			}
			values = list.AsReadOnly();
			return flag;
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x00036C48 File Offset: 0x00034E48
		public V GetValue(K key)
		{
			bool flag;
			return this.GetValue(key, out flag);
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x00036C60 File Offset: 0x00034E60
		public V GetValue(K key, out bool hasExpired)
		{
			V result;
			if (!this.TryGetValue(key, out result, out hasExpired))
			{
				throw new ArgumentException("Value does not exist for given key");
			}
			return result;
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x00036C88 File Offset: 0x00034E88
		public bool TryGetValue(K key, out V value)
		{
			bool flag;
			return this.TryGetValue(key, out value, out flag);
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x00036CA0 File Offset: 0x00034EA0
		public bool TryGetValue(K key, out V value, out bool hasExpired)
		{
			value = default(V);
			hasExpired = false;
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			AccessStatus accessStatus = AccessStatus.Miss;
			if (!this.disposed)
			{
				DateTime utcNow;
				lock (this.syncObject)
				{
					utcNow = DateTime.UtcNow;
					Cache<K, V>.CacheItemWrapper cacheItemWrapper;
					if (this.memoryCache.TryGetValue(key, out cacheItemWrapper) && cacheItemWrapper != null)
					{
						if (!(cacheItemWrapper.CreationTime < utcNow.Subtract(this.cleanupInterval)))
						{
							V cacheItem = cacheItemWrapper.CacheItem;
							if (!cacheItem.IsExpired(utcNow))
							{
								this.mruList.Remove(cacheItemWrapper);
								this.mruList.Add(cacheItemWrapper);
								value = cacheItemWrapper.CacheItem;
								if (cacheItemWrapper.CreationTime < utcNow.Subtract(this.cacheExpirationInterval))
								{
									hasExpired = true;
								}
								accessStatus = AccessStatus.Hit;
								goto IL_D7;
							}
						}
						this.Remove(key, CacheItemRemovedReason.Expired);
					}
					IL_D7:;
				}
				this.cacheTracer.Accessed(key, value, accessStatus, utcNow);
				this.cachePerfCounters.Accessed(accessStatus);
			}
			return accessStatus == AccessStatus.Hit;
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x00036DCC File Offset: 0x00034FCC
		public virtual bool ContainsKey(K key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (!this.disposed)
			{
				lock (this.syncObject)
				{
					Cache<K, V>.CacheItemWrapper cacheItemWrapper;
					return this.memoryCache.TryGetValue(key, out cacheItemWrapper) && cacheItemWrapper != null;
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x00036E40 File Offset: 0x00035040
		public void Clear()
		{
			Dictionary<K, Cache<K, V>.CacheItemWrapper> dictionary = null;
			DateTime utcNow;
			lock (this.syncObject)
			{
				utcNow = DateTime.UtcNow;
				this.currentSize = 0L;
				dictionary = this.memoryCache;
				this.memoryCache = new Dictionary<K, Cache<K, V>.CacheItemWrapper>();
				this.mruList.Clear();
			}
			this.cacheTracer.Flushed(this.maxCacheSize, utcNow);
			this.cachePerfCounters.SizeUpdated(this.currentSize);
			if (this.OnRemoved != null)
			{
				foreach (KeyValuePair<K, Cache<K, V>.CacheItemWrapper> keyValuePair in dictionary)
				{
					this.RaiseOnRemovedEvent(keyValuePair.Key, keyValuePair.Value.CacheItem, CacheItemRemovedReason.Clear);
				}
			}
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x00036F28 File Offset: 0x00035128
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.cleanupTimer.Dispose(true);
				this.disposed = true;
				this.Clear();
			}
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x00036F4C File Offset: 0x0003514C
		private void HandleCleanUp(object state)
		{
			lock (this.syncObject)
			{
				List<K> list = new List<K>();
				DateTime utcNow = DateTime.UtcNow;
				DateTime t = utcNow.Subtract(this.cleanupInterval);
				foreach (KeyValuePair<K, Cache<K, V>.CacheItemWrapper> keyValuePair in this.memoryCache)
				{
					Cache<K, V>.CacheItemWrapper value = keyValuePair.Value;
					if (!(value.CreationTime < t))
					{
						V cacheItem = value.CacheItem;
						if (!cacheItem.IsExpired(utcNow))
						{
							continue;
						}
					}
					list.Add(value.ItemKey);
				}
				foreach (K key in list)
				{
					this.Remove(key, CacheItemRemovedReason.Expired);
				}
			}
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x00037060 File Offset: 0x00035260
		private void Remove(K key, CacheItemRemovedReason removalReason)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (!this.disposed)
			{
				bool flag = false;
				Cache<K, V>.CacheItemWrapper cacheItemWrapper = null;
				DateTime utcNow;
				lock (this.syncObject)
				{
					utcNow = DateTime.UtcNow;
					if (this.memoryCache.TryGetValue(key, out cacheItemWrapper))
					{
						this.mruList.Remove(cacheItemWrapper);
						this.memoryCache.Remove(key);
						long num = this.currentSize;
						V cacheItem = cacheItemWrapper.CacheItem;
						this.currentSize = num - cacheItem.ItemSize;
						flag = true;
					}
				}
				if (flag)
				{
					this.cacheTracer.ItemRemoved(key, cacheItemWrapper.CacheItem, removalReason, utcNow);
					this.cachePerfCounters.SizeUpdated(this.currentSize);
					this.RaiseOnRemovedEvent(key, cacheItemWrapper.CacheItem, removalReason);
				}
			}
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x0003714C File Offset: 0x0003534C
		private void RaiseOnRemovedEvent(K key, V value, CacheItemRemovedReason removalReason)
		{
			Cache<K, V>.OnRemovedEventHandler onRemoved = this.OnRemoved;
			if (onRemoved != null)
			{
				OnRemovedEventArgs<K, V> e = new OnRemovedEventArgs<K, V>(key, value, removalReason);
				onRemoved(this, e);
			}
		}

		// Token: 0x04001E0E RID: 7694
		public static readonly TimeSpan DefaultPurgeInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x04001E0F RID: 7695
		private readonly long maxCacheSize;

		// Token: 0x04001E10 RID: 7696
		private readonly TimeSpan cacheExpirationInterval;

		// Token: 0x04001E11 RID: 7697
		private readonly ICacheTracer<K> cacheTracer;

		// Token: 0x04001E12 RID: 7698
		private readonly ICachePerformanceCounters cachePerfCounters;

		// Token: 0x04001E13 RID: 7699
		private readonly TimeSpan cleanupInterval;

		// Token: 0x04001E14 RID: 7700
		private long currentSize;

		// Token: 0x04001E15 RID: 7701
		private GuardedTimer cleanupTimer;

		// Token: 0x04001E16 RID: 7702
		private bool disposed;

		// Token: 0x04001E17 RID: 7703
		private object syncObject = new object();

		// Token: 0x04001E18 RID: 7704
		private Dictionary<K, Cache<K, V>.CacheItemWrapper> memoryCache;

		// Token: 0x04001E19 RID: 7705
		private Cache<K, V>.MruList mruList;

		// Token: 0x02000674 RID: 1652
		// (Invoke) Token: 0x06001DF7 RID: 7671
		public delegate void OnRemovedEventHandler(object sender, OnRemovedEventArgs<K, V> e);

		// Token: 0x02000675 RID: 1653
		[DebuggerDisplay("{itemKey.ToString()}, Created = {creationTime.ToString()}")]
		private class CacheItemWrapper
		{
			// Token: 0x06001DFA RID: 7674 RVA: 0x00037174 File Offset: 0x00035374
			public CacheItemWrapper(K itemKey, V cacheItem)
			{
				this.creationTime = DateTime.UtcNow;
				this.cacheItem = cacheItem;
				this.itemKey = itemKey;
			}

			// Token: 0x170007FF RID: 2047
			// (get) Token: 0x06001DFB RID: 7675 RVA: 0x00037195 File Offset: 0x00035395
			public DateTime CreationTime
			{
				get
				{
					return this.creationTime;
				}
			}

			// Token: 0x17000800 RID: 2048
			// (get) Token: 0x06001DFC RID: 7676 RVA: 0x0003719D File Offset: 0x0003539D
			public K ItemKey
			{
				get
				{
					return this.itemKey;
				}
			}

			// Token: 0x17000801 RID: 2049
			// (get) Token: 0x06001DFD RID: 7677 RVA: 0x000371A5 File Offset: 0x000353A5
			public V CacheItem
			{
				get
				{
					return this.cacheItem;
				}
			}

			// Token: 0x17000802 RID: 2050
			// (get) Token: 0x06001DFE RID: 7678 RVA: 0x000371AD File Offset: 0x000353AD
			// (set) Token: 0x06001DFF RID: 7679 RVA: 0x000371B5 File Offset: 0x000353B5
			public Cache<K, V>.CacheItemWrapper Next
			{
				get
				{
					return this.next;
				}
				set
				{
					this.next = value;
				}
			}

			// Token: 0x17000803 RID: 2051
			// (get) Token: 0x06001E00 RID: 7680 RVA: 0x000371BE File Offset: 0x000353BE
			// (set) Token: 0x06001E01 RID: 7681 RVA: 0x000371C6 File Offset: 0x000353C6
			public Cache<K, V>.CacheItemWrapper Previous
			{
				get
				{
					return this.previous;
				}
				set
				{
					this.previous = value;
				}
			}

			// Token: 0x04001E1B RID: 7707
			private DateTime creationTime;

			// Token: 0x04001E1C RID: 7708
			private V cacheItem;

			// Token: 0x04001E1D RID: 7709
			private K itemKey;

			// Token: 0x04001E1E RID: 7710
			private Cache<K, V>.CacheItemWrapper next;

			// Token: 0x04001E1F RID: 7711
			private Cache<K, V>.CacheItemWrapper previous;
		}

		// Token: 0x02000676 RID: 1654
		private class MruList
		{
			// Token: 0x17000804 RID: 2052
			// (get) Token: 0x06001E02 RID: 7682 RVA: 0x000371CF File Offset: 0x000353CF
			public Cache<K, V>.CacheItemWrapper Oldest
			{
				get
				{
					return this.tail;
				}
			}

			// Token: 0x06001E03 RID: 7683 RVA: 0x000371D7 File Offset: 0x000353D7
			public void Add(Cache<K, V>.CacheItemWrapper item)
			{
				item.Next = this.head;
				item.Previous = null;
				if (this.head != null)
				{
					this.head.Previous = item;
				}
				this.head = item;
				if (this.tail == null)
				{
					this.tail = item;
				}
			}

			// Token: 0x06001E04 RID: 7684 RVA: 0x00037218 File Offset: 0x00035418
			public void Remove(Cache<K, V>.CacheItemWrapper item)
			{
				if (this.head == null || this.tail == null)
				{
					throw new InvalidOperationException("Cannot remove from an empty list");
				}
				if (item.Previous != null)
				{
					item.Previous.Next = item.Next;
				}
				else
				{
					this.head = this.head.Next;
				}
				if (item.Next != null)
				{
					item.Next.Previous = item.Previous;
				}
				else
				{
					this.tail = this.tail.Previous;
				}
				item.Previous = null;
				item.Next = null;
			}

			// Token: 0x06001E05 RID: 7685 RVA: 0x000372A6 File Offset: 0x000354A6
			public void Clear()
			{
				this.head = null;
				this.tail = null;
			}

			// Token: 0x06001E06 RID: 7686 RVA: 0x000372B8 File Offset: 0x000354B8
			[Conditional("DEBUG")]
			private void Validate(Cache<K, V>.CacheItemWrapper item)
			{
				if (this.head != null && this.head.Previous != null)
				{
					throw new InvalidOperationException("Head does not point to the start of the list.");
				}
				if (this.tail != null && this.tail.Next != null)
				{
					throw new InvalidOperationException("Tail does not point to the end of the list.");
				}
				if ((this.head == null && this.tail != null) || (this.head != null && this.head == null))
				{
					throw new InvalidOperationException("Head and tail are inconsistently null.");
				}
				if (this.head != null)
				{
					bool flag = false;
					bool flag2 = false;
					for (Cache<K, V>.CacheItemWrapper cacheItemWrapper = this.head; cacheItemWrapper != null; cacheItemWrapper = cacheItemWrapper.Next)
					{
						flag = false;
						if (cacheItemWrapper == item)
						{
							flag2 = true;
						}
						if (cacheItemWrapper == this.tail)
						{
							flag = true;
						}
					}
					if (item != null && !flag2)
					{
						throw new InvalidOperationException("Item not reachable from head.");
					}
					if (!flag)
					{
						throw new InvalidOperationException("Tail not reachable from head.");
					}
					bool flag3 = false;
					flag2 = false;
					for (Cache<K, V>.CacheItemWrapper cacheItemWrapper = this.tail; cacheItemWrapper != null; cacheItemWrapper = cacheItemWrapper.Previous)
					{
						flag3 = false;
						if (cacheItemWrapper == item)
						{
							flag2 = true;
						}
						if (cacheItemWrapper == this.head)
						{
							flag3 = true;
						}
					}
					if (item != null && !flag2)
					{
						throw new InvalidOperationException("Item not reachable from tail.");
					}
					if (!flag3)
					{
						throw new InvalidOperationException("Head not reachable from tail.");
					}
				}
			}

			// Token: 0x04001E20 RID: 7712
			private Cache<K, V>.CacheItemWrapper head;

			// Token: 0x04001E21 RID: 7713
			private Cache<K, V>.CacheItemWrapper tail;
		}

		// Token: 0x02000678 RID: 1656
		private class NoopCacheTracer : ICacheTracer<K>
		{
			// Token: 0x06001E0D RID: 7693 RVA: 0x000373D5 File Offset: 0x000355D5
			public void Accessed(K key, CachableItem value, AccessStatus accessStatus, DateTime timestamp)
			{
			}

			// Token: 0x06001E0E RID: 7694 RVA: 0x000373D7 File Offset: 0x000355D7
			public void Flushed(long cacheSize, DateTime timestamp)
			{
			}

			// Token: 0x06001E0F RID: 7695 RVA: 0x000373D9 File Offset: 0x000355D9
			public void ItemAdded(K key, CachableItem value, DateTime timestamp)
			{
			}

			// Token: 0x06001E10 RID: 7696 RVA: 0x000373DB File Offset: 0x000355DB
			public void ItemRemoved(K key, CachableItem value, CacheItemRemovedReason removeReason, DateTime timestamp)
			{
			}

			// Token: 0x06001E11 RID: 7697 RVA: 0x000373DD File Offset: 0x000355DD
			public void TraceException(string details, Exception exception)
			{
			}
		}

		// Token: 0x0200067A RID: 1658
		private class NoopCachePerformanceCounters : ICachePerformanceCounters
		{
			// Token: 0x06001E15 RID: 7701 RVA: 0x000373E7 File Offset: 0x000355E7
			public void Accessed(AccessStatus accessStatus)
			{
			}

			// Token: 0x06001E16 RID: 7702 RVA: 0x000373E9 File Offset: 0x000355E9
			public void SizeUpdated(long cacheSize)
			{
			}
		}
	}
}
