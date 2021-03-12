using System;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.Configuration.Core;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x02000023 RID: 35
	public class PingDetectionModule : IHttpModule
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x0000634C File Offset: 0x0000454C
		void IHttpModule.Init(HttpApplication context)
		{
			context.PreSendRequestHeaders += this.OnPreSendRequestHeaders;
			context.EndRequest += this.OnEndRequest;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00006372 File Offset: 0x00004572
		void IHttpModule.Dispose()
		{
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00006374 File Offset: 0x00004574
		private void OnEndRequest(object sender, EventArgs e)
		{
			this.ReviseAction("OnEndRequest");
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00006381 File Offset: 0x00004581
		private void OnPreSendRequestHeaders(object sender, EventArgs e)
		{
			this.ReviseAction("OnPreSendRequestHeaders");
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00006390 File Offset: 0x00004590
		private void ReviseAction(string funcName)
		{
			ExTraceGlobals.HttpModuleTracer.TraceFunction<string>((long)this.GetHashCode(), "[PingDetectionModule::{0}] Enter", funcName);
			HttpContext httpContext = HttpContext.Current;
			HttpResponse response = httpContext.Response;
			if (response == null)
			{
				return;
			}
			if (httpContext.Items["ActionHasBeenRevised"] != null)
			{
				return;
			}
			httpContext.Items["ActionHasBeenRevised"] = "Y";
			WinRMInfo winRMInfoFromHttpHeaders = WinRMInfo.GetWinRMInfoFromHttpHeaders(httpContext.Request.Headers);
			string text;
			if (WinRMRequestTracker.TryReviseAction(winRMInfoFromHttpHeaders, response.StatusCode, response.SubStatusCode, out text))
			{
				HttpLogger.SafeSetLogger(RpsHttpMetadata.Action, text);
				string headerValue = "Ping".Equals(text) ? "Ping" : "Non-Ping";
				this.SafeSetResponseHeader(response, "X-RemotePS-Ping", headerValue);
				this.SafeSetResponseHeader(response, "X-RemotePS-RevisedAction", text);
			}
			ExTraceGlobals.HttpModuleTracer.TraceFunction<string>((long)this.GetHashCode(), "[PingDetectionModule::{0}] Exit.", funcName);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00006470 File Offset: 0x00004670
		private void SafeSetResponseHeader(HttpResponse response, string header, string headerValue)
		{
			try
			{
				response.Headers[header] = headerValue;
			}
			catch (Exception ex)
			{
				HttpLogger.SafeAppendGenericError("PingDetctionModule.SafeSetResponseHeader", ex.ToString(), false);
			}
		}

		// Token: 0x04000089 RID: 137
		private const string ActionHasBeenRevisedItemKey = "ActionHasBeenRevised";
	}
}
