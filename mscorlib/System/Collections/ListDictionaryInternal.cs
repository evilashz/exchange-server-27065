using System;
using System.Threading;

namespace System.Collections
{
	// Token: 0x02000468 RID: 1128
	[Serializable]
	internal class ListDictionaryInternal : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x17000838 RID: 2104
		public object this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
				}
				for (ListDictionaryInternal.DictionaryNode next = this.head; next != null; next = next.next)
				{
					if (next.key.Equals(key))
					{
						return next.value;
					}
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
				this.version++;
				ListDictionaryInternal.DictionaryNode dictionaryNode = null;
				ListDictionaryInternal.DictionaryNode next = this.head;
				while (next != null && !next.key.Equals(key))
				{
					dictionaryNode = next;
					next = next.next;
				}
				if (next != null)
				{
					next.value = value;
					return;
				}
				ListDictionaryInternal.DictionaryNode dictionaryNode2 = new ListDictionaryInternal.DictionaryNode();
				dictionaryNode2.key = key;
				dictionaryNode2.value = value;
				if (dictionaryNode != null)
				{
					dictionaryNode.next = dictionaryNode2;
				}
				else
				{
					this.head = dictionaryNode2;
				}
				this.count++;
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06003721 RID: 14113 RVA: 0x000D355F File Offset: 0x000D175F
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06003722 RID: 14114 RVA: 0x000D3567 File Offset: 0x000D1767
		public ICollection Keys
		{
			get
			{
				return new ListDictionaryInternal.NodeKeyValueCollection(this, true);
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06003723 RID: 14115 RVA: 0x000D3570 File Offset: 0x000D1770
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06003724 RID: 14116 RVA: 0x000D3573 File Offset: 0x000D1773
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06003725 RID: 14117 RVA: 0x000D3576 File Offset: 0x000D1776
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06003726 RID: 14118 RVA: 0x000D3579 File Offset: 0x000D1779
		public object SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06003727 RID: 14119 RVA: 0x000D359B File Offset: 0x000D179B
		public ICollection Values
		{
			get
			{
				return new ListDictionaryInternal.NodeKeyValueCollection(this, false);
			}
		}

		// Token: 0x06003728 RID: 14120 RVA: 0x000D35A4 File Offset: 0x000D17A4
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
			this.version++;
			ListDictionaryInternal.DictionaryNode dictionaryNode = null;
			ListDictionaryInternal.DictionaryNode next;
			for (next = this.head; next != null; next = next.next)
			{
				if (next.key.Equals(key))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_AddingDuplicate__", new object[]
					{
						next.key,
						key
					}));
				}
				dictionaryNode = next;
			}
			if (next != null)
			{
				next.value = value;
				return;
			}
			ListDictionaryInternal.DictionaryNode dictionaryNode2 = new ListDictionaryInternal.DictionaryNode();
			dictionaryNode2.key = key;
			dictionaryNode2.value = value;
			if (dictionaryNode != null)
			{
				dictionaryNode.next = dictionaryNode2;
			}
			else
			{
				this.head = dictionaryNode2;
			}
			this.count++;
		}

		// Token: 0x06003729 RID: 14121 RVA: 0x000D36A6 File Offset: 0x000D18A6
		public void Clear()
		{
			this.count = 0;
			this.head = null;
			this.version++;
		}

		// Token: 0x0600372A RID: 14122 RVA: 0x000D36C4 File Offset: 0x000D18C4
		public bool Contains(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
			}
			for (ListDictionaryInternal.DictionaryNode next = this.head; next != null; next = next.next)
			{
				if (next.key.Equals(key))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600372B RID: 14123 RVA: 0x000D3710 File Offset: 0x000D1910
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
			for (ListDictionaryInternal.DictionaryNode next = this.head; next != null; next = next.next)
			{
				array.SetValue(new DictionaryEntry(next.key, next.value), index);
				index++;
			}
		}

		// Token: 0x0600372C RID: 14124 RVA: 0x000D37B7 File Offset: 0x000D19B7
		public IDictionaryEnumerator GetEnumerator()
		{
			return new ListDictionaryInternal.NodeEnumerator(this);
		}

		// Token: 0x0600372D RID: 14125 RVA: 0x000D37BF File Offset: 0x000D19BF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ListDictionaryInternal.NodeEnumerator(this);
		}

		// Token: 0x0600372E RID: 14126 RVA: 0x000D37C8 File Offset: 0x000D19C8
		public void Remove(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
			}
			this.version++;
			ListDictionaryInternal.DictionaryNode dictionaryNode = null;
			ListDictionaryInternal.DictionaryNode next = this.head;
			while (next != null && !next.key.Equals(key))
			{
				dictionaryNode = next;
				next = next.next;
			}
			if (next == null)
			{
				return;
			}
			if (next == this.head)
			{
				this.head = next.next;
			}
			else
			{
				dictionaryNode.next = next.next;
			}
			this.count--;
		}

		// Token: 0x04001822 RID: 6178
		private ListDictionaryInternal.DictionaryNode head;

		// Token: 0x04001823 RID: 6179
		private int version;

		// Token: 0x04001824 RID: 6180
		private int count;

		// Token: 0x04001825 RID: 6181
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x02000B7C RID: 2940
		private class NodeEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06006CB2 RID: 27826 RVA: 0x00176D00 File Offset: 0x00174F00
			public NodeEnumerator(ListDictionaryInternal list)
			{
				this.list = list;
				this.version = list.version;
				this.start = true;
				this.current = null;
			}

			// Token: 0x1700128A RID: 4746
			// (get) Token: 0x06006CB3 RID: 27827 RVA: 0x00176D29 File Offset: 0x00174F29
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x1700128B RID: 4747
			// (get) Token: 0x06006CB4 RID: 27828 RVA: 0x00176D36 File Offset: 0x00174F36
			public DictionaryEntry Entry
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
					}
					return new DictionaryEntry(this.current.key, this.current.value);
				}
			}

			// Token: 0x1700128C RID: 4748
			// (get) Token: 0x06006CB5 RID: 27829 RVA: 0x00176D6B File Offset: 0x00174F6B
			public object Key
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
					}
					return this.current.key;
				}
			}

			// Token: 0x1700128D RID: 4749
			// (get) Token: 0x06006CB6 RID: 27830 RVA: 0x00176D90 File Offset: 0x00174F90
			public object Value
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
					}
					return this.current.value;
				}
			}

			// Token: 0x06006CB7 RID: 27831 RVA: 0x00176DB8 File Offset: 0x00174FB8
			public bool MoveNext()
			{
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				if (this.start)
				{
					this.current = this.list.head;
					this.start = false;
				}
				else if (this.current != null)
				{
					this.current = this.current.next;
				}
				return this.current != null;
			}

			// Token: 0x06006CB8 RID: 27832 RVA: 0x00176E2C File Offset: 0x0017502C
			public void Reset()
			{
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				this.start = true;
				this.current = null;
			}

			// Token: 0x0400347A RID: 13434
			private ListDictionaryInternal list;

			// Token: 0x0400347B RID: 13435
			private ListDictionaryInternal.DictionaryNode current;

			// Token: 0x0400347C RID: 13436
			private int version;

			// Token: 0x0400347D RID: 13437
			private bool start;
		}

		// Token: 0x02000B7D RID: 2941
		private class NodeKeyValueCollection : ICollection, IEnumerable
		{
			// Token: 0x06006CB9 RID: 27833 RVA: 0x00176E5F File Offset: 0x0017505F
			public NodeKeyValueCollection(ListDictionaryInternal list, bool isKeys)
			{
				this.list = list;
				this.isKeys = isKeys;
			}

			// Token: 0x06006CBA RID: 27834 RVA: 0x00176E78 File Offset: 0x00175078
			void ICollection.CopyTo(Array array, int index)
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
				if (array.Length - index < this.list.Count)
				{
					throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_Index"), "index");
				}
				for (ListDictionaryInternal.DictionaryNode dictionaryNode = this.list.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
				{
					array.SetValue(this.isKeys ? dictionaryNode.key : dictionaryNode.value, index);
					index++;
				}
			}

			// Token: 0x1700128E RID: 4750
			// (get) Token: 0x06006CBB RID: 27835 RVA: 0x00176F2C File Offset: 0x0017512C
			int ICollection.Count
			{
				get
				{
					int num = 0;
					for (ListDictionaryInternal.DictionaryNode dictionaryNode = this.list.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
					{
						num++;
					}
					return num;
				}
			}

			// Token: 0x1700128F RID: 4751
			// (get) Token: 0x06006CBC RID: 27836 RVA: 0x00176F58 File Offset: 0x00175158
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001290 RID: 4752
			// (get) Token: 0x06006CBD RID: 27837 RVA: 0x00176F5B File Offset: 0x0017515B
			object ICollection.SyncRoot
			{
				get
				{
					return this.list.SyncRoot;
				}
			}

			// Token: 0x06006CBE RID: 27838 RVA: 0x00176F68 File Offset: 0x00175168
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new ListDictionaryInternal.NodeKeyValueCollection.NodeKeyValueEnumerator(this.list, this.isKeys);
			}

			// Token: 0x0400347E RID: 13438
			private ListDictionaryInternal list;

			// Token: 0x0400347F RID: 13439
			private bool isKeys;

			// Token: 0x02000CCD RID: 3277
			private class NodeKeyValueEnumerator : IEnumerator
			{
				// Token: 0x060070C9 RID: 28873 RVA: 0x00183EAF File Offset: 0x001820AF
				public NodeKeyValueEnumerator(ListDictionaryInternal list, bool isKeys)
				{
					this.list = list;
					this.isKeys = isKeys;
					this.version = list.version;
					this.start = true;
					this.current = null;
				}

				// Token: 0x1700136A RID: 4970
				// (get) Token: 0x060070CA RID: 28874 RVA: 0x00183EDF File Offset: 0x001820DF
				public object Current
				{
					get
					{
						if (this.current == null)
						{
							throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
						}
						if (!this.isKeys)
						{
							return this.current.value;
						}
						return this.current.key;
					}
				}

				// Token: 0x060070CB RID: 28875 RVA: 0x00183F18 File Offset: 0x00182118
				public bool MoveNext()
				{
					if (this.version != this.list.version)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
					}
					if (this.start)
					{
						this.current = this.list.head;
						this.start = false;
					}
					else if (this.current != null)
					{
						this.current = this.current.next;
					}
					return this.current != null;
				}

				// Token: 0x060070CC RID: 28876 RVA: 0x00183F8C File Offset: 0x0018218C
				public void Reset()
				{
					if (this.version != this.list.version)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
					}
					this.start = true;
					this.current = null;
				}

				// Token: 0x04003856 RID: 14422
				private ListDictionaryInternal list;

				// Token: 0x04003857 RID: 14423
				private ListDictionaryInternal.DictionaryNode current;

				// Token: 0x04003858 RID: 14424
				private int version;

				// Token: 0x04003859 RID: 14425
				private bool isKeys;

				// Token: 0x0400385A RID: 14426
				private bool start;
			}
		}

		// Token: 0x02000B7E RID: 2942
		[Serializable]
		private class DictionaryNode
		{
			// Token: 0x04003480 RID: 13440
			public object key;

			// Token: 0x04003481 RID: 13441
			public object value;

			// Token: 0x04003482 RID: 13442
			public ListDictionaryInternal.DictionaryNode next;
		}
	}
}
