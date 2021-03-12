using System;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000006 RID: 6
	internal interface IMonitoringMailboxNotificationRecipientFactory
	{
		// Token: 0x0600000F RID: 15
		MailboxNotificationRecipient CreateMonitoringRecipient(string appId, int recipientId);

		// Token: 0x06000010 RID: 16
		MailboxNotificationRecipient CreateMonitoringRecipient(string appId, string recipientId);
	}
}
