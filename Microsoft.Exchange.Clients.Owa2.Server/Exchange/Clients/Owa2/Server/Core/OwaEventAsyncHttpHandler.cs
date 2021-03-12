using System;
using System.Reflection;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001D5 RID: 469
	internal class OwaEventAsyncHttpHandler : IHttpAsyncHandler, IHttpHandler
	{
		// Token: 0x0600109F RID: 4255 RVA: 0x0003FBE0 File Offset: 0x0003DDE0
		internal OwaEventAsyncHttpHandler(OwaEventHandlerBase eventHandler)
		{
			this.eventHandler = eventHandler;
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060010A0 RID: 4256 RVA: 0x0003FBEF File Offset: 0x0003DDEF
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060010A1 RID: 4257 RVA: 0x0003FBF2 File Offset: 0x0003DDF2
		public OwaEventHandlerBase EventHandler
		{
			get
			{
				return this.eventHandler;
			}
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x0003FBFA File Offset: 0x0003DDFA
		public void ProcessRequest(HttpContext httpContext)
		{
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0003FBFC File Offset: 0x0003DDFC
		public IAsyncResult BeginProcessRequest(HttpContext httpContext, AsyncCallback callback, object context)
		{
			ExTraceGlobals.OehCallTracer.TraceDebug(0L, "OwaEventAsyncHttpHandler.BeginProcessRequest");
			string text = httpContext.Request.QueryString["X-SuiteServiceProxyOrigin"] ?? httpContext.Request.Headers["X-SuiteServiceProxyOrigin"];
			if (!string.IsNullOrWhiteSpace(text))
			{
				httpContext.Response.AppendToLog("&SuiteServiceOrigin=" + text);
			}
			ExTraceGlobals.OehTracer.TraceDebug(0L, "Parsing request");
			OwaEventParserBase parser = this.GetParser();
			this.EventHandler.SetParameterTable(parser.Parse());
			ExTraceGlobals.OehTracer.TraceDebug(0L, "Invoking handler");
			object[] parameters = new object[]
			{
				callback,
				context
			};
			object obj = this.EventHandler.EventInfo.BeginMethodInfo.Invoke(this.EventHandler, BindingFlags.InvokeMethod, null, parameters, null);
			return (IAsyncResult)obj;
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x0003FCE0 File Offset: 0x0003DEE0
		public void EndProcessRequest(IAsyncResult asyncResult)
		{
			ExTraceGlobals.OehCallTracer.TraceDebug(0L, "OwaEventAsyncHttpHandler.EndProcessRequest");
			try
			{
				object[] parameters = new object[]
				{
					asyncResult
				};
				this.EventHandler.EventInfo.EndMethodInfo.Invoke(this.EventHandler, BindingFlags.InvokeMethod, null, parameters, null);
				this.FinishSuccesfulRequest();
			}
			catch (ThreadAbortException)
			{
			}
			catch (TargetInvocationException)
			{
				ExTraceGlobals.OehTracer.TraceDebug(0L, "Handler async end invocation target threw an exception");
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

		// Token: 0x060010A5 RID: 4261 RVA: 0x0003FD94 File Offset: 0x0003DF94
		internal OwaEventParserBase GetParser()
		{
			return new OwaEventUrlParser(this.EventHandler);
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x0003FDA4 File Offset: 0x0003DFA4
		protected void FinishSuccesfulRequest()
		{
			if (!this.EventHandler.DontWriteHeaders)
			{
				this.EventHandler.HttpContext.Response.AppendHeader("X-OWA-EventResult", "0");
				HttpUtilities.MakePageNoCacheNoStore(this.EventHandler.HttpContext.Response);
				this.EventHandler.HttpContext.Response.ContentType = HttpUtilities.GetContentTypeString(this.EventHandler.ResponseContentType);
			}
		}

		// Token: 0x040009D4 RID: 2516
		private OwaEventHandlerBase eventHandler;
	}
}
