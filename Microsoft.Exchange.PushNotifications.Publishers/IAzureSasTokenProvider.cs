using System;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000055 RID: 85
	internal interface IAzureSasTokenProvider
	{
		// Token: 0x0600033E RID: 830
		AzureSasToken CreateSasToken(string resourceUri);

		// Token: 0x0600033F RID: 831
		AzureSasToken CreateSasToken(string resourceUri, int expirationInSeconds);
	}
}
