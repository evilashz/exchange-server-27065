using System;
using System.Threading;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000841 RID: 2113
	internal abstract class ReaderWriterLockScopeBase : IDisposable
	{
		// Token: 0x06004961 RID: 18785 RVA: 0x0012DE6C File Offset: 0x0012C06C
		protected ReaderWriterLockScopeBase(ReaderWriterLockSlim readerWriterLock)
		{
			this.readerWriterLock = readerWriterLock;
		}

		// Token: 0x17001618 RID: 5656
		// (get) Token: 0x06004962 RID: 18786 RVA: 0x0012DE7B File Offset: 0x0012C07B
		protected ReaderWriterLockSlim ScopedReaderWriterLock
		{
			get
			{
				return this.readerWriterLock;
			}
		}

		// Token: 0x17001619 RID: 5657
		// (get) Token: 0x06004963 RID: 18787 RVA: 0x0012DE83 File Offset: 0x0012C083
		protected bool IsDisposed
		{
			get
			{
				return this.disposed;
			}
		}

		// Token: 0x06004964 RID: 18788
		protected abstract void Acquire();

		// Token: 0x06004965 RID: 18789
		protected abstract void Release();

		// Token: 0x06004966 RID: 18790 RVA: 0x0012DE8C File Offset: 0x0012C08C
		~ReaderWriterLockScopeBase()
		{
			this.Dispose(false);
		}

		// Token: 0x06004967 RID: 18791 RVA: 0x0012DEBC File Offset: 0x0012C0BC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06004968 RID: 18792 RVA: 0x0012DECB File Offset: 0x0012C0CB
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.disposed = true;
				if (disposing)
				{
					this.Release();
				}
			}
		}

		// Token: 0x04002C47 RID: 11335
		private bool disposed;

		// Token: 0x04002C48 RID: 11336
		private readonly ReaderWriterLockSlim readerWriterLock;
	}
}
