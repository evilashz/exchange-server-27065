using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009A8 RID: 2472
	internal sealed class VectorToCollectionAdapter
	{
		// Token: 0x060062F7 RID: 25335 RVA: 0x001508CC File Offset: 0x0014EACC
		private VectorToCollectionAdapter()
		{
		}

		// Token: 0x060062F8 RID: 25336 RVA: 0x001508D4 File Offset: 0x0014EAD4
		[SecurityCritical]
		internal int Count<T>()
		{
			IVector<T> vector = JitHelpers.UnsafeCast<IVector<T>>(this);
			uint size = vector.Size;
			if (2147483647U < size)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
			}
			return (int)size;
		}

		// Token: 0x060062F9 RID: 25337 RVA: 0x00150908 File Offset: 0x0014EB08
		[SecurityCritical]
		internal bool IsReadOnly<T>()
		{
			return false;
		}

		// Token: 0x060062FA RID: 25338 RVA: 0x0015090C File Offset: 0x0014EB0C
		[SecurityCritical]
		internal void Add<T>(T item)
		{
			IVector<T> vector = JitHelpers.UnsafeCast<IVector<T>>(this);
			vector.Append(item);
		}

		// Token: 0x060062FB RID: 25339 RVA: 0x00150928 File Offset: 0x0014EB28
		[SecurityCritical]
		internal void Clear<T>()
		{
			IVector<T> vector = JitHelpers.UnsafeCast<IVector<T>>(this);
			vector.Clear();
		}

		// Token: 0x060062FC RID: 25340 RVA: 0x00150944 File Offset: 0x0014EB44
		[SecurityCritical]
		internal bool Contains<T>(T item)
		{
			IVector<T> vector = JitHelpers.UnsafeCast<IVector<T>>(this);
			uint num;
			return vector.IndexOf(item, out num);
		}

		// Token: 0x060062FD RID: 25341 RVA: 0x00150964 File Offset: 0x0014EB64
		[SecurityCritical]
		internal void CopyTo<T>(T[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			if (array.Length <= arrayIndex && this.Count<T>() > 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IndexOutOfArrayBounds"));
			}
			if (array.Length - arrayIndex < this.Count<T>())
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InsufficientSpaceToCopyCollection"));
			}
			IVector<T> this2 = JitHelpers.UnsafeCast<IVector<T>>(this);
			int num = this.Count<T>();
			for (int i = 0; i < num; i++)
			{
				array[i + arrayIndex] = VectorToListAdapter.GetAt<T>(this2, (uint)i);
			}
		}

		// Token: 0x060062FE RID: 25342 RVA: 0x001509F4 File Offset: 0x0014EBF4
		[SecurityCritical]
		internal bool Remove<T>(T item)
		{
			IVector<T> vector = JitHelpers.UnsafeCast<IVector<T>>(this);
			uint num;
			if (!vector.IndexOf(item, out num))
			{
				return false;
			}
			if (2147483647U < num)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
			}
			VectorToListAdapter.RemoveAtHelper<T>(vector, num);
			return true;
		}
	}
}
