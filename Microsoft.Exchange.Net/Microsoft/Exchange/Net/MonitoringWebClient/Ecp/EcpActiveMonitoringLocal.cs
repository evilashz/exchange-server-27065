using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Ecp
{
	// Token: 0x020007A8 RID: 1960
	internal class EcpActiveMonitoringLocal : EcpFeatureTestStepBase
	{
		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x060027AB RID: 10155 RVA: 0x00054262 File Offset: 0x00052462
		protected override TestId Id
		{
			get
			{
				return TestId.EcpActiveMonitoringLocal;
			}
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x00054265 File Offset: 0x00052465
		public EcpActiveMonitoringLocal(Uri uri, string userName, string userDomain, AuthenticationParameters authenticationParameters, ITestFactory factory, Func<EcpStartPage, ITestStep> getFollowingTestStep) : base(uri, userName, userDomain, null, authenticationParameters, factory)
		{
			this.getFollowingTestStep = getFollowingTestStep;
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x0005427D File Offset: 0x0005247D
		protected override ITestStep GetFeatureTestStep(EcpStartPage startPage)
		{
			if (this.getFollowingTestStep == null)
			{
				return null;
			}
			return this.getFollowingTestStep(startPage);
		}

		// Token: 0x040023B7 RID: 9143
		private const TestId ID = TestId.EcpActiveMonitoringLocal;

		// Token: 0x040023B8 RID: 9144
		private Func<EcpStartPage, ITestStep> getFollowingTestStep;
	}
}
