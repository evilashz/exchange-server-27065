using System;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000083 RID: 131
	internal sealed class AzureHubCreationMonitoringNotification : AzureHubCreationNotification
	{
		// Token: 0x06000498 RID: 1176 RVA: 0x0000F4D5 File Offset: 0x0000D6D5
		public AzureHubCreationMonitoringNotification(string appId, string hubName, string partitionName, AzureHubPayload hubPayload) : base(appId, hubName, partitionName, hubPayload)
		{
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0000F4E2 File Offset: 0x0000D6E2
		public override bool IsMonitoring
		{
			get
			{
				return true;
			}
		}
	}
}
