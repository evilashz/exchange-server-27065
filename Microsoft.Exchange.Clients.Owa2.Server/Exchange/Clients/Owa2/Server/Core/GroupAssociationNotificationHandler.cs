using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000173 RID: 371
	internal class GroupAssociationNotificationHandler : MapiNotificationHandlerBase
	{
		// Token: 0x06000DAC RID: 3500 RVA: 0x000335BE File Offset: 0x000317BE
		internal GroupAssociationNotificationHandler(string subscriptionId, IMailboxContext userContext, IRecipientSession adSession) : base(subscriptionId, userContext, false)
		{
			this.adSession = adSession;
			this.notifier = new GroupAssociationNotifier(subscriptionId, base.UserContext);
			this.notifier.RegisterWithPendingRequestNotifier();
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x000335F0 File Offset: 0x000317F0
		internal override void HandleNotificationInternal(Notification notification, MapiNotificationsLogEvent logEvent, object context)
		{
			QueryNotification queryNotification = notification as QueryNotification;
			if (queryNotification == null)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<SmtpAddress, string>((long)this.GetHashCode(), "GroupAssociationNotificationHandler.HandleNotificationInternal: Received a null QueryNotification object for user {0} SubscriptionId: {1}", base.UserContext.PrimarySmtpAddress, base.SubscriptionId);
				logEvent.NullNotification = true;
				return;
			}
			GroupAssociationNotificationPayload payloadFromNotification = this.GetPayloadFromNotification(queryNotification);
			lock (base.SyncRoot)
			{
				this.notifier.AddGroupAssociationNotificationPayload(payloadFromNotification);
				this.notifier.PickupData();
			}
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00033684 File Offset: 0x00031884
		internal override void HandlePendingGetTimerCallback(MapiNotificationsLogEvent logEvent)
		{
			lock (base.SyncRoot)
			{
				base.InitSubscription();
				if (base.MissedNotifications)
				{
					this.notifier.AddRefreshPayload();
					this.notifier.PickupData();
					base.MissedNotifications = false;
				}
			}
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x000336EC File Offset: 0x000318EC
		protected override void InitSubscriptionInternal()
		{
			if (!base.UserContext.MailboxSessionLockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling method UnseenItemNotificationHandler.InitSubscriptionInternal");
			}
			using (Folder folder = Folder.Bind(base.UserContext.MailboxSession, DefaultFolderType.MailboxAssociation))
			{
				base.QueryResult = this.GetQueryResult(folder);
				base.QueryResult.GetRows(base.QueryResult.EstimatedRowCount);
				base.Subscription = Subscription.Create(base.QueryResult, new NotificationHandler(base.HandleNotification));
			}
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "GroupAssociationNotificationHandler.InitSubscriptionInternal succeeded for subscription {0}", base.SubscriptionId);
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x000337A0 File Offset: 0x000319A0
		private static T GetItemProperty<T>(QueryNotification notification, int index, T defaultValue)
		{
			if (index >= notification.Row.Length || notification.Row[index] == null || notification.Row[index] is PropertyError)
			{
				return defaultValue;
			}
			if (notification.Row[index] is T)
			{
				return (T)((object)notification.Row[index]);
			}
			return defaultValue;
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x000337F1 File Offset: 0x000319F1
		private QueryResult GetQueryResult(Folder folder)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "GroupAssociationNotificationHandler.GetQueryResult for subscription {0}", base.SubscriptionId);
			return folder.ItemQuery(ItemQueryType.None, null, GroupAssociationNotificationHandler.MailboxAssociationSortBy, GroupAssociationNotificationHandler.MailboxAssociationQuerySubscriptionProperties);
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x00033824 File Offset: 0x00031A24
		private GroupAssociationNotificationPayload GetPayloadFromNotification(QueryNotification notification)
		{
			GroupAssociationNotificationPayload groupAssociationNotificationPayload = new GroupAssociationNotificationPayload();
			groupAssociationNotificationPayload.SubscriptionId = base.SubscriptionId;
			groupAssociationNotificationPayload.EventType = notification.EventType;
			groupAssociationNotificationPayload.Source = MailboxLocation.FromMailboxContext(base.UserContext);
			if (notification.EventType != QueryNotificationType.RowDeleted)
			{
				string itemProperty = GroupAssociationNotificationHandler.GetItemProperty<string>(notification, 0, string.Empty);
				string itemProperty2 = GroupAssociationNotificationHandler.GetItemProperty<string>(notification, 1, string.Empty);
				bool itemProperty3 = GroupAssociationNotificationHandler.GetItemProperty<bool>(notification, 2, false);
				GroupMailboxLocator groupMailboxLocator = new GroupMailboxLocator(this.adSession, itemProperty, itemProperty2);
				ADUser aduser = groupMailboxLocator.FindAdUser();
				if (aduser != null)
				{
					groupAssociationNotificationPayload.Group = new ModernGroupType
					{
						DisplayName = aduser.DisplayName,
						SmtpAddress = aduser.PrimarySmtpAddress.ToString(),
						IsPinned = itemProperty3
					};
				}
				else
				{
					ExTraceGlobals.NotificationsCallTracer.TraceError<string, string>((long)this.GetHashCode(), "GroupAssociationNotificationHandler.GetPayloadFromNotification: Could not find Group in AD with ExternalObjectId {0} or LegacyDn {1}", itemProperty, itemProperty2);
				}
			}
			return groupAssociationNotificationPayload;
		}

		// Token: 0x0400084A RID: 2122
		private static readonly PropertyDefinition[] MailboxAssociationQuerySubscriptionProperties = new PropertyDefinition[]
		{
			MailboxAssociationBaseSchema.ExternalId,
			MailboxAssociationBaseSchema.LegacyDN,
			MailboxAssociationBaseSchema.IsPin
		};

		// Token: 0x0400084B RID: 2123
		private static readonly SortBy[] MailboxAssociationSortBy = new SortBy[]
		{
			new SortBy(StoreObjectSchema.LastModifiedTime, SortOrder.Descending)
		};

		// Token: 0x0400084C RID: 2124
		private readonly IRecipientSession adSession;

		// Token: 0x0400084D RID: 2125
		protected readonly GroupAssociationNotifier notifier;
	}
}
