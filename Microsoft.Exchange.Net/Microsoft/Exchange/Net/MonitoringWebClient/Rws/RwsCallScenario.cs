using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Rws
{
	// Token: 0x020007F1 RID: 2033
	internal class RwsCallScenario : BaseTestStep
	{
		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06002A98 RID: 10904 RVA: 0x0005CD67 File Offset: 0x0005AF67
		// (set) Token: 0x06002A99 RID: 10905 RVA: 0x0005CD6F File Offset: 0x0005AF6F
		public Uri Uri { get; private set; }

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06002A9A RID: 10906 RVA: 0x0005CD78 File Offset: 0x0005AF78
		// (set) Token: 0x06002A9B RID: 10907 RVA: 0x0005CD80 File Offset: 0x0005AF80
		public RwsAuthenticationInfo AuthenticationInfo { get; private set; }

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06002A9C RID: 10908 RVA: 0x0005CD89 File Offset: 0x0005AF89
		// (set) Token: 0x06002A9D RID: 10909 RVA: 0x0005CD91 File Offset: 0x0005AF91
		public ITestFactory TestFactory { get; private set; }

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06002A9E RID: 10910 RVA: 0x0005CD9A File Offset: 0x0005AF9A
		protected override TestId Id
		{
			get
			{
				return TestId.RwsCallScenario;
			}
		}

		// Token: 0x06002A9F RID: 10911 RVA: 0x0005CD9E File Offset: 0x0005AF9E
		public RwsCallScenario(Uri uri, RwsAuthenticationInfo authenticationInfo, ITestFactory factory)
		{
			this.Uri = uri;
			this.AuthenticationInfo = authenticationInfo;
			this.TestFactory = factory;
		}

		// Token: 0x06002AA0 RID: 10912 RVA: 0x0005CDBB File Offset: 0x0005AFBB
		protected override void Finally()
		{
			this.session.CloseConnections();
		}

		// Token: 0x06002AA1 RID: 10913 RVA: 0x0005CDE0 File Offset: 0x0005AFE0
		protected override void StartTest()
		{
			ITestStep testStep = this.TestFactory.CreateRwsAuthenticateStep(this.Uri, this.AuthenticationInfo, this.TestFactory);
			testStep.BeginExecute(this.session, delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.AuthenticationCompleted), tempResult);
			}, testStep);
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x0005CE3C File Offset: 0x0005B03C
		private void AuthenticationCompleted(IAsyncResult result)
		{
			ITestStep testStep = result.AsyncState as ITestStep;
			testStep.EndExecute(result);
			ITestStep testStep2 = this.TestFactory.CreateRwsCallStep(this.Uri);
			testStep2.BeginExecute(this.session, delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.EndpointCallCompleted), tempResult);
			}, testStep2);
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x0005CE88 File Offset: 0x0005B088
		private void EndpointCallCompleted(IAsyncResult result)
		{
			ITestStep testStep = result.AsyncState as ITestStep;
			testStep.EndExecute(result);
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x04002546 RID: 9542
		private const TestId ID = TestId.RwsCallScenario;
	}
}
