using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000009 RID: 9
	internal class Imap4FastQueryViewHandler : FastQueryView, IMapiNotificationHandler
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002C7F File Offset: 0x00000E7F
		public Imap4FastQueryViewHandler(ResponseFactory factory, Folder folder) : base(factory, folder, Imap4Message.SortBys, Imap4Message.MessageProperties)
		{
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002C93 File Offset: 0x00000E93
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002C9B File Offset: 0x00000E9B
		public MapiNotificationManager Manager { get; set; }

		// Token: 0x0600002C RID: 44 RVA: 0x00002CA4 File Offset: 0x00000EA4
		public void SubscribeNotification()
		{
			if (this.subscription != null)
			{
				throw new InvalidOperationException("Should unsubscribe first");
			}
			this.subscription = Subscription.Create(base.TableView, new NotificationHandler(this.HandleNotificationCallback));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002CD6 File Offset: 0x00000ED6
		public void UnsubscribeNotification()
		{
			if (this.subscription != null)
			{
				this.subscription.Dispose();
				this.subscription = null;
			}
			if (this.Manager != null)
			{
				this.Manager.UnregisterHandler(this);
				this.Manager = null;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002D10 File Offset: 0x00000F10
		public void ProcessNotification(Notification notification)
		{
			QueryNotification queryNotification = notification as QueryNotification;
			QueryNotificationType eventType = queryNotification.EventType;
			switch (eventType)
			{
			case QueryNotificationType.QueryResultChanged:
			case QueryNotificationType.Error:
				break;
			case QueryNotificationType.RowAdded:
				goto IL_8C;
			case QueryNotificationType.RowDeleted:
			{
				int docIdFromInstanceKey = ProtocolMessage.GetDocIdFromInstanceKey(queryNotification.Index);
				this.Manager.Factory.SelectedMailbox.DeleteMessage(docIdFromInstanceKey);
				return;
			}
			default:
				if (eventType != QueryNotificationType.Reload)
				{
					goto IL_8C;
				}
				break;
			}
			ProtocolBaseServices.SessionTracer.TraceDebug(this.Manager.Factory.Session.SessionId, "Start reload all views and notification handlers.");
			this.Manager.Factory.ReloadAllNotificationHandlers();
			this.reloadNeeded = false;
			return;
			IL_8C:
			throw new InvalidOperationException("Unexpected table notification type: " + queryNotification.EventType);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002DC4 File Offset: 0x00000FC4
		protected override void InternalDispose(bool isDisposing)
		{
			try
			{
				this.UnsubscribeNotification();
			}
			catch (LocalizedException ex)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug<string>(this.Manager.Factory.Session.SessionId, "Exception caught while disposing unsubscribing fastQueryView notifications. {0}", ex.ToString());
			}
			finally
			{
				base.InternalDispose(isDisposing);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002E2C File Offset: 0x0000102C
		private void HandleNotificationCallback(Notification notification)
		{
			MapiNotificationManager manager = this.Manager;
			try
			{
				Trace sessionTracer = ProtocolBaseServices.SessionTracer;
				if (sessionTracer != null)
				{
					sessionTracer.TraceDebug<string>((manager == null) ? 0L : manager.Factory.Session.SessionId, "User {0} entering Imap4FastQueryViewHandler.HandleNotificationCallback.", (manager == null) ? "(Unknown user)" : manager.Factory.Session.GetUserNameForLogging());
				}
				if (notification.Type != NotificationType.Query)
				{
					throw new NotSupportedException("Not supported notification type: " + notification.Type);
				}
				if (!this.reloadNeeded)
				{
					if (manager != null)
					{
						QueryNotification queryNotification = notification as QueryNotification;
						QueryNotificationType eventType = queryNotification.EventType;
						switch (eventType)
						{
						case QueryNotificationType.QueryResultChanged:
						case QueryNotificationType.Error:
							break;
						case QueryNotificationType.RowAdded:
							goto IL_D1;
						case QueryNotificationType.RowDeleted:
							manager.EnqueueNotification(notification, this);
							return;
						default:
							if (eventType != QueryNotificationType.Reload)
							{
								goto IL_D1;
							}
							break;
						}
						manager.EnqueueNotification(notification, this);
						this.reloadNeeded = true;
						IL_D1:;
					}
				}
			}
			finally
			{
				ProtocolBaseServices.InMemoryTraceOperationCompleted((manager == null) ? 0L : manager.Factory.Session.SessionId);
			}
		}

		// Token: 0x04000011 RID: 17
		private Subscription subscription;

		// Token: 0x04000012 RID: 18
		private bool reloadNeeded;
	}
}
