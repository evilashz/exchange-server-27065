using System;

namespace Microsoft.Exchange.Net.ExSmtpClient
{
	// Token: 0x02000700 RID: 1792
	internal enum SmtpAuthNegotiationState
	{
		// Token: 0x04002049 RID: 8265
		UnAuthenticated,
		// Token: 0x0400204A RID: 8266
		AuthenticationInProgressNoUserOrPasswordGiven,
		// Token: 0x0400204B RID: 8267
		AuthenticationInProgressUserGiven,
		// Token: 0x0400204C RID: 8268
		AuthenticationInProgressPasswordGiven,
		// Token: 0x0400204D RID: 8269
		Authenticated
	}
}
