using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.Threading;

namespace System.Collections.Generic
{
	// Token: 0x020004AF RID: 1199
	[DebuggerTypeProxy(typeof(Mscorlib_CollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[__DynamicallyInvokable]
	[Serializable]
	public class List<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IReadOnlyList<!0>, IReadOnlyCollection<T>
	{
		// Token: 0x060039CC RID: 14796 RVA: 0x000DB119 File Offset: 0x000D9319
		[__DynamicallyInvokable]
		public List()
		{
			this._items = List<T>._emptyArray;
		}

		// Token: 0x060039CD RID: 14797 RVA: 0x000DB12C File Offset: 0x000D932C
		[__DynamicallyInvokable]
		public List(int capacity)
		{
			if (capacity < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (capacity == 0)
			{
				this._items = List<T>._emptyArray;
				return;
			}
			this._items = new T[capacity];
		}

		// Token: 0x060039CE RID: 14798 RVA: 0x000DB15C File Offset: 0x000D935C
		[__DynamicallyInvokable]
		public List(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			ICollection<T> collection2 = collection as ICollection<!0>;
			if (collection2 == null)
			{
				this._size = 0;
				this._items = List<T>._emptyArray;
				foreach (!0 item in collection)
				{
					this.Add(item);
				}
				return;
			}
			int count = collection2.Count;
			if (count == 0)
			{
				this._items = List<T>._emptyArray;
				return;
			}
			this._items = new T[count];
			collection2.CopyTo(this._items, 0);
			this._size = count;
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x060039CF RID: 14799 RVA: 0x000DB204 File Offset: 0x000D9404
		// (set) Token: 0x060039D0 RID: 14800 RVA: 0x000DB210 File Offset: 0x000D9410
		[__DynamicallyInvokable]
		public int Capacity
		{
			[__DynamicallyInvokable]
			get
			{
				return this._items.Length;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < this._size)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value, ExceptionResource.ArgumentOutOfRange_SmallCapacity);
				}
				if (value != this._items.Length)
				{
					if (value > 0)
					{
						T[] array = new T[value];
						if (this._size > 0)
						{
							Array.Copy(this._items, 0, array, 0, this._size);
						}
						this._items = array;
						return;
					}
					this._items = List<T>._emptyArray;
				}
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x060039D1 RID: 14801 RVA: 0x000DB275 File Offset: 0x000D9475
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this._size;
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x060039D2 RID: 14802 RVA: 0x000DB27D File Offset: 0x000D947D
		[__DynamicallyInvokable]
		bool IList.IsFixedSize
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x060039D3 RID: 14803 RVA: 0x000DB280 File Offset: 0x000D9480
		[__DynamicallyInvokable]
		bool ICollection<!0>.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x060039D4 RID: 14804 RVA: 0x000DB283 File Offset: 0x000D9483
		[__DynamicallyInvokable]
		bool IList.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x060039D5 RID: 14805 RVA: 0x000DB286 File Offset: 0x000D9486
		[__DynamicallyInvokable]
		bool ICollection.IsSynchronized
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x060039D6 RID: 14806 RVA: 0x000DB289 File Offset: 0x000D9489
		[__DynamicallyInvokable]
		object ICollection.SyncRoot
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x170008E4 RID: 2276
		[__DynamicallyInvokable]
		public T this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				if (index >= this._size)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				return this._items[index];
			}
			[__DynamicallyInvokable]
			set
			{
				if (index >= this._size)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				this._items[index] = value;
				this._version++;
			}
		}

		// Token: 0x060039D9 RID: 14809 RVA: 0x000DB2F4 File Offset: 0x000D94F4
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && default(T) == null);
		}

		// Token: 0x170008E5 RID: 2277
		[__DynamicallyInvokable]
		object IList.this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				return this[index];
			}
			[__DynamicallyInvokable]
			set
			{
				ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
				try
				{
					this[index] = (T)((object)value);
				}
				catch (InvalidCastException)
				{
					ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(T));
				}
			}
		}

		// Token: 0x060039DC RID: 14812 RVA: 0x000DB378 File Offset: 0x000D9578
		[__DynamicallyInvokable]
		public void Add(T item)
		{
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			T[] items = this._items;
			int size = this._size;
			this._size = size + 1;
			items[size] = item;
			this._version++;
		}

		// Token: 0x060039DD RID: 14813 RVA: 0x000DB3D0 File Offset: 0x000D95D0
		[__DynamicallyInvokable]
		int IList.Add(object item)
		{
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(item, ExceptionArgument.item);
			try
			{
				this.Add((T)((object)item));
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowWrongValueTypeArgumentException(item, typeof(T));
			}
			return this.Count - 1;
		}

		// Token: 0x060039DE RID: 14814 RVA: 0x000DB420 File Offset: 0x000D9620
		[__DynamicallyInvokable]
		public void AddRange(IEnumerable<T> collection)
		{
			this.InsertRange(this._size, collection);
		}

		// Token: 0x060039DF RID: 14815 RVA: 0x000DB42F File Offset: 0x000D962F
		[__DynamicallyInvokable]
		public ReadOnlyCollection<T> AsReadOnly()
		{
			return new ReadOnlyCollection<T>(this);
		}

		// Token: 0x060039E0 RID: 14816 RVA: 0x000DB437 File Offset: 0x000D9637
		[__DynamicallyInvokable]
		public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			return Array.BinarySearch<T>(this._items, index, count, item, comparer);
		}

		// Token: 0x060039E1 RID: 14817 RVA: 0x000DB473 File Offset: 0x000D9673
		[__DynamicallyInvokable]
		public int BinarySearch(T item)
		{
			return this.BinarySearch(0, this.Count, item, null);
		}

		// Token: 0x060039E2 RID: 14818 RVA: 0x000DB484 File Offset: 0x000D9684
		[__DynamicallyInvokable]
		public int BinarySearch(T item, IComparer<T> comparer)
		{
			return this.BinarySearch(0, this.Count, item, comparer);
		}

		// Token: 0x060039E3 RID: 14819 RVA: 0x000DB495 File Offset: 0x000D9695
		[__DynamicallyInvokable]
		public void Clear()
		{
			if (this._size > 0)
			{
				Array.Clear(this._items, 0, this._size);
				this._size = 0;
			}
			this._version++;
		}

		// Token: 0x060039E4 RID: 14820 RVA: 0x000DB4C8 File Offset: 0x000D96C8
		[__DynamicallyInvokable]
		public bool Contains(T item)
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
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			for (int j = 0; j < this._size; j++)
			{
				if (@default.Equals(this._items[j], item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060039E5 RID: 14821 RVA: 0x000DB534 File Offset: 0x000D9734
		[__DynamicallyInvokable]
		bool IList.Contains(object item)
		{
			return List<T>.IsCompatibleObject(item) && this.Contains((T)((object)item));
		}

		// Token: 0x060039E6 RID: 14822 RVA: 0x000DB54C File Offset: 0x000D974C
		public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
		{
			if (converter == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.converter);
			}
			List<TOutput> list = new List<TOutput>(this._size);
			for (int i = 0; i < this._size; i++)
			{
				list._items[i] = converter(this._items[i]);
			}
			list._size = this._size;
			return list;
		}

		// Token: 0x060039E7 RID: 14823 RVA: 0x000DB5AB File Offset: 0x000D97AB
		[__DynamicallyInvokable]
		public void CopyTo(T[] array)
		{
			this.CopyTo(array, 0);
		}

		// Token: 0x060039E8 RID: 14824 RVA: 0x000DB5B8 File Offset: 0x000D97B8
		[__DynamicallyInvokable]
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			if (array != null && array.Rank != 1)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
			}
			try
			{
				Array.Copy(this._items, 0, array, arrayIndex, this._size);
			}
			catch (ArrayTypeMismatchException)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
			}
		}

		// Token: 0x060039E9 RID: 14825 RVA: 0x000DB608 File Offset: 0x000D9808
		[__DynamicallyInvokable]
		public void CopyTo(int index, T[] array, int arrayIndex, int count)
		{
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			Array.Copy(this._items, index, array, arrayIndex, count);
		}

		// Token: 0x060039EA RID: 14826 RVA: 0x000DB62D File Offset: 0x000D982D
		[__DynamicallyInvokable]
		public void CopyTo(T[] array, int arrayIndex)
		{
			Array.Copy(this._items, 0, array, arrayIndex, this._size);
		}

		// Token: 0x060039EB RID: 14827 RVA: 0x000DB644 File Offset: 0x000D9844
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

		// Token: 0x060039EC RID: 14828 RVA: 0x000DB68E File Offset: 0x000D988E
		[__DynamicallyInvokable]
		public bool Exists(Predicate<T> match)
		{
			return this.FindIndex(match) != -1;
		}

		// Token: 0x060039ED RID: 14829 RVA: 0x000DB6A0 File Offset: 0x000D98A0
		[__DynamicallyInvokable]
		public T Find(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			for (int i = 0; i < this._size; i++)
			{
				if (match(this._items[i]))
				{
					return this._items[i];
				}
			}
			return default(T);
		}

		// Token: 0x060039EE RID: 14830 RVA: 0x000DB6F4 File Offset: 0x000D98F4
		[__DynamicallyInvokable]
		public List<T> FindAll(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			List<T> list = new List<T>();
			for (int i = 0; i < this._size; i++)
			{
				if (match(this._items[i]))
				{
					list.Add(this._items[i]);
				}
			}
			return list;
		}

		// Token: 0x060039EF RID: 14831 RVA: 0x000DB748 File Offset: 0x000D9948
		[__DynamicallyInvokable]
		public int FindIndex(Predicate<T> match)
		{
			return this.FindIndex(0, this._size, match);
		}

		// Token: 0x060039F0 RID: 14832 RVA: 0x000DB758 File Offset: 0x000D9958
		[__DynamicallyInvokable]
		public int FindIndex(int startIndex, Predicate<T> match)
		{
			return this.FindIndex(startIndex, this._size - startIndex, match);
		}

		// Token: 0x060039F1 RID: 14833 RVA: 0x000DB76C File Offset: 0x000D996C
		[__DynamicallyInvokable]
		public int FindIndex(int startIndex, int count, Predicate<T> match)
		{
			if (startIndex > this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
			}
			if (count < 0 || startIndex > this._size - count)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_Count);
			}
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			int num = startIndex + count;
			for (int i = startIndex; i < num; i++)
			{
				if (match(this._items[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060039F2 RID: 14834 RVA: 0x000DB7D4 File Offset: 0x000D99D4
		[__DynamicallyInvokable]
		public T FindLast(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			for (int i = this._size - 1; i >= 0; i--)
			{
				if (match(this._items[i]))
				{
					return this._items[i];
				}
			}
			return default(T);
		}

		// Token: 0x060039F3 RID: 14835 RVA: 0x000DB827 File Offset: 0x000D9A27
		[__DynamicallyInvokable]
		public int FindLastIndex(Predicate<T> match)
		{
			return this.FindLastIndex(this._size - 1, this._size, match);
		}

		// Token: 0x060039F4 RID: 14836 RVA: 0x000DB83E File Offset: 0x000D9A3E
		[__DynamicallyInvokable]
		public int FindLastIndex(int startIndex, Predicate<T> match)
		{
			return this.FindLastIndex(startIndex, startIndex + 1, match);
		}

		// Token: 0x060039F5 RID: 14837 RVA: 0x000DB84C File Offset: 0x000D9A4C
		[__DynamicallyInvokable]
		public int FindLastIndex(int startIndex, int count, Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			if (this._size == 0)
			{
				if (startIndex != -1)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
				}
			}
			else if (startIndex >= this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
			}
			if (count < 0 || startIndex - count + 1 < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_Count);
			}
			int num = startIndex - count;
			for (int i = startIndex; i > num; i--)
			{
				if (match(this._items[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060039F6 RID: 14838 RVA: 0x000DB8C8 File Offset: 0x000D9AC8
		[__DynamicallyInvokable]
		public void ForEach(Action<T> action)
		{
			if (action == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			int version = this._version;
			int num = 0;
			while (num < this._size && (version == this._version || !BinaryCompatibility.TargetsAtLeast_Desktop_V4_5))
			{
				action(this._items[num]);
				num++;
			}
			if (version != this._version && BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
			}
		}

		// Token: 0x060039F7 RID: 14839 RVA: 0x000DB92F File Offset: 0x000D9B2F
		[__DynamicallyInvokable]
		public List<T>.Enumerator GetEnumerator()
		{
			return new List<T>.Enumerator(this);
		}

		// Token: 0x060039F8 RID: 14840 RVA: 0x000DB937 File Offset: 0x000D9B37
		[__DynamicallyInvokable]
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return new List<T>.Enumerator(this);
		}

		// Token: 0x060039F9 RID: 14841 RVA: 0x000DB944 File Offset: 0x000D9B44
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new List<T>.Enumerator(this);
		}

		// Token: 0x060039FA RID: 14842 RVA: 0x000DB954 File Offset: 0x000D9B54
		[__DynamicallyInvokable]
		public List<T> GetRange(int index, int count)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			List<T> list = new List<T>(count);
			Array.Copy(this._items, index, list._items, 0, count);
			list._size = count;
			return list;
		}

		// Token: 0x060039FB RID: 14843 RVA: 0x000DB9AE File Offset: 0x000D9BAE
		[__DynamicallyInvokable]
		public int IndexOf(T item)
		{
			return Array.IndexOf<T>(this._items, item, 0, this._size);
		}

		// Token: 0x060039FC RID: 14844 RVA: 0x000DB9C3 File Offset: 0x000D9BC3
		[__DynamicallyInvokable]
		int IList.IndexOf(object item)
		{
			if (List<T>.IsCompatibleObject(item))
			{
				return this.IndexOf((T)((object)item));
			}
			return -1;
		}

		// Token: 0x060039FD RID: 14845 RVA: 0x000DB9DB File Offset: 0x000D9BDB
		[__DynamicallyInvokable]
		public int IndexOf(T item, int index)
		{
			if (index > this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
			}
			return Array.IndexOf<T>(this._items, item, index, this._size - index);
		}

		// Token: 0x060039FE RID: 14846 RVA: 0x000DBA04 File Offset: 0x000D9C04
		[__DynamicallyInvokable]
		public int IndexOf(T item, int index, int count)
		{
			if (index > this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
			}
			if (count < 0 || index > this._size - count)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_Count);
			}
			return Array.IndexOf<T>(this._items, item, index, count);
		}

		// Token: 0x060039FF RID: 14847 RVA: 0x000DBA40 File Offset: 0x000D9C40
		[__DynamicallyInvokable]
		public void Insert(int index, T item)
		{
			if (index > this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_ListInsert);
			}
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			if (index < this._size)
			{
				Array.Copy(this._items, index, this._items, index + 1, this._size - index);
			}
			this._items[index] = item;
			this._size++;
			this._version++;
		}

		// Token: 0x06003A00 RID: 14848 RVA: 0x000DBACC File Offset: 0x000D9CCC
		[__DynamicallyInvokable]
		void IList.Insert(int index, object item)
		{
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(item, ExceptionArgument.item);
			try
			{
				this.Insert(index, (T)((object)item));
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowWrongValueTypeArgumentException(item, typeof(T));
			}
		}

		// Token: 0x06003A01 RID: 14849 RVA: 0x000DBB14 File Offset: 0x000D9D14
		[__DynamicallyInvokable]
		public void InsertRange(int index, IEnumerable<T> collection)
		{
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			if (index > this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
			}
			ICollection<T> collection2 = collection as ICollection<!0>;
			if (collection2 != null)
			{
				int count = collection2.Count;
				if (count > 0)
				{
					this.EnsureCapacity(this._size + count);
					if (index < this._size)
					{
						Array.Copy(this._items, index, this._items, index + count, this._size - index);
					}
					if (this == collection2)
					{
						Array.Copy(this._items, 0, this._items, index, index);
						Array.Copy(this._items, index + count, this._items, index * 2, this._size - index);
					}
					else
					{
						T[] array = new T[count];
						collection2.CopyTo(array, 0);
						array.CopyTo(this._items, index);
					}
					this._size += count;
				}
			}
			else
			{
				foreach (!0 item in collection)
				{
					this.Insert(index++, item);
				}
			}
			this._version++;
		}

		// Token: 0x06003A02 RID: 14850 RVA: 0x000DBC40 File Offset: 0x000D9E40
		[__DynamicallyInvokable]
		public int LastIndexOf(T item)
		{
			if (this._size == 0)
			{
				return -1;
			}
			return this.LastIndexOf(item, this._size - 1, this._size);
		}

		// Token: 0x06003A03 RID: 14851 RVA: 0x000DBC61 File Offset: 0x000D9E61
		[__DynamicallyInvokable]
		public int LastIndexOf(T item, int index)
		{
			if (index >= this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
			}
			return this.LastIndexOf(item, index, index + 1);
		}

		// Token: 0x06003A04 RID: 14852 RVA: 0x000DBC80 File Offset: 0x000D9E80
		[__DynamicallyInvokable]
		public int LastIndexOf(T item, int index, int count)
		{
			if (this.Count != 0 && index < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this.Count != 0 && count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size == 0)
			{
				return -1;
			}
			if (index >= this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_BiggerThanCollection);
			}
			if (count > index + 1)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_BiggerThanCollection);
			}
			return Array.LastIndexOf<T>(this._items, item, index, count);
		}

		// Token: 0x06003A05 RID: 14853 RVA: 0x000DBCF0 File Offset: 0x000D9EF0
		[__DynamicallyInvokable]
		public bool Remove(T item)
		{
			int num = this.IndexOf(item);
			if (num >= 0)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x06003A06 RID: 14854 RVA: 0x000DBD13 File Offset: 0x000D9F13
		[__DynamicallyInvokable]
		void IList.Remove(object item)
		{
			if (List<T>.IsCompatibleObject(item))
			{
				this.Remove((T)((object)item));
			}
		}

		// Token: 0x06003A07 RID: 14855 RVA: 0x000DBD2C File Offset: 0x000D9F2C
		[__DynamicallyInvokable]
		public int RemoveAll(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			int num = 0;
			while (num < this._size && !match(this._items[num]))
			{
				num++;
			}
			if (num >= this._size)
			{
				return 0;
			}
			int i = num + 1;
			while (i < this._size)
			{
				while (i < this._size && match(this._items[i]))
				{
					i++;
				}
				if (i < this._size)
				{
					this._items[num++] = this._items[i++];
				}
			}
			Array.Clear(this._items, num, this._size - num);
			int result = this._size - num;
			this._size = num;
			this._version++;
			return result;
		}

		// Token: 0x06003A08 RID: 14856 RVA: 0x000DBE00 File Offset: 0x000DA000
		[__DynamicallyInvokable]
		public void RemoveAt(int index)
		{
			if (index >= this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this._items, index + 1, this._items, index, this._size - index);
			}
			this._items[this._size] = default(T);
			this._version++;
		}

		// Token: 0x06003A09 RID: 14857 RVA: 0x000DBE78 File Offset: 0x000DA078
		[__DynamicallyInvokable]
		public void RemoveRange(int index, int count)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			if (count > 0)
			{
				int size = this._size;
				this._size -= count;
				if (index < this._size)
				{
					Array.Copy(this._items, index + count, this._items, index, this._size - index);
				}
				Array.Clear(this._items, this._size, count);
				this._version++;
			}
		}

		// Token: 0x06003A0A RID: 14858 RVA: 0x000DBF0E File Offset: 0x000DA10E
		[__DynamicallyInvokable]
		public void Reverse()
		{
			this.Reverse(0, this.Count);
		}

		// Token: 0x06003A0B RID: 14859 RVA: 0x000DBF20 File Offset: 0x000DA120
		[__DynamicallyInvokable]
		public void Reverse(int index, int count)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			Array.Reverse(this._items, index, count);
			this._version++;
		}

		// Token: 0x06003A0C RID: 14860 RVA: 0x000DBF72 File Offset: 0x000DA172
		[__DynamicallyInvokable]
		public void Sort()
		{
			this.Sort(0, this.Count, null);
		}

		// Token: 0x06003A0D RID: 14861 RVA: 0x000DBF82 File Offset: 0x000DA182
		[__DynamicallyInvokable]
		public void Sort(IComparer<T> comparer)
		{
			this.Sort(0, this.Count, comparer);
		}

		// Token: 0x06003A0E RID: 14862 RVA: 0x000DBF94 File Offset: 0x000DA194
		[__DynamicallyInvokable]
		public void Sort(int index, int count, IComparer<T> comparer)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			Array.Sort<T>(this._items, index, count, comparer);
			this._version++;
		}

		// Token: 0x06003A0F RID: 14863 RVA: 0x000DBFE8 File Offset: 0x000DA1E8
		[__DynamicallyInvokable]
		public void Sort(Comparison<T> comparison)
		{
			if (comparison == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			if (this._size > 0)
			{
				IComparer<T> comparer = new Array.FunctorComparer<T>(comparison);
				Array.Sort<T>(this._items, 0, this._size, comparer);
			}
		}

		// Token: 0x06003A10 RID: 14864 RVA: 0x000DC024 File Offset: 0x000DA224
		[__DynamicallyInvokable]
		public T[] ToArray()
		{
			T[] array = new T[this._size];
			Array.Copy(this._items, 0, array, 0, this._size);
			return array;
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x000DC054 File Offset: 0x000DA254
		[__DynamicallyInvokable]
		public void TrimExcess()
		{
			int num = (int)((double)this._items.Length * 0.9);
			if (this._size < num)
			{
				this.Capacity = this._size;
			}
		}

		// Token: 0x06003A12 RID: 14866 RVA: 0x000DC08C File Offset: 0x000DA28C
		[__DynamicallyInvokable]
		public bool TrueForAll(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			for (int i = 0; i < this._size; i++)
			{
				if (!match(this._items[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003A13 RID: 14867 RVA: 0x000DC0CA File Offset: 0x000DA2CA
		internal static IList<T> Synchronized(List<T> list)
		{
			return new List<T>.SynchronizedList(list);
		}

		// Token: 0x0400189F RID: 6303
		private const int _defaultCapacity = 4;

		// Token: 0x040018A0 RID: 6304
		private T[] _items;

		// Token: 0x040018A1 RID: 6305
		private int _size;

		// Token: 0x040018A2 RID: 6306
		private int _version;

		// Token: 0x040018A3 RID: 6307
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x040018A4 RID: 6308
		private static readonly T[] _emptyArray = new T[0];

		// Token: 0x02000BAD RID: 2989
		[Serializable]
		internal class SynchronizedList : IList<!0>, ICollection<!0>, IEnumerable<!0>, IEnumerable
		{
			// Token: 0x06006DFB RID: 28155 RVA: 0x00179FD0 File Offset: 0x001781D0
			internal SynchronizedList(List<T> list)
			{
				this._list = list;
				this._root = ((ICollection)list).SyncRoot;
			}

			// Token: 0x170012FB RID: 4859
			// (get) Token: 0x06006DFC RID: 28156 RVA: 0x00179FEC File Offset: 0x001781EC
			public int Count
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

			// Token: 0x170012FC RID: 4860
			// (get) Token: 0x06006DFD RID: 28157 RVA: 0x0017A034 File Offset: 0x00178234
			public bool IsReadOnly
			{
				get
				{
					return ((ICollection<!0>)this._list).IsReadOnly;
				}
			}

			// Token: 0x06006DFE RID: 28158 RVA: 0x0017A044 File Offset: 0x00178244
			public void Add(T item)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Add(item);
				}
			}

			// Token: 0x06006DFF RID: 28159 RVA: 0x0017A08C File Offset: 0x0017828C
			public void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._list.Clear();
				}
			}

			// Token: 0x06006E00 RID: 28160 RVA: 0x0017A0D4 File Offset: 0x001782D4
			public bool Contains(T item)
			{
				object root = this._root;
				bool result;
				lock (root)
				{
					result = this._list.Contains(item);
				}
				return result;
			}

			// Token: 0x06006E01 RID: 28161 RVA: 0x0017A11C File Offset: 0x0017831C
			public void CopyTo(T[] array, int arrayIndex)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(array, arrayIndex);
				}
			}

			// Token: 0x06006E02 RID: 28162 RVA: 0x0017A164 File Offset: 0x00178364
			public bool Remove(T item)
			{
				object root = this._root;
				bool result;
				lock (root)
				{
					result = this._list.Remove(item);
				}
				return result;
			}

			// Token: 0x06006E03 RID: 28163 RVA: 0x0017A1AC File Offset: 0x001783AC
			IEnumerator IEnumerable.GetEnumerator()
			{
				object root = this._root;
				IEnumerator result;
				lock (root)
				{
					result = this._list.GetEnumerator();
				}
				return result;
			}

			// Token: 0x06006E04 RID: 28164 RVA: 0x0017A1F8 File Offset: 0x001783F8
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				object root = this._root;
				IEnumerator<T> enumerator;
				lock (root)
				{
					enumerator = ((IEnumerable<!0>)this._list).GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x170012FD RID: 4861
			public T this[int index]
			{
				get
				{
					object root = this._root;
					T result;
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

			// Token: 0x06006E07 RID: 28167 RVA: 0x0017A2D0 File Offset: 0x001784D0
			public int IndexOf(T item)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.IndexOf(item);
				}
				return result;
			}

			// Token: 0x06006E08 RID: 28168 RVA: 0x0017A318 File Offset: 0x00178518
			public void Insert(int index, T item)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Insert(index, item);
				}
			}

			// Token: 0x06006E09 RID: 28169 RVA: 0x0017A360 File Offset: 0x00178560
			public void RemoveAt(int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.RemoveAt(index);
				}
			}

			// Token: 0x0400350F RID: 13583
			private List<T> _list;

			// Token: 0x04003510 RID: 13584
			private object _root;
		}

		// Token: 0x02000BAE RID: 2990
		[__DynamicallyInvokable]
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06006E0A RID: 28170 RVA: 0x0017A3A8 File Offset: 0x001785A8
			internal Enumerator(List<T> list)
			{
				this.list = list;
				this.index = 0;
				this.version = list._version;
				this.current = default(T);
			}

			// Token: 0x06006E0B RID: 28171 RVA: 0x0017A3D0 File Offset: 0x001785D0
			[__DynamicallyInvokable]
			public void Dispose()
			{
			}

			// Token: 0x06006E0C RID: 28172 RVA: 0x0017A3D4 File Offset: 0x001785D4
			[__DynamicallyInvokable]
			public bool MoveNext()
			{
				List<T> list = this.list;
				if (this.version == list._version && this.index < list._size)
				{
					this.current = list._items[this.index];
					this.index++;
					return true;
				}
				return this.MoveNextRare();
			}

			// Token: 0x06006E0D RID: 28173 RVA: 0x0017A431 File Offset: 0x00178631
			private bool MoveNextRare()
			{
				if (this.version != this.list._version)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				this.index = this.list._size + 1;
				this.current = default(T);
				return false;
			}

			// Token: 0x170012FE RID: 4862
			// (get) Token: 0x06006E0E RID: 28174 RVA: 0x0017A46D File Offset: 0x0017866D
			[__DynamicallyInvokable]
			public T Current
			{
				[__DynamicallyInvokable]
				get
				{
					return this.current;
				}
			}

			// Token: 0x170012FF RID: 4863
			// (get) Token: 0x06006E0F RID: 28175 RVA: 0x0017A475 File Offset: 0x00178675
			[__DynamicallyInvokable]
			object IEnumerator.Current
			{
				[__DynamicallyInvokable]
				get
				{
					if (this.index == 0 || this.index == this.list._size + 1)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					return this.Current;
				}
			}

			// Token: 0x06006E10 RID: 28176 RVA: 0x0017A4A6 File Offset: 0x001786A6
			[__DynamicallyInvokable]
			void IEnumerator.Reset()
			{
				if (this.version != this.list._version)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				this.index = 0;
				this.current = default(T);
			}

			// Token: 0x04003511 RID: 13585
			private List<T> list;

			// Token: 0x04003512 RID: 13586
			private int index;

			// Token: 0x04003513 RID: 13587
			private int version;

			// Token: 0x04003514 RID: 13588
			private T current;
		}
	}
}
