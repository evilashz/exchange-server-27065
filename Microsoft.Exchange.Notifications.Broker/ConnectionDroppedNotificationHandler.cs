using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000009 RID: 9
	internal sealed class ConnectionDroppedNotificationHandler : MapiNotificationHandlerBase
	{
		// Token: 0x06000053 RID: 83 RVA: 0x0000336B File Offset: 0x0000156B
		public ConnectionDroppedNotificationHandler(string name, MailboxSessionContext sessionContext, Action<Notification> connectionDroppedCallback) : base(name, sessionContext)
		{
			ArgumentValidator.ThrowIfNull("name", name);
			ArgumentValidator.ThrowIfNull("sessionContext", sessionContext);
			ArgumentValidator.ThrowIfNull("connectionDroppedCallback", connectionDroppedCallback);
			this.connectionDroppedCallback = connectionDroppedCallback;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000339D File Offset: 0x0000159D
		internal override void HandleNotificationInternal(Notification notification, object context)
		{
			this.connectionDroppedCallback(notification);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000033AC File Offset: 0x000015AC
		internal override void KeepAliveInternal()
		{
			lock (base.SyncRoot)
			{
				if (base.WasConnectionDropped)
				{
					base.InitSubscription();
				}
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000033F4 File Offset: 0x000015F4
		protected override void InitSubscriptionInternal(MailboxSession session)
		{
			if (!base.SessionContext.MailboxSessionLockedByCurrentThread())
			{
				throw new InvalidOperationException("SessionContext lock should be acquired before calling this method");
			}
			lock (base.SyncRoot)
			{
				if (base.Subscription != null)
				{
					throw new InvalidOperationException("There is an existing undisposed subscription. Dispose it before creating a new one");
				}
				base.Subscription = Subscription.CreateMailboxSubscription(session, new NotificationHandler(base.HandleNotification), NotificationType.ConnectionDropped);
			}
		}

		// Token: 0x0400003D RID: 61
		private Action<Notification> connectionDroppedCallback;

		// Token: 0x0200000A RID: 10
		// (Invoke) Token: 0x06000058 RID: 88
		internal delegate void ConnectionDroppedEventHandler(Notification notification);
	}
}
