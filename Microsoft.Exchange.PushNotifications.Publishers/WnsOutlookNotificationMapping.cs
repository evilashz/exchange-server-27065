using System;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000EC RID: 236
	internal class WnsOutlookNotificationMapping : IPushNotificationMapping<WnsNotification>
	{
		// Token: 0x0600078C RID: 1932 RVA: 0x00017A62 File Offset: 0x00015C62
		public WnsOutlookNotificationMapping()
		{
			this.InputType = typeof(OutlookNotificationFragment);
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x00017A7A File Offset: 0x00015C7A
		// (set) Token: 0x0600078E RID: 1934 RVA: 0x00017A82 File Offset: 0x00015C82
		public Type InputType { get; private set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x00017A8B File Offset: 0x00015C8B
		// (set) Token: 0x06000790 RID: 1936 RVA: 0x00017A93 File Offset: 0x00015C93
		private OrganizationIdConverter OrgIdConverter { get; set; }

		// Token: 0x06000791 RID: 1937 RVA: 0x00017A9C File Offset: 0x00015C9C
		public bool TryMap(Notification notification, PushNotificationPublishingContext context, out WnsNotification pushNotification)
		{
			OutlookNotificationFragment outlookNotificationFragment = notification as OutlookNotificationFragment;
			if (outlookNotificationFragment == null)
			{
				throw new InvalidOperationException("Notification type not supported: " + notification.ToFullString());
			}
			pushNotification = new WnsRawNotification(outlookNotificationFragment.Recipient.AppId, context.OrgId, outlookNotificationFragment.Recipient.DeviceId, outlookNotificationFragment.Payload.Data);
			return true;
		}
	}
}
