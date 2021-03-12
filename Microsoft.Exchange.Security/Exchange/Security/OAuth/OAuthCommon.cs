using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000D1 RID: 209
	internal static class OAuthCommon
	{
		// Token: 0x06000724 RID: 1828 RVA: 0x00032AC0 File Offset: 0x00030CC0
		static OAuthCommon()
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				OAuthCommon.processName = currentProcess.ProcessName;
				if (string.Equals(currentProcess.ProcessName, OAuthCommon.IISWorkerProcessName, StringComparison.OrdinalIgnoreCase) && currentProcess.StartInfo != null && currentProcess.StartInfo.EnvironmentVariables != null && currentProcess.StartInfo.EnvironmentVariables.ContainsKey(OAuthCommon.AppPoolId))
				{
					OAuthCommon.appPoolName = currentProcess.StartInfo.EnvironmentVariables[OAuthCommon.AppPoolId];
				}
			}
			string text = OAuthCommon.processName;
			if (!string.IsNullOrEmpty(OAuthCommon.appPoolName))
			{
				text = text + "_" + OAuthCommon.appPoolName;
			}
			OAuthCommon.counters = OAuthCounters.GetInstance(text);
			ExPerformanceCounter[] array = new ExPerformanceCounter[]
			{
				OAuthCommon.counters.NumberOfAuthRequests,
				OAuthCommon.counters.NumberOfFailedAuthRequests,
				OAuthCommon.counters.AverageResponseTime,
				OAuthCommon.counters.AverageAuthServerResponseTime,
				OAuthCommon.counters.NumberOfAuthServerTokenRequests,
				OAuthCommon.counters.NumberOfPendingAuthServerRequests,
				OAuthCommon.counters.AuthServerTokenCacheSize,
				OAuthCommon.counters.NumberOfAuthServerTimeoutTokenRequests
			};
			foreach (ExPerformanceCounter exPerformanceCounter in array)
			{
				exPerformanceCounter.RawValue = 0L;
			}
			OAuthCommon.latencycounterToRunningAverageFloatMap = new Dictionary<string, RunningAverageFloat>
			{
				{
					OAuthCommon.counters.AverageResponseTime.CounterName,
					new RunningAverageFloat(200)
				},
				{
					OAuthCommon.counters.AverageAuthServerResponseTime.CounterName,
					new RunningAverageFloat(200)
				}
			};
			OAuthCommon.eventLogger = new ExEventLog(ExTraceGlobals.AuthenticationTracer.Category, OAuthCommon.OAuthComponent);
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x00032CA8 File Offset: 0x00030EA8
		public static string OAuthComponent
		{
			get
			{
				return OAuthCommon.componentName;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x00032CAF File Offset: 0x00030EAF
		public static ExEventLog EventLogger
		{
			get
			{
				return OAuthCommon.eventLogger;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x00032CB6 File Offset: 0x00030EB6
		public static OAuthCountersInstance PerfCounters
		{
			get
			{
				return OAuthCommon.counters;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x00032CBD File Offset: 0x00030EBD
		public static string CurrentAppPoolName
		{
			get
			{
				return OAuthCommon.appPoolName;
			}
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00032CC4 File Offset: 0x00030EC4
		public static void UpdateMovingAveragePerformanceCounter(ExPerformanceCounter performanceCounter, long newValue)
		{
			string counterName = performanceCounter.CounterName;
			lock (performanceCounter)
			{
				OAuthCommon.latencycounterToRunningAverageFloatMap[counterName].Update((float)newValue);
				performanceCounter.RawValue = (long)OAuthCommon.latencycounterToRunningAverageFloatMap[counterName].Value;
			}
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00032D2C File Offset: 0x00030F2C
		public static bool IsIdMatch(string id1, string id2)
		{
			if (string.IsNullOrEmpty(id1))
			{
				throw new ArgumentNullException("id1");
			}
			if (string.IsNullOrEmpty(id2))
			{
				throw new ArgumentNullException("id2");
			}
			return string.Equals(id1, id2, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00032D5C File Offset: 0x00030F5C
		public static bool IsRealmMatch(string realm1, string realm2)
		{
			return string.Equals(realm1, realm2, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00032D66 File Offset: 0x00030F66
		public static bool IsRealmMatchIncludingEmpty(string realm1, string realm2)
		{
			return (OAuthCommon.IsRealmEmpty(realm1) && OAuthCommon.IsRealmEmpty(realm2)) || OAuthCommon.IsRealmMatch(realm1, realm2);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00032D81 File Offset: 0x00030F81
		public static bool IsRealmEmpty(string realm)
		{
			return string.IsNullOrEmpty(realm) || string.Equals(realm, "*", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00032D9C File Offset: 0x00030F9C
		public static object VerifyNonNullArgument(string name, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(name);
			}
			string text = value as string;
			if (text != null && string.IsNullOrEmpty(text))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "{0} cannot be an empty string", new object[]
				{
					name
				}), name);
			}
			return value;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00032DE8 File Offset: 0x00030FE8
		public static string SerializeToJson(this object value)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			return javaScriptSerializer.Serialize(value);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00032E04 File Offset: 0x00031004
		public static T DeserializeFromJson<T>(this string value)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			return javaScriptSerializer.Deserialize<T>(value);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00032E20 File Offset: 0x00031020
		public static string GetReadableTokenString(string token)
		{
			if (string.IsNullOrEmpty(token))
			{
				return token;
			}
			string result;
			try
			{
				string[] array = token.Split(new char[]
				{
					'.'
				});
				string arg = array[0];
				string arg2 = array[1];
				Dictionary<string, object> value = OAuthCommon.Base64UrlEncoder.Decode(arg).DeserializeFromJson<Dictionary<string, object>>();
				Dictionary<string, object> dictionary = OAuthCommon.Base64UrlEncoder.Decode(arg2).DeserializeFromJson<Dictionary<string, object>>();
				object obj = null;
				if (dictionary.TryGetValue(Constants.ClaimTypes.ActorToken, out obj))
				{
					dictionary[Constants.ClaimTypes.ActorToken] = "...";
				}
				string text = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
				{
					value.SerializeToJson(),
					dictionary.SerializeToJson()
				});
				if (obj == null)
				{
					result = text;
				}
				else
				{
					result = string.Format("{0}; actor: {1}", text, OAuthCommon.GetReadableTokenString(obj as string));
				}
			}
			catch
			{
				result = token;
			}
			return result;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00032F04 File Offset: 0x00031104
		public static bool TryGetClaimValue(JwtSecurityToken token, string claimType, out string claimValue)
		{
			claimValue = OAuthCommon.TryGetClaimValue(token.Payload, claimType);
			return claimValue != null;
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00032F1C File Offset: 0x0003111C
		public static string TryGetClaimValue(JwtPayload payload, string claimType)
		{
			object obj;
			payload.TryGetValue(claimType, out obj);
			return obj as string;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00032F3C File Offset: 0x0003113C
		public static string GetLoggableTokenString(string rawToken, JwtSecurityToken token)
		{
			string empty = string.Empty;
			if (token == null || OAuthCommon.TryGetClaimValue(token, Constants.ClaimTypes.ActorToken, out empty))
			{
				return OAuthCommon.GetReadableTokenString(rawToken);
			}
			return token.ToString();
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00032F70 File Offset: 0x00031170
		public static string WriteAuthorizationHeader(string token)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
			{
				Constants.BearerAuthenticationType,
				token
			});
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x00032FA0 File Offset: 0x000311A0
		internal static string DefaultAcceptedDomain
		{
			get
			{
				if (OAuthCommon.defaultAcceptedDomain == null)
				{
					OAuthCommon.defaultAcceptedDomain = OAuthConfigHelper.GetOrganizationRealm(OrganizationId.ForestWideOrgId);
				}
				return OAuthCommon.defaultAcceptedDomain;
			}
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00032FC0 File Offset: 0x000311C0
		internal static OrganizationId ResolveOrganizationByDomain(string domain)
		{
			SmtpDomain smtpDomain;
			if (AuthCommon.IsMultiTenancyEnabled)
			{
				if (SmtpDomain.TryParse(domain, out smtpDomain))
				{
					return DomainToOrganizationIdCache.Singleton.Get(smtpDomain);
				}
			}
			else if (SmtpDomain.TryParse(domain, out smtpDomain) && OrganizationId.ForestWideOrgId == DomainToOrganizationIdCache.Singleton.Get(smtpDomain))
			{
				return OrganizationId.ForestWideOrgId;
			}
			return null;
		}

		// Token: 0x04000691 RID: 1681
		private const int LatencyCounterNumberOfSamples = 200;

		// Token: 0x04000692 RID: 1682
		private static readonly string componentName = "MSExchange OAuth";

		// Token: 0x04000693 RID: 1683
		private static readonly string IISWorkerProcessName = "w3wp";

		// Token: 0x04000694 RID: 1684
		private static readonly string AppPoolId = "APP_POOL_ID";

		// Token: 0x04000695 RID: 1685
		private static OAuthCountersInstance counters;

		// Token: 0x04000696 RID: 1686
		private static ExEventLog eventLogger;

		// Token: 0x04000697 RID: 1687
		private static readonly Dictionary<string, RunningAverageFloat> latencycounterToRunningAverageFloatMap;

		// Token: 0x04000698 RID: 1688
		private static string defaultAcceptedDomain;

		// Token: 0x04000699 RID: 1689
		private static string processName;

		// Token: 0x0400069A RID: 1690
		private static string appPoolName;

		// Token: 0x020000D2 RID: 210
		public static class Base64UrlEncoder
		{
			// Token: 0x06000738 RID: 1848 RVA: 0x00033040 File Offset: 0x00031240
			public static string EncodeBytes(byte[] array)
			{
				return string.Concat<char>(Convert.ToBase64String(array).TakeWhile((char c) => c != OAuthCommon.Base64UrlEncoder.Base64PadCharacter).Select(delegate(char c)
				{
					if (c == OAuthCommon.Base64UrlEncoder.Base64Character62)
					{
						return OAuthCommon.Base64UrlEncoder.Base64UrlCharacter62;
					}
					if (c != OAuthCommon.Base64UrlEncoder.Base64Character63)
					{
						return c;
					}
					return OAuthCommon.Base64UrlEncoder.Base64UrlCharacter63;
				}));
			}

			// Token: 0x06000739 RID: 1849 RVA: 0x0003309C File Offset: 0x0003129C
			public static byte[] DecodeBytes(string arg)
			{
				string text = arg.Replace(OAuthCommon.Base64UrlEncoder.Base64UrlCharacter62, OAuthCommon.Base64UrlEncoder.Base64Character62);
				text = text.Replace(OAuthCommon.Base64UrlEncoder.Base64UrlCharacter63, OAuthCommon.Base64UrlEncoder.Base64Character63);
				switch (text.Length % 4)
				{
				case 0:
					goto IL_72;
				case 2:
					text += OAuthCommon.Base64UrlEncoder.DoubleBase64PadCharacter;
					goto IL_72;
				case 3:
					text += OAuthCommon.Base64UrlEncoder.Base64PadCharacter;
					goto IL_72;
				}
				throw new ArgumentException("Illegal base64url string!", arg);
				IL_72:
				return Convert.FromBase64String(text);
			}

			// Token: 0x0600073A RID: 1850 RVA: 0x00033121 File Offset: 0x00031321
			public static string Decode(string arg)
			{
				return Encoding.UTF8.GetString(OAuthCommon.Base64UrlEncoder.DecodeBytes(arg));
			}

			// Token: 0x0400069B RID: 1691
			private static char Base64PadCharacter = '=';

			// Token: 0x0400069C RID: 1692
			private static string DoubleBase64PadCharacter = string.Format(CultureInfo.InvariantCulture, "{0}{0}", new object[]
			{
				OAuthCommon.Base64UrlEncoder.Base64PadCharacter
			});

			// Token: 0x0400069D RID: 1693
			private static char Base64Character62 = '+';

			// Token: 0x0400069E RID: 1694
			private static char Base64Character63 = '/';

			// Token: 0x0400069F RID: 1695
			private static char Base64UrlCharacter62 = '-';

			// Token: 0x040006A0 RID: 1696
			private static char Base64UrlCharacter63 = '_';
		}
	}
}
