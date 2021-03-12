using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000076 RID: 118
	internal class AzureNewRegistrationRequest : AzureRequestBase
	{
		// Token: 0x06000426 RID: 1062 RVA: 0x0000DD65 File Offset: 0x0000BF65
		public AzureNewRegistrationRequest(AzureNotification notification, AzureSasToken sasToken, string resourceUri, string registrationTemplate) : base("application/atom+xml;type=entry;charset=utf-8", "POST", sasToken, resourceUri, "")
		{
			ArgumentValidator.ThrowIfNull("notification", notification);
			base.RequestBody = AzureNewRegistrationRequest.CreateRequestBody(notification.RecipientId, notification.DeviceId, registrationTemplate);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000DDA2 File Offset: 0x0000BFA2
		public static string CreateRequestBody(string tags, string deviceToken, string registrationTemplate)
		{
			return string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?><entry xmlns=\"http://www.w3.org/2005/Atom\"><content type=\"application/xml\">{0}</content></entry>", string.Format(registrationTemplate, tags, deviceToken));
		}

		// Token: 0x040001E7 RID: 487
		public const string NewRegistrationContentType = "application/atom+xml;type=entry;charset=utf-8";

		// Token: 0x040001E8 RID: 488
		private const string RequestBodyTemplate = "<?xml version=\"1.0\" encoding=\"utf-8\"?><entry xmlns=\"http://www.w3.org/2005/Atom\"><content type=\"application/xml\">{0}</content></entry>";
	}
}
