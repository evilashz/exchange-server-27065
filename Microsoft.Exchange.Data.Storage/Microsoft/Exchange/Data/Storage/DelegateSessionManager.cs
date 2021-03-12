using System;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000690 RID: 1680
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class DelegateSessionManager : IDisposeTrackable, IDisposable
	{
		// Token: 0x060044CB RID: 17611 RVA: 0x0012516C File Offset: 0x0012336C
		internal DelegateSessionManager(MailboxSession masterMailboxSession)
		{
			this.masterMailboxSession = masterMailboxSession;
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x060044CC RID: 17612 RVA: 0x00125187 File Offset: 0x00123387
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<DelegateSessionManager>(this);
		}

		// Token: 0x060044CD RID: 17613 RVA: 0x0012518F File Offset: 0x0012338F
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060044CE RID: 17614 RVA: 0x001251A4 File Offset: 0x001233A4
		internal void SetTimeZone(ExTimeZone exTimeZone)
		{
			foreach (DelegateSessionEntry delegateSessionEntry in this.DelegateSessionCacheInstance)
			{
				delegateSessionEntry.MailboxSession.ExTimeZone = exTimeZone;
			}
		}

		// Token: 0x060044CF RID: 17615 RVA: 0x001251F8 File Offset: 0x001233F8
		internal DelegateSessionEntry GetDelegateSessionEntry(IExchangePrincipal principal, OpenBy openBy)
		{
			MailboxSession mailboxSession = null;
			DelegateSessionEntry delegateSessionEntry = null;
			bool flag = false;
			try
			{
				if (!this.DelegateSessionCacheInstance.TryGet(principal, openBy, out delegateSessionEntry))
				{
					mailboxSession = MailboxSession.InternalOpenDelegateAccess(this.masterMailboxSession, principal);
					delegateSessionEntry = this.DelegateSessionCacheInstance.Add(new DelegateSessionEntry(mailboxSession, openBy));
				}
				if (!delegateSessionEntry.IsConnected)
				{
					delegateSessionEntry.Connect();
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					if (delegateSessionEntry != null)
					{
						this.DelegateSessionCacheInstance.RemoveEntry(delegateSessionEntry);
					}
					else if (mailboxSession != null)
					{
						mailboxSession.CanDispose = true;
						mailboxSession.Dispose();
						mailboxSession = null;
					}
				}
			}
			return delegateSessionEntry;
		}

		// Token: 0x060044D0 RID: 17616 RVA: 0x00125288 File Offset: 0x00123488
		internal void DisconnectAll()
		{
			if (this.delegateSessionCache != null)
			{
				foreach (DelegateSessionEntry delegateSessionEntry in this.delegateSessionCache)
				{
					if (!delegateSessionEntry.MailboxSession.IsDisposed && delegateSessionEntry.IsConnected)
					{
						delegateSessionEntry.Disconnect();
					}
				}
			}
		}

		// Token: 0x060044D1 RID: 17617 RVA: 0x001252F4 File Offset: 0x001234F4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060044D2 RID: 17618 RVA: 0x00125303 File Offset: 0x00123503
		private void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.InternalDispose(disposing);
			}
		}

		// Token: 0x060044D3 RID: 17619 RVA: 0x0012531B File Offset: 0x0012351B
		private void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.delegateSessionCache != null)
				{
					this.delegateSessionCache.Clear();
				}
				this.delegateSessionCache = null;
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
				}
			}
		}

		// Token: 0x17001401 RID: 5121
		// (get) Token: 0x060044D4 RID: 17620 RVA: 0x0012534D File Offset: 0x0012354D
		private DelegateSessionCache DelegateSessionCacheInstance
		{
			get
			{
				if (this.delegateSessionCache == null)
				{
					this.delegateSessionCache = new DelegateSessionCache(6);
				}
				return this.delegateSessionCache;
			}
		}

		// Token: 0x0400256F RID: 9583
		private bool isDisposed;

		// Token: 0x04002570 RID: 9584
		private readonly MailboxSession masterMailboxSession;

		// Token: 0x04002571 RID: 9585
		private DelegateSessionCache delegateSessionCache;

		// Token: 0x04002572 RID: 9586
		private readonly DisposeTracker disposeTracker;
	}
}
