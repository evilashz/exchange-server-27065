using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000072 RID: 114
	internal class AzureDeviceRegistrationPublisherFactory : PushNotificationPublisherFactory
	{
		// Token: 0x06000412 RID: 1042 RVA: 0x0000DB2B File Offset: 0x0000BD2B
		public AzureDeviceRegistrationPublisherFactory(List<IPushNotificationMapping<AzureDeviceRegistrationNotification>> mappings = null, AzureHubEventHandler hubEventHandler = null)
		{
			this.Mappings = mappings;
			this.AzureHubHandler = hubEventHandler;
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0000DB41 File Offset: 0x0000BD41
		public override PushNotificationPlatform Platform
		{
			get
			{
				return PushNotificationPlatform.AzureDeviceRegistration;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000DB45 File Offset: 0x0000BD45
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x0000DB4D File Offset: 0x0000BD4D
		public virtual AzureHubEventHandler AzureHubHandler { get; private set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x0000DB56 File Offset: 0x0000BD56
		// (set) Token: 0x06000417 RID: 1047 RVA: 0x0000DB5E File Offset: 0x0000BD5E
		private List<IPushNotificationMapping<AzureDeviceRegistrationNotification>> Mappings { get; set; }

		// Token: 0x06000418 RID: 1048 RVA: 0x0000DB68 File Offset: 0x0000BD68
		public override PushNotificationPublisherBase CreatePublisher(PushNotificationPublisherSettings settings)
		{
			AzureDeviceRegistrationPublisherSettings azureDeviceRegistrationPublisherSettings = settings as AzureDeviceRegistrationPublisherSettings;
			if (azureDeviceRegistrationPublisherSettings == null)
			{
				throw new ArgumentException(string.Format("settings should be an AzureDeviceRegistrationPublisherSettings instance: {0}", (settings == null) ? "null" : settings.GetType().ToString()));
			}
			AzureDeviceRegistrationPublisher azureDeviceRegistrationPublisher = new AzureDeviceRegistrationPublisher(azureDeviceRegistrationPublisherSettings, this.Mappings);
			if (this.AzureHubHandler != null)
			{
				azureDeviceRegistrationPublisher.MissingHubDetected += this.AzureHubHandler.OnMissingHub;
			}
			return azureDeviceRegistrationPublisher;
		}
	}
}
