using System;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000074 RID: 116
	internal class AzureNewRegistrationIdRequest : AzureRequestBase
	{
		// Token: 0x0600041E RID: 1054 RVA: 0x0000DC58 File Offset: 0x0000BE58
		public AzureNewRegistrationIdRequest(AzureDeviceRegistrationNotification notification, AzureSasToken sasToken, string resourceUri) : base("application/atom+xml;type=entry;charset=utf-8", "POST", sasToken, resourceUri, "")
		{
			base.RequestBody = string.Empty;
		}

		// Token: 0x040001E3 RID: 483
		public const string NewRegistrationIdContentType = "application/atom+xml;type=entry;charset=utf-8";
	}
}
