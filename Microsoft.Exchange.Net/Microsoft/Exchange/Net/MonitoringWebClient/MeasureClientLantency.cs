using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x0200079D RID: 1949
	internal class MeasureClientLantency : BaseTestStep
	{
		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x0600273A RID: 10042 RVA: 0x0005350A File Offset: 0x0005170A
		protected override TestId Id
		{
			get
			{
				return TestId.MeasureClientLatency;
			}
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x0005352C File Offset: 0x0005172C
		protected override void StartTest()
		{
			HttpPingServer instance = HttpPingServer.Instance;
			instance.Initialize();
			string uri = string.Format("http://localhost:{0}", instance.Port);
			this.session.BeginGet(this.Id, uri, delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.ClientPingResponseReceived), tempResult);
			}, null);
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x0005357C File Offset: 0x0005177C
		private void ClientPingResponseReceived(IAsyncResult result)
		{
			try
			{
				this.session.EndGet<object>(result, null);
			}
			catch (Exception)
			{
			}
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x0400236F RID: 9071
		private const TestId ID = TestId.MeasureClientLatency;
	}
}
