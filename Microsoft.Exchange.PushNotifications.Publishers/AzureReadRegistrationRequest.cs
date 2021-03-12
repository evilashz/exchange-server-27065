using System;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000077 RID: 119
	internal class AzureReadRegistrationRequest : AzureRequestBase
	{
		// Token: 0x06000428 RID: 1064 RVA: 0x0000DDB6 File Offset: 0x0000BFB6
		public AzureReadRegistrationRequest(AzureSasToken sasToken, string resourceUri) : base("application/atom+xml;type=entry;charset=utf-8", "GET", sasToken, resourceUri, "&$top=1")
		{
		}

		// Token: 0x040001E9 RID: 489
		public const string ReadRegistrationContentType = "application/atom+xml;type=entry;charset=utf-8";

		// Token: 0x040001EA RID: 490
		public const string ReadRegistrationAdditionalParameters = "&$top=1";
	}
}
