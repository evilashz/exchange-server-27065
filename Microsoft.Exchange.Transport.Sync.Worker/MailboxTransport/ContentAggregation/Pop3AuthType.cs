using System;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x020001F4 RID: 500
	internal enum Pop3AuthType
	{
		// Token: 0x04000958 RID: 2392
		Basic,
		// Token: 0x04000959 RID: 2393
		Ntlm,
		// Token: 0x0400095A RID: 2394
		SSL = 16,
		// Token: 0x0400095B RID: 2395
		NtlmOverSSL,
		// Token: 0x0400095C RID: 2396
		TLS = 32,
		// Token: 0x0400095D RID: 2397
		NtlmOverTLS
	}
}
