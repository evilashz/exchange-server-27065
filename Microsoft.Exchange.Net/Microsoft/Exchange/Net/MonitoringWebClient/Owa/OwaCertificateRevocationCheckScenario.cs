using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Owa
{
	// Token: 0x020007D6 RID: 2006
	internal class OwaCertificateRevocationCheckScenario : BaseTestStep
	{
		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x0600297B RID: 10619 RVA: 0x000595DE File Offset: 0x000577DE
		// (set) Token: 0x0600297C RID: 10620 RVA: 0x000595E6 File Offset: 0x000577E6
		public Uri Uri { get; private set; }

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x0600297D RID: 10621 RVA: 0x000595EF File Offset: 0x000577EF
		// (set) Token: 0x0600297E RID: 10622 RVA: 0x000595F7 File Offset: 0x000577F7
		public ITestFactory TestFactory { get; private set; }

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x0600297F RID: 10623 RVA: 0x00059600 File Offset: 0x00057800
		protected override TestId Id
		{
			get
			{
				return TestId.OwaCertificateRevocationCheckScenario;
			}
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x00059603 File Offset: 0x00057803
		public OwaCertificateRevocationCheckScenario(Uri uri, ITestFactory factory)
		{
			this.Uri = uri;
			this.TestFactory = factory;
		}

		// Token: 0x06002981 RID: 10625 RVA: 0x00059619 File Offset: 0x00057819
		protected override void Finally()
		{
			this.session.CloseConnections();
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x0005963B File Offset: 0x0005783B
		protected override void StartTest()
		{
			this.session.SslValidationOptions = SslValidationOptions.Revocation;
			this.session.BeginGet(this.Id, this.Uri.ToString(), delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.ResponseReceived), tempResult);
			}, null);
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x00059676 File Offset: 0x00057876
		private void ResponseReceived(IAsyncResult result)
		{
			this.session.EndGet<object>(result, HttpSession.AllHttpStatusCodes, (HttpWebResponseWrapper response) => null);
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x040024AA RID: 9386
		private const TestId ID = TestId.OwaCertificateRevocationCheckScenario;
	}
}
