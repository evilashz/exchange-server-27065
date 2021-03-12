using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000034 RID: 52
	internal class ApnsNotificationFactory : PushNotificationFactory<ApnsNotification>
	{
		// Token: 0x06000201 RID: 513 RVA: 0x0000812A File Offset: 0x0000632A
		public override MailboxNotificationRecipient CreateMonitoringRecipient(string appId, int recipientId)
		{
			return new MailboxNotificationRecipient(appId, string.Format("{0:d64}", recipientId), DateTime.UtcNow, null);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00008148 File Offset: 0x00006348
		public override MailboxNotificationRecipient CreateMonitoringRecipient(string appId, string recipientId)
		{
			throw new NotImplementedException("RecipientId is required as int for creating APNS MonitoringRecipient");
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00008154 File Offset: 0x00006354
		protected override bool TryCreateUnseenEmailNotification(MailboxNotificationPayload payload, MailboxNotificationRecipient recipient, PushNotificationPublishingContext context, out ApnsNotification notification)
		{
			return this.TryCreateApnsNotification(payload, recipient, context.OrgId, null, null, out notification);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00008168 File Offset: 0x00006368
		protected override bool TryCreateMonitoringNotification(MailboxNotificationPayload payload, MailboxNotificationRecipient recipient, PushNotificationPublishingContext context, out ApnsNotification notification)
		{
			notification = new ApnsMonitoringNotification(recipient.AppId, recipient.DeviceId);
			return true;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00008180 File Offset: 0x00006380
		private bool TryCreateApnsNotification(MailboxNotificationPayload payload, MailboxNotificationRecipient recipient, OrganizationId tenantId, ApnsAlert apnsAlert, string storeObjectId, out ApnsNotification notification)
		{
			notification = new ApnsNotification(recipient.AppId, tenantId, recipient.DeviceId, new ApnsPayload(new ApnsPayloadBasicData(payload.UnseenEmailCount, null, apnsAlert, (payload.BackgroundSyncType == BackgroundSyncType.None) ? 0 : 1), storeObjectId, base.GetBackgroundSyncTypeString(payload.BackgroundSyncType)), 0, recipient.LastSubscriptionUpdate);
			return true;
		}

		// Token: 0x040000C9 RID: 201
		public static readonly ApnsNotificationFactory Default = new ApnsNotificationFactory();
	}
}
