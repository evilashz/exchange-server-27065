using System;
using System.Threading;

namespace Microsoft.Exchange.Data.Internal
{
	// Token: 0x0200004B RID: 75
	internal abstract class RefCountable : IDisposable
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000FE80 File Offset: 0x0000E080
		protected bool IsDisposed
		{
			get
			{
				return this.isDisposed;
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000FE88 File Offset: 0x0000E088
		protected void ThrowIfDisposed()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("RefCountable");
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000FE9D File Offset: 0x0000E09D
		protected RefCountable()
		{
			this.refCount = 1;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000FEAC File Offset: 0x0000E0AC
		public int RefCount
		{
			get
			{
				return this.refCount;
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000FEB4 File Offset: 0x0000E0B4
		public void AddRef()
		{
			this.ThrowIfDisposed();
			Interlocked.Increment(ref this.refCount);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000FEC8 File Offset: 0x0000E0C8
		public void Release()
		{
			this.ThrowIfDisposed();
			if (Interlocked.Decrement(ref this.refCount) == 0)
			{
				this.Dispose();
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000FEF0 File Offset: 0x0000E0F0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000FEFF File Offset: 0x0000E0FF
		protected virtual void Dispose(bool disposing)
		{
			this.isDisposed = true;
		}

		// Token: 0x04000257 RID: 599
		private int refCount;

		// Token: 0x04000258 RID: 600
		private bool isDisposed;
	}
}
