using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x0200005A RID: 90
	public abstract class Disposable : IDisposeTrackable, IDisposable
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x00003ED7 File Offset: 0x000020D7
		public Disposable()
		{
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00003EEC File Offset: 0x000020EC
		~Disposable()
		{
			this.Dispose(false);
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00003F1C File Offset: 0x0000211C
		public bool IsDisposed
		{
			get
			{
				return Interlocked.CompareExchange(ref this.isDisposedFlag, 0, 0) != 0;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00003F31 File Offset: 0x00002131
		public bool IsDisposing
		{
			get
			{
				return Interlocked.CompareExchange(ref this.isDisposingFlag, 0, 0) != 0;
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00003F46 File Offset: 0x00002146
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00003F5B File Offset: 0x0000215B
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00003F6A File Offset: 0x0000216A
		public DisposeTracker GetDisposeTracker()
		{
			return this.InternalGetDisposeTracker();
		}

		// Token: 0x060001C9 RID: 457
		protected abstract DisposeTracker InternalGetDisposeTracker();

		// Token: 0x060001CA RID: 458
		protected abstract void InternalDispose(bool calledFromDispose);

		// Token: 0x060001CB RID: 459 RVA: 0x00003F72 File Offset: 0x00002172
		protected void CheckDisposed()
		{
			if (this.IsDisposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00003F90 File Offset: 0x00002190
		private void Dispose(bool calledFromDispose)
		{
			if (Interlocked.Exchange(ref this.isDisposingFlag, 1) == 0)
			{
				try
				{
					if (!this.IsDisposed)
					{
						if (calledFromDispose && this.disposeTracker != null)
						{
							this.disposeTracker.Dispose();
							this.disposeTracker = null;
						}
						this.InternalDispose(calledFromDispose);
						Interlocked.Exchange(ref this.isDisposedFlag, 1);
					}
				}
				finally
				{
					Interlocked.Exchange(ref this.isDisposingFlag, 0);
				}
			}
		}

		// Token: 0x040000AC RID: 172
		private int isDisposedFlag;

		// Token: 0x040000AD RID: 173
		private int isDisposingFlag;

		// Token: 0x040000AE RID: 174
		private DisposeTracker disposeTracker;
	}
}
