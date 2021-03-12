using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000175 RID: 373
	internal class GroupAssociationNotifier : PendingRequestNotifierBase
	{
		// Token: 0x06000DB7 RID: 3511 RVA: 0x0003396E File Offset: 0x00031B6E
		internal GroupAssociationNotifier(string subscriptionId, IMailboxContext userContext) : base(subscriptionId, userContext)
		{
			this.refreshAll = false;
			this.payloadQueue = new Queue<NotificationPayloadBase>();
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0003398C File Offset: 0x00031B8C
		internal void AddGroupAssociationNotificationPayload(NotificationPayloadBase payload)
		{
			lock (this)
			{
				if (this.refreshAll)
				{
					NotificationStatisticsManager.Instance.NotificationCreated(payload);
					NotificationStatisticsManager.Instance.NotificationDropped(payload, NotificationState.CreatedOrReceived);
				}
				else
				{
					this.payloadQueue.Enqueue(payload);
					NotificationStatisticsManager.Instance.NotificationCreated(payload);
				}
			}
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x000339FC File Offset: 0x00031BFC
		internal void AddRefreshPayload()
		{
			lock (this)
			{
				if (!this.refreshAll)
				{
					NotificationStatisticsManager.Instance.NotificationDropped(this.payloadQueue, NotificationState.CreatedOrReceived);
					this.payloadQueue.Clear();
					this.refreshAll = true;
				}
			}
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x00033A5C File Offset: 0x00031C5C
		protected override IList<NotificationPayloadBase> ReadDataAndResetStateInternal()
		{
			List<NotificationPayloadBase> result;
			if (this.refreshAll)
			{
				GroupAssociationNotificationPayload groupAssociationNotificationPayload = new GroupAssociationNotificationPayload();
				groupAssociationNotificationPayload.EventType = QueryNotificationType.Reload;
				groupAssociationNotificationPayload.Source = MailboxLocation.FromMailboxContext(base.UserContext);
				this.refreshAll = false;
				result = new List<NotificationPayloadBase>
				{
					groupAssociationNotificationPayload
				};
				NotificationStatisticsManager.Instance.NotificationCreated(groupAssociationNotificationPayload);
			}
			else
			{
				result = new List<NotificationPayloadBase>(this.payloadQueue);
				this.payloadQueue.Clear();
			}
			return result;
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x00033ACB File Offset: 0x00031CCB
		protected override bool IsDataAvailableForPickup()
		{
			return this.payloadQueue.Count > 0 || this.refreshAll;
		}

		// Token: 0x0400084F RID: 2127
		private bool refreshAll;

		// Token: 0x04000850 RID: 2128
		private readonly Queue<NotificationPayloadBase> payloadQueue;
	}
}
