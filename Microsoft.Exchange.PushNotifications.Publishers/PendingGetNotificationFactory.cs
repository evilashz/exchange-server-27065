using System;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000B4 RID: 180
	internal class PendingGetNotificationFactory : PushNotificationFactory<PendingGetNotification>
	{
		// Token: 0x0600060C RID: 1548 RVA: 0x00013ADF File Offset: 0x00011CDF
		public override MailboxNotificationRecipient CreateMonitoringRecipient(string appId, string recipientId)
		{
			throw new NotImplementedException("Monitoring PendingGet publishers is not supported");
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00013AEB File Offset: 0x00011CEB
		public override MailboxNotificationRecipient CreateMonitoringRecipient(string appId, int recipientId)
		{
			throw new NotImplementedException("Monitoring PendingGet publishers is not supported");
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00013AF7 File Offset: 0x00011CF7
		protected override bool TryCreateUnseenEmailNotification(MailboxNotificationPayload payload, MailboxNotificationRecipient recipient, PushNotificationPublishingContext context, out PendingGetNotification notification)
		{
			notification = new PendingGetNotification(recipient.AppId, context.OrgId, recipient.DeviceId, new PendingGetPayload(payload.UnseenEmailCount, false));
			return true;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00013B20 File Offset: 0x00011D20
		protected override bool TryCreateMonitoringNotification(MailboxNotificationPayload payload, MailboxNotificationRecipient recipient, PushNotificationPublishingContext context, out PendingGetNotification notification)
		{
			notification = null;
			return false;
		}

		// Token: 0x04000305 RID: 773
		public static readonly PendingGetNotificationFactory Default = new PendingGetNotificationFactory();
	}
}
