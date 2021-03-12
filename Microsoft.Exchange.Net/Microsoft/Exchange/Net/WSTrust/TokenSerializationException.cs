using System;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B65 RID: 2917
	internal class TokenSerializationException : WSTrustException
	{
		// Token: 0x06003E78 RID: 15992 RVA: 0x000A3070 File Offset: 0x000A1270
		public TokenSerializationException(Exception innerException) : base(WSTrustStrings.FailedToSerializeToken(innerException), innerException)
		{
		}
	}
}
