using System;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000036 RID: 54
	internal class LocalUserNotificationFragment : UserNotificationFragment<LocalUserNotificationPayload>
	{
		// Token: 0x06000157 RID: 343 RVA: 0x00004D98 File Offset: 0x00002F98
		public LocalUserNotificationFragment(string notificationId, LocalUserNotificationPayload payload, UserNotificationRecipient recipient) : base(notificationId, payload, recipient)
		{
		}
	}
}
