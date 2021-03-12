using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Ecp
{
	// Token: 0x020007B4 RID: 1972
	internal class EcpSetOOFWSCall : EcpWebServiceCallBase
	{
		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06002820 RID: 10272 RVA: 0x00054EB2 File Offset: 0x000530B2
		protected override TestId Id
		{
			get
			{
				return TestId.ECPSetOOFWSCall;
			}
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06002821 RID: 10273 RVA: 0x00054EB6 File Offset: 0x000530B6
		protected override Uri WebServiceRelativeUri
		{
			get
			{
				return new Uri("Organize/AutomaticReplies.svc/SetObject", UriKind.Relative);
			}
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06002822 RID: 10274 RVA: 0x00054EC3 File Offset: 0x000530C3
		protected override RequestBody RequestBody
		{
			get
			{
				return EcpSetOOFWSCall.BuildRequestBody();
			}
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x00054ECA File Offset: 0x000530CA
		public EcpSetOOFWSCall(Uri uri, Func<Uri, ITestStep> getFollowingTestStep = null) : base(uri, getFollowingTestStep)
		{
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x00054ED4 File Offset: 0x000530D4
		private static RequestBody BuildRequestBody()
		{
			DateTime localTime = ExDateTime.Now.AddDays(2.0).LocalTime;
			DateTime dateTime = localTime.AddDays(1.0);
			string text = string.Format("{0:yyyy/MM/dd HH:mm:ss}", localTime);
			string text2 = string.Format("{0:yyyy/MM/dd HH:mm:ss}", dateTime);
			return RequestBody.Format("{{\"identity\":null,\"properties\":{{\"AutoReplyStateDisabled\":\"false\",\"AutoReplyStateScheduled\":true,\"StartTime\":\"{0}\",\"EndTime\":\"{1}\",\"InternalMessage\":\"<div style=\\\"font-family: Tahoma; font-size: 10pt;\\\">message for Set OOF Web Service Call</div>\",\"ExternalAudience\":true,\"ExternalAudienceKnownOnly\":\"false\"}}}}", new object[]
			{
				text,
				text2
			});
		}

		// Token: 0x040023DE RID: 9182
		private const string bodyTemplate = "{{\"identity\":null,\"properties\":{{\"AutoReplyStateDisabled\":\"false\",\"AutoReplyStateScheduled\":true,\"StartTime\":\"{0}\",\"EndTime\":\"{1}\",\"InternalMessage\":\"<div style=\\\"font-family: Tahoma; font-size: 10pt;\\\">message for Set OOF Web Service Call</div>\",\"ExternalAudience\":true,\"ExternalAudienceKnownOnly\":\"false\"}}}}";

		// Token: 0x040023DF RID: 9183
		private const TestId ID = TestId.ECPSetOOFWSCall;
	}
}
