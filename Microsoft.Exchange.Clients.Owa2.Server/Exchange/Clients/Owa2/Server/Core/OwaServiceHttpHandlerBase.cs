using System;
using System.Web;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200024D RID: 589
	internal abstract class OwaServiceHttpHandlerBase
	{
		// Token: 0x0600162D RID: 5677 RVA: 0x000510B7 File Offset: 0x0004F2B7
		internal OwaServiceHttpHandlerBase(HttpContext httpContext, OWAService service, ServiceMethodInfo methodInfo)
		{
			this.HttpContext = httpContext;
			this.Service = service;
			this.ServiceMethodInfo = methodInfo;
			this.Inspector = new OwaServiceMessageInspector();
			this.MethodDispatcher = new OwaServiceMethodDispatcher(this.Inspector);
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x0600162E RID: 5678 RVA: 0x000510F0 File Offset: 0x0004F2F0
		// (set) Token: 0x0600162F RID: 5679 RVA: 0x000510F8 File Offset: 0x0004F2F8
		private protected OWAService Service { protected get; private set; }

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001630 RID: 5680 RVA: 0x00051101 File Offset: 0x0004F301
		// (set) Token: 0x06001631 RID: 5681 RVA: 0x00051109 File Offset: 0x0004F309
		private protected IOwaServiceMessageInspector Inspector { protected get; private set; }

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001632 RID: 5682 RVA: 0x00051112 File Offset: 0x0004F312
		// (set) Token: 0x06001633 RID: 5683 RVA: 0x0005111A File Offset: 0x0004F31A
		private protected ServiceMethodInfo ServiceMethodInfo { protected get; private set; }

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x00051123 File Offset: 0x0004F323
		// (set) Token: 0x06001635 RID: 5685 RVA: 0x0005112B File Offset: 0x0004F32B
		private protected OwaServiceMethodDispatcher MethodDispatcher { protected get; private set; }

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001636 RID: 5686 RVA: 0x00051134 File Offset: 0x0004F334
		// (set) Token: 0x06001637 RID: 5687 RVA: 0x0005113C File Offset: 0x0004F33C
		private protected HttpContext HttpContext { protected get; private set; }

		// Token: 0x06001638 RID: 5688 RVA: 0x00051148 File Offset: 0x0004F348
		protected void Initialize(HttpResponse response)
		{
			response.ContentType = "application/json; charset=utf-8";
			response.AddHeader("X-OWA-HttpHandler", "true");
			if (!this.ServiceMethodInfo.IsResponseCacheable)
			{
				response.Cache.SetNoServerCaching();
				response.Cache.SetCacheability(HttpCacheability.NoCache);
				response.Cache.SetNoStore();
			}
		}
	}
}
