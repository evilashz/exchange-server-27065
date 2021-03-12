using System;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000006 RID: 6
	public static class CertificateHelper
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000026EC File Offset: 0x000008EC
		public static X509Certificate2 GetExchangeCertificate(string subject)
		{
			X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
			x509Store.Open(OpenFlags.OpenExistingOnly);
			X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindBySubjectDistinguishedName, subject, true);
			x509Store.Close();
			if (x509Certificate2Collection != null && x509Certificate2Collection.Count > 0)
			{
				return x509Certificate2Collection[0];
			}
			return null;
		}
	}
}
