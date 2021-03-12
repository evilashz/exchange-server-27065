using System;
using System.Collections;
using System.Collections.Generic;

namespace System
{
	// Token: 0x02000057 RID: 87
	[__DynamicallyInvokable]
	[Serializable]
	public struct ArraySegment<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyList<T>, IReadOnlyCollection<T>
	{
		// Token: 0x0600031A RID: 794 RVA: 0x00007D3A File Offset: 0x00005F3A
		[__DynamicallyInvokable]
		public ArraySegment(T[] array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			this._array = array;
			this._offset = 0;
			this._count = array.Length;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00007D64 File Offset: 0x00005F64
		[__DynamicallyInvokable]
		public ArraySegment(T[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			this._array = array;
			this._offset = offset;
			this._count = count;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00007DDE File Offset: 0x00005FDE
		[__DynamicallyInvokable]
		public T[] Array
		{
			[__DynamicallyInvokable]
			get
			{
				return this._array;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00007DE6 File Offset: 0x00005FE6
		[__DynamicallyInvokable]
		public int Offset
		{
			[__DynamicallyInvokable]
			get
			{
				return this._offset;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00007DEE File Offset: 0x00005FEE
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this._count;
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00007DF6 File Offset: 0x00005FF6
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			if (this._array != null)
			{
				return this._array.GetHashCode() ^ this._offset ^ this._count;
			}
			return 0;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00007E1B File Offset: 0x0000601B
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is ArraySegment<T> && this.Equals((ArraySegment<T>)obj);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00007E33 File Offset: 0x00006033
		[__DynamicallyInvokable]
		public bool Equals(ArraySegment<T> obj)
		{
			return obj._array == this._array && obj._offset == this._offset && obj._count == this._count;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00007E61 File Offset: 0x00006061
		[__DynamicallyInvokable]
		public static bool operator ==(ArraySegment<T> a, ArraySegment<T> b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00007E6B File Offset: 0x0000606B
		[__DynamicallyInvokable]
		public static bool operator !=(ArraySegment<T> a, ArraySegment<T> b)
		{
			return !(a == b);
		}

		// Token: 0x1700003B RID: 59
		[__DynamicallyInvokable]
		T IList<!0>.this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._array == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
				}
				if (index < 0 || index >= this._count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this._array[this._offset + index];
			}
			[__DynamicallyInvokable]
			set
			{
				if (this._array == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
				}
				if (index < 0 || index >= this._count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this._array[this._offset + index] = value;
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00007F1C File Offset: 0x0000611C
		[__DynamicallyInvokable]
		int IList<!0>.IndexOf(T item)
		{
			if (this._array == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
			}
			int num = System.Array.IndexOf<T>(this._array, item, this._offset, this._count);
			if (num < 0)
			{
				return -1;
			}
			return num - this._offset;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00007F68 File Offset: 0x00006168
		[__DynamicallyInvokable]
		void IList<!0>.Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00007F6F File Offset: 0x0000616F
		[__DynamicallyInvokable]
		void IList<!0>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700003C RID: 60
		[__DynamicallyInvokable]
		T IReadOnlyList<!0>.this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._array == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
				}
				if (index < 0 || index >= this._count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this._array[this._offset + index];
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00007FC8 File Offset: 0x000061C8
		[__DynamicallyInvokable]
		bool ICollection<!0>.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00007FCB File Offset: 0x000061CB
		[__DynamicallyInvokable]
		void ICollection<!0>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00007FD2 File Offset: 0x000061D2
		[__DynamicallyInvokable]
		void ICollection<!0>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00007FDC File Offset: 0x000061DC
		[__DynamicallyInvokable]
		bool ICollection<!0>.Contains(T item)
		{
			if (this._array == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
			}
			int num = System.Array.IndexOf<T>(this._array, item, this._offset, this._count);
			return num >= 0;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00008021 File Offset: 0x00006221
		[__DynamicallyInvokable]
		void ICollection<!0>.CopyTo(T[] array, int arrayIndex)
		{
			if (this._array == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
			}
			System.Array.Copy(this._array, this._offset, array, arrayIndex, this._count);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00008054 File Offset: 0x00006254
		[__DynamicallyInvokable]
		bool ICollection<!0>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000805B File Offset: 0x0000625B
		[__DynamicallyInvokable]
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			if (this._array == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
			}
			return new ArraySegment<T>.ArraySegmentEnumerator(this);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00008080 File Offset: 0x00006280
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			if (this._array == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
			}
			return new ArraySegment<T>.ArraySegmentEnumerator(this);
		}

		// Token: 0x040001F0 RID: 496
		private T[] _array;

		// Token: 0x040001F1 RID: 497
		private int _offset;

		// Token: 0x040001F2 RID: 498
		private int _count;

		// Token: 0x02000A99 RID: 2713
		[Serializable]
		private sealed class ArraySegmentEnumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x060068A9 RID: 26793 RVA: 0x00167F98 File Offset: 0x00166198
			internal ArraySegmentEnumerator(ArraySegment<T> arraySegment)
			{
				this._array = arraySegment._array;
				this._start = arraySegment._offset;
				this._end = this._start + arraySegment._count;
				this._current = this._start - 1;
			}

			// Token: 0x060068AA RID: 26794 RVA: 0x00167FE4 File Offset: 0x001661E4
			public bool MoveNext()
			{
				if (this._current < this._end)
				{
					this._current++;
					return this._current < this._end;
				}
				return false;
			}

			// Token: 0x170011D4 RID: 4564
			// (get) Token: 0x060068AB RID: 26795 RVA: 0x00168014 File Offset: 0x00166214
			public T Current
			{
				get
				{
					if (this._current < this._start)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					if (this._current >= this._end)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
					}
					return this._array[this._current];
				}
			}

			// Token: 0x170011D5 RID: 4565
			// (get) Token: 0x060068AC RID: 26796 RVA: 0x0016806E File Offset: 0x0016626E
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060068AD RID: 26797 RVA: 0x0016807B File Offset: 0x0016627B
			void IEnumerator.Reset()
			{
				this._current = this._start - 1;
			}

			// Token: 0x060068AE RID: 26798 RVA: 0x0016808B File Offset: 0x0016628B
			public void Dispose()
			{
			}

			// Token: 0x04003005 RID: 12293
			private T[] _array;

			// Token: 0x04003006 RID: 12294
			private int _start;

			// Token: 0x04003007 RID: 12295
			private int _end;

			// Token: 0x04003008 RID: 12296
			private int _current;
		}
	}
}
