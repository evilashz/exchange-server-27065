using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PushNotifications.Publishers;

namespace Microsoft.Exchange.PushNotifications.Server.Commands
{
	// Token: 0x0200000D RID: 13
	internal class CreateAzureHub : PublishNotificationsBase<AzureHubDefinition>
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00002A51 File Offset: 0x00000C51
		public CreateAzureHub(AzureHubDefinition hubDefinition, PushNotificationPublisherManager publisherManager, AsyncCallback asyncCallback, object asyncState) : base(hubDefinition, publisherManager, asyncCallback, asyncState)
		{
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002A5E File Offset: 0x00000C5E
		protected override void Publish()
		{
			base.PublisherManager.Publish(base.RequestInstance, new PushNotificationPublishingContext(base.NotificationSource, OrganizationId.ForestWideOrgId, false, null));
		}
	}
}
