using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000168 RID: 360
	internal sealed class ConnectionDroppedNotificationHandler : MapiNotificationHandlerBase
	{
		// Token: 0x06000D52 RID: 3410 RVA: 0x0003218C File Offset: 0x0003038C
		public ConnectionDroppedNotificationHandler(IMailboxContext userContext) : base(userContext, false)
		{
			ConnectionDroppedNotifier connectionDroppedNotifier = new ConnectionDroppedNotifier(userContext);
			connectionDroppedNotifier.RegisterWithPendingRequestNotifier();
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000D53 RID: 3411 RVA: 0x000321B0 File Offset: 0x000303B0
		// (remove) Token: 0x06000D54 RID: 3412 RVA: 0x000321E8 File Offset: 0x000303E8
		internal event ConnectionDroppedNotificationHandler.ConnectionDroppedEventHandler OnConnectionDropped;

		// Token: 0x06000D55 RID: 3413 RVA: 0x00032220 File Offset: 0x00030420
		internal override void HandleNotificationInternal(Notification notification, MapiNotificationsLogEvent logEvent, object context)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "ConnectionDroppedEventHandler.HandleNotification Begin.");
			lock (base.SyncRoot)
			{
				base.NeedToReinitSubscriptions = true;
			}
			if (this.OnConnectionDropped != null)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "ConnectionDroppedEventHandler.HandleNotification Call OnConnectionDropped.");
				this.OnConnectionDropped(notification);
			}
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x000322A4 File Offset: 0x000304A4
		internal override void HandlePendingGetTimerCallback(MapiNotificationsLogEvent logEvent)
		{
			bool flag = false;
			ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "ConnectionDroppedEventHandler.HandlePendingGetTimerCallback Begin.");
			lock (base.SyncRoot)
			{
				flag = base.NeedToReinitSubscriptions;
			}
			if (flag)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "ConnectionDroppedEventHandler.HandlePendingGetTimerCallback Need to reinit true.");
				try
				{
					lock (base.SyncRoot)
					{
						if (base.NeedToReinitSubscriptions)
						{
							base.InitSubscription();
							ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "ConnectionDroppedEventHandler.HandlePendingGetTimerCallback Successful reinit .");
						}
					}
				}
				catch (Exception)
				{
					base.NeedToReinitSubscriptions = true;
					throw;
				}
			}
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00032380 File Offset: 0x00030580
		protected override void InitSubscriptionInternal()
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "ConnectionDroppedEventHandler.InitSubscriptionInternal Begin.");
			if (!base.UserContext.MailboxSessionLockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			lock (base.SyncRoot)
			{
				if (base.Subscription != null)
				{
					throw new InvalidOperationException("There is an existing undisposed subscription. Dispose it before creating a new one");
				}
				base.Subscription = Subscription.CreateMailboxSubscription(base.UserContext.MailboxSession, new NotificationHandler(base.HandleNotification), NotificationType.ConnectionDropped);
				ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "ConnectionDroppedEventHandler.InitSubscriptionInternal Subscription created.");
			}
		}

		// Token: 0x02000169 RID: 361
		// (Invoke) Token: 0x06000D59 RID: 3417
		internal delegate void ConnectionDroppedEventHandler(Notification notification);
	}
}
