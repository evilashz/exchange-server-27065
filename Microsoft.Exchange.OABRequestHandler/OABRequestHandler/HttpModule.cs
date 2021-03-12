using System;
using System.Web;

namespace Microsoft.Exchange.OABRequestHandler
{
	// Token: 0x02000005 RID: 5
	public class HttpModule : IHttpModule
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00003E27 File Offset: 0x00002027
		public HttpModule()
		{
			this.fileHandler = new OABRequestHandler();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003E3A File Offset: 0x0000203A
		void IHttpModule.Init(HttpApplication application)
		{
			BITSDownloadManager.Instance.InitializeIfNecessary();
			application.PostAuthenticateRequest += this.PostAuthorizeRequest;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003E58 File Offset: 0x00002058
		void IHttpModule.Dispose()
		{
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003E5A File Offset: 0x0000205A
		private void PostAuthorizeRequest(object source, EventArgs args)
		{
			this.fileHandler.HandleRequest(HttpContext.Current);
		}

		// Token: 0x0400001D RID: 29
		private readonly OABRequestHandler fileHandler;
	}
}
