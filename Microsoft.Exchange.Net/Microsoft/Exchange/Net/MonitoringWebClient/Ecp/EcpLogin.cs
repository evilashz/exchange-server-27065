using System;
using System.Security;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Ecp
{
	// Token: 0x020007AB RID: 1963
	internal class EcpLogin : BaseTestStep
	{
		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x060027C5 RID: 10181 RVA: 0x00054466 File Offset: 0x00052666
		// (set) Token: 0x060027C6 RID: 10182 RVA: 0x0005446E File Offset: 0x0005266E
		public Uri Uri { get; private set; }

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x060027C7 RID: 10183 RVA: 0x00054477 File Offset: 0x00052677
		// (set) Token: 0x060027C8 RID: 10184 RVA: 0x0005447F File Offset: 0x0005267F
		public string UserName { get; private set; }

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x060027C9 RID: 10185 RVA: 0x00054488 File Offset: 0x00052688
		// (set) Token: 0x060027CA RID: 10186 RVA: 0x00054490 File Offset: 0x00052690
		public string UserDomain { get; private set; }

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x060027CB RID: 10187 RVA: 0x00054499 File Offset: 0x00052699
		// (set) Token: 0x060027CC RID: 10188 RVA: 0x000544A1 File Offset: 0x000526A1
		public SecureString Password { get; private set; }

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x060027CD RID: 10189 RVA: 0x000544AA File Offset: 0x000526AA
		// (set) Token: 0x060027CE RID: 10190 RVA: 0x000544B2 File Offset: 0x000526B2
		public AuthenticationParameters AuthenticationParameters { get; private set; }

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x060027CF RID: 10191 RVA: 0x000544BB File Offset: 0x000526BB
		// (set) Token: 0x060027D0 RID: 10192 RVA: 0x000544C3 File Offset: 0x000526C3
		public ITestFactory TestFactory { get; private set; }

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x060027D1 RID: 10193 RVA: 0x000544CC File Offset: 0x000526CC
		protected override TestId Id
		{
			get
			{
				return TestId.EcpLoginScenario;
			}
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x000544CF File Offset: 0x000526CF
		public EcpLogin(Uri uri, string userName, string userDomain, SecureString password, ITestFactory factory)
		{
			this.Uri = uri;
			this.UserName = userName;
			this.UserDomain = userDomain;
			this.Password = password;
			this.TestFactory = factory;
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x000544FC File Offset: 0x000526FC
		protected override void Finally()
		{
			this.session.CloseConnections();
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x00054520 File Offset: 0x00052720
		protected override void StartTest()
		{
			ITestStep testStep = this.TestFactory.CreateAuthenticateStep(this.Uri, this.UserName, this.UserDomain, this.Password, this.AuthenticationParameters, this.TestFactory);
			testStep.BeginExecute(this.session, delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.AuthenticationCompleted), tempResult);
			}, testStep);
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x0005458C File Offset: 0x0005278C
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

		// Token: 0x060027D6 RID: 10198 RVA: 0x000545F0 File Offset: 0x000527F0
		private void StartPageCompleted(IAsyncResult result)
		{
			ITestStep testStep = result.AsyncState as ITestStep;
			testStep.EndExecute(result);
			ITestStep testStep2 = this.TestFactory.CreateEcpWebServiceCallStep(this.Uri);
			testStep2.BeginExecute(this.session, delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.WebServiceCallCompleted), tempResult);
			}, testStep2);
		}

		// Token: 0x060027D7 RID: 10199 RVA: 0x00054654 File Offset: 0x00052854
		private void WebServiceCallCompleted(IAsyncResult result)
		{
			ITestStep testStep = result.AsyncState as ITestStep;
			testStep.EndExecute(result);
			ITestStep testStep2 = this.TestFactory.CreateLogoffStep(this.Uri, "logoff.aspx");
			testStep2.BeginExecute(this.session, delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.LogOffStepCompleted), tempResult);
			}, testStep2);
		}

		// Token: 0x060027D8 RID: 10200 RVA: 0x000546A8 File Offset: 0x000528A8
		private void LogOffStepCompleted(IAsyncResult result)
		{
			ITestStep testStep = result.AsyncState as ITestStep;
			testStep.EndExecute(result);
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x040023C2 RID: 9154
		private const TestId ID = TestId.EcpLoginScenario;
	}
}
