using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009A5 RID: 2469
	internal sealed class EnumerableToBindableIterableAdapter
	{
		// Token: 0x060062E5 RID: 25317 RVA: 0x00150567 File Offset: 0x0014E767
		private EnumerableToBindableIterableAdapter()
		{
		}

		// Token: 0x060062E6 RID: 25318 RVA: 0x00150570 File Offset: 0x0014E770
		[SecurityCritical]
		internal IBindableIterator First_Stub()
		{
			IEnumerable enumerable = JitHelpers.UnsafeCast<IEnumerable>(this);
			return new EnumeratorToIteratorAdapter<object>(new EnumerableToBindableIterableAdapter.NonGenericToGenericEnumerator(enumerable.GetEnumerator()));
		}

		// Token: 0x02000C6E RID: 3182
		internal sealed class NonGenericToGenericEnumerator : IEnumerator<object>, IDisposable, IEnumerator
		{
			// Token: 0x06007006 RID: 28678 RVA: 0x00180BC5 File Offset: 0x0017EDC5
			public NonGenericToGenericEnumerator(IEnumerator enumerator)
			{
				this.enumerator = enumerator;
			}

			// Token: 0x1700134E RID: 4942
			// (get) Token: 0x06007007 RID: 28679 RVA: 0x00180BD4 File Offset: 0x0017EDD4
			public object Current
			{
				get
				{
					return this.enumerator.Current;
				}
			}

			// Token: 0x06007008 RID: 28680 RVA: 0x00180BE1 File Offset: 0x0017EDE1
			public bool MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			// Token: 0x06007009 RID: 28681 RVA: 0x00180BEE File Offset: 0x0017EDEE
			public void Reset()
			{
				this.enumerator.Reset();
			}

			// Token: 0x0600700A RID: 28682 RVA: 0x00180BFB File Offset: 0x0017EDFB
			public void Dispose()
			{
			}

			// Token: 0x0400379D RID: 14237
			private IEnumerator enumerator;
		}
	}
}
