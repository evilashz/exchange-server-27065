using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PushNotifications.Publishers;

namespace Microsoft.Exchange.PushNotifications.Server.Commands
{
	// Token: 0x0200000E RID: 14
	internal class CreateDeviceRegistration : PublishNotificationsBase<AzureDeviceRegistrationInfo>
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00002A83 File Offset: 0x00000C83
		public CreateDeviceRegistration(AzureDeviceRegistrationInfo registrationInfo, PushNotificationPublisherManager publisherManager, AsyncCallback asyncCallback, object asyncState) : base(registrationInfo, publisherManager, asyncCallback, asyncState)
		{
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002A90 File Offset: 0x00000C90
		public CreateDeviceRegistration(AzureDeviceRegistrationInfo registrationInfo, PushNotificationPublisherManager publisherManager, string hubName, AsyncCallback asyncCallback, object asyncState) : base(registrationInfo, publisherManager, asyncCallback, asyncState)
		{
			this.HubName = hubName;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002AA5 File Offset: 0x00000CA5
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002AAD File Offset: 0x00000CAD
		protected string HubName { get; set; }

		// Token: 0x06000062 RID: 98 RVA: 0x00002AB6 File Offset: 0x00000CB6
		protected override void Publish()
		{
			base.PublisherManager.Publish(base.RequestInstance, new PushNotificationPublishingContext(base.NotificationSource, OrganizationId.ForestWideOrgId, false, this.HubName));
		}
	}
}
