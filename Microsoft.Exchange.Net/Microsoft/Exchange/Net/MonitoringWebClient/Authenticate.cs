using System;
using System.Collections.Generic;
using System.Net;
using System.Security;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x02000795 RID: 1941
	internal class Authenticate : BaseTestStep
	{
		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06002699 RID: 9881 RVA: 0x00051873 File Offset: 0x0004FA73
		// (set) Token: 0x0600269A RID: 9882 RVA: 0x0005187B File Offset: 0x0004FA7B
		public Uri Uri { get; private set; }

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x0600269B RID: 9883 RVA: 0x00051884 File Offset: 0x0004FA84
		// (set) Token: 0x0600269C RID: 9884 RVA: 0x0005188C File Offset: 0x0004FA8C
		public string UserName { get; private set; }

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x0600269D RID: 9885 RVA: 0x00051895 File Offset: 0x0004FA95
		// (set) Token: 0x0600269E RID: 9886 RVA: 0x0005189D File Offset: 0x0004FA9D
		public string UserDomain { get; private set; }

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x0600269F RID: 9887 RVA: 0x000518A6 File Offset: 0x0004FAA6
		// (set) Token: 0x060026A0 RID: 9888 RVA: 0x000518AE File Offset: 0x0004FAAE
		public SecureString Password { get; private set; }

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x060026A1 RID: 9889 RVA: 0x000518B7 File Offset: 0x0004FAB7
		// (set) Token: 0x060026A2 RID: 9890 RVA: 0x000518BF File Offset: 0x0004FABF
		public ITestFactory TestFactory { get; private set; }

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x060026A3 RID: 9891 RVA: 0x000518C8 File Offset: 0x0004FAC8
		// (set) Token: 0x060026A4 RID: 9892 RVA: 0x000518D0 File Offset: 0x0004FAD0
		public AuthenticationParameters AuthenticationParameters { get; private set; }

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x060026A5 RID: 9893 RVA: 0x000518D9 File Offset: 0x0004FAD9
		protected override TestId Id
		{
			get
			{
				return TestId.Authentication;
			}
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x060026A6 RID: 9894 RVA: 0x000518DD File Offset: 0x0004FADD
		public override object Result
		{
			get
			{
				return this.authenticationResult;
			}
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x000518E5 File Offset: 0x0004FAE5
		public Authenticate(Uri uri, string userName, string userDomain, SecureString password, AuthenticationParameters authenticationParameters, ITestFactory factory)
		{
			this.Uri = uri;
			this.UserName = userName;
			this.UserDomain = userDomain;
			this.Password = password;
			this.AuthenticationParameters = authenticationParameters;
			this.TestFactory = factory;
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x00051944 File Offset: 0x0004FB44
		protected override void StartTest()
		{
			if (this.Uri.Port == 444)
			{
				ITestStep testStep = this.TestFactory.CreateBrickAuthenticateStep(this.Uri, this.UserName, this.UserDomain, this.AuthenticationParameters);
				testStep.BeginExecute(this.session, delegate(IAsyncResult tempResult)
				{
					base.AsyncCallbackWrapper(new AsyncCallback(this.AuthenticationStepFinished), tempResult);
				}, testStep);
				return;
			}
			this.session.BeginGetFollowingRedirections(this.Id, this.Uri.ToString(), RedirectionOptions.StopOnFirstCrossDomainRedirect, delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.AuthenticationResponseReceived), tempResult);
			}, new Dictionary<string, object>
			{
				{
					"CafeErrorPageValidationRules",
					CafeErrorPageValidationRules.Accept401Response
				}
			});
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x00051B9C File Offset: 0x0004FD9C
		private void AuthenticationResponseReceived(IAsyncResult result)
		{
			var <>f__AnonymousType = this.session.EndGet(result, new HttpStatusCode[]
			{
				HttpStatusCode.OK,
				HttpStatusCode.Found,
				HttpStatusCode.Unauthorized
			}, (HttpWebResponseWrapper response) => new
			{
				StatusCode = response.StatusCode,
				RedirectUrl = response.Headers["Location"],
				LastUri = response.Request.RequestUri
			});
			ITestStep testStep;
			if (this.IsFbaAuthentication(<>f__AnonymousType.StatusCode, <>f__AnonymousType.LastUri))
			{
				testStep = this.TestFactory.CreateFbaAuthenticateStep(this.Uri, this.UserName, this.UserDomain, this.Password, this.AuthenticationParameters);
			}
			else if (this.IsLiveIdAuthentication(<>f__AnonymousType.StatusCode, <>f__AnonymousType.RedirectUrl))
			{
				testStep = this.TestFactory.CreateLiveIDAuthenticateStep(this.Uri, this.UserName, this.UserDomain, this.Password, this.AuthenticationParameters, this.TestFactory);
			}
			else
			{
				if (!this.IsIisAuthentication(<>f__AnonymousType.StatusCode))
				{
					throw new NotImplementedException("Authentication method was not recognized.");
				}
				testStep = this.TestFactory.CreateIisAuthenticateStep(this.Uri, this.UserName, this.UserDomain, this.Password);
			}
			testStep.BeginExecute(this.session, delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.AuthenticationStepFinished), tempResult);
			}, testStep);
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x00051CD8 File Offset: 0x0004FED8
		private void AuthenticationStepFinished(IAsyncResult result)
		{
			ITestStep testStep = result.AsyncState as ITestStep;
			testStep.EndExecute(result);
			this.authenticationResult = testStep.Result;
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x00051D0A File Offset: 0x0004FF0A
		private bool IsFbaAuthentication(HttpStatusCode httpStatusCode, Uri lastResponseUri)
		{
			return httpStatusCode == HttpStatusCode.OK && lastResponseUri.Host == this.Uri.Host;
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x00051D2C File Offset: 0x0004FF2C
		private bool IsLiveIdAuthentication(HttpStatusCode httpStatusCode, string redirectUrl)
		{
			return httpStatusCode == HttpStatusCode.Found;
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x00051D36 File Offset: 0x0004FF36
		private bool IsIisAuthentication(HttpStatusCode httpStatusCode)
		{
			return httpStatusCode == HttpStatusCode.Unauthorized;
		}

		// Token: 0x0400232F RID: 9007
		private const TestId ID = TestId.Authentication;

		// Token: 0x04002330 RID: 9008
		private const int BrickPort = 444;

		// Token: 0x04002331 RID: 9009
		private object authenticationResult;
	}
}
