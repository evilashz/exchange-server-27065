using System;
using System.Security;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Owa
{
	// Token: 0x020007D7 RID: 2007
	internal class OwaExternalLoginAgainstSpecificServer : BaseTestStep
	{
		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06002986 RID: 10630 RVA: 0x000596AD File Offset: 0x000578AD
		// (set) Token: 0x06002987 RID: 10631 RVA: 0x000596B5 File Offset: 0x000578B5
		public Uri Uri { get; private set; }

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06002988 RID: 10632 RVA: 0x000596BE File Offset: 0x000578BE
		// (set) Token: 0x06002989 RID: 10633 RVA: 0x000596C6 File Offset: 0x000578C6
		public string UserName { get; private set; }

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x0600298A RID: 10634 RVA: 0x000596CF File Offset: 0x000578CF
		// (set) Token: 0x0600298B RID: 10635 RVA: 0x000596D7 File Offset: 0x000578D7
		public string UserDomain { get; private set; }

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x0600298C RID: 10636 RVA: 0x000596E0 File Offset: 0x000578E0
		// (set) Token: 0x0600298D RID: 10637 RVA: 0x000596E8 File Offset: 0x000578E8
		public SecureString Password { get; private set; }

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x0600298E RID: 10638 RVA: 0x000596F1 File Offset: 0x000578F1
		// (set) Token: 0x0600298F RID: 10639 RVA: 0x000596F9 File Offset: 0x000578F9
		public string ServerToHit { get; private set; }

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06002990 RID: 10640 RVA: 0x00059702 File Offset: 0x00057902
		// (set) Token: 0x06002991 RID: 10641 RVA: 0x0005970A File Offset: 0x0005790A
		public ITestFactory TestFactory { get; private set; }

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06002992 RID: 10642 RVA: 0x00059713 File Offset: 0x00057913
		protected override TestId Id
		{
			get
			{
				return TestId.OwaExternalLoginAgainstSpecificServer;
			}
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x00059716 File Offset: 0x00057916
		public OwaExternalLoginAgainstSpecificServer(Uri uri, string userName, string userDomain, SecureString password, ITestFactory factory, string serverShortName)
		{
			this.Uri = uri;
			this.UserName = userName;
			this.UserDomain = userDomain;
			this.Password = password;
			this.TestFactory = factory;
			this.ServerToHit = serverShortName;
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x0005974B File Offset: 0x0005794B
		protected override void Finally()
		{
			this.session.CloseConnections();
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x00059770 File Offset: 0x00057970
		protected override void StartTest()
		{
			ITestStep testStep = this.TestFactory.CreateEstablishAffinityStep(this.Uri, this.ServerToHit);
			testStep.BeginExecute(this.session, delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.AffinityEstablished), tempResult);
			}, testStep);
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x000597C4 File Offset: 0x000579C4
		private void AffinityEstablished(IAsyncResult result)
		{
			ITestStep testStep = result.AsyncState as ITestStep;
			testStep.EndExecute(result);
			ITestStep testStep2 = this.TestFactory.CreateOwaLoginScenario(this.Uri, this.UserName, this.UserDomain, this.Password, new OwaLoginParameters(), this.TestFactory);
			testStep2.BeginExecute(this.session, delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.LoginScenarioCompleted), tempResult);
			}, testStep2);
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x00059830 File Offset: 0x00057A30
		private void LoginScenarioCompleted(IAsyncResult result)
		{
			ITestStep testStep = result.AsyncState as ITestStep;
			testStep.EndExecute(result);
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x040024AE RID: 9390
		private const TestId ID = TestId.OwaExternalLoginAgainstSpecificServer;
	}
}
