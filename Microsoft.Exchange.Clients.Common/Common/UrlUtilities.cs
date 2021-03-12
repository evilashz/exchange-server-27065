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
using Microsoft.Exchange.Data.TextConverters.Internal;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Clients.Common
{
	// Token: 0x0200002C RID: 44
	public static class UrlUtilities
	{
		// Token: 0x06000121 RID: 289 RVA: 0x00008518 File Offset: 0x00006718
		static UrlUtilities()
		{
			UrlUtilities.RemoteNotifiationDfpowaRequestPaths = new HashSet<string>(new string[]
			{
				"/dfpowa/remotenotification.ashx",
				"/dfpowa1/remotenotification.ashx",
				"/dfpowa2/remotenotification.ashx",
				"/dfpowa3/remotenotification.ashx",
				"/dfpowa4/remotenotification.ashx",
				"/dfpowa5/remotenotification.ashx"
			}, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00008698 File Offset: 0x00006898
		internal static bool IsSafeUrl(string urlString)
		{
			if (string.IsNullOrEmpty(urlString))
			{
				return false;
			}
			Uri uri;
			if (null == (uri = UrlUtilities.TryParseUri(urlString)))
			{
				return false;
			}
			string scheme = uri.Scheme;
			return !string.IsNullOrEmpty(scheme) && Uri.CheckSchemeName(scheme) && TextConvertersInternalHelpers.IsUrlSchemaSafe(scheme);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000086E7 File Offset: 0x000068E7
		internal static Uri TryParseUri(string uriString)
		{
			return UrlUtilities.TryParseUri(uriString, UriKind.Absolute);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000086F0 File Offset: 0x000068F0
		internal static Uri TryParseUri(string uriString, UriKind uriKind)
		{
			if (uriString == null)
			{
				throw new ArgumentNullException("uriString");
			}
			Uri result = null;
			if (!Uri.TryCreate(uriString, uriKind, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000871C File Offset: 0x0000691C
		public static bool IsAuthRedirectRequest(HttpRequest request)
		{
			bool result = false;
			if (request != null)
			{
				result = !string.IsNullOrEmpty(request.QueryString["authRedirect"]);
			}
			return result;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00008748 File Offset: 0x00006948
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
					if (UrlUtilities.VersionFolderRegex.IsMatch(text2))
					{
						return null;
					}
					for (int i = 0; i < UrlUtilities.blackListForFederatedDomainInURL.Length; i++)
					{
						if (text2.EndsWith(UrlUtilities.blackListForFederatedDomainInURL[i], StringComparison.OrdinalIgnoreCase))
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

		// Token: 0x06000127 RID: 295 RVA: 0x00008824 File Offset: 0x00006A24
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

		// Token: 0x06000128 RID: 296 RVA: 0x000088BC File Offset: 0x00006ABC
		public static void RewriteFederatedDomainInURL(HttpContext httpContext, out string domain)
		{
			domain = string.Empty;
			Uri url = httpContext.Request.Url;
			domain = UrlUtilities.ValidateFederatedDomainInURL(url);
			UrlUtilities.RewriteRealmParameterInURL(httpContext, domain);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000088EC File Offset: 0x00006AEC
		public static void RewriteFederatedDomainInURL(HttpContext httpContext)
		{
			string empty = string.Empty;
			UrlUtilities.RewriteFederatedDomainInURL(httpContext, out empty);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00008908 File Offset: 0x00006B08
		public static void RewriteDomainInVanityDomainURL(HttpContext httpContext)
		{
			Uri url = httpContext.Request.Url;
			if (url.Segments.Length >= 1)
			{
				string text = url.Segments[0];
				string text2 = text.Substring(text.IndexOf(".") + 1).TrimEnd(new char[]
				{
					'/'
				});
				if (text2.IndexOf(".") <= 0)
				{
					text2 = text;
				}
				UrlUtilities.RewriteRealmParameterInURL(httpContext, text2);
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00008974 File Offset: 0x00006B74
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
				UrlUtilities.RewriteRealmParameterInURL(httpContext, text);
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000089C8 File Offset: 0x00006BC8
		public static void RewriteRealmParameterInURL(HttpContext httpContext, string domain)
		{
			Uri url = httpContext.Request.Url;
			UrlUtilities.SaveOriginalRequestUrlToContext(httpContext, url);
			if (!string.IsNullOrEmpty(domain) && SmtpAddress.IsValidDomain(domain))
			{
				StringBuilder stringBuilder = new StringBuilder();
				int i = 0;
				while (i < url.Segments.Length)
				{
					string text = url.Segments[i];
					if (i != 2 || text.Equals("closewindow.aspx", StringComparison.OrdinalIgnoreCase) || text.Equals("logoff.aspx", StringComparison.OrdinalIgnoreCase))
					{
						goto IL_77;
					}
					int num = text.IndexOf('@');
					if (num > 0 && num < text.Length - 2)
					{
						goto IL_77;
					}
					IL_7F:
					i++;
					continue;
					IL_77:
					stringBuilder.Append(text);
					goto IL_7F;
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

		// Token: 0x0600012D RID: 301 RVA: 0x00008AC8 File Offset: 0x00006CC8
		public static void RewriteParameterInURL(HttpContext httpContext, string name, string value)
		{
			string parameter = string.Format("{0}={1}", name, value);
			UrlUtilities.RewriteParameterInURL(httpContext, parameter);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00008AEC File Offset: 0x00006CEC
		public static void RewriteParameterInURL(HttpContext httpContext, string parameter)
		{
			Uri url = httpContext.Request.Url;
			UrlUtilities.SaveOriginalRequestUrlToContext(httpContext, url);
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

		// Token: 0x0600012F RID: 303 RVA: 0x00008B54 File Offset: 0x00006D54
		public static bool IsWacRequest(HttpRequest request)
		{
			string a = HttpUtility.UrlDecode(request.Url.AbsolutePath);
			if (UrlUtilities.isPreCheckinApp)
			{
				return (string.Equals(request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase) || string.Equals(request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase)) && (string.Equals(a, "/dfpowa/wopi/files/@/owaatt", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "/dfpowa/wopi/files/@/owaatt/contents", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "/dfpowa1/wopi/files/@/owaatt", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "/dfpowa1/wopi/files/@/owaatt/contents", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "/dfpowa2/wopi/files/@/owaatt", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "/dfpowa2/wopi/files/@/owaatt/contents", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "/dfpowa3/wopi/files/@/owaatt", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "/dfpowa3/wopi/files/@/owaatt/contents", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "/dfpowa4/wopi/files/@/owaatt", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "/dfpowa4/wopi/files/@/owaatt/contents", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "/dfpowa5/wopi/files/@/owaatt", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "/dfpowa5/wopi/files/@/owaatt/contents", StringComparison.OrdinalIgnoreCase));
			}
			return (string.Equals(request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase) || string.Equals(request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase)) && (string.Equals(a, "/owa/wopi/files/@/owaatt", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "/owa/wopi/files/@/owaatt/contents", StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00008C9C File Offset: 0x00006E9C
		public static bool IsRemoteNotificationRequest(HttpRequest request)
		{
			if (!string.Equals(request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			string text = HttpUtility.UrlDecode(request.Url.AbsolutePath);
			if (UrlUtilities.isPreCheckinApp)
			{
				return UrlUtilities.RemoteNotifiationDfpowaRequestPaths.Contains(text);
			}
			return string.Equals(text, "/owa/remotenotification.ashx", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00008CEE File Offset: 0x00006EEE
		public static bool IsRealmRewrittenFromPathToQuery(HttpContext httpContext)
		{
			return UrlUtilities.IsRealmRewrittenFromPathToQuery(httpContext, UrlUtilities.GetOriginalRequestUrlFromContext(httpContext));
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00008CFC File Offset: 0x00006EFC
		public static bool IsRealmRewrittenFromPathToQuery(HttpContext httpContext, Uri originalUri)
		{
			bool result = false;
			HttpRequest request = httpContext.Request;
			string value = Uri.EscapeDataString(request.QueryString["realm"] ?? string.Empty);
			if (!string.IsNullOrEmpty(value))
			{
				bool flag = request.Url.AbsolutePath.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
				bool flag2 = originalUri.AbsolutePath.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
				if (!flag && flag2)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00008D74 File Offset: 0x00006F74
		public static Uri GetOriginalRequestUrlFromContext(HttpContext httpContext)
		{
			Uri result = httpContext.Request.Url;
			string text = httpContext.Request.Headers.Get("X-OWA-OriginalRequestUrl");
			if (!string.IsNullOrEmpty(text))
			{
				Uri uri = UrlUtilities.TryParseUri(HttpUtility.UrlDecode(text));
				if (uri != null)
				{
					result = uri;
				}
			}
			return result;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00008DC3 File Offset: 0x00006FC3
		public static void SaveOriginalRequestUrlToContext(HttpContext httpContext, Uri originalURL)
		{
			if (string.IsNullOrEmpty(httpContext.Request.Headers.Get("X-OWA-OriginalRequestUrl")))
			{
				httpContext.Request.Headers.Add("X-OWA-OriginalRequestUrl", HttpUtility.UrlEncode(originalURL.AbsoluteUri));
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00008E04 File Offset: 0x00007004
		public static void SaveOriginalRequestHostSchemePortToContext(HttpContext httpContext)
		{
			if (string.IsNullOrWhiteSpace(httpContext.Request.Headers.Get("X-OriginalRequestHost")))
			{
				httpContext.Request.Headers.Add("X-OriginalRequestHost", httpContext.Request.Url.Host);
			}
			if (string.IsNullOrWhiteSpace(httpContext.Request.Headers.Get("X-OriginalRequestHostSchemePort")))
			{
				httpContext.Request.Headers.Add("X-OriginalRequestHostSchemePort", string.Format("{0}:{1}:{2}", httpContext.Request.Url.Port.ToString(), httpContext.Request.Url.Scheme, httpContext.Request.Url.Host));
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00008EDC File Offset: 0x000070DC
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
						if ((from s in UrlUtilities.ImportantQueryParamNames
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

		// Token: 0x06000137 RID: 311 RVA: 0x000090B8 File Offset: 0x000072B8
		public static string GetPathWithExplictLogonHint(Uri requestUrl, string explicitLogonUser)
		{
			return ClientAuthenticationHelper.GetPathWithExplictLogonHint(requestUrl, explicitLogonUser);
		}

		// Token: 0x04000286 RID: 646
		public const string RealmParameter = "realm";

		// Token: 0x04000287 RID: 647
		public const string WacFileIdParameter = "owaatt";

		// Token: 0x04000288 RID: 648
		public const string WacCheckFileRequestPath = "/owa/wopi/files/@/owaatt";

		// Token: 0x04000289 RID: 649
		public const string WacGetFileRequestPath = "/owa/wopi/files/@/owaatt/contents";

		// Token: 0x0400028A RID: 650
		public const string WacDfpowaCheckFileRequestPath = "/dfpowa/wopi/files/@/owaatt";

		// Token: 0x0400028B RID: 651
		public const string WacDfpowaGetFileRequestPath = "/dfpowa/wopi/files/@/owaatt/contents";

		// Token: 0x0400028C RID: 652
		public const string WacDfpowa1CheckFileRequestPath = "/dfpowa1/wopi/files/@/owaatt";

		// Token: 0x0400028D RID: 653
		public const string WacDfpowa1GetFileRequestPath = "/dfpowa1/wopi/files/@/owaatt/contents";

		// Token: 0x0400028E RID: 654
		public const string WacDfpowa2CheckFileRequestPath = "/dfpowa2/wopi/files/@/owaatt";

		// Token: 0x0400028F RID: 655
		public const string WacDfpowa2GetFileRequestPath = "/dfpowa2/wopi/files/@/owaatt/contents";

		// Token: 0x04000290 RID: 656
		public const string WacDfpowa3CheckFileRequestPath = "/dfpowa3/wopi/files/@/owaatt";

		// Token: 0x04000291 RID: 657
		public const string WacDfpowa3GetFileRequestPath = "/dfpowa3/wopi/files/@/owaatt/contents";

		// Token: 0x04000292 RID: 658
		public const string WacDfpowa4CheckFileRequestPath = "/dfpowa4/wopi/files/@/owaatt";

		// Token: 0x04000293 RID: 659
		public const string WacDfpowa4GetFileRequestPath = "/dfpowa4/wopi/files/@/owaatt/contents";

		// Token: 0x04000294 RID: 660
		public const string WacDfpowa5CheckFileRequestPath = "/dfpowa5/wopi/files/@/owaatt";

		// Token: 0x04000295 RID: 661
		public const string WacDfpowa5GetFileRequestPath = "/dfpowa5/wopi/files/@/owaatt/contents";

		// Token: 0x04000296 RID: 662
		public const string SaveUrlOnLogoffParameter = "exsvurl=1";

		// Token: 0x04000297 RID: 663
		public const string OwaVdir = "owa";

		// Token: 0x04000298 RID: 664
		public const string OwaVdirWithSlash = "/owa/";

		// Token: 0x04000299 RID: 665
		public const string EcpCloseWindowPage = "closewindow.aspx";

		// Token: 0x0400029A RID: 666
		public const string OwaLogoffPage = "logoff.aspx";

		// Token: 0x0400029B RID: 667
		public const string AuthParamName = "authRedirect";

		// Token: 0x0400029C RID: 668
		public const string VersionParam = "ver";

		// Token: 0x0400029D RID: 669
		public const string ClientExistingVersionParam = "cver";

		// Token: 0x0400029E RID: 670
		public const string BootOnlineParam = "bO";

		// Token: 0x0400029F RID: 671
		public const string AppcacheCorruptParam = "aC";

		// Token: 0x040002A0 RID: 672
		private const string RemoteNotifiationRequestPath = "/owa/remotenotification.ashx";

		// Token: 0x040002A1 RID: 673
		private const string ExplicitLogonUser = "X-OWA-ExplicitLogonUser";

		// Token: 0x040002A2 RID: 674
		private const string OrignalRequestUrlKey = "X-OWA-OriginalRequestUrl";

		// Token: 0x040002A3 RID: 675
		private const string OriginalRequestHostKey = "X-OriginalRequestHost";

		// Token: 0x040002A4 RID: 676
		private const string OriginalRequestHostSchemePort = "X-OriginalRequestHostSchemePort";

		// Token: 0x040002A5 RID: 677
		private const string OriginalRequestHostSchemePortDelimiter = ":";

		// Token: 0x040002A6 RID: 678
		private const string OriginalRequestHostSchemePortFormatString = "{0}:{1}:{2}";

		// Token: 0x040002A7 RID: 679
		private static readonly HashSet<string> RemoteNotifiationDfpowaRequestPaths;

		// Token: 0x040002A8 RID: 680
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

		// Token: 0x040002A9 RID: 681
		private static readonly Regex VersionFolderRegex = new Regex("^[0-9]{1,4}\\.[0-9]{1,4}\\.[0-9]{1,4}\\.[0-9]{1,4}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040002AA RID: 682
		private static readonly bool isPreCheckinApp = new BoolAppSettingsEntry("IsPreCheckinApp", false, null).Value;

		// Token: 0x040002AB RID: 683
		private static readonly string[] ImportantQueryParamNames = new string[]
		{
			"layout",
			"url",
			"flights"
		};
	}
}
