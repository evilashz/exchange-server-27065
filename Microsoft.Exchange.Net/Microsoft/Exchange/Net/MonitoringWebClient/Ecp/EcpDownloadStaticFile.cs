using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Ecp
{
	// Token: 0x020007AC RID: 1964
	internal class EcpDownloadStaticFile : BaseTestStep
	{
		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x060027DD RID: 10205 RVA: 0x000546CE File Offset: 0x000528CE
		// (set) Token: 0x060027DE RID: 10206 RVA: 0x000546D6 File Offset: 0x000528D6
		public Uri Uri { get; private set; }

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x060027DF RID: 10207 RVA: 0x000546DF File Offset: 0x000528DF
		protected override TestId Id
		{
			get
			{
				return TestId.EcpDownloadStaticFile;
			}
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x000546E3 File Offset: 0x000528E3
		public EcpDownloadStaticFile(Uri uri)
		{
			this.Uri = uri;
		}

		// Token: 0x060027E1 RID: 10209 RVA: 0x00054707 File Offset: 0x00052907
		protected override void StartTest()
		{
			this.session.BeginGet(this.Id, this.Uri.ToString(), delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.ResponseReceived), tempResult);
			}, null);
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x00054733 File Offset: 0x00052933
		private void ResponseReceived(IAsyncResult result)
		{
			this.session.EndGet<object>(result, null);
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x040023C9 RID: 9161
		private const TestId ID = TestId.EcpDownloadStaticFile;
	}
}
