using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000697 RID: 1687
	internal class AuthMetadataClient
	{
		// Token: 0x06004E77 RID: 20087 RVA: 0x00120EE9 File Offset: 0x0011F0E9
		static AuthMetadataClient()
		{
			CertificateValidationManager.RegisterCallback("Microsoft.Exchange.Data.Directory.SystemConfiguration.AuthMetadataClient.NoSsl", new RemoteCertificateValidationCallback(AuthMetadataClient.ServerCertificateValidatorIgnoreSslErrors));
			CertificateValidationManager.RegisterCallback("Microsoft.Exchange.Data.Directory.SystemConfiguration.AuthMetadataClient", new RemoteCertificateValidationCallback(AuthMetadataClient.ServerCertificateValidator));
		}

		// Token: 0x06004E78 RID: 20088 RVA: 0x00120F17 File Offset: 0x0011F117
		private static bool ServerCertificateValidator(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return sslPolicyErrors == SslPolicyErrors.None;
		}

		// Token: 0x06004E79 RID: 20089 RVA: 0x00120F1D File Offset: 0x0011F11D
		private static bool ServerCertificateValidatorIgnoreSslErrors(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}

		// Token: 0x06004E7A RID: 20090 RVA: 0x00120F20 File Offset: 0x0011F120
		public AuthMetadataClient(string url, bool trustSslCert)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			this.Url = url;
			this.trustSslCert = trustSslCert;
			this.UserAgent = "MicrosoftExchangeAuthManagement";
		}

		// Token: 0x170019BE RID: 6590
		// (get) Token: 0x06004E7B RID: 20091 RVA: 0x00120F4F File Offset: 0x0011F14F
		// (set) Token: 0x06004E7C RID: 20092 RVA: 0x00120F57 File Offset: 0x0011F157
		public string Url { get; private set; }

		// Token: 0x170019BF RID: 6591
		// (get) Token: 0x06004E7D RID: 20093 RVA: 0x00120F60 File Offset: 0x0011F160
		// (set) Token: 0x06004E7E RID: 20094 RVA: 0x00120F68 File Offset: 0x0011F168
		public string UserAgent { get; set; }

		// Token: 0x170019C0 RID: 6592
		// (get) Token: 0x06004E7F RID: 20095 RVA: 0x00120F71 File Offset: 0x0011F171
		// (set) Token: 0x06004E80 RID: 20096 RVA: 0x00120F79 File Offset: 0x0011F179
		public int Timeout { get; set; }

		// Token: 0x06004E81 RID: 20097 RVA: 0x00120F84 File Offset: 0x0011F184
		public static AuthMetadata AcquireMetadata(string authMetadataUrl, bool requireIssuingEndpoint, bool trustSslCert, bool wrapException = true)
		{
			AuthMetadataClient authMetadataClient = new AuthMetadataClient(authMetadataUrl, trustSslCert);
			string content = authMetadataClient.Acquire(wrapException);
			AuthMetadata authMetadata;
			switch (AuthMetadataParser.DecideMetadataDocumentType(authMetadataUrl))
			{
			case AuthMetadataParser.MetadataDocType.OAuthS2SV1Metadata:
				return AuthMetadataParser.GetAuthMetadata(content, requireIssuingEndpoint);
			case AuthMetadataParser.MetadataDocType.WSFedMetadata:
				return AuthMetadataParser.GetWSFederationMetadata(content);
			case AuthMetadataParser.MetadataDocType.OAuthOpenIdConnectMetadata:
				authMetadata = AuthMetadataParser.GetOpenIdConnectAuthMetadata(content, requireIssuingEndpoint);
				if (!string.IsNullOrEmpty(authMetadata.KeysEndpoint))
				{
					authMetadataClient = new AuthMetadataClient(authMetadata.KeysEndpoint, trustSslCert);
					content = authMetadataClient.Acquire(wrapException);
					return AuthMetadataParser.GetOpenIdConnectKeys(content, authMetadata);
				}
				return authMetadata;
			}
			authMetadata = AuthMetadataParser.GetAuthMetadata(content, requireIssuingEndpoint);
			return authMetadata;
		}

		// Token: 0x06004E82 RID: 20098 RVA: 0x00121018 File Offset: 0x0011F218
		public string Acquire(bool wrapException = true)
		{
			if (wrapException)
			{
				try
				{
					return this.InternalAcquire();
				}
				catch (WebException ex)
				{
					WebResponse response = ex.Response;
					if (response != null)
					{
						if (response.Headers != null)
						{
							ExTraceGlobals.OAuthTracer.TraceError<WebHeaderCollection>((long)this.GetHashCode(), "[AuthMetadataClient:Acquire] response headers were {0}", response.Headers);
						}
						using (Stream responseStream = response.GetResponseStream())
						{
							using (StreamReader streamReader = new StreamReader(responseStream))
							{
								string arg = streamReader.ReadToEnd();
								ExTraceGlobals.OAuthTracer.TraceError<string>((long)this.GetHashCode(), "[AuthMetadataClient:Acquire] response content was {0}", arg);
							}
						}
					}
					throw new AuthMetadataClientException(DirectoryStrings.ErrorCannotAcquireAuthMetadata(this.Url, ex.Message), ex);
				}
			}
			return this.InternalAcquire();
		}

		// Token: 0x06004E83 RID: 20099 RVA: 0x001210F8 File Offset: 0x0011F2F8
		private string InternalAcquire()
		{
			string result;
			using (AuthMetadataClient.TimeOutWebClient timeOutWebClient = new AuthMetadataClient.TimeOutWebClient(this.trustSslCert))
			{
				if (this.Timeout != 0)
				{
					timeOutWebClient.Timeout = this.Timeout;
				}
				Server localServer = LocalServerCache.LocalServer;
				if (localServer != null && localServer.InternetWebProxy != null)
				{
					ExTraceGlobals.OAuthTracer.TraceDebug<Uri>((long)this.GetHashCode(), "[AuthMetadataClient:InternalAcquire] Using custom InternetWebProxy {0}", localServer.InternetWebProxy);
					timeOutWebClient.Proxy = new WebProxy(localServer.InternetWebProxy, true);
				}
				else
				{
					ExTraceGlobals.OAuthTracer.TraceDebug((long)this.GetHashCode(), "[AuthMetadataClient:InternalAcquire] Using null proxy");
					timeOutWebClient.Proxy = new WebProxy();
				}
				if (!string.IsNullOrEmpty(this.UserAgent))
				{
					timeOutWebClient.Headers.Add(HttpRequestHeader.UserAgent, this.UserAgent);
				}
				result = timeOutWebClient.DownloadString(this.Url);
			}
			return result;
		}

		// Token: 0x04003561 RID: 13665
		public const string CertificateValidationComponentId = "Microsoft.Exchange.Data.Directory.SystemConfiguration.AuthMetadataClient";

		// Token: 0x04003562 RID: 13666
		public const string CertificateValidationComponentIdNoSsl = "Microsoft.Exchange.Data.Directory.SystemConfiguration.AuthMetadataClient.NoSsl";

		// Token: 0x04003563 RID: 13667
		public const string AuthMetadataClientUserAgent = "MicrosoftExchangeAuthManagement";

		// Token: 0x04003564 RID: 13668
		private readonly bool trustSslCert;

		// Token: 0x02000698 RID: 1688
		private class TimeOutWebClient : WebClient
		{
			// Token: 0x06004E84 RID: 20100 RVA: 0x001211D8 File Offset: 0x0011F3D8
			public TimeOutWebClient(bool trustSslCert)
			{
				this.trustSslCert = trustSslCert;
			}

			// Token: 0x170019C1 RID: 6593
			// (get) Token: 0x06004E85 RID: 20101 RVA: 0x001211E7 File Offset: 0x0011F3E7
			// (set) Token: 0x06004E86 RID: 20102 RVA: 0x001211EF File Offset: 0x0011F3EF
			public int Timeout { get; set; }

			// Token: 0x06004E87 RID: 20103 RVA: 0x001211F8 File Offset: 0x0011F3F8
			protected override WebRequest GetWebRequest(Uri uri)
			{
				WebRequest webRequest = base.GetWebRequest(uri);
				if (webRequest is HttpWebRequest)
				{
					HttpWebRequest request = webRequest as HttpWebRequest;
					if (this.trustSslCert)
					{
						CertificateValidationManager.SetComponentId(request, "Microsoft.Exchange.Data.Directory.SystemConfiguration.AuthMetadataClient.NoSsl");
					}
					else
					{
						CertificateValidationManager.SetComponentId(request, "Microsoft.Exchange.Data.Directory.SystemConfiguration.AuthMetadataClient");
					}
				}
				if (this.Timeout != 0)
				{
					webRequest.Timeout = this.Timeout;
				}
				return webRequest;
			}

			// Token: 0x04003568 RID: 13672
			private readonly bool trustSslCert;
		}
	}
}
