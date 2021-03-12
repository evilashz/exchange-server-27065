using System;
using System.Net;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001D3 RID: 467
	public static class HttpUtilities
	{
		// Token: 0x0600108A RID: 4234 RVA: 0x0003F65E File Offset: 0x0003D85E
		public static string GetQueryStringParameter(HttpRequest httpRequest, string name)
		{
			return HttpUtilities.GetQueryStringParameter(httpRequest, name, true);
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x0003F668 File Offset: 0x0003D868
		public static string GetQueryStringParameter(HttpRequest httpRequest, string name, bool required)
		{
			if (httpRequest == null)
			{
				throw new ArgumentNullException("httpRequest");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			string text = httpRequest.QueryString[name];
			if (text == null && required)
			{
				throw new OwaInvalidRequestException(string.Format("Required URL parameter missing: {0}", name));
			}
			return text;
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x0003F6BC File Offset: 0x0003D8BC
		public static string GetFormParameter(HttpRequest httpRequest, string name, bool required)
		{
			if (httpRequest == null)
			{
				throw new ArgumentNullException("httpRequest");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name", "name cannot be null or empty");
			}
			string text = httpRequest.Form[name];
			if (text == null && required)
			{
				throw new OwaInvalidRequestException(string.Format("Required form parameter missing: {0}", name));
			}
			return text;
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x0003F714 File Offset: 0x0003D914
		public static void EndResponse(HttpContext httpContext, HttpStatusCode statusCode)
		{
			HttpUtilities.EndResponse(httpContext, statusCode, HttpCacheability.NoCache);
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x0003F720 File Offset: 0x0003D920
		public static void EndResponse(HttpContext httpContext, HttpStatusCode statusCode, HttpCacheability cacheability)
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug<int>(0L, "HttpUtilities.EndResponse: statusCode={0}", (int)statusCode);
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			if (cacheability == HttpCacheability.NoCache)
			{
				HttpUtilities.MakePageNoCacheNoStore(httpContext.Response);
			}
			httpContext.Response.StatusCode = (int)statusCode;
			try
			{
				httpContext.Response.Flush();
				httpContext.ApplicationInstance.CompleteRequest();
			}
			catch (HttpException arg)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<HttpException>(0L, "Failed to flush and send response to client. {0}", arg);
			}
			httpContext.Response.End();
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x0003F7B0 File Offset: 0x0003D9B0
		internal static string GetContentTypeString(OwaEventContentType contentType)
		{
			switch (contentType)
			{
			case OwaEventContentType.Html:
				return "text/html";
			case OwaEventContentType.Javascript:
				return "application/x-javascript";
			case OwaEventContentType.PlainText:
				return "text/plain";
			case OwaEventContentType.Css:
				return "text/css";
			case OwaEventContentType.Jpeg:
				return "image/jpeg";
			default:
				throw new ArgumentOutOfRangeException("contentType");
			}
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x0003F805 File Offset: 0x0003DA05
		internal static void MakePageNoCacheNoStore(HttpResponse response)
		{
			response.Cache.SetCacheability(HttpCacheability.NoCache);
			response.Cache.SetNoStore();
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x0003F820 File Offset: 0x0003DA20
		internal static void DeleteCookie(HttpResponse response, string name)
		{
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("name can not be null or empty string");
			}
			bool flag = false;
			for (int i = 0; i < response.Cookies.Count; i++)
			{
				HttpCookie httpCookie = response.Cookies[i];
				if (httpCookie.Name != null && string.Equals(httpCookie.Name, name, StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				response.Cookies.Add(new HttpCookie(name, string.Empty));
			}
			response.Cookies[name].Expires = (DateTime)ExDateTime.UtcNow.AddYears(-30);
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x0003F8CC File Offset: 0x0003DACC
		internal static bool IsPostRequest(HttpRequest request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			return string.Equals(request.HttpMethod, "post", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x0003F8F0 File Offset: 0x0003DAF0
		internal static string GetRequestCorrelationId(HttpContext httpContext)
		{
			ExAssert.RetailAssert(httpContext != null, "httpContext is null");
			string text = httpContext.Request.Headers["X-OWA-CorrelationId"];
			if (string.IsNullOrEmpty(text))
			{
				text = "<empty>";
			}
			return text;
		}

		// Token: 0x040009CB RID: 2507
		internal const string ISO8601DateTimeMsPattern = "yyyy-MM-ddTHH:mm:ss.fff";

		// Token: 0x040009CC RID: 2508
		internal static readonly string[] TransferrableHeaders = new string[]
		{
			"X-OWA-CorrelationId",
			"X-OWA-ClientBegin",
			"X-FrontEnd-Begin",
			"X-FrontEnd-End",
			"X-BackEnd-Begin",
			"X-BackEnd-End",
			"X-FrontEnd-Handler-Begin",
			"X-EXT-ClientName",
			"X-EXT-CorrelationId",
			"X-EXT-ProxyBegin",
			"X-EXT-ACSBegin",
			"X-EXT-ACSEnd"
		};
	}
}
