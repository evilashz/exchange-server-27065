using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Data.Directory.IsMemberOfProvider
{
	// Token: 0x020001C0 RID: 448
	internal class AutoRefreshCache<K, V, StateType> : IDisposable where V : CachableItem
	{
		// Token: 0x06001263 RID: 4707 RVA: 0x00058CB8 File Offset: 0x00056EB8
		public AutoRefreshCache(long cacheSizeInBytes, TimeSpan cacheExpirationInterval, TimeSpan cacheCleanupInterval, TimeSpan cachePurgeInterval, TimeSpan refreshInterval, ICacheTracer<K> tracer, ICachePerformanceCounters perfCounters, AutoRefreshCache<K, V, StateType>.CreateEntryDelegate createEntry)
		{
			if (createEntry == null)
			{
				throw new ArgumentNullException("createEntry");
			}
			this.createEntry = createEntry;
			this.tracer = tracer;
			this.cache = new Cache<K, V>(cacheSizeInBytes, cacheExpirationInterval, cacheCleanupInterval, cachePurgeInterval, tracer, perfCounters);
			this.itemsToRefresh = new List<K>();
			this.refreshExpiredEntriesTimer = new GuardedTimer(new TimerCallback(this.RefreshExpiredEntries), null, refreshInterval);
			for (int i = 0; i < this.groupsInADLookupSyncObjects.Length; i++)
			{
				this.groupsInADLookupSyncObjects[i] = new object();
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x00058D53 File Offset: 0x00056F53
		public int Count
		{
			get
			{
				return this.cache.Count;
			}
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x00058D60 File Offset: 0x00056F60
		public V GetValue(StateType state, K key)
		{
			V result = default(V);
			if (!this.disposed)
			{
				bool flag = false;
				if (!this.cache.TryGetValue(key, out result, out flag))
				{
					result = this.CreateAndCache(state, key, false);
				}
				if (flag)
				{
					lock (this.itemsToRefresh)
					{
						if (!this.itemsToRefresh.Contains(key))
						{
							this.itemsToRefresh.Add(key);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x00058DE8 File Offset: 0x00056FE8
		public void Remove(K key)
		{
			lock (this.itemsToRefresh)
			{
				this.itemsToRefresh.Remove(key);
			}
			this.cache.Remove(key);
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00058E3C File Offset: 0x0005703C
		public void Clear()
		{
			this.cache.Clear();
			lock (this.itemsToRefresh)
			{
				this.itemsToRefresh.Clear();
			}
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00058E8C File Offset: 0x0005708C
		public void Dispose()
		{
			this.refreshExpiredEntriesTimer.Dispose(true);
			this.cache.Dispose();
			this.disposed = true;
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00058EAC File Offset: 0x000570AC
		private void RefreshExpiredEntries(object unused)
		{
			List<K> list;
			lock (this.itemsToRefresh)
			{
				list = new List<K>(this.itemsToRefresh);
				this.itemsToRefresh.Clear();
			}
			foreach (K k in list)
			{
				if (this.disposed)
				{
					break;
				}
				if (this.cache.ContainsKey(k))
				{
					try
					{
						this.CreateAndCache(default(StateType), k, true);
					}
					catch (TransientException exception)
					{
						this.tracer.TraceException(string.Format("Encountered a Transient exception while refreshing the expired entry {0}", k), exception);
						lock (this.itemsToRefresh)
						{
							this.itemsToRefresh.Add(k);
						}
					}
				}
			}
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00058FC8 File Offset: 0x000571C8
		private V CreateAndCache(StateType state, K key, bool forceRefreshEntry)
		{
			uint hashCode = (uint)key.GetHashCode();
			uint num = (hashCode & 255U) ^ (hashCode >> 8 & 255U) ^ (hashCode >> 16 & 255U) ^ (hashCode >> 24 & 255U);
			V v = default(V);
			lock (this.groupsInADLookupSyncObjects[(int)((UIntPtr)num)])
			{
				if (!forceRefreshEntry && this.cache.ContainsKey(key))
				{
					bool flag2;
					this.cache.TryGetValue(key, out v, out flag2);
				}
				else
				{
					v = this.createEntry(state, key);
					if (!this.disposed)
					{
						this.cache.TryAdd(key, v);
					}
				}
			}
			return v;
		}

		// Token: 0x04000A9E RID: 2718
		private const int GroupsInADLookupSyncObjectsCount = 256;

		// Token: 0x04000A9F RID: 2719
		private readonly ICacheTracer<K> tracer;

		// Token: 0x04000AA0 RID: 2720
		private Cache<K, V> cache;

		// Token: 0x04000AA1 RID: 2721
		private List<K> itemsToRefresh;

		// Token: 0x04000AA2 RID: 2722
		private AutoRefreshCache<K, V, StateType>.CreateEntryDelegate createEntry;

		// Token: 0x04000AA3 RID: 2723
		private GuardedTimer refreshExpiredEntriesTimer;

		// Token: 0x04000AA4 RID: 2724
		private object[] groupsInADLookupSyncObjects = new object[256];

		// Token: 0x04000AA5 RID: 2725
		private bool disposed;

		// Token: 0x020001C1 RID: 449
		// (Invoke) Token: 0x0600126C RID: 4716
		public delegate V CreateEntryDelegate(StateType state, K key);
	}
}
