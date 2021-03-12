using System;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B64 RID: 2916
	internal class UnknownTokenIssuerException : WSTrustException
	{
		// Token: 0x06003E77 RID: 15991 RVA: 0x000A3061 File Offset: 0x000A1261
		public UnknownTokenIssuerException(string federatedTokenIssuerUri, string targetTokenIssuerUri) : base(WSTrustStrings.UnknownTokenIssuerException(federatedTokenIssuerUri, targetTokenIssuerUri))
		{
		}
	}
}
