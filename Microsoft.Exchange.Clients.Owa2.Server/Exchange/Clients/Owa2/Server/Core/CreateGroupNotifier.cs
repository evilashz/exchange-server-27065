using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000170 RID: 368
	internal class CreateGroupNotifier : PendingRequestNotifierBase
	{
		// Token: 0x06000D92 RID: 3474 RVA: 0x000333BA File Offset: 0x000315BA
		internal CreateGroupNotifier(IMailboxContext userContext) : base(CreateGroupNotifier.CreateGroupNotifierId, userContext)
		{
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x000333D4 File Offset: 0x000315D4
		// (set) Token: 0x06000D94 RID: 3476 RVA: 0x00033418 File Offset: 0x00031618
		internal CreateGroupNotificationPayload Payload
		{
			get
			{
				CreateGroupNotificationPayload result;
				lock (this.lockObject)
				{
					result = this.payload;
				}
				return result;
			}
			set
			{
				lock (this.lockObject)
				{
					this.payload = value;
				}
			}
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0003345C File Offset: 0x0003165C
		protected override IList<NotificationPayloadBase> ReadDataAndResetStateInternal()
		{
			List<NotificationPayloadBase> list = new List<NotificationPayloadBase>();
			lock (this.lockObject)
			{
				if (this.Payload != null)
				{
					list.Add(this.Payload);
					this.Payload = null;
				}
			}
			return list;
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x000334B8 File Offset: 0x000316B8
		protected override bool IsDataAvailableForPickup()
		{
			return this.Payload != null;
		}

		// Token: 0x04000840 RID: 2112
		public static string CreateGroupNotifierId = NotificationType.GroupCreateNotification.ToString();

		// Token: 0x04000841 RID: 2113
		private readonly object lockObject = new object();

		// Token: 0x04000842 RID: 2114
		private CreateGroupNotificationPayload payload;
	}
}
