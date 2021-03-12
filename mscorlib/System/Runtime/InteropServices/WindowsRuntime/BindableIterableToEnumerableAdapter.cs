using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009C2 RID: 2498
	internal sealed class BindableIterableToEnumerableAdapter
	{
		// Token: 0x0600639A RID: 25498 RVA: 0x00152611 File Offset: 0x00150811
		private BindableIterableToEnumerableAdapter()
		{
		}

		// Token: 0x0600639B RID: 25499 RVA: 0x0015261C File Offset: 0x0015081C
		[SecurityCritical]
		internal IEnumerator GetEnumerator_Stub()
		{
			IBindableIterable bindableIterable = JitHelpers.UnsafeCast<IBindableIterable>(this);
			return new IteratorToEnumeratorAdapter<object>(new BindableIterableToEnumerableAdapter.NonGenericToGenericIterator(bindableIterable.First()));
		}

		// Token: 0x02000C6F RID: 3183
		private sealed class NonGenericToGenericIterator : IIterator<object>
		{
			// Token: 0x0600700B RID: 28683 RVA: 0x00180BFD File Offset: 0x0017EDFD
			public NonGenericToGenericIterator(IBindableIterator iterator)
			{
				this.iterator = iterator;
			}

			// Token: 0x1700134F RID: 4943
			// (get) Token: 0x0600700C RID: 28684 RVA: 0x00180C0C File Offset: 0x0017EE0C
			public object Current
			{
				get
				{
					return this.iterator.Current;
				}
			}

			// Token: 0x17001350 RID: 4944
			// (get) Token: 0x0600700D RID: 28685 RVA: 0x00180C19 File Offset: 0x0017EE19
			public bool HasCurrent
			{
				get
				{
					return this.iterator.HasCurrent;
				}
			}

			// Token: 0x0600700E RID: 28686 RVA: 0x00180C26 File Offset: 0x0017EE26
			public bool MoveNext()
			{
				return this.iterator.MoveNext();
			}

			// Token: 0x0600700F RID: 28687 RVA: 0x00180C33 File Offset: 0x0017EE33
			public int GetMany(object[] items)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0400379E RID: 14238
			private IBindableIterator iterator;
		}
	}
}
