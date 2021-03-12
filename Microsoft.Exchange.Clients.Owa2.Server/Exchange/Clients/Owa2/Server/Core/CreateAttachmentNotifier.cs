using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200016E RID: 366
	internal class CreateAttachmentNotifier : PendingRequestNotifierBase
	{
		// Token: 0x06000D88 RID: 3464 RVA: 0x00033275 File Offset: 0x00031475
		internal CreateAttachmentNotifier(UserContext userContext, string subscriptionId) : base(subscriptionId, userContext)
		{
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x0003328C File Offset: 0x0003148C
		// (set) Token: 0x06000D8A RID: 3466 RVA: 0x000332D0 File Offset: 0x000314D0
		internal CreateAttachmentNotificationPayload Payload
		{
			get
			{
				CreateAttachmentNotificationPayload result;
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

		// Token: 0x06000D8B RID: 3467 RVA: 0x00033314 File Offset: 0x00031514
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

		// Token: 0x06000D8C RID: 3468 RVA: 0x00033370 File Offset: 0x00031570
		protected override bool IsDataAvailableForPickup()
		{
			return this.Payload != null;
		}

		// Token: 0x0400083C RID: 2108
		private CreateAttachmentNotificationPayload payload;

		// Token: 0x0400083D RID: 2109
		private object lockObject = new object();
	}
}
