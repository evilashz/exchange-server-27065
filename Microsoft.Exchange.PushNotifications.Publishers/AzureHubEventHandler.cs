using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000089 RID: 137
	internal class AzureHubEventHandler
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x0000F83F File Offset: 0x0000DA3F
		public AzureHubEventHandler()
		{
			this.PublishingContext = new PushNotificationPublishingContext(base.GetType().Name, OrganizationId.ForestWideOrgId, false, null);
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x0000F864 File Offset: 0x0000DA64
		// (set) Token: 0x060004B2 RID: 1202 RVA: 0x0000F86C File Offset: 0x0000DA6C
		public PushNotificationPublisherManager PublisherManager { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x0000F875 File Offset: 0x0000DA75
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x0000F87D File Offset: 0x0000DA7D
		private PushNotificationPublishingContext PublishingContext { get; set; }

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000F886 File Offset: 0x0000DA86
		public virtual void OnMissingHub(object sender, MissingHubEventArgs missingHubArgs)
		{
			if (this.PublisherManager != null)
			{
				this.PublisherManager.Publish(new AzureHubDefinition(missingHubArgs.HubName, missingHubArgs.TargetAppId), this.PublishingContext);
			}
		}
	}
}
