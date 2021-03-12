using System;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000060 RID: 96
	internal sealed class AzureChallengeRequestMonitoringNotification : AzureChallengeRequestNotification
	{
		// Token: 0x0600039C RID: 924 RVA: 0x0000C529 File Offset: 0x0000A729
		public AzureChallengeRequestMonitoringNotification(string appId, string targetAppId, IAzureSasTokenProvider sasTokenProvider, AzureUriTemplate uriTemplate, string deviceId, AzureChallengeRequestPayload payload, string hubName) : base(appId, targetAppId, sasTokenProvider, uriTemplate, deviceId, payload, hubName)
		{
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0000C53C File Offset: 0x0000A73C
		public override bool IsMonitoring
		{
			get
			{
				return true;
			}
		}
	}
}
