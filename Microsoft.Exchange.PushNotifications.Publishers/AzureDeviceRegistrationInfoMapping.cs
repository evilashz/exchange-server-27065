using System;
using System.Collections.Generic;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200006C RID: 108
	internal class AzureDeviceRegistrationInfoMapping : IPushNotificationMapping<AzureDeviceRegistrationNotification>
	{
		// Token: 0x060003E3 RID: 995 RVA: 0x0000D590 File Offset: 0x0000B790
		public AzureDeviceRegistrationInfoMapping(PushNotificationPublisherConfiguration configuration)
		{
			this.InputType = typeof(AzureDeviceRegistrationInfo);
			this.publisherConfiguration = configuration.AzureSendPublisherSettings;
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000D5B4 File Offset: 0x0000B7B4
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x0000D5BC File Offset: 0x0000B7BC
		public Type InputType { get; private set; }

		// Token: 0x060003E6 RID: 998 RVA: 0x0000D5C8 File Offset: 0x0000B7C8
		public bool TryMap(Notification notification, PushNotificationPublishingContext context, out AzureDeviceRegistrationNotification pushNotification)
		{
			AzureDeviceRegistrationInfo azureDeviceRegistrationInfo = notification as AzureDeviceRegistrationInfo;
			if (azureDeviceRegistrationInfo == null)
			{
				throw new InvalidOperationException("Notification type not supported: " + notification.ToFullString());
			}
			if (!this.publisherConfiguration.ContainsKey(azureDeviceRegistrationInfo.TargetAppId))
			{
				pushNotification = null;
				return false;
			}
			AzurePublisherSettings azurePublisherSettings = this.publisherConfiguration[azureDeviceRegistrationInfo.TargetAppId];
			AzureChannelSettings channelSettings = azurePublisherSettings.ChannelSettings;
			if (channelSettings.IsRegistrationEnabled && !azurePublisherSettings.IsMultifactorRegistrationEnabled)
			{
				pushNotification = null;
				if (PushNotificationsCrimsonEvents.AzureDeviceRegistrationMappingDroppingRequest.IsEnabled(PushNotificationsCrimsonEvent.Provider))
				{
					PushNotificationsCrimsonEvents.AzureDeviceRegistrationMappingDroppingRequest.Log<string, string>(azureDeviceRegistrationInfo.TargetAppId, azureDeviceRegistrationInfo.RecipientId);
				}
				return false;
			}
			if (azureDeviceRegistrationInfo.IsMonitoring)
			{
				pushNotification = new AzureDeviceRegistrationMonitoringNotification(azureDeviceRegistrationInfo.AppId, azureDeviceRegistrationInfo.TargetAppId, channelSettings.AzureSasTokenProvider, channelSettings.UriTemplate, new AzureDeviceRegistrationPayload(azureDeviceRegistrationInfo.RecipientId, channelSettings.RegistrationTemplate, azureDeviceRegistrationInfo.Tag), azureDeviceRegistrationInfo.HubName, azureDeviceRegistrationInfo.ServerChallenge);
			}
			else
			{
				string hubName = azureDeviceRegistrationInfo.HubName;
				if (string.IsNullOrEmpty(hubName) && !string.IsNullOrEmpty(context.HubName))
				{
					hubName = context.HubName;
				}
				pushNotification = new AzureDeviceRegistrationNotification(azureDeviceRegistrationInfo.AppId, azureDeviceRegistrationInfo.TargetAppId, channelSettings.AzureSasTokenProvider, channelSettings.UriTemplate, new AzureDeviceRegistrationPayload(azureDeviceRegistrationInfo.RecipientId, channelSettings.RegistrationTemplate, azureDeviceRegistrationInfo.Tag), hubName, azureDeviceRegistrationInfo.ServerChallenge);
			}
			return true;
		}

		// Token: 0x040001C6 RID: 454
		private readonly Dictionary<string, AzurePublisherSettings> publisherConfiguration;
	}
}
