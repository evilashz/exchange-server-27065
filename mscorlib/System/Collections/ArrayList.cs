using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections
{
	// Token: 0x02000463 RID: 1123
	[DebuggerTypeProxy(typeof(ArrayList.ArrayListDebugView))]
	[DebuggerDisplay("Count = {Count}")]
	[ComVisible(true)]
	[Serializable]
	public class ArrayList : IList, ICollection, IEnumerable, ICloneable
	{
		// Token: 0x060036AF RID: 13999 RVA: 0x000D1938 File Offset: 0x000CFB38
		internal ArrayList(bool trash)
		{
		}

		// Token: 0x060036B0 RID: 14000 RVA: 0x000D1940 File Offset: 0x000CFB40
		public ArrayList()
		{
			this._items = ArrayList.emptyArray;
		}

		// Token: 0x060036B1 RID: 14001 RVA: 0x000D1954 File Offset: 0x000CFB54
		public ArrayList(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_MustBeNonNegNum", new object[]
				{
					"capacity"
				}));
			}
			if (capacity == 0)
			{
				this._items = ArrayList.emptyArray;
				return;
			}
			this._items = new object[capacity];
		}

		// Token: 0x060036B2 RID: 14002 RVA: 0x000D19AC File Offset: 0x000CFBAC
		public ArrayList(ICollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c", Environment.GetResourceString("ArgumentNull_Collection"));
			}
			int count = c.Count;
			if (count == 0)
			{
				this._items = ArrayList.emptyArray;
				return;
			}
			this._items = new object[count];
			this.AddRange(c);
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x060036B3 RID: 14003 RVA: 0x000D1A00 File Offset: 0x000CFC00
		// (set) Token: 0x060036B4 RID: 14004 RVA: 0x000D1A0C File Offset: 0x000CFC0C
		public virtual int Capacity
		{
			get
			{
				return this._items.Length;
			}
			set
			{
				if (value < this._size)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
				}
				if (value != this._items.Length)
				{
					if (value > 0)
					{
						object[] array = new object[value];
						if (this._size > 0)
						{
							Array.Copy(this._items, 0, array, 0, this._size);
						}
						this._items = array;
						return;
					}
					this._items = new object[4];
				}
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x060036B5 RID: 14005 RVA: 0x000D1A7E File Offset: 0x000CFC7E
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x060036B6 RID: 14006 RVA: 0x000D1A86 File Offset: 0x000CFC86
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x060036B7 RID: 14007 RVA: 0x000D1A89 File Offset: 0x000CFC89
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x060036B8 RID: 14008 RVA: 0x000D1A8C File Offset: 0x000CFC8C
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x060036B9 RID: 14009 RVA: 0x000D1A8F File Offset: 0x000CFC8F
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

		// Token: 0x1700082C RID: 2092
		public virtual object this[int index]
		{
			get
			{
				if (index < 0 || index >= this._size)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				return this._items[index];
			}
			set
			{
				if (index < 0 || index >= this._size)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				this._items[index] = value;
				this._version++;
			}
		}

		// Token: 0x060036BC RID: 14012 RVA: 0x000D1B18 File Offset: 0x000CFD18
		public static ArrayList Adapter(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.IListWrapper(list);
		}

		// Token: 0x060036BD RID: 14013 RVA: 0x000D1B30 File Offset: 0x000CFD30
		public virtual int Add(object value)
		{
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			this._items[this._size] = value;
			this._version++;
			int size = this._size;
			this._size = size + 1;
			return size;
		}

		// Token: 0x060036BE RID: 14014 RVA: 0x000D1B88 File Offset: 0x000CFD88
		public virtual void AddRange(ICollection c)
		{
			this.InsertRange(this._size, c);
		}

		// Token: 0x060036BF RID: 14015 RVA: 0x000D1B98 File Offset: 0x000CFD98
		public virtual int BinarySearch(int index, int count, object value, IComparer comparer)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (this._size - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			return Array.BinarySearch(this._items, index, count, value, comparer);
		}

		// Token: 0x060036C0 RID: 14016 RVA: 0x000D1C02 File Offset: 0x000CFE02
		public virtual int BinarySearch(object value)
		{
			return this.BinarySearch(0, this.Count, value, null);
		}

		// Token: 0x060036C1 RID: 14017 RVA: 0x000D1C13 File Offset: 0x000CFE13
		public virtual int BinarySearch(object value, IComparer comparer)
		{
			return this.BinarySearch(0, this.Count, value, comparer);
		}

		// Token: 0x060036C2 RID: 14018 RVA: 0x000D1C24 File Offset: 0x000CFE24
		public virtual void Clear()
		{
			if (this._size > 0)
			{
				Array.Clear(this._items, 0, this._size);
				this._size = 0;
			}
			this._version++;
		}

		// Token: 0x060036C3 RID: 14019 RVA: 0x000D1C58 File Offset: 0x000CFE58
		public virtual object Clone()
		{
			ArrayList arrayList = new ArrayList(this._size);
			arrayList._size = this._size;
			arrayList._version = this._version;
			Array.Copy(this._items, 0, arrayList._items, 0, this._size);
			return arrayList;
		}

		// Token: 0x060036C4 RID: 14020 RVA: 0x000D1CA4 File Offset: 0x000CFEA4
		public virtual bool Contains(object item)
		{
			if (item == null)
			{
				for (int i = 0; i < this._size; i++)
				{
					if (this._items[i] == null)
					{
						return true;
					}
				}
				return false;
			}
			for (int j = 0; j < this._size; j++)
			{
				if (this._items[j] != null && this._items[j].Equals(item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060036C5 RID: 14021 RVA: 0x000D1D01 File Offset: 0x000CFF01
		public virtual void CopyTo(Array array)
		{
			this.CopyTo(array, 0);
		}

		// Token: 0x060036C6 RID: 14022 RVA: 0x000D1D0B File Offset: 0x000CFF0B
		public virtual void CopyTo(Array array, int arrayIndex)
		{
			if (array != null && array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
			}
			Array.Copy(this._items, 0, array, arrayIndex, this._size);
		}

		// Token: 0x060036C7 RID: 14023 RVA: 0x000D1D40 File Offset: 0x000CFF40
		public virtual void CopyTo(int index, Array array, int arrayIndex, int count)
		{
			if (this._size - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (array != null && array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
			}
			Array.Copy(this._items, index, array, arrayIndex, count);
		}

		// Token: 0x060036C8 RID: 14024 RVA: 0x000D1D98 File Offset: 0x000CFF98
		private void EnsureCapacity(int min)
		{
			if (this._items.Length < min)
			{
				int num = (this._items.Length == 0) ? 4 : (this._items.Length * 2);
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
		}

		// Token: 0x060036C9 RID: 14025 RVA: 0x000D1DE2 File Offset: 0x000CFFE2
		public static IList FixedSize(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.FixedSizeList(list);
		}

		// Token: 0x060036CA RID: 14026 RVA: 0x000D1DF8 File Offset: 0x000CFFF8
		public static ArrayList FixedSize(ArrayList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.FixedSizeArrayList(list);
		}

		// Token: 0x060036CB RID: 14027 RVA: 0x000D1E0E File Offset: 0x000D000E
		public virtual IEnumerator GetEnumerator()
		{
			return new ArrayList.ArrayListEnumeratorSimple(this);
		}

		// Token: 0x060036CC RID: 14028 RVA: 0x000D1E18 File Offset: 0x000D0018
		public virtual IEnumerator GetEnumerator(int index, int count)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (this._size - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			return new ArrayList.ArrayListEnumerator(this, index, count);
		}

		// Token: 0x060036CD RID: 14029 RVA: 0x000D1E7A File Offset: 0x000D007A
		public virtual int IndexOf(object value)
		{
			return Array.IndexOf(this._items, value, 0, this._size);
		}

		// Token: 0x060036CE RID: 14030 RVA: 0x000D1E8F File Offset: 0x000D008F
		public virtual int IndexOf(object value, int startIndex)
		{
			if (startIndex > this._size)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			return Array.IndexOf(this._items, value, startIndex, this._size - startIndex);
		}

		// Token: 0x060036CF RID: 14031 RVA: 0x000D1EC4 File Offset: 0x000D00C4
		public virtual int IndexOf(object value, int startIndex, int count)
		{
			if (startIndex > this._size)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (count < 0 || startIndex > this._size - count)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
			}
			return Array.IndexOf(this._items, value, startIndex, count);
		}

		// Token: 0x060036D0 RID: 14032 RVA: 0x000D1F24 File Offset: 0x000D0124
		public virtual void Insert(int index, object value)
		{
			if (index < 0 || index > this._size)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_ArrayListInsert"));
			}
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			if (index < this._size)
			{
				Array.Copy(this._items, index, this._items, index + 1, this._size - index);
			}
			this._items[index] = value;
			this._size++;
			this._version++;
		}

		// Token: 0x060036D1 RID: 14033 RVA: 0x000D1FBC File Offset: 0x000D01BC
		public virtual void InsertRange(int index, ICollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c", Environment.GetResourceString("ArgumentNull_Collection"));
			}
			if (index < 0 || index > this._size)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			int count = c.Count;
			if (count > 0)
			{
				this.EnsureCapacity(this._size + count);
				if (index < this._size)
				{
					Array.Copy(this._items, index, this._items, index + count, this._size - index);
				}
				object[] array = new object[count];
				c.CopyTo(array, 0);
				array.CopyTo(this._items, index);
				this._size += count;
				this._version++;
			}
		}

		// Token: 0x060036D2 RID: 14034 RVA: 0x000D207A File Offset: 0x000D027A
		public virtual int LastIndexOf(object value)
		{
			return this.LastIndexOf(value, this._size - 1, this._size);
		}

		// Token: 0x060036D3 RID: 14035 RVA: 0x000D2091 File Offset: 0x000D0291
		public virtual int LastIndexOf(object value, int startIndex)
		{
			if (startIndex >= this._size)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			return this.LastIndexOf(value, startIndex, startIndex + 1);
		}

		// Token: 0x060036D4 RID: 14036 RVA: 0x000D20BC File Offset: 0x000D02BC
		public virtual int LastIndexOf(object value, int startIndex, int count)
		{
			if (this.Count != 0 && (startIndex < 0 || count < 0))
			{
				throw new ArgumentOutOfRangeException((startIndex < 0) ? "startIndex" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (this._size == 0)
			{
				return -1;
			}
			if (startIndex >= this._size || count > startIndex + 1)
			{
				throw new ArgumentOutOfRangeException((startIndex >= this._size) ? "startIndex" : "count", Environment.GetResourceString("ArgumentOutOfRange_BiggerThanCollection"));
			}
			return Array.LastIndexOf(this._items, value, startIndex, count);
		}

		// Token: 0x060036D5 RID: 14037 RVA: 0x000D2145 File Offset: 0x000D0345
		public static IList ReadOnly(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.ReadOnlyList(list);
		}

		// Token: 0x060036D6 RID: 14038 RVA: 0x000D215B File Offset: 0x000D035B
		public static ArrayList ReadOnly(ArrayList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.ReadOnlyArrayList(list);
		}

		// Token: 0x060036D7 RID: 14039 RVA: 0x000D2174 File Offset: 0x000D0374
		public virtual void Remove(object obj)
		{
			int num = this.IndexOf(obj);
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
		}

		// Token: 0x060036D8 RID: 14040 RVA: 0x000D2194 File Offset: 0x000D0394
		public virtual void RemoveAt(int index)
		{
			if (index < 0 || index >= this._size)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this._items, index + 1, this._items, index, this._size - index);
			}
			this._items[this._size] = null;
			this._version++;
		}

		// Token: 0x060036D9 RID: 14041 RVA: 0x000D2214 File Offset: 0x000D0414
		public virtual void RemoveRange(int index, int count)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (this._size - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (count > 0)
			{
				int i = this._size;
				this._size -= count;
				if (index < this._size)
				{
					Array.Copy(this._items, index + count, this._items, index, this._size - index);
				}
				while (i > this._size)
				{
					this._items[--i] = null;
				}
				this._version++;
			}
		}

		// Token: 0x060036DA RID: 14042 RVA: 0x000D22D4 File Offset: 0x000D04D4
		public static ArrayList Repeat(object value, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			ArrayList arrayList = new ArrayList((count > 4) ? count : 4);
			for (int i = 0; i < count; i++)
			{
				arrayList.Add(value);
			}
			return arrayList;
		}

		// Token: 0x060036DB RID: 14043 RVA: 0x000D231D File Offset: 0x000D051D
		public virtual void Reverse()
		{
			this.Reverse(0, this.Count);
		}

		// Token: 0x060036DC RID: 14044 RVA: 0x000D232C File Offset: 0x000D052C
		public virtual void Reverse(int index, int count)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (this._size - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			Array.Reverse(this._items, index, count);
			this._version++;
		}

		// Token: 0x060036DD RID: 14045 RVA: 0x000D23A4 File Offset: 0x000D05A4
		public virtual void SetRange(int index, ICollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c", Environment.GetResourceString("ArgumentNull_Collection"));
			}
			int count = c.Count;
			if (index < 0 || index > this._size - count)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (count > 0)
			{
				c.CopyTo(this._items, index);
				this._version++;
			}
		}

		// Token: 0x060036DE RID: 14046 RVA: 0x000D2414 File Offset: 0x000D0614
		public virtual ArrayList GetRange(int index, int count)
		{
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (this._size - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			return new ArrayList.Range(this, index, count);
		}

		// Token: 0x060036DF RID: 14047 RVA: 0x000D246C File Offset: 0x000D066C
		public virtual void Sort()
		{
			this.Sort(0, this.Count, Comparer.Default);
		}

		// Token: 0x060036E0 RID: 14048 RVA: 0x000D2480 File Offset: 0x000D0680
		public virtual void Sort(IComparer comparer)
		{
			this.Sort(0, this.Count, comparer);
		}

		// Token: 0x060036E1 RID: 14049 RVA: 0x000D2490 File Offset: 0x000D0690
		public virtual void Sort(int index, int count, IComparer comparer)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (this._size - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			Array.Sort(this._items, index, count, comparer);
			this._version++;
		}

		// Token: 0x060036E2 RID: 14050 RVA: 0x000D2506 File Offset: 0x000D0706
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static IList Synchronized(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.SyncIList(list);
		}

		// Token: 0x060036E3 RID: 14051 RVA: 0x000D251C File Offset: 0x000D071C
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static ArrayList Synchronized(ArrayList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.SyncArrayList(list);
		}

		// Token: 0x060036E4 RID: 14052 RVA: 0x000D2534 File Offset: 0x000D0734
		public virtual object[] ToArray()
		{
			object[] array = new object[this._size];
			Array.Copy(this._items, 0, array, 0, this._size);
			return array;
		}

		// Token: 0x060036E5 RID: 14053 RVA: 0x000D2564 File Offset: 0x000D0764
		[SecuritySafeCritical]
		public virtual Array ToArray(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Array array = Array.UnsafeCreateInstance(type, this._size);
			Array.Copy(this._items, 0, array, 0, this._size);
			return array;
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x000D25A7 File Offset: 0x000D07A7
		public virtual void TrimToSize()
		{
			this.Capacity = this._size;
		}

		// Token: 0x04001809 RID: 6153
		private object[] _items;

		// Token: 0x0400180A RID: 6154
		private int _size;

		// Token: 0x0400180B RID: 6155
		private int _version;

		// Token: 0x0400180C RID: 6156
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x0400180D RID: 6157
		private const int _defaultCapacity = 4;

		// Token: 0x0400180E RID: 6158
		private static readonly object[] emptyArray = EmptyArray<object>.Value;

		// Token: 0x02000B6D RID: 2925
		[Serializable]
		private class IListWrapper : ArrayList
		{
			// Token: 0x06006B95 RID: 27541 RVA: 0x00173988 File Offset: 0x00171B88
			internal IListWrapper(IList list)
			{
				this._list = list;
				this._version = 0;
			}

			// Token: 0x1700124C RID: 4684
			// (get) Token: 0x06006B96 RID: 27542 RVA: 0x0017399E File Offset: 0x00171B9E
			// (set) Token: 0x06006B97 RID: 27543 RVA: 0x001739AB File Offset: 0x00171BAB
			public override int Capacity
			{
				get
				{
					return this._list.Count;
				}
				set
				{
					if (value < this.Count)
					{
						throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
					}
				}
			}

			// Token: 0x1700124D RID: 4685
			// (get) Token: 0x06006B98 RID: 27544 RVA: 0x001739CB File Offset: 0x00171BCB
			public override int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x1700124E RID: 4686
			// (get) Token: 0x06006B99 RID: 27545 RVA: 0x001739D8 File Offset: 0x00171BD8
			public override bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x1700124F RID: 4687
			// (get) Token: 0x06006B9A RID: 27546 RVA: 0x001739E5 File Offset: 0x00171BE5
			public override bool IsFixedSize
			{
				get
				{
					return this._list.IsFixedSize;
				}
			}

			// Token: 0x17001250 RID: 4688
			// (get) Token: 0x06006B9B RID: 27547 RVA: 0x001739F2 File Offset: 0x00171BF2
			public override bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x17001251 RID: 4689
			public override object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					this._list[index] = value;
					this._version++;
				}
			}

			// Token: 0x17001252 RID: 4690
			// (get) Token: 0x06006B9E RID: 27550 RVA: 0x00173A2A File Offset: 0x00171C2A
			public override object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x06006B9F RID: 27551 RVA: 0x00173A38 File Offset: 0x00171C38
			public override int Add(object obj)
			{
				int result = this._list.Add(obj);
				this._version++;
				return result;
			}

			// Token: 0x06006BA0 RID: 27552 RVA: 0x00173A61 File Offset: 0x00171C61
			public override void AddRange(ICollection c)
			{
				this.InsertRange(this.Count, c);
			}

			// Token: 0x06006BA1 RID: 27553 RVA: 0x00173A70 File Offset: 0x00171C70
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (this.Count - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				if (comparer == null)
				{
					comparer = Comparer.Default;
				}
				int i = index;
				int num = index + count - 1;
				while (i <= num)
				{
					int num2 = (i + num) / 2;
					int num3 = comparer.Compare(value, this._list[num2]);
					if (num3 == 0)
					{
						return num2;
					}
					if (num3 < 0)
					{
						num = num2 - 1;
					}
					else
					{
						i = num2 + 1;
					}
				}
				return ~i;
			}

			// Token: 0x06006BA2 RID: 27554 RVA: 0x00173B09 File Offset: 0x00171D09
			public override void Clear()
			{
				if (this._list.IsFixedSize)
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
				}
				this._list.Clear();
				this._version++;
			}

			// Token: 0x06006BA3 RID: 27555 RVA: 0x00173B41 File Offset: 0x00171D41
			public override object Clone()
			{
				return new ArrayList.IListWrapper(this._list);
			}

			// Token: 0x06006BA4 RID: 27556 RVA: 0x00173B4E File Offset: 0x00171D4E
			public override bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x06006BA5 RID: 27557 RVA: 0x00173B5C File Offset: 0x00171D5C
			public override void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x06006BA6 RID: 27558 RVA: 0x00173B6C File Offset: 0x00171D6C
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (index < 0 || arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "arrayIndex", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (array.Length - arrayIndex < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				for (int i = index; i < index + count; i++)
				{
					array.SetValue(this._list[i], arrayIndex++);
				}
			}

			// Token: 0x06006BA7 RID: 27559 RVA: 0x00173C46 File Offset: 0x00171E46
			public override IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x06006BA8 RID: 27560 RVA: 0x00173C54 File Offset: 0x00171E54
			public override IEnumerator GetEnumerator(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				return new ArrayList.IListWrapper.IListWrapperEnumWrapper(this, index, count);
			}

			// Token: 0x06006BA9 RID: 27561 RVA: 0x00173CB1 File Offset: 0x00171EB1
			public override int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x06006BAA RID: 27562 RVA: 0x00173CBF File Offset: 0x00171EBF
			public override int IndexOf(object value, int startIndex)
			{
				return this.IndexOf(value, startIndex, this._list.Count - startIndex);
			}

			// Token: 0x06006BAB RID: 27563 RVA: 0x00173CD8 File Offset: 0x00171ED8
			public override int IndexOf(object value, int startIndex, int count)
			{
				if (startIndex < 0 || startIndex > this.Count)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				if (count < 0 || startIndex > this.Count - count)
				{
					throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
				}
				int num = startIndex + count;
				if (value == null)
				{
					for (int i = startIndex; i < num; i++)
					{
						if (this._list[i] == null)
						{
							return i;
						}
					}
					return -1;
				}
				for (int j = startIndex; j < num; j++)
				{
					if (this._list[j] != null && this._list[j].Equals(value))
					{
						return j;
					}
				}
				return -1;
			}

			// Token: 0x06006BAC RID: 27564 RVA: 0x00173D81 File Offset: 0x00171F81
			public override void Insert(int index, object obj)
			{
				this._list.Insert(index, obj);
				this._version++;
			}

			// Token: 0x06006BAD RID: 27565 RVA: 0x00173DA0 File Offset: 0x00171FA0
			public override void InsertRange(int index, ICollection c)
			{
				if (c == null)
				{
					throw new ArgumentNullException("c", Environment.GetResourceString("ArgumentNull_Collection"));
				}
				if (index < 0 || index > this.Count)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				if (c.Count > 0)
				{
					ArrayList arrayList = this._list as ArrayList;
					if (arrayList != null)
					{
						arrayList.InsertRange(index, c);
					}
					else
					{
						foreach (object value in c)
						{
							this._list.Insert(index++, value);
						}
					}
					this._version++;
				}
			}

			// Token: 0x06006BAE RID: 27566 RVA: 0x00173E3F File Offset: 0x0017203F
			public override int LastIndexOf(object value)
			{
				return this.LastIndexOf(value, this._list.Count - 1, this._list.Count);
			}

			// Token: 0x06006BAF RID: 27567 RVA: 0x00173E60 File Offset: 0x00172060
			public override int LastIndexOf(object value, int startIndex)
			{
				return this.LastIndexOf(value, startIndex, startIndex + 1);
			}

			// Token: 0x06006BB0 RID: 27568 RVA: 0x00173E70 File Offset: 0x00172070
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				if (this._list.Count == 0)
				{
					return -1;
				}
				if (startIndex < 0 || startIndex >= this._list.Count)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				if (count < 0 || count > startIndex + 1)
				{
					throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
				}
				int num = startIndex - count + 1;
				if (value == null)
				{
					for (int i = startIndex; i >= num; i--)
					{
						if (this._list[i] == null)
						{
							return i;
						}
					}
					return -1;
				}
				for (int j = startIndex; j >= num; j--)
				{
					if (this._list[j] != null && this._list[j].Equals(value))
					{
						return j;
					}
				}
				return -1;
			}

			// Token: 0x06006BB1 RID: 27569 RVA: 0x00173F2C File Offset: 0x0017212C
			public override void Remove(object value)
			{
				int num = this.IndexOf(value);
				if (num >= 0)
				{
					this.RemoveAt(num);
				}
			}

			// Token: 0x06006BB2 RID: 27570 RVA: 0x00173F4C File Offset: 0x0017214C
			public override void RemoveAt(int index)
			{
				this._list.RemoveAt(index);
				this._version++;
			}

			// Token: 0x06006BB3 RID: 27571 RVA: 0x00173F68 File Offset: 0x00172168
			public override void RemoveRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				if (count > 0)
				{
					this._version++;
				}
				while (count > 0)
				{
					this._list.RemoveAt(index);
					count--;
				}
			}

			// Token: 0x06006BB4 RID: 27572 RVA: 0x00173FE8 File Offset: 0x001721E8
			public override void Reverse(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				int i = index;
				int num = index + count - 1;
				while (i < num)
				{
					object value = this._list[i];
					this._list[i++] = this._list[num];
					this._list[num--] = value;
				}
				this._version++;
			}

			// Token: 0x06006BB5 RID: 27573 RVA: 0x00174094 File Offset: 0x00172294
			public override void SetRange(int index, ICollection c)
			{
				if (c == null)
				{
					throw new ArgumentNullException("c", Environment.GetResourceString("ArgumentNull_Collection"));
				}
				if (index < 0 || index > this._list.Count - c.Count)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				if (c.Count > 0)
				{
					foreach (object value in c)
					{
						this._list[index++] = value;
					}
					this._version++;
				}
			}

			// Token: 0x06006BB6 RID: 27574 RVA: 0x00174128 File Offset: 0x00172328
			public override ArrayList GetRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				return new ArrayList.Range(this, index, count);
			}

			// Token: 0x06006BB7 RID: 27575 RVA: 0x00174188 File Offset: 0x00172388
			public override void Sort(int index, int count, IComparer comparer)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				object[] array = new object[count];
				this.CopyTo(index, array, 0, count);
				Array.Sort(array, 0, count, comparer);
				for (int i = 0; i < count; i++)
				{
					this._list[i + index] = array[i];
				}
				this._version++;
			}

			// Token: 0x06006BB8 RID: 27576 RVA: 0x00174224 File Offset: 0x00172424
			public override object[] ToArray()
			{
				object[] array = new object[this.Count];
				this._list.CopyTo(array, 0);
				return array;
			}

			// Token: 0x06006BB9 RID: 27577 RVA: 0x0017424C File Offset: 0x0017244C
			[SecuritySafeCritical]
			public override Array ToArray(Type type)
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				Array array = Array.UnsafeCreateInstance(type, this._list.Count);
				this._list.CopyTo(array, 0);
				return array;
			}

			// Token: 0x06006BBA RID: 27578 RVA: 0x0017428D File Offset: 0x0017248D
			public override void TrimToSize()
			{
			}

			// Token: 0x04003455 RID: 13397
			private IList _list;

			// Token: 0x02000CCC RID: 3276
			[Serializable]
			private sealed class IListWrapperEnumWrapper : IEnumerator, ICloneable
			{
				// Token: 0x060070C3 RID: 28867 RVA: 0x00183CFF File Offset: 0x00181EFF
				private IListWrapperEnumWrapper()
				{
				}

				// Token: 0x060070C4 RID: 28868 RVA: 0x00183D08 File Offset: 0x00181F08
				internal IListWrapperEnumWrapper(ArrayList.IListWrapper listWrapper, int startIndex, int count)
				{
					this._en = listWrapper.GetEnumerator();
					this._initialStartIndex = startIndex;
					this._initialCount = count;
					while (startIndex-- > 0 && this._en.MoveNext())
					{
					}
					this._remaining = count;
					this._firstCall = true;
				}

				// Token: 0x060070C5 RID: 28869 RVA: 0x00183D5C File Offset: 0x00181F5C
				public object Clone()
				{
					return new ArrayList.IListWrapper.IListWrapperEnumWrapper
					{
						_en = (IEnumerator)((ICloneable)this._en).Clone(),
						_initialStartIndex = this._initialStartIndex,
						_initialCount = this._initialCount,
						_remaining = this._remaining,
						_firstCall = this._firstCall
					};
				}

				// Token: 0x060070C6 RID: 28870 RVA: 0x00183DBC File Offset: 0x00181FBC
				public bool MoveNext()
				{
					if (this._firstCall)
					{
						this._firstCall = false;
						int remaining = this._remaining;
						this._remaining = remaining - 1;
						return remaining > 0 && this._en.MoveNext();
					}
					if (this._remaining < 0)
					{
						return false;
					}
					bool flag = this._en.MoveNext();
					if (flag)
					{
						int remaining = this._remaining;
						this._remaining = remaining - 1;
						return remaining > 0;
					}
					return false;
				}

				// Token: 0x17001369 RID: 4969
				// (get) Token: 0x060070C7 RID: 28871 RVA: 0x00183E2A File Offset: 0x0018202A
				public object Current
				{
					get
					{
						if (this._firstCall)
						{
							throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
						}
						if (this._remaining < 0)
						{
							throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
						}
						return this._en.Current;
					}
				}

				// Token: 0x060070C8 RID: 28872 RVA: 0x00183E68 File Offset: 0x00182068
				public void Reset()
				{
					this._en.Reset();
					int initialStartIndex = this._initialStartIndex;
					while (initialStartIndex-- > 0 && this._en.MoveNext())
					{
					}
					this._remaining = this._initialCount;
					this._firstCall = true;
				}

				// Token: 0x04003851 RID: 14417
				private IEnumerator _en;

				// Token: 0x04003852 RID: 14418
				private int _remaining;

				// Token: 0x04003853 RID: 14419
				private int _initialStartIndex;

				// Token: 0x04003854 RID: 14420
				private int _initialCount;

				// Token: 0x04003855 RID: 14421
				private bool _firstCall;
			}
		}

		// Token: 0x02000B6E RID: 2926
		[Serializable]
		private class SyncArrayList : ArrayList
		{
			// Token: 0x06006BBB RID: 27579 RVA: 0x0017428F File Offset: 0x0017248F
			internal SyncArrayList(ArrayList list) : base(false)
			{
				this._list = list;
				this._root = list.SyncRoot;
			}

			// Token: 0x17001253 RID: 4691
			// (get) Token: 0x06006BBC RID: 27580 RVA: 0x001742AC File Offset: 0x001724AC
			// (set) Token: 0x06006BBD RID: 27581 RVA: 0x001742F4 File Offset: 0x001724F4
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
				set
				{
					object root = this._root;
					lock (root)
					{
						this._list.Capacity = value;
					}
				}
			}

			// Token: 0x17001254 RID: 4692
			// (get) Token: 0x06006BBE RID: 27582 RVA: 0x0017433C File Offset: 0x0017253C
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

			// Token: 0x17001255 RID: 4693
			// (get) Token: 0x06006BBF RID: 27583 RVA: 0x00174384 File Offset: 0x00172584
			public override bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x17001256 RID: 4694
			// (get) Token: 0x06006BC0 RID: 27584 RVA: 0x00174391 File Offset: 0x00172591
			public override bool IsFixedSize
			{
				get
				{
					return this._list.IsFixedSize;
				}
			}

			// Token: 0x17001257 RID: 4695
			// (get) Token: 0x06006BC1 RID: 27585 RVA: 0x0017439E File Offset: 0x0017259E
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001258 RID: 4696
			public override object this[int index]
			{
				get
				{
					object root = this._root;
					object result;
					lock (root)
					{
						result = this._list[index];
					}
					return result;
				}
				set
				{
					object root = this._root;
					lock (root)
					{
						this._list[index] = value;
					}
				}
			}

			// Token: 0x17001259 RID: 4697
			// (get) Token: 0x06006BC4 RID: 27588 RVA: 0x00174434 File Offset: 0x00172634
			public override object SyncRoot
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x06006BC5 RID: 27589 RVA: 0x0017443C File Offset: 0x0017263C
			public override int Add(object value)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.Add(value);
				}
				return result;
			}

			// Token: 0x06006BC6 RID: 27590 RVA: 0x00174484 File Offset: 0x00172684
			public override void AddRange(ICollection c)
			{
				object root = this._root;
				lock (root)
				{
					this._list.AddRange(c);
				}
			}

			// Token: 0x06006BC7 RID: 27591 RVA: 0x001744CC File Offset: 0x001726CC
			public override int BinarySearch(object value)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.BinarySearch(value);
				}
				return result;
			}

			// Token: 0x06006BC8 RID: 27592 RVA: 0x00174514 File Offset: 0x00172714
			public override int BinarySearch(object value, IComparer comparer)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.BinarySearch(value, comparer);
				}
				return result;
			}

			// Token: 0x06006BC9 RID: 27593 RVA: 0x00174560 File Offset: 0x00172760
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.BinarySearch(index, count, value, comparer);
				}
				return result;
			}

			// Token: 0x06006BCA RID: 27594 RVA: 0x001745AC File Offset: 0x001727AC
			public override void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._list.Clear();
				}
			}

			// Token: 0x06006BCB RID: 27595 RVA: 0x001745F4 File Offset: 0x001727F4
			public override object Clone()
			{
				object root = this._root;
				object result;
				lock (root)
				{
					result = new ArrayList.SyncArrayList((ArrayList)this._list.Clone());
				}
				return result;
			}

			// Token: 0x06006BCC RID: 27596 RVA: 0x00174648 File Offset: 0x00172848
			public override bool Contains(object item)
			{
				object root = this._root;
				bool result;
				lock (root)
				{
					result = this._list.Contains(item);
				}
				return result;
			}

			// Token: 0x06006BCD RID: 27597 RVA: 0x00174690 File Offset: 0x00172890
			public override void CopyTo(Array array)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(array);
				}
			}

			// Token: 0x06006BCE RID: 27598 RVA: 0x001746D8 File Offset: 0x001728D8
			public override void CopyTo(Array array, int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(array, index);
				}
			}

			// Token: 0x06006BCF RID: 27599 RVA: 0x00174720 File Offset: 0x00172920
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(index, array, arrayIndex, count);
				}
			}

			// Token: 0x06006BD0 RID: 27600 RVA: 0x0017476C File Offset: 0x0017296C
			public override IEnumerator GetEnumerator()
			{
				object root = this._root;
				IEnumerator enumerator;
				lock (root)
				{
					enumerator = this._list.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06006BD1 RID: 27601 RVA: 0x001747B4 File Offset: 0x001729B4
			public override IEnumerator GetEnumerator(int index, int count)
			{
				object root = this._root;
				IEnumerator enumerator;
				lock (root)
				{
					enumerator = this._list.GetEnumerator(index, count);
				}
				return enumerator;
			}

			// Token: 0x06006BD2 RID: 27602 RVA: 0x00174800 File Offset: 0x00172A00
			public override int IndexOf(object value)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.IndexOf(value);
				}
				return result;
			}

			// Token: 0x06006BD3 RID: 27603 RVA: 0x00174848 File Offset: 0x00172A48
			public override int IndexOf(object value, int startIndex)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.IndexOf(value, startIndex);
				}
				return result;
			}

			// Token: 0x06006BD4 RID: 27604 RVA: 0x00174894 File Offset: 0x00172A94
			public override int IndexOf(object value, int startIndex, int count)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.IndexOf(value, startIndex, count);
				}
				return result;
			}

			// Token: 0x06006BD5 RID: 27605 RVA: 0x001748E0 File Offset: 0x00172AE0
			public override void Insert(int index, object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Insert(index, value);
				}
			}

			// Token: 0x06006BD6 RID: 27606 RVA: 0x00174928 File Offset: 0x00172B28
			public override void InsertRange(int index, ICollection c)
			{
				object root = this._root;
				lock (root)
				{
					this._list.InsertRange(index, c);
				}
			}

			// Token: 0x06006BD7 RID: 27607 RVA: 0x00174970 File Offset: 0x00172B70
			public override int LastIndexOf(object value)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.LastIndexOf(value);
				}
				return result;
			}

			// Token: 0x06006BD8 RID: 27608 RVA: 0x001749B8 File Offset: 0x00172BB8
			public override int LastIndexOf(object value, int startIndex)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.LastIndexOf(value, startIndex);
				}
				return result;
			}

			// Token: 0x06006BD9 RID: 27609 RVA: 0x00174A04 File Offset: 0x00172C04
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.LastIndexOf(value, startIndex, count);
				}
				return result;
			}

			// Token: 0x06006BDA RID: 27610 RVA: 0x00174A50 File Offset: 0x00172C50
			public override void Remove(object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Remove(value);
				}
			}

			// Token: 0x06006BDB RID: 27611 RVA: 0x00174A98 File Offset: 0x00172C98
			public override void RemoveAt(int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.RemoveAt(index);
				}
			}

			// Token: 0x06006BDC RID: 27612 RVA: 0x00174AE0 File Offset: 0x00172CE0
			public override void RemoveRange(int index, int count)
			{
				object root = this._root;
				lock (root)
				{
					this._list.RemoveRange(index, count);
				}
			}

			// Token: 0x06006BDD RID: 27613 RVA: 0x00174B28 File Offset: 0x00172D28
			public override void Reverse(int index, int count)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Reverse(index, count);
				}
			}

			// Token: 0x06006BDE RID: 27614 RVA: 0x00174B70 File Offset: 0x00172D70
			public override void SetRange(int index, ICollection c)
			{
				object root = this._root;
				lock (root)
				{
					this._list.SetRange(index, c);
				}
			}

			// Token: 0x06006BDF RID: 27615 RVA: 0x00174BB8 File Offset: 0x00172DB8
			public override ArrayList GetRange(int index, int count)
			{
				object root = this._root;
				ArrayList range;
				lock (root)
				{
					range = this._list.GetRange(index, count);
				}
				return range;
			}

			// Token: 0x06006BE0 RID: 27616 RVA: 0x00174C04 File Offset: 0x00172E04
			public override void Sort()
			{
				object root = this._root;
				lock (root)
				{
					this._list.Sort();
				}
			}

			// Token: 0x06006BE1 RID: 27617 RVA: 0x00174C4C File Offset: 0x00172E4C
			public override void Sort(IComparer comparer)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Sort(comparer);
				}
			}

			// Token: 0x06006BE2 RID: 27618 RVA: 0x00174C94 File Offset: 0x00172E94
			public override void Sort(int index, int count, IComparer comparer)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Sort(index, count, comparer);
				}
			}

			// Token: 0x06006BE3 RID: 27619 RVA: 0x00174CDC File Offset: 0x00172EDC
			public override object[] ToArray()
			{
				object root = this._root;
				object[] result;
				lock (root)
				{
					result = this._list.ToArray();
				}
				return result;
			}

			// Token: 0x06006BE4 RID: 27620 RVA: 0x00174D24 File Offset: 0x00172F24
			public override Array ToArray(Type type)
			{
				object root = this._root;
				Array result;
				lock (root)
				{
					result = this._list.ToArray(type);
				}
				return result;
			}

			// Token: 0x06006BE5 RID: 27621 RVA: 0x00174D6C File Offset: 0x00172F6C
			public override void TrimToSize()
			{
				object root = this._root;
				lock (root)
				{
					this._list.TrimToSize();
				}
			}

			// Token: 0x04003456 RID: 13398
			private ArrayList _list;

			// Token: 0x04003457 RID: 13399
			private object _root;
		}

		// Token: 0x02000B6F RID: 2927
		[Serializable]
		private class SyncIList : IList, ICollection, IEnumerable
		{
			// Token: 0x06006BE6 RID: 27622 RVA: 0x00174DB4 File Offset: 0x00172FB4
			internal SyncIList(IList list)
			{
				this._list = list;
				this._root = list.SyncRoot;
			}

			// Token: 0x1700125A RID: 4698
			// (get) Token: 0x06006BE7 RID: 27623 RVA: 0x00174DD0 File Offset: 0x00172FD0
			public virtual int Count
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

			// Token: 0x1700125B RID: 4699
			// (get) Token: 0x06006BE8 RID: 27624 RVA: 0x00174E18 File Offset: 0x00173018
			public virtual bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x1700125C RID: 4700
			// (get) Token: 0x06006BE9 RID: 27625 RVA: 0x00174E25 File Offset: 0x00173025
			public virtual bool IsFixedSize
			{
				get
				{
					return this._list.IsFixedSize;
				}
			}

			// Token: 0x1700125D RID: 4701
			// (get) Token: 0x06006BEA RID: 27626 RVA: 0x00174E32 File Offset: 0x00173032
			public virtual bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700125E RID: 4702
			public virtual object this[int index]
			{
				get
				{
					object root = this._root;
					object result;
					lock (root)
					{
						result = this._list[index];
					}
					return result;
				}
				set
				{
					object root = this._root;
					lock (root)
					{
						this._list[index] = value;
					}
				}
			}

			// Token: 0x1700125F RID: 4703
			// (get) Token: 0x06006BED RID: 27629 RVA: 0x00174EC8 File Offset: 0x001730C8
			public virtual object SyncRoot
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x06006BEE RID: 27630 RVA: 0x00174ED0 File Offset: 0x001730D0
			public virtual int Add(object value)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.Add(value);
				}
				return result;
			}

			// Token: 0x06006BEF RID: 27631 RVA: 0x00174F18 File Offset: 0x00173118
			public virtual void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._list.Clear();
				}
			}

			// Token: 0x06006BF0 RID: 27632 RVA: 0x00174F60 File Offset: 0x00173160
			public virtual bool Contains(object item)
			{
				object root = this._root;
				bool result;
				lock (root)
				{
					result = this._list.Contains(item);
				}
				return result;
			}

			// Token: 0x06006BF1 RID: 27633 RVA: 0x00174FA8 File Offset: 0x001731A8
			public virtual void CopyTo(Array array, int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(array, index);
				}
			}

			// Token: 0x06006BF2 RID: 27634 RVA: 0x00174FF0 File Offset: 0x001731F0
			public virtual IEnumerator GetEnumerator()
			{
				object root = this._root;
				IEnumerator enumerator;
				lock (root)
				{
					enumerator = this._list.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06006BF3 RID: 27635 RVA: 0x00175038 File Offset: 0x00173238
			public virtual int IndexOf(object value)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.IndexOf(value);
				}
				return result;
			}

			// Token: 0x06006BF4 RID: 27636 RVA: 0x00175080 File Offset: 0x00173280
			public virtual void Insert(int index, object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Insert(index, value);
				}
			}

			// Token: 0x06006BF5 RID: 27637 RVA: 0x001750C8 File Offset: 0x001732C8
			public virtual void Remove(object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Remove(value);
				}
			}

			// Token: 0x06006BF6 RID: 27638 RVA: 0x00175110 File Offset: 0x00173310
			public virtual void RemoveAt(int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.RemoveAt(index);
				}
			}

			// Token: 0x04003458 RID: 13400
			private IList _list;

			// Token: 0x04003459 RID: 13401
			private object _root;
		}

		// Token: 0x02000B70 RID: 2928
		[Serializable]
		private class FixedSizeList : IList, ICollection, IEnumerable
		{
			// Token: 0x06006BF7 RID: 27639 RVA: 0x00175158 File Offset: 0x00173358
			internal FixedSizeList(IList l)
			{
				this._list = l;
			}

			// Token: 0x17001260 RID: 4704
			// (get) Token: 0x06006BF8 RID: 27640 RVA: 0x00175167 File Offset: 0x00173367
			public virtual int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x17001261 RID: 4705
			// (get) Token: 0x06006BF9 RID: 27641 RVA: 0x00175174 File Offset: 0x00173374
			public virtual bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x17001262 RID: 4706
			// (get) Token: 0x06006BFA RID: 27642 RVA: 0x00175181 File Offset: 0x00173381
			public virtual bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001263 RID: 4707
			// (get) Token: 0x06006BFB RID: 27643 RVA: 0x00175184 File Offset: 0x00173384
			public virtual bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x17001264 RID: 4708
			public virtual object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					this._list[index] = value;
				}
			}

			// Token: 0x17001265 RID: 4709
			// (get) Token: 0x06006BFE RID: 27646 RVA: 0x001751AE File Offset: 0x001733AE
			public virtual object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x06006BFF RID: 27647 RVA: 0x001751BB File Offset: 0x001733BB
			public virtual int Add(object obj)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
			}

			// Token: 0x06006C00 RID: 27648 RVA: 0x001751CC File Offset: 0x001733CC
			public virtual void Clear()
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
			}

			// Token: 0x06006C01 RID: 27649 RVA: 0x001751DD File Offset: 0x001733DD
			public virtual bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x06006C02 RID: 27650 RVA: 0x001751EB File Offset: 0x001733EB
			public virtual void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x06006C03 RID: 27651 RVA: 0x001751FA File Offset: 0x001733FA
			public virtual IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x06006C04 RID: 27652 RVA: 0x00175207 File Offset: 0x00173407
			public virtual int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x06006C05 RID: 27653 RVA: 0x00175215 File Offset: 0x00173415
			public virtual void Insert(int index, object obj)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
			}

			// Token: 0x06006C06 RID: 27654 RVA: 0x00175226 File Offset: 0x00173426
			public virtual void Remove(object value)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
			}

			// Token: 0x06006C07 RID: 27655 RVA: 0x00175237 File Offset: 0x00173437
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
			}

			// Token: 0x0400345A RID: 13402
			private IList _list;
		}

		// Token: 0x02000B71 RID: 2929
		[Serializable]
		private class FixedSizeArrayList : ArrayList
		{
			// Token: 0x06006C08 RID: 27656 RVA: 0x00175248 File Offset: 0x00173448
			internal FixedSizeArrayList(ArrayList l)
			{
				this._list = l;
				this._version = this._list._version;
			}

			// Token: 0x17001266 RID: 4710
			// (get) Token: 0x06006C09 RID: 27657 RVA: 0x00175268 File Offset: 0x00173468
			public override int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x17001267 RID: 4711
			// (get) Token: 0x06006C0A RID: 27658 RVA: 0x00175275 File Offset: 0x00173475
			public override bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x17001268 RID: 4712
			// (get) Token: 0x06006C0B RID: 27659 RVA: 0x00175282 File Offset: 0x00173482
			public override bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001269 RID: 4713
			// (get) Token: 0x06006C0C RID: 27660 RVA: 0x00175285 File Offset: 0x00173485
			public override bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x1700126A RID: 4714
			public override object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					this._list[index] = value;
					this._version = this._list._version;
				}
			}

			// Token: 0x1700126B RID: 4715
			// (get) Token: 0x06006C0F RID: 27663 RVA: 0x001752C0 File Offset: 0x001734C0
			public override object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x06006C10 RID: 27664 RVA: 0x001752CD File Offset: 0x001734CD
			public override int Add(object obj)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
			}

			// Token: 0x06006C11 RID: 27665 RVA: 0x001752DE File Offset: 0x001734DE
			public override void AddRange(ICollection c)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
			}

			// Token: 0x06006C12 RID: 27666 RVA: 0x001752EF File Offset: 0x001734EF
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				return this._list.BinarySearch(index, count, value, comparer);
			}

			// Token: 0x1700126C RID: 4716
			// (get) Token: 0x06006C13 RID: 27667 RVA: 0x00175301 File Offset: 0x00173501
			// (set) Token: 0x06006C14 RID: 27668 RVA: 0x0017530E File Offset: 0x0017350E
			public override int Capacity
			{
				get
				{
					return this._list.Capacity;
				}
				set
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
				}
			}

			// Token: 0x06006C15 RID: 27669 RVA: 0x0017531F File Offset: 0x0017351F
			public override void Clear()
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
			}

			// Token: 0x06006C16 RID: 27670 RVA: 0x00175330 File Offset: 0x00173530
			public override object Clone()
			{
				return new ArrayList.FixedSizeArrayList(this._list)
				{
					_list = (ArrayList)this._list.Clone()
				};
			}

			// Token: 0x06006C17 RID: 27671 RVA: 0x00175360 File Offset: 0x00173560
			public override bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x06006C18 RID: 27672 RVA: 0x0017536E File Offset: 0x0017356E
			public override void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x06006C19 RID: 27673 RVA: 0x0017537D File Offset: 0x0017357D
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				this._list.CopyTo(index, array, arrayIndex, count);
			}

			// Token: 0x06006C1A RID: 27674 RVA: 0x0017538F File Offset: 0x0017358F
			public override IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x06006C1B RID: 27675 RVA: 0x0017539C File Offset: 0x0017359C
			public override IEnumerator GetEnumerator(int index, int count)
			{
				return this._list.GetEnumerator(index, count);
			}

			// Token: 0x06006C1C RID: 27676 RVA: 0x001753AB File Offset: 0x001735AB
			public override int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x06006C1D RID: 27677 RVA: 0x001753B9 File Offset: 0x001735B9
			public override int IndexOf(object value, int startIndex)
			{
				return this._list.IndexOf(value, startIndex);
			}

			// Token: 0x06006C1E RID: 27678 RVA: 0x001753C8 File Offset: 0x001735C8
			public override int IndexOf(object value, int startIndex, int count)
			{
				return this._list.IndexOf(value, startIndex, count);
			}

			// Token: 0x06006C1F RID: 27679 RVA: 0x001753D8 File Offset: 0x001735D8
			public override void Insert(int index, object obj)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
			}

			// Token: 0x06006C20 RID: 27680 RVA: 0x001753E9 File Offset: 0x001735E9
			public override void InsertRange(int index, ICollection c)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
			}

			// Token: 0x06006C21 RID: 27681 RVA: 0x001753FA File Offset: 0x001735FA
			public override int LastIndexOf(object value)
			{
				return this._list.LastIndexOf(value);
			}

			// Token: 0x06006C22 RID: 27682 RVA: 0x00175408 File Offset: 0x00173608
			public override int LastIndexOf(object value, int startIndex)
			{
				return this._list.LastIndexOf(value, startIndex);
			}

			// Token: 0x06006C23 RID: 27683 RVA: 0x00175417 File Offset: 0x00173617
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				return this._list.LastIndexOf(value, startIndex, count);
			}

			// Token: 0x06006C24 RID: 27684 RVA: 0x00175427 File Offset: 0x00173627
			public override void Remove(object value)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
			}

			// Token: 0x06006C25 RID: 27685 RVA: 0x00175438 File Offset: 0x00173638
			public override void RemoveAt(int index)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
			}

			// Token: 0x06006C26 RID: 27686 RVA: 0x00175449 File Offset: 0x00173649
			public override void RemoveRange(int index, int count)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
			}

			// Token: 0x06006C27 RID: 27687 RVA: 0x0017545A File Offset: 0x0017365A
			public override void SetRange(int index, ICollection c)
			{
				this._list.SetRange(index, c);
				this._version = this._list._version;
			}

			// Token: 0x06006C28 RID: 27688 RVA: 0x0017547C File Offset: 0x0017367C
			public override ArrayList GetRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (this.Count - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				return new ArrayList.Range(this, index, count);
			}

			// Token: 0x06006C29 RID: 27689 RVA: 0x001754D4 File Offset: 0x001736D4
			public override void Reverse(int index, int count)
			{
				this._list.Reverse(index, count);
				this._version = this._list._version;
			}

			// Token: 0x06006C2A RID: 27690 RVA: 0x001754F4 File Offset: 0x001736F4
			public override void Sort(int index, int count, IComparer comparer)
			{
				this._list.Sort(index, count, comparer);
				this._version = this._list._version;
			}

			// Token: 0x06006C2B RID: 27691 RVA: 0x00175515 File Offset: 0x00173715
			public override object[] ToArray()
			{
				return this._list.ToArray();
			}

			// Token: 0x06006C2C RID: 27692 RVA: 0x00175522 File Offset: 0x00173722
			public override Array ToArray(Type type)
			{
				return this._list.ToArray(type);
			}

			// Token: 0x06006C2D RID: 27693 RVA: 0x00175530 File Offset: 0x00173730
			public override void TrimToSize()
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
			}

			// Token: 0x0400345B RID: 13403
			private ArrayList _list;
		}

		// Token: 0x02000B72 RID: 2930
		[Serializable]
		private class ReadOnlyList : IList, ICollection, IEnumerable
		{
			// Token: 0x06006C2E RID: 27694 RVA: 0x00175541 File Offset: 0x00173741
			internal ReadOnlyList(IList l)
			{
				this._list = l;
			}

			// Token: 0x1700126D RID: 4717
			// (get) Token: 0x06006C2F RID: 27695 RVA: 0x00175550 File Offset: 0x00173750
			public virtual int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x1700126E RID: 4718
			// (get) Token: 0x06006C30 RID: 27696 RVA: 0x0017555D File Offset: 0x0017375D
			public virtual bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700126F RID: 4719
			// (get) Token: 0x06006C31 RID: 27697 RVA: 0x00175560 File Offset: 0x00173760
			public virtual bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001270 RID: 4720
			// (get) Token: 0x06006C32 RID: 27698 RVA: 0x00175563 File Offset: 0x00173763
			public virtual bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x17001271 RID: 4721
			public virtual object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
				}
			}

			// Token: 0x17001272 RID: 4722
			// (get) Token: 0x06006C35 RID: 27701 RVA: 0x0017558F File Offset: 0x0017378F
			public virtual object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x06006C36 RID: 27702 RVA: 0x0017559C File Offset: 0x0017379C
			public virtual int Add(object obj)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x06006C37 RID: 27703 RVA: 0x001755AD File Offset: 0x001737AD
			public virtual void Clear()
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x06006C38 RID: 27704 RVA: 0x001755BE File Offset: 0x001737BE
			public virtual bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x06006C39 RID: 27705 RVA: 0x001755CC File Offset: 0x001737CC
			public virtual void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x06006C3A RID: 27706 RVA: 0x001755DB File Offset: 0x001737DB
			public virtual IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x06006C3B RID: 27707 RVA: 0x001755E8 File Offset: 0x001737E8
			public virtual int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x06006C3C RID: 27708 RVA: 0x001755F6 File Offset: 0x001737F6
			public virtual void Insert(int index, object obj)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x06006C3D RID: 27709 RVA: 0x00175607 File Offset: 0x00173807
			public virtual void Remove(object value)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x06006C3E RID: 27710 RVA: 0x00175618 File Offset: 0x00173818
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x0400345C RID: 13404
			private IList _list;
		}

		// Token: 0x02000B73 RID: 2931
		[Serializable]
		private class ReadOnlyArrayList : ArrayList
		{
			// Token: 0x06006C3F RID: 27711 RVA: 0x00175629 File Offset: 0x00173829
			internal ReadOnlyArrayList(ArrayList l)
			{
				this._list = l;
			}

			// Token: 0x17001273 RID: 4723
			// (get) Token: 0x06006C40 RID: 27712 RVA: 0x00175638 File Offset: 0x00173838
			public override int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x17001274 RID: 4724
			// (get) Token: 0x06006C41 RID: 27713 RVA: 0x00175645 File Offset: 0x00173845
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001275 RID: 4725
			// (get) Token: 0x06006C42 RID: 27714 RVA: 0x00175648 File Offset: 0x00173848
			public override bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001276 RID: 4726
			// (get) Token: 0x06006C43 RID: 27715 RVA: 0x0017564B File Offset: 0x0017384B
			public override bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x17001277 RID: 4727
			public override object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
				}
			}

			// Token: 0x17001278 RID: 4728
			// (get) Token: 0x06006C46 RID: 27718 RVA: 0x00175677 File Offset: 0x00173877
			public override object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x06006C47 RID: 27719 RVA: 0x00175684 File Offset: 0x00173884
			public override int Add(object obj)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x06006C48 RID: 27720 RVA: 0x00175695 File Offset: 0x00173895
			public override void AddRange(ICollection c)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x06006C49 RID: 27721 RVA: 0x001756A6 File Offset: 0x001738A6
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				return this._list.BinarySearch(index, count, value, comparer);
			}

			// Token: 0x17001279 RID: 4729
			// (get) Token: 0x06006C4A RID: 27722 RVA: 0x001756B8 File Offset: 0x001738B8
			// (set) Token: 0x06006C4B RID: 27723 RVA: 0x001756C5 File Offset: 0x001738C5
			public override int Capacity
			{
				get
				{
					return this._list.Capacity;
				}
				set
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
				}
			}

			// Token: 0x06006C4C RID: 27724 RVA: 0x001756D6 File Offset: 0x001738D6
			public override void Clear()
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x06006C4D RID: 27725 RVA: 0x001756E8 File Offset: 0x001738E8
			public override object Clone()
			{
				return new ArrayList.ReadOnlyArrayList(this._list)
				{
					_list = (ArrayList)this._list.Clone()
				};
			}

			// Token: 0x06006C4E RID: 27726 RVA: 0x00175718 File Offset: 0x00173918
			public override bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x06006C4F RID: 27727 RVA: 0x00175726 File Offset: 0x00173926
			public override void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x06006C50 RID: 27728 RVA: 0x00175735 File Offset: 0x00173935
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				this._list.CopyTo(index, array, arrayIndex, count);
			}

			// Token: 0x06006C51 RID: 27729 RVA: 0x00175747 File Offset: 0x00173947
			public override IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x06006C52 RID: 27730 RVA: 0x00175754 File Offset: 0x00173954
			public override IEnumerator GetEnumerator(int index, int count)
			{
				return this._list.GetEnumerator(index, count);
			}

			// Token: 0x06006C53 RID: 27731 RVA: 0x00175763 File Offset: 0x00173963
			public override int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x06006C54 RID: 27732 RVA: 0x00175771 File Offset: 0x00173971
			public override int IndexOf(object value, int startIndex)
			{
				return this._list.IndexOf(value, startIndex);
			}

			// Token: 0x06006C55 RID: 27733 RVA: 0x00175780 File Offset: 0x00173980
			public override int IndexOf(object value, int startIndex, int count)
			{
				return this._list.IndexOf(value, startIndex, count);
			}

			// Token: 0x06006C56 RID: 27734 RVA: 0x00175790 File Offset: 0x00173990
			public override void Insert(int index, object obj)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x06006C57 RID: 27735 RVA: 0x001757A1 File Offset: 0x001739A1
			public override void InsertRange(int index, ICollection c)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x06006C58 RID: 27736 RVA: 0x001757B2 File Offset: 0x001739B2
			public override int LastIndexOf(object value)
			{
				return this._list.LastIndexOf(value);
			}

			// Token: 0x06006C59 RID: 27737 RVA: 0x001757C0 File Offset: 0x001739C0
			public override int LastIndexOf(object value, int startIndex)
			{
				return this._list.LastIndexOf(value, startIndex);
			}

			// Token: 0x06006C5A RID: 27738 RVA: 0x001757CF File Offset: 0x001739CF
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				return this._list.LastIndexOf(value, startIndex, count);
			}

			// Token: 0x06006C5B RID: 27739 RVA: 0x001757DF File Offset: 0x001739DF
			public override void Remove(object value)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x06006C5C RID: 27740 RVA: 0x001757F0 File Offset: 0x001739F0
			public override void RemoveAt(int index)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x06006C5D RID: 27741 RVA: 0x00175801 File Offset: 0x00173A01
			public override void RemoveRange(int index, int count)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x06006C5E RID: 27742 RVA: 0x00175812 File Offset: 0x00173A12
			public override void SetRange(int index, ICollection c)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x06006C5F RID: 27743 RVA: 0x00175824 File Offset: 0x00173A24
			public override ArrayList GetRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (this.Count - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				return new ArrayList.Range(this, index, count);
			}

			// Token: 0x06006C60 RID: 27744 RVA: 0x0017587C File Offset: 0x00173A7C
			public override void Reverse(int index, int count)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x06006C61 RID: 27745 RVA: 0x0017588D File Offset: 0x00173A8D
			public override void Sort(int index, int count, IComparer comparer)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x06006C62 RID: 27746 RVA: 0x0017589E File Offset: 0x00173A9E
			public override object[] ToArray()
			{
				return this._list.ToArray();
			}

			// Token: 0x06006C63 RID: 27747 RVA: 0x001758AB File Offset: 0x00173AAB
			public override Array ToArray(Type type)
			{
				return this._list.ToArray(type);
			}

			// Token: 0x06006C64 RID: 27748 RVA: 0x001758B9 File Offset: 0x00173AB9
			public override void TrimToSize()
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x0400345D RID: 13405
			private ArrayList _list;
		}

		// Token: 0x02000B74 RID: 2932
		[Serializable]
		private sealed class ArrayListEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x06006C65 RID: 27749 RVA: 0x001758CA File Offset: 0x00173ACA
			internal ArrayListEnumerator(ArrayList list, int index, int count)
			{
				this.list = list;
				this.startIndex = index;
				this.index = index - 1;
				this.endIndex = this.index + count;
				this.version = list._version;
				this.currentElement = null;
			}

			// Token: 0x06006C66 RID: 27750 RVA: 0x0017590A File Offset: 0x00173B0A
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06006C67 RID: 27751 RVA: 0x00175914 File Offset: 0x00173B14
			public bool MoveNext()
			{
				if (this.version != this.list._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				if (this.index < this.endIndex)
				{
					ArrayList arrayList = this.list;
					int num = this.index + 1;
					this.index = num;
					this.currentElement = arrayList[num];
					return true;
				}
				this.index = this.endIndex + 1;
				return false;
			}

			// Token: 0x1700127A RID: 4730
			// (get) Token: 0x06006C68 RID: 27752 RVA: 0x00175988 File Offset: 0x00173B88
			public object Current
			{
				get
				{
					if (this.index < this.startIndex)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					if (this.index > this.endIndex)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
					}
					return this.currentElement;
				}
			}

			// Token: 0x06006C69 RID: 27753 RVA: 0x001759D7 File Offset: 0x00173BD7
			public void Reset()
			{
				if (this.version != this.list._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				this.index = this.startIndex - 1;
			}

			// Token: 0x0400345E RID: 13406
			private ArrayList list;

			// Token: 0x0400345F RID: 13407
			private int index;

			// Token: 0x04003460 RID: 13408
			private int endIndex;

			// Token: 0x04003461 RID: 13409
			private int version;

			// Token: 0x04003462 RID: 13410
			private object currentElement;

			// Token: 0x04003463 RID: 13411
			private int startIndex;
		}

		// Token: 0x02000B75 RID: 2933
		[Serializable]
		private class Range : ArrayList
		{
			// Token: 0x06006C6A RID: 27754 RVA: 0x00175A0A File Offset: 0x00173C0A
			internal Range(ArrayList list, int index, int count) : base(false)
			{
				this._baseList = list;
				this._baseIndex = index;
				this._baseSize = count;
				this._baseVersion = list._version;
				this._version = list._version;
			}

			// Token: 0x06006C6B RID: 27755 RVA: 0x00175A40 File Offset: 0x00173C40
			private void InternalUpdateRange()
			{
				if (this._baseVersion != this._baseList._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_UnderlyingArrayListChanged"));
				}
			}

			// Token: 0x06006C6C RID: 27756 RVA: 0x00175A65 File Offset: 0x00173C65
			private void InternalUpdateVersion()
			{
				this._baseVersion++;
				this._version++;
			}

			// Token: 0x06006C6D RID: 27757 RVA: 0x00175A84 File Offset: 0x00173C84
			public override int Add(object value)
			{
				this.InternalUpdateRange();
				this._baseList.Insert(this._baseIndex + this._baseSize, value);
				this.InternalUpdateVersion();
				int baseSize = this._baseSize;
				this._baseSize = baseSize + 1;
				return baseSize;
			}

			// Token: 0x06006C6E RID: 27758 RVA: 0x00175AC8 File Offset: 0x00173CC8
			public override void AddRange(ICollection c)
			{
				if (c == null)
				{
					throw new ArgumentNullException("c");
				}
				this.InternalUpdateRange();
				int count = c.Count;
				if (count > 0)
				{
					this._baseList.InsertRange(this._baseIndex + this._baseSize, c);
					this.InternalUpdateVersion();
					this._baseSize += count;
				}
			}

			// Token: 0x06006C6F RID: 27759 RVA: 0x00175B24 File Offset: 0x00173D24
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				this.InternalUpdateRange();
				int num = this._baseList.BinarySearch(this._baseIndex + index, count, value, comparer);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return num + this._baseIndex;
			}

			// Token: 0x1700127B RID: 4731
			// (get) Token: 0x06006C70 RID: 27760 RVA: 0x00175BA7 File Offset: 0x00173DA7
			// (set) Token: 0x06006C71 RID: 27761 RVA: 0x00175BB4 File Offset: 0x00173DB4
			public override int Capacity
			{
				get
				{
					return this._baseList.Capacity;
				}
				set
				{
					if (value < this.Count)
					{
						throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
					}
				}
			}

			// Token: 0x06006C72 RID: 27762 RVA: 0x00175BD4 File Offset: 0x00173DD4
			public override void Clear()
			{
				this.InternalUpdateRange();
				if (this._baseSize != 0)
				{
					this._baseList.RemoveRange(this._baseIndex, this._baseSize);
					this.InternalUpdateVersion();
					this._baseSize = 0;
				}
			}

			// Token: 0x06006C73 RID: 27763 RVA: 0x00175C08 File Offset: 0x00173E08
			public override object Clone()
			{
				this.InternalUpdateRange();
				return new ArrayList.Range(this._baseList, this._baseIndex, this._baseSize)
				{
					_baseList = (ArrayList)this._baseList.Clone()
				};
			}

			// Token: 0x06006C74 RID: 27764 RVA: 0x00175C4C File Offset: 0x00173E4C
			public override bool Contains(object item)
			{
				this.InternalUpdateRange();
				if (item == null)
				{
					for (int i = 0; i < this._baseSize; i++)
					{
						if (this._baseList[this._baseIndex + i] == null)
						{
							return true;
						}
					}
					return false;
				}
				for (int j = 0; j < this._baseSize; j++)
				{
					if (this._baseList[this._baseIndex + j] != null && this._baseList[this._baseIndex + j].Equals(item))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06006C75 RID: 27765 RVA: 0x00175CD0 File Offset: 0x00173ED0
			public override void CopyTo(Array array, int index)
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
				if (array.Length - index < this._baseSize)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				this.InternalUpdateRange();
				this._baseList.CopyTo(this._baseIndex, array, index, this._baseSize);
			}

			// Token: 0x06006C76 RID: 27766 RVA: 0x00175D5C File Offset: 0x00173F5C
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
				}
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (array.Length - arrayIndex < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				this.InternalUpdateRange();
				this._baseList.CopyTo(this._baseIndex + index, array, arrayIndex, count);
			}

			// Token: 0x1700127C RID: 4732
			// (get) Token: 0x06006C77 RID: 27767 RVA: 0x00175E0E File Offset: 0x0017400E
			public override int Count
			{
				get
				{
					this.InternalUpdateRange();
					return this._baseSize;
				}
			}

			// Token: 0x1700127D RID: 4733
			// (get) Token: 0x06006C78 RID: 27768 RVA: 0x00175E1C File Offset: 0x0017401C
			public override bool IsReadOnly
			{
				get
				{
					return this._baseList.IsReadOnly;
				}
			}

			// Token: 0x1700127E RID: 4734
			// (get) Token: 0x06006C79 RID: 27769 RVA: 0x00175E29 File Offset: 0x00174029
			public override bool IsFixedSize
			{
				get
				{
					return this._baseList.IsFixedSize;
				}
			}

			// Token: 0x1700127F RID: 4735
			// (get) Token: 0x06006C7A RID: 27770 RVA: 0x00175E36 File Offset: 0x00174036
			public override bool IsSynchronized
			{
				get
				{
					return this._baseList.IsSynchronized;
				}
			}

			// Token: 0x06006C7B RID: 27771 RVA: 0x00175E43 File Offset: 0x00174043
			public override IEnumerator GetEnumerator()
			{
				return this.GetEnumerator(0, this._baseSize);
			}

			// Token: 0x06006C7C RID: 27772 RVA: 0x00175E54 File Offset: 0x00174054
			public override IEnumerator GetEnumerator(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				this.InternalUpdateRange();
				return this._baseList.GetEnumerator(this._baseIndex + index, count);
			}

			// Token: 0x06006C7D RID: 27773 RVA: 0x00175EC0 File Offset: 0x001740C0
			public override ArrayList GetRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				this.InternalUpdateRange();
				return new ArrayList.Range(this, index, count);
			}

			// Token: 0x17001280 RID: 4736
			// (get) Token: 0x06006C7E RID: 27774 RVA: 0x00175F1E File Offset: 0x0017411E
			public override object SyncRoot
			{
				get
				{
					return this._baseList.SyncRoot;
				}
			}

			// Token: 0x06006C7F RID: 27775 RVA: 0x00175F2C File Offset: 0x0017412C
			public override int IndexOf(object value)
			{
				this.InternalUpdateRange();
				int num = this._baseList.IndexOf(value, this._baseIndex, this._baseSize);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return -1;
			}

			// Token: 0x06006C80 RID: 27776 RVA: 0x00175F68 File Offset: 0x00174168
			public override int IndexOf(object value, int startIndex)
			{
				if (startIndex < 0)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (startIndex > this._baseSize)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				this.InternalUpdateRange();
				int num = this._baseList.IndexOf(value, this._baseIndex + startIndex, this._baseSize - startIndex);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return -1;
			}

			// Token: 0x06006C81 RID: 27777 RVA: 0x00175FE0 File Offset: 0x001741E0
			public override int IndexOf(object value, int startIndex, int count)
			{
				if (startIndex < 0 || startIndex > this._baseSize)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				if (count < 0 || startIndex > this._baseSize - count)
				{
					throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
				}
				this.InternalUpdateRange();
				int num = this._baseList.IndexOf(value, this._baseIndex + startIndex, count);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return -1;
			}

			// Token: 0x06006C82 RID: 27778 RVA: 0x00176060 File Offset: 0x00174260
			public override void Insert(int index, object value)
			{
				if (index < 0 || index > this._baseSize)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				this.InternalUpdateRange();
				this._baseList.Insert(this._baseIndex + index, value);
				this.InternalUpdateVersion();
				this._baseSize++;
			}

			// Token: 0x06006C83 RID: 27779 RVA: 0x001760C0 File Offset: 0x001742C0
			public override void InsertRange(int index, ICollection c)
			{
				if (index < 0 || index > this._baseSize)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				if (c == null)
				{
					throw new ArgumentNullException("c");
				}
				this.InternalUpdateRange();
				int count = c.Count;
				if (count > 0)
				{
					this._baseList.InsertRange(this._baseIndex + index, c);
					this._baseSize += count;
					this.InternalUpdateVersion();
				}
			}

			// Token: 0x06006C84 RID: 27780 RVA: 0x00176138 File Offset: 0x00174338
			public override int LastIndexOf(object value)
			{
				this.InternalUpdateRange();
				int num = this._baseList.LastIndexOf(value, this._baseIndex + this._baseSize - 1, this._baseSize);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return -1;
			}

			// Token: 0x06006C85 RID: 27781 RVA: 0x0017617B File Offset: 0x0017437B
			public override int LastIndexOf(object value, int startIndex)
			{
				return this.LastIndexOf(value, startIndex, startIndex + 1);
			}

			// Token: 0x06006C86 RID: 27782 RVA: 0x00176188 File Offset: 0x00174388
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				this.InternalUpdateRange();
				if (this._baseSize == 0)
				{
					return -1;
				}
				if (startIndex >= this._baseSize)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				if (startIndex < 0)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				int num = this._baseList.LastIndexOf(value, this._baseIndex + startIndex, count);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return -1;
			}

			// Token: 0x06006C87 RID: 27783 RVA: 0x00176200 File Offset: 0x00174400
			public override void RemoveAt(int index)
			{
				if (index < 0 || index >= this._baseSize)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				this.InternalUpdateRange();
				this._baseList.RemoveAt(this._baseIndex + index);
				this.InternalUpdateVersion();
				this._baseSize--;
			}

			// Token: 0x06006C88 RID: 27784 RVA: 0x0017625C File Offset: 0x0017445C
			public override void RemoveRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				this.InternalUpdateRange();
				if (count > 0)
				{
					this._baseList.RemoveRange(this._baseIndex + index, count);
					this.InternalUpdateVersion();
					this._baseSize -= count;
				}
			}

			// Token: 0x06006C89 RID: 27785 RVA: 0x001762E0 File Offset: 0x001744E0
			public override void Reverse(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				this.InternalUpdateRange();
				this._baseList.Reverse(this._baseIndex + index, count);
				this.InternalUpdateVersion();
			}

			// Token: 0x06006C8A RID: 27786 RVA: 0x00176350 File Offset: 0x00174550
			public override void SetRange(int index, ICollection c)
			{
				this.InternalUpdateRange();
				if (index < 0 || index >= this._baseSize)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				this._baseList.SetRange(this._baseIndex + index, c);
				if (c.Count > 0)
				{
					this.InternalUpdateVersion();
				}
			}

			// Token: 0x06006C8B RID: 27787 RVA: 0x001763A8 File Offset: 0x001745A8
			public override void Sort(int index, int count, IComparer comparer)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				this.InternalUpdateRange();
				this._baseList.Sort(this._baseIndex + index, count, comparer);
				this.InternalUpdateVersion();
			}

			// Token: 0x17001281 RID: 4737
			public override object this[int index]
			{
				get
				{
					this.InternalUpdateRange();
					if (index < 0 || index >= this._baseSize)
					{
						throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
					}
					return this._baseList[this._baseIndex + index];
				}
				set
				{
					this.InternalUpdateRange();
					if (index < 0 || index >= this._baseSize)
					{
						throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
					}
					this._baseList[this._baseIndex + index] = value;
					this.InternalUpdateVersion();
				}
			}

			// Token: 0x06006C8E RID: 27790 RVA: 0x001764A8 File Offset: 0x001746A8
			public override object[] ToArray()
			{
				this.InternalUpdateRange();
				object[] array = new object[this._baseSize];
				Array.Copy(this._baseList._items, this._baseIndex, array, 0, this._baseSize);
				return array;
			}

			// Token: 0x06006C8F RID: 27791 RVA: 0x001764E8 File Offset: 0x001746E8
			[SecuritySafeCritical]
			public override Array ToArray(Type type)
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				this.InternalUpdateRange();
				Array array = Array.UnsafeCreateInstance(type, this._baseSize);
				this._baseList.CopyTo(this._baseIndex, array, 0, this._baseSize);
				return array;
			}

			// Token: 0x06006C90 RID: 27792 RVA: 0x00176536 File Offset: 0x00174736
			public override void TrimToSize()
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_RangeCollection"));
			}

			// Token: 0x04003464 RID: 13412
			private ArrayList _baseList;

			// Token: 0x04003465 RID: 13413
			private int _baseIndex;

			// Token: 0x04003466 RID: 13414
			private int _baseSize;

			// Token: 0x04003467 RID: 13415
			private int _baseVersion;
		}

		// Token: 0x02000B76 RID: 2934
		[Serializable]
		private sealed class ArrayListEnumeratorSimple : IEnumerator, ICloneable
		{
			// Token: 0x06006C91 RID: 27793 RVA: 0x00176548 File Offset: 0x00174748
			internal ArrayListEnumeratorSimple(ArrayList list)
			{
				this.list = list;
				this.index = -1;
				this.version = list._version;
				this.isArrayList = (list.GetType() == typeof(ArrayList));
				this.currentElement = ArrayList.ArrayListEnumeratorSimple.dummyObject;
			}

			// Token: 0x06006C92 RID: 27794 RVA: 0x0017659B File Offset: 0x0017479B
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06006C93 RID: 27795 RVA: 0x001765A4 File Offset: 0x001747A4
			public bool MoveNext()
			{
				if (this.version != this.list._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				if (this.isArrayList)
				{
					if (this.index < this.list._size - 1)
					{
						object[] items = this.list._items;
						int num = this.index + 1;
						this.index = num;
						this.currentElement = items[num];
						return true;
					}
					this.currentElement = ArrayList.ArrayListEnumeratorSimple.dummyObject;
					this.index = this.list._size;
					return false;
				}
				else
				{
					if (this.index < this.list.Count - 1)
					{
						ArrayList arrayList = this.list;
						int num = this.index + 1;
						this.index = num;
						this.currentElement = arrayList[num];
						return true;
					}
					this.index = this.list.Count;
					this.currentElement = ArrayList.ArrayListEnumeratorSimple.dummyObject;
					return false;
				}
			}

			// Token: 0x17001282 RID: 4738
			// (get) Token: 0x06006C94 RID: 27796 RVA: 0x0017668C File Offset: 0x0017488C
			public object Current
			{
				get
				{
					object obj = this.currentElement;
					if (ArrayList.ArrayListEnumeratorSimple.dummyObject != obj)
					{
						return obj;
					}
					if (this.index == -1)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
				}
			}

			// Token: 0x06006C95 RID: 27797 RVA: 0x001766D2 File Offset: 0x001748D2
			public void Reset()
			{
				if (this.version != this.list._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				this.currentElement = ArrayList.ArrayListEnumeratorSimple.dummyObject;
				this.index = -1;
			}

			// Token: 0x04003468 RID: 13416
			private ArrayList list;

			// Token: 0x04003469 RID: 13417
			private int index;

			// Token: 0x0400346A RID: 13418
			private int version;

			// Token: 0x0400346B RID: 13419
			private object currentElement;

			// Token: 0x0400346C RID: 13420
			[NonSerialized]
			private bool isArrayList;

			// Token: 0x0400346D RID: 13421
			private static object dummyObject = new object();
		}

		// Token: 0x02000B77 RID: 2935
		internal class ArrayListDebugView
		{
			// Token: 0x06006C97 RID: 27799 RVA: 0x00176715 File Offset: 0x00174915
			public ArrayListDebugView(ArrayList arrayList)
			{
				if (arrayList == null)
				{
					throw new ArgumentNullException("arrayList");
				}
				this.arrayList = arrayList;
			}

			// Token: 0x17001283 RID: 4739
			// (get) Token: 0x06006C98 RID: 27800 RVA: 0x00176732 File Offset: 0x00174932
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public object[] Items
			{
				get
				{
					return this.arrayList.ToArray();
				}
			}

			// Token: 0x0400346E RID: 13422
			private ArrayList arrayList;
		}
	}
}
