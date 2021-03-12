using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Utils
{
	// Token: 0x02000044 RID: 68
	internal abstract class PushNotificationDisposable : IDisposeTrackable, IDisposable
	{
		// Token: 0x060001AC RID: 428 RVA: 0x0000591D File Offset: 0x00003B1D
		public PushNotificationDisposable()
		{
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00005931 File Offset: 0x00003B31
		public bool IsDisposed
		{
			get
			{
				return this.isDisposed;
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00005939 File Offset: 0x00003B39
		public DisposeTracker GetDisposeTracker()
		{
			return this.InternalGetDisposeTracker();
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00005941 File Offset: 0x00003B41
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00005956 File Offset: 0x00003B56
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001B1 RID: 433
		protected abstract void InternalDispose(bool disposing);

		// Token: 0x060001B2 RID: 434
		protected abstract DisposeTracker InternalGetDisposeTracker();

		// Token: 0x060001B3 RID: 435 RVA: 0x00005965 File Offset: 0x00003B65
		protected void CheckDisposed()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00005980 File Offset: 0x00003B80
		private void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				if (disposing && this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
					this.disposeTracker = null;
				}
				this.InternalDispose(disposing);
			}
		}

		// Token: 0x04000098 RID: 152
		private bool isDisposed;

		// Token: 0x04000099 RID: 153
		private DisposeTracker disposeTracker;
	}
}
