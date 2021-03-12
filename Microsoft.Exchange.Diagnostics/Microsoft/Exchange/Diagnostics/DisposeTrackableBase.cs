using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200001D RID: 29
	[CLSCompliant(true)]
	public abstract class DisposeTrackableBase : IForceReportDisposeTrackable, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00003199 File Offset: 0x00001399
		public DisposeTrackableBase()
		{
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000031AD File Offset: 0x000013AD
		public bool IsDisposed
		{
			get
			{
				return this.disposed;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000031B5 File Offset: 0x000013B5
		public DisposeTracker DisposeTracker
		{
			get
			{
				return this.disposeTracker;
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000031BD File Offset: 0x000013BD
		public virtual void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000031D2 File Offset: 0x000013D2
		public void ForceLeakReport()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.AddExtraDataWithStackTrace("Force leak was called");
			}
			this.disposeTracker = null;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000031F3 File Offset: 0x000013F3
		public virtual void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003202 File Offset: 0x00001402
		public DisposeTracker GetDisposeTracker()
		{
			return this.InternalGetDisposeTracker();
		}

		// Token: 0x0600006F RID: 111
		protected abstract DisposeTracker InternalGetDisposeTracker();

		// Token: 0x06000070 RID: 112
		protected abstract void InternalDispose(bool disposing);

		// Token: 0x06000071 RID: 113 RVA: 0x0000320A File Offset: 0x0000140A
		protected void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003225 File Offset: 0x00001425
		protected void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing && this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
					this.disposeTracker = null;
				}
				this.InternalDispose(disposing);
				this.disposed = true;
			}
		}

		// Token: 0x04000080 RID: 128
		private bool disposed;

		// Token: 0x04000081 RID: 129
		private DisposeTracker disposeTracker;
	}
}
