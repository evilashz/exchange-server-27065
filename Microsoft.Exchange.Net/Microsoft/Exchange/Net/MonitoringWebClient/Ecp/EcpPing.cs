using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Ecp
{
	// Token: 0x020007B3 RID: 1971
	internal class EcpPing : BaseTestStep
	{
		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06002818 RID: 10264 RVA: 0x00054DA3 File Offset: 0x00052FA3
		protected override TestId Id
		{
			get
			{
				return TestId.EcpPing;
			}
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x00054DA7 File Offset: 0x00052FA7
		public EcpPing(Uri uri)
		{
			this.Uri = new Uri(uri, "exhealth.check");
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x0600281A RID: 10266 RVA: 0x00054DC0 File Offset: 0x00052FC0
		// (set) Token: 0x0600281B RID: 10267 RVA: 0x00054DC8 File Offset: 0x00052FC8
		public Uri Uri { get; private set; }

		// Token: 0x0600281C RID: 10268 RVA: 0x00054DE8 File Offset: 0x00052FE8
		protected override void StartTest()
		{
			this.session.PersistentHeaders.Add("X-IsFromCafe", "1");
			this.session.BeginGet(this.Id, this.Uri.ToString(), delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.PingResponseReceived), tempResult);
			}, null);
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x00054E80 File Offset: 0x00053080
		private void PingResponseReceived(IAsyncResult result)
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
	}
}
