using System;
using System.Reflection;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001DB RID: 475
	internal class OwaEventHttpHandler : IHttpHandler
	{
		// Token: 0x060010D6 RID: 4310 RVA: 0x00040198 File Offset: 0x0003E398
		internal OwaEventHttpHandler(OwaEventHandlerBase eventHandler)
		{
			this.eventHandler = eventHandler;
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x000401A7 File Offset: 0x0003E3A7
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060010D8 RID: 4312 RVA: 0x000401AA File Offset: 0x0003E3AA
		public OwaEventHandlerBase EventHandler
		{
			get
			{
				return this.eventHandler;
			}
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x000401B4 File Offset: 0x0003E3B4
		public void ProcessRequest(HttpContext httpContext)
		{
			ExTraceGlobals.OehCallTracer.TraceDebug(0L, "OwaEventHttpHandler.ProcessRequest");
			try
			{
				ExTraceGlobals.OehTracer.TraceDebug(0L, "Parsing request");
				string text = httpContext.Request.QueryString["X-SuiteServiceProxyOrigin"] ?? httpContext.Request.Headers["X-SuiteServiceProxyOrigin"];
				if (!string.IsNullOrWhiteSpace(text))
				{
					httpContext.Response.AppendToLog("&SuiteServiceOrigin=" + text);
				}
				OwaEventParserBase parser = this.GetParser();
				this.EventHandler.SetParameterTable(parser.Parse());
				ExTraceGlobals.OehTracer.TraceDebug<string>(0L, "Invoking event {0}", this.EventHandler.EventInfo.Name);
				this.EventHandler.EventInfo.MethodInfo.Invoke(this.EventHandler, BindingFlags.InvokeMethod, null, null, null);
				this.FinishSuccesfulRequest();
			}
			catch (ThreadAbortException)
			{
			}
			catch (TargetInvocationException)
			{
				ExTraceGlobals.OehTracer.TraceDebug(0L, "Handler invocation target threw an exception");
			}
			finally
			{
				if (this.eventHandler != null)
				{
					this.eventHandler.Dispose();
				}
				this.eventHandler = null;
			}
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x000402F4 File Offset: 0x0003E4F4
		internal OwaEventParserBase GetParser()
		{
			return new OwaEventUrlParser(this.EventHandler);
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x00040304 File Offset: 0x0003E504
		protected void FinishSuccesfulRequest()
		{
			if (!this.EventHandler.DontWriteHeaders)
			{
				this.EventHandler.HttpContext.Response.AppendHeader("X-OWA-EventResult", "0");
				HttpUtilities.MakePageNoCacheNoStore(this.EventHandler.HttpContext.Response);
				this.EventHandler.HttpContext.Response.ContentType = HttpUtilities.GetContentTypeString(this.EventHandler.ResponseContentType);
			}
		}

		// Token: 0x040009F5 RID: 2549
		private OwaEventHandlerBase eventHandler;
	}
}
