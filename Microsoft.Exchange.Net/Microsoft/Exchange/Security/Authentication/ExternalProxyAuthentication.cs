using System;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000BBD RID: 3005
	// (Invoke) Token: 0x06004074 RID: 16500
	internal delegate SecurityStatus ExternalProxyAuthentication(byte[] userid, byte[] password, Guid requestId, out string commonAccessToken, out IAccountValidationContext accountValidationContext);
}
