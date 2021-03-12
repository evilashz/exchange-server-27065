using System;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B61 RID: 2913
	internal class RequestSecurityTokenResponseException : WSTrustException
	{
		// Token: 0x06003E73 RID: 15987 RVA: 0x000A302C File Offset: 0x000A122C
		public RequestSecurityTokenResponseException() : base(WSTrustStrings.MalformedRequestSecurityTokenResponse)
		{
		}

		// Token: 0x06003E74 RID: 15988 RVA: 0x000A3039 File Offset: 0x000A1239
		public RequestSecurityTokenResponseException(Exception innerException) : base(WSTrustStrings.MalformedRequestSecurityTokenResponse, innerException)
		{
		}
	}
}
