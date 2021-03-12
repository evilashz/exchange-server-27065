using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Collections.ObjectModel
{
	// Token: 0x02000489 RID: 1161
	[ComVisible(false)]
	[DebuggerTypeProxy(typeof(Mscorlib_CollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[__DynamicallyInvokable]
	[Serializable]
	public class ReadOnlyCollection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IReadOnlyList<!0>, IReadOnlyCollection<T>
	{
		// Token: 0x060038AB RID: 14507 RVA: 0x000D8605 File Offset: 0x000D6805
		[__DynamicallyInvokable]
		public ReadOnlyCollection(IList<T> list)
		{
			if (list == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.list);
			}
			this.list = list;
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x060038AC RID: 14508 RVA: 0x000D861D File Offset: 0x000D681D
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x17000899 RID: 2201
		[__DynamicallyInvokable]
		public T this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				return this.list[index];
			}
		}

		// Token: 0x060038AE RID: 14510 RVA: 0x000D8638 File Offset: 0x000D6838
		[__DynamicallyInvokable]
		public bool Contains(T value)
		{
			return this.list.Contains(value);
		}

		// Token: 0x060038AF RID: 14511 RVA: 0x000D8646 File Offset: 0x000D6846
		[__DynamicallyInvokable]
		public void CopyTo(T[] array, int index)
		{
			this.list.CopyTo(array, index);
		}

		// Token: 0x060038B0 RID: 14512 RVA: 0x000D8655 File Offset: 0x000D6855
		[__DynamicallyInvokable]
		public IEnumerator<T> GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x060038B1 RID: 14513 RVA: 0x000D8662 File Offset: 0x000D6862
		[__DynamicallyInvokable]
		public int IndexOf(T value)
		{
			return this.list.IndexOf(value);
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x060038B2 RID: 14514 RVA: 0x000D8670 File Offset: 0x000D6870
		[__DynamicallyInvokable]
		protected IList<T> Items
		{
			[__DynamicallyInvokable]
			get
			{
				return this.list;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x060038B3 RID: 14515 RVA: 0x000D8678 File Offset: 0x000D6878
		[__DynamicallyInvokable]
		bool ICollection<!0>.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		// Token: 0x1700089C RID: 2204
		[__DynamicallyInvokable]
		T IList<!0>.this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				return this.list[index];
			}
			[__DynamicallyInvokable]
			set
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
		}

		// Token: 0x060038B6 RID: 14518 RVA: 0x000D8692 File Offset: 0x000D6892
		[__DynamicallyInvokable]
		void ICollection<!0>.Add(T value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x060038B7 RID: 14519 RVA: 0x000D869B File Offset: 0x000D689B
		[__DynamicallyInvokable]
		void ICollection<!0>.Clear()
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x060038B8 RID: 14520 RVA: 0x000D86A4 File Offset: 0x000D68A4
		[__DynamicallyInvokable]
		void IList<!0>.Insert(int index, T value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x060038B9 RID: 14521 RVA: 0x000D86AD File Offset: 0x000D68AD
		[__DynamicallyInvokable]
		bool ICollection<!0>.Remove(T value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			return false;
		}

		// Token: 0x060038BA RID: 14522 RVA: 0x000D86B7 File Offset: 0x000D68B7
		[__DynamicallyInvokable]
		void IList<!0>.RemoveAt(int index)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x060038BB RID: 14523 RVA: 0x000D86C0 File Offset: 0x000D68C0
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x060038BC RID: 14524 RVA: 0x000D86CD File Offset: 0x000D68CD
		[__DynamicallyInvokable]
		bool ICollection.IsSynchronized
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x060038BD RID: 14525 RVA: 0x000D86D0 File Offset: 0x000D68D0
		[__DynamicallyInvokable]
		object ICollection.SyncRoot
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._syncRoot == null)
				{
					ICollection collection = this.list as ICollection;
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

		// Token: 0x060038BE RID: 14526 RVA: 0x000D871C File Offset: 0x000D691C
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
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.arrayIndex, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (array.Length - index < this.Count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			T[] array2 = array as T[];
			if (array2 != null)
			{
				this.list.CopyTo(array2, index);
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
			int count = this.list.Count;
			try
			{
				for (int i = 0; i < count; i++)
				{
					array3[index++] = this.list[i];
				}
			}
			catch (ArrayTypeMismatchException)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x060038BF RID: 14527 RVA: 0x000D8820 File Offset: 0x000D6A20
		[__DynamicallyInvokable]
		bool IList.IsFixedSize
		{
			[__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x060038C0 RID: 14528 RVA: 0x000D8823 File Offset: 0x000D6A23
		[__DynamicallyInvokable]
		bool IList.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		// Token: 0x170008A1 RID: 2209
		[__DynamicallyInvokable]
		object IList.this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				return this.list[index];
			}
			[__DynamicallyInvokable]
			set
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
		}

		// Token: 0x060038C3 RID: 14531 RVA: 0x000D8842 File Offset: 0x000D6A42
		[__DynamicallyInvokable]
		int IList.Add(object value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			return -1;
		}

		// Token: 0x060038C4 RID: 14532 RVA: 0x000D884C File Offset: 0x000D6A4C
		[__DynamicallyInvokable]
		void IList.Clear()
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x060038C5 RID: 14533 RVA: 0x000D8858 File Offset: 0x000D6A58
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && default(T) == null);
		}

		// Token: 0x060038C6 RID: 14534 RVA: 0x000D8885 File Offset: 0x000D6A85
		[__DynamicallyInvokable]
		bool IList.Contains(object value)
		{
			return ReadOnlyCollection<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
		}

		// Token: 0x060038C7 RID: 14535 RVA: 0x000D889D File Offset: 0x000D6A9D
		[__DynamicallyInvokable]
		int IList.IndexOf(object value)
		{
			if (ReadOnlyCollection<T>.IsCompatibleObject(value))
			{
				return this.IndexOf((T)((object)value));
			}
			return -1;
		}

		// Token: 0x060038C8 RID: 14536 RVA: 0x000D88B5 File Offset: 0x000D6AB5
		[__DynamicallyInvokable]
		void IList.Insert(int index, object value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x060038C9 RID: 14537 RVA: 0x000D88BE File Offset: 0x000D6ABE
		[__DynamicallyInvokable]
		void IList.Remove(object value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x060038CA RID: 14538 RVA: 0x000D88C7 File Offset: 0x000D6AC7
		[__DynamicallyInvokable]
		void IList.RemoveAt(int index)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x0400187A RID: 6266
		private IList<T> list;

		// Token: 0x0400187B RID: 6267
		[NonSerialized]
		private object _syncRoot;
	}
}
