using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x0200099F RID: 2463
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	internal sealed class ConstantSplittableMap<TKey, TValue> : IMapView<TKey, TValue>, IIterable<IKeyValuePair<TKey, TValue>>, IEnumerable<IKeyValuePair<TKey, TValue>>, IEnumerable
	{
		// Token: 0x060062B0 RID: 25264 RVA: 0x0014FE28 File Offset: 0x0014E028
		internal ConstantSplittableMap(IReadOnlyDictionary<TKey, TValue> data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this.firstItemIndex = 0;
			this.lastItemIndex = data.Count - 1;
			this.items = this.CreateKeyValueArray(data.Count, data.GetEnumerator());
		}

		// Token: 0x060062B1 RID: 25265 RVA: 0x0014FE78 File Offset: 0x0014E078
		internal ConstantSplittableMap(IMapView<TKey, TValue> data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (2147483647U < data.Size)
			{
				Exception ex = new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingDictionaryTooLarge"));
				ex.SetErrorCode(-2147483637);
				throw ex;
			}
			int size = (int)data.Size;
			this.firstItemIndex = 0;
			this.lastItemIndex = size - 1;
			this.items = this.CreateKeyValueArray(size, data.GetEnumerator());
		}

		// Token: 0x060062B2 RID: 25266 RVA: 0x0014FEED File Offset: 0x0014E0ED
		private ConstantSplittableMap(KeyValuePair<TKey, TValue>[] items, int firstItemIndex, int lastItemIndex)
		{
			this.items = items;
			this.firstItemIndex = firstItemIndex;
			this.lastItemIndex = lastItemIndex;
		}

		// Token: 0x060062B3 RID: 25267 RVA: 0x0014FF0C File Offset: 0x0014E10C
		private KeyValuePair<TKey, TValue>[] CreateKeyValueArray(int count, IEnumerator<KeyValuePair<TKey, TValue>> data)
		{
			KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[count];
			int num = 0;
			while (data.MoveNext())
			{
				KeyValuePair<!0, !1> keyValuePair = data.Current;
				array[num++] = keyValuePair;
			}
			Array.Sort<KeyValuePair<TKey, TValue>>(array, ConstantSplittableMap<TKey, TValue>.keyValuePairComparator);
			return array;
		}

		// Token: 0x060062B4 RID: 25268 RVA: 0x0014FF4C File Offset: 0x0014E14C
		private KeyValuePair<TKey, TValue>[] CreateKeyValueArray(int count, IEnumerator<IKeyValuePair<TKey, TValue>> data)
		{
			KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[count];
			int num = 0;
			while (data.MoveNext())
			{
				IKeyValuePair<TKey, TValue> keyValuePair = data.Current;
				array[num++] = new KeyValuePair<TKey, TValue>(keyValuePair.Key, keyValuePair.Value);
			}
			Array.Sort<KeyValuePair<TKey, TValue>>(array, ConstantSplittableMap<TKey, TValue>.keyValuePairComparator);
			return array;
		}

		// Token: 0x17001129 RID: 4393
		// (get) Token: 0x060062B5 RID: 25269 RVA: 0x0014FF9B File Offset: 0x0014E19B
		public int Count
		{
			get
			{
				return this.lastItemIndex - this.firstItemIndex + 1;
			}
		}

		// Token: 0x1700112A RID: 4394
		// (get) Token: 0x060062B6 RID: 25270 RVA: 0x0014FFAC File Offset: 0x0014E1AC
		public uint Size
		{
			get
			{
				return (uint)(this.lastItemIndex - this.firstItemIndex + 1);
			}
		}

		// Token: 0x060062B7 RID: 25271 RVA: 0x0014FFC0 File Offset: 0x0014E1C0
		public TValue Lookup(TKey key)
		{
			TValue result;
			if (!this.TryGetValue(key, out result))
			{
				Exception ex = new KeyNotFoundException(Environment.GetResourceString("Arg_KeyNotFound"));
				ex.SetErrorCode(-2147483637);
				throw ex;
			}
			return result;
		}

		// Token: 0x060062B8 RID: 25272 RVA: 0x0014FFF8 File Offset: 0x0014E1F8
		public bool HasKey(TKey key)
		{
			TValue tvalue;
			return this.TryGetValue(key, out tvalue);
		}

		// Token: 0x060062B9 RID: 25273 RVA: 0x00150010 File Offset: 0x0014E210
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<IKeyValuePair<TKey, TValue>>)this).GetEnumerator();
		}

		// Token: 0x060062BA RID: 25274 RVA: 0x00150018 File Offset: 0x0014E218
		public IIterator<IKeyValuePair<TKey, TValue>> First()
		{
			return new EnumeratorToIteratorAdapter<IKeyValuePair<TKey, TValue>>(this.GetEnumerator());
		}

		// Token: 0x060062BB RID: 25275 RVA: 0x00150025 File Offset: 0x0014E225
		public IEnumerator<IKeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return new ConstantSplittableMap<TKey, TValue>.IKeyValuePairEnumerator(this.items, this.firstItemIndex, this.lastItemIndex);
		}

		// Token: 0x060062BC RID: 25276 RVA: 0x00150044 File Offset: 0x0014E244
		public void Split(out IMapView<TKey, TValue> firstPartition, out IMapView<TKey, TValue> secondPartition)
		{
			if (this.Count < 2)
			{
				firstPartition = null;
				secondPartition = null;
				return;
			}
			int num = (int)(((long)this.firstItemIndex + (long)this.lastItemIndex) / 2L);
			firstPartition = new ConstantSplittableMap<TKey, TValue>(this.items, this.firstItemIndex, num);
			secondPartition = new ConstantSplittableMap<TKey, TValue>(this.items, num + 1, this.lastItemIndex);
		}

		// Token: 0x060062BD RID: 25277 RVA: 0x001500A0 File Offset: 0x0014E2A0
		public bool ContainsKey(TKey key)
		{
			KeyValuePair<TKey, TValue> value = new KeyValuePair<TKey, TValue>(key, default(TValue));
			int num = Array.BinarySearch<KeyValuePair<TKey, TValue>>(this.items, this.firstItemIndex, this.Count, value, ConstantSplittableMap<TKey, TValue>.keyValuePairComparator);
			return num >= 0;
		}

		// Token: 0x060062BE RID: 25278 RVA: 0x001500E4 File Offset: 0x0014E2E4
		public bool TryGetValue(TKey key, out TValue value)
		{
			KeyValuePair<TKey, TValue> value2 = new KeyValuePair<TKey, TValue>(key, default(TValue));
			int num = Array.BinarySearch<KeyValuePair<TKey, TValue>>(this.items, this.firstItemIndex, this.Count, value2, ConstantSplittableMap<TKey, TValue>.keyValuePairComparator);
			if (num < 0)
			{
				value = default(TValue);
				return false;
			}
			value = this.items[num].Value;
			return true;
		}

		// Token: 0x1700112B RID: 4395
		public TValue this[TKey key]
		{
			get
			{
				return this.Lookup(key);
			}
		}

		// Token: 0x1700112C RID: 4396
		// (get) Token: 0x060062C0 RID: 25280 RVA: 0x0015014E File Offset: 0x0014E34E
		public IEnumerable<TKey> Keys
		{
			get
			{
				throw new NotImplementedException("NYI");
			}
		}

		// Token: 0x1700112D RID: 4397
		// (get) Token: 0x060062C1 RID: 25281 RVA: 0x0015015A File Offset: 0x0014E35A
		public IEnumerable<TValue> Values
		{
			get
			{
				throw new NotImplementedException("NYI");
			}
		}

		// Token: 0x04002C26 RID: 11302
		private static readonly ConstantSplittableMap<TKey, TValue>.KeyValuePairComparator keyValuePairComparator = new ConstantSplittableMap<TKey, TValue>.KeyValuePairComparator();

		// Token: 0x04002C27 RID: 11303
		private readonly KeyValuePair<TKey, TValue>[] items;

		// Token: 0x04002C28 RID: 11304
		private readonly int firstItemIndex;

		// Token: 0x04002C29 RID: 11305
		private readonly int lastItemIndex;

		// Token: 0x02000C6C RID: 3180
		private class KeyValuePairComparator : IComparer<KeyValuePair<TKey, TValue>>
		{
			// Token: 0x06006FFD RID: 28669 RVA: 0x00180AD5 File Offset: 0x0017ECD5
			public int Compare(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
			{
				return ConstantSplittableMap<TKey, TValue>.KeyValuePairComparator.keyComparator.Compare(x.Key, y.Key);
			}

			// Token: 0x04003798 RID: 14232
			private static readonly IComparer<TKey> keyComparator = Comparer<TKey>.Default;
		}

		// Token: 0x02000C6D RID: 3181
		[Serializable]
		internal struct IKeyValuePairEnumerator : IEnumerator<IKeyValuePair<TKey, TValue>>, IDisposable, IEnumerator
		{
			// Token: 0x06007000 RID: 28672 RVA: 0x00180B03 File Offset: 0x0017ED03
			internal IKeyValuePairEnumerator(KeyValuePair<TKey, TValue>[] items, int first, int end)
			{
				this._array = items;
				this._start = first;
				this._end = end;
				this._current = this._start - 1;
			}

			// Token: 0x06007001 RID: 28673 RVA: 0x00180B28 File Offset: 0x0017ED28
			public bool MoveNext()
			{
				if (this._current < this._end)
				{
					this._current++;
					return true;
				}
				return false;
			}

			// Token: 0x1700134C RID: 4940
			// (get) Token: 0x06007002 RID: 28674 RVA: 0x00180B4C File Offset: 0x0017ED4C
			public IKeyValuePair<TKey, TValue> Current
			{
				get
				{
					if (this._current < this._start)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					if (this._current > this._end)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
					}
					return new CLRIKeyValuePairImpl<TKey, TValue>(ref this._array[this._current]);
				}
			}

			// Token: 0x1700134D RID: 4941
			// (get) Token: 0x06007003 RID: 28675 RVA: 0x00180BAB File Offset: 0x0017EDAB
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06007004 RID: 28676 RVA: 0x00180BB3 File Offset: 0x0017EDB3
			void IEnumerator.Reset()
			{
				this._current = this._start - 1;
			}

			// Token: 0x06007005 RID: 28677 RVA: 0x00180BC3 File Offset: 0x0017EDC3
			public void Dispose()
			{
			}

			// Token: 0x04003799 RID: 14233
			private KeyValuePair<TKey, TValue>[] _array;

			// Token: 0x0400379A RID: 14234
			private int _start;

			// Token: 0x0400379B RID: 14235
			private int _end;

			// Token: 0x0400379C RID: 14236
			private int _current;
		}
	}
}
