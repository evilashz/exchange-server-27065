using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PswsClient;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x0200063B RID: 1595
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CommonAccessTokenAsAuthenticator : IAuthenticator
	{
		// Token: 0x06001CE5 RID: 7397 RVA: 0x0003409F File Offset: 0x0003229F
		public CommonAccessTokenAsAuthenticator(string token, IAuthenticator serviceAuthenticator)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("token", token);
			ArgumentValidator.ThrowIfNull("serviceAuthenticator", serviceAuthenticator);
			this.token = token;
			this.serviceAuthenticator = serviceAuthenticator;
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x000340CB File Offset: 0x000322CB
		public IDisposable Authenticate(HttpWebRequest request)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			request.Headers["X-CommonAccessToken"] = this.token;
			return this.serviceAuthenticator.Authenticate(request);
		}

		// Token: 0x04001D2C RID: 7468
		private readonly string token;

		// Token: 0x04001D2D RID: 7469
		private readonly IAuthenticator serviceAuthenticator;
	}
}
