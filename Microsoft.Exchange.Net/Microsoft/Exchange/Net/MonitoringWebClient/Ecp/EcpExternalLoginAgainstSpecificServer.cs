using System;
using System.Security;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Ecp
{
	// Token: 0x020007AA RID: 1962
	internal class EcpExternalLoginAgainstSpecificServer : BaseTestStep
	{
		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x060027B1 RID: 10161 RVA: 0x000542C8 File Offset: 0x000524C8
		// (set) Token: 0x060027B2 RID: 10162 RVA: 0x000542D0 File Offset: 0x000524D0
		public Uri Uri { get; private set; }

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x060027B3 RID: 10163 RVA: 0x000542D9 File Offset: 0x000524D9
		// (set) Token: 0x060027B4 RID: 10164 RVA: 0x000542E1 File Offset: 0x000524E1
		public string UserName { get; private set; }

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x060027B5 RID: 10165 RVA: 0x000542EA File Offset: 0x000524EA
		// (set) Token: 0x060027B6 RID: 10166 RVA: 0x000542F2 File Offset: 0x000524F2
		public string UserDomain { get; private set; }

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x060027B7 RID: 10167 RVA: 0x000542FB File Offset: 0x000524FB
		// (set) Token: 0x060027B8 RID: 10168 RVA: 0x00054303 File Offset: 0x00052503
		public SecureString Password { get; private set; }

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x060027B9 RID: 10169 RVA: 0x0005430C File Offset: 0x0005250C
		// (set) Token: 0x060027BA RID: 10170 RVA: 0x00054314 File Offset: 0x00052514
		public string ServerToHit { get; private set; }

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x060027BB RID: 10171 RVA: 0x0005431D File Offset: 0x0005251D
		// (set) Token: 0x060027BC RID: 10172 RVA: 0x00054325 File Offset: 0x00052525
		public ITestFactory TestFactory { get; private set; }

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x060027BD RID: 10173 RVA: 0x0005432E File Offset: 0x0005252E
		protected override TestId Id
		{
			get
			{
				return TestId.EcpExternalLoginAgainstSpecificServer;
			}
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x00054331 File Offset: 0x00052531
		public EcpExternalLoginAgainstSpecificServer(Uri uri, string userName, string userDomain, SecureString password, ITestFactory factory, string serverShortName)
		{
			this.Uri = uri;
			this.UserName = userName;
			this.UserDomain = userDomain;
			this.Password = password;
			this.TestFactory = factory;
			this.ServerToHit = serverShortName;
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x00054366 File Offset: 0x00052566
		protected override void Finally()
		{
			this.session.CloseConnections();
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x00054388 File Offset: 0x00052588
		protected override void StartTest()
		{
			ITestStep testStep = this.TestFactory.CreateEstablishAffinityStep(this.Uri, this.ServerToHit);
			testStep.BeginExecute(this.session, delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.AffinityEstablished), tempResult);
			}, testStep);
		}

		// Token: 0x060027C1 RID: 10177 RVA: 0x000543DC File Offset: 0x000525DC
		private void AffinityEstablished(IAsyncResult result)
		{
			ITestStep testStep = result.AsyncState as ITestStep;
			testStep.EndExecute(result);
			ITestStep testStep2 = this.TestFactory.CreateEcpLoginScenario(this.Uri, this.UserName, this.UserDomain, this.Password, this.TestFactory);
			testStep2.BeginExecute(this.session, delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.LoginScenarioCompleted), tempResult);
			}, testStep2);
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x00054440 File Offset: 0x00052640
		private void LoginScenarioCompleted(IAsyncResult result)
		{
			ITestStep testStep = result.AsyncState as ITestStep;
			testStep.EndExecute(result);
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x040023BB RID: 9147
		private const TestId ID = TestId.EcpExternalLoginAgainstSpecificServer;
	}
}
