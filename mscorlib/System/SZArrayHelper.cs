using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System
{
	// Token: 0x02000056 RID: 86
	internal sealed class SZArrayHelper
	{
		// Token: 0x0600030C RID: 780 RVA: 0x00007BCD File Offset: 0x00005DCD
		private SZArrayHelper()
		{
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00007BD8 File Offset: 0x00005DD8
		[SecuritySafeCritical]
		internal IEnumerator<T> GetEnumerator<T>()
		{
			T[] array = JitHelpers.UnsafeCast<T[]>(this);
			int num = array.Length;
			if (num != 0)
			{
				return new SZArrayHelper.SZGenericArrayEnumerator<T>(array, num);
			}
			return SZArrayHelper.SZGenericArrayEnumerator<T>.Empty;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00007C00 File Offset: 0x00005E00
		[SecuritySafeCritical]
		private void CopyTo<T>(T[] array, int index)
		{
			if (array != null && array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Rank_MultiDimNotSupported"));
			}
			T[] array2 = JitHelpers.UnsafeCast<T[]>(this);
			Array.Copy(array2, 0, array, index, array2.Length);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00007C3C File Offset: 0x00005E3C
		[SecuritySafeCritical]
		internal int get_Count<T>()
		{
			T[] array = JitHelpers.UnsafeCast<T[]>(this);
			return array.Length;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00007C54 File Offset: 0x00005E54
		[SecuritySafeCritical]
		internal T get_Item<T>(int index)
		{
			T[] array = JitHelpers.UnsafeCast<T[]>(this);
			if (index >= array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return array[index];
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00007C7C File Offset: 0x00005E7C
		[SecuritySafeCritical]
		internal void set_Item<T>(int index, T value)
		{
			T[] array = JitHelpers.UnsafeCast<T[]>(this);
			if (index >= array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			array[index] = value;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00007CA3 File Offset: 0x00005EA3
		private void Add<T>(T value)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00007CB4 File Offset: 0x00005EB4
		[SecuritySafeCritical]
		private bool Contains<T>(T value)
		{
			T[] array = JitHelpers.UnsafeCast<T[]>(this);
			return Array.IndexOf<T>(array, value) != -1;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00007CD5 File Offset: 0x00005ED5
		private bool get_IsReadOnly<T>()
		{
			return true;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00007CD8 File Offset: 0x00005ED8
		private void Clear<T>()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00007CEC File Offset: 0x00005EEC
		[SecuritySafeCritical]
		private int IndexOf<T>(T value)
		{
			T[] array = JitHelpers.UnsafeCast<T[]>(this);
			return Array.IndexOf<T>(array, value);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00007D07 File Offset: 0x00005F07
		private void Insert<T>(int index, T value)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00007D18 File Offset: 0x00005F18
		private bool Remove<T>(T value)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00007D29 File Offset: 0x00005F29
		private void RemoveAt<T>(int index)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
		}

		// Token: 0x02000A98 RID: 2712
		[Serializable]
		private sealed class SZGenericArrayEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x060068A2 RID: 26786 RVA: 0x00167ECE File Offset: 0x001660CE
			internal SZGenericArrayEnumerator(T[] array, int endIndex)
			{
				this._array = array;
				this._index = -1;
				this._endIndex = endIndex;
			}

			// Token: 0x060068A3 RID: 26787 RVA: 0x00167EEB File Offset: 0x001660EB
			public bool MoveNext()
			{
				if (this._index < this._endIndex)
				{
					this._index++;
					return this._index < this._endIndex;
				}
				return false;
			}

			// Token: 0x170011D2 RID: 4562
			// (get) Token: 0x060068A4 RID: 26788 RVA: 0x00167F1C File Offset: 0x0016611C
			public T Current
			{
				get
				{
					if (this._index < 0)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					if (this._index >= this._endIndex)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
					}
					return this._array[this._index];
				}
			}

			// Token: 0x170011D3 RID: 4563
			// (get) Token: 0x060068A5 RID: 26789 RVA: 0x00167F71 File Offset: 0x00166171
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060068A6 RID: 26790 RVA: 0x00167F7E File Offset: 0x0016617E
			void IEnumerator.Reset()
			{
				this._index = -1;
			}

			// Token: 0x060068A7 RID: 26791 RVA: 0x00167F87 File Offset: 0x00166187
			public void Dispose()
			{
			}

			// Token: 0x04003001 RID: 12289
			private T[] _array;

			// Token: 0x04003002 RID: 12290
			private int _index;

			// Token: 0x04003003 RID: 12291
			private int _endIndex;

			// Token: 0x04003004 RID: 12292
			internal static readonly SZArrayHelper.SZGenericArrayEnumerator<T> Empty = new SZArrayHelper.SZGenericArrayEnumerator<T>(null, -1);
		}
	}
}
