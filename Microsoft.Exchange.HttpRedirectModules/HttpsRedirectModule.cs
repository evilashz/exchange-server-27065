using System;
using System.Web;
using Microsoft.Exchange.HttpProxy;

namespace Microsoft.Exchange.HttpRedirect
{
	// Token: 0x02000005 RID: 5
	public class HttpsRedirectModule : IHttpModule
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002337 File Offset: 0x00000537
		public void Init(HttpApplication application)
		{
			application.BeginRequest += this.OnBeginRequest;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000234B File Offset: 0x0000054B
		public void Dispose()
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002350 File Offset: 0x00000550
		internal static bool TryGetRedirectUri(Uri uri, out Uri redirectUri)
		{
			redirectUri = null;
			if (HttpRedirectCommon.UriIsHttps(uri))
			{
				return false;
			}
			redirectUri = new UriBuilder(uri)
			{
				Scheme = "https",
				Port = 443
			}.Uri;
			return true;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023B0 File Offset: 0x000005B0
		private void OnBeginRequest(object sender, EventArgs e)
		{
			Diagnostics.SendWatsonReportOnUnhandledException(delegate()
			{
				this.OnBeginRequestInternal((HttpApplication)sender);
			});
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023E4 File Offset: 0x000005E4
		private void OnBeginRequestInternal(HttpApplication httpApplication)
		{
			HttpContext context = httpApplication.Context;
			Uri url = context.Request.Url;
			Uri redirectUri = null;
			if (HttpsRedirectModule.TryGetRedirectUri(url, out redirectUri))
			{
				HttpRedirectCommon.RedirectRequestToNewUri(httpApplication, HttpRedirectCommon.HttpRedirectType.Permanent, redirectUri, "HttpsRedirectUri=");
			}
		}

		// Token: 0x04000013 RID: 19
		internal const string IISLogFieldPrefix = "HttpsRedirectUri=";
	}
}
