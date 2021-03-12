using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200003B RID: 59
	internal abstract class BaseObject : IDisposeTrackable, IDisposable
	{
		// Token: 0x06000121 RID: 289 RVA: 0x00004CD0 File Offset: 0x00002ED0
		protected BaseObject()
		{
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004CE4 File Offset: 0x00002EE4
		protected virtual void InternalDispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004CF9 File Offset: 0x00002EF9
		protected void CheckDisposed()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString() + " has already been disposed.");
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00004D1E File Offset: 0x00002F1E
		protected bool IsDisposed
		{
			get
			{
				return this.isDisposed;
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004D26 File Offset: 0x00002F26
		public void Dispose()
		{
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.InternalDispose();
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004D43 File Offset: 0x00002F43
		DisposeTracker IDisposeTrackable.GetDisposeTracker()
		{
			return this.GetDisposeTracker();
		}

		// Token: 0x06000127 RID: 295
		protected abstract DisposeTracker GetDisposeTracker();

		// Token: 0x06000128 RID: 296 RVA: 0x00004D4B File Offset: 0x00002F4B
		void IDisposeTrackable.SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x040000BC RID: 188
		private bool isDisposed;

		// Token: 0x040000BD RID: 189
		private DisposeTracker disposeTracker;
	}
}
