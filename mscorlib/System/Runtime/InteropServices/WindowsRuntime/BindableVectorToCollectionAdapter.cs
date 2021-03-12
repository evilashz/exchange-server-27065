using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009B0 RID: 2480
	internal sealed class BindableVectorToCollectionAdapter
	{
		// Token: 0x0600633D RID: 25405 RVA: 0x001516D8 File Offset: 0x0014F8D8
		private BindableVectorToCollectionAdapter()
		{
		}

		// Token: 0x0600633E RID: 25406 RVA: 0x001516E0 File Offset: 0x0014F8E0
		[SecurityCritical]
		internal int Count()
		{
			IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>(this);
			uint size = bindableVector.Size;
			if (2147483647U < size)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
			}
			return (int)size;
		}

		// Token: 0x0600633F RID: 25407 RVA: 0x00151714 File Offset: 0x0014F914
		[SecurityCritical]
		internal bool IsSynchronized()
		{
			return false;
		}

		// Token: 0x06006340 RID: 25408 RVA: 0x00151717 File Offset: 0x0014F917
		[SecurityCritical]
		internal object SyncRoot()
		{
			return this;
		}

		// Token: 0x06006341 RID: 25409 RVA: 0x0015171C File Offset: 0x0014F91C
		[SecurityCritical]
		internal void CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
			}
			int lowerBound = array.GetLowerBound(0);
			int num = this.Count();
			int length = array.GetLength(0);
			if (arrayIndex < lowerBound)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			if (num > length - (arrayIndex - lowerBound))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InsufficientSpaceToCopyCollection"));
			}
			if (arrayIndex - lowerBound > length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IndexOutOfArrayBounds"));
			}
			IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>(this);
			uint num2 = 0U;
			while ((ulong)num2 < (ulong)((long)num))
			{
				array.SetValue(bindableVector.GetAt(num2), (long)((ulong)num2 + (ulong)((long)arrayIndex)));
				num2 += 1U;
			}
		}
	}
}
