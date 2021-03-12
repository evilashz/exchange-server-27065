using System;
using System.Web;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001B1 RID: 433
	internal sealed class DiagnosticsModule : IHttpModule
	{
		// Token: 0x060023B9 RID: 9145 RVA: 0x0006D582 File Offset: 0x0006B782
		public void Init(HttpApplication application)
		{
			application.BeginRequest += this.OnBeginRequest;
			application.EndRequest += this.OnEndRequest;
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x0006D5A8 File Offset: 0x0006B7A8
		public void Dispose()
		{
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x0006D5AC File Offset: 0x0006B7AC
		private void OnBeginRequest(object sender, EventArgs e)
		{
			HttpContext httpContext = HttpContext.Current;
			if (Utility.IsResourceRequest(httpContext.GetRequestUrl().LocalPath))
			{
				return;
			}
			ActivityContextManager.InitializeActivityContext(httpContext, ActivityContextLoggerId.Request);
			string str = ActivityContext.ActivityId.FormatForLog();
			if (HttpContext.Current.Request.QueryString.ToString().Length > 0)
			{
				HttpContext.Current.Response.AppendToLog("&ActID=" + str);
				return;
			}
			HttpContext.Current.Response.AppendToLog("ActID=" + str);
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x0006D638 File Offset: 0x0006B838
		private void OnEndRequest(object sender, EventArgs e)
		{
			HttpContext httpContext = HttpContext.Current;
			if (Utility.IsResourceRequest(httpContext.GetRequestUrl().LocalPath))
			{
				return;
			}
			ActivityContextManager.CleanupActivityContext(httpContext);
		}
	}
}
