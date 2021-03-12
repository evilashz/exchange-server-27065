using System;
using System.Net;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x0200000A RID: 10
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class OAuthCredentialFactory : IOAuthCredentialFactory
	{
		// Token: 0x0600003F RID: 63 RVA: 0x000026FA File Offset: 0x000008FA
		public ICredentials Get(OrganizationId organizationId)
		{
			return OAuthCredentials.GetOAuthCredentialsForAppToken(organizationId, "PlaceHolder");
		}
	}
}
