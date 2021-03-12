using System;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x020000F4 RID: 244
	public sealed class CredentialsFactory : ICredentialsFactory
	{
		// Token: 0x06000690 RID: 1680 RVA: 0x000147AE File Offset: 0x000129AE
		public ICredentials GetCredential(TenantContext tenantContext)
		{
			throw new InvalidOperationException("must call derived-class for oauth authentication");
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x000147BC File Offset: 0x000129BC
		public X509Certificate2 GetCredential(string certificateSubject)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("certificateSubject", certificateSubject);
			X509Store x509Store = null;
			X509Certificate2 result = null;
			try
			{
				x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
				x509Store.Open(OpenFlags.OpenExistingOnly);
				X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindBySubjectDistinguishedName, certificateSubject, false);
				if (x509Certificate2Collection == null || x509Certificate2Collection.Count == 0)
				{
					throw new ArgumentException("The cert " + certificateSubject + " is not found", "certificateSubject");
				}
				result = x509Certificate2Collection[0];
			}
			catch (CryptographicException ex)
			{
				throw new SyncAgentTransientException(ex.Message, ex);
			}
			finally
			{
				if (x509Store != null)
				{
					x509Store.Close();
				}
			}
			return result;
		}
	}
}
