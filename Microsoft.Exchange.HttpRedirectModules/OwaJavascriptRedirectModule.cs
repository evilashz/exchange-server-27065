using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.HttpProxy;

namespace Microsoft.Exchange.HttpRedirect
{
	// Token: 0x02000007 RID: 7
	public class OwaJavascriptRedirectModule : IHttpModule
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000027BC File Offset: 0x000009BC
		public static bool TryGetOwa302PathAndQuery(string requestPath, string requestPathAndQuery, out string owaPathAndQuery)
		{
			requestPath = (requestPath ?? string.Empty);
			requestPathAndQuery = (requestPathAndQuery ?? string.Empty);
			owaPathAndQuery = string.Empty;
			if (Regex.IsMatch(requestPath, "^/?[^./]+[.][^/]+([/][^./@]+[@][^./]+[.][^/]+)?/?$"))
			{
				string str = requestPathAndQuery.StartsWith("/") ? "/owa" : "/owa/";
				owaPathAndQuery = str + requestPathAndQuery;
			}
			return !string.IsNullOrEmpty(owaPathAndQuery);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002824 File Offset: 0x00000A24
		public void Init(HttpApplication application)
		{
			if (!OwaJavascriptRedirectModule.staticInitialized)
			{
				bool flag = false;
				if (WebConfigurationManager.AppSettings["GallatinSplashPageEnabled"] != null)
				{
					string value = WebConfigurationManager.AppSettings["GallatinSplashPageEnabled"];
					bool.TryParse(value, out flag);
				}
				if (flag)
				{
					string text = WebConfigurationManager.AppSettings["GallatinSplashPagePath"];
					OwaJavascriptRedirectModule.gallatinSplashPageRequiredForUrl = (text ?? string.Empty);
				}
				OwaJavascriptRedirectModule.gallatinSplashPageEnabled = flag;
				OwaJavascriptRedirectModule.staticInitialized = true;
			}
			application.BeginRequest += this.OnBeginRequest;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000028A4 File Offset: 0x00000AA4
		public void Dispose()
		{
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000028C8 File Offset: 0x00000AC8
		private void OnBeginRequest(object sender, EventArgs e)
		{
			Diagnostics.SendWatsonReportOnUnhandledException(delegate()
			{
				this.OnBeginRequestInternal((HttpApplication)sender);
			});
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000028FC File Offset: 0x00000AFC
		private void OnBeginRequestInternal(HttpApplication httpApplication)
		{
			HttpContext context = httpApplication.Context;
			HttpRequest request = context.Request;
			if (context.Request.IsSecureConnection)
			{
				string text = request.Url.AbsolutePath ?? string.Empty;
				string requestPathAndQuery = request.Url.PathAndQuery ?? string.Empty;
				string text2 = request.Url.AbsoluteUri ?? string.Empty;
				HttpResponse response = context.Response;
				if (OwaJavascriptRedirectModule.gallatinSplashPageEnabled && OwaJavascriptRedirectModule.gallatinSplashPageRequiredForUrl.Equals(text2, StringComparison.OrdinalIgnoreCase))
				{
					context.Server.TransferRequest("/owa/auth/outlookcn.aspx");
					return;
				}
				if (string.IsNullOrEmpty(text) || text == "/")
				{
					if (OfflineClientRequestUtilities.IsRequestForAppCachedVersion(context))
					{
						string arg = Uri.EscapeUriString(Uri.UnescapeDataString("/owa/" + context.Request.Url.Query));
						response.StatusCode = 200;
						response.ContentType = "text/html";
						response.Write(string.Format("<!DOCTYPE html><html><head><script type=\"text/javascript\">window.location.replace(\"{0}\" + window.location.hash);</script></head><body></body></html>", arg));
						response.AppendToLog("OwaJavascriptRedirectUri=/owa/");
						httpApplication.CompleteRequest();
						return;
					}
					HttpRedirectCommon.RedirectRequestToNewUri(httpApplication, HttpRedirectCommon.HttpRedirectType.Permanent, new UriBuilder(text2)
					{
						Path = "/owa/"
					}.Uri, "Owa301RedirectUri=");
					return;
				}
				else
				{
					string empty = string.Empty;
					if (OwaJavascriptRedirectModule.TryGetOwa302PathAndQuery(text, requestPathAndQuery, out empty))
					{
						response.AppendToLog("Owa302RedirectUri=" + empty);
						response.Redirect(empty);
					}
				}
			}
		}

		// Token: 0x04000019 RID: 25
		internal const string IISLogFieldPrefixForJavascriptRedirects = "OwaJavascriptRedirectUri=";

		// Token: 0x0400001A RID: 26
		internal const string IISLogFieldPrefixFor301Redirects = "Owa301RedirectUri=";

		// Token: 0x0400001B RID: 27
		internal const string IISLogFieldPrefixFor302Redirects = "Owa302RedirectUri=";

		// Token: 0x0400001C RID: 28
		internal const string RedirectPath = "/owa/";

		// Token: 0x0400001D RID: 29
		private const string ClientSideRedirectBody = "<!DOCTYPE html><html><head><script type=\"text/javascript\">window.location.replace(\"{0}\" + window.location.hash);</script></head><body></body></html>";

		// Token: 0x0400001E RID: 30
		private const string GallatinSplashPageEnabledAppSetting = "GallatinSplashPageEnabled";

		// Token: 0x0400001F RID: 31
		private const string GallatinSplashPagePathAppSetting = "GallatinSplashPagePath";

		// Token: 0x04000020 RID: 32
		private const string SplashPage = "/owa/auth/outlookcn.aspx";

		// Token: 0x04000021 RID: 33
		private static bool staticInitialized = false;

		// Token: 0x04000022 RID: 34
		private static bool gallatinSplashPageEnabled;

		// Token: 0x04000023 RID: 35
		private static string gallatinSplashPageRequiredForUrl;
	}
}
