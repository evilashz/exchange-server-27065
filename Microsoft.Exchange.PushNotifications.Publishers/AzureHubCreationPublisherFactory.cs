using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000085 RID: 133
	internal class AzureHubCreationPublisherFactory : PushNotificationPublisherFactory
	{
		// Token: 0x060004A0 RID: 1184 RVA: 0x0000F53C File Offset: 0x0000D73C
		public AzureHubCreationPublisherFactory(List<IPushNotificationMapping<AzureHubCreationNotification>> mappings = null)
		{
			this.Mappings = mappings;
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x0000F54B File Offset: 0x0000D74B
		public override PushNotificationPlatform Platform
		{
			get
			{
				return PushNotificationPlatform.AzureHubCreation;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0000F54E File Offset: 0x0000D74E
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x0000F556 File Offset: 0x0000D756
		private List<IPushNotificationMapping<AzureHubCreationNotification>> Mappings { get; set; }

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000F560 File Offset: 0x0000D760
		public override PushNotificationPublisherBase CreatePublisher(PushNotificationPublisherSettings settings)
		{
			AzureHubCreationPublisherSettings azureHubCreationPublisherSettings = settings as AzureHubCreationPublisherSettings;
			if (azureHubCreationPublisherSettings == null)
			{
				throw new ArgumentException(string.Format("settings should be an AzureHubCreationPublisherSettings instance: {0}", (settings == null) ? "null" : settings.GetType().ToString()));
			}
			return new AzureHubCreationPublisher(azureHubCreationPublisherSettings, this.Mappings);
		}
	}
}
