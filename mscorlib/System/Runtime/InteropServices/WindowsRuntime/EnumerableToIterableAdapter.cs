using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009A4 RID: 2468
	internal sealed class EnumerableToIterableAdapter
	{
		// Token: 0x060062E3 RID: 25315 RVA: 0x0015053F File Offset: 0x0014E73F
		private EnumerableToIterableAdapter()
		{
		}

		// Token: 0x060062E4 RID: 25316 RVA: 0x00150548 File Offset: 0x0014E748
		[SecurityCritical]
		internal IIterator<T> First_Stub<T>()
		{
			IEnumerable<T> enumerable = JitHelpers.UnsafeCast<IEnumerable<T>>(this);
			return new EnumeratorToIteratorAdapter<T>(enumerable.GetEnumerator());
		}
	}
}
