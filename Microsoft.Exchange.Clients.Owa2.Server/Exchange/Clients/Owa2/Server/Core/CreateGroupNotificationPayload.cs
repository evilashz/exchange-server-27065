using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200016F RID: 367
	[DataContract]
	public class CreateGroupNotificationPayload : NotificationPayloadBase
	{
		// Token: 0x06000D8D RID: 3469 RVA: 0x0003337E File Offset: 0x0003157E
		public CreateGroupNotificationPayload()
		{
			base.SubscriptionId = NotificationType.GroupCreateNotification.ToString();
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x00033398 File Offset: 0x00031598
		// (set) Token: 0x06000D8F RID: 3471 RVA: 0x000333A0 File Offset: 0x000315A0
		[DataMember]
		public string ExternalDirectoryObjectId { get; set; }

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x000333A9 File Offset: 0x000315A9
		// (set) Token: 0x06000D91 RID: 3473 RVA: 0x000333B1 File Offset: 0x000315B1
		[DataMember]
		public string PushToken { get; set; }
	}
}
