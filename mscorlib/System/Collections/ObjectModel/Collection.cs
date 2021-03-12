using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Collections.ObjectModel
{
	// Token: 0x02000488 RID: 1160
	[ComVisible(false)]
	[DebuggerTypeProxy(typeof(Mscorlib_CollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[__DynamicallyInvokable]
	[Serializable]
	public class Collection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IReadOnlyList<!0>, IReadOnlyCollection<T>
	{
		// Token: 0x06003889 RID: 14473 RVA: 0x000D80C0 File Offset: 0x000D62C0
		[__DynamicallyInvokable]
		public Collection()
		{
			this.items = new List<T>();
		}

		// Token: 0x0600388A RID: 14474 RVA: 0x000D80D3 File Offset: 0x000D62D3
		[__DynamicallyInvokable]
		public Collection(IList<T> list)
		{
			if (list == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.list);
			}
			this.items = list;
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x0600388B RID: 14475 RVA: 0x000D80EB File Offset: 0x000D62EB
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x0600388C RID: 14476 RVA: 0x000D80F8 File Offset: 0x000D62F8
		[__DynamicallyInvokable]
		protected IList<T> Items
		{
			[__DynamicallyInvokable]
			get
			{
				return this.items;
			}
		}

		// Token: 0x17000891 RID: 2193
		[__DynamicallyInvokable]
		public T this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				return this.items[index];
			}
			[__DynamicallyInvokable]
			set
			{
				if (this.items.IsReadOnly)
				{
					ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
				}
				if (index < 0 || index >= this.items.Count)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				this.SetItem(index, value);
			}
		}

		// Token: 0x0600388F RID: 14479 RVA: 0x000D8144 File Offset: 0x000D6344
		[__DynamicallyInvokable]
		public void Add(T item)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			int count = this.items.Count;
			this.InsertItem(count, item);
		}

		// Token: 0x06003890 RID: 14480 RVA: 0x000D8179 File Offset: 0x000D6379
		[__DynamicallyInvokable]
		public void Clear()
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			this.ClearItems();
		}

		// Token: 0x06003891 RID: 14481 RVA: 0x000D8195 File Offset: 0x000D6395
		[__DynamicallyInvokable]
		public void CopyTo(T[] array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x06003892 RID: 14482 RVA: 0x000D81A4 File Offset: 0x000D63A4
		[__DynamicallyInvokable]
		public bool Contains(T item)
		{
			return this.items.Contains(item);
		}

		// Token: 0x06003893 RID: 14483 RVA: 0x000D81B2 File Offset: 0x000D63B2
		[__DynamicallyInvokable]
		public IEnumerator<T> GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x06003894 RID: 14484 RVA: 0x000D81BF File Offset: 0x000D63BF
		[__DynamicallyInvokable]
		public int IndexOf(T item)
		{
			return this.items.IndexOf(item);
		}

		// Token: 0x06003895 RID: 14485 RVA: 0x000D81CD File Offset: 0x000D63CD
		[__DynamicallyInvokable]
		public void Insert(int index, T item)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			if (index < 0 || index > this.items.Count)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_ListInsert);
			}
			this.InsertItem(index, item);
		}

		// Token: 0x06003896 RID: 14486 RVA: 0x000D8208 File Offset: 0x000D6408
		[__DynamicallyInvokable]
		public bool Remove(T item)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			int num = this.items.IndexOf(item);
			if (num < 0)
			{
				return false;
			}
			this.RemoveItem(num);
			return true;
		}

		// Token: 0x06003897 RID: 14487 RVA: 0x000D8244 File Offset: 0x000D6444
		[__DynamicallyInvokable]
		public void RemoveAt(int index)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			if (index < 0 || index >= this.items.Count)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this.RemoveItem(index);
		}

		// Token: 0x06003898 RID: 14488 RVA: 0x000D8278 File Offset: 0x000D6478
		[__DynamicallyInvokable]
		protected virtual void ClearItems()
		{
			this.items.Clear();
		}

		// Token: 0x06003899 RID: 14489 RVA: 0x000D8285 File Offset: 0x000D6485
		[__DynamicallyInvokable]
		protected virtual void InsertItem(int index, T item)
		{
			this.items.Insert(index, item);
		}

		// Token: 0x0600389A RID: 14490 RVA: 0x000D8294 File Offset: 0x000D6494
		[__DynamicallyInvokable]
		protected virtual void RemoveItem(int index)
		{
			this.items.RemoveAt(index);
		}

		// Token: 0x0600389B RID: 14491 RVA: 0x000D82A2 File Offset: 0x000D64A2
		[__DynamicallyInvokable]
		protected virtual void SetItem(int index, T item)
		{
			this.items[index] = item;
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x0600389C RID: 14492 RVA: 0x000D82B1 File Offset: 0x000D64B1
		[__DynamicallyInvokable]
		bool ICollection<!0>.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return this.items.IsReadOnly;
			}
		}

		// Token: 0x0600389D RID: 14493 RVA: 0x000D82BE File Offset: 0x000D64BE
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x0600389E RID: 14494 RVA: 0x000D82CB File Offset: 0x000D64CB
		[__DynamicallyInvokable]
		bool ICollection.IsSynchronized
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x0600389F RID: 14495 RVA: 0x000D82D0 File Offset: 0x000D64D0
		[__DynamicallyInvokable]
		object ICollection.SyncRoot
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._syncRoot == null)
				{
					ICollection collection = this.items as ICollection;
					if (collection != null)
					{
						this._syncRoot = collection.SyncRoot;
					}
					else
					{
						Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
					}
				}
				return this._syncRoot;
			}
		}

		// Token: 0x060038A0 RID: 14496 RVA: 0x000D831C File Offset: 0x000D651C
		[__DynamicallyInvokable]
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (array.Rank != 1)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
			}
			if (array.GetLowerBound(0) != 0)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
			}
			if (index < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (array.Length - index < this.Count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			T[] array2 = array as T[];
			if (array2 != null)
			{
				this.items.CopyTo(array2, index);
				return;
			}
			Type elementType = array.GetType().GetElementType();
			Type typeFromHandle = typeof(T);
			if (!elementType.IsAssignableFrom(typeFromHandle) && !typeFromHandle.IsAssignableFrom(elementType))
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
			}
			object[] array3 = array as object[];
			if (array3 == null)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
			}
			int count = this.items.Count;
			try
			{
				for (int i = 0; i < count; i++)
				{
					array3[index++] = this.items[i];
				}
			}
			catch (ArrayTypeMismatchException)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
			}
		}

		// Token: 0x17000895 RID: 2197
		[__DynamicallyInvokable]
		object IList.this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				return this.items[index];
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

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x060038A3 RID: 14499 RVA: 0x000D847C File Offset: 0x000D667C
		[__DynamicallyInvokable]
		bool IList.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return this.items.IsReadOnly;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x060038A4 RID: 14500 RVA: 0x000D848C File Offset: 0x000D668C
		[__DynamicallyInvokable]
		bool IList.IsFixedSize
		{
			[__DynamicallyInvokable]
			get
			{
				IList list = this.items as IList;
				if (list != null)
				{
					return list.IsFixedSize;
				}
				return this.items.IsReadOnly;
			}
		}

		// Token: 0x060038A5 RID: 14501 RVA: 0x000D84BC File Offset: 0x000D66BC
		[__DynamicallyInvokable]
		int IList.Add(object value)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
			try
			{
				this.Add((T)((object)value));
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(T));
			}
			return this.Count - 1;
		}

		// Token: 0x060038A6 RID: 14502 RVA: 0x000D8520 File Offset: 0x000D6720
		[__DynamicallyInvokable]
		bool IList.Contains(object value)
		{
			return Collection<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
		}

		// Token: 0x060038A7 RID: 14503 RVA: 0x000D8538 File Offset: 0x000D6738
		[__DynamicallyInvokable]
		int IList.IndexOf(object value)
		{
			if (Collection<T>.IsCompatibleObject(value))
			{
				return this.IndexOf((T)((object)value));
			}
			return -1;
		}

		// Token: 0x060038A8 RID: 14504 RVA: 0x000D8550 File Offset: 0x000D6750
		[__DynamicallyInvokable]
		void IList.Insert(int index, object value)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
			try
			{
				this.Insert(index, (T)((object)value));
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(T));
			}
		}

		// Token: 0x060038A9 RID: 14505 RVA: 0x000D85AC File Offset: 0x000D67AC
		[__DynamicallyInvokable]
		void IList.Remove(object value)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			if (Collection<T>.IsCompatibleObject(value))
			{
				this.Remove((T)((object)value));
			}
		}

		// Token: 0x060038AA RID: 14506 RVA: 0x000D85D8 File Offset: 0x000D67D8
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && default(T) == null);
		}

		// Token: 0x04001878 RID: 6264
		private IList<T> items;

		// Token: 0x04001879 RID: 6265
		[NonSerialized]
		private object _syncRoot;
	}
}
