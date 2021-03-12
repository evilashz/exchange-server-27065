using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.GroupMailbox;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001BF RID: 447
	internal class UnseenItemNotifier : PendingRequestNotifierBase
	{
		// Token: 0x06000FDE RID: 4062 RVA: 0x0003D482 File Offset: 0x0003B682
		internal UnseenItemNotifier(string subscriptionId, IMailboxContext userContext, string storeSubscriptionId, UserMailboxLocator userMailboxLocator) : base(userContext)
		{
			this.userMailboxLocator = userMailboxLocator;
			this.payloadSubscriptionId = subscriptionId;
			base.SubscriptionId = storeSubscriptionId;
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x0003D4A1 File Offset: 0x0003B6A1
		internal UserMailboxLocator UserMailboxLocator
		{
			get
			{
				return this.userMailboxLocator;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x0003D4A9 File Offset: 0x0003B6A9
		internal string PayloadSubscriptionId
		{
			get
			{
				return this.payloadSubscriptionId;
			}
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0003D4B4 File Offset: 0x0003B6B4
		internal void AddGroupNotificationPayload(UnseenItemNotificationPayload payload)
		{
			lock (this)
			{
				if (this.unseenItemPayload == null)
				{
					NotificationStatisticsManager.Instance.NotificationCreated(payload);
				}
				else if (payload != null)
				{
					payload.CreatedTime = this.unseenItemPayload.CreatedTime;
				}
				this.unseenItemPayload = payload;
			}
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0003D51C File Offset: 0x0003B71C
		protected override IList<NotificationPayloadBase> ReadDataAndResetStateInternal()
		{
			List<NotificationPayloadBase> list = new List<NotificationPayloadBase>();
			if (this.unseenItemPayload != null)
			{
				list.Add(this.unseenItemPayload);
				this.unseenItemPayload = null;
			}
			return list;
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0003D54B File Offset: 0x0003B74B
		protected override bool IsDataAvailableForPickup()
		{
			return this.unseenItemPayload != null;
		}

		// Token: 0x04000979 RID: 2425
		private readonly UserMailboxLocator userMailboxLocator;

		// Token: 0x0400097A RID: 2426
		private UnseenItemNotificationPayload unseenItemPayload;

		// Token: 0x0400097B RID: 2427
		private readonly string payloadSubscriptionId;
	}
}
