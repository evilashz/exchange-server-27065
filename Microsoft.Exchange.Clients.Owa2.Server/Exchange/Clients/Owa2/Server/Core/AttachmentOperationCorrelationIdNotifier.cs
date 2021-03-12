using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000154 RID: 340
	internal class AttachmentOperationCorrelationIdNotifier : PendingRequestNotifierBase
	{
		// Token: 0x06000C62 RID: 3170 RVA: 0x0002E25E File Offset: 0x0002C45E
		internal AttachmentOperationCorrelationIdNotifier(UserContext userContext, string subscriptionId) : base(subscriptionId, userContext)
		{
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x0002E274 File Offset: 0x0002C474
		// (set) Token: 0x06000C64 RID: 3172 RVA: 0x0002E2B8 File Offset: 0x0002C4B8
		internal AttachmentOperationCorrelationIdNotificationPayload Payload
		{
			get
			{
				AttachmentOperationCorrelationIdNotificationPayload result;
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

		// Token: 0x06000C65 RID: 3173 RVA: 0x0002E2FC File Offset: 0x0002C4FC
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

		// Token: 0x06000C66 RID: 3174 RVA: 0x0002E358 File Offset: 0x0002C558
		protected override bool IsDataAvailableForPickup()
		{
			return this.Payload != null;
		}

		// Token: 0x040007C3 RID: 1987
		private AttachmentOperationCorrelationIdNotificationPayload payload;

		// Token: 0x040007C4 RID: 1988
		private object lockObject = new object();
	}
}
