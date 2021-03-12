using System;
using System.Reflection;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000182 RID: 386
	internal class OwaEventAsyncHttpHandler : OwaEventHttpHandler, IHttpAsyncHandler, IHttpHandler
	{
		// Token: 0x06000E1C RID: 3612 RVA: 0x0005B644 File Offset: 0x00059844
		public OwaEventAsyncHttpHandler(OwaEventHandlerBase eventHandler) : base(eventHandler)
		{
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x0005B650 File Offset: 0x00059850
		public IAsyncResult BeginProcessRequest(HttpContext httpContext, AsyncCallback callback, object context)
		{
			ExTraceGlobals.OehCallTracer.TraceDebug(0L, "OwaEventAsyncHttpHandler.BeginProcessRequest");
			IAsyncResult result = null;
			try
			{
				ExTraceGlobals.OehTracer.TraceDebug(0L, "Parsing request");
				OwaEventParserBase parser = base.GetParser();
				base.EventHandler.SetParameterTable(parser.Parse());
				ExTraceGlobals.OehTracer.TraceDebug(0L, "Invoking handler");
				object[] parameters = new object[]
				{
					callback,
					context
				};
				object obj = base.EventHandler.EventInfo.BeginMethodInfo.Invoke(base.EventHandler, BindingFlags.InvokeMethod, null, parameters, null);
				result = (IAsyncResult)obj;
			}
			catch (ThreadAbortException)
			{
				base.EventHandler.OwaContext.UnlockMinResourcesOnCriticalError();
			}
			catch (TargetInvocationException ex)
			{
				ExTraceGlobals.OehTracer.TraceDebug(0L, "Handler async begin invocation target threw an exception");
				Utilities.HandleException(base.EventHandler.OwaContext, ex.InnerException, base.EventHandler.ShowErrorInPage);
			}
			return result;
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0005B750 File Offset: 0x00059950
		public void EndProcessRequest(IAsyncResult asyncResult)
		{
			ExTraceGlobals.OehCallTracer.TraceDebug(0L, "OwaEventAsyncHttpHandler.EndProcessRequest");
			try
			{
				object[] parameters = new object[]
				{
					asyncResult
				};
				base.EventHandler.EventInfo.EndMethodInfo.Invoke(base.EventHandler, BindingFlags.InvokeMethod, null, parameters, null);
				base.FinishSuccesfulRequest();
			}
			catch (ThreadAbortException)
			{
				base.EventHandler.OwaContext.UnlockMinResourcesOnCriticalError();
			}
			catch (TargetInvocationException ex)
			{
				ExTraceGlobals.OehTracer.TraceDebug(0L, "Handler async end invocation target threw an exception");
				Utilities.HandleException(base.EventHandler.OwaContext, ex.InnerException, base.EventHandler.ShowErrorInPage);
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
	}
}
