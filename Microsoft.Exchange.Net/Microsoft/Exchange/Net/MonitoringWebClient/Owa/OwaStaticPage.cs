using System;
using Microsoft.Exchange.Net.MonitoringWebClient.Owa.Parsers;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Owa
{
	// Token: 0x020007DB RID: 2011
	internal class OwaStaticPage : BaseTestStep
	{
		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x060029CE RID: 10702 RVA: 0x00059F3E File Offset: 0x0005813E
		// (set) Token: 0x060029CF RID: 10703 RVA: 0x00059F46 File Offset: 0x00058146
		public Uri Uri { get; private set; }

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x060029D0 RID: 10704 RVA: 0x00059F4F File Offset: 0x0005814F
		// (set) Token: 0x060029D1 RID: 10705 RVA: 0x00059F57 File Offset: 0x00058157
		public ITestFactory TestFactory { get; private set; }

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x060029D2 RID: 10706 RVA: 0x00059F60 File Offset: 0x00058160
		protected override TestId Id
		{
			get
			{
				return TestId.OwaStaticPage;
			}
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x00059F63 File Offset: 0x00058163
		public OwaStaticPage(Uri uri, ITestFactory factory)
		{
			this.Uri = uri;
			this.TestFactory = factory;
		}

		// Token: 0x060029D4 RID: 10708 RVA: 0x00059F79 File Offset: 0x00058179
		protected override void Finally()
		{
			this.session.CloseConnections();
		}

		// Token: 0x060029D5 RID: 10709 RVA: 0x00059F9B File Offset: 0x0005819B
		protected override void StartTest()
		{
			this.session.BeginGet(this.Id, new Uri(this.Uri, "auth/preload.aspx").ToString(), delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.PreloadPageResponseReceived), tempResult);
			}, null);
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x00059FF0 File Offset: 0x000581F0
		private void PreloadPageResponseReceived(IAsyncResult result)
		{
			OwaPreloadPage owaPreloadPage = this.session.EndGet<OwaPreloadPage>(result, (HttpWebResponseWrapper response) => OwaPreloadPage.Parse(response));
			string str = "/themes/resources/clear1x1.gif";
			if (owaPreloadPage.CdnUri.PathAndQuery.Contains("/15."))
			{
				str = "/owa2/scripts/microsoft.exchange.clients.owa2.client.core.framework.js";
			}
			UriBuilder uriBuilder = new UriBuilder(owaPreloadPage.CdnUri);
			UriBuilder uriBuilder2 = uriBuilder;
			uriBuilder2.Path += str;
			ITestStep testStep = this.TestFactory.CreateOwaDownloadStaticFileStep(uriBuilder.Uri);
			testStep.BeginExecute(this.session, delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.OwaDownloadStaticFileStepCompleted), tempResult);
			}, testStep);
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x0005A094 File Offset: 0x00058294
		private void OwaDownloadStaticFileStepCompleted(IAsyncResult result)
		{
			ITestStep testStep = result.AsyncState as ITestStep;
			testStep.EndExecute(result);
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x040024C7 RID: 9415
		private const TestId ID = TestId.OwaStaticPage;
	}
}
