using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Owa
{
	// Token: 0x020007DE RID: 2014
	internal class OwaHealthCheck : BaseTestStep
	{
		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x060029FB RID: 10747 RVA: 0x0005A485 File Offset: 0x00058685
		// (set) Token: 0x060029FC RID: 10748 RVA: 0x0005A48D File Offset: 0x0005868D
		public Uri Uri { get; private set; }

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x060029FD RID: 10749 RVA: 0x0005A496 File Offset: 0x00058696
		protected override TestId Id
		{
			get
			{
				return TestId.OwaHealthCheck;
			}
		}

		// Token: 0x060029FE RID: 10750 RVA: 0x0005A49A File Offset: 0x0005869A
		public OwaHealthCheck(Uri uri)
		{
			this.Uri = uri;
		}

		// Token: 0x060029FF RID: 10751 RVA: 0x0005A4BE File Offset: 0x000586BE
		protected override void StartTest()
		{
			this.session.BeginGet(this.Id, new Uri(this.Uri, "exhealth.check").ToString(), delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.HealthCheckResponseReceived), tempResult);
			}, null);
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x0005A538 File Offset: 0x00058738
		private void HealthCheckResponseReceived(IAsyncResult result)
		{
			this.session.EndGet<object>(result, delegate(HttpWebResponseWrapper response)
			{
				string text = response.Headers["X-MSExchApplicationHealthHandlerStatus"];
				if (!text.Equals("Passed", StringComparison.OrdinalIgnoreCase))
				{
					throw new MissingKeywordException(MonitoringWebClientStrings.HealthCheckRequestFailed, response.Request, response, "Passed");
				}
				return null;
			});
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x040024D6 RID: 9430
		private const TestId ID = TestId.OwaHealthCheck;
	}
}
