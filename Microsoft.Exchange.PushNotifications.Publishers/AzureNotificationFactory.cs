using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200004B RID: 75
	internal class AzureNotificationFactory : PushNotificationFactory<AzureNotification>
	{
		// Token: 0x060002E0 RID: 736 RVA: 0x0000A599 File Offset: 0x00008799
		public override MailboxNotificationRecipient CreateMonitoringRecipient(string appId, string recipientId)
		{
			return new MailboxNotificationRecipient(appId, recipientId, DateTime.UtcNow, null);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000A5A8 File Offset: 0x000087A8
		public override MailboxNotificationRecipient CreateMonitoringRecipient(string appId, int recipientId)
		{
			throw new NotImplementedException("RecipientId is required as string for creating Azure MonitoringRecipient");
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000A5B4 File Offset: 0x000087B4
		protected override bool TryCreateUnseenEmailNotification(MailboxNotificationPayload payload, MailboxNotificationRecipient recipient, PushNotificationPublishingContext context, out AzureNotification notification)
		{
			notification = this.CreateAzureNotification(payload, recipient, context, null, null);
			return true;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000A5C5 File Offset: 0x000087C5
		protected override bool TryCreateMonitoringNotification(MailboxNotificationPayload payload, MailboxNotificationRecipient recipient, PushNotificationPublishingContext context, out AzureNotification notification)
		{
			notification = new AzureMonitoringNotification(recipient.AppId, payload.TenantId, recipient.DeviceId);
			return true;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000A5E4 File Offset: 0x000087E4
		private AzureNotification CreateAzureNotification(MailboxNotificationPayload payload, MailboxNotificationRecipient recipient, PushNotificationPublishingContext context, string message, string storeObjectId)
		{
			string recipient2 = (!string.IsNullOrWhiteSpace(recipient.InstallationId)) ? recipient.InstallationId : recipient.DeviceId;
			if (OrganizationId.ForestWideOrgId.Equals(context.OrgId) && !string.IsNullOrEmpty(context.HubName))
			{
				return new AzureNotification(recipient.AppId, recipient2, context.HubName, new AzurePayload(payload.UnseenEmailCount, message, storeObjectId, base.GetBackgroundSyncTypeString(payload.BackgroundSyncType)), context.RequireDeviceRegistration);
			}
			return new AzureNotification(recipient.AppId, recipient2, context.OrgId, new AzurePayload(payload.UnseenEmailCount, message, storeObjectId, base.GetBackgroundSyncTypeString(payload.BackgroundSyncType)), context.RequireDeviceRegistration);
		}

		// Token: 0x04000139 RID: 313
		public static readonly AzureNotificationFactory Default = new AzureNotificationFactory();
	}
}
