using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PushNotifications.Publishers;

namespace Microsoft.Exchange.PushNotifications.Server.Commands
{
	// Token: 0x02000016 RID: 22
	internal class PublishUserNotification : PublishNotificationsBase<RemoteUserNotification>
	{
		// Token: 0x06000082 RID: 130 RVA: 0x000031B8 File Offset: 0x000013B8
		public PublishUserNotification(RemoteUserNotification notification, PushNotificationPublisherManager publisherManager, AsyncCallback asyncCallback, object asyncState) : base(notification, publisherManager, asyncCallback, asyncState)
		{
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000031C8 File Offset: 0x000013C8
		protected override void Publish()
		{
			string externalDirectoryOrgId = (base.RequestInstance != null && base.RequestInstance.Payload != null) ? base.RequestInstance.Payload.TenantId : null;
			OrganizationId organizationId = OrganizationIdConverter.Default.GetOrganizationId(externalDirectoryOrgId);
			PushNotificationPublishingContext context = new PushNotificationPublishingContext(base.NotificationSource, organizationId, false, null);
			base.PublisherManager.Publish(base.RequestInstance, context);
		}
	}
}
