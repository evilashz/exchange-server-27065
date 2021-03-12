using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.SecurityTokenService;

namespace Microsoft.Exchange.Management.ControlPanel.Psws
{
	// Token: 0x0200036A RID: 874
	internal static class CertificateStore
	{
		// Token: 0x06002FF4 RID: 12276 RVA: 0x00091F8C File Offset: 0x0009018C
		public static X509SigningCredentials GetSigningCredentials(string certSubject)
		{
			X509Certificate2 orAdd = CertificateStore.certificates.GetOrAdd(certSubject, new Func<string, X509Certificate2>(CertificateStore.LoadCertificate));
			return new X509SigningCredentials(orAdd, "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256", "http://www.w3.org/2001/04/xmlenc#sha256");
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x00091FC4 File Offset: 0x000901C4
		private static X509Certificate2 LoadCertificate(string certificateSubject)
		{
			X509Store x509Store = new X509Store(StoreLocation.LocalMachine);
			X509Certificate2 result;
			try
			{
				x509Store.Open(OpenFlags.ReadOnly);
				X509Certificate2 x509Certificate = CertificateStore.FindLatestCertificate(x509Store, X509FindType.FindBySubjectName, certificateSubject) ?? CertificateStore.FindLatestCertificate(x509Store, X509FindType.FindBySubjectDistinguishedName, certificateSubject);
				if (x509Certificate == null)
				{
					throw new InvalidOperationException("Unable to load certificate.");
				}
				result = x509Certificate;
			}
			finally
			{
				x509Store.Close();
			}
			return result;
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x00092028 File Offset: 0x00090228
		private static X509Certificate2 FindLatestCertificate(X509Store store, X509FindType findType, string certificateName)
		{
			X509Certificate2Collection source = store.Certificates.Find(findType, certificateName, true);
			return (from X509Certificate2 cert in source
			orderby cert.NotAfter descending
			select cert).FirstOrDefault<X509Certificate2>();
		}

		// Token: 0x0400232B RID: 9003
		private static ConcurrentDictionary<string, X509Certificate2> certificates = new ConcurrentDictionary<string, X509Certificate2>();
	}
}
