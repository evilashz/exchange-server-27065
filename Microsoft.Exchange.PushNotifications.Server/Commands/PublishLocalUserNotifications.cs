using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PushNotifications.Publishers;

namespace Microsoft.Exchange.PushNotifications.Server.Commands
{
	// Token: 0x02000011 RID: 17
	internal class PublishLocalUserNotifications : PublishNotificationsBase<LocalUserNotificationBatch>
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00002E1E File Offset: 0x0000101E
		public PublishLocalUserNotifications(LocalUserNotificationBatch notifications, PushNotificationPublisherManager publisherManager, AsyncCallback asyncCallback, object asyncState) : base(notifications, publisherManager, asyncCallback, asyncState)
		{
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002E2C File Offset: 0x0000102C
		protected override void Publish()
		{
			foreach (LocalUserNotification localUserNotification in base.RequestInstance.Notifications)
			{
				string externalDirectoryOrgId = (localUserNotification != null && localUserNotification.Payload != null) ? localUserNotification.Payload.TenantId : null;
				OrganizationId organizationId = OrganizationIdConverter.Default.GetOrganizationId(externalDirectoryOrgId);
				PushNotificationPublishingContext context = new PushNotificationPublishingContext(base.NotificationSource, organizationId, false, null);
				base.PublisherManager.Publish(localUserNotification, context);
			}
		}
	}
}
