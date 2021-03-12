using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200017C RID: 380
	internal class NewMailNotificationHandler : MapiNotificationHandlerBase
	{
		// Token: 0x06000DD4 RID: 3540 RVA: 0x0003454C File Offset: 0x0003274C
		public NewMailNotificationHandler(string subscriptionId, IMailboxContext userContext) : base(subscriptionId, userContext, false)
		{
			this.newMailNotifier = new NewMailNotifier(subscriptionId, userContext);
			this.newMailNotifier.RegisterWithPendingRequestNotifier();
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x000345A8 File Offset: 0x000327A8
		internal StoreObjectId InboxFolderId
		{
			get
			{
				if (this.inboxFolderId == null)
				{
					try
					{
						base.UserContext.LockAndReconnectMailboxSession(3000);
						this.inboxFolderId = base.UserContext.MailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox);
					}
					catch (OwaLockTimeoutException ex)
					{
						ExTraceGlobals.CoreCallTracer.TraceError<string>((long)this.GetHashCode(), "User context lock timed out in trying to get InboxFolderId. Exception: {0}", ex.Message);
					}
					finally
					{
						if (base.UserContext.MailboxSessionLockedByCurrentThread())
						{
							base.UserContext.UnlockAndDisconnectMailboxSession();
						}
					}
				}
				return this.inboxFolderId;
			}
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x00034644 File Offset: 0x00032844
		internal override void HandleNotificationInternal(Notification notif, MapiNotificationsLogEvent logEvent, object context)
		{
			NewMailNotification newMailNotification = notif as NewMailNotification;
			if (newMailNotification == null)
			{
				return;
			}
			if (newMailNotification.NewMailItemId == null || newMailNotification.ParentFolderId == null)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug((long)this.GetHashCode(), "notification has a null notifying item id");
				return;
			}
			StoreObjectId parentFolderId = newMailNotification.ParentFolderId;
			if (parentFolderId == null || newMailNotification.NewMailItemId == null)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug((long)this.GetHashCode(), "notification has a null notifying item id");
				return;
			}
			if (!parentFolderId.Equals(this.InboxFolderId))
			{
				return;
			}
			NewMailNotificationPayload newMailNotificationPayload = this.BindToItemAndCreatePayload(newMailNotification);
			if (newMailNotificationPayload != null)
			{
				this.newMailNotifier.Payload = newMailNotificationPayload;
				this.newMailNotifier.PickupData();
			}
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x000346E0 File Offset: 0x000328E0
		internal override void HandlePendingGetTimerCallback(MapiNotificationsLogEvent logEvent)
		{
			lock (base.SyncRoot)
			{
				base.InitSubscription();
			}
			this.newMailNotifier.PickupData();
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0003472C File Offset: 0x0003292C
		protected override void InitSubscriptionInternal()
		{
			if (!base.UserContext.MailboxSessionLockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method NewMailNotificationHandler.InitSubscriptionInternal");
			}
			base.Subscription = Subscription.CreateMailboxSubscription(base.UserContext.MailboxSession, new NotificationHandler(base.HandleNotification), NotificationType.NewMail);
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x000347EC File Offset: 0x000329EC
		protected NewMailNotificationPayload BindToItemAndCreatePayload(NewMailNotification notification)
		{
			NewMailNotificationPayload result;
			try
			{
				base.UserContext.LockAndReconnectMailboxSession(3000);
				MessageItem newMessage = null;
				NewMailNotificationPayload payload = new NewMailNotificationPayload();
				payload.Source = MailboxLocation.FromMailboxContext(base.UserContext);
				payload.SubscriptionId = base.SubscriptionId;
				try
				{
					newMessage = Item.BindAsMessage(base.UserContext.MailboxSession, notification.NewMailItemId, this.GetAdditionalPropsToLoad());
					ExchangeVersion.ExecuteWithSpecifiedVersion(ExchangeVersion.Exchange2012, delegate
					{
						payload.ItemId = IdConverter.ConvertStoreItemIdToItemId(newMessage.Id, this.UserContext.MailboxSession).Id;
						payload.ConversationId = IdConverter.ConversationIdToEwsId(this.UserContext.MailboxSession.MailboxGuid, newMessage.GetConversation(new PropertyDefinition[0]).ConversationId);
					});
					if (newMessage != null)
					{
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
						result = payload;
					}
					else
					{
						result = null;
					}
				}
				catch (ObjectNotFoundException)
				{
					result = null;
				}
				finally
				{
					if (newMessage != null)
					{
						newMessage.Dispose();
					}
				}
			}
			finally
			{
				if (base.UserContext.MailboxSessionLockedByCurrentThread())
				{
					base.UserContext.UnlockAndDisconnectMailboxSession();
				}
			}
			return result;
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x000349BC File Offset: 0x00032BBC
		protected virtual PropertyDefinition[] GetAdditionalPropsToLoad()
		{
			return null;
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x000349BF File Offset: 0x00032BBF
		protected virtual void OnPayloadCreated(MessageItem newMessage, NewMailNotificationPayload payload)
		{
		}

		// Token: 0x04000864 RID: 2148
		private PropertyDefinition[] querySubscriptionProperties = new PropertyDefinition[]
		{
			FolderSchema.Id,
			StoreObjectSchema.DisplayName,
			FolderSchema.ItemCount,
			FolderSchema.UnreadCount
		};

		// Token: 0x04000865 RID: 2149
		private NewMailNotifier newMailNotifier;

		// Token: 0x04000866 RID: 2150
		private StoreObjectId inboxFolderId;
	}
}
