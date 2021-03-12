using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000181 RID: 385
	internal class NewMailNotifier : PendingRequestNotifierBase
	{
		// Token: 0x06000DF5 RID: 3573 RVA: 0x00034BAA File Offset: 0x00032DAA
		internal NewMailNotifier(string subscriptionId, IMailboxContext userContext) : base(subscriptionId, userContext)
		{
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x00034BC0 File Offset: 0x00032DC0
		// (set) Token: 0x06000DF7 RID: 3575 RVA: 0x00034C04 File Offset: 0x00032E04
		internal NewMailNotificationPayload Payload
		{
			get
			{
				NewMailNotificationPayload result;
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

		// Token: 0x06000DF8 RID: 3576 RVA: 0x00034C48 File Offset: 0x00032E48
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

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00034CA4 File Offset: 0x00032EA4
		protected override bool IsDataAvailableForPickup()
		{
			return this.Payload != null;
		}

		// Token: 0x04000871 RID: 2161
		private NewMailNotificationPayload payload;

		// Token: 0x04000872 RID: 2162
		private object lockObject = new object();
	}
}
