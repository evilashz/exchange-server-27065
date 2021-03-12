using System;
using System.Diagnostics;
using System.Net;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.EventLogs;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000222 RID: 546
	internal static class ProxyUtilities
	{
		// Token: 0x0600125F RID: 4703 RVA: 0x0006FE64 File Offset: 0x0006E064
		internal static bool ShouldCopyProxyRequestHeader(string headerName)
		{
			return !WebHeaderCollection.IsRestricted(headerName) && StringComparer.OrdinalIgnoreCase.Compare(headerName, "accept-encoding") != 0 && StringComparer.OrdinalIgnoreCase.Compare(headerName, "cookie") != 0 && StringComparer.OrdinalIgnoreCase.Compare(headerName, "authorization") != 0 && 0 != StringComparer.OrdinalIgnoreCase.Compare(headerName, "proxy-authorization");
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0006FEC8 File Offset: 0x0006E0C8
		internal static bool ShouldCopyProxyResponseHeader(string headerName)
		{
			return !WebHeaderCollection.IsRestricted(headerName) && StringComparer.OrdinalIgnoreCase.Compare(headerName, "set-cookie") != 0 && StringComparer.OrdinalIgnoreCase.Compare(headerName, "server") != 0 && StringComparer.OrdinalIgnoreCase.Compare(headerName, "x-powered-by") != 0 && StringComparer.OrdinalIgnoreCase.Compare(headerName, "x-aspnet-version") != 0 && 0 != StringComparer.OrdinalIgnoreCase.Compare(headerName, "www-authenticate");
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x0006FF40 File Offset: 0x0006E140
		internal static HttpWebRequest CreateHttpWebRequestForProxying(OwaContext owaContext, Uri requestUri)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUri);
			httpWebRequest.UnsafeAuthenticatedConnectionSharing = true;
			httpWebRequest.PreAuthenticate = true;
			httpWebRequest.AllowAutoRedirect = false;
			httpWebRequest.Credentials = CredentialCache.DefaultNetworkCredentials.GetCredential(requestUri, "Kerberos");
			if (httpWebRequest.Credentials == null)
			{
				throw new OwaInvalidOperationException("Can't get credentials for the proxy request");
			}
			httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip;
			CertificateValidationManager.SetComponentId(httpWebRequest, requestUri.IsLoopback ? "OWA_IgnoreCertErrors" : "OWA");
			GccUtils.CopyClientIPEndpointsForServerToServerProxy(owaContext.HttpContext, httpWebRequest);
			return httpWebRequest;
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0006FFC8 File Offset: 0x0006E1C8
		internal static HttpWebRequest GetProxyRequestInstance(HttpRequest originalRequest, OwaContext owaContext, Uri requestUri)
		{
			string name = "X-LogonType";
			string value = "Public";
			HttpWebRequest httpWebRequest = ProxyUtilities.CreateHttpWebRequestForProxying(owaContext, requestUri);
			for (int i = 0; i < originalRequest.Headers.Count; i++)
			{
				string text = originalRequest.Headers.Keys[i];
				if (ProxyUtilities.ShouldCopyProxyRequestHeader(text))
				{
					httpWebRequest.Headers[text] = originalRequest.Headers[text];
				}
			}
			httpWebRequest.UserAgent = originalRequest.UserAgent;
			string text2 = originalRequest.Headers["referer"];
			if (text2 != null)
			{
				httpWebRequest.Referer = text2;
			}
			text2 = originalRequest.Headers["accept"];
			if (text2 != null)
			{
				httpWebRequest.Accept = text2;
			}
			text2 = originalRequest.Headers["transfer-encoding"];
			if (text2 != null)
			{
				httpWebRequest.TransferEncoding = text2;
			}
			httpWebRequest.ContentLength = (long)originalRequest.ContentLength;
			httpWebRequest.ContentType = originalRequest.ContentType;
			httpWebRequest.UserAgent = originalRequest.UserAgent;
			if (httpWebRequest.Headers[name] == null && owaContext.SessionContext != null && owaContext.SessionContext.IsPublicRequest(originalRequest))
			{
				httpWebRequest.Headers.Add(name, value);
			}
			httpWebRequest.Headers["X-OWA-ProxyUri"] = owaContext.LocalHostName;
			httpWebRequest.Headers["X-OWA-ProxyVersion"] = Globals.ApplicationVersion;
			if (Globals.OwaVDirType == OWAVDirType.OWA)
			{
				httpWebRequest.Headers["X-OWA-ProxySid"] = owaContext.LogonIdentity.UserSid.ToString();
			}
			httpWebRequest.Headers["X-OWA-ProxyCanary"] = Utilities.GetCurrentCanary(owaContext.SessionContext);
			if (owaContext.RequestType == OwaRequestType.WebPart)
			{
				httpWebRequest.Headers["X-OWA-ProxyWebPart"] = "1";
			}
			if (owaContext.TryGetUserContext() != null && owaContext.UserContext.ProxyUserContextCookie != null)
			{
				ExTraceGlobals.ProxyDataTracer.TraceDebug<UserContextCookie>(0L, "Setting proxy user context cookie: {0}", owaContext.UserContext.ProxyUserContextCookie);
				Cookie netCookie = owaContext.UserContext.ProxyUserContextCookie.NetCookie;
				ProxyUtilities.AddCookieToProxyRequest(httpWebRequest, netCookie, requestUri.Host);
			}
			else
			{
				ExTraceGlobals.ProxyDataTracer.TraceDebug(0L, "No user context cookie used for the proxy request");
			}
			foreach (string name2 in ProxyUtilities.ProxyAllowedCookies)
			{
				if (owaContext.HttpContext.Request.Cookies[name2] != null)
				{
					Cookie cookie = new Cookie(name2, owaContext.HttpContext.Request.Cookies[name2].Value);
					ProxyUtilities.AddCookieToProxyRequest(httpWebRequest, cookie, requestUri.Host);
				}
			}
			return httpWebRequest;
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x0007024F File Offset: 0x0006E44F
		private static void AddCookieToProxyRequest(HttpWebRequest request, Cookie cookie, string domain)
		{
			if (request.CookieContainer == null)
			{
				request.CookieContainer = new CookieContainer();
			}
			cookie.Domain = domain;
			request.CookieContainer.Add(cookie);
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00070278 File Offset: 0x0006E478
		internal static void UpdateProxyClientDataCollectingCookieFromResponse(HttpWebResponse proxyResponse, HttpResponse originalResponse)
		{
			if (proxyResponse.Cookies["owacsdc"] != null)
			{
				HttpCookie cookie = new HttpCookie("owacsdc", proxyResponse.Cookies["owacsdc"].Value);
				originalResponse.Cookies.Add(cookie);
			}
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x000702C4 File Offset: 0x0006E4C4
		internal static void UpdateProxyUserContextIdFromResponse(HttpWebResponse response, UserContext userContext)
		{
			ExTraceGlobals.UserContextCallTracer.TraceDebug(0L, "UpdateProxyUserContextIdFromResponse");
			string text = response.Headers["Set-Cookie"];
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			int num = text.IndexOf("UserContext", StringComparison.Ordinal);
			if (num < 0)
			{
				return;
			}
			int num2 = text.IndexOf(';', num);
			if (num2 < 0)
			{
				num2 = text.Length - 1;
			}
			string text2 = text.Substring(num, num2 - num);
			if (!text2.StartsWith("UserContext", StringComparison.OrdinalIgnoreCase))
			{
				throw new OwaInvalidOperationException("Invalid user context cookie found in proxy response");
			}
			int num3 = text2.IndexOf('=');
			if (num3 < 0)
			{
				throw new OwaInvalidOperationException("Invalid user context cookie found in proxy response");
			}
			string cookieName = text2.Substring(0, num3);
			string cookieValue = text2.Substring(num3 + 1, text2.Length - num3 - 1);
			string cookieId = null;
			if (!UserContextCookie.TryParseCookieName(cookieName, out cookieId))
			{
				throw new OwaInvalidOperationException("Invalid user context cookie found in proxy response");
			}
			string canaryString = null;
			string mailboxUniqueKey = null;
			if (!UserContextCookie.TryParseCookieValue(cookieValue, out canaryString, out mailboxUniqueKey))
			{
				throw new OwaInvalidOperationException("Invalid user context cookie found in proxy response");
			}
			Canary canary = Canary.RestoreCanary(canaryString, userContext.LogonIdentity.UniqueId);
			userContext.ProxyUserContextCookie = UserContextCookie.Create(cookieId, canary, mailboxUniqueKey);
			ExTraceGlobals.UserContextTracer.TraceDebug<UserContextCookie>(0L, "Found set-cookie returned by second CAS: {0}", userContext.ProxyUserContextCookie);
			if (userContext.ProxyUserContextCookie == null)
			{
				throw new OwaInvalidOperationException("Invalid user context cookie found in proxy response");
			}
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x00070410 File Offset: 0x0006E610
		internal static IAsyncResult BeginGetResponse(HttpWebRequest request, AsyncCallback asyncCallback, object context, out Stopwatch requestClock)
		{
			if (Globals.ArePerfCountersEnabled)
			{
				long num = (long)request.Headers.ToByteArray().Length;
				if (request.ContentLength > 0L)
				{
					num += request.ContentLength;
				}
				OwaSingleCounters.ProxyRequestBytes.IncrementBy(num);
			}
			requestClock = Stopwatch.StartNew();
			ProxyUtilities.TraceProxyRequest(request);
			return request.BeginGetResponse(asyncCallback, context);
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00070468 File Offset: 0x0006E668
		internal static HttpWebResponse EndGetResponse(HttpWebRequest request, IAsyncResult asyncResult, Stopwatch requestClock)
		{
			requestClock.Stop();
			long elapsedMilliseconds = requestClock.ElapsedMilliseconds;
			HttpWebResponse result;
			try
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)request.EndGetResponse(asyncResult);
				ProxyUtilities.TraceProxyResponse(request, (int)httpWebResponse.StatusCode, elapsedMilliseconds);
				result = httpWebResponse;
			}
			catch (WebException exception)
			{
				ProxyUtilities.TraceFailedProxyRequest(request, exception, elapsedMilliseconds);
				throw;
			}
			return result;
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x000704C0 File Offset: 0x0006E6C0
		private static void TraceProxyRequest(HttpWebRequest request)
		{
			ExTraceGlobals.ProxyRequestTracer.TraceDebug<string, Uri, long>((long)request.GetHashCode(), "Request: {0} {1}, content-length:{2}", request.Method, request.RequestUri, request.ContentLength);
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x000704EA File Offset: 0x0006E6EA
		private static void TraceProxyResponse(HttpWebRequest request, int httpStatusCode, long elapsedMilliseconds)
		{
			ExTraceGlobals.ProxyRequestTracer.TraceDebug<int, long>((long)request.GetHashCode(), "Response: HTTP {0}, time:{1} ms.", httpStatusCode, elapsedMilliseconds);
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00070504 File Offset: 0x0006E704
		private static void TraceFailedProxyRequest(HttpWebRequest request, WebException exception, long elapsedMilliseconds)
		{
			if (!ExTraceGlobals.ProxyRequestTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				return;
			}
			if (exception.Status == WebExceptionStatus.ProtocolError)
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)exception.Response;
				int statusCode = (int)httpWebResponse.StatusCode;
				ExTraceGlobals.ProxyRequestTracer.TraceDebug<int, long>((long)request.GetHashCode(), "Response: HTTP {0}, time:{1} ms.", statusCode, elapsedMilliseconds);
				return;
			}
			string arg = exception.Status.ToString();
			string arg2 = (exception.Message != null) ? exception.Message : "N/A";
			ExTraceGlobals.ProxyRequestTracer.TraceDebug<string, string, long>((long)request.GetHashCode(), "Response (failed): {0}, {1}, time:{2} ms.", arg, arg2, elapsedMilliseconds);
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00070594 File Offset: 0x0006E794
		internal static void ThrowMalformedCasUriException(OwaContext owaContext, string malformedUri)
		{
			string text = owaContext.ExchangePrincipal.LegacyDn;
			if (text == null)
			{
				text = string.Empty;
			}
			OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_ProxyErrorWrongUriFormat, malformedUri, new object[]
			{
				owaContext.LocalHostName,
				text,
				malformedUri
			});
			throw new OwaProxyException(string.Format("The format of the uri is wrong: {0}", malformedUri), string.Format(Strings.ErrorWrongUriFormat, malformedUri));
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x000705F8 File Offset: 0x0006E7F8
		internal static Uri TryCreateCasUri(string uriString, bool needVdirValidation)
		{
			if (string.IsNullOrEmpty(uriString) || !Uri.IsWellFormedUriString(uriString, UriKind.Absolute))
			{
				return null;
			}
			Uri uri;
			if (null == (uri = Utilities.TryParseUri(uriString)))
			{
				return null;
			}
			if (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
			{
				return null;
			}
			if (needVdirValidation)
			{
				if (uri.Segments.Length != 2 && uri.Segments.Length != 3)
				{
					return null;
				}
				if (StringComparer.OrdinalIgnoreCase.Compare(uri.Segments[0], "/") != 0 || (StringComparer.OrdinalIgnoreCase.Compare(uri.Segments[1], Globals.VirtualRootName) != 0 && StringComparer.OrdinalIgnoreCase.Compare(uri.Segments[1], Globals.VirtualRootName + "/") != 0))
				{
					return null;
				}
				if (uri.Segments.Length == 3)
				{
					string value = UrlUtilities.ValidateFederatedDomainInURL(uri);
					if (string.IsNullOrEmpty(value))
					{
						return null;
					}
				}
			}
			if (!string.IsNullOrEmpty(uri.Query))
			{
				return null;
			}
			return uri;
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x000706F4 File Offset: 0x0006E8F4
		internal static void EnsureProxyUrlSslPolicy(OwaContext owaContext, ProxyUri secondCasUri)
		{
			if (!OwaRegistryKeys.AllowProxyingWithoutSsl && secondCasUri.Uri.Scheme != Uri.UriSchemeHttps)
			{
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_ProxyErrorSslConnection, secondCasUri.ToString(), new object[]
				{
					owaContext.LocalHostName,
					secondCasUri.ToString(),
					secondCasUri.ToString()
				});
				throw new OwaProxyException(string.Format("The URI found for proxying does not start with \"https\". Value={0}", secondCasUri.ToString()), LocalizedStrings.GetNonEncoded(-750997814));
			}
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00070773 File Offset: 0x0006E973
		internal static bool IsVersionFolderInProxy(ServerVersion version)
		{
			return Globals.LocalVersionFolders.ContainsKey(version);
		}

		// Token: 0x04000C9D RID: 3229
		internal const int HttpStatusNeedIdentity = 441;

		// Token: 0x04000C9E RID: 3230
		internal const int HttpStatusRetryRequest = 241;

		// Token: 0x04000C9F RID: 3231
		internal const int HttpStatusProxyPingSucceeded = 242;

		// Token: 0x04000CA0 RID: 3232
		internal const int HttpStatusNeedLanguage = 442;

		// Token: 0x04000CA1 RID: 3233
		internal const string CertificateValidationComponentId = "OWA";

		// Token: 0x04000CA2 RID: 3234
		internal const string CertificateIgnoreComponentId = "OWA_IgnoreCertErrors";

		// Token: 0x04000CA3 RID: 3235
		private static readonly string[] ProxyAllowedCookies = new string[]
		{
			"UpdatedUserSettings",
			"mkt"
		};

		// Token: 0x02000223 RID: 547
		internal enum CauseOfUnkownRequestExecution
		{
			// Token: 0x04000CA5 RID: 3237
			None,
			// Token: 0x04000CA6 RID: 3238
			NoCASFoundForInSiteMailbox,
			// Token: 0x04000CA7 RID: 3239
			NoCASFoundForCrossSiteMailboxToRedirect,
			// Token: 0x04000CA8 RID: 3240
			NoCASFoundForCrossSiteMailboxToProxy
		}

		// Token: 0x02000224 RID: 548
		public enum LegacyRedirectFailureCause
		{
			// Token: 0x04000CAA RID: 3242
			None,
			// Token: 0x04000CAB RID: 3243
			NoCasFound
		}
	}
}
