using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Owa
{
	// Token: 0x020007DD RID: 2013
	internal class OwaDownloadStaticFile : BaseTestStep
	{
		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x060029F4 RID: 10740 RVA: 0x0005A40A File Offset: 0x0005860A
		// (set) Token: 0x060029F5 RID: 10741 RVA: 0x0005A412 File Offset: 0x00058612
		public Uri Uri { get; private set; }

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x060029F6 RID: 10742 RVA: 0x0005A41B File Offset: 0x0005861B
		protected override TestId Id
		{
			get
			{
				return TestId.OwaDownloadStaticFile;
			}
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x0005A41F File Offset: 0x0005861F
		public OwaDownloadStaticFile(Uri uri)
		{
			this.Uri = uri;
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x0005A443 File Offset: 0x00058643
		protected override void StartTest()
		{
			this.session.BeginGet(this.Id, this.Uri.ToString(), delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.ResponseReceived), tempResult);
			}, null);
		}

		// Token: 0x060029F9 RID: 10745 RVA: 0x0005A46F File Offset: 0x0005866F
		private void ResponseReceived(IAsyncResult result)
		{
			this.session.EndGet<object>(result, null);
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x040024D4 RID: 9428
		private const TestId ID = TestId.OwaDownloadStaticFile;
	}
}
