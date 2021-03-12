using System;
using System.Web;

namespace Microsoft.Exchange.Clients.Common
{
	// Token: 0x02000028 RID: 40
	public class OfflineClientRequestUtilities
	{
		// Token: 0x06000113 RID: 275 RVA: 0x00008214 File Offset: 0x00006414
		public static bool? IsRequestForOfflineAppcacheManifest(HttpRequest request)
		{
			HttpCookie httpCookie = request.Cookies.Get("offline");
			if (httpCookie == null || httpCookie.Value == null)
			{
				return null;
			}
			string value = httpCookie.Value;
			if (value == "1")
			{
				return new bool?(true);
			}
			if (value == "0")
			{
				return new bool?(false);
			}
			return null;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00008280 File Offset: 0x00006480
		public static bool IsRequestFromOfflineClient(HttpRequest request)
		{
			HttpCookie httpCookie = request.Cookies.Get("X-Offline");
			return (httpCookie != null && httpCookie.Value == "1") || OfflineClientRequestUtilities.IsRequestFromMOWAClient(request, request.UserAgent);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000082C4 File Offset: 0x000064C4
		public static bool IsRequestFromMOWAClient(HttpRequest request, string userAgent)
		{
			if (request != null && request.Cookies != null && request.Cookies["PALEnabled"] != null)
			{
				return request.Cookies["PALEnabled"].Value != "-1";
			}
			return request.QueryString["palenabled"] == "1" || (userAgent != null && userAgent.Contains("MSAppHost")) || request.Headers["X-OWA-Protocol"] == "MOWA";
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00008358 File Offset: 0x00006558
		public static bool IsRequestForAppCachedVersion(HttpContext context)
		{
			HttpCookie httpCookie = context.Request.Cookies["IsClientAppCacheEnabled"];
			return httpCookie != null && httpCookie.Value.Equals(true.ToString(), StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0400026F RID: 623
		public const string PalEnabledCookie = "PALEnabled";

		// Token: 0x04000270 RID: 624
		public const string IsClientAppCacheEnabledCookieName = "IsClientAppCacheEnabled";

		// Token: 0x04000271 RID: 625
		internal const string OfflineManifestCookie = "offline";

		// Token: 0x04000272 RID: 626
		internal const string OfflineCookie = "X-Offline";

		// Token: 0x04000273 RID: 627
		internal const string PALEnabledQueryStringConstant = "palenabled";

		// Token: 0x04000274 RID: 628
		internal const string MSAppHostConstant = "MSAppHost";

		// Token: 0x04000275 RID: 629
		internal const string OwaProtocolHeaderName = "X-OWA-Protocol";

		// Token: 0x04000276 RID: 630
		internal const string MOWA = "MOWA";
	}
}
