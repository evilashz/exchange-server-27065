using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009A9 RID: 2473
	internal sealed class VectorViewToReadOnlyCollectionAdapter
	{
		// Token: 0x060062FF RID: 25343 RVA: 0x00150A37 File Offset: 0x0014EC37
		private VectorViewToReadOnlyCollectionAdapter()
		{
		}

		// Token: 0x06006300 RID: 25344 RVA: 0x00150A40 File Offset: 0x0014EC40
		[SecurityCritical]
		internal int Count<T>()
		{
			IVectorView<T> vectorView = JitHelpers.UnsafeCast<IVectorView<T>>(this);
			uint size = vectorView.Size;
			if (2147483647U < size)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
			}
			return (int)size;
		}
	}
}
