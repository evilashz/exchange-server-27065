using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000A0 RID: 160
	internal class GcmNotificationFactory : PushNotificationFactory<GcmNotification>
	{
		// Token: 0x0600059A RID: 1434 RVA: 0x00012B75 File Offset: 0x00010D75
		public override MailboxNotificationRecipient CreateMonitoringRecipient(string appId, int recipientId)
		{
			return new MailboxNotificationRecipient(appId, recipientId.ToString(), DateTime.UtcNow, null);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00012B8A File Offset: 0x00010D8A
		public override MailboxNotificationRecipient CreateMonitoringRecipient(string appId, string recipientId)
		{
			throw new NotImplementedException("RecipientId is required as int for creating GCM MonitoringRecipient");
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00012B96 File Offset: 0x00010D96
		protected override bool TryCreateUnseenEmailNotification(MailboxNotificationPayload payload, MailboxNotificationRecipient recipient, PushNotificationPublishingContext context, out GcmNotification notification)
		{
			notification = this.CreateGcmNotification(payload, recipient, context.OrgId, null, null);
			return true;
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00012BAC File Offset: 0x00010DAC
		protected override bool TryCreateMonitoringNotification(MailboxNotificationPayload payload, MailboxNotificationRecipient recipient, PushNotificationPublishingContext context, out GcmNotification notification)
		{
			notification = new GcmMonitoringNotification(recipient.AppId, recipient.DeviceId);
			return true;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00012BC4 File Offset: 0x00010DC4
		private GcmNotification CreateGcmNotification(MailboxNotificationPayload payload, MailboxNotificationRecipient recipient, OrganizationId tenantId, string message, string extraData)
		{
			return new GcmNotification(recipient.AppId, tenantId, recipient.DeviceId, new GcmPayload(payload.UnseenEmailCount, message, extraData, payload.BackgroundSyncType), "c", null, null);
		}

		// Token: 0x040002BD RID: 701
		public static readonly GcmNotificationFactory Default = new GcmNotificationFactory();
	}
}
