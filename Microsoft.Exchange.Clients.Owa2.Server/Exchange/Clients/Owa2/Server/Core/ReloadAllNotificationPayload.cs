using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001A7 RID: 423
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	internal class ReloadAllNotificationPayload : NotificationPayloadBase
	{
		// Token: 0x06000F2C RID: 3884 RVA: 0x0003AE1B File Offset: 0x0003901B
		public ReloadAllNotificationPayload()
		{
			base.SubscriptionId = "ReloadAllNotification";
		}

		// Token: 0x0400092C RID: 2348
		public const string ReloadAllNotificationId = "ReloadAllNotification";
	}
}
