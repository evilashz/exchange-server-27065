using System;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x0200002C RID: 44
	internal class OutlookNotificationFragment : MulticastNotificationFragment<OutlookNotificationPayload, OutlookNotificationRecipient>
	{
		// Token: 0x06000130 RID: 304 RVA: 0x00004A02 File Offset: 0x00002C02
		public OutlookNotificationFragment(string notificationId, OutlookNotificationPayload payload, OutlookNotificationRecipient recipient) : base(notificationId, payload, recipient)
		{
		}
	}
}
