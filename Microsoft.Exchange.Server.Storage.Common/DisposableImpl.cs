using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000030 RID: 48
	public struct DisposableImpl<T> where T : class, IDisposableImpl
	{
		// Token: 0x06000402 RID: 1026 RVA: 0x0000B571 File Offset: 0x00009771
		public DisposableImpl(T disposable)
		{
			this.disposeTracker = disposable.InternalGetDisposeTracker();
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x0000B586 File Offset: 0x00009786
		public bool IsDisposed
		{
			get
			{
				return object.ReferenceEquals(this.disposeTracker, DisposableImpl<T>.Disposed);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0000B598 File Offset: 0x00009798
		public bool IsDisposing
		{
			get
			{
				return object.ReferenceEquals(this.disposeTracker, DisposableImpl<T>.Disposing);
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000B5AA File Offset: 0x000097AA
		public void DisposeImpl(T disposable)
		{
			this.Dispose(disposable, true);
			GC.SuppressFinalize(disposable);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000B5BF File Offset: 0x000097BF
		public void FinalizeImpl(T disposable)
		{
			this.Dispose(disposable, false);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000B5CC File Offset: 0x000097CC
		public void SuppressTracking()
		{
			IDisposable disposable = this.disposeTracker;
			if (disposable != null && !object.ReferenceEquals(disposable, DisposableImpl<T>.Disposing) && !object.ReferenceEquals(disposable, DisposableImpl<T>.Disposed))
			{
				((DisposeTracker)disposable).Suppress();
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000B608 File Offset: 0x00009808
		public void CheckDisposed(T disposable)
		{
			if (object.ReferenceEquals(this.disposeTracker, DisposableImpl<T>.Disposed))
			{
				throw new ObjectDisposedException(disposable.GetType().ToString());
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000B634 File Offset: 0x00009834
		private void Dispose(T disposable, bool calledFromDispose)
		{
			IDisposable disposable2 = Interlocked.Exchange<IDisposable>(ref this.disposeTracker, DisposableImpl<T>.Disposing);
			if (!object.ReferenceEquals(disposable2, DisposableImpl<T>.Disposing))
			{
				try
				{
					if (!object.ReferenceEquals(disposable2, DisposableImpl<T>.Disposed))
					{
						if (calledFromDispose && disposable2 != null)
						{
							disposable2.Dispose();
							disposable2 = null;
						}
						disposable.InternalDispose(calledFromDispose);
						disposable2 = DisposableImpl<T>.Disposed;
					}
				}
				finally
				{
					Interlocked.Exchange<IDisposable>(ref this.disposeTracker, disposable2);
				}
			}
		}

		// Token: 0x040004AE RID: 1198
		private static readonly IDisposable Disposing = new DisposableImpl<T>.FakeFlag();

		// Token: 0x040004AF RID: 1199
		private static readonly IDisposable Disposed = new DisposableImpl<T>.FakeFlag();

		// Token: 0x040004B0 RID: 1200
		private IDisposable disposeTracker;

		// Token: 0x02000031 RID: 49
		private class FakeFlag : IDisposable
		{
			// Token: 0x0600040B RID: 1035 RVA: 0x0000B6C6 File Offset: 0x000098C6
			public void Dispose()
			{
			}
		}
	}
}
