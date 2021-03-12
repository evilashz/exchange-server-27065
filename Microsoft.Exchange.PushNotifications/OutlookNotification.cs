using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x0200002A RID: 42
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal class OutlookNotification : BasicMulticastNotification<OutlookNotificationPayload, OutlookNotificationRecipient>
	{
		// Token: 0x06000125 RID: 293 RVA: 0x0000493B File Offset: 0x00002B3B
		public OutlookNotification(OutlookNotificationPayload payload, List<OutlookNotificationRecipient> recipients) : base(payload, recipients)
		{
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004945 File Offset: 0x00002B45
		protected override Notification CreateFragment(OutlookNotificationPayload payload, OutlookNotificationRecipient recipient)
		{
			return new OutlookNotificationFragment(base.Identifier, payload, recipient);
		}
	}
}
