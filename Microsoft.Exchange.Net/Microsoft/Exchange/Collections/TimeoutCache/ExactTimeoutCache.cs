using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Collections.TimeoutCache
{
	// Token: 0x02000057 RID: 87
	internal class ExactTimeoutCache<K, T> : IDisposable
	{
		// Token: 0x0600024C RID: 588 RVA: 0x0000ADCC File Offset: 0x00008FCC
		public ExactTimeoutCache(RemoveItemDelegate<K, T> removeItemDelegate, ShouldRemoveDelegate<K, T> shouldRemoveDelegate, UnhandledExceptionDelegate unhandledExceptionDelegate, int cacheSizeLimit, bool callbackOnDispose) : this(removeItemDelegate, shouldRemoveDelegate, unhandledExceptionDelegate, cacheSizeLimit, callbackOnDispose, CacheFullBehavior.ExpireExisting)
		{
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000ADDC File Offset: 0x00008FDC
		public ExactTimeoutCache(RemoveItemDelegate<K, T> removeItemDelegate, ShouldRemoveDelegate<K, T> shouldRemoveDelegate, UnhandledExceptionDelegate unhandledExceptionDelegate, int cacheSizeLimit, bool callbackOnDispose, CacheFullBehavior cacheFullBehavior)
		{
			this.removeItemDelegate = removeItemDelegate;
			this.shouldRemoveDelegate = shouldRemoveDelegate;
			this.cacheSizeLimit = cacheSizeLimit;
			this.callbackOnDispose = callbackOnDispose;
			this.unhandledExceptionDelegate = unhandledExceptionDelegate;
			this.cacheFullBehavior = cacheFullBehavior;
			this.CreateWorkerThread();
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000AE74 File Offset: 0x00009074
		~ExactTimeoutCache()
		{
			this.Dispose(false);
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000AEA4 File Offset: 0x000090A4
		internal int Count
		{
			get
			{
				bool flag = false;
				int count;
				try
				{
					flag = this.readerWriterLock.TryEnterReadLock(-1);
					count = this.items.Count;
				}
				finally
				{
					if (flag || this.readerWriterLock.IsReadLockHeld)
					{
						this.readerWriterLock.ExitReadLock();
					}
				}
				return count;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000AEFC File Offset: 0x000090FC
		internal List<K> Keys
		{
			get
			{
				bool flag = false;
				List<K> result;
				try
				{
					flag = this.readerWriterLock.TryEnterReadLock(-1);
					List<K> list = new List<K>(this.items.Count);
					foreach (KeyValuePair<K, CacheEntryBase<K, T>> keyValuePair in this.items)
					{
						list.Add(keyValuePair.Key);
					}
					result = list;
				}
				finally
				{
					if (flag || this.readerWriterLock.IsReadLockHeld)
					{
						this.readerWriterLock.ExitReadLock();
					}
				}
				return result;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000AFA4 File Offset: 0x000091A4
		internal List<T> Values
		{
			get
			{
				bool flag = false;
				List<T> result;
				try
				{
					flag = this.readerWriterLock.TryEnterReadLock(-1);
					List<T> list = new List<T>(this.items.Count);
					foreach (CacheEntryBase<K, T> cacheEntryBase in this.items.Values)
					{
						list.Add(cacheEntryBase.Value);
					}
					result = list;
				}
				finally
				{
					if (flag || this.readerWriterLock.IsReadLockHeld)
					{
						this.readerWriterLock.ExitReadLock();
					}
				}
				return result;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000B050 File Offset: 0x00009250
		// (set) Token: 0x06000253 RID: 595 RVA: 0x0000B058 File Offset: 0x00009258
		internal Func<bool> WorkerThreadTestHookDelegate { get; set; }

		// Token: 0x06000254 RID: 596 RVA: 0x0000B061 File Offset: 0x00009261
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000B070 File Offset: 0x00009270
		internal T Remove(K key)
		{
			CacheEntryBase<K, T> cacheEntryBase = null;
			bool flag = false;
			try
			{
				flag = this.readerWriterLock.TryEnterWriteLock(-1);
				cacheEntryBase = this.InternalRemoveItem(key);
			}
			finally
			{
				if (flag || this.readerWriterLock.IsWriteLockHeld)
				{
					this.readerWriterLock.ExitWriteLock();
				}
			}
			if (cacheEntryBase != null)
			{
				this.FireRemoveCallbackAsync(cacheEntryBase, RemoveReason.Removed);
				return cacheEntryBase.Value;
			}
			return default(T);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000B0E0 File Offset: 0x000092E0
		internal bool Contains(K key)
		{
			bool flag = false;
			bool result;
			try
			{
				flag = this.readerWriterLock.TryEnterReadLock(-1);
				result = this.items.ContainsKey(key);
			}
			finally
			{
				if (flag || this.readerWriterLock.IsReadLockHeld)
				{
					this.readerWriterLock.ExitReadLock();
				}
			}
			return result;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000B138 File Offset: 0x00009338
		internal T Get(K key)
		{
			T result;
			if (!this.TryGetValue(key, out result))
			{
				throw new KeyNotFoundException();
			}
			return result;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000B158 File Offset: 0x00009358
		internal bool TryGetValue(K key, out T value)
		{
			this.RecreateThreadIfNecessary();
			bool flag = false;
			try
			{
				flag = this.readerWriterLock.TryEnterWriteLock(-1);
				CacheEntryBase<K, T> cacheEntryBase;
				if (this.items.TryGetValue(key, out cacheEntryBase))
				{
					cacheEntryBase.OnTouch();
					value = cacheEntryBase.Value;
					return true;
				}
			}
			finally
			{
				if (flag || this.readerWriterLock.IsWriteLockHeld)
				{
					this.readerWriterLock.ExitWriteLock();
				}
			}
			value = default(T);
			return false;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000B1D8 File Offset: 0x000093D8
		internal bool TryAddAbsolute(K key, T value, TimeSpan expiration)
		{
			CacheEntryBase<K, T> entry = new CacheEntryFixed<K, T>(key, value, expiration);
			return this.TryInternalAddOuter(entry);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000B1F8 File Offset: 0x000093F8
		internal bool TryAddAbsolute(K key, T value, DateTime absoluteExpiration)
		{
			CacheEntryBase<K, T> entry = new CacheEntryFixed<K, T>(key, value, (absoluteExpiration == DateTime.MaxValue) ? TimeSpan.MaxValue : (absoluteExpiration - DateTime.UtcNow));
			return this.TryInternalAddOuter(entry);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000B234 File Offset: 0x00009434
		internal bool TryAddSliding(K key, T value, TimeSpan slidingExpiration)
		{
			CacheEntryBase<K, T> entry = new CacheEntrySliding<K, T>(key, value, slidingExpiration);
			return this.TryInternalAddOuter(entry);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000B254 File Offset: 0x00009454
		internal bool TryAddLimitedSliding(K key, T value, TimeSpan absoluteLiveTime, TimeSpan slidingLiveTime)
		{
			CacheEntryBase<K, T> entry = new CacheEntryLimitedSliding<K, T>(key, value, slidingLiveTime, absoluteLiveTime);
			return this.TryInternalAddOuter(entry);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000B273 File Offset: 0x00009473
		internal bool TryInsertAbsolute(K key, T value, TimeSpan absoluteLiveTime)
		{
			return this.TryInternalInsertOuter(new CacheEntryFixed<K, T>(key, value, absoluteLiveTime));
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000B283 File Offset: 0x00009483
		internal bool TryInsertSliding(K key, T value, TimeSpan slidingLiveTime)
		{
			return this.TryInternalInsertOuter(new CacheEntrySliding<K, T>(key, value, slidingLiveTime));
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000B293 File Offset: 0x00009493
		internal bool TryInsertLimitedSliding(K key, T value, TimeSpan absoluteLiveTime, TimeSpan slidingLiveTime)
		{
			return this.TryInternalInsertOuter(new CacheEntryLimitedSliding<K, T>(key, value, slidingLiveTime, absoluteLiveTime));
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000B2A8 File Offset: 0x000094A8
		internal void Clear()
		{
			List<CacheEntryBase<K, T>> entries = null;
			bool flag = false;
			try
			{
				flag = this.readerWriterLock.TryEnterWriteLock(-1);
				this.InternalClear(false, out entries);
			}
			finally
			{
				if (flag || this.readerWriterLock.IsWriteLockHeld)
				{
					this.readerWriterLock.ExitWriteLock();
				}
			}
			this.FireRemoveCallbackAsync(entries, RemoveReason.Cleanup);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000B308 File Offset: 0x00009508
		private void InternalClear(bool fromDispose, out List<CacheEntryBase<K, T>> callbackList)
		{
			callbackList = new List<CacheEntryBase<K, T>>(this.items.Count);
			if (!fromDispose || this.callbackOnDispose)
			{
				foreach (KeyValuePair<K, CacheEntryBase<K, T>> keyValuePair in this.items)
				{
					callbackList.Add(keyValuePair.Value);
				}
			}
			this.itemsByExpiration.Clear();
			this.items.Clear();
			this.nextExpirationDate = DateTime.MaxValue;
			if (!fromDispose)
			{
				this.TriggerModifyEvent();
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000B3AC File Offset: 0x000095AC
		private void TriggerModifyEvent()
		{
			if (!this.disposed)
			{
				this.RecreateThreadIfNecessary();
				this.collectionModifyEvent.Set();
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000B3C8 File Offset: 0x000095C8
		private void WorkerThreadProc()
		{
			WaitHandle[] waitHandles = new WaitHandle[]
			{
				this.collectionModifyEvent,
				this.abortEvent
			};
			bool flag = true;
			while (flag)
			{
				try
				{
					bool flag2 = false;
					TimeSpan timeSpan = TimeSpan.Zero;
					bool flag3 = false;
					try
					{
						flag3 = this.readerWriterLock.TryEnterReadLock(-1);
						if (this.itemsByExpiration.Count > 0)
						{
							timeSpan = this.nextExpirationDate - DateTime.UtcNow;
							if (timeSpan.TotalMilliseconds > 2147483647.0)
							{
								timeSpan = TimeSpan.FromMilliseconds(2147483647.0);
							}
							flag2 = (timeSpan <= TimeSpan.Zero);
						}
					}
					finally
					{
						if (flag3 || this.readerWriterLock.IsReadLockHeld)
						{
							this.readerWriterLock.ExitReadLock();
						}
					}
					if (flag2)
					{
						this.InternalRemoveItemsByExpiration(DateTime.UtcNow, RemoveReason.Expired);
						if (this.abortEvent.WaitOne(TimeSpan.Zero))
						{
							flag = false;
						}
					}
					else
					{
						if (timeSpan != TimeSpan.Zero && timeSpan < ExactTimeoutCache<K, T>.MinimumWaitInterval)
						{
							timeSpan = ExactTimeoutCache<K, T>.MinimumWaitInterval;
						}
						int num = (timeSpan > TimeSpan.Zero) ? WaitHandle.WaitAny(waitHandles, timeSpan) : WaitHandle.WaitAny(waitHandles);
						int num2 = num;
						switch (num2)
						{
						case 0:
							break;
						case 1:
							flag = false;
							break;
						default:
							if (num2 == 258)
							{
								this.InternalRemoveItemsByExpiration(DateTime.UtcNow, RemoveReason.Expired);
							}
							break;
						}
					}
					if (flag && this.WorkerThreadTestHookDelegate != null)
					{
						flag = this.WorkerThreadTestHookDelegate();
					}
				}
				catch (Exception e)
				{
					if (this.unhandledExceptionDelegate == null)
					{
						throw;
					}
					this.unhandledExceptionDelegate(e);
				}
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000B580 File Offset: 0x00009780
		private CacheEntryBase<K, T> AddEntryToChain(CacheEntryBase<K, T> chain, CacheEntryBase<K, T> entryToAdd)
		{
			if (chain == null)
			{
				return entryToAdd;
			}
			CacheEntryBase<K, T> next = chain.Next;
			if (next != null)
			{
				next.Previous = entryToAdd;
				entryToAdd.Next = next;
			}
			chain.Next = entryToAdd;
			entryToAdd.Previous = chain;
			return chain;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000B5BC File Offset: 0x000097BC
		private void InternalRemoveItemsByExpiration(DateTime expiration, RemoveReason reason)
		{
			List<CacheEntryBase<K, T>> entries = null;
			List<CacheEntryBase<K, T>> entries2 = null;
			bool flag = false;
			try
			{
				flag = this.readerWriterLock.TryEnterWriteLock(-1);
				this.InternalRemoveItemsByExpiration(expiration, reason, out entries, out entries2);
			}
			finally
			{
				if (flag || this.readerWriterLock.IsWriteLockHeld)
				{
					this.readerWriterLock.ExitWriteLock();
				}
			}
			this.FireRemoveCallbackAsync(entries, reason);
			this.FireShouldRemoveItemsAsync(entries2, reason);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000B628 File Offset: 0x00009828
		private void InternalRemoveItemsByExpiration(DateTime expiration, RemoveReason reason, out List<CacheEntryBase<K, T>> removeCallbacks, out List<CacheEntryBase<K, T>> shouldRemoveCallbacks)
		{
			CacheEntryBase<K, T> cacheEntryBase = null;
			removeCallbacks = null;
			shouldRemoveCallbacks = null;
			while (this.itemsByExpiration.Count > 0 && this.nextExpirationDate <= expiration)
			{
				try
				{
					cacheEntryBase = this.itemsByExpiration[this.nextExpirationDate];
				}
				catch (KeyNotFoundException)
				{
					this.UpdateNextExpirationTime();
					continue;
				}
				this.itemsByExpiration.Remove(this.nextExpirationDate);
				this.UpdateNextExpirationTime();
				while (cacheEntryBase != null)
				{
					CacheEntryBase<K, T> next = cacheEntryBase.Next;
					cacheEntryBase.Previous = null;
					cacheEntryBase.Next = null;
					bool flag = false;
					if (reason == RemoveReason.Expired && !cacheEntryBase.OnBeforeExpire())
					{
						this.InternalAddToSortedList(cacheEntryBase);
						flag = true;
					}
					if (!flag)
					{
						if (reason != RemoveReason.Expired || this.shouldRemoveDelegate == null)
						{
							this.items.Remove(cacheEntryBase.Key);
							if (this.removeItemDelegate != null)
							{
								if (removeCallbacks == null)
								{
									removeCallbacks = new List<CacheEntryBase<K, T>>();
								}
								removeCallbacks.Add(cacheEntryBase);
							}
						}
						else
						{
							if (shouldRemoveCallbacks == null)
							{
								shouldRemoveCallbacks = new List<CacheEntryBase<K, T>>();
							}
							cacheEntryBase.InShouldRemoveCycle = true;
							shouldRemoveCallbacks.Add(cacheEntryBase);
						}
					}
					cacheEntryBase = next;
				}
			}
			this.TriggerModifyEvent();
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000B744 File Offset: 0x00009944
		private CacheEntryBase<K, T> InternalRemoveItem(K key)
		{
			CacheEntryBase<K, T> cacheEntryBase;
			if (!this.items.TryGetValue(key, out cacheEntryBase))
			{
				return null;
			}
			bool flag = this.InternalRemoveFromSortedList(cacheEntryBase);
			this.items.Remove(cacheEntryBase.Key);
			if (flag)
			{
				this.TriggerModifyEvent();
			}
			return cacheEntryBase;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000B788 File Offset: 0x00009988
		private void RemoveCallbackWorker(object state)
		{
			ExactTimeoutCache<K, T>.EntryAndReason entryAndReason = state as ExactTimeoutCache<K, T>.EntryAndReason;
			this.RemoveCallbackSingleEntry(entryAndReason.Entry, entryAndReason.Reason);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000B7B0 File Offset: 0x000099B0
		private void RemoveCallbackWorkerArray(object state)
		{
			ExactTimeoutCache<K, T>.EntryListAndReason entryListAndReason = state as ExactTimeoutCache<K, T>.EntryListAndReason;
			List<CacheEntryBase<K, T>> entryList = entryListAndReason.EntryList;
			foreach (CacheEntryBase<K, T> cacheEntry in entryList)
			{
				this.RemoveCallbackSingleEntry(cacheEntry, entryListAndReason.Reason);
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000B814 File Offset: 0x00009A14
		private void RemoveCallbackSingleEntry(CacheEntryBase<K, T> cacheEntry, RemoveReason reason)
		{
			if (this.removeItemDelegate != null)
			{
				this.removeItemDelegate(cacheEntry.Key, cacheEntry.Value, reason);
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000B838 File Offset: 0x00009A38
		private bool TryInternalAdd(CacheEntryBase<K, T> entry, out CacheEntryBase<K, T> singleItemRemoval, out List<CacheEntryBase<K, T>> removeCallbacks, out List<CacheEntryBase<K, T>> shouldRemoveCallbacks)
		{
			removeCallbacks = null;
			shouldRemoveCallbacks = null;
			singleItemRemoval = null;
			bool flag = false;
			if (this.items.Count >= this.cacheSizeLimit)
			{
				if (this.cacheFullBehavior != CacheFullBehavior.ExpireExisting)
				{
					return false;
				}
				this.PreemptiveExpire(out singleItemRemoval, out removeCallbacks, out shouldRemoveCallbacks);
			}
			if (this.items.ContainsKey(entry.Key))
			{
				throw new DuplicateKeyException();
			}
			this.items.Add(entry.Key, entry);
			if (entry.ExpirationUtc < DateTime.MaxValue)
			{
				flag = this.InternalAddToSortedList(entry);
			}
			if (flag)
			{
				this.TriggerModifyEvent();
			}
			return true;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000B8CC File Offset: 0x00009ACC
		private bool TryInternalInsertOuter(CacheEntryBase<K, T> entry)
		{
			bool result = false;
			CacheEntryBase<K, T> entry2 = null;
			CacheEntryBase<K, T> entry3 = null;
			List<CacheEntryBase<K, T>> entries = null;
			List<CacheEntryBase<K, T>> entries2 = null;
			bool flag = false;
			try
			{
				flag = this.readerWriterLock.TryEnterWriteLock(-1);
				entry2 = this.InternalRemoveItem(entry.Key);
				result = this.TryInternalAdd(entry, out entry3, out entries, out entries2);
			}
			finally
			{
				if (flag || this.readerWriterLock.IsWriteLockHeld)
				{
					this.readerWriterLock.ExitWriteLock();
				}
			}
			this.FireRemoveCallbackAsync(entry2, RemoveReason.Removed);
			this.FireRemoveCallbackAsync(entry3, RemoveReason.Removed);
			this.FireRemoveCallbackAsync(entries, RemoveReason.PreemptivelyExpired);
			this.FireShouldRemoveItemsAsync(entries2, RemoveReason.PreemptivelyExpired);
			return result;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000B964 File Offset: 0x00009B64
		private bool TryInternalAddOuter(CacheEntryBase<K, T> entry)
		{
			bool result = false;
			CacheEntryBase<K, T> entry2 = null;
			List<CacheEntryBase<K, T>> entries = null;
			List<CacheEntryBase<K, T>> entries2 = null;
			bool flag = false;
			try
			{
				flag = this.readerWriterLock.TryEnterWriteLock(-1);
				result = this.TryInternalAdd(entry, out entry2, out entries, out entries2);
			}
			finally
			{
				if (flag || this.readerWriterLock.IsWriteLockHeld)
				{
					this.readerWriterLock.ExitWriteLock();
				}
			}
			this.FireRemoveCallbackAsync(entry2, RemoveReason.PreemptivelyExpired);
			this.FireRemoveCallbackAsync(entries, RemoveReason.PreemptivelyExpired);
			this.FireShouldRemoveItemsAsync(entries2, RemoveReason.PreemptivelyExpired);
			return result;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000B9E0 File Offset: 0x00009BE0
		private void PreemptiveExpire(out CacheEntryBase<K, T> singleRemovedItem, out List<CacheEntryBase<K, T>> removeCallbacks, out List<CacheEntryBase<K, T>> shouldRemoveCallbacks)
		{
			removeCallbacks = null;
			shouldRemoveCallbacks = null;
			singleRemovedItem = null;
			if (this.itemsByExpiration.Count == 0)
			{
				using (Dictionary<K, CacheEntryBase<K, T>>.Enumerator enumerator = this.items.GetEnumerator())
				{
					enumerator.MoveNext();
					KeyValuePair<K, CacheEntryBase<K, T>> keyValuePair = enumerator.Current;
					singleRemovedItem = this.InternalRemoveItem(keyValuePair.Key);
					return;
				}
			}
			DateTime expiration = (DateTime.UtcNow > this.nextExpirationDate) ? DateTime.UtcNow : this.nextExpirationDate;
			this.InternalRemoveItemsByExpiration(expiration, RemoveReason.PreemptivelyExpired, out removeCallbacks, out shouldRemoveCallbacks);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000BA78 File Offset: 0x00009C78
		private void UpdateNextExpirationTime()
		{
			if (this.itemsByExpiration.Count == 0)
			{
				this.nextExpirationDate = DateTime.MaxValue;
				return;
			}
			using (SortedDictionary<DateTime, CacheEntryBase<K, T>>.Enumerator enumerator = this.itemsByExpiration.GetEnumerator())
			{
				enumerator.MoveNext();
				KeyValuePair<DateTime, CacheEntryBase<K, T>> keyValuePair = enumerator.Current;
				this.nextExpirationDate = keyValuePair.Key;
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000BAE8 File Offset: 0x00009CE8
		private bool InternalAddToSortedList(CacheEntryBase<K, T> entry)
		{
			CacheEntryBase<K, T> chain;
			if (!this.itemsByExpiration.TryGetValue(entry.ExpirationUtc, out chain))
			{
				bool flag = this.itemsByExpiration.Count == 0 || this.nextExpirationDate > entry.ExpirationUtc;
				this.itemsByExpiration.Add(entry.ExpirationUtc, entry);
				if (flag)
				{
					this.nextExpirationDate = entry.ExpirationUtc;
				}
				return flag;
			}
			this.AddEntryToChain(chain, entry);
			return false;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000BB5C File Offset: 0x00009D5C
		private bool InternalRemoveFromSortedList(CacheEntryBase<K, T> entryToRemove)
		{
			if (this.itemsByExpiration.Count == 0)
			{
				return false;
			}
			CacheEntryBase<K, T> previous = entryToRemove.Previous;
			CacheEntryBase<K, T> next = entryToRemove.Next;
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
			CacheEntryBase<K, T> cacheEntryBase;
			if (this.itemsByExpiration.TryGetValue(entryToRemove.ExpirationUtc, out cacheEntryBase) && cacheEntryBase == entryToRemove)
			{
				if (next == null)
				{
					bool result = this.nextExpirationDate == entryToRemove.ExpirationUtc;
					this.itemsByExpiration.Remove(entryToRemove.ExpirationUtc);
					this.UpdateNextExpirationTime();
					return result;
				}
				this.itemsByExpiration[cacheEntryBase.ExpirationUtc] = next;
			}
			return false;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000BC04 File Offset: 0x00009E04
		private void CreateWorkerThread()
		{
			using (ActivityContext.SuppressThreadScope())
			{
				this.workerThread = new Thread(new ThreadStart(this.WorkerThreadProc));
				this.workerThread.IsBackground = true;
				this.workerThread.Start();
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000BC64 File Offset: 0x00009E64
		protected virtual void Dispose(bool isDisposing)
		{
			if (isDisposing && !this.disposed)
			{
				this.abortEvent.Set();
				if (!this.workerThread.Join(TimeSpan.FromMinutes(1.0)))
				{
					this.workerThread.Abort();
				}
				List<CacheEntryBase<K, T>> entries = null;
				bool flag = false;
				try
				{
					flag = this.readerWriterLock.TryEnterWriteLock(-1);
					if (!this.disposed)
					{
						this.InternalClear(true, out entries);
						this.disposed = true;
						this.abortEvent.Close();
						this.abortEvent = null;
						this.collectionModifyEvent.Close();
						this.collectionModifyEvent = null;
					}
				}
				finally
				{
					if (flag || this.readerWriterLock.IsWriteLockHeld)
					{
						this.readerWriterLock.ExitWriteLock();
						if (this.readerWriterLock != null)
						{
							this.readerWriterLock.Dispose();
							this.readerWriterLock = null;
						}
					}
				}
				this.FireRemoveCallbackAsync(entries, RemoveReason.Cleanup);
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000BD54 File Offset: 0x00009F54
		private void RecreateThreadIfNecessary()
		{
			if (!this.disposed && ((this.workerThread.ThreadState & ThreadState.Aborted) == ThreadState.Aborted || (this.workerThread.ThreadState & ThreadState.Stopped) == ThreadState.Stopped))
			{
				bool flag = false;
				try
				{
					Monitor.TryEnter(this.threadCreateCritSect, 0, ref flag);
					if (flag && !this.disposed && ((this.workerThread.ThreadState & ThreadState.Aborted) == ThreadState.Aborted || (this.workerThread.ThreadState & ThreadState.Stopped) == ThreadState.Stopped))
					{
						this.CreateWorkerThread();
					}
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(this.threadCreateCritSect);
					}
				}
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000C00C File Offset: 0x0000A20C
		private void FireShouldRemoveItemsAsync(List<CacheEntryBase<K, T>> entries, RemoveReason reason)
		{
			if (entries == null || entries.Count == 0)
			{
				return;
			}
			using (ActivityContext.SuppressThreadScope())
			{
				ThreadPool.QueueUserWorkItem(delegate(object state)
				{
					List<CacheEntryBase<K, T>> list = null;
					bool flag = false;
					foreach (CacheEntryBase<K, T> cacheEntryBase in entries)
					{
						if (this.shouldRemoveDelegate(cacheEntryBase.Key, cacheEntryBase.Value))
						{
							if (list == null)
							{
								list = new List<CacheEntryBase<K, T>>();
							}
							list.Add(cacheEntryBase);
						}
						else
						{
							cacheEntryBase.OnForceExtend();
							cacheEntryBase.InShouldRemoveCycle = false;
							bool flag2 = false;
							bool flag3 = false;
							try
							{
								flag3 = this.readerWriterLock.TryEnterWriteLock(-1);
								flag2 = this.InternalAddToSortedList(cacheEntryBase);
							}
							finally
							{
								if (flag3 || this.readerWriterLock.IsWriteLockHeld)
								{
									this.readerWriterLock.ExitWriteLock();
								}
							}
							flag = (flag2 || flag);
						}
					}
					if (list != null)
					{
						bool flag4 = false;
						try
						{
							flag4 = this.readerWriterLock.TryEnterWriteLock(-1);
							foreach (CacheEntryBase<K, T> cacheEntryBase2 in list)
							{
								this.items.Remove(cacheEntryBase2.Key);
							}
						}
						finally
						{
							if (flag4 || this.readerWriterLock.IsWriteLockHeld)
							{
								this.readerWriterLock.ExitWriteLock();
							}
						}
					}
					if (flag)
					{
						this.TriggerModifyEvent();
					}
					if (list != null && this.removeItemDelegate != null)
					{
						foreach (CacheEntryBase<K, T> cacheEntry in list)
						{
							this.RemoveCallbackSingleEntry(cacheEntry, reason);
						}
					}
				});
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000C088 File Offset: 0x0000A288
		private void FireRemoveCallbackAsync(CacheEntryBase<K, T> entry, RemoveReason reason)
		{
			if (entry == null)
			{
				return;
			}
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.RemoveCallbackWorker), new ExactTimeoutCache<K, T>.EntryAndReason(entry, reason));
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000C0A7 File Offset: 0x0000A2A7
		private void FireRemoveCallbackAsync(List<CacheEntryBase<K, T>> entries, RemoveReason reason)
		{
			if (entries == null || entries.Count == 0)
			{
				return;
			}
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.RemoveCallbackWorkerArray), new ExactTimeoutCache<K, T>.EntryListAndReason(entries, reason));
		}

		// Token: 0x04000174 RID: 372
		private const int CollectionModifiedSignaled = 0;

		// Token: 0x04000175 RID: 373
		private const int AbortSignaled = 1;

		// Token: 0x04000176 RID: 374
		private static readonly TimeSpan MinimumWaitInterval = TimeSpan.FromMilliseconds(100.0);

		// Token: 0x04000177 RID: 375
		private Dictionary<K, CacheEntryBase<K, T>> items = new Dictionary<K, CacheEntryBase<K, T>>();

		// Token: 0x04000178 RID: 376
		private SortedDictionary<DateTime, CacheEntryBase<K, T>> itemsByExpiration = new SortedDictionary<DateTime, CacheEntryBase<K, T>>();

		// Token: 0x04000179 RID: 377
		private ReaderWriterLockSlim readerWriterLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

		// Token: 0x0400017A RID: 378
		private readonly ShouldRemoveDelegate<K, T> shouldRemoveDelegate;

		// Token: 0x0400017B RID: 379
		private readonly RemoveItemDelegate<K, T> removeItemDelegate;

		// Token: 0x0400017C RID: 380
		private bool disposed;

		// Token: 0x0400017D RID: 381
		private int cacheSizeLimit;

		// Token: 0x0400017E RID: 382
		private bool callbackOnDispose;

		// Token: 0x0400017F RID: 383
		private AutoResetEvent collectionModifyEvent = new AutoResetEvent(false);

		// Token: 0x04000180 RID: 384
		private AutoResetEvent abortEvent = new AutoResetEvent(false);

		// Token: 0x04000181 RID: 385
		private UnhandledExceptionDelegate unhandledExceptionDelegate;

		// Token: 0x04000182 RID: 386
		private CacheFullBehavior cacheFullBehavior;

		// Token: 0x04000183 RID: 387
		private DateTime nextExpirationDate = DateTime.MaxValue;

		// Token: 0x04000184 RID: 388
		private Thread workerThread;

		// Token: 0x04000185 RID: 389
		private object threadCreateCritSect = new object();

		// Token: 0x02000058 RID: 88
		private class EntryAndReason
		{
			// Token: 0x06000279 RID: 633 RVA: 0x0000C0E3 File Offset: 0x0000A2E3
			public EntryAndReason(CacheEntryBase<K, T> entry, RemoveReason reason)
			{
				this.Entry = entry;
				this.Reason = reason;
			}

			// Token: 0x17000074 RID: 116
			// (get) Token: 0x0600027A RID: 634 RVA: 0x0000C0F9 File Offset: 0x0000A2F9
			// (set) Token: 0x0600027B RID: 635 RVA: 0x0000C101 File Offset: 0x0000A301
			public CacheEntryBase<K, T> Entry { get; private set; }

			// Token: 0x17000075 RID: 117
			// (get) Token: 0x0600027C RID: 636 RVA: 0x0000C10A File Offset: 0x0000A30A
			// (set) Token: 0x0600027D RID: 637 RVA: 0x0000C112 File Offset: 0x0000A312
			public RemoveReason Reason { get; private set; }
		}

		// Token: 0x02000059 RID: 89
		private class EntryListAndReason
		{
			// Token: 0x0600027E RID: 638 RVA: 0x0000C11B File Offset: 0x0000A31B
			public EntryListAndReason(List<CacheEntryBase<K, T>> entryList, RemoveReason reason)
			{
				this.EntryList = entryList;
				this.Reason = reason;
			}

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x0600027F RID: 639 RVA: 0x0000C131 File Offset: 0x0000A331
			// (set) Token: 0x06000280 RID: 640 RVA: 0x0000C139 File Offset: 0x0000A339
			public List<CacheEntryBase<K, T>> EntryList { get; private set; }

			// Token: 0x17000077 RID: 119
			// (get) Token: 0x06000281 RID: 641 RVA: 0x0000C142 File Offset: 0x0000A342
			// (set) Token: 0x06000282 RID: 642 RVA: 0x0000C14A File Offset: 0x0000A34A
			public RemoveReason Reason { get; private set; }
		}
	}
}
