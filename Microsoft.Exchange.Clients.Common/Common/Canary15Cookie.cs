using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Net;
using System.Text;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Common
{
	// Token: 0x02000009 RID: 9
	public sealed class Canary15Cookie
	{
		// Token: 0x06000022 RID: 34 RVA: 0x000048AC File Offset: 0x00002AAC
		private Canary15Cookie(Canary15 canary, Canary15Profile profile)
		{
			this.profile = profile;
			this.Canary = canary;
			this.domain = string.Empty;
			this.HttpCookie = new HttpCookie(this.profile.Name, this.Value);
			this.HttpCookie.Domain = this.Domain;
			this.HttpCookie.Path = this.profile.Path;
			this.NetCookie = new Cookie(this.profile.Name, this.Value, this.profile.Path, this.Domain);
			this.HttpCookie.Secure = true;
			this.NetCookie.Secure = true;
			this.HttpCookie.HttpOnly = false;
			this.NetCookie.HttpOnly = false;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00004978 File Offset: 0x00002B78
		public Canary15Cookie(string logOnUniqueKey, Canary15Profile profile) : this(new Canary15(logOnUniqueKey), profile)
		{
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00004987 File Offset: 0x00002B87
		public string Value
		{
			get
			{
				return this.Canary.ToString();
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00004994 File Offset: 0x00002B94
		// (set) Token: 0x06000026 RID: 38 RVA: 0x0000499C File Offset: 0x00002B9C
		public string Domain
		{
			get
			{
				return this.domain;
			}
			set
			{
				this.domain = value;
				this.HttpCookie.Domain = this.domain;
				this.NetCookie.Domain = this.domain;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000049C7 File Offset: 0x00002BC7
		public bool IsRenewed
		{
			get
			{
				return this.Canary.IsRenewed;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000049D4 File Offset: 0x00002BD4
		public bool IsAboutToExpire
		{
			get
			{
				return this.Canary.IsAboutToExpire;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000049E1 File Offset: 0x00002BE1
		public DateTime CreationTime
		{
			get
			{
				return this.Canary.CreationTime;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000049EE File Offset: 0x00002BEE
		public string LogData
		{
			get
			{
				return this.Canary.LogData;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000049FB File Offset: 0x00002BFB
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00004A03 File Offset: 0x00002C03
		public HttpCookie HttpCookie { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00004A0C File Offset: 0x00002C0C
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00004A14 File Offset: 0x00002C14
		public Cookie NetCookie { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00004A1D File Offset: 0x00002C1D
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00004A25 File Offset: 0x00002C25
		internal Canary15 Canary { get; private set; }

		// Token: 0x06000031 RID: 49 RVA: 0x00004A30 File Offset: 0x00002C30
		public static Canary15Cookie TryCreateFromHttpContext(HttpContext httpContext, string logOnUniqueKey, Canary15Profile profile)
		{
			HttpCookie cookie = httpContext.Request.Cookies.Get(profile.Name);
			return Canary15Cookie.TryCreateFromHttpCookie(cookie, logOnUniqueKey, profile);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00004A60 File Offset: 0x00002C60
		public static bool ValidateCanaryInHeaders(HttpContext httpContext, string userSid, Canary15Profile profile, out Canary15Cookie.CanaryValidationResult result)
		{
			string text = httpContext.Request.Headers[profile.Name];
			bool flag = true;
			if (Canary15.RestoreCanary15(text, userSid) != null)
			{
				result = Canary15Cookie.CanaryValidationResult.HeaderMatch;
			}
			else
			{
				string text2;
				try
				{
					string components = httpContext.Request.Url.GetComponents(UriComponents.Query, UriFormat.Unescaped);
					string query = HttpUtility.HtmlDecode(components);
					NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(query);
					text2 = nameValueCollection[profile.Name];
				}
				catch
				{
					text2 = null;
				}
				if (Canary15.RestoreCanary15(text2, userSid) != null)
				{
					result = Canary15Cookie.CanaryValidationResult.UrlParameterMatch;
				}
				else
				{
					string text3 = httpContext.Request.Form[profile.Name];
					if (Canary15.RestoreCanary15(text3, userSid) != null)
					{
						result = Canary15Cookie.CanaryValidationResult.FormParameterMatch;
					}
					else
					{
						flag = false;
						result = Canary15Cookie.CanaryValidationResult.NotFound;
						if (ExTraceGlobals.CoreCallTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							StringBuilder stringBuilder = new StringBuilder();
							for (int i = 0; i < httpContext.Request.Cookies.Count; i++)
							{
								HttpCookie httpCookie = httpContext.Request.Cookies.Get(i);
								if (string.Equals(httpCookie.Name, profile.Name, StringComparison.OrdinalIgnoreCase))
								{
									stringBuilder.AppendFormat("[{0}]", httpCookie.Value);
								}
							}
							ExTraceGlobals.CoreTracer.TraceDebug(11L, "Canary15Cookie='{0}',HttpHeader.Canary='{1}', UrlParam.Canary='{2}', Form.Canary='{3}', success={4}, result={5}", new object[]
							{
								stringBuilder.ToString(),
								text,
								text2,
								text3,
								flag,
								result.ToString()
							});
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00004BE8 File Offset: 0x00002DE8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}={1}", new object[]
			{
				this.profile.Name,
				this.Value
			});
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00004C24 File Offset: 0x00002E24
		public string ToLoggerString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0}", this.HttpCookie.Value);
			return stringBuilder.ToString();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00004C54 File Offset: 0x00002E54
		private static Canary15Cookie Create(Canary15 canary, Canary15Profile profile)
		{
			if (canary == null)
			{
				ExTraceGlobals.CoreTracer.TraceDebug(20L, "Canary == null");
				return null;
			}
			return new Canary15Cookie(canary, profile);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00004C74 File Offset: 0x00002E74
		private static Canary15Cookie TryCreateFromHttpCookie(HttpCookie cookie, string logonUniqueKey, Canary15Profile profile)
		{
			string text = null;
			Canary15 canary = null;
			if (cookie == null)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string>(21L, "Http cookie is null, Name={0}", profile.Name);
			}
			else if (string.IsNullOrEmpty(cookie.Value))
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string, string, string>(21L, "Http cookie value is null, Name={0}, Domain={1}, Path={2}", cookie.Name, cookie.Domain, cookie.Path);
			}
			else if (!Canary15Cookie.TryGetCookieValue(cookie.Value, out text))
			{
				ExTraceGlobals.CoreTracer.TraceDebug(21L, "TryParseCookeValue failed, Name={0}, Domain={1}, Path={2}, Value={3}", new object[]
				{
					cookie.Name,
					cookie.Domain,
					cookie.Path,
					cookie.Value
				});
			}
			else
			{
				canary = Canary15.RestoreCanary15(text, logonUniqueKey);
			}
			if (canary == null)
			{
				if (cookie != null)
				{
					ExTraceGlobals.CoreTracer.TraceDebug(21L, "restoredCanary==null, Name={0}, Domain={1}, Path={2}, Value={3}, canaryString={4}, logonUniqueKey={5}", new object[]
					{
						cookie.Name,
						cookie.Domain,
						cookie.Path,
						cookie.Value,
						text,
						logonUniqueKey
					});
				}
				canary = new Canary15(logonUniqueKey);
				ExTraceGlobals.CoreTracer.TraceDebug<string, string, string>(21L, "Canary is recreated, userContextId={0}, logonUniqueKey={1}, canaryString={2}", canary.UserContextId, canary.LogonUniqueKey, canary.ToString());
			}
			return Canary15Cookie.Create(canary, profile);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00004DAD File Offset: 0x00002FAD
		private static bool TryGetCookieValue(string cookieValue, out string canaryString)
		{
			if (string.IsNullOrEmpty(cookieValue) || cookieValue.Length != 76)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string, int>(22L, "Invalid format cookie={0}, cookieValue.Length={1}", cookieValue, (cookieValue == null) ? -1 : cookieValue.Length);
				canaryString = null;
				return false;
			}
			canaryString = cookieValue;
			return true;
		}

		// Token: 0x040001C0 RID: 448
		internal const int UserContextCanaryLength = 76;

		// Token: 0x040001C1 RID: 449
		private const int GuidLength = 32;

		// Token: 0x040001C2 RID: 450
		private string domain;

		// Token: 0x040001C3 RID: 451
		private Canary15Profile profile;

		// Token: 0x0200000A RID: 10
		public enum CanaryValidationResult
		{
			// Token: 0x040001C8 RID: 456
			NotFound,
			// Token: 0x040001C9 RID: 457
			HeaderMatch,
			// Token: 0x040001CA RID: 458
			UrlParameterMatch,
			// Token: 0x040001CB RID: 459
			FormParameterMatch
		}
	}
}
