using System;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Web;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000255 RID: 597
	internal class OwaServiceHttpAsyncHandler : OwaServiceHttpHandler, IHttpAsyncHandler, IHttpHandler
	{
		// Token: 0x06001683 RID: 5763 RVA: 0x00053124 File Offset: 0x00051324
		internal OwaServiceHttpAsyncHandler(HttpContext httpContext, OWAService service, ServiceMethodInfo methodInfo) : base(httpContext, service, methodInfo)
		{
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x00053130 File Offset: 0x00051330
		public IAsyncResult BeginProcessRequest(HttpContext httpContext, AsyncCallback cb, object extraData)
		{
			OwaServerLogger.LogWcfLatency(httpContext);
			IAsyncResult result;
			try
			{
				result = this.InternalBeginProcessRequest(httpContext, cb, extraData);
			}
			catch (Exception ex)
			{
				base.FaultHandler.ProvideFault(ex, httpContext.Response);
				result = new AsyncResult(cb, null, true)
				{
					Exception = ex
				};
			}
			return result;
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x00053188 File Offset: 0x00051388
		public void EndProcessRequest(IAsyncResult result)
		{
			AsyncResult asyncResult = result as AsyncResult;
			if (asyncResult != null && asyncResult.Exception != null)
			{
				return;
			}
			HttpContext.Current = base.HttpContext;
			try
			{
				this.InternalEndProcessRequest(result);
			}
			catch (Exception error)
			{
				base.FaultHandler.ProvideFault(error, base.HttpContext.Response);
			}
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x000531E8 File Offset: 0x000513E8
		private IAsyncResult InternalBeginProcessRequest(HttpContext httpContext, AsyncCallback cb, object extraData)
		{
			try
			{
				HttpRequest request = httpContext.Request;
				HttpResponse response = httpContext.Response;
				base.Initialize(response);
				if (base.ServiceMethodInfo.IsHttpGet)
				{
					return base.MethodDispatcher.InvokeBeginGetMethod(base.ServiceMethodInfo, base.Service, request, cb);
				}
				return base.MethodDispatcher.InvokeBeginMethod(base.ServiceMethodInfo, base.Service, request, cb);
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
			return null;
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x000532B0 File Offset: 0x000514B0
		private void InternalEndProcessRequest(IAsyncResult result)
		{
			try
			{
				base.MethodDispatcher.InvokeEndMethod(base.ServiceMethodInfo, base.Service, result, base.HttpContext.Response);
			}
			catch (TargetInvocationException ex)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<Exception>(0L, "Method invocation target threw an exception: {0}", ex.InnerException);
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
