using System;
using System.Net;
using System.Text;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200027B RID: 635
	public sealed class UserContextCookie
	{
		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001647 RID: 5703 RVA: 0x000831F0 File Offset: 0x000813F0
		// (set) Token: 0x06001648 RID: 5704 RVA: 0x000831F8 File Offset: 0x000813F8
		internal Canary ContextCanary { get; private set; }

		// Token: 0x06001649 RID: 5705 RVA: 0x00083201 File Offset: 0x00081401
		private UserContextCookie(string cookieId, Canary canary, string mailboxUniqueKey)
		{
			this.cookieId = cookieId;
			this.mailboxUniqueKey = mailboxUniqueKey;
			this.ContextCanary = canary;
			this.cookieValue = null;
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x00083228 File Offset: 0x00081428
		internal UserContextCookie(string cookieId, string userContextId, string logonUniqueKey, string mailboxUniqueKey)
		{
			Guid userContextId2;
			if (!Guid.TryParse(userContextId, out userContextId2))
			{
				userContextId2 = Guid.NewGuid();
			}
			this.cookieId = cookieId;
			this.mailboxUniqueKey = mailboxUniqueKey;
			this.ContextCanary = new Canary(userContextId2, logonUniqueKey);
			this.cookieValue = null;
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x0008326E File Offset: 0x0008146E
		internal static UserContextCookie CreateFromKey(string cookieId, UserContextKey userContextKey)
		{
			return new UserContextCookie(cookieId, userContextKey.Canary, userContextKey.MailboxUniqueKey);
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x00083282 File Offset: 0x00081482
		internal static UserContextCookie Create(string cookieId, Canary canary, string mailboxUniqueKey)
		{
			if (canary == null)
			{
				ExTraceGlobals.UserContextTracer.TraceDebug<string, string>(20L, "Canary == null, cookieId={0}, mailboxUniqueKey={1}", cookieId, mailboxUniqueKey);
				return null;
			}
			return new UserContextCookie(cookieId, canary, mailboxUniqueKey);
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x000832A8 File Offset: 0x000814A8
		internal static UserContextCookie TryCreateFromHttpCookie(HttpCookie cookie, string logonUniqueKey)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			Canary canary = null;
			if (string.IsNullOrEmpty(cookie.Value))
			{
				ExTraceGlobals.UserContextTracer.TraceDebug<string, string, string>(21L, "Http cookie value is null, Name={0}, Domain={1}, Path={2}", cookie.Name, cookie.Domain, cookie.Path);
			}
			else if (!UserContextCookie.TryParseCookieValue(cookie.Value, out text, out text2))
			{
				ExTraceGlobals.UserContextTracer.TraceDebug(21L, "TryParseCookeValue failed, Name={0}, Domain={1}, Path={2}, Value={3}", new object[]
				{
					cookie.Name,
					cookie.Domain,
					cookie.Path,
					cookie.Value
				});
			}
			else
			{
				if (!UserContextCookie.TryParseCookieName(cookie.Name, out text3))
				{
					ExTraceGlobals.UserContextTracer.TraceDebug(21L, "TryParseCookieName failed, Name={0}, Domain={1}, Path={2}, vVlue={3}, canaryString={4}, mailboxUniqueKey={5}", new object[]
					{
						cookie.Name,
						cookie.Domain,
						cookie.Path,
						cookie.Value,
						text,
						text2
					});
				}
				canary = Canary.RestoreCanary(text, logonUniqueKey);
			}
			if (canary == null)
			{
				ExTraceGlobals.UserContextTracer.TraceDebug(21L, "restoredCanary==null, Name={0}, Domain={1}, Path={2}, Value={3}, canaryString={4}, mailboxUniqueKey={5}, logonUniqueKey={6}", new object[]
				{
					cookie.Name,
					cookie.Domain,
					cookie.Path,
					cookie.Value,
					text,
					text2,
					logonUniqueKey
				});
				canary = new Canary(Guid.NewGuid(), logonUniqueKey);
				ExTraceGlobals.UserContextTracer.TraceDebug<string, string, string>(21L, "Canary is recreated, userContextId={0}, logonUniqueKey={1}, canaryString={2}", canary.UserContextId, canary.LogonUniqueKey, canary.ToString());
			}
			return UserContextCookie.Create(text3, canary, text2);
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x00083440 File Offset: 0x00081640
		internal HttpCookie HttpCookie
		{
			get
			{
				if (this.httpCookie == null)
				{
					this.httpCookie = new HttpCookie(this.CookieName, this.CookieValue);
					this.httpCookie.Secure = true;
				}
				return this.httpCookie;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x00083473 File Offset: 0x00081673
		internal Cookie NetCookie
		{
			get
			{
				if (this.netCookie == null)
				{
					this.netCookie = new Cookie(this.CookieName, this.CookieValue);
					this.netCookie.Secure = true;
				}
				return this.netCookie;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x000834A8 File Offset: 0x000816A8
		internal string CookieName
		{
			get
			{
				string text = "UserContext";
				if (this.cookieId != null)
				{
					text = text + "_" + this.cookieId;
				}
				return text;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x000834D8 File Offset: 0x000816D8
		internal string CookieValue
		{
			get
			{
				if (this.cookieValue == null)
				{
					this.cookieValue = this.ContextCanary.ToString();
					if (this.mailboxUniqueKey != null)
					{
						UTF8Encoding utf8Encoding = new UTF8Encoding();
						byte[] bytes = utf8Encoding.GetBytes(this.mailboxUniqueKey);
						this.cookieValue = this.cookieValue + "&" + Utilities.ValidTokenBase64Encode(bytes);
					}
				}
				return this.cookieValue;
			}
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x0008353B File Offset: 0x0008173B
		public override string ToString()
		{
			return this.CookieName + "=" + this.CookieValue;
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001653 RID: 5715 RVA: 0x00083553 File Offset: 0x00081753
		internal string MailboxUniqueKey
		{
			get
			{
				return this.mailboxUniqueKey;
			}
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x0008355C File Offset: 0x0008175C
		internal static bool TryParseCookieValue(string cookieValue, out string canaryString, out string mailboxUniqueKey)
		{
			canaryString = null;
			mailboxUniqueKey = null;
			if (cookieValue.Length == 76)
			{
				canaryString = cookieValue;
			}
			else
			{
				if (cookieValue.Length < 78)
				{
					ExTraceGlobals.UserContextTracer.TraceDebug<string, int>(22L, "Invalid format cookie={0}, cookieValue.Length={1}", cookieValue, cookieValue.Length);
					return false;
				}
				int num = cookieValue.IndexOf('&');
				if (num != 76)
				{
					ExTraceGlobals.UserContextTracer.TraceDebug<int, int>(22L, "keyIndex={0}!= UserContextCookie.UserContextCanaryLength={1}", num, 76);
					return false;
				}
				num++;
				canaryString = cookieValue.Substring(0, num - 1);
				string text = cookieValue.Substring(num, cookieValue.Length - num);
				byte[] bytes = null;
				try
				{
					bytes = Utilities.ValidTokenBase64Decode(text);
				}
				catch (FormatException ex)
				{
					if (ExTraceGlobals.UserContextTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.UserContextTracer.TraceDebug<string, string>(22L, "FormatException:{0}, encodedMailboxUniqueKey={1}", ex.ToString(), text);
					}
					return false;
				}
				UTF8Encoding utf8Encoding = new UTF8Encoding();
				mailboxUniqueKey = utf8Encoding.GetString(bytes);
				return true;
			}
			return true;
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x0008364C File Offset: 0x0008184C
		internal static bool TryParseCookieName(string cookieName, out string cookieId)
		{
			cookieId = null;
			if (!cookieName.StartsWith("UserContext", StringComparison.OrdinalIgnoreCase))
			{
				ExTraceGlobals.UserContextTracer.TraceDebug<string>(23L, "cookieName={0}", cookieName);
				return false;
			}
			int length = "UserContext".Length;
			if (cookieName.Length == length)
			{
				return true;
			}
			cookieId = cookieName.Substring(length + 1, cookieName.Length - length - 1);
			if (!Utilities.IsValidGuid(cookieId))
			{
				ExTraceGlobals.UserContextTracer.TraceDebug<string>(23L, "invalid cookieId={0}", cookieId);
				return false;
			}
			return true;
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x000836CA File Offset: 0x000818CA
		private static bool IsValidUserContextId(string userContextId)
		{
			return Utilities.IsValidGuid(userContextId);
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x000836D4 File Offset: 0x000818D4
		internal static UserContextCookie GetUserContextCookie(OwaContext owaContext)
		{
			HttpRequest request = owaContext.HttpContext.Request;
			for (int i = 0; i < request.Cookies.Count; i++)
			{
				HttpCookie httpCookie = request.Cookies[i];
				if (httpCookie.Name != null && httpCookie.Name.StartsWith("UserContext", StringComparison.OrdinalIgnoreCase))
				{
					UserContextCookie userContextCookie = UserContextCookie.TryCreateFromHttpCookie(httpCookie, owaContext.LogonIdentity.UniqueId);
					if (userContextCookie == null)
					{
						ExTraceGlobals.UserContextTracer.TraceDebug(24L, "Invalid user context cookie received. Cookie value={0}, logonIdentity={1}, owaContext.MailboxIdentity.UniqueId={2}, owaContext.IsDifferentMailbox={3}", new object[]
						{
							httpCookie.Value,
							owaContext.LogonIdentity.UniqueId,
							owaContext.MailboxIdentity.UniqueId,
							owaContext.IsDifferentMailbox
						});
						throw new OwaInvalidRequestException("Invalid user context cookie received. Cookie value:" + httpCookie.Value + " logonIdentity:" + owaContext.LogonIdentity.UniqueId);
					}
					if (userContextCookie.MailboxUniqueKey == null)
					{
						if (!owaContext.IsDifferentMailbox)
						{
							return userContextCookie;
						}
					}
					else if (string.Equals(userContextCookie.MailboxUniqueKey, owaContext.MailboxIdentity.UniqueId, StringComparison.Ordinal))
					{
						return userContextCookie;
					}
					ExTraceGlobals.UserContextTracer.TraceDebug<string, string, bool>(24L, "currentCookie.MailboxUniqueKey={0}, owaContext.MailboxIdentity.UniqueId={1}, owaContext.IsDifferentMailbox={2}", userContextCookie.MailboxUniqueKey, owaContext.MailboxIdentity.UniqueId, owaContext.IsDifferentMailbox);
				}
			}
			return null;
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x0008381C File Offset: 0x00081A1C
		internal UserContextCookie CloneWithNewCanary()
		{
			return new UserContextCookie(this.cookieId, this.ContextCanary.UserContextId, this.ContextCanary.LogonUniqueKey, this.mailboxUniqueKey);
		}

		// Token: 0x04001155 RID: 4437
		internal const int UserContextCanaryLength = 76;

		// Token: 0x04001156 RID: 4438
		public const string UserContextCookiePrefix = "UserContext";

		// Token: 0x04001157 RID: 4439
		private string mailboxUniqueKey;

		// Token: 0x04001158 RID: 4440
		private string cookieId;

		// Token: 0x04001159 RID: 4441
		private HttpCookie httpCookie;

		// Token: 0x0400115A RID: 4442
		private Cookie netCookie;

		// Token: 0x0400115B RID: 4443
		private string cookieValue;
	}
}
