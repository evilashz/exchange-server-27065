using System;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x0200010B RID: 267
	internal interface IOAuthExtensionAuthenticationHandler
	{
		// Token: 0x060008C0 RID: 2240
		bool TryHandleRequestPreAuthentication(OAuthExtensionContext context, out bool isAuthenticationNeeded);

		// Token: 0x060008C1 RID: 2241
		bool TryGetBearerToken(OAuthExtensionContext context, out string token);

		// Token: 0x060008C2 RID: 2242
		bool TryHandleRequestPostAuthentication(OAuthExtensionContext context);
	}
}
