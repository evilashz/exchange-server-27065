using System;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000035 RID: 53
	internal class UserNotificationFragment<T> : MulticastNotificationFragment<T, UserNotificationRecipient> where T : UserNotificationPayload
	{
		// Token: 0x06000156 RID: 342 RVA: 0x00004D8D File Offset: 0x00002F8D
		public UserNotificationFragment(string notificationId, T payload, UserNotificationRecipient recipient) : base(notificationId, payload, recipient)
		{
		}
	}
}
