using System;
using System.Security;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Ecp
{
	// Token: 0x020007A7 RID: 1959
	internal abstract class EcpFeatureTestStepBase : BaseTestStep
	{
		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06002792 RID: 10130 RVA: 0x00053FC6 File Offset: 0x000521C6
		// (set) Token: 0x06002793 RID: 10131 RVA: 0x00053FCE File Offset: 0x000521CE
		public Uri Uri { get; private set; }

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06002794 RID: 10132 RVA: 0x00053FD7 File Offset: 0x000521D7
		// (set) Token: 0x06002795 RID: 10133 RVA: 0x00053FDF File Offset: 0x000521DF
		public string UserName { get; private set; }

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06002796 RID: 10134 RVA: 0x00053FE8 File Offset: 0x000521E8
		// (set) Token: 0x06002797 RID: 10135 RVA: 0x00053FF0 File Offset: 0x000521F0
		public string UserDomain { get; private set; }

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06002798 RID: 10136 RVA: 0x00053FF9 File Offset: 0x000521F9
		// (set) Token: 0x06002799 RID: 10137 RVA: 0x00054001 File Offset: 0x00052201
		public SecureString Password { get; private set; }

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x0600279A RID: 10138 RVA: 0x0005400A File Offset: 0x0005220A
		// (set) Token: 0x0600279B RID: 10139 RVA: 0x00054012 File Offset: 0x00052212
		public AuthenticationParameters AuthenticationParameters { get; private set; }

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x0600279C RID: 10140 RVA: 0x0005401B File Offset: 0x0005221B
		// (set) Token: 0x0600279D RID: 10141 RVA: 0x00054023 File Offset: 0x00052223
		public ITestFactory TestFactory { get; private set; }

		// Token: 0x0600279E RID: 10142 RVA: 0x0005402C File Offset: 0x0005222C
		public EcpFeatureTestStepBase(Uri uri, string userName, string userDomain, SecureString password, AuthenticationParameters authenticationParameters, ITestFactory factory)
		{
			this.Uri = uri;
			this.UserName = userName;
			this.UserDomain = userDomain;
			this.Password = password;
			this.AuthenticationParameters = authenticationParameters;
			this.TestFactory = factory;
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x00054061 File Offset: 0x00052261
		protected override void Finally()
		{
			this.session.CloseConnections();
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x00054084 File Offset: 0x00052284
		protected override void StartTest()
		{
			ITestStep testStep = this.TestFactory.CreateAuthenticateStep(this.Uri, this.UserName, this.UserDomain, this.Password, this.AuthenticationParameters, this.TestFactory);
			testStep.BeginExecute(this.session, delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.AuthenticationCompleted), tempResult);
			}, testStep);
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x000540F0 File Offset: 0x000522F0
		private void AuthenticationCompleted(IAsyncResult result)
		{
			ITestStep testStep = result.AsyncState as ITestStep;
			testStep.EndExecute(result);
			ITestStep testStep2 = this.TestFactory.CreateEcpStartPageStep(this.Uri);
			testStep2.BeginExecute(this.session, delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.StartPageCompleted), tempResult);
			}, testStep2);
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x00054154 File Offset: 0x00052354
		private void StartPageCompleted(IAsyncResult result)
		{
			ITestStep testStep = result.AsyncState as ITestStep;
			testStep.EndExecute(result);
			this.Uri = ((EcpStartPage)testStep).Uri;
			ITestStep featureTestStep = this.GetFeatureTestStep((EcpStartPage)testStep);
			if (featureTestStep != null)
			{
				featureTestStep.BeginExecute(this.session, delegate(IAsyncResult tempResult)
				{
					base.AsyncCallbackWrapper(new AsyncCallback(this.FeatureTestStepCompleted), tempResult);
				}, featureTestStep);
				return;
			}
			this.ExecuteLogoff();
		}

		// Token: 0x060027A3 RID: 10147
		protected abstract ITestStep GetFeatureTestStep(EcpStartPage uri);

		// Token: 0x060027A4 RID: 10148 RVA: 0x000541C0 File Offset: 0x000523C0
		private void FeatureTestStepCompleted(IAsyncResult result)
		{
			ITestStep testStep = result.AsyncState as ITestStep;
			testStep.EndExecute(result);
			this.ExecuteLogoff();
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x000541FC File Offset: 0x000523FC
		private void ExecuteLogoff()
		{
			ITestStep testStep = this.TestFactory.CreateLogoffStep(this.Uri, "logoff.aspx");
			testStep.BeginExecute(this.session, delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.LogOffStepCompleted), tempResult);
			}, testStep);
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x0005423C File Offset: 0x0005243C
		private void LogOffStepCompleted(IAsyncResult result)
		{
			ITestStep testStep = result.AsyncState as ITestStep;
			testStep.EndExecute(result);
			base.ExecutionCompletedSuccessfully();
		}
	}
}
