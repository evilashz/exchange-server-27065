using System;
using Microsoft.Exchange.Data.PushNotifications;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000D1 RID: 209
	internal class WebAppNotificationFactory : PushNotificationFactory<WebAppNotification>
	{
		// Token: 0x060006D6 RID: 1750 RVA: 0x00015B57 File Offset: 0x00013D57
		public override MailboxNotificationRecipient CreateMonitoringRecipient(string appId, int recipientId)
		{
			return new MailboxNotificationRecipient(appId, recipientId.ToString(), DateTime.UtcNow, null);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00015B6C File Offset: 0x00013D6C
		public override MailboxNotificationRecipient CreateMonitoringRecipient(string appId, string recipientId)
		{
			throw new NotImplementedException("RecipientId is required as int for creating WebApp MonitoringRecipient");
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00015B78 File Offset: 0x00013D78
		protected override bool TryCreateUnseenEmailNotification(MailboxNotificationPayload payload, MailboxNotificationRecipient recipient, PushNotificationPublishingContext context, out WebAppNotification notification)
		{
			notification = new WebAppNotification(recipient.AppId, context.OrgId, "PublishO365Notification", new O365Notification(recipient.DeviceId, payload.UnseenEmailCount.ToString()).ToJson());
			return true;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00015BC3 File Offset: 0x00013DC3
		protected override bool TryCreateMonitoringNotification(MailboxNotificationPayload payload, MailboxNotificationRecipient recipient, PushNotificationPublishingContext context, out WebAppNotification notification)
		{
			notification = new WebAppMonitoringNotification(recipient.AppId, recipient.DeviceId);
			return true;
		}

		// Token: 0x04000372 RID: 882
		public static readonly WebAppNotificationFactory Default = new WebAppNotificationFactory();
	}
}
