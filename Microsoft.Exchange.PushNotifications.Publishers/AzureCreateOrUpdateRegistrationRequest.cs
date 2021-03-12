using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000068 RID: 104
	internal class AzureCreateOrUpdateRegistrationRequest : AzureRequestBase
	{
		// Token: 0x060003C1 RID: 961 RVA: 0x0000C8F8 File Offset: 0x0000AAF8
		public AzureCreateOrUpdateRegistrationRequest(AzureDeviceRegistrationNotification notification, AzureSasToken sasToken, string resourceUri) : base("application/atom+xml;type=entry;charset=utf-8", "PUT", sasToken, resourceUri, "")
		{
			ArgumentValidator.ThrowIfNull("notification", notification);
			base.RequestBody = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?><entry xmlns=\"http://www.w3.org/2005/Atom\"><content type=\"application/xml\">{0}</content></entry>", notification.SerializedPaylod);
			if (!string.IsNullOrWhiteSpace(notification.ServerChallenge))
			{
				base.Headers["ServiceBusNotification-RegistrationSecret"] = notification.ServerChallenge;
			}
		}

		// Token: 0x040001AD RID: 429
		public const string NewRegistrationContentType = "application/atom+xml;type=entry;charset=utf-8";

		// Token: 0x040001AE RID: 430
		public const string MultiFactorChallengeHeaderName = "ServiceBusNotification-RegistrationSecret";

		// Token: 0x040001AF RID: 431
		private const string RequestBodyTemplate = "<?xml version=\"1.0\" encoding=\"utf-8\"?><entry xmlns=\"http://www.w3.org/2005/Atom\"><content type=\"application/xml\">{0}</content></entry>";
	}
}
