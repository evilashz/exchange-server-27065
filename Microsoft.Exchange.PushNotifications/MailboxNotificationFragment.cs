using System;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000021 RID: 33
	internal class MailboxNotificationFragment : MulticastNotificationFragment<MailboxNotificationPayload, MailboxNotificationRecipient>
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x00003E1A File Offset: 0x0000201A
		public MailboxNotificationFragment(string notificationId, MailboxNotificationPayload payload, MailboxNotificationRecipient recipient) : base(notificationId, payload, recipient)
		{
		}
	}
}
