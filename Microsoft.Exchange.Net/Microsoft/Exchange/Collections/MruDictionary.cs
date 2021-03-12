using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x02000045 RID: 69
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MruDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060001D3 RID: 467 RVA: 0x00009260 File Offset: 0x00007460
		// (remove) Token: 0x060001D4 RID: 468 RVA: 0x00009298 File Offset: 0x00007498
		public event EventHandler<MruDictionaryElementRemovedEventArgs<TKey, TValue>> OnRemoved;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060001D5 RID: 469 RVA: 0x000092D0 File Offset: 0x000074D0
		// (remove) Token: 0x060001D6 RID: 470 RVA: 0x00009308 File Offset: 0x00007508
		public event EventHandler<MruDictionaryElementReplacedEventArgs<TKey, TValue>> OnReplaced;

		// Token: 0x060001D7 RID: 471 RVA: 0x00009340 File Offset: 0x00007540
		public MruDictionary(int maxCapacity, IComparer<TKey> keyComparer, IMruDictionaryPerfCounters perfCounters)
		{
			if (maxCapacity < 1)
			{
				throw new ArgumentOutOfRangeException("maxCapacity", maxCapacity, "maxCapacity must be greater than 0!");
			}
			if (keyComparer == null)
			{
				throw new ArgumentNullException("keyComparer");
			}
			this.maxCapacity = maxCapacity;
			this.actualKeyComparer = keyComparer;
			this.dictionaryTime = new Dictionary<TKey, DateTime>(new MruEqualityComparer<TKey>(this.actualKeyComparer));
			this.perfCounters = (perfCounters ?? NoopMruDictionaryPerfCounters.Instance);
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x000093C8 File Offset: 0x000075C8
		public int Count
		{
			get
			{
				int count;
				lock (this.SyncRoot)
				{
					count = this.dictionaryTime.Count;
				}
				return count;
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00009410 File Offset: 0x00007610
		public bool TryGetValue(TKey key, out TValue value)
		{
			value = default(TValue);
			if (key == null)
			{
				return false;
			}
			bool flag = false;
			lock (this.SyncRoot)
			{
				DateTime lastAccessedTime;
				if (this.dictionaryTime.TryGetValue(key, out lastAccessedTime))
				{
					MruDictionaryInternalKey<TKey> key2 = new MruDictionaryInternalKey<TKey>(key, this.actualKeyComparer, lastAccessedTime);
					value = this.dictionaryData[key2];
					MruDictionaryInternalKey<TKey> mruDictionaryInternalKey = new MruDictionaryInternalKey<TKey>(key, this.actualKeyComparer, DateTime.UtcNow);
					this.dictionaryData.Remove(key2);
					this.dictionaryData.Add(mruDictionaryInternalKey, value);
					this.dictionaryTime[key] = mruDictionaryInternalKey.LastAccessedTime;
					flag = true;
				}
			}
			if (flag)
			{
				this.perfCounters.CacheHit();
			}
			else
			{
				this.perfCounters.CacheMiss();
			}
			return flag;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000094F4 File Offset: 0x000076F4
		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			TKey tkey = default(TKey);
			TValue tvalue = default(TValue);
			bool flag = false;
			bool flag2 = false;
			lock (this.SyncRoot)
			{
				DateTime lastAccessedTime;
				if (this.dictionaryTime.TryGetValue(key, out lastAccessedTime))
				{
					MruDictionaryInternalKey<TKey> key2 = new MruDictionaryInternalKey<TKey>(key, this.actualKeyComparer, lastAccessedTime);
					tvalue = this.dictionaryData[key2];
					tkey = key;
					this.dictionaryData.Remove(key2);
					flag2 = true;
				}
				else if (this.dictionaryTime.Count >= this.maxCapacity)
				{
					KeyValuePair<MruDictionaryInternalKey<TKey>, TValue> keyValuePair = this.dictionaryData.First<KeyValuePair<MruDictionaryInternalKey<TKey>, TValue>>();
					tkey = keyValuePair.Key.OriginalKey;
					tvalue = keyValuePair.Value;
					this.dictionaryData.Remove(keyValuePair.Key);
					this.dictionaryTime.Remove(keyValuePair.Key.OriginalKey);
					flag = true;
				}
				MruDictionaryInternalKey<TKey> mruDictionaryInternalKey = new MruDictionaryInternalKey<TKey>(key, this.actualKeyComparer, DateTime.UtcNow);
				this.dictionaryData.Add(mruDictionaryInternalKey, value);
				this.dictionaryTime[key] = mruDictionaryInternalKey.LastAccessedTime;
			}
			this.perfCounters.CacheAdd(flag2, flag);
			if (flag)
			{
				this.InvokeOnRemovedEventHandler(tkey, tvalue);
				return;
			}
			if (flag2)
			{
				this.InvokeOnReplacedEventHandler(tkey, tvalue, key, value);
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000966C File Offset: 0x0000786C
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			TValue value = default(TValue);
			bool flag = false;
			lock (this.SyncRoot)
			{
				DateTime lastAccessedTime;
				if (this.dictionaryTime.TryGetValue(key, out lastAccessedTime))
				{
					MruDictionaryInternalKey<TKey> key2 = new MruDictionaryInternalKey<TKey>(key, this.actualKeyComparer, lastAccessedTime);
					value = this.dictionaryData[key2];
					this.dictionaryData.Remove(key2);
					this.dictionaryTime.Remove(key);
					flag = true;
				}
			}
			if (flag)
			{
				this.perfCounters.CacheRemove();
				this.InvokeOnRemovedEventHandler(key, value);
			}
			return flag;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000972C File Offset: 0x0000792C
		private void InvokeOnRemovedEventHandler(TKey key, TValue value)
		{
			EventHandler<MruDictionaryElementRemovedEventArgs<TKey, TValue>> onRemoved = this.OnRemoved;
			if (onRemoved != null)
			{
				KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(key, value);
				MruDictionaryElementRemovedEventArgs<TKey, TValue> e = new MruDictionaryElementRemovedEventArgs<TKey, TValue>(keyValuePair);
				onRemoved(this, e);
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000975C File Offset: 0x0000795C
		private void InvokeOnReplacedEventHandler(TKey oldKey, TValue oldValue, TKey newKey, TValue newValue)
		{
			EventHandler<MruDictionaryElementReplacedEventArgs<TKey, TValue>> onReplaced = this.OnReplaced;
			if (onReplaced != null)
			{
				KeyValuePair<TKey, TValue> oldKeyValuePair = new KeyValuePair<TKey, TValue>(oldKey, oldValue);
				KeyValuePair<TKey, TValue> newKeyValuePair = new KeyValuePair<TKey, TValue>(newKey, newValue);
				MruDictionaryElementReplacedEventArgs<TKey, TValue> e = new MruDictionaryElementReplacedEventArgs<TKey, TValue>(oldKeyValuePair, newKeyValuePair);
				onReplaced(this, e);
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00009796 File Offset: 0x00007996
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return new MruDictionary<TKey, TValue>.Mlu2MruEnumerator(this);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000979E File Offset: 0x0000799E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400013B RID: 315
		public readonly object SyncRoot = new object();

		// Token: 0x0400013C RID: 316
		private readonly int maxCapacity;

		// Token: 0x0400013D RID: 317
		private readonly SortedDictionary<MruDictionaryInternalKey<TKey>, TValue> dictionaryData = new SortedDictionary<MruDictionaryInternalKey<TKey>, TValue>();

		// Token: 0x0400013E RID: 318
		private readonly Dictionary<TKey, DateTime> dictionaryTime;

		// Token: 0x0400013F RID: 319
		private readonly IComparer<TKey> actualKeyComparer;

		// Token: 0x04000142 RID: 322
		private IMruDictionaryPerfCounters perfCounters;

		// Token: 0x02000046 RID: 70
		internal sealed class Mlu2MruEnumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator
		{
			// Token: 0x060001E0 RID: 480 RVA: 0x000097A6 File Offset: 0x000079A6
			internal Mlu2MruEnumerator(MruDictionary<TKey, TValue> dictionary)
			{
				this.internalEnumerator = dictionary.dictionaryData.GetEnumerator();
			}

			// Token: 0x1700005F RID: 95
			// (get) Token: 0x060001E1 RID: 481 RVA: 0x000097C4 File Offset: 0x000079C4
			public KeyValuePair<TKey, TValue> Current
			{
				get
				{
					KeyValuePair<MruDictionaryInternalKey<TKey>, TValue> keyValuePair = this.internalEnumerator.Current;
					return new KeyValuePair<TKey, TValue>(keyValuePair.Key.OriginalKey, keyValuePair.Value);
				}
			}

			// Token: 0x060001E2 RID: 482 RVA: 0x000097F5 File Offset: 0x000079F5
			public void Dispose()
			{
				this.internalEnumerator.Dispose();
			}

			// Token: 0x17000060 RID: 96
			// (get) Token: 0x060001E3 RID: 483 RVA: 0x00009802 File Offset: 0x00007A02
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060001E4 RID: 484 RVA: 0x0000980F File Offset: 0x00007A0F
			public bool MoveNext()
			{
				return this.internalEnumerator.MoveNext();
			}

			// Token: 0x060001E5 RID: 485 RVA: 0x0000981C File Offset: 0x00007A1C
			public void Reset()
			{
				this.internalEnumerator.Reset();
			}

			// Token: 0x04000143 RID: 323
			private readonly IEnumerator<KeyValuePair<MruDictionaryInternalKey<TKey>, TValue>> internalEnumerator;
		}
	}
}
