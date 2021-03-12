using System;
using System.Net;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PswsClient;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000034 RID: 52
	internal class Authenticator : IAuthenticator
	{
		// Token: 0x06000278 RID: 632 RVA: 0x0000CC5C File Offset: 0x0000AE5C
		private Authenticator(ICredentials credentials)
		{
			this.credentials = credentials;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000CC6C File Offset: 0x0000AE6C
		public static IAuthenticator Create(OrganizationId organizationId, ADObjectId executingUserId)
		{
			ArgumentValidator.ThrowIfNull("organizationId", organizationId);
			ArgumentValidator.ThrowIfNull("executingUserId", executingUserId);
			ADSessionSettings sessionSettings = ADSessionSettings.FromExternalDirectoryOrganizationId(new Guid(organizationId.ToExternalDirectoryOrganizationId()));
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 54, "Create", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\EOPRecipient\\Authenticator.cs");
			ADUser actAsUser = tenantOrRootOrgRecipientSession.FindADUserByObjectId(executingUserId);
			OAuthCredentials oauthCredentialsForAppActAsToken = OAuthCredentials.GetOAuthCredentialsForAppActAsToken(organizationId, actAsUser, null);
			return new Authenticator(oauthCredentialsForAppActAsToken);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000CCD0 File Offset: 0x0000AED0
		public IDisposable Authenticate(HttpWebRequest request)
		{
			this.authenticateExecuted = true;
			request.Credentials = this.credentials;
			request.PreAuthenticate = true;
			return null;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000CCED File Offset: 0x0000AEED
		public override string ToString()
		{
			return string.Format("Authenticator.Credentials? {0}; Authenticator.AuthenticateExecuted = {1}.", this.credentials != null, this.authenticateExecuted);
		}

		// Token: 0x04000055 RID: 85
		private readonly ICredentials credentials;

		// Token: 0x04000056 RID: 86
		private bool authenticateExecuted;
	}
}
