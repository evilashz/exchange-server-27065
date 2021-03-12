using System;
using System.Net;
using System.Text;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000BC RID: 188
	public sealed class UserContextCookie
	{
		// Token: 0x06000786 RID: 1926 RVA: 0x00018089 File Offset: 0x00016289
		private UserContextCookie(string cookieId, string userContextId, string mailboxUniqueKey, bool isSecure)
		{
			this.cookieId = cookieId;
			this.userContextId = userContextId;
			this.mailboxUniqueKey = mailboxUniqueKey;
			this.isSecure = isSecure;
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x000180B0 File Offset: 0x000162B0
		internal HttpCookie HttpCookie
		{
			get
			{
				if (this.httpCookie == null)
				{
					this.httpCookie = new HttpCookie(this.CookieName, this.CookieValue);
					this.httpCookie.HttpOnly = true;
					this.httpCookie.Secure = this.isSecure;
				}
				return this.httpCookie;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x000180FF File Offset: 0x000162FF
		internal Cookie NetCookie
		{
			get
			{
				if (this.netCookie == null)
				{
					this.netCookie = new Cookie(this.CookieName, this.CookieValue);
				}
				return this.netCookie;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x00018128 File Offset: 0x00016328
		internal string CookieName
		{
			get
			{
				string text = UserContextCookie.UserContextCookiePrefix;
				if (this.cookieId != null)
				{
					text = text + "_" + this.cookieId;
				}
				return text;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x00018156 File Offset: 0x00016356
		internal string UserContextId
		{
			get
			{
				return this.userContextId;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x0001815E File Offset: 0x0001635E
		internal string MailboxUniqueKey
		{
			get
			{
				return this.mailboxUniqueKey;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x00018168 File Offset: 0x00016368
		internal string CookieValue
		{
			get
			{
				if (this.cookieValue == null)
				{
					this.cookieValue = this.userContextId;
					if (this.mailboxUniqueKey != null)
					{
						UTF8Encoding utf8Encoding = new UTF8Encoding();
						byte[] bytes = utf8Encoding.GetBytes(this.mailboxUniqueKey);
						this.cookieValue = this.cookieValue + "&" + UserContextUtilities.ValidTokenBase64Encode(bytes);
					}
				}
				return this.cookieValue;
			}
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x000181C6 File Offset: 0x000163C6
		public override string ToString()
		{
			return this.CookieName + "=" + this.CookieValue;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x000181DE File Offset: 0x000163DE
		internal static UserContextCookie Create(string cookieId, string userContextId, string mailboxUniqueKey, bool isSecure)
		{
			return new UserContextCookie(cookieId, userContextId, mailboxUniqueKey, isSecure);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x000181E9 File Offset: 0x000163E9
		internal static UserContextCookie CreateFromKey(string cookieId, UserContextKey userContextKey, bool isSecure)
		{
			return UserContextCookie.Create(cookieId, userContextKey.UserContextId, userContextKey.MailboxUniqueKey, isSecure);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00018200 File Offset: 0x00016400
		internal static UserContextCookie TryCreateFromHttpCookie(HttpCookie cookie)
		{
			string text = null;
			string text2 = null;
			if (string.IsNullOrEmpty(cookie.Value))
			{
				return null;
			}
			if (!UserContextCookie.TryParseCookieValue(cookie.Value, out text, out text2))
			{
				return null;
			}
			string text3 = null;
			if (!UserContextCookie.TryParseCookieName(cookie.Name, out text3))
			{
				return null;
			}
			return UserContextCookie.Create(text3, text, text2, cookie.Secure);
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00018254 File Offset: 0x00016454
		internal static bool TryParseCookieValue(string cookieValue, out string userContextId, out string mailboxUniqueKey)
		{
			userContextId = null;
			mailboxUniqueKey = null;
			if (cookieValue.Length == 32)
			{
				userContextId = cookieValue;
			}
			else
			{
				if (cookieValue.Length < 34)
				{
					return false;
				}
				int num = cookieValue.IndexOf('&');
				if (num != 32)
				{
					return false;
				}
				num++;
				userContextId = cookieValue.Substring(0, num - 1);
				string tokenValidBase64String = cookieValue.Substring(num, cookieValue.Length - num);
				byte[] bytes = null;
				try
				{
					bytes = UserContextUtilities.ValidTokenBase64Decode(tokenValidBase64String);
				}
				catch (FormatException)
				{
					return false;
				}
				UTF8Encoding utf8Encoding = new UTF8Encoding();
				mailboxUniqueKey = utf8Encoding.GetString(bytes);
			}
			return UserContextCookie.IsValidUserContextId(userContextId);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x000182F4 File Offset: 0x000164F4
		internal static bool TryParseCookieName(string cookieName, out string cookieId)
		{
			cookieId = null;
			if (!cookieName.StartsWith(UserContextCookie.UserContextCookiePrefix, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			int length = UserContextCookie.UserContextCookiePrefix.Length;
			if (cookieName.Length == length)
			{
				return true;
			}
			cookieId = cookieName.Substring(length + 1, cookieName.Length - length - 1);
			return UserContextUtilities.IsValidGuid(cookieId);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001834C File Offset: 0x0001654C
		internal static UserContextCookie GetUserContextCookie(HttpContext httpContext)
		{
			HttpRequest request = httpContext.Request;
			for (int i = 0; i < request.Cookies.Count; i++)
			{
				HttpCookie httpCookie = request.Cookies[i];
				if (httpCookie.Name != null && httpCookie.Name.StartsWith(UserContextCookie.UserContextCookiePrefix, StringComparison.OrdinalIgnoreCase))
				{
					UserContextCookie userContextCookie = UserContextCookie.TryCreateFromHttpCookie(httpCookie);
					if (userContextCookie == null)
					{
						ExTraceGlobals.UserContextTracer.TraceDebug<string, string, string>(0L, "Invalid user context cookie received. Name={0}, Value={1}, httpContext.Request.RawUrl={2}", httpCookie.Name, httpCookie.Value, request.RawUrl);
						return null;
					}
					if (userContextCookie.MailboxUniqueKey == null)
					{
						if (!UserContextUtilities.IsDifferentMailbox(httpContext))
						{
							return userContextCookie;
						}
					}
					else
					{
						string explicitLogonUser = UserContextUtilities.GetExplicitLogonUser(httpContext);
						if (!string.IsNullOrEmpty(explicitLogonUser))
						{
							using (OwaIdentity owaIdentity = OwaIdentity.CreateOwaIdentityFromExplicitLogonAddress(explicitLogonUser))
							{
								if (string.Equals(userContextCookie.MailboxUniqueKey, owaIdentity.UniqueId, StringComparison.Ordinal))
								{
									return userContextCookie;
								}
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00018444 File Offset: 0x00016644
		private static bool IsValidUserContextId(string userContextId)
		{
			return UserContextUtilities.IsValidGuid(userContextId);
		}

		// Token: 0x0400042B RID: 1067
		internal const int UserContextIdLength = 32;

		// Token: 0x0400042C RID: 1068
		public static readonly string UserContextCookiePrefix = "UC";

		// Token: 0x0400042D RID: 1069
		private readonly bool isSecure;

		// Token: 0x0400042E RID: 1070
		private string userContextId;

		// Token: 0x0400042F RID: 1071
		private string mailboxUniqueKey;

		// Token: 0x04000430 RID: 1072
		private string cookieId;

		// Token: 0x04000431 RID: 1073
		private HttpCookie httpCookie;

		// Token: 0x04000432 RID: 1074
		private Cookie netCookie;

		// Token: 0x04000433 RID: 1075
		private string cookieValue;
	}
}
