using System;
using Microsoft.Exchange.Net.MonitoringWebClient.Ecp.Parsers;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Ecp
{
	// Token: 0x020007B1 RID: 1969
	internal class EcpGoToSyndicatedAdminStartPage : BaseTestStep
	{
		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x060027FA RID: 10234 RVA: 0x0005493D File Offset: 0x00052B3D
		protected override TestId Id
		{
			get
			{
				return TestId.EcpGoToSyndicatedAdminStartPage;
			}
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x060027FB RID: 10235 RVA: 0x00054941 File Offset: 0x00052B41
		// (set) Token: 0x060027FC RID: 10236 RVA: 0x00054949 File Offset: 0x00052B49
		public Uri Uri { get; private set; }

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x060027FD RID: 10237 RVA: 0x00054952 File Offset: 0x00052B52
		// (set) Token: 0x060027FE RID: 10238 RVA: 0x0005495A File Offset: 0x00052B5A
		public string PartnerDomain { get; private set; }

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x060027FF RID: 10239 RVA: 0x00054963 File Offset: 0x00052B63
		// (set) Token: 0x06002800 RID: 10240 RVA: 0x0005496B File Offset: 0x00052B6B
		public string TargetDomain { get; private set; }

		// Token: 0x06002801 RID: 10241 RVA: 0x00054974 File Offset: 0x00052B74
		public EcpGoToSyndicatedAdminStartPage(Uri uri, string partnerDomain, string targetDomain, Func<Uri, ITestStep> getFollowingTestStep)
		{
			this.Uri = uri;
			this.PartnerDomain = partnerDomain;
			this.TargetDomain = targetDomain;
			this.getFollowingTestStep = getFollowingTestStep;
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x000549B0 File Offset: 0x00052BB0
		protected override void StartTest()
		{
			this.session.BeginGetFollowingRedirections(this.Id, new Uri(this.Uri, string.Format("/ecp/@{0}/?realm={1}&mkt=en-US&exsvurl=1", this.TargetDomain, this.PartnerDomain)).ToString(), delegate(IAsyncResult result)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.SyndicatedAdminStartPageReceived), result);
			}, null);
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x00054A20 File Offset: 0x00052C20
		private void SyndicatedAdminStartPageReceived(IAsyncResult result)
		{
			EcpHelpDeskStartPage ecpHelpDeskStartPage = this.session.EndGetFollowingRedirections<EcpHelpDeskStartPage>(result, (HttpWebResponseWrapper response) => EcpHelpDeskStartPage.Parse(response));
			this.Uri = ecpHelpDeskStartPage.FinalUri;
			ITestStep testStep = (this.getFollowingTestStep != null) ? this.getFollowingTestStep(this.Uri) : null;
			if (testStep != null)
			{
				testStep.BeginExecute(this.session, delegate(IAsyncResult tempResult)
				{
					base.AsyncCallbackWrapper(new AsyncCallback(this.FollowingTestStepCompleted), tempResult);
				}, testStep);
				return;
			}
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x00054AAC File Offset: 0x00052CAC
		private void FollowingTestStepCompleted(IAsyncResult result)
		{
			ITestStep testStep = result.AsyncState as ITestStep;
			testStep.EndExecute(result);
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x040023D0 RID: 9168
		private const TestId ID = TestId.EcpGoToSyndicatedAdminStartPage;

		// Token: 0x040023D1 RID: 9169
		private Func<Uri, ITestStep> getFollowingTestStep;
	}
}
