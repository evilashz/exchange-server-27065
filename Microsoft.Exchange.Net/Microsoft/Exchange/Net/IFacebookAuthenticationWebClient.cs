using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000713 RID: 1811
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IFacebookAuthenticationWebClient
	{
		// Token: 0x06002231 RID: 8753
		AuthenticateApplicationResponse AuthenticateApplication(Uri accessTokenEndpoint, TimeSpan requestTimeout);
	}
}
