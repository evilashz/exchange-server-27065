using System;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200006E RID: 110
	internal sealed class AzureDeviceRegistrationMonitoringNotification : AzureDeviceRegistrationNotification
	{
		// Token: 0x060003FA RID: 1018 RVA: 0x0000D925 File Offset: 0x0000BB25
		public AzureDeviceRegistrationMonitoringNotification(string appId, string targetAppId, IAzureSasTokenProvider sasTokenProvider, AzureUriTemplate uriTemplate, AzureDeviceRegistrationPayload payload, string hubName, string serverChallenge = null) : base(appId, targetAppId, sasTokenProvider, uriTemplate, payload, hubName, serverChallenge)
		{
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000D938 File Offset: 0x0000BB38
		public override bool IsMonitoring
		{
			get
			{
				return true;
			}
		}
	}
}
