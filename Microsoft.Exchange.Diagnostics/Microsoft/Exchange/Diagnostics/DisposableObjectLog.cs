using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001B1 RID: 433
	internal class DisposableObjectLog<T> : ObjectLog<T>, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000C06 RID: 3078 RVA: 0x0002C430 File Offset: 0x0002A630
		public DisposableObjectLog(ObjectLogSchema schema, ObjectLogConfiguration configuration) : base(schema, configuration)
		{
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x0002C43A File Offset: 0x0002A63A
		public bool IsDisposed
		{
			get
			{
				return this.disposed;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x0002C442 File Offset: 0x0002A642
		public DisposeTracker DisposeTracker
		{
			get
			{
				return this.disposeTracker;
			}
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0002C44A File Offset: 0x0002A64A
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0002C45F File Offset: 0x0002A65F
		public void ForceLeakReport()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.AddExtraDataWithStackTrace("Force leak was called");
			}
			this.disposeTracker = null;
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0002C480 File Offset: 0x0002A680
		public virtual void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0002C48F File Offset: 0x0002A68F
		public DisposeTracker GetDisposeTracker()
		{
			return this.InternalGetDisposeTracker();
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0002C497 File Offset: 0x0002A697
		protected void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0002C4B2 File Offset: 0x0002A6B2
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

		// Token: 0x06000C0F RID: 3087 RVA: 0x0002C4E7 File Offset: 0x0002A6E7
		protected virtual void InternalDispose(bool disposing)
		{
			base.CloseLog();
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0002C4EF File Offset: 0x0002A6EF
		protected virtual DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<DisposableObjectLog<T>>(this);
		}

		// Token: 0x040008C3 RID: 2243
		private bool disposed;

		// Token: 0x040008C4 RID: 2244
		private DisposeTracker disposeTracker;
	}
}
