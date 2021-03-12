using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200073E RID: 1854
	internal class DelegateSessionHandleWrapper : IDisposeTrackable, IDisposable
	{
		// Token: 0x060037CA RID: 14282 RVA: 0x000C5D1A File Offset: 0x000C3F1A
		public DelegateSessionHandleWrapper(DelegateSessionHandle sessionHandle)
		{
			this.handle = sessionHandle;
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x060037CB RID: 14283 RVA: 0x000C5D35 File Offset: 0x000C3F35
		public DelegateSessionHandle Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x060037CC RID: 14284 RVA: 0x000C5D3D File Offset: 0x000C3F3D
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<DelegateSessionHandleWrapper>(this);
		}

		// Token: 0x060037CD RID: 14285 RVA: 0x000C5D45 File Offset: 0x000C3F45
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060037CE RID: 14286 RVA: 0x000C5D5A File Offset: 0x000C3F5A
		public void Dispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			this.Dispose(true);
		}

		// Token: 0x060037CF RID: 14287 RVA: 0x000C5D76 File Offset: 0x000C3F76
		private void Dispose(bool isDisposing)
		{
			if (!this.disposed)
			{
				this.disposed = true;
				if (this.handle != null)
				{
					this.handle.Dispose();
					this.handle = null;
				}
				if (isDisposing)
				{
					GC.SuppressFinalize(this);
				}
			}
		}

		// Token: 0x04001F07 RID: 7943
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04001F08 RID: 7944
		private DelegateSessionHandle handle;

		// Token: 0x04001F09 RID: 7945
		private bool disposed;
	}
}
