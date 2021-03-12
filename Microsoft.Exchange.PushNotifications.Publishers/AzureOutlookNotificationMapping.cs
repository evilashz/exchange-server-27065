using System;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200004C RID: 76
	internal class AzureOutlookNotificationMapping : IPushNotificationMapping<AzureNotification>
	{
		// Token: 0x060002E7 RID: 743 RVA: 0x0000A6A7 File Offset: 0x000088A7
		public AzureOutlookNotificationMapping()
		{
			this.InputType = typeof(OutlookNotificationFragment);
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000A6BF File Offset: 0x000088BF
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x0000A6C7 File Offset: 0x000088C7
		public Type InputType { get; private set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000A6D0 File Offset: 0x000088D0
		// (set) Token: 0x060002EB RID: 747 RVA: 0x0000A6D8 File Offset: 0x000088D8
		private OrganizationIdConverter OrgIdConverter { get; set; }

		// Token: 0x060002EC RID: 748 RVA: 0x0000A6E4 File Offset: 0x000088E4
		public bool TryMap(Notification notification, PushNotificationPublishingContext context, out AzureNotification pushNotification)
		{
			OutlookNotificationFragment outlookNotificationFragment = notification as OutlookNotificationFragment;
			if (outlookNotificationFragment == null)
			{
				throw new InvalidOperationException("Notification type not supported: " + notification.ToFullString());
			}
			pushNotification = new AzureNotification(outlookNotificationFragment.Recipient.AppId, outlookNotificationFragment.Recipient.DeviceId, context.OrgId, new AzurePayload(null, null, outlookNotificationFragment.Payload.Data, null), false);
			return true;
		}
	}
}
