using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Notifications.Broker;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200000F RID: 15
	internal class NewMailNotificationHandler : MapiNotificationHandlerBase
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00004A2C File Offset: 0x00002C2C
		public NewMailNotificationHandler(string name, MailboxSessionContext sessionContext, BaseSubscription parameters) : base(name, sessionContext)
		{
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00004A80 File Offset: 0x00002C80
		internal StoreObjectId InboxFolderId
		{
			get
			{
				if (this.inboxFolderId == null)
				{
					base.SessionContext.DoOperationUnderSessionLock(delegate(MailboxSession session)
					{
						this.inboxFolderId = session.GetDefaultFolderId(DefaultFolderType.Inbox);
					});
				}
				return this.inboxFolderId;
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004ABC File Offset: 0x00002CBC
		internal override void HandleNotificationInternal(Notification notif, object context)
		{
			ExTraceGlobals.GeneratorTracer.TraceDebug((long)this.GetHashCode(), "NewMailNotificationHandler.HandleNotificationInternal start");
			BrokerLogger.Set(LogField.NotificationType, notif.GetType().Name);
			NewMailNotification newMailNotification = notif as NewMailNotification;
			if (newMailNotification == null)
			{
				ExTraceGlobals.GeneratorTracer.TraceError((long)this.GetHashCode(), "notification is of the wrong type");
				BrokerLogger.Set(LogField.RejectReason, "WrongType");
				return;
			}
			if (newMailNotification.NewMailItemId == null)
			{
				ExTraceGlobals.GeneratorTracer.TraceDebug((long)this.GetHashCode(), "notification has a null notifying item id");
				BrokerLogger.Set(LogField.RejectReason, "NullItemId");
				return;
			}
			StoreObjectId parentFolderId = newMailNotification.ParentFolderId;
			if (parentFolderId == null || newMailNotification.NewMailItemId == null)
			{
				ExTraceGlobals.GeneratorTracer.TraceDebug((long)this.GetHashCode(), "notification has a null notifying item id");
				BrokerLogger.Set(LogField.RejectReason, "NullParentFolderId");
				return;
			}
			if (!parentFolderId.Equals(this.InboxFolderId))
			{
				ExTraceGlobals.GeneratorTracer.TraceDebug((long)this.GetHashCode(), "notification was not for inbox folder");
				BrokerLogger.Set(LogField.RejectReason, "NotInbox");
				return;
			}
			NewMailNotification newMailNotification2 = this.BindToItemAndCreatePayload(newMailNotification);
			if (newMailNotification2 != null)
			{
				base.SendPayloadsToQueue(newMailNotification2);
				return;
			}
			ExTraceGlobals.GeneratorTracer.TraceDebug((long)this.GetHashCode(), "no payload was generated");
			BrokerLogger.Set(LogField.RejectReason, "NoPayload");
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004C10 File Offset: 0x00002E10
		internal override void KeepAliveInternal()
		{
			lock (base.SyncRoot)
			{
				base.InitSubscription();
				if (base.NeedRefreshPayload)
				{
					base.SendPayloadsToQueue((BrokerSubscription brokerSubscription) => new NewMailNotification
					{
						EventType = QueryNotificationType.Reload,
						ConsumerSubscriptionId = brokerSubscription.Parameters.ConsumerSubscriptionId
					});
					base.NeedRefreshPayload = false;
				}
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004C84 File Offset: 0x00002E84
		protected override void InitSubscriptionInternal(MailboxSession session)
		{
			if (!base.SessionContext.MailboxSessionLockedByCurrentThread())
			{
				throw new InvalidOperationException("SessionContext lock should be acquired before calling this method NewMailNotificationHandler.InitSubscriptionInternal");
			}
			ExTraceGlobals.GeneratorTracer.TraceDebug((long)this.GetHashCode(), "Creating XSO Subscription");
			base.Subscription = Subscription.CreateMailboxSubscription(session, new NotificationHandler(base.HandleNotification), NotificationType.NewMail);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004EB4 File Offset: 0x000030B4
		protected NewMailNotification BindToItemAndCreatePayload(NewMailNotification notification)
		{
			NewMailNotification payload = null;
			base.SessionContext.DoOperationUnderSessionLock(delegate(MailboxSession session)
			{
				MessageItem newMessage = null;
				payload = new NewMailNotification();
				try
				{
					newMessage = Item.BindAsMessage(session, notification.NewMailItemId, this.GetAdditionalPropsToLoad());
					if (newMessage != null)
					{
						ExchangeVersion.ExecuteWithSpecifiedVersion(ExchangeVersion.Exchange2012, delegate
						{
							payload.ItemId = IdConverter.ConvertStoreItemIdToItemId(newMessage.Id, session).Id;
							payload.ConversationId = IdConverter.ConversationIdToEwsId(session.MailboxGuid, newMessage.GetConversation(new PropertyDefinition[0]).ConversationId);
						});
						if (newMessage.From != null && newMessage.From.DisplayName != null)
						{
							payload.Sender = newMessage.From.DisplayName;
						}
						if (newMessage.Subject != null)
						{
							payload.Subject = newMessage.Subject;
						}
						string previewText = newMessage.Body.PreviewText;
						if (previewText != null)
						{
							payload.PreviewText = previewText;
						}
						this.OnPayloadCreated(newMessage, payload);
					}
				}
				catch (ObjectNotFoundException)
				{
					payload = null;
				}
				finally
				{
					if (newMessage != null)
					{
						newMessage.Dispose();
					}
				}
			});
			return payload;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004EF9 File Offset: 0x000030F9
		protected virtual PropertyDefinition[] GetAdditionalPropsToLoad()
		{
			return null;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004EFC File Offset: 0x000030FC
		protected virtual void OnPayloadCreated(MessageItem newMessage, NewMailNotification payload)
		{
		}

		// Token: 0x04000052 RID: 82
		private PropertyDefinition[] querySubscriptionProperties = new PropertyDefinition[]
		{
			FolderSchema.Id,
			StoreObjectSchema.DisplayName,
			FolderSchema.ItemCount,
			FolderSchema.UnreadCount
		};

		// Token: 0x04000053 RID: 83
		private StoreObjectId inboxFolderId;
	}
}
