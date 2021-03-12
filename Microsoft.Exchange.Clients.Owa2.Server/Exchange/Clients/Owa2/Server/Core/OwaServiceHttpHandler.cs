using System;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Web;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000254 RID: 596
	internal class OwaServiceHttpHandler : OwaServiceHttpHandlerBase, IHttpHandler
	{
		// Token: 0x0600167D RID: 5757 RVA: 0x00052FCF File Offset: 0x000511CF
		internal OwaServiceHttpHandler(HttpContext httpContext, OWAService service, ServiceMethodInfo methodInfo) : base(httpContext, service, methodInfo)
		{
			this.FaultHandler = new OWAFaultHandler();
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x0600167E RID: 5758 RVA: 0x00052FE5 File Offset: 0x000511E5
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x0600167F RID: 5759 RVA: 0x00052FE8 File Offset: 0x000511E8
		// (set) Token: 0x06001680 RID: 5760 RVA: 0x00052FF0 File Offset: 0x000511F0
		private protected OWAFaultHandler FaultHandler { protected get; private set; }

		// Token: 0x06001681 RID: 5761 RVA: 0x00052FFC File Offset: 0x000511FC
		public void ProcessRequest(HttpContext httpContext)
		{
			try
			{
				this.InternalProcessRequest(httpContext);
			}
			catch (Exception error)
			{
				this.FaultHandler.ProvideFault(error, httpContext.Response);
			}
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x00053038 File Offset: 0x00051238
		private void InternalProcessRequest(HttpContext httpContext)
		{
			ExTraceGlobals.CoreTracer.TraceDebug(0L, "OwaServiceHttpHandler.ProcessRequest");
			OwaServerLogger.LogWcfLatency(httpContext);
			try
			{
				HttpRequest request = httpContext.Request;
				HttpResponse response = httpContext.Response;
				base.Initialize(response);
				if (base.ServiceMethodInfo.IsHttpGet)
				{
					Uri url = request.Url;
					new Uri(request.Path, UriKind.Relative);
					base.MethodDispatcher.InvokeGetMethod(base.ServiceMethodInfo, base.Service, request, response);
				}
				else
				{
					base.MethodDispatcher.InvokeMethod(base.ServiceMethodInfo, base.Service, request, response);
				}
			}
			catch (TargetInvocationException ex)
			{
				ExTraceGlobals.CoreTracer.TraceError<Exception>(0L, "Method invocation target threw an exception: {0}", ex.InnerException);
				ExceptionDispatchInfo exceptionDispatchInfo = ExceptionDispatchInfo.Capture(ex.InnerException ?? ex);
				exceptionDispatchInfo.Throw();
			}
			finally
			{
				base.MethodDispatcher.DisposeParameters();
			}
		}
	}
}
