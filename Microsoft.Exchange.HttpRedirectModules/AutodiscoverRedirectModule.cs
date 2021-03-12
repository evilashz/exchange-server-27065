using System;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.HttpProxy;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.HttpRedirect
{
	// Token: 0x02000002 RID: 2
	public class AutodiscoverRedirectModule : IHttpModule
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public void Init(HttpApplication application)
		{
			application.BeginRequest += this.OnBeginRequest;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020E4 File Offset: 0x000002E4
		public void Dispose()
		{
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020E8 File Offset: 0x000002E8
		internal static bool TryGetRedirectUri(Uri uri, string canonicalSecureHostName, out Uri redirectUri)
		{
			redirectUri = null;
			if (HttpRedirectCommon.UriIsHttps(uri))
			{
				return false;
			}
			redirectUri = new UriBuilder(uri)
			{
				Host = canonicalSecureHostName,
				Scheme = "https",
				Port = 443
			}.Uri;
			return true;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002150 File Offset: 0x00000350
		private void OnBeginRequest(object sender, EventArgs e)
		{
			Diagnostics.SendWatsonReportOnUnhandledException(delegate()
			{
				this.OnBeginRequestInternal((HttpApplication)sender);
			});
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002184 File Offset: 0x00000384
		private void OnBeginRequestInternal(HttpApplication httpApplication)
		{
			HttpContext context = httpApplication.Context;
			Uri url = context.Request.Url;
			Uri redirectUri = null;
			if (string.IsNullOrEmpty(AutodiscoverRedirectModule.CanonicalSecureHostNameSetting.Value))
			{
				return;
			}
			if (AutodiscoverRedirectModule.TryGetRedirectUri(url, AutodiscoverRedirectModule.CanonicalSecureHostNameSetting.Value, out redirectUri))
			{
				HttpRedirectCommon.RedirectRequestToNewUri(httpApplication, HttpRedirectCommon.HttpRedirectType.Temporary, redirectUri, "AutodiscoverRedirectUri=");
			}
		}

		// Token: 0x04000001 RID: 1
		internal const string IISLogFieldPrefix = "AutodiscoverRedirectUri=";

		// Token: 0x04000002 RID: 2
		internal const string AutodiscoverCanonicalSecureHostNameKey = "AutodiscoverCanonicalSecureHostName";

		// Token: 0x04000003 RID: 3
		private static readonly StringAppSettingsEntry CanonicalSecureHostNameSetting = new StringAppSettingsEntry("AutodiscoverCanonicalSecureHostName", string.Empty, ExTraceGlobals.VerboseTracer);
	}
}
