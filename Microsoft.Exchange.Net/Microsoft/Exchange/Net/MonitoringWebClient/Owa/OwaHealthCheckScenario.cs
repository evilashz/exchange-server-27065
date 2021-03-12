using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Owa
{
	// Token: 0x020007D8 RID: 2008
	internal class OwaHealthCheckScenario : BaseTestStep
	{
		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x0600299A RID: 10650 RVA: 0x00059856 File Offset: 0x00057A56
		// (set) Token: 0x0600299B RID: 10651 RVA: 0x0005985E File Offset: 0x00057A5E
		public Uri Uri { get; private set; }

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x0600299C RID: 10652 RVA: 0x00059867 File Offset: 0x00057A67
		// (set) Token: 0x0600299D RID: 10653 RVA: 0x0005986F File Offset: 0x00057A6F
		public ITestFactory TestFactory { get; private set; }

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x0600299E RID: 10654 RVA: 0x00059878 File Offset: 0x00057A78
		protected override TestId Id
		{
			get
			{
				return TestId.OwaHealthCheckScenario;
			}
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x0005987B File Offset: 0x00057A7B
		public OwaHealthCheckScenario(Uri uri, ITestFactory factory)
		{
			this.Uri = uri;
			this.TestFactory = factory;
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x00059891 File Offset: 0x00057A91
		protected override void Finally()
		{
			this.session.CloseConnections();
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x000598B4 File Offset: 0x00057AB4
		protected override void StartTest()
		{
			ITestStep testStep = this.TestFactory.CreateOwaHealthCheckStep(this.Uri);
			testStep.BeginExecute(this.session, delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.HealthCheckStepCompleted), tempResult);
			}, testStep);
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x000598F0 File Offset: 0x00057AF0
		private void HealthCheckStepCompleted(IAsyncResult result)
		{
			ITestStep testStep = result.AsyncState as ITestStep;
			testStep.EndExecute(result);
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x040024B5 RID: 9397
		private const TestId ID = TestId.OwaHealthCheckScenario;
	}
}
