using System;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B5E RID: 2910
	internal class HttpClientException : WSTrustException
	{
		// Token: 0x06003E67 RID: 15975 RVA: 0x000A2EA7 File Offset: 0x000A10A7
		public HttpClientException(Uri endpoint) : base(WSTrustStrings.HttpClientFailedToCommunicate(endpoint.ToString()))
		{
		}

		// Token: 0x06003E68 RID: 15976 RVA: 0x000A2EBA File Offset: 0x000A10BA
		public HttpClientException(Uri endpoint, Exception innerException) : base(WSTrustStrings.HttpClientFailedToCommunicate(endpoint.ToString()), innerException)
		{
		}
	}
}
