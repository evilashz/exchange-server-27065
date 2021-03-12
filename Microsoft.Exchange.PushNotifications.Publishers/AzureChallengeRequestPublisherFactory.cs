using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000065 RID: 101
	internal class AzureChallengeRequestPublisherFactory : PushNotificationPublisherFactory
	{
		// Token: 0x060003B4 RID: 948 RVA: 0x0000C775 File Offset: 0x0000A975
		public AzureChallengeRequestPublisherFactory(List<IPushNotificationMapping<AzureChallengeRequestNotification>> mappings = null, AzureHubEventHandler hubEventHandler = null)
		{
			this.Mappings = mappings;
			this.AzureHubHandler = hubEventHandler;
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000C78B File Offset: 0x0000A98B
		public override PushNotificationPlatform Platform
		{
			get
			{
				return PushNotificationPlatform.AzureChallengeRequest;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000C78F File Offset: 0x0000A98F
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x0000C797 File Offset: 0x0000A997
		public virtual AzureHubEventHandler AzureHubHandler { get; private set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000C7A0 File Offset: 0x0000A9A0
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x0000C7A8 File Offset: 0x0000A9A8
		private List<IPushNotificationMapping<AzureChallengeRequestNotification>> Mappings { get; set; }

		// Token: 0x060003BA RID: 954 RVA: 0x0000C7B4 File Offset: 0x0000A9B4
		public override PushNotificationPublisherBase CreatePublisher(PushNotificationPublisherSettings settings)
		{
			AzureChallengeRequestPublisherSettings azureChallengeRequestPublisherSettings = settings as AzureChallengeRequestPublisherSettings;
			if (azureChallengeRequestPublisherSettings == null)
			{
				throw new ArgumentException(string.Format("settings should be an AzureSecretRequestPublisherSettings instance: {0}", (settings == null) ? "null" : settings.GetType().ToString()));
			}
			AzureChallengeRequestPublisher azureChallengeRequestPublisher = new AzureChallengeRequestPublisher(azureChallengeRequestPublisherSettings, this.Mappings);
			if (this.AzureHubHandler != null)
			{
				azureChallengeRequestPublisher.MissingHubDetected += this.AzureHubHandler.OnMissingHub;
			}
			return azureChallengeRequestPublisher;
		}
	}
}
