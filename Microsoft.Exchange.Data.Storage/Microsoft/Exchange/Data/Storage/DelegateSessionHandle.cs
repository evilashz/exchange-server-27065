using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000691 RID: 1681
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DelegateSessionHandle : IDisposeTrackable, IDisposable
	{
		// Token: 0x060044D5 RID: 17621 RVA: 0x00125369 File Offset: 0x00123569
		internal DelegateSessionHandle(DelegateSessionEntry entry)
		{
			this.entry = entry;
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x060044D6 RID: 17622 RVA: 0x00125384 File Offset: 0x00123584
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<DelegateSessionHandle>(this);
		}

		// Token: 0x060044D7 RID: 17623 RVA: 0x0012538C File Offset: 0x0012358C
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x17001402 RID: 5122
		// (get) Token: 0x060044D8 RID: 17624 RVA: 0x001253A1 File Offset: 0x001235A1
		public MailboxSession MailboxSession
		{
			get
			{
				this.CheckDisposed("MailboxSession::get.");
				return this.entry.MailboxSession;
			}
		}

		// Token: 0x060044D9 RID: 17625 RVA: 0x001253B9 File Offset: 0x001235B9
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060044DA RID: 17626 RVA: 0x001253C8 File Offset: 0x001235C8
		private void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				this.InternalDispose(disposing);
				this.isDisposed = true;
			}
		}

		// Token: 0x060044DB RID: 17627 RVA: 0x001253E0 File Offset: 0x001235E0
		private void InternalDispose(bool disposing)
		{
			StorageGlobals.TraceDispose(this, this.isDisposed, disposing);
			if (disposing)
			{
				if (!this.isDisposed)
				{
					this.entry.DecrementExternalRefCount();
				}
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
				}
			}
		}

		// Token: 0x060044DC RID: 17628 RVA: 0x00125419 File Offset: 0x00123619
		private void CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				StorageGlobals.TraceFailedCheckDisposed(this, methodName);
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x04002573 RID: 9587
		public const int DelegateSessionCacheCapacity = 6;

		// Token: 0x04002574 RID: 9588
		private readonly DelegateSessionEntry entry;

		// Token: 0x04002575 RID: 9589
		private bool isDisposed;

		// Token: 0x04002576 RID: 9590
		private readonly DisposeTracker disposeTracker;
	}
}
