using System;
using System.Security;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Ecp
{
	// Token: 0x020007A9 RID: 1961
	internal class EcpActiveMonitoringOutsideIn : EcpFeatureTestStepBase
	{
		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x060027AE RID: 10158 RVA: 0x00054295 File Offset: 0x00052495
		protected override TestId Id
		{
			get
			{
				return TestId.EcpActiveMonitoringOutsideIn;
			}
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x00054299 File Offset: 0x00052499
		public EcpActiveMonitoringOutsideIn(Uri uri, string userName, SecureString password, ITestFactory factory, Func<EcpStartPage, ITestStep> getFollowingTestStep) : base(uri, userName, null, password, null, factory)
		{
			this.getFollowingTestStep = getFollowingTestStep;
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x000542B0 File Offset: 0x000524B0
		protected override ITestStep GetFeatureTestStep(EcpStartPage startPage)
		{
			if (this.getFollowingTestStep == null)
			{
				return null;
			}
			return this.getFollowingTestStep(startPage);
		}

		// Token: 0x040023B9 RID: 9145
		private const TestId ID = TestId.EcpActiveMonitoringOutsideIn;

		// Token: 0x040023BA RID: 9146
		private Func<EcpStartPage, ITestStep> getFollowingTestStep;
	}
}
