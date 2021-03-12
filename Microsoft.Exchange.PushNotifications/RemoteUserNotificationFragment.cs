using System;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x0200003B RID: 59
	internal class RemoteUserNotificationFragment : UserNotificationFragment<RemoteUserNotificationPayload>
	{
		// Token: 0x06000170 RID: 368 RVA: 0x00005066 File Offset: 0x00003266
		public RemoteUserNotificationFragment(string notificationId, RemoteUserNotificationPayload payload, UserNotificationRecipient recipient) : base(notificationId, payload, recipient)
		{
		}
	}
}
