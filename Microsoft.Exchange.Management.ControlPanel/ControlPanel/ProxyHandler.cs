using System;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200039C RID: 924
	internal sealed class ProxyHandler : IHttpAsyncHandler, IHttpHandler
	{
		// Token: 0x0600310A RID: 12554 RVA: 0x00095BE9 File Offset: 0x00093DE9
		public ProxyHandler(OutboundProxySession session)
		{
			this.session = session;
		}

		// Token: 0x0600310B RID: 12555 RVA: 0x00095BF8 File Offset: 0x00093DF8
		public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback requestCompletedCallback, object requestCompletedData)
		{
			ExTraceGlobals.ProxyTracer.TraceInformation<string, string>(0, 0L, "BeginProcessRequest: {0} {1}", context.Request.RequestType, context.GetRequestUrlForLog());
			context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
			context.Response.Cache.SetNoStore();
			return this.session.BeginSendOutboundProxyRequest(context, requestCompletedCallback, requestCompletedData);
		}

		// Token: 0x0600310C RID: 12556 RVA: 0x00095C57 File Offset: 0x00093E57
		public void EndProcessRequest(IAsyncResult result)
		{
			ExTraceGlobals.ProxyTracer.TraceInformation(0, 0L, "EndProcessRequest");
			this.session.EndSendOutboundProxyRequest(result);
		}

		// Token: 0x17001F57 RID: 8023
		// (get) Token: 0x0600310D RID: 12557 RVA: 0x00095C77 File Offset: 0x00093E77
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600310E RID: 12558 RVA: 0x00095C7A File Offset: 0x00093E7A
		public void ProcessRequest(HttpContext context)
		{
			this.EndProcessRequest(this.BeginProcessRequest(context, null, null));
		}

		// Token: 0x040023BA RID: 9146
		private OutboundProxySession session;
	}
}
