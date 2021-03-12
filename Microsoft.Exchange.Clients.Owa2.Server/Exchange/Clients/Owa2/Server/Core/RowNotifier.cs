using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001AC RID: 428
	internal class RowNotifier : PendingRequestNotifierBase
	{
		// Token: 0x06000F4A RID: 3914 RVA: 0x0003B225 File Offset: 0x00039425
		public RowNotifier(string subscriptionId, IMailboxContext context, Guid mailboxGuid) : base(subscriptionId, context)
		{
			this.queue = new Queue<NotificationPayloadBase>();
			this.mailboxGuid = mailboxGuid;
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000F4B RID: 3915 RVA: 0x0003B241 File Offset: 0x00039441
		protected Queue<NotificationPayloadBase> Queue
		{
			get
			{
				return this.queue;
			}
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x0003B24C File Offset: 0x0003944C
		internal void AddFolderRefreshPayload(StoreObjectId folderId, string subscriptionId)
		{
			RowNotificationPayload rowNotificationPayload = new RowNotificationPayload();
			rowNotificationPayload.FolderId = StoreId.StoreIdToEwsId(this.mailboxGuid, folderId);
			rowNotificationPayload.EventType = QueryNotificationType.Reload;
			rowNotificationPayload.SubscriptionId = subscriptionId;
			rowNotificationPayload.Source = new MailboxLocation(this.mailboxGuid);
			NotificationStatisticsManager.Instance.NotificationDropped(this.queue, NotificationState.CreatedOrReceived);
			this.ClearRowNotificationPayload();
			this.queue.Enqueue(rowNotificationPayload);
			NotificationStatisticsManager.Instance.NotificationCreated(rowNotificationPayload);
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x0003B2C0 File Offset: 0x000394C0
		internal void AddQueryResultChangedPayload(StoreObjectId folderId, string subscriptionId)
		{
			RowNotificationPayload rowNotificationPayload = new RowNotificationPayload();
			rowNotificationPayload.FolderId = StoreId.StoreIdToEwsId(this.mailboxGuid, folderId);
			rowNotificationPayload.EventType = QueryNotificationType.QueryResultChanged;
			rowNotificationPayload.SubscriptionId = subscriptionId;
			rowNotificationPayload.Source = new MailboxLocation(this.mailboxGuid);
			this.queue.Enqueue(rowNotificationPayload);
			NotificationStatisticsManager.Instance.NotificationCreated(rowNotificationPayload);
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x0003B31C File Offset: 0x0003951C
		internal void AddFolderContentChangePayload(StoreObjectId folderId, NotificationPayloadBase payload)
		{
			lock (this)
			{
				if (this.queue != null && this.queue.Count >= 40)
				{
					NotificationStatisticsManager.Instance.NotificationCreated(payload);
					NotificationStatisticsManager.Instance.NotificationDropped(payload, NotificationState.CreatedOrReceived);
					this.AddFolderRefreshPayload(folderId, payload.SubscriptionId);
				}
				else
				{
					this.queue.Enqueue(payload);
					NotificationStatisticsManager.Instance.NotificationCreated(payload);
				}
			}
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x0003B3A8 File Offset: 0x000395A8
		protected override IList<NotificationPayloadBase> ReadDataAndResetStateInternal()
		{
			List<NotificationPayloadBase> list = new List<NotificationPayloadBase>();
			if (this.queue.Count > 0)
			{
				foreach (NotificationPayloadBase item in this.queue)
				{
					list.Add(item);
				}
				this.queue.Clear();
			}
			return list;
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x0003B41C File Offset: 0x0003961C
		protected override bool IsDataAvailableForPickup()
		{
			return this.AreThereNotifications();
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0003B424 File Offset: 0x00039624
		private bool AreThereNotifications()
		{
			return this.queue.Count > 0;
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0003B438 File Offset: 0x00039638
		private void ClearRowNotificationPayload()
		{
			lock (this)
			{
				if (this.queue != null)
				{
					this.queue.Clear();
				}
			}
		}

		// Token: 0x0400093A RID: 2362
		protected const int MaxFolderContentNotificationQueueSize = 40;

		// Token: 0x0400093B RID: 2363
		private readonly Guid mailboxGuid;

		// Token: 0x0400093C RID: 2364
		private Queue<NotificationPayloadBase> queue;
	}
}
