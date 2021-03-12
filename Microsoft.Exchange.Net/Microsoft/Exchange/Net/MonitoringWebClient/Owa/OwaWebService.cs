using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Owa
{
	// Token: 0x020007E2 RID: 2018
	internal class OwaWebService : BaseTestStep
	{
		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06002A26 RID: 10790 RVA: 0x0005ACB4 File Offset: 0x00058EB4
		// (set) Token: 0x06002A27 RID: 10791 RVA: 0x0005ACBC File Offset: 0x00058EBC
		public Uri Uri { get; private set; }

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06002A28 RID: 10792 RVA: 0x0005ACC5 File Offset: 0x00058EC5
		// (set) Token: 0x06002A29 RID: 10793 RVA: 0x0005ACCD File Offset: 0x00058ECD
		public RequestBody RequestBody { get; private set; }

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06002A2A RID: 10794 RVA: 0x0005ACD6 File Offset: 0x00058ED6
		protected override TestId Id
		{
			get
			{
				return TestId.OwaWebService;
			}
		}

		// Token: 0x06002A2B RID: 10795 RVA: 0x0005ACDA File Offset: 0x00058EDA
		public OwaWebService(Uri uri, string actionName)
		{
			this.Uri = uri;
			this.action = actionName;
			this.RequestBody = RequestBody.Format(string.Empty, new object[0]);
		}

		// Token: 0x06002A2C RID: 10796 RVA: 0x0005AD06 File Offset: 0x00058F06
		public OwaWebService(Uri uri, string actionName, RequestBody requestBody) : this(uri, actionName)
		{
			this.RequestBody = requestBody;
		}

		// Token: 0x06002A2D RID: 10797 RVA: 0x0005AD2C File Offset: 0x00058F2C
		protected override void StartTest()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("action", this.action);
			this.session.BeginPost(this.Id, new Uri(this.Uri, "service.svc?action=" + this.action).ToString(), this.RequestBody, "application/json; charset=utf-8", dictionary, delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.WebServiceResponseReceived), tempResult);
			}, null);
		}

		// Token: 0x06002A2E RID: 10798 RVA: 0x0005AD9B File Offset: 0x00058F9B
		private void WebServiceResponseReceived(IAsyncResult result)
		{
			this.session.EndGet<object>(result, null);
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x040024E3 RID: 9443
		private const TestId ID = TestId.OwaWebService;

		// Token: 0x040024E4 RID: 9444
		private readonly string action;
	}
}
