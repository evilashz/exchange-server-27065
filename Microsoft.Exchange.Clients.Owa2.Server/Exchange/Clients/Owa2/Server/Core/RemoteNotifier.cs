using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001CF RID: 463
	internal class RemoteNotifier : PendingRequestNotifierBase
	{
		// Token: 0x06001067 RID: 4199 RVA: 0x0003EEEA File Offset: 0x0003D0EA
		public RemoteNotifier(IMailboxContext context) : base(context)
		{
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0003EF00 File Offset: 0x0003D100
		internal void AddRemoteNotificationPayload(RemoteNotificationPayload remoteNotificationPayload)
		{
			lock (this)
			{
				if (this.reloadAll)
				{
					NotificationStatisticsManager.Instance.NotificationReceived(remoteNotificationPayload);
					NotificationStatisticsManager.Instance.NotificationDropped(remoteNotificationPayload, NotificationState.CreatedOrReceived);
				}
				else if (this.queue.Count >= 40)
				{
					NotificationStatisticsManager.Instance.NotificationReceived(remoteNotificationPayload);
					NotificationStatisticsManager.Instance.NotificationDropped(remoteNotificationPayload, NotificationState.CreatedOrReceived);
					NotificationStatisticsManager.Instance.NotificationDropped(this.queue, NotificationState.CreatedOrReceived);
					this.queue.Clear();
					ReloadAllNotificationPayload reloadAllNotificationPayload = new ReloadAllNotificationPayload();
					reloadAllNotificationPayload.Source = new TypeLocation(base.GetType());
					this.queue.Enqueue(reloadAllNotificationPayload);
					NotificationStatisticsManager.Instance.NotificationCreated(reloadAllNotificationPayload);
					this.reloadAll = true;
				}
				else
				{
					this.queue.Enqueue(remoteNotificationPayload);
					NotificationStatisticsManager.Instance.NotificationReceived(remoteNotificationPayload);
				}
			}
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x0003EFEC File Offset: 0x0003D1EC
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
				this.reloadAll = false;
			}
			return list;
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0003F068 File Offset: 0x0003D268
		protected override bool IsDataAvailableForPickup()
		{
			return this.AreThereNotifications();
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0003F070 File Offset: 0x0003D270
		private bool AreThereNotifications()
		{
			return this.queue.Count > 0;
		}

		// Token: 0x040009BD RID: 2493
		public const int MaxRemoteNotificationQueueSize = 40;

		// Token: 0x040009BE RID: 2494
		protected Queue<NotificationPayloadBase> queue = new Queue<NotificationPayloadBase>();

		// Token: 0x040009BF RID: 2495
		private bool reloadAll;
	}
}
