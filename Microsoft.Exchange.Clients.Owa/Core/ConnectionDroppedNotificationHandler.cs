using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000F6 RID: 246
	internal sealed class ConnectionDroppedNotificationHandler : DisposeTrackableBase
	{
		// Token: 0x0600082E RID: 2094 RVA: 0x0003CCC0 File Offset: 0x0003AEC0
		public ConnectionDroppedNotificationHandler(UserContext userContext, MailboxSession mailboxSession)
		{
			this.userContext = userContext;
			this.mailboxSession = mailboxSession;
			ConnectionDroppedPayload connectionDroppedPayload = new ConnectionDroppedPayload(userContext, mailboxSession, this);
			connectionDroppedPayload.RegisterWithPendingRequestNotifier();
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600082F RID: 2095 RVA: 0x0003CCF0 File Offset: 0x0003AEF0
		// (remove) Token: 0x06000830 RID: 2096 RVA: 0x0003CD28 File Offset: 0x0003AF28
		internal event ConnectionDroppedNotificationHandler.ConnectionDroppedEventHandler OnConnectionDropped;

		// Token: 0x06000831 RID: 2097 RVA: 0x0003CD60 File Offset: 0x0003AF60
		protected override void InternalDispose(bool isDisposing)
		{
			bool flag = false;
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<bool>((long)this.GetHashCode(), "ConnectionDroppedNotificationHandler.Dispose. IsDisposing: {0}", isDisposing);
			lock (this)
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
				this.DisposeInternal();
			}
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0003CDD0 File Offset: 0x0003AFD0
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ConnectionDroppedNotificationHandler>(this);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0003CDD8 File Offset: 0x0003AFD8
		public void Subscribe()
		{
			this.InitSubscription();
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0003CDE0 File Offset: 0x0003AFE0
		internal void HandleNotification(Notification notification)
		{
			lock (this)
			{
				if (this.isDisposed || this.needReinitSubscription)
				{
					return;
				}
				this.needReinitSubscription = true;
			}
			if (this.OnConnectionDropped != null)
			{
				this.OnConnectionDropped(notification);
			}
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0003CE44 File Offset: 0x0003B044
		internal void HandlePendingGetTimerCallback()
		{
			bool flag = false;
			lock (this)
			{
				if (this.isDisposed)
				{
					return;
				}
				flag = this.needReinitSubscription;
			}
			if (flag)
			{
				try
				{
					this.userContext.Lock();
					Utilities.ReconnectStoreSession(this.mailboxSession, this.userContext);
					lock (this)
					{
						if (this.needReinitSubscription)
						{
							this.DisposeInternal();
							this.InitSubscription();
							this.needReinitSubscription = false;
						}
					}
				}
				catch (Exception ex)
				{
					ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "Unexpected exception in pending GET timer callback thread. Exception: {0}", ex.Message);
					this.needReinitSubscription = true;
				}
				finally
				{
					if (this.userContext.LockedByCurrentThread())
					{
						Utilities.DisconnectStoreSessionSafe(this.mailboxSession);
						this.userContext.Unlock();
					}
				}
			}
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0003CF5C File Offset: 0x0003B15C
		private void InitSubscription()
		{
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			lock (this)
			{
				if (this.mapiSubscription != null)
				{
					throw new InvalidOperationException("There is an existing undisposed subscription. Dispose it before creating a new one");
				}
				this.mapiSubscription = Subscription.CreateMailboxSubscription(this.mailboxSession, new NotificationHandler(this.HandleNotification), NotificationType.ConnectionDropped);
			}
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0003CFE0 File Offset: 0x0003B1E0
		private void DisposeInternal()
		{
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("User context needs to be locked while this operation is called");
			}
			if (this.mapiSubscription != null)
			{
				OwaMapiNotificationHandler.DisposeXSOObjects(this.mapiSubscription);
				this.mapiSubscription = null;
			}
		}

		// Token: 0x040005DD RID: 1501
		private Subscription mapiSubscription;

		// Token: 0x040005DE RID: 1502
		private MailboxSession mailboxSession;

		// Token: 0x040005DF RID: 1503
		private UserContext userContext;

		// Token: 0x040005E0 RID: 1504
		private bool isDisposed;

		// Token: 0x040005E1 RID: 1505
		private bool needReinitSubscription;

		// Token: 0x020000F7 RID: 247
		// (Invoke) Token: 0x06000839 RID: 2105
		internal delegate void ConnectionDroppedEventHandler(Notification notification);
	}
}
