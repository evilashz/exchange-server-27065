using System;

namespace System.Collections
{
	// Token: 0x02000469 RID: 1129
	[Serializable]
	internal sealed class EmptyReadOnlyDictionaryInternal : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06003730 RID: 14128 RVA: 0x000D385D File Offset: 0x000D1A5D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new EmptyReadOnlyDictionaryInternal.NodeEnumerator();
		}

		// Token: 0x06003731 RID: 14129 RVA: 0x000D3864 File Offset: 0x000D1A64
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_Index"), "index");
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06003732 RID: 14130 RVA: 0x000D38D6 File Offset: 0x000D1AD6
		public int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06003733 RID: 14131 RVA: 0x000D38D9 File Offset: 0x000D1AD9
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06003734 RID: 14132 RVA: 0x000D38DC File Offset: 0x000D1ADC
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000843 RID: 2115
		public object this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
				}
				if (!key.GetType().IsSerializable)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_NotSerializable"), "key");
				}
				if (value != null && !value.GetType().IsSerializable)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_NotSerializable"), "value");
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
			}
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06003737 RID: 14135 RVA: 0x000D3977 File Offset: 0x000D1B77
		public ICollection Keys
		{
			get
			{
				return EmptyArray<object>.Value;
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06003738 RID: 14136 RVA: 0x000D397E File Offset: 0x000D1B7E
		public ICollection Values
		{
			get
			{
				return EmptyArray<object>.Value;
			}
		}

		// Token: 0x06003739 RID: 14137 RVA: 0x000D3985 File Offset: 0x000D1B85
		public bool Contains(object key)
		{
			return false;
		}

		// Token: 0x0600373A RID: 14138 RVA: 0x000D3988 File Offset: 0x000D1B88
		public void Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
			}
			if (!key.GetType().IsSerializable)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NotSerializable"), "key");
			}
			if (value != null && !value.GetType().IsSerializable)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NotSerializable"), "value");
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
		}

		// Token: 0x0600373B RID: 14139 RVA: 0x000D3A03 File Offset: 0x000D1C03
		public void Clear()
		{
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x0600373C RID: 14140 RVA: 0x000D3A14 File Offset: 0x000D1C14
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x0600373D RID: 14141 RVA: 0x000D3A17 File Offset: 0x000D1C17
		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600373E RID: 14142 RVA: 0x000D3A1A File Offset: 0x000D1C1A
		public IDictionaryEnumerator GetEnumerator()
		{
			return new EmptyReadOnlyDictionaryInternal.NodeEnumerator();
		}

		// Token: 0x0600373F RID: 14143 RVA: 0x000D3A21 File Offset: 0x000D1C21
		public void Remove(object key)
		{
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
		}

		// Token: 0x02000B7F RID: 2943
		private sealed class NodeEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06006CC1 RID: 27841 RVA: 0x00176F8B File Offset: 0x0017518B
			public bool MoveNext()
			{
				return false;
			}

			// Token: 0x17001291 RID: 4753
			// (get) Token: 0x06006CC2 RID: 27842 RVA: 0x00176F8E File Offset: 0x0017518E
			public object Current
			{
				get
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
				}
			}

			// Token: 0x06006CC3 RID: 27843 RVA: 0x00176F9F File Offset: 0x0017519F
			public void Reset()
			{
			}

			// Token: 0x17001292 RID: 4754
			// (get) Token: 0x06006CC4 RID: 27844 RVA: 0x00176FA1 File Offset: 0x001751A1
			public object Key
			{
				get
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
				}
			}

			// Token: 0x17001293 RID: 4755
			// (get) Token: 0x06006CC5 RID: 27845 RVA: 0x00176FB2 File Offset: 0x001751B2
			public object Value
			{
				get
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
				}
			}

			// Token: 0x17001294 RID: 4756
			// (get) Token: 0x06006CC6 RID: 27846 RVA: 0x00176FC3 File Offset: 0x001751C3
			public DictionaryEntry Entry
			{
				get
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
				}
			}
		}
	}
}
