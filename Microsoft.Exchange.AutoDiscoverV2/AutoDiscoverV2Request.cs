using System;
using Microsoft.Exchange.Autodiscover;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AutoDiscoverV2
{
	// Token: 0x02000009 RID: 9
	public class AutoDiscoverV2Request
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002A28 File Offset: 0x00000C28
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002A30 File Offset: 0x00000C30
		public SmtpAddress EmailAddress { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002A39 File Offset: 0x00000C39
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002A41 File Offset: 0x00000C41
		public string Protocol { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002A4A File Offset: 0x00000C4A
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002A52 File Offset: 0x00000C52
		public string HostNameHint { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002A5B File Offset: 0x00000C5B
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002A63 File Offset: 0x00000C63
		public uint RedirectCount { get; set; }

		// Token: 0x0600003F RID: 63 RVA: 0x00002A6C File Offset: 0x00000C6C
		internal void ValidateRequest(string emailAddress, string protocol, uint redirectCount, RequestDetailsLogger logger)
		{
			if (string.IsNullOrWhiteSpace(protocol))
			{
				throw AutoDiscoverResponseException.BadRequest("MandatoryParameterMissing", "A valid value must be provided for the query parameter 'Protocol'", null);
			}
			if (string.IsNullOrWhiteSpace(emailAddress))
			{
				throw AutoDiscoverResponseException.BadRequest("MandatoryParameterMissing", "A valid smtp address must be provided", null);
			}
			logger.Set(ServiceCommonMetadata.AuthenticatedUser, emailAddress);
			logger.AppendGenericInfo("RequestedProtocol", protocol);
			if (!SmtpAddress.IsValidSmtpAddress(emailAddress))
			{
				throw AutoDiscoverResponseException.BadRequest("InvalidUserId", string.Format("The given SMTP address is invalid '{0}'", emailAddress), null);
			}
			if (redirectCount >= 10U)
			{
				logger.AppendGenericError("RedirectCountExceeded", "Redirect count exceeded for the given user");
				throw AutoDiscoverResponseException.NotFound();
			}
		}
	}
}
