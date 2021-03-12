using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Microsoft.Exchange.Management.FederationProvisioning
{
	// Token: 0x02000330 RID: 816
	internal static class FederatedDomainProofAlgorithm
	{
		// Token: 0x06001BC2 RID: 7106 RVA: 0x0007B53C File Offset: 0x0007973C
		public static byte[] GetSignature(X509Certificate2 certificate, string domain)
		{
			byte[] buffer = FederatedDomainProofAlgorithm.Canonicalize(domain);
			byte[] result;
			using (SHA1CryptoServiceProvider sha1CryptoServiceProvider = new SHA1CryptoServiceProvider())
			{
				RSACryptoServiceProvider rsacryptoServiceProvider = certificate.PrivateKey as RSACryptoServiceProvider;
				result = rsacryptoServiceProvider.SignData(buffer, sha1CryptoServiceProvider);
			}
			return result;
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x0007B588 File Offset: 0x00079788
		private static byte[] Canonicalize(string domain)
		{
			return Encoding.UTF8.GetBytes(domain.Trim().ToLowerInvariant());
		}

		// Token: 0x04000C21 RID: 3105
		public const string HashAlgorithmName = "SHA-512";
	}
}
