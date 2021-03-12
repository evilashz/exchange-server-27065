using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000087 RID: 135
	internal class AzureHubCreationRequest : AzureRequestBase
	{
		// Token: 0x060004AA RID: 1194 RVA: 0x0000F62C File Offset: 0x0000D82C
		public AzureHubCreationRequest(AzureHubCreationNotification notification, AcsAccessToken azureKey, string resourceUri) : base("application/xml;type=entry;charset=utf-8", "PUT", azureKey, resourceUri, "")
		{
			ArgumentValidator.ThrowIfNull("notification", notification);
			base.RequestBody = notification.SerializedPaylod;
		}

		// Token: 0x0400024B RID: 587
		public const string CreateHubContentType = "application/xml;type=entry;charset=utf-8";
	}
}
