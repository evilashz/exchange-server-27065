using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009C3 RID: 2499
	internal sealed class IteratorToEnumeratorAdapter<T> : IEnumerator<!0>, IDisposable, IEnumerator
	{
		// Token: 0x0600639C RID: 25500 RVA: 0x00152640 File Offset: 0x00150840
		internal IteratorToEnumeratorAdapter(IIterator<T> iterator)
		{
			this.m_iterator = iterator;
			this.m_hadCurrent = true;
			this.m_isInitialized = false;
		}

		// Token: 0x17001140 RID: 4416
		// (get) Token: 0x0600639D RID: 25501 RVA: 0x0015265D File Offset: 0x0015085D
		public T Current
		{
			get
			{
				if (!this.m_isInitialized)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumNotStarted);
				}
				if (!this.m_hadCurrent)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumEnded);
				}
				return this.m_current;
			}
		}

		// Token: 0x17001141 RID: 4417
		// (get) Token: 0x0600639E RID: 25502 RVA: 0x00152683 File Offset: 0x00150883
		object IEnumerator.Current
		{
			get
			{
				if (!this.m_isInitialized)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumNotStarted);
				}
				if (!this.m_hadCurrent)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumEnded);
				}
				return this.m_current;
			}
		}

		// Token: 0x0600639F RID: 25503 RVA: 0x001526B0 File Offset: 0x001508B0
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			if (!this.m_hadCurrent)
			{
				return false;
			}
			try
			{
				if (!this.m_isInitialized)
				{
					this.m_hadCurrent = this.m_iterator.HasCurrent;
					this.m_isInitialized = true;
				}
				else
				{
					this.m_hadCurrent = this.m_iterator.MoveNext();
				}
				if (this.m_hadCurrent)
				{
					this.m_current = this.m_iterator.Current;
				}
			}
			catch (Exception e)
			{
				if (Marshal.GetHRForException(e) != -2147483636)
				{
					throw;
				}
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
			}
			return this.m_hadCurrent;
		}

		// Token: 0x060063A0 RID: 25504 RVA: 0x00152748 File Offset: 0x00150948
		public void Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060063A1 RID: 25505 RVA: 0x0015274F File Offset: 0x0015094F
		public void Dispose()
		{
		}

		// Token: 0x04002C3D RID: 11325
		private IIterator<T> m_iterator;

		// Token: 0x04002C3E RID: 11326
		private bool m_hadCurrent;

		// Token: 0x04002C3F RID: 11327
		private T m_current;

		// Token: 0x04002C40 RID: 11328
		private bool m_isInitialized;
	}
}
