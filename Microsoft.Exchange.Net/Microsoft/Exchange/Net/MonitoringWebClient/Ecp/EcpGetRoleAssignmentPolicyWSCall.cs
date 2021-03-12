using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Ecp
{
	// Token: 0x020007B0 RID: 1968
	internal class EcpGetRoleAssignmentPolicyWSCall : EcpWebServiceCallBase
	{
		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x060027F6 RID: 10230 RVA: 0x00054910 File Offset: 0x00052B10
		protected override TestId Id
		{
			get
			{
				return TestId.EcpGetRoleAssignmentPolicyWSCall;
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x060027F7 RID: 10231 RVA: 0x00054914 File Offset: 0x00052B14
		protected override Uri WebServiceRelativeUri
		{
			get
			{
				return new Uri("UsersGroups/RoleAssignmentPolicies.svc/GetList", UriKind.Relative);
			}
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x060027F8 RID: 10232 RVA: 0x00054921 File Offset: 0x00052B21
		protected override RequestBody RequestBody
		{
			get
			{
				return RequestBody.Format("{\"filter\":{},\"sort\":{\"Direction\":0,\"PropertyName\":\"Name\"}}", new object[0]);
			}
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x00054933 File Offset: 0x00052B33
		public EcpGetRoleAssignmentPolicyWSCall(Uri uri, Func<Uri, ITestStep> getFollowingTestStep = null) : base(uri, getFollowingTestStep)
		{
		}

		// Token: 0x040023CF RID: 9167
		private const TestId ID = TestId.EcpGetRoleAssignmentPolicyWSCall;
	}
}
