using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Passport.RPS;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x0200001F RID: 31
	internal static class LiveIdAuthentication
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000034DE File Offset: 0x000016DE
		public static bool IsInitialized
		{
			get
			{
				return LiveIdAuthentication.rpsOrgIdSession != null;
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000034EC File Offset: 0x000016EC
		public static void Initialize(string virtualDirectoryNameParam, bool sslOffloadedParam)
		{
			if (!string.IsNullOrEmpty(virtualDirectoryNameParam) && (virtualDirectoryNameParam.StartsWith("/", StringComparison.OrdinalIgnoreCase) || virtualDirectoryNameParam.EndsWith("/", StringComparison.OrdinalIgnoreCase)))
			{
				throw new ArgumentException("virtualDirectoryNameParam should not contain leading or trailing slashes", "virtualDirectoryNameParam");
			}
			if (!string.IsNullOrEmpty(virtualDirectoryNameParam))
			{
				LiveIdAuthentication.virtualDirectoryNameWithLeadingSlash = "/" + virtualDirectoryNameParam;
			}
			try
			{
				RPS rps = new RPS();
				rps.Initialize(null);
				LiveIdAuthentication.rpsOrgIdSession = rps;
			}
			catch (COMException e)
			{
				LiveIdAuthentication.rpsOrgIdSession = null;
				LiveIdErrorHandler.ThrowRPSException(e);
			}
			LiveIdAuthentication.sslOffloaded = sslOffloadedParam;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003580 File Offset: 0x00001780
		internal static string GetCurrentEnvironment(bool useConsumerRps)
		{
			string result;
			if (useConsumerRps)
			{
				if (LiveIdAuthentication.consumerCurrentEnvironment == null)
				{
					using (RPSHttpAuthClient rpshttpAuthClient = LiveIdAuthentication.CreateRPSClient(true))
					{
						LiveIdAuthentication.consumerCurrentEnvironment = rpshttpAuthClient.GetCurrentEnvironment();
					}
				}
				result = LiveIdAuthentication.consumerCurrentEnvironment;
			}
			else
			{
				if (LiveIdAuthentication.enterpriseCurrentEnvironment == null)
				{
					using (RPSHttpAuthClient rpshttpAuthClient2 = LiveIdAuthentication.CreateRPSClient(false))
					{
						LiveIdAuthentication.enterpriseCurrentEnvironment = rpshttpAuthClient2.GetCurrentEnvironment();
					}
				}
				result = LiveIdAuthentication.enterpriseCurrentEnvironment;
			}
			return result;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003608 File Offset: 0x00001808
		private static string GetSiteProperty(string siteName, string siteProperty, bool useConsumerRps)
		{
			string siteProperty2;
			using (RPSHttpAuthClient rpshttpAuthClient = LiveIdAuthentication.CreateRPSClient(useConsumerRps))
			{
				siteProperty2 = rpshttpAuthClient.GetSiteProperty(siteName, siteProperty);
			}
			return siteProperty2;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003644 File Offset: 0x00001844
		public static string GetDefaultReturnUrl(string siteName, bool useConsumerRps)
		{
			if (siteName == null)
			{
				throw new ArgumentNullException("siteName");
			}
			return LiveIdAuthentication.GetSiteProperty(siteName, "ReturnURL", useConsumerRps);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003660 File Offset: 0x00001860
		public static void Shutdown()
		{
			if (LiveIdAuthentication.rpsOrgIdSession != null)
			{
				LiveIdAuthentication.rpsOrgIdSession.Shutdown();
				LiveIdAuthentication.rpsOrgIdSession = null;
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000367C File Offset: 0x0000187C
		public static bool Authenticate(HttpContext httpContext, string siteName, string authPolicyOverrideValue, string[] memberNameIgnorePrefixes, bool useConsumerRps, out string puid, out string orgIdPuid, out string cid, out string membername, out uint issueTime, out uint loginAttributes, out string responseHeaders, out uint rpsTicketType, out RPSTicket deprecatedRpsTicketObject, out bool hasAcceptedAccrual, out uint rpsAuthState, out bool isOrgIdFederatedMsaIdentity)
		{
			if (!LiveIdAuthentication.IsInitialized)
			{
				throw new InvalidOperationException(Strings.ComponentNotInitialized);
			}
			if (siteName == null)
			{
				throw new ArgumentNullException("siteName");
			}
			hasAcceptedAccrual = false;
			puid = null;
			orgIdPuid = null;
			cid = null;
			membername = null;
			issueTime = 0U;
			loginAttributes = 0U;
			responseHeaders = null;
			rpsTicketType = 0U;
			deprecatedRpsTicketObject = null;
			rpsAuthState = 0U;
			isOrgIdFederatedMsaIdentity = false;
			RPSPropBag rpspropBag = null;
			string text = httpContext.Request.QueryString["f"];
			if (!string.IsNullOrEmpty(text))
			{
				ExTraceGlobals.LiveIdAuthenticationModuleTracer.TraceError<string>(0L, "Querystring contains F-code: {0}.", text);
				return false;
			}
			try
			{
				if (!useConsumerRps)
				{
					rpspropBag = new RPSPropBag(LiveIdAuthentication.rpsOrgIdSession);
				}
				RPSProfile rpsprofile = null;
				using (RPSHttpAuthClient rpshttpAuthClient = LiveIdAuthentication.CreateRPSClient(useConsumerRps))
				{
					int? rpsErrorCode;
					string rpsErrorString;
					rpsprofile = rpshttpAuthClient.Authenticate(siteName, authPolicyOverrideValue, LiveIdAuthentication.sslOffloaded, httpContext.Request, rpspropBag, out rpsErrorCode, out rpsErrorString, out deprecatedRpsTicketObject);
					LiveIdAuthentication.ValidateRpsCallAndThrowOnFailure(rpsErrorCode, rpsErrorString);
				}
				if (rpsprofile == null)
				{
					return false;
				}
				if (!useConsumerRps && deprecatedRpsTicketObject != null)
				{
					try
					{
						using (RPSPropBag rpspropBag2 = new RPSPropBag(LiveIdAuthentication.rpsOrgIdSession))
						{
							rpspropBag2["SlidingWindow"] = 0;
							if (!string.IsNullOrEmpty(authPolicyOverrideValue))
							{
								rpspropBag2["AuthPolicy"] = authPolicyOverrideValue;
							}
							if (!deprecatedRpsTicketObject.Validate(rpspropBag2))
							{
								return false;
							}
						}
					}
					catch (COMException ex)
					{
						ExTraceGlobals.LiveIdAuthenticationModuleTracer.TraceError<COMException>(0L, "Failed to validate ticket: {0}.", ex);
						LiveIdErrorHandler.ThrowRPSException(ex);
					}
				}
				rpsAuthState = rpsprofile.RPSAuthState;
				rpsTicketType = rpsprofile.TicketType;
				if (LiveIdAuthenticationModule.AppPasswordCheckEnabled && !httpContext.Request.Url.AbsolutePath.StartsWith("/owa/", StringComparison.OrdinalIgnoreCase) && rpsprofile.AppPassword)
				{
					AppPasswordAccessException exception = new AppPasswordAccessException();
					httpContext.Response.AppendToLog("&AppPasswordBlocked");
					Utilities.HandleException(httpContext, exception, false);
				}
				hasAcceptedAccrual = LiveIdAuthentication.HasAcceptedAccruals(rpsprofile);
				orgIdPuid = rpsprofile.HexPuid;
				cid = (string.IsNullOrWhiteSpace(rpsprofile.ConsumerCID) ? rpsprofile.HexCID : rpsprofile.ConsumerCID);
				puid = (string.IsNullOrWhiteSpace(rpsprofile.ConsumerPuid) ? orgIdPuid : rpsprofile.ConsumerPuid);
				membername = rpsprofile.MemberName;
				string text2;
				if (LiveIdAuthentication.TryRemoveMemberNamePrefixes(membername, memberNameIgnorePrefixes, out text2))
				{
					membername = text2;
					isOrgIdFederatedMsaIdentity = true;
				}
				issueTime = rpsprofile.IssueInstant;
				loginAttributes = rpsprofile.LoginAttributes;
				string text3 = loginAttributes.ToString();
				httpContext.Response.AppendToLog("&loginAttributes=" + text3);
				if (!string.IsNullOrWhiteSpace(text3))
				{
					httpContext.Response.AppendToLog(string.Format("loginAttributes={0}", text3));
					httpContext.Request.Headers.Add("X-LoginAttributes", text3);
				}
				responseHeaders = rpsprofile.ResponseHeader;
			}
			finally
			{
				if (rpspropBag != null)
				{
					rpspropBag.Dispose();
				}
			}
			return true;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003998 File Offset: 0x00001B98
		public static bool ValidateWithSlidingWindow(RPSTicket rpsTicket, TimeSpan slidingWindow)
		{
			RPSPropBag rpspropBag = null;
			try
			{
				rpspropBag = new RPSPropBag(LiveIdAuthentication.rpsOrgIdSession);
				rpspropBag["SlidingWindow"] = slidingWindow.TotalSeconds;
				if (!rpsTicket.Validate(rpspropBag))
				{
					int num = (int)rpspropBag["ReasonHR"];
					if (num == -2147184087)
					{
						return false;
					}
				}
			}
			catch (COMException e)
			{
				LiveIdErrorHandler.ThrowRPSException(e);
			}
			finally
			{
				if (rpspropBag != null)
				{
					rpspropBag.Dispose();
				}
			}
			return true;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003A28 File Offset: 0x00001C28
		public static void Logout(HttpContext httpContext, string siteName, bool useConsumerRps)
		{
			using (RPSHttpAuthClient rpshttpAuthClient = LiveIdAuthentication.CreateRPSClient(useConsumerRps))
			{
				int? rpsErrorCode = null;
				string rpsErrorString = null;
				string logoutHeaders = rpshttpAuthClient.GetLogoutHeaders(siteName, out rpsErrorCode, out rpsErrorString);
				LiveIdAuthentication.ValidateRpsCallAndThrowOnFailure(rpsErrorCode, rpsErrorString);
				LiveIdAuthentication.WriteHeadersToResponse(httpContext, logoutHeaders, useConsumerRps);
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003A80 File Offset: 0x00001C80
		public static void WriteHeadersToResponse(HttpContext httpContext, string headers, bool useConsumerRps)
		{
			HttpResponse response = httpContext.Response;
			if (!"no-cache".Equals(response.CacheControl, StringComparison.OrdinalIgnoreCase) && !"no-store".Equals(response.CacheControl, StringComparison.OrdinalIgnoreCase) && !"private".Equals(response.CacheControl, StringComparison.OrdinalIgnoreCase))
			{
				response.Cache.SetCacheability(HttpCacheability.NoCache, "set-cookie");
			}
			try
			{
				using (RPSHttpAuth rpshttpAuth = new RPSHttpAuth(LiveIdAuthentication.rpsOrgIdSession))
				{
					if (AuthCommon.IsFrontEnd || CafeHelper.IsFromNativeProxy(httpContext.Request))
					{
						rpshttpAuth.WriteHeaders(response, headers);
					}
					else
					{
						response.SetCookie(new HttpCookie("CopyLiveIdAuthCookieFromBE", HttpUtility.UrlEncode(headers)));
					}
				}
			}
			catch (COMException e)
			{
				LiveIdErrorHandler.ThrowRPSException(e);
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003B50 File Offset: 0x00001D50
		private static string GetRedirectUrl(LiveIdAuthentication.RedirectType rt, string siteName, string returnUrl, string authPolicy, bool useConsumerRps)
		{
			string constructUrlParam;
			if (rt == LiveIdAuthentication.RedirectType.Logout)
			{
				constructUrlParam = "Logout";
			}
			else if (rt == LiveIdAuthentication.RedirectType.SilentAuthenticate)
			{
				constructUrlParam = "SilentAuth";
			}
			else
			{
				constructUrlParam = "Auth";
			}
			string formattedReturnUrl;
			if (!LiveIdAuthentication.TryFormatUrl(returnUrl, out formattedReturnUrl))
			{
				formattedReturnUrl = returnUrl;
			}
			string result;
			using (RPSHttpAuthClient rpshttpAuthClient = LiveIdAuthentication.CreateRPSClient(useConsumerRps))
			{
				int? rpsErrorCode = null;
				string rpsErrorString = null;
				string redirectUrl = rpshttpAuthClient.GetRedirectUrl(constructUrlParam, siteName, formattedReturnUrl, authPolicy, out rpsErrorCode, out rpsErrorString);
				LiveIdAuthentication.ValidateRpsCallAndThrowOnFailure(rpsErrorCode, rpsErrorString);
				result = redirectUrl;
			}
			return result;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003BD4 File Offset: 0x00001DD4
		private static bool HasAcceptedAccruals(RPSProfile ticket)
		{
			string consumerPuid = ticket.ConsumerPuid;
			bool flag;
			bool flag2;
			bool flag3;
			if (string.IsNullOrEmpty(consumerPuid))
			{
				int tokenFlags = ticket.TokenFlags;
				flag = ((tokenFlags & 536870912) == 0 || (tokenFlags & 16384) == 0);
				flag2 = ((tokenFlags & 128) != 0);
				flag3 = ((tokenFlags & 32) != 0 || (tokenFlags & 64) != 0);
			}
			else
			{
				flag = Convert.ToBoolean(ticket.HasSignedTOU);
				flag2 = Convert.ToBoolean(ticket.ConsumerChild);
				string consumerConsentLevel = ticket.ConsumerConsentLevel;
				flag3 = ("FULL".Equals(consumerConsentLevel) || "PARTIAL".Equals(consumerConsentLevel));
			}
			return flag && (!flag2 || flag3);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003C88 File Offset: 0x00001E88
		public static string GetAuthenticateRedirectUrl(string returnUrl, string siteName, string authPolicy, string federatedDomain, string userName, bool addCBCXT, bool useSilentAuthentication, bool useConsumerRps)
		{
			if (useSilentAuthentication)
			{
				returnUrl += ((returnUrl.IndexOf('?') == -1) ? "?" : "&");
				returnUrl += "silent=1";
			}
			string text = LiveIdAuthentication.GetRedirectUrl(useSilentAuthentication ? LiveIdAuthentication.RedirectType.SilentAuthenticate : LiveIdAuthentication.RedirectType.Authenticate, siteName, returnUrl, authPolicy, useConsumerRps);
			if (!string.IsNullOrEmpty(federatedDomain))
			{
				ExTraceGlobals.LiveIdAuthenticationModuleTracer.TraceDebug<string>(0L, "Append whr parameter {0} for live authentication to bypass the 'go there' experience", federatedDomain);
				text = text + "&whr=" + HttpUtility.UrlEncode(federatedDomain);
			}
			if (!string.IsNullOrEmpty(userName))
			{
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					"&",
					Utilities.UserNameParameter,
					"=",
					userName
				});
			}
			if (addCBCXT)
			{
				text += "&CBCXT=out";
			}
			return text;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003D4F File Offset: 0x00001F4F
		public static string GetLiveLogoutRedirectUrl(string returnUrl, string siteName, bool useConsumerRps)
		{
			return LiveIdAuthentication.GetRedirectUrl(LiveIdAuthentication.RedirectType.Logout, siteName, returnUrl, null, useConsumerRps);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003D5C File Offset: 0x00001F5C
		private static bool TryFormatUrl(string url, out string formattedUrl)
		{
			formattedUrl = string.Empty;
			Uri uri = new Uri(url);
			if (!string.IsNullOrEmpty(LiveIdAuthentication.virtualDirectoryNameWithLeadingSlash) && uri.LocalPath.EndsWith(LiveIdAuthentication.virtualDirectoryNameWithLeadingSlash, StringComparison.OrdinalIgnoreCase))
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("https://");
				stringBuilder.Append(uri.Host);
				stringBuilder.Append(uri.LocalPath);
				stringBuilder.Append("/");
				stringBuilder.Append(uri.Query);
				formattedUrl = stringBuilder.ToString();
				return true;
			}
			if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append("https://");
				stringBuilder2.Append(uri.Host);
				stringBuilder2.Append(uri.PathAndQuery);
				formattedUrl = stringBuilder2.ToString();
				return true;
			}
			return false;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003E2C File Offset: 0x0000202C
		public static void DeleteCookie(HttpResponse response, string name)
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
			response.Cookies[name].Expires = DateTime.UtcNow.AddYears(-30);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003EE8 File Offset: 0x000020E8
		internal static bool TryRemoveMemberNamePrefixes(string memberName, IEnumerable<string> memberNameIgnorePrefixes, out string normalizedMemberName)
		{
			if (string.IsNullOrWhiteSpace(memberName))
			{
				throw new ArgumentNullException("memberName", "memberName cannot be null or empty.");
			}
			bool result = false;
			normalizedMemberName = memberName;
			ExTraceGlobals.LiveIdAuthenticationModuleTracer.TraceDebug<string>(0L, "OrgID returned RPSMembername '{0}'.", memberName);
			if (memberNameIgnorePrefixes != null)
			{
				foreach (string text in memberNameIgnorePrefixes.OrderBy((string s) => s.Length, Comparer<int>.Create((int len1, int len2) => len2.CompareTo(len1))))
				{
					ExTraceGlobals.LiveIdAuthenticationModuleTracer.TraceDebug<string>(0L, "Attempting to remove prefix '{0}'.", text);
					if (normalizedMemberName.IndexOf(text, StringComparison.OrdinalIgnoreCase) == 0)
					{
						normalizedMemberName = normalizedMemberName.Remove(0, text.Length).Trim();
						ExTraceGlobals.LiveIdAuthenticationModuleTracer.TraceDebug<string>(0L, "Normalized RPSMembername to '{0}'.", normalizedMemberName);
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003FEC File Offset: 0x000021EC
		private static void ValidateRpsCallAndThrowOnFailure(int? rpsErrorCode, string rpsErrorString)
		{
			try
			{
				if (rpsErrorCode != null)
				{
					rpsErrorString = (string.IsNullOrWhiteSpace(rpsErrorString) ? "An error occurred calling RPS" : rpsErrorString);
					ExTraceGlobals.LiveIdAuthenticationModuleTracer.TraceDebug<int, string>(0L, "RPSHttpAuthClient failed with error code {0} and message {1}.", rpsErrorCode.Value, rpsErrorString);
					throw new COMException(rpsErrorString, rpsErrorCode.Value);
				}
			}
			catch (COMException e)
			{
				LiveIdErrorHandler.ThrowRPSException(e);
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004058 File Offset: 0x00002258
		private static RPSHttpAuthClient CreateRPSClient(bool useConsumerRps)
		{
			int maxValue = int.MaxValue;
			return new RPSHttpAuthClient(useConsumerRps, LiveIdAuthentication.rpsOrgIdSession, maxValue);
		}

		// Token: 0x04000042 RID: 66
		internal const string CopyLiveIdAuthCookieName = "CopyLiveIdAuthCookieFromBE";

		// Token: 0x04000043 RID: 67
		internal const string RPSConstructURLLogout = "Logout";

		// Token: 0x04000044 RID: 68
		internal const string RPSHexCID = "HexCID";

		// Token: 0x04000045 RID: 69
		internal const string RPSMembername = "Membername";

		// Token: 0x04000046 RID: 70
		internal const string RPSConstructURLAuth = "Auth";

		// Token: 0x04000047 RID: 71
		internal const string RPSConstructURLSilentAuth = "SilentAuth";

		// Token: 0x04000048 RID: 72
		internal const string RPSReturnURL = "ReturnURL";

		// Token: 0x04000049 RID: 73
		internal const string RPSAuthPolicy = "AuthPolicy";

		// Token: 0x0400004A RID: 74
		internal const string RPSAuthInstant = "AuthInstant";

		// Token: 0x0400004B RID: 75
		internal const string RPSIssueInstant = "IssueInstant";

		// Token: 0x0400004C RID: 76
		internal const string RPSConsumerTOUAccepted = "ConsumerTOUAccepted";

		// Token: 0x0400004D RID: 77
		internal const string RPSConsumerChild = "ConsumerChild";

		// Token: 0x0400004E RID: 78
		internal const string RPSConsumerChildConsent = "ConsumerConsentLevel";

		// Token: 0x0400004F RID: 79
		internal const string RPSConsumerPUID = "ConsumerPUID";

		// Token: 0x04000050 RID: 80
		internal const string RPSConsumerCID = "ConsumerCID";

		// Token: 0x04000051 RID: 81
		internal const string RPSLoginAttributes = "LoginAttributes";

		// Token: 0x04000052 RID: 82
		private const string RPSRespHeaders = "RPSRespHeaders";

		// Token: 0x04000053 RID: 83
		private const string WHRParameter = "whr";

		// Token: 0x04000054 RID: 84
		internal const string AuthPolicy_MBI_KEY = "MBI_KEY";

		// Token: 0x04000055 RID: 85
		internal const string AuthPolicy_MBI_SSL = "MBI_SSL";

		// Token: 0x04000056 RID: 86
		internal const string AuthPolicy_MBI_SSL_60SECTEST = "MBI_SSL_60SECTEST";

		// Token: 0x04000057 RID: 87
		private const string CBCXTParameter = "CBCXT=out";

		// Token: 0x04000058 RID: 88
		private const string ConsentLevelNone = "NONE";

		// Token: 0x04000059 RID: 89
		private const string ConsentLevelPartial = "PARTIAL";

		// Token: 0x0400005A RID: 90
		private const string ConsentLevelFull = "FULL";

		// Token: 0x0400005B RID: 91
		private const string HttpPrefix = "http://";

		// Token: 0x0400005C RID: 92
		private const string HttpsPrefix = "https://";

		// Token: 0x0400005D RID: 93
		private const int AccrualTouBitMask = 16384;

		// Token: 0x0400005E RID: 94
		private const int AccrualMsnTouBitMask = 536870912;

		// Token: 0x0400005F RID: 95
		private const int AccrualParentalLimitedConsentBitMask = 32;

		// Token: 0x04000060 RID: 96
		private const int AccrualParentalFullConsentBitMask = 64;

		// Token: 0x04000061 RID: 97
		private const int AccrualIsChildBitMask = 128;

		// Token: 0x04000062 RID: 98
		private const string ForwardSlash = "/";

		// Token: 0x04000063 RID: 99
		private const string RPSPropBagHttps = "HTTPS";

		// Token: 0x04000064 RID: 100
		private const int SlidingWindowExpired = -2147184087;

		// Token: 0x04000065 RID: 101
		private const string SlidingWindow = "SlidingWindow";

		// Token: 0x04000066 RID: 102
		private const string ReasonHR = "ReasonHR";

		// Token: 0x04000067 RID: 103
		private const string NoCacheHeader = "no-cache";

		// Token: 0x04000068 RID: 104
		private const string NoStoreCacheHeader = "no-store";

		// Token: 0x04000069 RID: 105
		private const string PrivateCacheHeader = "private";

		// Token: 0x0400006A RID: 106
		private static RPS rpsOrgIdSession;

		// Token: 0x0400006B RID: 107
		private static bool sslOffloaded;

		// Token: 0x0400006C RID: 108
		private static string enterpriseCurrentEnvironment;

		// Token: 0x0400006D RID: 109
		private static string consumerCurrentEnvironment;

		// Token: 0x0400006E RID: 110
		private static string virtualDirectoryNameWithLeadingSlash;

		// Token: 0x02000020 RID: 32
		internal enum RPSTicketType
		{
			// Token: 0x04000072 RID: 114
			Compact = 2,
			// Token: 0x04000073 RID: 115
			RPSAuth,
			// Token: 0x04000074 RID: 116
			RPSSecAuth
		}

		// Token: 0x02000021 RID: 33
		private enum RedirectType
		{
			// Token: 0x04000076 RID: 118
			Authenticate,
			// Token: 0x04000077 RID: 119
			Logout,
			// Token: 0x04000078 RID: 120
			SilentAuthenticate
		}

		// Token: 0x02000022 RID: 34
		internal enum RPSAuthState
		{
			// Token: 0x0400007A RID: 122
			No,
			// Token: 0x0400007B RID: 123
			Yes,
			// Token: 0x0400007C RID: 124
			Maybe
		}
	}
}
