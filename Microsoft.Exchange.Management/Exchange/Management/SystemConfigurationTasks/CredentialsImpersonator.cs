using System;
using System.Net;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000802 RID: 2050
	internal sealed class CredentialsImpersonator
	{
		// Token: 0x0600476D RID: 18285 RVA: 0x00125B5A File Offset: 0x00123D5A
		public CredentialsImpersonator()
		{
		}

		// Token: 0x0600476E RID: 18286 RVA: 0x00125B62 File Offset: 0x00123D62
		public CredentialsImpersonator(ICredentials credentials)
		{
			this.credentials = credentials;
		}

		// Token: 0x0600476F RID: 18287 RVA: 0x00125B74 File Offset: 0x00123D74
		public void Impersonate(ImpersonateDelegate impersonateDelegate)
		{
			if (this.credentials != null)
			{
				impersonateDelegate(this.credentials);
				return;
			}
			NetworkServiceImpersonator.Initialize();
			if (NetworkServiceImpersonator.Exception != null)
			{
				impersonateDelegate(CredentialCache.DefaultNetworkCredentials);
				return;
			}
			using (NetworkServiceImpersonator.Impersonate())
			{
				impersonateDelegate(CredentialCache.DefaultNetworkCredentials);
			}
		}

		// Token: 0x04002B25 RID: 11045
		private ICredentials credentials;
	}
}
