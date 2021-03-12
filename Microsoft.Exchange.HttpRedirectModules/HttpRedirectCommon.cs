using System;
using System.Net;
using System.Web;

namespace Microsoft.Exchange.HttpRedirect
{
	// Token: 0x02000003 RID: 3
	internal class HttpRedirectCommon
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000021FC File Offset: 0x000003FC
		static HttpRedirectCommon()
		{
			if (!string.IsNullOrWhiteSpace(HttpRuntime.AppDomainAppVirtualPath))
			{
				HttpRedirectCommon.VirtualDirectoryNameLeadingSlash = HttpRuntime.AppDomainAppVirtualPath.Replace("'", "''");
				if (!HttpRedirectCommon.VirtualDirectoryNameLeadingSlash.StartsWith("/"))
				{
					HttpRedirectCommon.VirtualDirectoryNameLeadingSlash = "/" + HttpRedirectCommon.VirtualDirectoryNameLeadingSlash;
				}
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002253 File Offset: 0x00000453
		// (set) Token: 0x0600000A RID: 10 RVA: 0x0000225A File Offset: 0x0000045A
		public static string VirtualDirectoryNameLeadingSlash { get; private set; }

		// Token: 0x0600000B RID: 11 RVA: 0x00002262 File Offset: 0x00000462
		public static bool UriIsHttps(Uri uri)
		{
			return uri.Scheme == "https" && uri.Port == 443;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002288 File Offset: 0x00000488
		public static void RedirectRequestToNewUri(HttpApplication httpApplication, HttpRedirectCommon.HttpRedirectType httpRedirectType, Uri redirectUri, string logFieldPrefix)
		{
			HttpContext context = httpApplication.Context;
			HttpResponse response = context.Response;
			string text = redirectUri.ToString();
			HttpStatusCode statusCode = (httpRedirectType == HttpRedirectCommon.HttpRedirectType.Permanent) ? HttpStatusCode.MovedPermanently : HttpStatusCode.Found;
			string statusDescription = (httpRedirectType == HttpRedirectCommon.HttpRedirectType.Permanent) ? "Moved Permanently" : "Moved Temporarily";
			response.Clear();
			response.StatusCode = (int)statusCode;
			response.StatusDescription = statusDescription;
			response.AddHeader("Location", text);
			response.AddHeader("Connection", "close");
			response.AddHeader("Cache-Control", "no-cache");
			response.AddHeader("Pragma", "no-cache");
			response.AppendToLog(logFieldPrefix + text);
			httpApplication.CompleteRequest();
		}

		// Token: 0x04000004 RID: 4
		public const string MovedPermanentlyHttpStatusText = "Moved Permanently";

		// Token: 0x04000005 RID: 5
		public const string MovedTemporarilyHttpStatusText = "Moved Temporarily";

		// Token: 0x04000006 RID: 6
		public const string HttpsScheme = "https";

		// Token: 0x04000007 RID: 7
		public const string LocationHttpHeaderName = "Location";

		// Token: 0x04000008 RID: 8
		public const string ConnectionHttpHeaderName = "Connection";

		// Token: 0x04000009 RID: 9
		public const string CacheControlHttpHeaderName = "Cache-Control";

		// Token: 0x0400000A RID: 10
		public const string PragmaHttpHeaderName = "Pragma";

		// Token: 0x0400000B RID: 11
		public const string NoCacheHttpHeaderValue = "no-cache";

		// Token: 0x0400000C RID: 12
		public const string CloseHttpHeaderValue = "close";

		// Token: 0x0400000D RID: 13
		public const int DefaultWebSiteSslPort = 443;

		// Token: 0x0400000E RID: 14
		public const string ForwardSlash = "/";

		// Token: 0x02000004 RID: 4
		public enum HttpRedirectType
		{
			// Token: 0x04000011 RID: 17
			Permanent,
			// Token: 0x04000012 RID: 18
			Temporary
		}
	}
}
