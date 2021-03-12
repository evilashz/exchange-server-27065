using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PushNotifications.Publishers;

namespace Microsoft.Exchange.PushNotifications.Server.Commands
{
	// Token: 0x02000014 RID: 20
	internal class PublishOutlookNotifications : PublishNotificationsBase<OutlookNotificationBatch>
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00002FC0 File Offset: 0x000011C0
		public PublishOutlookNotifications(OutlookNotificationBatch notifications, PushNotificationPublisherManager publisherManager, AsyncCallback asyncCallback, object asyncState) : base(notifications, publisherManager, asyncCallback, asyncState)
		{
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002FD0 File Offset: 0x000011D0
		protected override void Publish()
		{
			foreach (OutlookNotification outlookNotification in base.RequestInstance.Notifications)
			{
				string externalDirectoryOrgId = (outlookNotification != null && outlookNotification.Payload != null) ? outlookNotification.Payload.TenantId : null;
				OrganizationId organizationId = OrganizationIdConverter.Default.GetOrganizationId(externalDirectoryOrgId);
				PushNotificationPublishingContext context = new PushNotificationPublishingContext(base.NotificationSource, organizationId, false, null);
				base.PublisherManager.Publish(outlookNotification, context);
			}
		}
	}
}
