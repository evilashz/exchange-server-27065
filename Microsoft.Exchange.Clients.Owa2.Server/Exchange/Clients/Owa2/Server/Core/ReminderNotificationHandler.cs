using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001A8 RID: 424
	internal class ReminderNotificationHandler : MapiNotificationHandlerBase
	{
		// Token: 0x06000F2D RID: 3885 RVA: 0x0003AE2E File Offset: 0x0003902E
		public ReminderNotificationHandler(string subscriptionId, IMailboxContext userContext) : base(subscriptionId, userContext, false)
		{
			this.reminderNotifier = new ReminderNotifier(subscriptionId, userContext);
			this.reminderNotifier.RegisterWithPendingRequestNotifier();
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x0003AE54 File Offset: 0x00039054
		internal override void HandleNotificationInternal(Notification notif, MapiNotificationsLogEvent logEvent, object context)
		{
			if (notif == null)
			{
				return;
			}
			ReminderNotificationPayload reminderNotificationPayload = new ReminderNotificationPayload(true);
			reminderNotificationPayload.SubscriptionId = base.SubscriptionId;
			reminderNotificationPayload.Source = MailboxLocation.FromMailboxContext(base.UserContext);
			this.reminderNotifier.AddGetRemindersPayload(reminderNotificationPayload);
			this.reminderNotifier.PickupData();
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x0003AEA0 File Offset: 0x000390A0
		internal override void HandlePendingGetTimerCallback(MapiNotificationsLogEvent logEvent)
		{
			lock (base.SyncRoot)
			{
				base.InitSubscription();
				if (base.MissedNotifications)
				{
					base.NeedRefreshPayload = true;
				}
				base.MissedNotifications = false;
			}
			if (base.NeedRefreshPayload)
			{
				ReminderNotificationPayload reminderNotificationPayload = new ReminderNotificationPayload(true);
				reminderNotificationPayload.SubscriptionId = base.SubscriptionId;
				reminderNotificationPayload.Source = MailboxLocation.FromMailboxContext(base.UserContext);
				this.reminderNotifier.AddGetRemindersPayload(reminderNotificationPayload);
				base.NeedRefreshPayload = false;
			}
			this.reminderNotifier.PickupData();
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x0003AF40 File Offset: 0x00039140
		protected override void InitSubscriptionInternal()
		{
			this.SetUpRemindersSubscription(0);
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x0003AF4C File Offset: 0x0003914C
		private void SetUpRemindersSubscription(int currentRetryCount)
		{
			if (!base.UserContext.MailboxSessionLockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method ReminderNotificationHandler.SetUpRemindersSubscription");
			}
			try
			{
				using (SearchFolder searchFolder = SearchFolder.Bind(base.UserContext.MailboxSession, DefaultFolderType.Reminders))
				{
					base.QueryResult = searchFolder.ItemQuery(ItemQueryType.None, null, ReminderNotificationHandler.sorts, ReminderNotificationHandler.querySubscriptionProperties);
					base.QueryResult.GetRows(1);
					base.Subscription = Subscription.Create(base.QueryResult, new NotificationHandler(base.HandleNotification));
				}
			}
			catch (QueryInProgressException)
			{
				if (currentRetryCount >= 5)
				{
					throw;
				}
				this.SetUpRemindersSubscription(currentRetryCount + 1);
			}
		}

		// Token: 0x0400092D RID: 2349
		private const int MaxSubscriptionSetUpRetryCount = 5;

		// Token: 0x0400092E RID: 2350
		private static readonly PropertyDefinition[] querySubscriptionProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			ItemSchema.ReminderIsSet,
			ItemSchema.ReminderNextTime,
			ItemSchema.ReminderDueBy
		};

		// Token: 0x0400092F RID: 2351
		private static readonly SortBy[] sorts = new SortBy[]
		{
			new SortBy(ItemSchema.ReminderIsSet, SortOrder.Descending),
			new SortBy(ItemSchema.ReminderNextTime, SortOrder.Descending)
		};

		// Token: 0x04000930 RID: 2352
		private ReminderNotifier reminderNotifier;
	}
}
