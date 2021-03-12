using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000012 RID: 18
	public abstract class DisposableBase : IDisposableImpl, IDisposable
	{
		// Token: 0x06000220 RID: 544 RVA: 0x00004D5D File Offset: 0x00002F5D
		public DisposableBase()
		{
			this.disposableImpl = new DisposableImpl<DisposableBase>(this);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00004D74 File Offset: 0x00002F74
		~DisposableBase()
		{
			this.disposableImpl.FinalizeImpl(this);
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00004DA8 File Offset: 0x00002FA8
		public bool IsDisposed
		{
			get
			{
				return this.disposableImpl.IsDisposed;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00004DB5 File Offset: 0x00002FB5
		public bool IsDisposing
		{
			get
			{
				return this.disposableImpl.IsDisposing;
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00004DC2 File Offset: 0x00002FC2
		public void SuppressDisposeTracker()
		{
			this.disposableImpl.SuppressTracking();
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00004DCF File Offset: 0x00002FCF
		public void Dispose()
		{
			this.disposableImpl.DisposeImpl(this);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00004DDD File Offset: 0x00002FDD
		protected void CheckDisposed()
		{
			this.disposableImpl.CheckDisposed(this);
		}

		// Token: 0x06000227 RID: 551
		protected abstract DisposeTracker InternalGetDisposeTracker();

		// Token: 0x06000228 RID: 552
		protected abstract void InternalDispose(bool calledFromDispose);

		// Token: 0x06000229 RID: 553 RVA: 0x00004DEB File Offset: 0x00002FEB
		DisposeTracker IDisposableImpl.InternalGetDisposeTracker()
		{
			return this.InternalGetDisposeTracker();
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00004DF3 File Offset: 0x00002FF3
		void IDisposableImpl.InternalDispose(bool calledFromDispose)
		{
			this.InternalDispose(calledFromDispose);
		}

		// Token: 0x040002D9 RID: 729
		private DisposableImpl<DisposableBase> disposableImpl;
	}
}
