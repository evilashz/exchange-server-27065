using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PushNotifications.Publishers;

namespace Microsoft.Exchange.PushNotifications.Server.Commands
{
	// Token: 0x02000012 RID: 18
	internal class PublishNotifications : PublishNotificationsBase<MailboxNotificationBatch>
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00002EC0 File Offset: 0x000010C0
		public PublishNotifications(MailboxNotificationBatch notifications, PushNotificationPublisherManager publisherManager, AsyncCallback asyncCallback, object asyncState) : base(notifications, publisherManager, asyncCallback, asyncState)
		{
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002ECD File Offset: 0x000010CD
		public PublishNotifications(MailboxNotificationBatch notifications, PushNotificationPublisherManager publisherManager, string hubName, AsyncCallback asyncCallback, object asyncState) : base(notifications, publisherManager, asyncCallback, asyncState)
		{
			this.HubName = hubName;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002EE2 File Offset: 0x000010E2
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00002EEA File Offset: 0x000010EA
		protected string HubName { get; set; }

		// Token: 0x06000078 RID: 120 RVA: 0x00002EF4 File Offset: 0x000010F4
		protected override void Publish()
		{
			foreach (MailboxNotification mailboxNotification in base.RequestInstance.Notifications)
			{
				string externalDirectoryOrgId = (mailboxNotification != null && mailboxNotification.Payload != null) ? mailboxNotification.Payload.TenantId : null;
				OrganizationId organizationId = OrganizationIdConverter.Default.GetOrganizationId(externalDirectoryOrgId);
				PushNotificationPublishingContext context = new PushNotificationPublishingContext(base.NotificationSource, organizationId, false, this.HubName);
				base.PublisherManager.Publish(mailboxNotification, context);
			}
		}
	}
}
