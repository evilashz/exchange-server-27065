using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;

namespace Microsoft.Exchange.Collections.TimeoutCache
{
	// Token: 0x02000052 RID: 82
	internal class TimeoutCacheBucket<K, T> : IDisposable
	{
		// Token: 0x0600021C RID: 540 RVA: 0x0000A2C4 File Offset: 0x000084C4
		public TimeoutCacheBucket(ShouldRemoveDelegate<K, T> shouldRemoveDelegate, int cacheSizeLimit, bool callbackOnDispose)
		{
			this.InitTimer();
			this.shouldRemoveDelegate = shouldRemoveDelegate;
			this.cacheSizeLimit = cacheSizeLimit;
			this.callbackOnDispose = callbackOnDispose;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000A31E File Offset: 0x0000851E
		internal int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000A32B File Offset: 0x0000852B
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000A33C File Offset: 0x0000853C
		internal T Remove(K key)
		{
			CacheEntry<K, T> cacheEntry;
			lock (this.instanceLock)
			{
				if (this.items.TryGetValue(key, out cacheEntry))
				{
					this.InternalRemoveItem(cacheEntry, RemoveReason.Removed);
				}
			}
			if (cacheEntry != null)
			{
				return cacheEntry.Value;
			}
			return default(T);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000A3A4 File Offset: 0x000085A4
		internal bool Contains(K key)
		{
			bool result;
			lock (this.instanceLock)
			{
				result = this.items.ContainsKey(key);
			}
			return result;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000A3EC File Offset: 0x000085EC
		internal T Get(K key)
		{
			T result;
			if (!this.TryGetValue(key, out result))
			{
				throw new KeyNotFoundException();
			}
			return result;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000A40C File Offset: 0x0000860C
		internal bool TryGetValue(K key, out T value)
		{
			lock (this.instanceLock)
			{
				CacheEntry<K, T> cacheEntry;
				if (this.items.TryGetValue(key, out cacheEntry))
				{
					if (cacheEntry.TimeoutType == TimeoutType.Sliding)
					{
						this.InternalRemoveFromSortedList(cacheEntry);
						cacheEntry.Extend();
						this.InternalAddToSortedList(cacheEntry);
					}
					value = cacheEntry.Value;
					return true;
				}
			}
			value = default(T);
			return false;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000A490 File Offset: 0x00008690
		internal void AddAbsolute(K key, T value, TimeSpan expiration, RemoveItemDelegate<K, T> callback)
		{
			CacheEntry<K, T> entry = CacheEntry<K, T>.CreateAbsolute(expiration, key, value, callback);
			this.InternalAdd(entry);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000A4B0 File Offset: 0x000086B0
		internal void AddAbsolute(K key, T value, DateTime absoluteExpiration, RemoveItemDelegate<K, T> callback)
		{
			CacheEntry<K, T> entry = CacheEntry<K, T>.CreateAbsolute(absoluteExpiration, key, value, callback);
			this.InternalAdd(entry);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000A4D0 File Offset: 0x000086D0
		internal void InsertAbsolute(K key, T value, DateTime absoluteExpiration, RemoveItemDelegate<K, T> callback)
		{
			lock (this.instanceLock)
			{
				this.Remove(key);
				this.AddAbsolute(key, value, absoluteExpiration, callback);
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000A520 File Offset: 0x00008720
		internal void InsertAbsolute(K key, T value, TimeSpan expiration, RemoveItemDelegate<K, T> callback)
		{
			lock (this.instanceLock)
			{
				this.Remove(key);
				this.AddAbsolute(key, value, expiration, callback);
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000A570 File Offset: 0x00008770
		internal void AddSliding(K key, T value, TimeSpan slidingExpiration, RemoveItemDelegate<K, T> callback)
		{
			CacheEntry<K, T> entry = CacheEntry<K, T>.CreateSliding(slidingExpiration, key, value, callback);
			this.InternalAdd(entry);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000A590 File Offset: 0x00008790
		internal void InsertSliding(K key, T value, TimeSpan slidingExpiration, RemoveItemDelegate<K, T> callback)
		{
			lock (this.instanceLock)
			{
				this.Remove(key);
				this.AddSliding(key, value, slidingExpiration, callback);
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000A5E0 File Offset: 0x000087E0
		internal void AddLimitedSliding(K key, T value, TimeSpan absoluteLiveTime, TimeSpan slidingLiveTime, RemoveItemDelegate<K, T> callback)
		{
			CacheEntry<K, T> entry = CacheEntry<K, T>.CreateLimitedSliding(slidingLiveTime, absoluteLiveTime, key, value, callback);
			this.InternalAdd(entry);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000A604 File Offset: 0x00008804
		internal void InsertLimitedSliding(K key, T value, TimeSpan absoluteLiveTime, TimeSpan slidingLiveTime, RemoveItemDelegate<K, T> callback)
		{
			lock (this.instanceLock)
			{
				this.Remove(key);
				this.AddLimitedSliding(key, value, absoluteLiveTime, slidingLiveTime, callback);
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000A654 File Offset: 0x00008854
		internal virtual void Clear()
		{
			List<CacheEntry<K, T>> list = null;
			lock (this.instanceLock)
			{
				if (this.callbackOnDispose)
				{
					foreach (KeyValuePair<K, CacheEntry<K, T>> keyValuePair in this.items)
					{
						if (keyValuePair.Value.Callback != null)
						{
							if (list == null)
							{
								list = new List<CacheEntry<K, T>>();
							}
							list.Add(keyValuePair.Value);
						}
					}
				}
				this.itemsByExpiration.Clear();
				this.items.Clear();
			}
			if (list != null)
			{
				this.FireRemoveCallbackAsync(list, RemoveReason.Cleanup);
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000A71C File Offset: 0x0000891C
		protected virtual void Dispose(bool isDisposing)
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			lock (this.instanceLock)
			{
				this.Clear();
				this.DisposeTimer();
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000A774 File Offset: 0x00008974
		private CacheEntry<K, T> AddEntryToChain(CacheEntry<K, T> chain, CacheEntry<K, T> entryToAdd)
		{
			if (chain == null)
			{
				return entryToAdd;
			}
			while (chain.Next != null)
			{
				chain = chain.Next;
			}
			chain.Next = entryToAdd;
			entryToAdd.Previous = chain;
			return chain;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000A79C File Offset: 0x0000899C
		private void InternalRemoveItemsByExpiration(DateTime expiration, RemoveReason reason)
		{
			List<CacheEntry<K, T>> list = null;
			lock (this.instanceLock)
			{
				while (this.itemsByExpiration.Count > 0 && this.GetOldestExpiration() <= expiration)
				{
					CacheEntry<K, T> next;
					for (CacheEntry<K, T> cacheEntry = this.PopOldestExpiration(); cacheEntry != null; cacheEntry = next)
					{
						next = cacheEntry.Next;
						cacheEntry.Extend();
						if (this.shouldRemoveDelegate == null || cacheEntry.NextExpirationTime <= expiration || this.shouldRemoveDelegate(cacheEntry.Key, cacheEntry.Value))
						{
							this.items.Remove(cacheEntry.Key);
							if (cacheEntry.Callback != null)
							{
								if (list == null)
								{
									list = new List<CacheEntry<K, T>>();
								}
								list.Add(cacheEntry);
							}
						}
						else
						{
							this.InternalAddToSortedList(cacheEntry);
						}
					}
				}
			}
			if (list != null)
			{
				this.FireRemoveCallbackAsync(list, reason);
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000A888 File Offset: 0x00008A88
		private object InternalRemoveItem(CacheEntry<K, T> entry, RemoveReason reason)
		{
			lock (this.instanceLock)
			{
				this.InternalRemoveFromSortedList(entry);
				this.items.Remove(entry.Key);
			}
			if (entry.Callback != null)
			{
				this.FireRemoveCallbackAsync(entry, reason);
			}
			return entry.Value;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000A8F8 File Offset: 0x00008AF8
		private void RemoveCallbackWorker(object state)
		{
			TimeoutCacheBucket<K, T>.EntryAndReason entryAndReason = state as TimeoutCacheBucket<K, T>.EntryAndReason;
			this.RemoveCallbackSingleEntry(entryAndReason.Entry, entryAndReason.Reason);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000A920 File Offset: 0x00008B20
		private void RemoveCallbackWorkerArray(object state)
		{
			TimeoutCacheBucket<K, T>.EntryListAndReason entryListAndReason = state as TimeoutCacheBucket<K, T>.EntryListAndReason;
			List<CacheEntry<K, T>> entryList = entryListAndReason.EntryList;
			foreach (CacheEntry<K, T> cacheEntry in entryList)
			{
				this.RemoveCallbackSingleEntry(cacheEntry, entryListAndReason.Reason);
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000A984 File Offset: 0x00008B84
		private void RemoveCallbackSingleEntry(CacheEntry<K, T> cacheEntry, RemoveReason reason)
		{
			if (cacheEntry.Callback != null)
			{
				cacheEntry.Callback(cacheEntry.Key, cacheEntry.Value, reason);
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000A9A8 File Offset: 0x00008BA8
		private void InternalAdd(CacheEntry<K, T> entry)
		{
			lock (this.instanceLock)
			{
				if (this.Count >= this.cacheSizeLimit)
				{
					this.PreemptiveExpire();
				}
				if (this.items.ContainsKey(entry.Key))
				{
					throw new DuplicateKeyException();
				}
				this.items.Add(entry.Key, entry);
				this.InternalAddToSortedList(entry);
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000AA28 File Offset: 0x00008C28
		private void PreemptiveExpire()
		{
			if (this.itemsByExpiration.Count == 0)
			{
				lock (this.instanceLock)
				{
					CacheEntry<K, T> entry = null;
					using (Dictionary<K, CacheEntry<K, T>>.KeyCollection.Enumerator enumerator = this.items.Keys.GetEnumerator())
					{
						enumerator.MoveNext();
						entry = this.items[enumerator.Current];
					}
					this.InternalRemoveItem(entry, RemoveReason.PreemptivelyExpired);
					return;
				}
			}
			DateTime expiration = (DateTime.UtcNow > this.GetOldestExpiration()) ? DateTime.UtcNow : this.GetOldestExpiration();
			this.InternalRemoveItemsByExpiration(expiration, RemoveReason.PreemptivelyExpired);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000AAEC File Offset: 0x00008CEC
		private void InternalAddToSortedList(CacheEntry<K, T> entry)
		{
			if (entry.TimeoutType == TimeoutType.Absolute && entry.AbsoluteExpirationTime == DateTime.MaxValue)
			{
				return;
			}
			lock (this.instanceLock)
			{
				this.InternalRemoveFromSortedList(entry);
				CacheEntry<K, T> chain;
				if (!this.itemsByExpiration.TryGetValue(entry.NextExpirationTime, out chain))
				{
					this.itemsByExpiration.Add(entry.NextExpirationTime, entry);
				}
				else
				{
					this.AddEntryToChain(chain, entry);
				}
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000AB7C File Offset: 0x00008D7C
		private void InternalRemoveFromSortedList(CacheEntry<K, T> entryToRemove)
		{
			lock (this.instanceLock)
			{
				CacheEntry<K, T> previous = entryToRemove.Previous;
				CacheEntry<K, T> next = entryToRemove.Next;
				entryToRemove.Previous = null;
				entryToRemove.Next = null;
				if (previous != null)
				{
					previous.Next = next;
				}
				if (next != null)
				{
					next.Previous = previous;
				}
				CacheEntry<K, T> cacheEntry;
				if (this.itemsByExpiration.TryGetValue(entryToRemove.NextExpirationTime, out cacheEntry) && cacheEntry == entryToRemove)
				{
					if (next == null)
					{
						this.itemsByExpiration.Remove(entryToRemove.NextExpirationTime);
					}
					else
					{
						this.itemsByExpiration[cacheEntry.NextExpirationTime] = next;
					}
				}
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000AC2C File Offset: 0x00008E2C
		private void HandleTimer(object sender, ElapsedEventArgs e)
		{
			lock (this.instanceLock)
			{
				if (this.itemsByExpiration.Count > 0)
				{
					this.InternalRemoveItemsByExpiration(DateTime.UtcNow, RemoveReason.Expired);
				}
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000AC80 File Offset: 0x00008E80
		private void FireRemoveCallbackAsync(CacheEntry<K, T> entry, RemoveReason reason)
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.RemoveCallbackWorker), new TimeoutCacheBucket<K, T>.EntryAndReason(entry, reason));
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000AC9B File Offset: 0x00008E9B
		private void FireRemoveCallbackAsync(List<CacheEntry<K, T>> entries, RemoveReason reason)
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.RemoveCallbackWorkerArray), new TimeoutCacheBucket<K, T>.EntryListAndReason(entries, reason));
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000ACB6 File Offset: 0x00008EB6
		private DateTime GetOldestExpiration()
		{
			return this.itemsByExpiration.Keys[0];
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000ACCC File Offset: 0x00008ECC
		private CacheEntry<K, T> PopOldestExpiration()
		{
			CacheEntry<K, T> result = this.itemsByExpiration.Values[0];
			this.itemsByExpiration.RemoveAt(0);
			return result;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000ACF8 File Offset: 0x00008EF8
		private void InitTimer()
		{
			this.timer.AutoReset = true;
			this.timer.Enabled = true;
			this.timer.Interval = 10000.0;
			this.timer.Elapsed += this.HandleTimer;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000AD48 File Offset: 0x00008F48
		private void DisposeTimer()
		{
			this.timer.Dispose();
			this.timer = null;
		}

		// Token: 0x04000164 RID: 356
		internal const int TimerInterval = 10000;

		// Token: 0x04000165 RID: 357
		private Dictionary<K, CacheEntry<K, T>> items = new Dictionary<K, CacheEntry<K, T>>();

		// Token: 0x04000166 RID: 358
		private object instanceLock = new object();

		// Token: 0x04000167 RID: 359
		private ShouldRemoveDelegate<K, T> shouldRemoveDelegate;

		// Token: 0x04000168 RID: 360
		private bool disposed;

		// Token: 0x04000169 RID: 361
		private int cacheSizeLimit;

		// Token: 0x0400016A RID: 362
		private bool callbackOnDispose;

		// Token: 0x0400016B RID: 363
		private SortedList<DateTime, CacheEntry<K, T>> itemsByExpiration = new SortedList<DateTime, CacheEntry<K, T>>();

		// Token: 0x0400016C RID: 364
		private System.Timers.Timer timer = new System.Timers.Timer();

		// Token: 0x02000053 RID: 83
		private class EntryAndReason
		{
			// Token: 0x1700006C RID: 108
			// (get) Token: 0x0600023E RID: 574 RVA: 0x0000AD5C File Offset: 0x00008F5C
			// (set) Token: 0x0600023F RID: 575 RVA: 0x0000AD64 File Offset: 0x00008F64
			public CacheEntry<K, T> Entry { get; private set; }

			// Token: 0x1700006D RID: 109
			// (get) Token: 0x06000240 RID: 576 RVA: 0x0000AD6D File Offset: 0x00008F6D
			// (set) Token: 0x06000241 RID: 577 RVA: 0x0000AD75 File Offset: 0x00008F75
			public RemoveReason Reason { get; private set; }

			// Token: 0x06000242 RID: 578 RVA: 0x0000AD7E File Offset: 0x00008F7E
			public EntryAndReason(CacheEntry<K, T> entry, RemoveReason reason)
			{
				this.Entry = entry;
				this.Reason = reason;
			}
		}

		// Token: 0x02000054 RID: 84
		private class EntryListAndReason
		{
			// Token: 0x1700006E RID: 110
			// (get) Token: 0x06000243 RID: 579 RVA: 0x0000AD94 File Offset: 0x00008F94
			// (set) Token: 0x06000244 RID: 580 RVA: 0x0000AD9C File Offset: 0x00008F9C
			public List<CacheEntry<K, T>> EntryList { get; private set; }

			// Token: 0x1700006F RID: 111
			// (get) Token: 0x06000245 RID: 581 RVA: 0x0000ADA5 File Offset: 0x00008FA5
			// (set) Token: 0x06000246 RID: 582 RVA: 0x0000ADAD File Offset: 0x00008FAD
			public RemoveReason Reason { get; private set; }

			// Token: 0x06000247 RID: 583 RVA: 0x0000ADB6 File Offset: 0x00008FB6
			public EntryListAndReason(List<CacheEntry<K, T>> entryList, RemoveReason reason)
			{
				this.EntryList = entryList;
				this.Reason = reason;
			}
		}
	}
}
