using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000026 RID: 38
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class DisposableObject : IDisposeTrackable, IDisposable
	{
		// Token: 0x06000366 RID: 870 RVA: 0x0001D07C File Offset: 0x0001B27C
		protected DisposableObject()
		{
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0001D090 File Offset: 0x0001B290
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0001D09F File Offset: 0x0001B29F
		protected virtual void InternalDispose(bool disposing)
		{
			if (disposing && this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0001D0B8 File Offset: 0x0001B2B8
		protected virtual void CheckDisposed(string methodName = null)
		{
			if (this.isDisposed)
			{
				if (methodName == null)
				{
					methodName = new StackTrace(1).GetFrame(0).GetMethod().Name;
				}
				StorageGlobals.TraceFailedCheckDisposed(this, methodName);
				throw new ObjectDisposedException(base.GetType().FullName + " has already been disposed.");
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0001D10A File Offset: 0x0001B30A
		public DisposeTracker DisposeTracker
		{
			get
			{
				return this.disposeTracker;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0001D112 File Offset: 0x0001B312
		protected bool IsDisposed
		{
			get
			{
				return this.isDisposed;
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0001D11A File Offset: 0x0001B31A
		private void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.InternalDispose(disposing);
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0001D132 File Offset: 0x0001B332
		DisposeTracker IDisposeTrackable.GetDisposeTracker()
		{
			return this.GetDisposeTracker();
		}

		// Token: 0x0600036E RID: 878
		protected abstract DisposeTracker GetDisposeTracker();

		// Token: 0x0600036F RID: 879 RVA: 0x0001D13A File Offset: 0x0001B33A
		void IDisposeTrackable.SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x040000EF RID: 239
		private readonly DisposeTracker disposeTracker;

		// Token: 0x040000F0 RID: 240
		private bool isDisposed;
	}
}
