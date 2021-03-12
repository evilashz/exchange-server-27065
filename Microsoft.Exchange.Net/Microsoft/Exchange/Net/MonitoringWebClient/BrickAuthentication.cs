using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using Microsoft.Exchange.Net.MonitoringWebClient.Owa.Parsers;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x02000797 RID: 1943
	internal class BrickAuthentication : BaseTestStep
	{
		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x060026C8 RID: 9928 RVA: 0x000520EE File Offset: 0x000502EE
		// (set) Token: 0x060026C9 RID: 9929 RVA: 0x000520F6 File Offset: 0x000502F6
		public Uri Uri { get; private set; }

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x060026CA RID: 9930 RVA: 0x000520FF File Offset: 0x000502FF
		// (set) Token: 0x060026CB RID: 9931 RVA: 0x00052107 File Offset: 0x00050307
		public string UserName { get; private set; }

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x060026CC RID: 9932 RVA: 0x00052110 File Offset: 0x00050310
		// (set) Token: 0x060026CD RID: 9933 RVA: 0x00052118 File Offset: 0x00050318
		public string UserDomain { get; private set; }

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x060026CE RID: 9934 RVA: 0x00052121 File Offset: 0x00050321
		// (set) Token: 0x060026CF RID: 9935 RVA: 0x00052129 File Offset: 0x00050329
		public AuthenticationParameters AuthenticationParameters { get; private set; }

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x060026D0 RID: 9936 RVA: 0x00052132 File Offset: 0x00050332
		// (set) Token: 0x060026D1 RID: 9937 RVA: 0x0005213A File Offset: 0x0005033A
		public ITestFactory TestFactory { get; private set; }

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x060026D2 RID: 9938 RVA: 0x00052143 File Offset: 0x00050343
		protected override TestId Id
		{
			get
			{
				return TestId.BrickAuthentication;
			}
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x00052147 File Offset: 0x00050347
		public BrickAuthentication(Uri uri, string userName, string userDomain, AuthenticationParameters authenticationParameters)
		{
			this.Uri = uri;
			this.UserName = userName;
			this.UserDomain = userDomain;
			this.AuthenticationParameters = authenticationParameters;
		}

		// Token: 0x060026D4 RID: 9940 RVA: 0x00052184 File Offset: 0x00050384
		protected override void StartTest()
		{
			if (this.AuthenticationParameters == null)
			{
				throw new ArgumentException("Authentication parameters cannot be null. Check if the parameter was created properly");
			}
			CommonAccessToken commonAccessToken = this.AuthenticationParameters.CommonAccessToken;
			this.session.PersistentHeaders.Add("X-CommonAccessToken", commonAccessToken.Serialize());
			this.session.AuthenticationData = new AuthenticationData?(new AuthenticationData
			{
				UseDefaultCredentials = true,
				Credentials = CredentialCache.DefaultNetworkCredentials.GetCredential(this.Uri, "Kerberos")
			});
			Uri uri = new Uri(this.Uri, "/owa/proxylogon.owa");
			RequestBody body = RequestBody.Format(commonAccessToken.Serialize(), new object[0]);
			this.session.BeginPost(this.Id, uri.ToString(), body, null, delegate(IAsyncResult resultObj)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.BrickCscPostResponseReceived), resultObj);
			}, null);
		}

		// Token: 0x060026D5 RID: 9941 RVA: 0x00052304 File Offset: 0x00050504
		private void BrickCscPostResponseReceived(IAsyncResult result)
		{
			this.session.EndPost<HttpStatusCode>(result, new HttpStatusCode[]
			{
				(HttpStatusCode)241,
				HttpStatusCode.Found
			}, delegate(HttpWebResponseWrapper response)
			{
				if (response.StatusCode == HttpStatusCode.Found && !OwaLanguageSelectionPage.ContainsLanguagePageRedirection(response))
				{
					throw new MissingKeywordException(MonitoringWebClientStrings.BrickAuthenticationMissingOkOrLanguageSelection, response.Request, response, HttpStatusCode.Found.ToString());
				}
				if (response.Headers["Set-Cookie"] == null || (response.Headers["Set-Cookie"].IndexOf("UserContext", StringComparison.OrdinalIgnoreCase) < 0 && response.Headers["Set-Cookie"].IndexOf("UC", StringComparison.OrdinalIgnoreCase) < 0))
				{
					throw new MissingKeywordException(MonitoringWebClientStrings.MissingUserContextCookie, response.Request, response, "User Context");
				}
				return response.StatusCode;
			});
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x060026D6 RID: 9942 RVA: 0x0005235C File Offset: 0x0005055C
		private byte[] GetSecurityContext(ClientSecurityContext clientSecurityContext)
		{
			SerializedSecurityAccessToken serializedSecurityAccessToken = new SerializedSecurityAccessToken();
			using (ClientSecurityContext clientSecurityContext2 = clientSecurityContext.Clone())
			{
				clientSecurityContext2.SetSecurityAccessToken(serializedSecurityAccessToken);
			}
			if (serializedSecurityAccessToken.GroupSids != null)
			{
				SidStringAndAttributes[] groupSids = serializedSecurityAccessToken.GroupSids;
			}
			if (serializedSecurityAccessToken.RestrictedGroupSids != null)
			{
				SidStringAndAttributes[] restrictedGroupSids = serializedSecurityAccessToken.RestrictedGroupSids;
			}
			byte[] bytes = serializedSecurityAccessToken.GetBytes();
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
				{
					gzipStream.Write(bytes, 0, bytes.Length);
				}
				memoryStream.Flush();
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x04002341 RID: 9025
		private const TestId ID = TestId.BrickAuthentication;

		// Token: 0x04002342 RID: 9026
		private const string OWAProxyLogonURL = "/owa/proxylogon.owa";
	}
}
