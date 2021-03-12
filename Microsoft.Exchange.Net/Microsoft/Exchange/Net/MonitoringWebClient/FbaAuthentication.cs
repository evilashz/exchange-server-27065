using System;
using System.Collections.Generic;
using System.Net;
using System.Security;
using System.Threading;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x02000799 RID: 1945
	internal class FbaAuthentication : BaseTestStep
	{
		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x060026E4 RID: 9956 RVA: 0x00052774 File Offset: 0x00050974
		// (set) Token: 0x060026E5 RID: 9957 RVA: 0x0005277C File Offset: 0x0005097C
		public Uri Uri { get; private set; }

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x060026E6 RID: 9958 RVA: 0x00052785 File Offset: 0x00050985
		// (set) Token: 0x060026E7 RID: 9959 RVA: 0x0005278D File Offset: 0x0005098D
		public string UserName { get; private set; }

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x060026E8 RID: 9960 RVA: 0x00052796 File Offset: 0x00050996
		// (set) Token: 0x060026E9 RID: 9961 RVA: 0x0005279E File Offset: 0x0005099E
		public string UserDomain { get; private set; }

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x060026EA RID: 9962 RVA: 0x000527A7 File Offset: 0x000509A7
		// (set) Token: 0x060026EB RID: 9963 RVA: 0x000527AF File Offset: 0x000509AF
		public SecureString Password { get; private set; }

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x060026EC RID: 9964 RVA: 0x000527B8 File Offset: 0x000509B8
		// (set) Token: 0x060026ED RID: 9965 RVA: 0x000527C0 File Offset: 0x000509C0
		public ITestFactory TestFactory { get; private set; }

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x060026EE RID: 9966 RVA: 0x000527C9 File Offset: 0x000509C9
		protected override TestId Id
		{
			get
			{
				return TestId.FbaAuthentication;
			}
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x060026EF RID: 9967 RVA: 0x000527CD File Offset: 0x000509CD
		// (set) Token: 0x060026F0 RID: 9968 RVA: 0x000527D5 File Offset: 0x000509D5
		public AuthenticationParameters AuthenticationParameters { get; set; }

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x060026F1 RID: 9969 RVA: 0x000527DE File Offset: 0x000509DE
		public override object Result
		{
			get
			{
				return this.Uri;
			}
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x000527E6 File Offset: 0x000509E6
		public FbaAuthentication(Uri uri, string userName, string userDomain, SecureString password, AuthenticationParameters authenticationParameters, TestFactory testFactory)
		{
			this.Uri = uri;
			this.UserName = userName;
			this.UserDomain = userDomain;
			this.Password = password;
			this.AuthenticationParameters = authenticationParameters;
			this.TestFactory = testFactory;
		}

		// Token: 0x060026F3 RID: 9971 RVA: 0x00052837 File Offset: 0x00050A37
		protected override void StartTest()
		{
			this.session.BeginGetFollowingRedirections(this.Id, this.Uri.ToString(), delegate(IAsyncResult result)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.FbaRedirectToLogonPageReceived), result);
			}, null);
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x00052880 File Offset: 0x00050A80
		private void FbaRedirectToLogonPageReceived(IAsyncResult result)
		{
			FbaRedirectPage fbaRedirectPage = this.session.EndGetFollowingRedirections<FbaRedirectPage>(result, (HttpWebResponseWrapper response) => FbaRedirectPage.Parse(response));
			Uri uri = new Uri(this.Uri, fbaRedirectPage.FbaLogonPagePathAndQuery);
			this.session.BeginGet(this.Id, uri.OriginalString, delegate(IAsyncResult resultObj)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.FbaLogonPageReceived), resultObj);
			}, null);
		}

		// Token: 0x060026F5 RID: 9973 RVA: 0x0005290C File Offset: 0x00050B0C
		private void FbaLogonPageReceived(IAsyncResult result)
		{
			FbaLogonPage fbaLogonPage = this.session.EndGet<FbaLogonPage>(result, (HttpWebResponseWrapper response) => FbaLogonPage.Parse(response));
			if (fbaLogonPage.StaticFileUri != null && this.AuthenticationParameters.ShouldDownloadStaticFileOnLogonPage)
			{
				this.simultaneousStepCount = 2;
				ITestStep testStep = this.TestFactory.CreateOwaDownloadStaticFileStep(fbaLogonPage.StaticFileUri);
				testStep.BeginExecute(this.session, delegate(IAsyncResult tempResult)
				{
					base.AsyncCallbackWrapper(new AsyncCallback(this.DownloadStaticFileStepCompleted), tempResult);
				}, testStep);
			}
			Cookie cookie = new Cookie("PBack", "0");
			this.session.CookieContainer.Add(cookie);
			RequestBody requestBody = RequestBody.Format("{0}&username={1}&password={2}", new object[]
			{
				fbaLogonPage.HiddenFieldsString,
				this.UserName,
				this.Password
			});
			this.PostCredentials(requestBody, fbaLogonPage, 0);
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x00052A10 File Offset: 0x00050C10
		private void PostCredentials(RequestBody requestBody, FbaLogonPage fbaLogonPage, int credentialPostCount = 0)
		{
			Uri uri = new Uri(this.Uri, fbaLogonPage.PostTarget);
			this.session.BeginPost(this.Id, uri.ToString(), requestBody, "application/x-www-form-urlencoded", delegate(IAsyncResult resultObj)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.FbaCredentialPostResponseReceived), resultObj);
			}, new Dictionary<string, object>
			{
				{
					"FbaLogonPage",
					fbaLogonPage
				},
				{
					"CredentialPostCount",
					credentialPostCount
				}
			});
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x00052B38 File Offset: 0x00050D38
		private void FbaCredentialPostResponseReceived(IAsyncResult result)
		{
			Dictionary<string, object> dictionary = result.AsyncState as Dictionary<string, object>;
			FbaLogonPage fbaLogonPage = dictionary["FbaLogonPage"] as FbaLogonPage;
			int credentialPostCount = (int)dictionary["CredentialPostCount"];
			FbaSilentRedirectPage fbaSilentRedirectPage = this.session.EndPost<FbaSilentRedirectPage>(result, delegate(HttpWebResponseWrapper response)
			{
				FbaSilentRedirectPage result2 = null;
				if (credentialPostCount < 5 && FbaSilentRedirectPage.TryParse(response, out result2))
				{
					return result2;
				}
				FbaLogonErrorPage fbaLogonErrorPage = null;
				if (FbaLogonErrorPage.TryParse(response, out fbaLogonErrorPage))
				{
					throw new LogonException(MonitoringWebClientStrings.LogonError(fbaLogonErrorPage.ErrorString), response.Request, response, fbaLogonErrorPage);
				}
				if (response.Headers["Set-Cookie"] == null || response.Headers["Set-Cookie"].IndexOf("cadata", StringComparison.OrdinalIgnoreCase) < 0 || response.Headers["Set-Cookie"].IndexOf("cadata=;", StringComparison.OrdinalIgnoreCase) >= 0)
				{
					throw new MissingKeywordException(MonitoringWebClientStrings.MissingFbaAuthCookies, response.Request, response, "CAData cookie");
				}
				return null;
			});
			if (fbaSilentRedirectPage != null)
			{
				this.Uri = fbaSilentRedirectPage.Destination;
				RequestBody requestBody = RequestBody.Format("{0}", new object[]
				{
					fbaSilentRedirectPage.HiddenFieldsString
				});
				this.PostCredentials(requestBody, fbaLogonPage, credentialPostCount + 1);
				return;
			}
			this.FinishTestStep();
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x00052BE0 File Offset: 0x00050DE0
		private void DownloadStaticFileStepCompleted(IAsyncResult result)
		{
			ITestStep testStep = result.AsyncState as ITestStep;
			testStep.EndExecute(result);
			this.FinishTestStep();
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x00052C08 File Offset: 0x00050E08
		private void FinishTestStep()
		{
			int num = Interlocked.Increment(ref this.currentFinishedStepCount);
			if (num >= this.simultaneousStepCount)
			{
				base.ExecutionCompletedSuccessfully();
			}
		}

		// Token: 0x0400234F RID: 9039
		private const TestId ID = TestId.FbaAuthentication;

		// Token: 0x04002350 RID: 9040
		private int simultaneousStepCount = 1;

		// Token: 0x04002351 RID: 9041
		private int currentFinishedStepCount;
	}
}
