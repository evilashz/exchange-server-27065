using System;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B62 RID: 2914
	internal class TokenDecryptionException : WSTrustException
	{
		// Token: 0x06003E75 RID: 15989 RVA: 0x000A3047 File Offset: 0x000A1247
		public TokenDecryptionException() : base(WSTrustStrings.CannotDecryptToken)
		{
		}
	}
}
