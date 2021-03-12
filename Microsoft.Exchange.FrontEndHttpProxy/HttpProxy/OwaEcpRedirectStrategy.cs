using System;
using System.Text;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000041 RID: 65
	internal class OwaEcpRedirectStrategy : DatacenterRedirectStrategy
	{
		// Token: 0x060001F8 RID: 504 RVA: 0x0000AE35 File Offset: 0x00009035
		public OwaEcpRedirectStrategy(IRequestContext requestContext) : base(requestContext)
		{
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000AE40 File Offset: 0x00009040
		protected override Uri GetRedirectUrl(string redirectServer)
		{
			string podRedirectUrl = OwaEcpRedirectStrategy.GetPodRedirectUrl(base.RequestContext.HttpContext.Request.Url, redirectServer);
			Uri uri = new Uri(podRedirectUrl);
			string text = null;
			string host = base.RequestContext.HttpContext.Request.Url.Host;
			HttpCookie httpCookie = base.RequestContext.HttpContext.Request.Cookies["orgName"];
			if (httpCookie != null && !string.IsNullOrEmpty(httpCookie.Value))
			{
				text = httpCookie.Value.ToLowerInvariant();
			}
			if (text != null && !host.Contains(Constants.OutlookDomain))
			{
				uri = OwaEcpRedirectStrategy.AddRealmParameter(uri, text);
			}
			return uri;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000AEE8 File Offset: 0x000090E8
		private static Uri AddRealmParameter(Uri uri, string org)
		{
			if (!string.IsNullOrEmpty(org))
			{
				string text = "realm=" + HttpUtility.UrlEncode(org);
				UriBuilder uriBuilder = new UriBuilder(uri);
				if (uriBuilder.Query != null && uriBuilder.Query.Length > 1)
				{
					uriBuilder.Query = uriBuilder.Query.Substring(1) + "&" + text;
				}
				else
				{
					uriBuilder.Query = text;
				}
				uri = uriBuilder.Uri;
			}
			return uri;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000AF5C File Offset: 0x0000915C
		private static string GetPodRedirectUrl(Uri url, string fqdn)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(url.Scheme);
			stringBuilder.Append("://");
			stringBuilder.Append(fqdn);
			if (OwaEcpRedirectStrategy.ShouldSaveUrlOnLogoff(url) || OwaEcpRedirectStrategy.IsInCalendarVDir.Member)
			{
				stringBuilder.Append(url.PathAndQuery);
			}
			else
			{
				stringBuilder.Append("/");
				stringBuilder.Append(HttpProxyGlobals.VirtualDirectoryName.Member);
				stringBuilder.Append("/");
				string value;
				if (OwaEcpRedirectStrategy.TryGetExplicitLogonUrlSegment(url, out value))
				{
					stringBuilder.Append(value);
					stringBuilder.Append("/");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000B000 File Offset: 0x00009200
		private static bool ShouldSaveUrlOnLogoff(Uri url)
		{
			return OwaEcpRedirectStrategy.ReturnToOriginalUrlByDefault.Value || url.Query.Contains("exsvurl=1") || url.Query.Contains("rru=contacts");
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000B034 File Offset: 0x00009234
		private static bool TryGetExplicitLogonUrlSegment(Uri url, out string explicitLogonSegment)
		{
			explicitLogonSegment = string.Empty;
			string originalString = url.OriginalString;
			string text = HttpProxyGlobals.VirtualDirectoryName.Member + "/";
			int num = originalString.IndexOf(text) + text.Length;
			if (num < 0 || num >= originalString.Length)
			{
				return false;
			}
			int num2 = originalString.IndexOf("/", num);
			if (num2 == -1)
			{
				return false;
			}
			int length = num2 - num;
			explicitLogonSegment = originalString.Substring(num, length);
			int num3 = explicitLogonSegment.IndexOf('@');
			return num3 > 0 && num3 < explicitLogonSegment.Length - 2;
		}

		// Token: 0x040000FC RID: 252
		private const string OrganizationNameCookieName = "orgName";

		// Token: 0x040000FD RID: 253
		private const string CalendarVDirPostfix = "/calendar";

		// Token: 0x040000FE RID: 254
		private const string SaveUrlOnLogoffParameter = "exsvurl=1";

		// Token: 0x040000FF RID: 255
		private const string RruUrlParameter = "rru=contacts";

		// Token: 0x04000100 RID: 256
		private static readonly LazyMember<bool> IsInCalendarVDir = new LazyMember<bool>(() => !string.IsNullOrEmpty(HttpRuntime.AppDomainAppId) && HttpRuntime.AppDomainAppId.EndsWith("/calendar", StringComparison.CurrentCultureIgnoreCase));

		// Token: 0x04000101 RID: 257
		private static readonly BoolAppSettingsEntry ReturnToOriginalUrlByDefault = new BoolAppSettingsEntry("ReturnToOriginalUrlByDefault", false, ExTraceGlobals.VerboseTracer);
	}
}
