using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000067 RID: 103
	internal class AzureRegistrationChallengeRequest : AzureRequestBase
	{
		// Token: 0x060003C0 RID: 960 RVA: 0x0000C8A4 File Offset: 0x0000AAA4
		public AzureRegistrationChallengeRequest(AzureChallengeRequestNotification notification, AzureSasToken sasToken, string resourceUri) : base("application/json", "POST", sasToken, resourceUri, "")
		{
			ArgumentValidator.ThrowIfNull("notification", notification);
			base.Headers["TrackingId"] = notification.Identifier;
			base.RequestBody = notification.SerializedPaylod;
		}

		// Token: 0x040001AC RID: 428
		public const string IssueSecretContentType = "application/json";
	}
}
