using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Owa
{
	// Token: 0x020007DF RID: 2015
	internal class OwaPing : BaseTestStep
	{
		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06002A03 RID: 10755 RVA: 0x0005A56A File Offset: 0x0005876A
		// (set) Token: 0x06002A04 RID: 10756 RVA: 0x0005A572 File Offset: 0x00058772
		public Uri Uri { get; private set; }

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06002A05 RID: 10757 RVA: 0x0005A57B File Offset: 0x0005877B
		protected override TestId Id
		{
			get
			{
				return TestId.OwaPing;
			}
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x0005A57F File Offset: 0x0005877F
		public OwaPing(Uri uri)
		{
			this.Uri = uri;
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x0005A5A3 File Offset: 0x000587A3
		protected override void StartTest()
		{
			this.session.BeginGet(this.Id, new Uri(this.Uri, "ev.owa?oeh=1&ns=Monitoring&ev=Ping").ToString(), delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.PingResponseReceived), tempResult);
			}, null);
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x0005A5D9 File Offset: 0x000587D9
		private void PingResponseReceived(IAsyncResult result)
		{
			this.session.EndGet<object>(result, null);
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x040024D9 RID: 9433
		private const TestId ID = TestId.OwaPing;
	}
}
