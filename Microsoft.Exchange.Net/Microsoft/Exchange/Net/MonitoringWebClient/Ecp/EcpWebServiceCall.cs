using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Ecp
{
	// Token: 0x020007B6 RID: 1974
	internal class EcpWebServiceCall : BaseTestStep
	{
		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x0600283A RID: 10298 RVA: 0x00055547 File Offset: 0x00053747
		// (set) Token: 0x0600283B RID: 10299 RVA: 0x0005554F File Offset: 0x0005374F
		public Uri Uri { get; private set; }

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x0600283C RID: 10300 RVA: 0x00055558 File Offset: 0x00053758
		protected override TestId Id
		{
			get
			{
				return TestId.EcpWebServiceCall;
			}
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x0005555C File Offset: 0x0005375C
		public EcpWebServiceCall(Uri uri)
		{
			this.Uri = uri;
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x00055580 File Offset: 0x00053780
		protected override void StartTest()
		{
			string value = this.session.CookieContainer.GetCookies(this.Uri)["msExchEcpCanary"].Value;
			this.session.BeginPost(this.Id, new Uri(this.Uri, "RulesEditor/InboxRules.svc/GetList?msExchEcpCanary=" + value).ToString(), RequestBody.Format("{\"filter\":{\"SearchText\":\"\"},\"sort\":{\"Direction\":0,\"PropertyName\":\"Name\"}}", new object[0]), "application/json", delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.WebServiceResponseReceived), tempResult);
			}, null);
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x00055602 File Offset: 0x00053802
		private void WebServiceResponseReceived(IAsyncResult result)
		{
			this.session.EndPost<object>(result, null);
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x040023E7 RID: 9191
		private const TestId ID = TestId.EcpWebServiceCall;
	}
}
