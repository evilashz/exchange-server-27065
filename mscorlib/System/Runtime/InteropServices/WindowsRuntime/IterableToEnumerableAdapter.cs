using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using System.StubHelpers;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009C1 RID: 2497
	internal sealed class IterableToEnumerableAdapter
	{
		// Token: 0x06006397 RID: 25495 RVA: 0x00152597 File Offset: 0x00150797
		private IterableToEnumerableAdapter()
		{
		}

		// Token: 0x06006398 RID: 25496 RVA: 0x001525A0 File Offset: 0x001507A0
		[SecurityCritical]
		internal IEnumerator<T> GetEnumerator_Stub<T>()
		{
			IIterable<T> iterable = JitHelpers.UnsafeCast<IIterable<T>>(this);
			return new IteratorToEnumeratorAdapter<T>(iterable.First());
		}

		// Token: 0x06006399 RID: 25497 RVA: 0x001525C0 File Offset: 0x001507C0
		[SecurityCritical]
		internal IEnumerator<T> GetEnumerator_Variance_Stub<T>() where T : class
		{
			bool flag;
			Delegate targetForAmbiguousVariantCall = StubHelpers.GetTargetForAmbiguousVariantCall(this, typeof(IEnumerable<T>).TypeHandle.Value, out flag);
			if (targetForAmbiguousVariantCall != null)
			{
				return JitHelpers.UnsafeCast<GetEnumerator_Delegate<T>>(targetForAmbiguousVariantCall)();
			}
			if (flag)
			{
				return JitHelpers.UnsafeCast<IEnumerator<T>>(this.GetEnumerator_Stub<string>());
			}
			return this.GetEnumerator_Stub<T>();
		}
	}
}
