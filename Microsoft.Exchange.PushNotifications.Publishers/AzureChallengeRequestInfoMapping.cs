using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200005E RID: 94
	internal class AzureChallengeRequestInfoMapping : IPushNotificationMapping<AzureChallengeRequestNotification>
	{
		// Token: 0x06000387 RID: 903 RVA: 0x0000C1ED File Offset: 0x0000A3ED
		public AzureChallengeRequestInfoMapping(PushNotificationPublisherConfiguration configuration)
		{
			this.InputType = typeof(AzureChallengeRequestInfo);
			this.publisherConfiguration = configuration.AzureSendPublisherSettings;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0000C211 File Offset: 0x0000A411
		// (set) Token: 0x06000389 RID: 905 RVA: 0x0000C219 File Offset: 0x0000A419
		public Type InputType { get; private set; }

		// Token: 0x0600038A RID: 906 RVA: 0x0000C224 File Offset: 0x0000A424
		public bool TryMap(Notification notification, PushNotificationPublishingContext context, out AzureChallengeRequestNotification pushNotification)
		{
			AzureChallengeRequestInfo azureChallengeRequestInfo = notification as AzureChallengeRequestInfo;
			if (azureChallengeRequestInfo == null)
			{
				throw new InvalidOperationException("Notification type not supported: " + notification.ToFullString());
			}
			if (this.publisherConfiguration.ContainsKey(azureChallengeRequestInfo.TargetAppId))
			{
				AzureChannelSettings channelSettings = this.publisherConfiguration[azureChallengeRequestInfo.TargetAppId].ChannelSettings;
				IAzureSasTokenProvider azureSasTokenProvider = channelSettings.AzureSasTokenProvider;
				AzureUriTemplate uriTemplate = channelSettings.UriTemplate;
				if (azureChallengeRequestInfo.IsMonitoring)
				{
					pushNotification = new AzureChallengeRequestMonitoringNotification(azureChallengeRequestInfo.AppId, azureChallengeRequestInfo.TargetAppId, azureSasTokenProvider, uriTemplate, azureChallengeRequestInfo.DeviceId, new AzureChallengeRequestPayload(azureChallengeRequestInfo.Platform.Value, azureChallengeRequestInfo.DeviceChallenge), azureChallengeRequestInfo.HubName);
				}
				else
				{
					string hubName = azureChallengeRequestInfo.HubName;
					if (string.IsNullOrEmpty(hubName) && !string.IsNullOrEmpty(context.HubName))
					{
						hubName = context.HubName;
					}
					pushNotification = new AzureChallengeRequestNotification(azureChallengeRequestInfo.AppId, azureChallengeRequestInfo.TargetAppId, azureSasTokenProvider, uriTemplate, azureChallengeRequestInfo.DeviceId, new AzureChallengeRequestPayload(azureChallengeRequestInfo.Platform.Value, azureChallengeRequestInfo.DeviceChallenge), hubName);
				}
				return true;
			}
			pushNotification = null;
			return false;
		}

		// Token: 0x04000189 RID: 393
		private readonly Dictionary<string, AzurePublisherSettings> publisherConfiguration;
	}
}
