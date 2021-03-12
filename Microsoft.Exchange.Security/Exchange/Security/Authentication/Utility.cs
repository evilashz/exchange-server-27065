using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.DirectoryServices;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000022 RID: 34
	public static class Utility
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x00008940 File Offset: 0x00006B40
		public static bool IsResourceRequest(string localPath)
		{
			bool result = false;
			foreach (string value in Utility.resourcesExtensions)
			{
				if (localPath.EndsWith(value, StringComparison.OrdinalIgnoreCase))
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00008998 File Offset: 0x00006B98
		public static bool IsOwaRequestWithRoutingHint(HttpRequest request)
		{
			string localPath = request.Url.LocalPath;
			int num = localPath.IndexOf("owa/", StringComparison.OrdinalIgnoreCase);
			if (num == -1)
			{
				return false;
			}
			int num2 = localPath.IndexOfAny(new char[]
			{
				'@',
				'/'
			}, num + "owa/".Length);
			return num2 != -1 && localPath[num2] == '@' && Utility.routingHintRegex.IsMatch(localPath);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00008A08 File Offset: 0x00006C08
		public static bool HasResourceRoutingHint(HttpRequest request)
		{
			HttpCookie httpCookie = request.Cookies["X-BEResource"];
			return httpCookie != null && !string.IsNullOrEmpty(httpCookie.Value);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00008A3C File Offset: 0x00006C3C
		public static bool IsAnonymousResourceRequest(HttpRequest request)
		{
			HttpCookie httpCookie = request.Cookies["X-AnonResource"];
			return httpCookie != null && !string.IsNullOrEmpty(httpCookie.Value) && string.Compare(httpCookie.Value, "true", CultureInfo.InvariantCulture, CompareOptions.IgnoreCase) == 0;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00008A88 File Offset: 0x00006C88
		internal static ADRawEntry GetVirtualDirectoryObject(Guid vDirObjectGuid, ITopologyConfigurationSession session, params PropertyDefinition[] virtualDirectoryPropertyDefinitions)
		{
			ADObjectId entryId;
			if (vDirObjectGuid == Guid.Empty)
			{
				string text = HttpRuntime.AppDomainAppVirtualPath.Replace("'", "''");
				if (text[0] == '/')
				{
					text = text.Substring(1);
				}
				Server server = session.FindLocalServer();
				string text2 = HttpRuntime.AppDomainAppId;
				if (text2[0] == '/')
				{
					text2 = text2.Substring(1);
				}
				int num = text2.IndexOf('/');
				text2 = text2.Substring(num);
				text2 = string.Format(CultureInfo.InvariantCulture, "IIS://{0}{1}", new object[]
				{
					server.Fqdn,
					text2
				});
				num = text2.LastIndexOf('/');
				using (DirectoryEntry directoryEntry = new DirectoryEntry(text2.Substring(0, num)))
				{
					using (DirectoryEntry parent = directoryEntry.Parent)
					{
						if (parent != null)
						{
							text2 = (((string)parent.Properties["ServerComment"].Value) ?? string.Empty);
						}
					}
				}
				entryId = new ADObjectId(server.DistinguishedName).GetDescendantId("Protocols", "HTTP", new string[]
				{
					string.Format(CultureInfo.InvariantCulture, "{0} ({1})", new object[]
					{
						text,
						text2
					})
				});
			}
			else
			{
				entryId = new ADObjectId(vDirObjectGuid);
			}
			return session.ReadADRawEntry(entryId, virtualDirectoryPropertyDefinitions);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00008C0C File Offset: 0x00006E0C
		internal static bool IsSignOutCleanupRequest(this HttpRequest request)
		{
			return request.QueryString["wa"] == "wsignoutcleanup1.0";
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00008C28 File Offset: 0x00006E28
		internal static bool PreferAdfsAuthentication(this HttpRequest request)
		{
			return (!VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).ActiveMonitoring.AllowBasicAuthForOutsideInMonitoringMailboxes.Enabled || !request.IsActiveMonitoringUserAgent()) && (!AdfsFederationAuthModule.HasOtherAuthenticationMethod || request.ExplicitPreferAdfsAuthentication() || (request.QueryString["cross"] == null && (request.QueryString["rfr"] == null || "admin".Equals(request.QueryString["rfr"], StringComparison.OrdinalIgnoreCase))));
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00008CB4 File Offset: 0x00006EB4
		internal static bool ExplicitPreferAdfsAuthentication(this HttpRequest request)
		{
			string text = request.QueryString["cross"];
			return text != null && text != "0";
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00008CE2 File Offset: 0x00006EE2
		internal static bool IsAuthenticatedByAdfs(this HttpRequest request)
		{
			return request.Cookies["TimeWindow"] != null;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00008CFC File Offset: 0x00006EFC
		internal static bool IsAdfsLogoffRequest(this HttpRequest request)
		{
			string[] segments = request.Url.Segments;
			return request.IsAuthenticatedByAdfs() && (request.Url.LocalPath.EndsWith("/logoff.aspx", StringComparison.OrdinalIgnoreCase) || request.Url.LocalPath.EndsWith("/logoff.owa", StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00008D4F File Offset: 0x00006F4F
		internal static bool IsOWAPingRequest(HttpRequest request)
		{
			return request.Path.EndsWith("ping.owa", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00008D64 File Offset: 0x00006F64
		internal static bool IsNotOwaGetOrOehRequest(this HttpRequest request)
		{
			bool flag = false;
			if (request.Url.Segments.Length > 1)
			{
				flag = request.Url.Segments[1].Equals("owa/", StringComparison.OrdinalIgnoreCase);
			}
			return flag && request.IsNotGetOrOehRequest();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00008DA7 File Offset: 0x00006FA7
		internal static bool IsNotGetOrOehRequest(this HttpRequest request)
		{
			return !request.HttpMethod.Equals("GET", StringComparison.OrdinalIgnoreCase) || request.Url.Query.Contains("oeh=1&");
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00008DD4 File Offset: 0x00006FD4
		internal static bool IsAjaxRequest(this HttpRequest request)
		{
			string a = request.Headers["x-requested-with"];
			return a == "XMLHttpRequest";
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00008E00 File Offset: 0x00007000
		internal static bool IsAnonymousAuthFolderRequest(this HttpRequest request)
		{
			return (HttpRuntime.AppDomainAppVirtualPath.Equals("/ecp", StringComparison.OrdinalIgnoreCase) || HttpRuntime.AppDomainAppVirtualPath.Equals("/owa", StringComparison.OrdinalIgnoreCase)) && request.Url.Segments.Length >= 3 && request.Url.Segments[2].Equals("auth/", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00008E5B File Offset: 0x0000705B
		internal static bool TryReadConfigBool(string key, out bool value)
		{
			return bool.TryParse(ConfigurationManager.AppSettings[key], out value);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00008E6E File Offset: 0x0000706E
		internal static bool TryReadConfigInt(string key, out int value)
		{
			return int.TryParse(ConfigurationManager.AppSettings[key], out value);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00008E84 File Offset: 0x00007084
		internal static void DeleteFbaAuthCookies(HttpRequest httpRequest, HttpResponse httpResponse)
		{
			if (!Utility.isPartnerHostedOnly.Member && !VariantConfiguration.InvariantNoFlightingSnapshot.Cafe.NoFormBasedAuthentication.Enabled)
			{
				Utility.DeleteCookie(httpRequest, httpResponse, "cadata", null, false);
				Utility.DeleteCookie(httpRequest, httpResponse, "cadataKey", null, false);
				Utility.DeleteCookie(httpRequest, httpResponse, "cadataIV", null, false);
				Utility.DeleteCookie(httpRequest, httpResponse, "cadataSig", null, false);
				Utility.DeleteCookie(httpRequest, httpResponse, "cadataTTL", null, false);
			}
			Utility.DeleteCookie(httpRequest, httpResponse, "X-DFPOWA-Vdir", null, false);
			Utility.DeleteCookie(httpRequest, httpResponse, "TargetServer", null, false);
			Utility.DeleteCookie(httpRequest, httpResponse, "ExchClientVer", null, false);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00008F28 File Offset: 0x00007128
		internal static void DeleteCookie(HttpRequest httpRequest, HttpResponse httpResponse, string name, string path = null, bool forceToAddNewCookie = false)
		{
			if (httpRequest == null)
			{
				throw new ArgumentNullException("httpRequest");
			}
			if (httpResponse == null)
			{
				throw new ArgumentNullException("httpResponse");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("name can not be null or empty string");
			}
			if (httpRequest.Cookies[name] == null || httpRequest.Cookies[name].Value == null)
			{
				return;
			}
			HttpCookie httpCookie = httpResponse.Cookies[name];
			bool flag = forceToAddNewCookie || httpCookie == null;
			if (flag)
			{
				httpCookie = new HttpCookie(name, string.Empty);
			}
			httpCookie.Expires = DateTime.UtcNow.AddYears(-30);
			if (path != null)
			{
				httpCookie.Path = path;
			}
			if (flag)
			{
				httpResponse.Cookies.Add(httpCookie);
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00008FDC File Offset: 0x000071DC
		internal static X509Certificate2[] GetCertificates()
		{
			LocalConfiguration configuration = ConfigProvider.Instance.Configuration;
			X509Certificate2[] certificates = configuration.Certificates;
			if (certificates == null || certificates.Length < 1)
			{
				throw new AdfsConfigurationException(AdfsConfigErrorReason.NoCertificates, "Encryption certificate is absent");
			}
			return certificates;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00009011 File Offset: 0x00007211
		internal static AdfsAuthCountersInstance AdfsAuthCountersInstance
		{
			get
			{
				return Utility.counters.Member;
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000901D File Offset: 0x0000721D
		internal static bool IsActiveMonitoringUserAgent(this HttpRequest request)
		{
			return request.UserAgent != null && request.UserAgent.IndexOf("ACTIVEMONITORING", StringComparison.OrdinalIgnoreCase) >= 0;
		}

		// Token: 0x0400015A RID: 346
		public const string FbaLogoffPage = "logoff.aspx";

		// Token: 0x0400015B RID: 347
		public const string EcpLogoffUrlSegment = "/logoff.aspx";

		// Token: 0x0400015C RID: 348
		public const string OwaLogoffUrlSegment = "/logoff.owa";

		// Token: 0x0400015D RID: 349
		public const string PreferAdfsParameter = "cross";

		// Token: 0x0400015E RID: 350
		public const string UserActionQueryString = "UA";

		// Token: 0x0400015F RID: 351
		public const string TimeoutLogoutPage = "auth/TimeoutLogout.aspx";

		// Token: 0x04000160 RID: 352
		public const string AJAXHeaderName = "x-requested-with";

		// Token: 0x04000161 RID: 353
		public const string AJAXHeaderValue = "XMLHttpRequest";

		// Token: 0x04000162 RID: 354
		public const string Admin = "admin";

		// Token: 0x04000163 RID: 355
		private const string OehParameter = "oeh=1&";

		// Token: 0x04000164 RID: 356
		private const string HttpMethodGet = "GET";

		// Token: 0x04000165 RID: 357
		private const string OWAPingUrl = "ping.owa";

		// Token: 0x04000166 RID: 358
		private const string ActiveMonitoringUserAgentIdentifier = "ACTIVEMONITORING";

		// Token: 0x04000167 RID: 359
		private static readonly IList<string> resourcesExtensions = new List<string>
		{
			".gif",
			".jpg",
			".css",
			".xap",
			".js",
			".wav",
			".htm",
			".html",
			".png",
			".msi",
			".ico",
			".mp3",
			".axd",
			".eot",
			".ttf",
			".svg",
			".woff"
		}.AsReadOnly();

		// Token: 0x04000168 RID: 360
		private static readonly Regex routingHintRegex = new Regex("/owa/[a-fA-F0-9]{8}-([a-fA-F0-9]{4}-){3}[a-fA-F0-9]{12}@[A-Z0-9.-]+\\.[A-Z]{2,4}/prem/\\d{2}\\.\\d{1,}\\.\\d{1,}\\.\\d{1,}/", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04000169 RID: 361
		private static LazyMember<AdfsAuthCountersInstance> counters = new LazyMember<AdfsAuthCountersInstance>(() => AdfsAuthCounters.GetInstance(Process.GetCurrentProcess().ProcessName));

		// Token: 0x0400016A RID: 362
		private static LazyMember<bool> isPartnerHostedOnly = new LazyMember<bool>(delegate()
		{
			try
			{
				if (Datacenter.IsPartnerHostedOnly(true))
				{
					return true;
				}
			}
			catch (CannotDetermineExchangeModeException)
			{
			}
			return false;
		});
	}
}
