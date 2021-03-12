using System;
using System.Net;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200082D RID: 2093
	internal static class OauthUtils
	{
		// Token: 0x06003C6F RID: 15471 RVA: 0x000D5CFA File Offset: 0x000D3EFA
		public static ICredentials GetOauthCredential(ADUser user)
		{
			return OAuthCredentials.GetOAuthCredentialsForAppActAsToken(user.OrganizationId, user, null);
		}

		// Token: 0x06003C70 RID: 15472 RVA: 0x000D5D09 File Offset: 0x000D3F09
		public static ICredentials GetOauthCredential(MiniRecipient user)
		{
			return OAuthCredentials.GetOAuthCredentialsForAppActAsToken(user.OrganizationId, user, null);
		}
	}
}
