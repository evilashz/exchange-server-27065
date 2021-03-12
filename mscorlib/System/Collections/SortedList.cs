using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections
{
	// Token: 0x02000477 RID: 1143
	[DebuggerTypeProxy(typeof(SortedList.SortedListDebugView))]
	[DebuggerDisplay("Count = {Count}")]
	[ComVisible(true)]
	[Serializable]
	public class SortedList : IDictionary, ICollection, IEnumerable, ICloneable
	{
		// Token: 0x060037B0 RID: 14256 RVA: 0x000D528B File Offset: 0x000D348B
		public SortedList()
		{
			this.Init();
		}

		// Token: 0x060037B1 RID: 14257 RVA: 0x000D5299 File Offset: 0x000D3499
		private void Init()
		{
			this.keys = SortedList.emptyArray;
			this.values = SortedList.emptyArray;
			this._size = 0;
			this.comparer = new Comparer(CultureInfo.CurrentCulture);
		}

		// Token: 0x060037B2 RID: 14258 RVA: 0x000D52C8 File Offset: 0x000D34C8
		public SortedList(int initialCapacity)
		{
			if (initialCapacity < 0)
			{
				throw new ArgumentOutOfRangeException("initialCapacity", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.keys = new object[initialCapacity];
			this.values = new object[initialCapacity];
			this.comparer = new Comparer(CultureInfo.CurrentCulture);
		}

		// Token: 0x060037B3 RID: 14259 RVA: 0x000D531C File Offset: 0x000D351C
		public SortedList(IComparer comparer) : this()
		{
			if (comparer != null)
			{
				this.comparer = comparer;
			}
		}

		// Token: 0x060037B4 RID: 14260 RVA: 0x000D532E File Offset: 0x000D352E
		public SortedList(IComparer comparer, int capacity) : this(comparer)
		{
			this.Capacity = capacity;
		}

		// Token: 0x060037B5 RID: 14261 RVA: 0x000D533E File Offset: 0x000D353E
		public SortedList(IDictionary d) : this(d, null)
		{
		}

		// Token: 0x060037B6 RID: 14262 RVA: 0x000D5348 File Offset: 0x000D3548
		public SortedList(IDictionary d, IComparer comparer) : this(comparer, (d != null) ? d.Count : 0)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d", Environment.GetResourceString("ArgumentNull_Dictionary"));
			}
			d.Keys.CopyTo(this.keys, 0);
			d.Values.CopyTo(this.values, 0);
			Array.Sort(this.keys, this.values, comparer);
			this._size = d.Count;
		}

		// Token: 0x060037B7 RID: 14263 RVA: 0x000D53C4 File Offset: 0x000D35C4
		public virtual void Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
			}
			int num = Array.BinarySearch(this.keys, 0, this._size, key, this.comparer);
			if (num >= 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_AddingDuplicate__", new object[]
				{
					this.GetKey(num),
					key
				}));
			}
			this.Insert(~num, key, value);
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x060037B8 RID: 14264 RVA: 0x000D5435 File Offset: 0x000D3635
		// (set) Token: 0x060037B9 RID: 14265 RVA: 0x000D5440 File Offset: 0x000D3640
		public virtual int Capacity
		{
			get
			{
				return this.keys.Length;
			}
			set
			{
				if (value < this.Count)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
				}
				if (value != this.keys.Length)
				{
					if (value > 0)
					{
						object[] destinationArray = new object[value];
						object[] destinationArray2 = new object[value];
						if (this._size > 0)
						{
							Array.Copy(this.keys, 0, destinationArray, 0, this._size);
							Array.Copy(this.values, 0, destinationArray2, 0, this._size);
						}
						this.keys = destinationArray;
						this.values = destinationArray2;
						return;
					}
					this.keys = SortedList.emptyArray;
					this.values = SortedList.emptyArray;
				}
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x060037BA RID: 14266 RVA: 0x000D54DE File Offset: 0x000D36DE
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x060037BB RID: 14267 RVA: 0x000D54E6 File Offset: 0x000D36E6
		public virtual ICollection Keys
		{
			get
			{
				return this.GetKeyList();
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x060037BC RID: 14268 RVA: 0x000D54EE File Offset: 0x000D36EE
		public virtual ICollection Values
		{
			get
			{
				return this.GetValueList();
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x060037BD RID: 14269 RVA: 0x000D54F6 File Offset: 0x000D36F6
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x060037BE RID: 14270 RVA: 0x000D54F9 File Offset: 0x000D36F9
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x060037BF RID: 14271 RVA: 0x000D54FC File Offset: 0x000D36FC
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x060037C0 RID: 14272 RVA: 0x000D54FF File Offset: 0x000D36FF
		public virtual object SyncRoot
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

		// Token: 0x060037C1 RID: 14273 RVA: 0x000D5521 File Offset: 0x000D3721
		public virtual void Clear()
		{
			this.version++;
			Array.Clear(this.keys, 0, this._size);
			Array.Clear(this.values, 0, this._size);
			this._size = 0;
		}

		// Token: 0x060037C2 RID: 14274 RVA: 0x000D555C File Offset: 0x000D375C
		public virtual object Clone()
		{
			SortedList sortedList = new SortedList(this._size);
			Array.Copy(this.keys, 0, sortedList.keys, 0, this._size);
			Array.Copy(this.values, 0, sortedList.values, 0, this._size);
			sortedList._size = this._size;
			sortedList.version = this.version;
			sortedList.comparer = this.comparer;
			return sortedList;
		}

		// Token: 0x060037C3 RID: 14275 RVA: 0x000D55CC File Offset: 0x000D37CC
		public virtual bool Contains(object key)
		{
			return this.IndexOfKey(key) >= 0;
		}

		// Token: 0x060037C4 RID: 14276 RVA: 0x000D55DB File Offset: 0x000D37DB
		public virtual bool ContainsKey(object key)
		{
			return this.IndexOfKey(key) >= 0;
		}

		// Token: 0x060037C5 RID: 14277 RVA: 0x000D55EA File Offset: 0x000D37EA
		public virtual bool ContainsValue(object value)
		{
			return this.IndexOfValue(value) >= 0;
		}

		// Token: 0x060037C6 RID: 14278 RVA: 0x000D55FC File Offset: 0x000D37FC
		public virtual void CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - arrayIndex < this.Count)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ArrayPlusOffTooSmall"));
			}
			for (int i = 0; i < this.Count; i++)
			{
				DictionaryEntry dictionaryEntry = new DictionaryEntry(this.keys[i], this.values[i]);
				array.SetValue(dictionaryEntry, i + arrayIndex);
			}
		}

		// Token: 0x060037C7 RID: 14279 RVA: 0x000D56AC File Offset: 0x000D38AC
		internal virtual KeyValuePairs[] ToKeyValuePairsArray()
		{
			KeyValuePairs[] array = new KeyValuePairs[this.Count];
			for (int i = 0; i < this.Count; i++)
			{
				array[i] = new KeyValuePairs(this.keys[i], this.values[i]);
			}
			return array;
		}

		// Token: 0x060037C8 RID: 14280 RVA: 0x000D56F0 File Offset: 0x000D38F0
		private void EnsureCapacity(int min)
		{
			int num = (this.keys.Length == 0) ? 16 : (this.keys.Length * 2);
			if (num > 2146435071)
			{
				num = 2146435071;
			}
			if (num < min)
			{
				num = min;
			}
			this.Capacity = num;
		}

		// Token: 0x060037C9 RID: 14281 RVA: 0x000D5730 File Offset: 0x000D3930
		public virtual object GetByIndex(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			return this.values[index];
		}

		// Token: 0x060037CA RID: 14282 RVA: 0x000D575C File Offset: 0x000D395C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SortedList.SortedListEnumerator(this, 0, this._size, 3);
		}

		// Token: 0x060037CB RID: 14283 RVA: 0x000D576C File Offset: 0x000D396C
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new SortedList.SortedListEnumerator(this, 0, this._size, 3);
		}

		// Token: 0x060037CC RID: 14284 RVA: 0x000D577C File Offset: 0x000D397C
		public virtual object GetKey(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			return this.keys[index];
		}

		// Token: 0x060037CD RID: 14285 RVA: 0x000D57A8 File Offset: 0x000D39A8
		public virtual IList GetKeyList()
		{
			if (this.keyList == null)
			{
				this.keyList = new SortedList.KeyList(this);
			}
			return this.keyList;
		}

		// Token: 0x060037CE RID: 14286 RVA: 0x000D57C4 File Offset: 0x000D39C4
		public virtual IList GetValueList()
		{
			if (this.valueList == null)
			{
				this.valueList = new SortedList.ValueList(this);
			}
			return this.valueList;
		}

		// Token: 0x1700086F RID: 2159
		public virtual object this[object key]
		{
			get
			{
				int num = this.IndexOfKey(key);
				if (num >= 0)
				{
					return this.values[num];
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
				}
				int num = Array.BinarySearch(this.keys, 0, this._size, key, this.comparer);
				if (num >= 0)
				{
					this.values[num] = value;
					this.version++;
					return;
				}
				this.Insert(~num, key, value);
			}
		}

		// Token: 0x060037D1 RID: 14289 RVA: 0x000D586C File Offset: 0x000D3A6C
		public virtual int IndexOfKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
			}
			int num = Array.BinarySearch(this.keys, 0, this._size, key, this.comparer);
			if (num < 0)
			{
				return -1;
			}
			return num;
		}

		// Token: 0x060037D2 RID: 14290 RVA: 0x000D58B2 File Offset: 0x000D3AB2
		public virtual int IndexOfValue(object value)
		{
			return Array.IndexOf<object>(this.values, value, 0, this._size);
		}

		// Token: 0x060037D3 RID: 14291 RVA: 0x000D58C8 File Offset: 0x000D3AC8
		private void Insert(int index, object key, object value)
		{
			if (this._size == this.keys.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			if (index < this._size)
			{
				Array.Copy(this.keys, index, this.keys, index + 1, this._size - index);
				Array.Copy(this.values, index, this.values, index + 1, this._size - index);
			}
			this.keys[index] = key;
			this.values[index] = value;
			this._size++;
			this.version++;
		}

		// Token: 0x060037D4 RID: 14292 RVA: 0x000D5964 File Offset: 0x000D3B64
		public virtual void RemoveAt(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this.keys, index + 1, this.keys, index, this._size - index);
				Array.Copy(this.values, index + 1, this.values, index, this._size - index);
			}
			this.keys[this._size] = null;
			this.values[this._size] = null;
			this.version++;
		}

		// Token: 0x060037D5 RID: 14293 RVA: 0x000D5A10 File Offset: 0x000D3C10
		public virtual void Remove(object key)
		{
			int num = this.IndexOfKey(key);
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
		}

		// Token: 0x060037D6 RID: 14294 RVA: 0x000D5A30 File Offset: 0x000D3C30
		public virtual void SetByIndex(int index, object value)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			this.values[index] = value;
			this.version++;
		}

		// Token: 0x060037D7 RID: 14295 RVA: 0x000D5A6B File Offset: 0x000D3C6B
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static SortedList Synchronized(SortedList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new SortedList.SyncSortedList(list);
		}

		// Token: 0x060037D8 RID: 14296 RVA: 0x000D5A81 File Offset: 0x000D3C81
		public virtual void TrimToSize()
		{
			this.Capacity = this._size;
		}

		// Token: 0x04001849 RID: 6217
		private object[] keys;

		// Token: 0x0400184A RID: 6218
		private object[] values;

		// Token: 0x0400184B RID: 6219
		private int _size;

		// Token: 0x0400184C RID: 6220
		private int version;

		// Token: 0x0400184D RID: 6221
		private IComparer comparer;

		// Token: 0x0400184E RID: 6222
		private SortedList.KeyList keyList;

		// Token: 0x0400184F RID: 6223
		private SortedList.ValueList valueList;

		// Token: 0x04001850 RID: 6224
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04001851 RID: 6225
		private const int _defaultCapacity = 16;

		// Token: 0x04001852 RID: 6226
		private static object[] emptyArray = EmptyArray<object>.Value;

		// Token: 0x02000B86 RID: 2950
		[Serializable]
		private class SyncSortedList : SortedList
		{
			// Token: 0x06006CF5 RID: 27893 RVA: 0x001777C4 File Offset: 0x001759C4
			internal SyncSortedList(SortedList list)
			{
				this._list = list;
				this._root = list.SyncRoot;
			}

			// Token: 0x170012A8 RID: 4776
			// (get) Token: 0x06006CF6 RID: 27894 RVA: 0x001777E0 File Offset: 0x001759E0
			public override int Count
			{
				get
				{
					object root = this._root;
					int count;
					lock (root)
					{
						count = this._list.Count;
					}
					return count;
				}
			}

			// Token: 0x170012A9 RID: 4777
			// (get) Token: 0x06006CF7 RID: 27895 RVA: 0x00177828 File Offset: 0x00175A28
			public override object SyncRoot
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x170012AA RID: 4778
			// (get) Token: 0x06006CF8 RID: 27896 RVA: 0x00177830 File Offset: 0x00175A30
			public override bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x170012AB RID: 4779
			// (get) Token: 0x06006CF9 RID: 27897 RVA: 0x0017783D File Offset: 0x00175A3D
			public override bool IsFixedSize
			{
				get
				{
					return this._list.IsFixedSize;
				}
			}

			// Token: 0x170012AC RID: 4780
			// (get) Token: 0x06006CFA RID: 27898 RVA: 0x0017784A File Offset: 0x00175A4A
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170012AD RID: 4781
			public override object this[object key]
			{
				get
				{
					object root = this._root;
					object result;
					lock (root)
					{
						result = this._list[key];
					}
					return result;
				}
				set
				{
					object root = this._root;
					lock (root)
					{
						this._list[key] = value;
					}
				}
			}

			// Token: 0x06006CFD RID: 27901 RVA: 0x001778E0 File Offset: 0x00175AE0
			public override void Add(object key, object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Add(key, value);
				}
			}

			// Token: 0x170012AE RID: 4782
			// (get) Token: 0x06006CFE RID: 27902 RVA: 0x00177928 File Offset: 0x00175B28
			public override int Capacity
			{
				get
				{
					object root = this._root;
					int capacity;
					lock (root)
					{
						capacity = this._list.Capacity;
					}
					return capacity;
				}
			}

			// Token: 0x06006CFF RID: 27903 RVA: 0x00177970 File Offset: 0x00175B70
			public override void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._list.Clear();
				}
			}

			// Token: 0x06006D00 RID: 27904 RVA: 0x001779B8 File Offset: 0x00175BB8
			public override object Clone()
			{
				object root = this._root;
				object result;
				lock (root)
				{
					result = this._list.Clone();
				}
				return result;
			}

			// Token: 0x06006D01 RID: 27905 RVA: 0x00177A00 File Offset: 0x00175C00
			public override bool Contains(object key)
			{
				object root = this._root;
				bool result;
				lock (root)
				{
					result = this._list.Contains(key);
				}
				return result;
			}

			// Token: 0x06006D02 RID: 27906 RVA: 0x00177A48 File Offset: 0x00175C48
			public override bool ContainsKey(object key)
			{
				object root = this._root;
				bool result;
				lock (root)
				{
					result = this._list.ContainsKey(key);
				}
				return result;
			}

			// Token: 0x06006D03 RID: 27907 RVA: 0x00177A90 File Offset: 0x00175C90
			public override bool ContainsValue(object key)
			{
				object root = this._root;
				bool result;
				lock (root)
				{
					result = this._list.ContainsValue(key);
				}
				return result;
			}

			// Token: 0x06006D04 RID: 27908 RVA: 0x00177AD8 File Offset: 0x00175CD8
			public override void CopyTo(Array array, int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(array, index);
				}
			}

			// Token: 0x06006D05 RID: 27909 RVA: 0x00177B20 File Offset: 0x00175D20
			public override object GetByIndex(int index)
			{
				object root = this._root;
				object byIndex;
				lock (root)
				{
					byIndex = this._list.GetByIndex(index);
				}
				return byIndex;
			}

			// Token: 0x06006D06 RID: 27910 RVA: 0x00177B68 File Offset: 0x00175D68
			public override IDictionaryEnumerator GetEnumerator()
			{
				object root = this._root;
				IDictionaryEnumerator enumerator;
				lock (root)
				{
					enumerator = this._list.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06006D07 RID: 27911 RVA: 0x00177BB0 File Offset: 0x00175DB0
			public override object GetKey(int index)
			{
				object root = this._root;
				object key;
				lock (root)
				{
					key = this._list.GetKey(index);
				}
				return key;
			}

			// Token: 0x06006D08 RID: 27912 RVA: 0x00177BF8 File Offset: 0x00175DF8
			public override IList GetKeyList()
			{
				object root = this._root;
				IList keyList;
				lock (root)
				{
					keyList = this._list.GetKeyList();
				}
				return keyList;
			}

			// Token: 0x06006D09 RID: 27913 RVA: 0x00177C40 File Offset: 0x00175E40
			public override IList GetValueList()
			{
				object root = this._root;
				IList valueList;
				lock (root)
				{
					valueList = this._list.GetValueList();
				}
				return valueList;
			}

			// Token: 0x06006D0A RID: 27914 RVA: 0x00177C88 File Offset: 0x00175E88
			public override int IndexOfKey(object key)
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
				}
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.IndexOfKey(key);
				}
				return result;
			}

			// Token: 0x06006D0B RID: 27915 RVA: 0x00177CE8 File Offset: 0x00175EE8
			public override int IndexOfValue(object value)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.IndexOfValue(value);
				}
				return result;
			}

			// Token: 0x06006D0C RID: 27916 RVA: 0x00177D30 File Offset: 0x00175F30
			public override void RemoveAt(int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.RemoveAt(index);
				}
			}

			// Token: 0x06006D0D RID: 27917 RVA: 0x00177D78 File Offset: 0x00175F78
			public override void Remove(object key)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Remove(key);
				}
			}

			// Token: 0x06006D0E RID: 27918 RVA: 0x00177DC0 File Offset: 0x00175FC0
			public override void SetByIndex(int index, object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.SetByIndex(index, value);
				}
			}

			// Token: 0x06006D0F RID: 27919 RVA: 0x00177E08 File Offset: 0x00176008
			internal override KeyValuePairs[] ToKeyValuePairsArray()
			{
				return this._list.ToKeyValuePairsArray();
			}

			// Token: 0x06006D10 RID: 27920 RVA: 0x00177E18 File Offset: 0x00176018
			public override void TrimToSize()
			{
				object root = this._root;
				lock (root)
				{
					this._list.TrimToSize();
				}
			}

			// Token: 0x04003494 RID: 13460
			private SortedList _list;

			// Token: 0x04003495 RID: 13461
			private object _root;
		}

		// Token: 0x02000B87 RID: 2951
		[Serializable]
		private class SortedListEnumerator : IDictionaryEnumerator, IEnumerator, ICloneable
		{
			// Token: 0x06006D11 RID: 27921 RVA: 0x00177E60 File Offset: 0x00176060
			internal SortedListEnumerator(SortedList sortedList, int index, int count, int getObjRetType)
			{
				this.sortedList = sortedList;
				this.index = index;
				this.startIndex = index;
				this.endIndex = index + count;
				this.version = sortedList.version;
				this.getObjectRetType = getObjRetType;
				this.current = false;
			}

			// Token: 0x06006D12 RID: 27922 RVA: 0x00177EAC File Offset: 0x001760AC
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x170012AF RID: 4783
			// (get) Token: 0x06006D13 RID: 27923 RVA: 0x00177EB4 File Offset: 0x001760B4
			public virtual object Key
			{
				get
				{
					if (this.version != this.sortedList.version)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
					}
					if (!this.current)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
					}
					return this.key;
				}
			}

			// Token: 0x06006D14 RID: 27924 RVA: 0x00177F04 File Offset: 0x00176104
			public virtual bool MoveNext()
			{
				if (this.version != this.sortedList.version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				if (this.index < this.endIndex)
				{
					this.key = this.sortedList.keys[this.index];
					this.value = this.sortedList.values[this.index];
					this.index++;
					this.current = true;
					return true;
				}
				this.key = null;
				this.value = null;
				this.current = false;
				return false;
			}

			// Token: 0x170012B0 RID: 4784
			// (get) Token: 0x06006D15 RID: 27925 RVA: 0x00177FA0 File Offset: 0x001761A0
			public virtual DictionaryEntry Entry
			{
				get
				{
					if (this.version != this.sortedList.version)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
					}
					if (!this.current)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
					}
					return new DictionaryEntry(this.key, this.value);
				}
			}

			// Token: 0x170012B1 RID: 4785
			// (get) Token: 0x06006D16 RID: 27926 RVA: 0x00177FFC File Offset: 0x001761FC
			public virtual object Current
			{
				get
				{
					if (!this.current)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
					}
					if (this.getObjectRetType == 1)
					{
						return this.key;
					}
					if (this.getObjectRetType == 2)
					{
						return this.value;
					}
					return new DictionaryEntry(this.key, this.value);
				}
			}

			// Token: 0x170012B2 RID: 4786
			// (get) Token: 0x06006D17 RID: 27927 RVA: 0x00178058 File Offset: 0x00176258
			public virtual object Value
			{
				get
				{
					if (this.version != this.sortedList.version)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
					}
					if (!this.current)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
					}
					return this.value;
				}
			}

			// Token: 0x06006D18 RID: 27928 RVA: 0x001780A8 File Offset: 0x001762A8
			public virtual void Reset()
			{
				if (this.version != this.sortedList.version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				this.index = this.startIndex;
				this.current = false;
				this.key = null;
				this.value = null;
			}

			// Token: 0x04003496 RID: 13462
			private SortedList sortedList;

			// Token: 0x04003497 RID: 13463
			private object key;

			// Token: 0x04003498 RID: 13464
			private object value;

			// Token: 0x04003499 RID: 13465
			private int index;

			// Token: 0x0400349A RID: 13466
			private int startIndex;

			// Token: 0x0400349B RID: 13467
			private int endIndex;

			// Token: 0x0400349C RID: 13468
			private int version;

			// Token: 0x0400349D RID: 13469
			private bool current;

			// Token: 0x0400349E RID: 13470
			private int getObjectRetType;

			// Token: 0x0400349F RID: 13471
			internal const int Keys = 1;

			// Token: 0x040034A0 RID: 13472
			internal const int Values = 2;

			// Token: 0x040034A1 RID: 13473
			internal const int DictEntry = 3;
		}

		// Token: 0x02000B88 RID: 2952
		[Serializable]
		private class KeyList : IList, ICollection, IEnumerable
		{
			// Token: 0x06006D19 RID: 27929 RVA: 0x001780F9 File Offset: 0x001762F9
			internal KeyList(SortedList sortedList)
			{
				this.sortedList = sortedList;
			}

			// Token: 0x170012B3 RID: 4787
			// (get) Token: 0x06006D1A RID: 27930 RVA: 0x00178108 File Offset: 0x00176308
			public virtual int Count
			{
				get
				{
					return this.sortedList._size;
				}
			}

			// Token: 0x170012B4 RID: 4788
			// (get) Token: 0x06006D1B RID: 27931 RVA: 0x00178115 File Offset: 0x00176315
			public virtual bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170012B5 RID: 4789
			// (get) Token: 0x06006D1C RID: 27932 RVA: 0x00178118 File Offset: 0x00176318
			public virtual bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170012B6 RID: 4790
			// (get) Token: 0x06006D1D RID: 27933 RVA: 0x0017811B File Offset: 0x0017631B
			public virtual bool IsSynchronized
			{
				get
				{
					return this.sortedList.IsSynchronized;
				}
			}

			// Token: 0x170012B7 RID: 4791
			// (get) Token: 0x06006D1E RID: 27934 RVA: 0x00178128 File Offset: 0x00176328
			public virtual object SyncRoot
			{
				get
				{
					return this.sortedList.SyncRoot;
				}
			}

			// Token: 0x06006D1F RID: 27935 RVA: 0x00178135 File Offset: 0x00176335
			public virtual int Add(object key)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
			}

			// Token: 0x06006D20 RID: 27936 RVA: 0x00178146 File Offset: 0x00176346
			public virtual void Clear()
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
			}

			// Token: 0x06006D21 RID: 27937 RVA: 0x00178157 File Offset: 0x00176357
			public virtual bool Contains(object key)
			{
				return this.sortedList.Contains(key);
			}

			// Token: 0x06006D22 RID: 27938 RVA: 0x00178165 File Offset: 0x00176365
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				if (array != null && array.Rank != 1)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
				}
				Array.Copy(this.sortedList.keys, 0, array, arrayIndex, this.sortedList.Count);
			}

			// Token: 0x06006D23 RID: 27939 RVA: 0x001781A1 File Offset: 0x001763A1
			public virtual void Insert(int index, object value)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
			}

			// Token: 0x170012B8 RID: 4792
			public virtual object this[int index]
			{
				get
				{
					return this.sortedList.GetKey(index);
				}
				set
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_KeyCollectionSet"));
				}
			}

			// Token: 0x06006D26 RID: 27942 RVA: 0x001781D1 File Offset: 0x001763D1
			public virtual IEnumerator GetEnumerator()
			{
				return new SortedList.SortedListEnumerator(this.sortedList, 0, this.sortedList.Count, 1);
			}

			// Token: 0x06006D27 RID: 27943 RVA: 0x001781EC File Offset: 0x001763EC
			public virtual int IndexOf(object key)
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
				}
				int num = Array.BinarySearch(this.sortedList.keys, 0, this.sortedList.Count, key, this.sortedList.comparer);
				if (num >= 0)
				{
					return num;
				}
				return -1;
			}

			// Token: 0x06006D28 RID: 27944 RVA: 0x00178241 File Offset: 0x00176441
			public virtual void Remove(object key)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
			}

			// Token: 0x06006D29 RID: 27945 RVA: 0x00178252 File Offset: 0x00176452
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
			}

			// Token: 0x040034A2 RID: 13474
			private SortedList sortedList;
		}

		// Token: 0x02000B89 RID: 2953
		[Serializable]
		private class ValueList : IList, ICollection, IEnumerable
		{
			// Token: 0x06006D2A RID: 27946 RVA: 0x00178263 File Offset: 0x00176463
			internal ValueList(SortedList sortedList)
			{
				this.sortedList = sortedList;
			}

			// Token: 0x170012B9 RID: 4793
			// (get) Token: 0x06006D2B RID: 27947 RVA: 0x00178272 File Offset: 0x00176472
			public virtual int Count
			{
				get
				{
					return this.sortedList._size;
				}
			}

			// Token: 0x170012BA RID: 4794
			// (get) Token: 0x06006D2C RID: 27948 RVA: 0x0017827F File Offset: 0x0017647F
			public virtual bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170012BB RID: 4795
			// (get) Token: 0x06006D2D RID: 27949 RVA: 0x00178282 File Offset: 0x00176482
			public virtual bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170012BC RID: 4796
			// (get) Token: 0x06006D2E RID: 27950 RVA: 0x00178285 File Offset: 0x00176485
			public virtual bool IsSynchronized
			{
				get
				{
					return this.sortedList.IsSynchronized;
				}
			}

			// Token: 0x170012BD RID: 4797
			// (get) Token: 0x06006D2F RID: 27951 RVA: 0x00178292 File Offset: 0x00176492
			public virtual object SyncRoot
			{
				get
				{
					return this.sortedList.SyncRoot;
				}
			}

			// Token: 0x06006D30 RID: 27952 RVA: 0x0017829F File Offset: 0x0017649F
			public virtual int Add(object key)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
			}

			// Token: 0x06006D31 RID: 27953 RVA: 0x001782B0 File Offset: 0x001764B0
			public virtual void Clear()
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
			}

			// Token: 0x06006D32 RID: 27954 RVA: 0x001782C1 File Offset: 0x001764C1
			public virtual bool Contains(object value)
			{
				return this.sortedList.ContainsValue(value);
			}

			// Token: 0x06006D33 RID: 27955 RVA: 0x001782CF File Offset: 0x001764CF
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				if (array != null && array.Rank != 1)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
				}
				Array.Copy(this.sortedList.values, 0, array, arrayIndex, this.sortedList.Count);
			}

			// Token: 0x06006D34 RID: 27956 RVA: 0x0017830B File Offset: 0x0017650B
			public virtual void Insert(int index, object value)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
			}

			// Token: 0x170012BE RID: 4798
			public virtual object this[int index]
			{
				get
				{
					return this.sortedList.GetByIndex(index);
				}
				set
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
				}
			}

			// Token: 0x06006D37 RID: 27959 RVA: 0x0017833B File Offset: 0x0017653B
			public virtual IEnumerator GetEnumerator()
			{
				return new SortedList.SortedListEnumerator(this.sortedList, 0, this.sortedList.Count, 2);
			}

			// Token: 0x06006D38 RID: 27960 RVA: 0x00178355 File Offset: 0x00176555
			public virtual int IndexOf(object value)
			{
				return Array.IndexOf<object>(this.sortedList.values, value, 0, this.sortedList.Count);
			}

			// Token: 0x06006D39 RID: 27961 RVA: 0x00178374 File Offset: 0x00176574
			public virtual void Remove(object value)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
			}

			// Token: 0x06006D3A RID: 27962 RVA: 0x00178385 File Offset: 0x00176585
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
			}

			// Token: 0x040034A3 RID: 13475
			private SortedList sortedList;
		}

		// Token: 0x02000B8A RID: 2954
		internal class SortedListDebugView
		{
			// Token: 0x06006D3B RID: 27963 RVA: 0x00178396 File Offset: 0x00176596
			public SortedListDebugView(SortedList sortedList)
			{
				if (sortedList == null)
				{
					throw new ArgumentNullException("sortedList");
				}
				this.sortedList = sortedList;
			}

			// Token: 0x170012BF RID: 4799
			// (get) Token: 0x06006D3C RID: 27964 RVA: 0x001783B3 File Offset: 0x001765B3
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public KeyValuePairs[] Items
			{
				get
				{
					return this.sortedList.ToKeyValuePairsArray();
				}
			}

			// Token: 0x040034A4 RID: 13476
			private SortedList sortedList;
		}
	}
}
