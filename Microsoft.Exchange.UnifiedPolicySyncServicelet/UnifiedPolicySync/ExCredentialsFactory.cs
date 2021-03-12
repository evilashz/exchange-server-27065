using System;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Office.CompliancePolicy;
using Microsoft.Office.CompliancePolicy.PolicySync;

namespace Microsoft.Exchange.Servicelets.UnifiedPolicySync
{
	// Token: 0x02000006 RID: 6
	public sealed class ExCredentialsFactory : ICredentialsFactory
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002D5C File Offset: 0x00000F5C
		public ICredentials GetCredential(TenantContext tenantContext)
		{
			if (tenantContext == null)
			{
				throw new ArgumentNullException("tenantContext");
			}
			OrganizationId organizationId = null;
			try
			{
				organizationId = OrganizationId.FromExternalDirectoryOrganizationId(tenantContext.TenantId);
			}
			catch (TransientException innerException)
			{
				throw new SyncAgentTransientException(string.Format("FromExternalDirectoryOrganizationId failed with TransientException for tenant {0}", tenantContext.TenantId), innerException);
			}
			catch (DataSourceOperationException innerException2)
			{
				throw new SyncAgentPermanentException(string.Format("FromExternalDirectoryOrganizationId failed with DataSourceOperationException for tenant {0}", tenantContext.TenantId), innerException2);
			}
			catch (DataValidationException innerException3)
			{
				throw new SyncAgentPermanentException(string.Format("FromExternalDirectoryOrganizationId failed with DataValidationException for tenant {0}", tenantContext.TenantId), innerException3);
			}
			return OAuthCredentials.GetOAuthCredentialsForAppToken(organizationId, "PlaceHolder");
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002E14 File Offset: 0x00001014
		public X509Certificate2 GetCredential(string certificateSubject)
		{
			if (string.IsNullOrEmpty(certificateSubject))
			{
				throw new ArgumentNullException("certificateSubject");
			}
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
