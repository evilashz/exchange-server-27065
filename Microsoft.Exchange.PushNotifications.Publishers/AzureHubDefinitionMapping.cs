using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000088 RID: 136
	internal class AzureHubDefinitionMapping : IPushNotificationMapping<AzureHubCreationNotification>
	{
		// Token: 0x060004AB RID: 1195 RVA: 0x0000F65C File Offset: 0x0000D85C
		public AzureHubDefinitionMapping(PushNotificationPublisherConfiguration configuration)
		{
			this.InputType = typeof(AzureHubDefinition);
			if (configuration.AzureHubCreationPublisherSettings.Count<AzureHubCreationPublisherSettings>() > 1)
			{
				StringBuilder stringBuilder = new StringBuilder(string.Format("Server:{0};", Environment.MachineName));
				foreach (AzureHubCreationPublisherSettings azureHubCreationPublisherSettings in configuration.AzureHubCreationPublisherSettings)
				{
					stringBuilder.AppendFormat("{0};", azureHubCreationPublisherSettings.AppId);
				}
				PushNotificationsCrimsonEvents.InvalidAzureHubCreationApps.Log<string>(stringBuilder.ToString());
			}
			this.publisherConfiguration = configuration.AzureSendPublisherSettings;
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0000F70C File Offset: 0x0000D90C
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x0000F714 File Offset: 0x0000D914
		public Type InputType { get; private set; }

		// Token: 0x060004AE RID: 1198 RVA: 0x0000F720 File Offset: 0x0000D920
		public bool TryMap(Notification notification, PushNotificationPublishingContext context, out AzureHubCreationNotification pushNotification)
		{
			AzureHubDefinition azureHubDefinition = notification as AzureHubDefinition;
			if (azureHubDefinition == null)
			{
				throw new InvalidOperationException("Notification type not supported: " + notification.ToFullString());
			}
			if (this.publisherConfiguration.ContainsKey(azureHubDefinition.TargetAppId))
			{
				AzureChannelSettings channelSettings = this.publisherConfiguration[azureHubDefinition.TargetAppId].ChannelSettings;
				IAzureSasTokenProvider azureSasTokenProvider = channelSettings.AzureSasTokenProvider;
				AzureSasKey azureSasKey = azureSasTokenProvider as AzureSasKey;
				if (azureSasKey != null)
				{
					AzureHubPayload azureHubPayload = new AzureHubPayload(new AzureSasKey[]
					{
						azureSasKey.ChangeClaims(AzureHubDefinitionMapping.DefaultSasKeyClaims),
						AzureSasKey.GenerateRandomKey(AzureHubDefinitionMapping.DefaultSasKeyClaims, null)
					});
					string partitionName = this.publisherConfiguration[azureHubDefinition.TargetAppId].ChannelSettings.PartitionName;
					if (azureHubDefinition.IsMonitoring)
					{
						pushNotification = new AzureHubCreationMonitoringNotification(azureHubDefinition.TargetAppId, azureHubDefinition.HubName, partitionName, azureHubPayload);
					}
					else
					{
						pushNotification = new AzureHubCreationNotification(azureHubDefinition.TargetAppId, azureHubDefinition.HubName, partitionName, azureHubPayload);
					}
					return true;
				}
				PushNotificationsCrimsonEvents.InvalidAzureSasToken.LogPeriodic<string, IAzureSasTokenProvider>(channelSettings.AppId, CrimsonConstants.DefaultLogPeriodicSuppressionInMinutes, channelSettings.AppId, azureSasTokenProvider);
			}
			pushNotification = null;
			return false;
		}

		// Token: 0x0400024C RID: 588
		internal static readonly AzureSasKey.ClaimType DefaultSasKeyClaims = AzureSasKey.ClaimType.Send | AzureSasKey.ClaimType.Listen | AzureSasKey.ClaimType.Manage;

		// Token: 0x0400024D RID: 589
		private readonly Dictionary<string, AzurePublisherSettings> publisherConfiguration;
	}
}
