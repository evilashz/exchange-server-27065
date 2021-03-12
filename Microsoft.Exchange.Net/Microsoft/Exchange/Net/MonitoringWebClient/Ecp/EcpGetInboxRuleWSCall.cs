using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Ecp
{
	// Token: 0x020007AE RID: 1966
	internal class EcpGetInboxRuleWSCall : EcpWebServiceCallBase
	{
		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x060027EE RID: 10222 RVA: 0x000548B6 File Offset: 0x00052AB6
		protected override TestId Id
		{
			get
			{
				return TestId.EcpGetInboxRuleWSCall;
			}
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x060027EF RID: 10223 RVA: 0x000548BA File Offset: 0x00052ABA
		protected override Uri WebServiceRelativeUri
		{
			get
			{
				return new Uri("RulesEditor/InboxRules.svc/GetList", UriKind.Relative);
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x060027F0 RID: 10224 RVA: 0x000548C7 File Offset: 0x00052AC7
		protected override RequestBody RequestBody
		{
			get
			{
				return RequestBody.Format("{\"filter\":{\"SearchText\":\"\"},\"sort\":{\"Direction\":0,\"PropertyName\":\"Name\"}}", new object[0]);
			}
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x000548D9 File Offset: 0x00052AD9
		public EcpGetInboxRuleWSCall(Uri uri, Func<Uri, ITestStep> getFollowingTestStep = null) : base(uri, getFollowingTestStep)
		{
		}

		// Token: 0x040023CD RID: 9165
		private const TestId ID = TestId.EcpGetInboxRuleWSCall;
	}
}
