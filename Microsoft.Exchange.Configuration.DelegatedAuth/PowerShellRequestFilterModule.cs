using System;
using System.Collections.Specialized;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.DelegatedAuthentication;

namespace Microsoft.Exchange.Configuration.DelegatedAuthentication
{
	// Token: 0x02000008 RID: 8
	public class PowerShellRequestFilterModule : IHttpModule
	{
		// Token: 0x0600004B RID: 75 RVA: 0x000041FA File Offset: 0x000023FA
		void IHttpModule.Init(HttpApplication application)
		{
			application.BeginRequest += PowerShellRequestFilterModule.OnBeginRequest;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000420E File Offset: 0x0000240E
		void IHttpModule.Dispose()
		{
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00004210 File Offset: 0x00002410
		private static void OnBeginRequest(object sender, EventArgs e)
		{
			ExTraceGlobals.DelegatedAuthTracer.TraceFunction<string>(0L, "Enter Function: {0}.", "OnBeginRequest");
			HttpApplication httpApplication = (HttpApplication)sender;
			HttpContext context = httpApplication.Context;
			HttpRequest request = context.Request;
			PowerShellRequestFilterModule.AddHeadersForDelegation(request);
			ExTraceGlobals.DelegatedAuthTracer.TraceFunction<string>(0L, "Exit Function: {0}.", "OnBeginRequest");
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00004264 File Offset: 0x00002464
		private static void AddHeadersForDelegation(HttpRequest request)
		{
			string targetOrgName = PowerShellRequestFilterModule.GetTargetOrgName(request);
			if (targetOrgName != null)
			{
				request.Headers.Add("msExchTargetTenant", targetOrgName);
			}
			request.Headers.Add("msExchOriginalUrl", request.Url.AbsoluteUri);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000042A8 File Offset: 0x000024A8
		private static string GetTargetOrgName(HttpRequest request)
		{
			UriBuilder uriBuilder = new UriBuilder(request.Url);
			NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(uriBuilder.Query.Replace(';', '&'));
			return nameValueCollection["DelegatedOrg"];
		}
	}
}
