using System;
using System.Web;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.Configuration.Core;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x02000025 RID: 37
	public class ProbePingModule : IHttpModule
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x00006611 File Offset: 0x00004811
		void IHttpModule.Init(HttpApplication context)
		{
			context.BeginRequest += this.OnBeginRequest;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00006625 File Offset: 0x00004825
		void IHttpModule.Dispose()
		{
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00006628 File Offset: 0x00004828
		private void OnBeginRequest(object sender, EventArgs e)
		{
			ExTraceGlobals.HttpModuleTracer.TraceFunction((long)this.GetHashCode(), "[ProbePingModule::OnBeginRequest] Enter");
			HttpContext httpContext = HttpContext.Current;
			HttpRequest request = httpContext.Request;
			if (LoggerHelper.IsProbePingRequest(request))
			{
				HttpLogger.SafeSetLogger(RpsHttpMetadata.Action, "ProbePing");
				HttpContext.Current.ApplicationInstance.CompleteRequest();
			}
			ExTraceGlobals.HttpModuleTracer.TraceFunction((long)this.GetHashCode(), "[ProbePingModule::OnBeginRequest] Exit.");
		}
	}
}
