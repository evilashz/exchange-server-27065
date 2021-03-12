using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Ecp
{
	// Token: 0x020007AF RID: 1967
	internal class EcpGetRecipientWSCall : EcpWebServiceCallBase
	{
		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x060027F2 RID: 10226 RVA: 0x000548E3 File Offset: 0x00052AE3
		protected override TestId Id
		{
			get
			{
				return TestId.EcpGetRecipientWSCall;
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x060027F3 RID: 10227 RVA: 0x000548E7 File Offset: 0x00052AE7
		protected override Uri WebServiceRelativeUri
		{
			get
			{
				return new Uri("PersonalSettings/Accounts.svc/GetList", UriKind.Relative);
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x060027F4 RID: 10228 RVA: 0x000548F4 File Offset: 0x00052AF4
		protected override RequestBody RequestBody
		{
			get
			{
				return RequestBody.Format("{\"filter\":{\"SearchText\":\"\"},\"sort\":{\"Direction\":0,\"PropertyName\":\"DisplayName\"}}", new object[0]);
			}
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x00054906 File Offset: 0x00052B06
		public EcpGetRecipientWSCall(Uri uri, Func<Uri, ITestStep> getFollowingTestStep = null) : base(uri, getFollowingTestStep)
		{
		}

		// Token: 0x040023CE RID: 9166
		private const TestId ID = TestId.EcpGetRecipientWSCall;
	}
}
