using System;
using System.Web;
using Microsoft.Exchange.HttpProxy;
using Microsoft.Exchange.Net.Protocols;

namespace Microsoft.Exchange.HttpUtilities
{
	// Token: 0x02000005 RID: 5
	internal class StrictTransportSecurityModule : IHttpModule
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002440 File Offset: 0x00000640
		public void Init(HttpApplication application)
		{
			application.PreSendRequestHeaders += this.Application_PreSendRequestHeaders;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002454 File Offset: 0x00000654
		public void Dispose()
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000024BC File Offset: 0x000006BC
		private void Application_PreSendRequestHeaders(object sender, EventArgs e)
		{
			HttpApplication httpApplication = (HttpApplication)sender;
			HttpContext httpContext = httpApplication.Context;
			Diagnostics.SendWatsonReportOnUnhandledException(delegate()
			{
				if (httpContext.Request.IsSecureConnection || httpContext.Request.Url.Scheme == Uri.UriSchemeHttps)
				{
					httpContext.Response.AddHeader(WellKnownHeader.StrictTransportSecurity, "max-age=31536000; includeSubDomains");
				}
			}, new Diagnostics.LastChanceExceptionHandler(this.LastChanceExceptionHandler));
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002500 File Offset: 0x00000700
		private void LastChanceExceptionHandler(Exception unhandledException)
		{
			RequestLogger logger = RequestLogger.GetLogger(new HttpContextWrapper(HttpContext.Current));
			if (logger != null)
			{
				logger.LastChanceExceptionHandler(unhandledException);
			}
		}
	}
}
