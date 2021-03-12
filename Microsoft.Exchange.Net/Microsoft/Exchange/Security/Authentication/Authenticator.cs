using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PswsClient;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000636 RID: 1590
	internal static class Authenticator
	{
		// Token: 0x06001CD8 RID: 7384 RVA: 0x00033FEC File Offset: 0x000321EC
		public static IAuthenticator Create(ICredentials credentials)
		{
			ArgumentValidator.ThrowIfNull("credentials", credentials);
			return new Authenticator.CredentialsAuthenticator(credentials);
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x00033FFF File Offset: 0x000321FF
		public static IAuthenticator Create(X509Certificate2 certificate)
		{
			ArgumentValidator.ThrowIfNull("certificate", certificate);
			return new Authenticator.CertificateAuthenticator(certificate);
		}

		// Token: 0x04001D29 RID: 7465
		public static readonly IAuthenticator NetworkService = new Authenticator.NetworkServiceAuthenticator();

		// Token: 0x02000637 RID: 1591
		private sealed class NetworkServiceAuthenticator : IAuthenticator
		{
			// Token: 0x06001CDC RID: 7388 RVA: 0x00034028 File Offset: 0x00032228
			public IDisposable Authenticate(HttpWebRequest request)
			{
				NetworkServiceImpersonator.Initialize();
				IDisposable result = NetworkServiceImpersonator.Impersonate();
				request.Credentials = CredentialCache.DefaultCredentials;
				return result;
			}
		}

		// Token: 0x02000638 RID: 1592
		private sealed class CredentialsAuthenticator : IAuthenticator
		{
			// Token: 0x06001CDD RID: 7389 RVA: 0x0003404C File Offset: 0x0003224C
			public CredentialsAuthenticator(ICredentials credentials)
			{
				this.credentials = credentials;
			}

			// Token: 0x06001CDE RID: 7390 RVA: 0x0003405B File Offset: 0x0003225B
			public IDisposable Authenticate(HttpWebRequest request)
			{
				request.Credentials = this.credentials;
				return null;
			}

			// Token: 0x04001D2A RID: 7466
			private ICredentials credentials;
		}

		// Token: 0x02000639 RID: 1593
		private sealed class CertificateAuthenticator : IAuthenticator
		{
			// Token: 0x06001CDF RID: 7391 RVA: 0x0003406A File Offset: 0x0003226A
			public CertificateAuthenticator(X509Certificate2 certificate)
			{
				this.certificate = certificate;
			}

			// Token: 0x06001CE0 RID: 7392 RVA: 0x00034079 File Offset: 0x00032279
			public NetworkCredential GetCredential(Uri uri, string authType)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x06001CE1 RID: 7393 RVA: 0x00034080 File Offset: 0x00032280
			public IDisposable Authenticate(HttpWebRequest request)
			{
				request.ClientCertificates.Add(this.certificate);
				return null;
			}

			// Token: 0x04001D2B RID: 7467
			private readonly X509Certificate2 certificate;
		}
	}
}
