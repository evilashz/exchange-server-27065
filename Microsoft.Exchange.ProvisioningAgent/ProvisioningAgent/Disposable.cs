using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x0200001C RID: 28
	public abstract class Disposable : IDisposeTrackable, IDisposable
	{
		// Token: 0x060000DD RID: 221 RVA: 0x00005B99 File Offset: 0x00003D99
		public Disposable()
		{
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005BB0 File Offset: 0x00003DB0
		~Disposable()
		{
			this.Dispose(false);
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00005BE0 File Offset: 0x00003DE0
		public bool IsDisposed
		{
			get
			{
				return Interlocked.CompareExchange(ref this.isDisposedFlag, 0, 0) != 0;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00005BF5 File Offset: 0x00003DF5
		public bool IsDisposing
		{
			get
			{
				return Interlocked.CompareExchange(ref this.isDisposingFlag, 0, 0) != 0;
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005C0A File Offset: 0x00003E0A
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005C1F File Offset: 0x00003E1F
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005C2E File Offset: 0x00003E2E
		public DisposeTracker GetDisposeTracker()
		{
			return this.InternalGetDisposeTracker();
		}

		// Token: 0x060000E4 RID: 228
		protected abstract DisposeTracker InternalGetDisposeTracker();

		// Token: 0x060000E5 RID: 229
		protected abstract void InternalDispose(bool calledFromDispose);

		// Token: 0x060000E6 RID: 230 RVA: 0x00005C36 File Offset: 0x00003E36
		protected void CheckDisposed()
		{
			if (this.IsDisposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005C54 File Offset: 0x00003E54
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

		// Token: 0x0400007C RID: 124
		private int isDisposedFlag;

		// Token: 0x0400007D RID: 125
		private int isDisposingFlag;

		// Token: 0x0400007E RID: 126
		private DisposeTracker disposeTracker;
	}
}
