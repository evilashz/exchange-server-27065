using System;
using System.Net;
using System.Net.Security;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.Services.OnlineMeetings.Autodiscover;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x0200003B RID: 59
	internal class OAuthRequestFactory : UcwaRequestFactory
	{
		// Token: 0x0600021D RID: 541 RVA: 0x00007B7D File Offset: 0x00005D7D
		static OAuthRequestFactory()
		{
			CertificateValidationManager.RegisterCallback("OnlineMeeting", new RemoteCertificateValidationCallback(AutodiscoverWorker.ServerCertificateValidator));
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00007B95 File Offset: 0x00005D95
		public OAuthRequestFactory(OAuthCredentials oauthCredentials)
		{
			this.oauthCredentials = oauthCredentials;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00007BA4 File Offset: 0x00005DA4
		public override string LandingPageToken
		{
			get
			{
				return "oauth";
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00007BAB File Offset: 0x00005DAB
		internal static string UserAgent
		{
			get
			{
				return string.Format("Exchange/{0}/OnlineMeeting", OAuthUtilities.ServerVersionString);
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00007BBC File Offset: 0x00005DBC
		protected override HttpWebRequest CreateHttpWebRequest(string httpMethod, string url)
		{
			HttpWebRequest httpWebRequest = base.CreateHttpWebRequest(httpMethod, url);
			httpWebRequest.Credentials = this.oauthCredentials;
			httpWebRequest.UserAgent = OAuthRequestFactory.UserAgent;
			if (this.oauthCredentials.ClientRequestId != null)
			{
				httpWebRequest.Headers.Add("client-request-id", this.oauthCredentials.ClientRequestId.Value.ToString());
				httpWebRequest.Headers.Add("return-client-request-id", bool.TrueString);
			}
			CertificateValidationManager.SetComponentId(httpWebRequest, "OnlineMeeting");
			return httpWebRequest;
		}

		// Token: 0x0400016B RID: 363
		internal const string CertificateValidationComponentId = "OnlineMeeting";

		// Token: 0x0400016C RID: 364
		private const string UserAgentFormat = "Exchange/{0}/OnlineMeeting";

		// Token: 0x0400016D RID: 365
		private readonly OAuthCredentials oauthCredentials;
	}
}
