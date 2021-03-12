using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Rws
{
	// Token: 0x020007F2 RID: 2034
	internal class RwsAuthentication : BaseTestStep
	{
		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06002AA6 RID: 10918 RVA: 0x0005CEAE File Offset: 0x0005B0AE
		// (set) Token: 0x06002AA7 RID: 10919 RVA: 0x0005CEB6 File Offset: 0x0005B0B6
		public ITestFactory TestFactory { get; private set; }

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06002AA8 RID: 10920 RVA: 0x0005CEBF File Offset: 0x0005B0BF
		protected override TestId Id
		{
			get
			{
				return TestId.RwsAuthentication;
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06002AA9 RID: 10921 RVA: 0x0005CEC3 File Offset: 0x0005B0C3
		// (set) Token: 0x06002AAA RID: 10922 RVA: 0x0005CECB File Offset: 0x0005B0CB
		public Uri Uri { get; private set; }

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06002AAB RID: 10923 RVA: 0x0005CED4 File Offset: 0x0005B0D4
		// (set) Token: 0x06002AAC RID: 10924 RVA: 0x0005CEDC File Offset: 0x0005B0DC
		public RwsAuthenticationInfo AuthenticationInfo { get; private set; }

		// Token: 0x06002AAD RID: 10925 RVA: 0x0005CEE5 File Offset: 0x0005B0E5
		public RwsAuthentication(Uri uri, RwsAuthenticationInfo authenticationInfo, ITestFactory factory)
		{
			this.Uri = uri;
			this.AuthenticationInfo = authenticationInfo;
			this.TestFactory = factory;
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x0005CF18 File Offset: 0x0005B118
		protected override void StartTest()
		{
			ITestStep testStep;
			if (this.AuthenticationInfo.AuthenticationType == RwsAuthenticationType.Brick)
			{
				testStep = this.TestFactory.CreateRwsBrickAuthenticateStep(this.AuthenticationInfo.Token, this.Uri);
			}
			else
			{
				testStep = this.TestFactory.CreateIisAuthenticateStep(this.Uri, this.AuthenticationInfo.UserName, this.AuthenticationInfo.UserDomain, this.AuthenticationInfo.Password);
			}
			testStep.BeginExecute(this.session, delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.AuthenticationStepFinished), tempResult);
			}, testStep);
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x0005CFA4 File Offset: 0x0005B1A4
		private void AuthenticationStepFinished(IAsyncResult result)
		{
			ITestStep testStep = result.AsyncState as ITestStep;
			testStep.EndExecute(result);
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x0400254A RID: 9546
		private const TestId ID = TestId.RwsAuthentication;
	}
}
