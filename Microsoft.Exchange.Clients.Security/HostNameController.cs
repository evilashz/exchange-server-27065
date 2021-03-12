using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x02000010 RID: 16
	public class HostNameController
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00002C94 File Offset: 0x00000E94
		public HostNameController(NameValueCollection appSettings)
		{
			this.deprecatedToNewHostNameMap = new Dictionary<string, string>();
			string text = appSettings["FlightedOwaEcpCanonicalHostName"];
			if (!string.IsNullOrEmpty(text))
			{
				string text2 = appSettings["DeprecatedOwaEcpCanonicalHostName"];
				if (!string.IsNullOrEmpty(text2))
				{
					string[] array = text2.Split(new char[]
					{
						','
					}, StringSplitOptions.RemoveEmptyEntries);
					text = text.ToLowerInvariant().Trim();
					foreach (string text3 in array)
					{
						this.deprecatedToNewHostNameMap[text3.ToLowerInvariant().Trim()] = text;
					}
				}
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002D31 File Offset: 0x00000F31
		public bool IsDeprecatedHostName(string hostName, out string newHostName)
		{
			newHostName = null;
			return !string.IsNullOrEmpty(hostName) && this.deprecatedToNewHostNameMap.TryGetValue(hostName.ToLowerInvariant(), out newHostName);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002D58 File Offset: 0x00000F58
		public bool TrySwitchOwaHostNameAndReturnPermanentRedirect(HttpContext context)
		{
			HttpRequest request = context.Request;
			Uri uri = request.GetRequestUrlEvenIfProxied();
			HttpResponse response = context.Response;
			HttpCookie httpCookie = request.Cookies.Get("HostSwitch");
			bool flag = httpCookie != null && httpCookie.Value == "1";
			string host = uri.Host;
			string host2 = null;
			if (flag && request.RequestType == "GET" && this.IsDeprecatedHostName(host, out host2) && this.IsOwaStartPageRequest(uri) && !this.HasNonRedirectableQueryParams(request.QueryString.AllKeys))
			{
				bool flag2 = OfflineClientRequestUtilities.IsRequestForAppCachedVersion(context);
				bool flag3 = OfflineClientRequestUtilities.IsRequestFromMOWAClient(request, request.UserAgent);
				if (!flag2 && !flag3)
				{
					uri = new UriBuilder(uri)
					{
						Host = host2
					}.Uri;
					this.RedirectPermanent(context, uri);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002E2C File Offset: 0x0000102C
		public void AddHostSwitchFlightEnabledCookie(HttpResponse response)
		{
			HttpCookie httpCookie = new HttpCookie("HostSwitch");
			httpCookie.HttpOnly = true;
			httpCookie.Path = "/";
			httpCookie.Value = "1";
			httpCookie.Expires = DateTime.UtcNow.AddMonths(1);
			response.Cookies.Add(httpCookie);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002E84 File Offset: 0x00001084
		public virtual bool IsUserAgentExcludedFromHostNameSwitchFlight(HttpRequest request)
		{
			UserAgent userAgent = new UserAgent(request.UserAgent, false, request.Cookies);
			return OfflineClientRequestUtilities.IsRequestFromMOWAClient(request, request.UserAgent) || userAgent.IsIos || userAgent.IsAndroid;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002EC4 File Offset: 0x000010C4
		protected virtual void RedirectPermanent(HttpContext context, Uri requestUri)
		{
			string text = requestUri.ToString();
			context.Response.Clear();
			context.Response.Cache.SetCacheability(HttpCacheability.Private);
			context.Response.Cache.SetMaxAge(HostNameController.PermanentRedirectToNewHostNameMaxAge);
			context.Response.AppendToLog("OwaHostChange301RedirectUri=" + text);
			context.Response.RedirectPermanent(text, true);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002F2C File Offset: 0x0000112C
		protected bool IsOwaStartPageRequest(Uri requestUri)
		{
			string[] segments = requestUri.Segments;
			int num = segments.Length;
			return num > 1 && num < 5 && segments[1].IndexOf("owa", StringComparison.OrdinalIgnoreCase) != -1 && (num <= 2 || !string.IsNullOrEmpty(UrlUtilities.ValidateFederatedDomainInURL(requestUri)) || segments[2].Contains("@")) && (num <= 3 || segments[3].Contains("@"));
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002F9C File Offset: 0x0000119C
		protected bool HasNonRedirectableQueryParams(string[] queryParams)
		{
			foreach (string key in queryParams)
			{
				if (HostNameController.NonRedirectableQueryParams.ContainsKey(key))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000033 RID: 51
		public const string DeprecatedHostNameConfigKey = "DeprecatedOwaEcpCanonicalHostName";

		// Token: 0x04000034 RID: 52
		public const string FlightedHostNameConfigKey = "FlightedOwaEcpCanonicalHostName";

		// Token: 0x04000035 RID: 53
		public const string HostNameFlightEnabledCookieName = "HostSwitch";

		// Token: 0x04000036 RID: 54
		public const string HostNameFlightEnabledCookieValue = "1";

		// Token: 0x04000037 RID: 55
		public const int HostNameChangeCookieValidityInMonths = 1;

		// Token: 0x04000038 RID: 56
		internal const string IISLogFieldPrefixFor301Redirects = "OwaHostChange301RedirectUri=";

		// Token: 0x04000039 RID: 57
		public static readonly TimeSpan PermanentRedirectToNewHostNameMaxAge = TimeSpan.FromHours(720.0);

		// Token: 0x0400003A RID: 58
		internal static readonly Dictionary<string, string> NonRedirectableQueryParams = new Dictionary<string, string>
		{
			{
				"authRedirect",
				"true"
			},
			{
				"ver",
				"true"
			},
			{
				"cver",
				"true"
			},
			{
				"bO",
				"true"
			},
			{
				"aC",
				"true"
			},
			{
				"flights",
				"true"
			}
		};

		// Token: 0x0400003B RID: 59
		private Dictionary<string, string> deprecatedToNewHostNameMap;
	}
}
