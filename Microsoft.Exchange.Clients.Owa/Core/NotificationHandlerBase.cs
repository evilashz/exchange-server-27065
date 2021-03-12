using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200025A RID: 602
	internal abstract class NotificationHandlerBase : DisposeTrackableBase
	{
		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x0007B8BE File Offset: 0x00079ABE
		internal MailboxSession MailboxSession
		{
			get
			{
				return this.mailboxSession;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001439 RID: 5177 RVA: 0x0007B8C6 File Offset: 0x00079AC6
		// (set) Token: 0x0600143A RID: 5178 RVA: 0x0007B8CE File Offset: 0x00079ACE
		internal bool MissedNotifications
		{
			get
			{
				return this.missedNotifications;
			}
			set
			{
				this.missedNotifications = value;
			}
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0007B8D7 File Offset: 0x00079AD7
		public NotificationHandlerBase(UserContext userContext, MailboxSession mailboxSession)
		{
			this.userContext = userContext;
			this.mailboxSession = mailboxSession;
			this.syncRoot = new object();
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0007B8F8 File Offset: 0x00079AF8
		internal virtual void Subscribe()
		{
			try
			{
				this.userContext.Lock();
				this.InitSubscription();
			}
			catch (OwaLockTimeoutException ex)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "User context lock timed out in Subscribe. Exception: {0}", ex.Message);
			}
			finally
			{
				if (this.userContext.LockedByCurrentThread())
				{
					this.userContext.Unlock();
				}
			}
		}

		// Token: 0x0600143D RID: 5181
		internal abstract void HandleNotification(Notification notif);

		// Token: 0x0600143E RID: 5182
		internal abstract void HandlePendingGetTimerCallback();

		// Token: 0x0600143F RID: 5183
		protected abstract void InitSubscription();

		// Token: 0x06001440 RID: 5184 RVA: 0x0007B970 File Offset: 0x00079B70
		internal virtual void HandleConnectionDroppedNotification(Notification notification)
		{
			lock (this.syncRoot)
			{
				if (!this.isDisposed)
				{
					this.needReinitSubscriptions = true;
				}
			}
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0007B9BC File Offset: 0x00079BBC
		protected override void InternalDispose(bool isDisposing)
		{
			bool flag = false;
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<bool>((long)this.GetHashCode(), "NotificationHandlerBase.Dispose. IsDisposing: {0}", isDisposing);
			lock (this.syncRoot)
			{
				if (this.isDisposed)
				{
					return;
				}
				if (isDisposing)
				{
					this.isDisposed = true;
					flag = true;
				}
			}
			if (flag)
			{
				bool flag3 = false;
				try
				{
					this.userContext.Lock();
					if (!this.mailboxSession.IsConnected)
					{
						Utilities.ReconnectStoreSession(this.mailboxSession, this.userContext);
						flag3 = true;
					}
					this.DisposeInternal();
				}
				catch (Exception ex)
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "Unexpected exception in NotificationHandlerBase Dispose. Exception: {0}", ex.Message);
				}
				finally
				{
					if (flag3)
					{
						Utilities.DisconnectStoreSessionSafe(this.mailboxSession);
					}
					if (this.userContext.LockedByCurrentThread())
					{
						this.userContext.Unlock();
					}
				}
			}
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x0007BAC4 File Offset: 0x00079CC4
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<NotificationHandlerBase>(this);
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x0007BACC File Offset: 0x00079CCC
		internal virtual void DisposeInternal()
		{
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("User context needs to be locked while this operation is called");
			}
			this.DisposeInternal(false);
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x0007BAF0 File Offset: 0x00079CF0
		internal virtual void DisposeInternal(bool doNotDisposeQueryResult)
		{
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("User context needs to be locked while this operation is called");
			}
			if (!doNotDisposeQueryResult && this.result != null)
			{
				OwaMapiNotificationHandler.DisposeXSOObjects(this.result);
				this.result = null;
			}
			if (this.mapiSubscription != null)
			{
				OwaMapiNotificationHandler.DisposeXSOObjects(this.mapiSubscription);
				this.mapiSubscription = null;
			}
		}

		// Token: 0x04000DD3 RID: 3539
		protected Subscription mapiSubscription;

		// Token: 0x04000DD4 RID: 3540
		protected MailboxSession mailboxSession;

		// Token: 0x04000DD5 RID: 3541
		protected UserContext userContext;

		// Token: 0x04000DD6 RID: 3542
		protected QueryResult result;

		// Token: 0x04000DD7 RID: 3543
		protected object syncRoot;

		// Token: 0x04000DD8 RID: 3544
		protected bool isDisposed;

		// Token: 0x04000DD9 RID: 3545
		protected bool missedNotifications;

		// Token: 0x04000DDA RID: 3546
		protected bool needReinitSubscriptions;
	}
}
