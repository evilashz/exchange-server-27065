using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000110 RID: 272
	public static class ClientAuthenticationHelper
	{
		// Token: 0x060008D7 RID: 2263 RVA: 0x000396B4 File Offset: 0x000378B4
		static ClientAuthenticationHelper()
		{
			ClientAuthenticationHelper.RemoteNotifiationDfpowaRequestPaths = new HashSet<string>(new string[]
			{
				"/dfpowa/remotenotification.ashx",
				"/dfpowa1/remotenotification.ashx",
				"/dfpowa2/remotenotification.ashx",
				"/dfpowa3/remotenotification.ashx",
				"/dfpowa4/remotenotification.ashx",
				"/dfpowa5/remotenotification.ashx"
			}, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00039834 File Offset: 0x00037A34
		public static bool IsOwaRequest(HttpContext httpContext)
		{
			httpContext.Request.GetRequestUrlEvenIfProxied();
			return HttpRuntime.AppDomainAppVirtualPath.Equals("/owa", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00039854 File Offset: 0x00037A54
		public static bool IsOwaAnonymousRequest(HttpContext httpContext)
		{
			Uri requestUrlEvenIfProxied = httpContext.Request.GetRequestUrlEvenIfProxied();
			return HttpRuntime.AppDomainAppVirtualPath.Equals("/owa", StringComparison.OrdinalIgnoreCase) && requestUrlEvenIfProxied.Segments.Length > 3 && requestUrlEvenIfProxied.Segments[2].Equals("auth/", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x000398A0 File Offset: 0x00037AA0
		public static bool IsAuthRedirectRequest(HttpRequest request)
		{
			bool result = false;
			if (request != null)
			{
				result = !string.IsNullOrEmpty(request.QueryString["authRedirect"]);
			}
			return result;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x000398CC File Offset: 0x00037ACC
		public static string ValidateFederatedDomainInURL(Uri uri)
		{
			if (uri.Segments.Length >= 3)
			{
				if (!uri.Segments[1].Equals("owa/", StringComparison.OrdinalIgnoreCase))
				{
					return null;
				}
				string text = null;
				if (uri.Segments[2].StartsWith("@"))
				{
					text = uri.Segments[2].Substring(1).TrimEnd(new char[]
					{
						'/'
					});
				}
				else
				{
					string text2 = uri.Segments[2].TrimEnd(new char[]
					{
						'/'
					});
					if (ClientAuthenticationHelper.VersionFolderRegex.IsMatch(text2))
					{
						return null;
					}
					for (int i = 0; i < ClientAuthenticationHelper.blackListForFederatedDomainInURL.Length; i++)
					{
						if (text2.EndsWith(ClientAuthenticationHelper.blackListForFederatedDomainInURL[i], StringComparison.OrdinalIgnoreCase))
						{
							return null;
						}
					}
					if (text2.IndexOf('.') != -1)
					{
						text = text2;
					}
				}
				if (!string.IsNullOrEmpty(text) && SmtpAddress.IsValidDomain(text))
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x000399A8 File Offset: 0x00037BA8
		public static bool IsSpecialDomainUrl(Uri uri, string domain)
		{
			if (uri.Segments.Length >= 3)
			{
				if (!uri.Segments[1].Equals("owa/", StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
				string text;
				if (uri.Segments[2].StartsWith("@"))
				{
					text = uri.Segments[2].Substring(1).TrimEnd(new char[]
					{
						'/'
					});
				}
				else
				{
					text = uri.Segments[2].TrimEnd(new char[]
					{
						'/'
					});
				}
				if (!string.IsNullOrEmpty(text) && text.Equals(domain, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00039A40 File Offset: 0x00037C40
		public static void RewriteFederatedDomainInURL(HttpContext httpContext, out string domain)
		{
			domain = string.Empty;
			Uri url = httpContext.Request.Url;
			domain = ClientAuthenticationHelper.ValidateFederatedDomainInURL(url);
			ClientAuthenticationHelper.RewriteRealmParameterInURL(httpContext, domain);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00039A70 File Offset: 0x00037C70
		public static void RewriteFederatedDomainInURL(HttpContext httpContext)
		{
			string empty = string.Empty;
			ClientAuthenticationHelper.RewriteFederatedDomainInURL(httpContext, out empty);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00039A8C File Offset: 0x00037C8C
		public static void RewriteDomainFromTenantSpecificURL(HttpContext httpContext, string tenantUrl)
		{
			if (!string.IsNullOrEmpty(tenantUrl))
			{
				string text = tenantUrl.Substring(tenantUrl.IndexOf(".") + 1).TrimEnd(new char[]
				{
					'/'
				});
				if (text.IndexOf(".") <= 0)
				{
					text = tenantUrl;
				}
				ClientAuthenticationHelper.RewriteRealmParameterInURL(httpContext, text);
			}
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00039AE0 File Offset: 0x00037CE0
		public static void RewriteRealmParameterInURL(HttpContext httpContext, string domain)
		{
			Uri url = httpContext.Request.Url;
			ClientAuthenticationHelper.SaveOriginalRequestUrlToContext(httpContext, url);
			if (!string.IsNullOrEmpty(domain) && SmtpAddress.IsValidDomain(domain))
			{
				StringBuilder stringBuilder = new StringBuilder();
				int i = 0;
				while (i < url.Segments.Length)
				{
					string text = url.Segments[i];
					if (i != 2 || text.Equals("closewindow.aspx", StringComparison.OrdinalIgnoreCase))
					{
						goto IL_69;
					}
					int num = text.IndexOf('@');
					if (num > 0 && num < text.Length - 2)
					{
						goto IL_69;
					}
					IL_71:
					i++;
					continue;
					IL_69:
					stringBuilder.Append(text);
					goto IL_71;
				}
				if (string.IsNullOrEmpty(url.Query))
				{
					stringBuilder.Append("?");
				}
				else
				{
					stringBuilder.Append(url.Query);
					stringBuilder.Append("&");
				}
				stringBuilder.Append("realm");
				stringBuilder.Append("=");
				stringBuilder.Append(HttpUtility.UrlEncode(domain));
				httpContext.RewritePath(stringBuilder.ToString());
			}
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00039BD4 File Offset: 0x00037DD4
		public static void RewriteParameterInURL(HttpContext httpContext, string name, string value)
		{
			string parameter = string.Format("{0}={1}", name, value);
			ClientAuthenticationHelper.RewriteParameterInURL(httpContext, parameter);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00039BF8 File Offset: 0x00037DF8
		public static void RewriteParameterInURL(HttpContext httpContext, string parameter)
		{
			Uri url = httpContext.Request.Url;
			ClientAuthenticationHelper.SaveOriginalRequestUrlToContext(httpContext, url);
			StringBuilder stringBuilder = new StringBuilder(url.PathAndQuery);
			if (string.IsNullOrEmpty(url.Query))
			{
				stringBuilder.Append("?");
			}
			else
			{
				stringBuilder.Append("&");
			}
			stringBuilder.Append(parameter);
			httpContext.RewritePath(stringBuilder.ToString());
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00039C60 File Offset: 0x00037E60
		public static bool IsWacRequest(HttpRequest request)
		{
			string a = HttpUtility.UrlDecode(request.Url.AbsolutePath);
			if (ClientAuthenticationHelper.isPreCheckinApp)
			{
				return (string.Equals(request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase) || string.Equals(request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase)) && (string.Equals(a, "/dfpowa/wopi/files/@/owaatt", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "/dfpowa/wopi/files/@/owaatt/contents", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "/dfpowa1/wopi/files/@/owaatt", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "/dfpowa1/wopi/files/@/owaatt/contents", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "/dfpowa2/wopi/files/@/owaatt", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "/dfpowa2/wopi/files/@/owaatt/contents", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "/dfpowa3/wopi/files/@/owaatt", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "/dfpowa3/wopi/files/@/owaatt/contents", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "/dfpowa4/wopi/files/@/owaatt", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "/dfpowa4/wopi/files/@/owaatt/contents", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "/dfpowa5/wopi/files/@/owaatt", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "/dfpowa5/wopi/files/@/owaatt/contents", StringComparison.OrdinalIgnoreCase));
			}
			return (string.Equals(request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase) || string.Equals(request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase)) && (string.Equals(a, "/owa/wopi/files/@/owaatt", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "/owa/wopi/files/@/owaatt/contents", StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00039DA8 File Offset: 0x00037FA8
		public static bool IsRemoteNotificationRequest(HttpRequest request)
		{
			if (!string.Equals(request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			string text = HttpUtility.UrlDecode(request.Url.AbsolutePath);
			if (ClientAuthenticationHelper.isPreCheckinApp)
			{
				return ClientAuthenticationHelper.RemoteNotifiationDfpowaRequestPaths.Contains(text);
			}
			return string.Equals(text, "/owa/remotenotification.ashx", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00039DFA File Offset: 0x00037FFA
		public static void SaveOriginalRequestUrlToContext(HttpContext httpContext, Uri originalURL)
		{
			if (string.IsNullOrEmpty(httpContext.Request.Headers.Get("X-OWA-OriginalRequestUrl")))
			{
				httpContext.Request.Headers.Add("X-OWA-OriginalRequestUrl", HttpUtility.UrlEncode(originalURL.AbsoluteUri));
			}
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00039E38 File Offset: 0x00038038
		public static Uri GetRequestUrlEvenIfProxied(this HttpRequest request)
		{
			Uri uri = request.Url;
			if (!AuthCommon.IsFrontEnd)
			{
				UriBuilder uriBuilder = new UriBuilder(uri);
				string text = request.Headers.Get("X-OriginalRequestHostSchemePort");
				if (!string.IsNullOrWhiteSpace(text))
				{
					int port = 443;
					string[] array = text.Split(":".ToCharArray(), 3);
					if (array.Length != 3 || !int.TryParse(array[0], out port))
					{
						throw new ArgumentException(string.Format("{0} header has invalid format ({1})", "X-OriginalRequestHostSchemePort", text));
					}
					uriBuilder.Port = port;
					uriBuilder.Scheme = array[1];
					uriBuilder.Host = array[2];
				}
				else
				{
					string text2 = request.Headers.Get("X-OriginalRequestHost");
					if (!string.IsNullOrWhiteSpace(text2))
					{
						uriBuilder.Host = text2;
						uriBuilder.Port = 443;
					}
				}
				string text3 = request.Headers.Get("X-OWA-ExplicitLogonUser");
				if (!string.IsNullOrWhiteSpace(text3) && uriBuilder.Path.StartsWith("/owa/", StringComparison.OrdinalIgnoreCase))
				{
					uriBuilder.Path = ClientAuthenticationHelper.GetPathWithExplictLogonHint(request.Url, text3);
				}
				uri = uriBuilder.Uri;
			}
			return uri;
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00039F50 File Offset: 0x00038150
		public static string GetPathWithExplictLogonHint(Uri requestUrl, string explicitLogonUser)
		{
			string[] segments = requestUrl.Segments;
			int num;
			string text;
			if (string.IsNullOrWhiteSpace(ClientAuthenticationHelper.ValidateFederatedDomainInURL(requestUrl)))
			{
				num = 2;
				if (segments[1].EndsWith("/"))
				{
					text = string.Format(CultureInfo.InvariantCulture, "/{0}{1}/", new object[]
					{
						segments[1],
						explicitLogonUser
					});
				}
				else
				{
					text = string.Format(CultureInfo.InvariantCulture, "/{0}/{1}/", new object[]
					{
						segments[1],
						explicitLogonUser
					});
				}
			}
			else
			{
				num = 3;
				if (segments[2].EndsWith("/"))
				{
					text = string.Format(CultureInfo.InvariantCulture, "/{0}{1}{2}/", new object[]
					{
						segments[1],
						segments[2],
						explicitLogonUser
					});
				}
				else
				{
					text = string.Format(CultureInfo.InvariantCulture, "/{0}{1}/{2}/", new object[]
					{
						segments[1],
						segments[2],
						explicitLogonUser
					});
				}
			}
			if (segments.Length > num)
			{
				StringBuilder stringBuilder = new StringBuilder(requestUrl.AbsolutePath.Length + explicitLogonUser.Length + 1);
				stringBuilder.Append(text);
				for (int i = num; i < segments.Length; i++)
				{
					stringBuilder.Append(segments[i]);
				}
				text = stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0003A0A8 File Offset: 0x000382A8
		public static bool ShouldRedirectQueryParamsAsHashes(Uri originalUrl, out string uriQueryOptimized)
		{
			bool result = false;
			uriQueryOptimized = null;
			if (originalUrl.AbsolutePath != null && originalUrl.AbsolutePath.StartsWith("/owa/", StringComparison.OrdinalIgnoreCase) && !originalUrl.Query.Contains("exsvurl=1", StringComparison.OrdinalIgnoreCase))
			{
				NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(originalUrl.Query);
				NameValueCollection nameValueCollection2 = HttpUtility.ParseQueryString(string.Empty);
				StringBuilder stringBuilder = new StringBuilder();
				using (IEnumerator enumerator = nameValueCollection.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string queryParam = (string)enumerator.Current;
						if ((from s in ClientAuthenticationHelper.ImportantQueryParamNames
						where s.Equals(queryParam, StringComparison.OrdinalIgnoreCase)
						select s).Any<string>())
						{
							nameValueCollection2.Add(queryParam, nameValueCollection[queryParam]);
						}
						else if (stringBuilder.Length <= 0)
						{
							stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "#{0}={1}", new object[]
							{
								queryParam,
								nameValueCollection[queryParam]
							});
						}
						else
						{
							stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "&{0}={1}", new object[]
							{
								queryParam,
								nameValueCollection[queryParam]
							});
						}
					}
				}
				UriBuilder uriBuilder = new UriBuilder(originalUrl);
				uriBuilder.Query = nameValueCollection2.ToString();
				if (originalUrl != uriBuilder.Uri)
				{
					result = true;
					uriQueryOptimized = string.Format(CultureInfo.InvariantCulture, "{0}{1}", new object[]
					{
						uriBuilder.Uri.AbsoluteUri,
						stringBuilder.ToString()
					});
				}
			}
			return result;
		}

		// Token: 0x040007F6 RID: 2038
		public const string RealmParameter = "realm";

		// Token: 0x040007F7 RID: 2039
		public const string WacFileIdParameter = "owaatt";

		// Token: 0x040007F8 RID: 2040
		public const string WacCheckFileRequestPath = "/owa/wopi/files/@/owaatt";

		// Token: 0x040007F9 RID: 2041
		public const string WacGetFileRequestPath = "/owa/wopi/files/@/owaatt/contents";

		// Token: 0x040007FA RID: 2042
		public const string WacDfpowaCheckFileRequestPath = "/dfpowa/wopi/files/@/owaatt";

		// Token: 0x040007FB RID: 2043
		public const string WacDfpowaGetFileRequestPath = "/dfpowa/wopi/files/@/owaatt/contents";

		// Token: 0x040007FC RID: 2044
		public const string WacDfpowa1CheckFileRequestPath = "/dfpowa1/wopi/files/@/owaatt";

		// Token: 0x040007FD RID: 2045
		public const string WacDfpowa1GetFileRequestPath = "/dfpowa1/wopi/files/@/owaatt/contents";

		// Token: 0x040007FE RID: 2046
		public const string WacDfpowa2CheckFileRequestPath = "/dfpowa2/wopi/files/@/owaatt";

		// Token: 0x040007FF RID: 2047
		public const string WacDfpowa2GetFileRequestPath = "/dfpowa2/wopi/files/@/owaatt/contents";

		// Token: 0x04000800 RID: 2048
		public const string WacDfpowa3CheckFileRequestPath = "/dfpowa3/wopi/files/@/owaatt";

		// Token: 0x04000801 RID: 2049
		public const string WacDfpowa3GetFileRequestPath = "/dfpowa3/wopi/files/@/owaatt/contents";

		// Token: 0x04000802 RID: 2050
		public const string WacDfpowa4CheckFileRequestPath = "/dfpowa4/wopi/files/@/owaatt";

		// Token: 0x04000803 RID: 2051
		public const string WacDfpowa4GetFileRequestPath = "/dfpowa4/wopi/files/@/owaatt/contents";

		// Token: 0x04000804 RID: 2052
		public const string WacDfpowa5CheckFileRequestPath = "/dfpowa5/wopi/files/@/owaatt";

		// Token: 0x04000805 RID: 2053
		public const string WacDfpowa5GetFileRequestPath = "/dfpowa5/wopi/files/@/owaatt/contents";

		// Token: 0x04000806 RID: 2054
		public const string SaveUrlOnLogoffParameter = "exsvurl=1";

		// Token: 0x04000807 RID: 2055
		public const string OwaVdir = "owa";

		// Token: 0x04000808 RID: 2056
		public const string OwaVdirWithSlash = "/owa/";

		// Token: 0x04000809 RID: 2057
		public const string EcpCloseWindowPage = "closewindow.aspx";

		// Token: 0x0400080A RID: 2058
		public const string AuthParamName = "authRedirect";

		// Token: 0x0400080B RID: 2059
		public const string VersionParam = "ver";

		// Token: 0x0400080C RID: 2060
		public const string ClientExistingVersionParam = "cver";

		// Token: 0x0400080D RID: 2061
		public const string BootOnlineParam = "bO";

		// Token: 0x0400080E RID: 2062
		public const string AppcacheCorruptParam = "aC";

		// Token: 0x0400080F RID: 2063
		private const string EduNamespaceKey = "EduNamespace";

		// Token: 0x04000810 RID: 2064
		private const string RemoteNotifiationRequestPath = "/owa/remotenotification.ashx";

		// Token: 0x04000811 RID: 2065
		private const string ExplicitLogonUser = "X-OWA-ExplicitLogonUser";

		// Token: 0x04000812 RID: 2066
		private const string OrignalRequestUrlKey = "X-OWA-OriginalRequestUrl";

		// Token: 0x04000813 RID: 2067
		private const string OriginalRequestHostKey = "X-OriginalRequestHost";

		// Token: 0x04000814 RID: 2068
		private const string OriginalRequestHostSchemePort = "X-OriginalRequestHostSchemePort";

		// Token: 0x04000815 RID: 2069
		private const string OriginalRequestHostSchemePortDelimiter = ":";

		// Token: 0x04000816 RID: 2070
		private const string OriginalRequestHostSchemePortFormatString = "{0}:{1}:{2}";

		// Token: 0x04000817 RID: 2071
		private static readonly HashSet<string> RemoteNotifiationDfpowaRequestPaths;

		// Token: 0x04000818 RID: 2072
		private static readonly string[] blackListForFederatedDomainInURL = new string[]
		{
			".aspx",
			".owa",
			".gif",
			".ashx",
			"ico",
			"css",
			"jpg",
			"xap",
			"js",
			"wav",
			"htm",
			"html",
			"png",
			"msi",
			"manifest",
			".reco",
			".crx",
			"wopi",
			".ttf",
			".eot",
			".woff",
			".svg",
			".svc",
			".owa2"
		};

		// Token: 0x04000819 RID: 2073
		private static readonly Regex VersionFolderRegex = new Regex("^[0-9]{1,4}\\.[0-9]{1,4}\\.[0-9]{1,4}\\.[0-9]{1,4}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x0400081A RID: 2074
		private static readonly bool isPreCheckinApp = new BoolAppSettingsEntry("IsPreCheckinApp", false, null).Value;

		// Token: 0x0400081B RID: 2075
		private static readonly string[] ImportantQueryParamNames = new string[]
		{
			"layout",
			"url",
			"flights"
		};
	}
}
