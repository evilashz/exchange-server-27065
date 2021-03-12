using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000153 RID: 339
	[DataContract]
	public class AttachmentOperationCorrelationIdNotificationPayload : NotificationPayloadBase
	{
		// Token: 0x06000C5D RID: 3165 RVA: 0x0002E222 File Offset: 0x0002C422
		public AttachmentOperationCorrelationIdNotificationPayload()
		{
			base.SubscriptionId = NotificationType.AttachmentOperationCorrelationIdNotification.ToString();
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x0002E23C File Offset: 0x0002C43C
		// (set) Token: 0x06000C5F RID: 3167 RVA: 0x0002E244 File Offset: 0x0002C444
		[DataMember]
		public string CorrelationId { get; set; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x0002E24D File Offset: 0x0002C44D
		// (set) Token: 0x06000C61 RID: 3169 RVA: 0x0002E255 File Offset: 0x0002C455
		[DataMember]
		public string SharePointCallName { get; set; }
	}
}
