using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x020000F3 RID: 243
	public interface ICredentialsFactory
	{
		// Token: 0x0600068E RID: 1678
		ICredentials GetCredential(TenantContext tenantContext);

		// Token: 0x0600068F RID: 1679
		X509Certificate2 GetCredential(string certName);
	}
}
