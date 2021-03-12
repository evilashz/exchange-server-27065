using System;
using System.Security.Principal;
using System.Text;
using System.Web;
using Microsoft.Exchange.Clients.Security;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000BD RID: 189
	internal sealed class UserContextKey
	{
		// Token: 0x06000796 RID: 1942 RVA: 0x00018458 File Offset: 0x00016658
		private UserContextKey(string userContextId, string logonUniqueKey, string mailboxUniqueKey)
		{
			this.userContextId = userContextId;
			this.logonUniqueKey = logonUniqueKey;
			this.mailboxUniqueKey = mailboxUniqueKey;
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x00018475 File Offset: 0x00016675
		internal string UserContextId
		{
			get
			{
				return this.userContextId;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x0001847D File Offset: 0x0001667D
		// (set) Token: 0x06000799 RID: 1945 RVA: 0x00018485 File Offset: 0x00016685
		internal string LogonUniqueKey
		{
			get
			{
				return this.logonUniqueKey;
			}
			set
			{
				this.logonUniqueKey = value;
				this.keyString = null;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x00018495 File Offset: 0x00016695
		// (set) Token: 0x0600079B RID: 1947 RVA: 0x0001849D File Offset: 0x0001669D
		internal string MailboxUniqueKey
		{
			get
			{
				return this.mailboxUniqueKey;
			}
			set
			{
				this.mailboxUniqueKey = value;
				this.keyString = null;
			}
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x000184B0 File Offset: 0x000166B0
		public override string ToString()
		{
			if (this.keyString == null)
			{
				this.keyString = this.userContextId + ":" + this.logonUniqueKey;
				if (this.mailboxUniqueKey != null)
				{
					this.keyString = this.keyString + ":" + this.mailboxUniqueKey;
				}
			}
			return this.keyString;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0001850C File Offset: 0x0001670C
		internal static bool TryParse(string keyString, out UserContextKey userContextKey)
		{
			ArgumentValidator.ThrowIfNull("keyString", keyString);
			userContextKey = null;
			string[] array = keyString.Split(new char[]
			{
				':'
			});
			if (array.Length < 2 || array.Length > 3)
			{
				return false;
			}
			string text = array[0];
			string text2 = array[1];
			string text3 = null;
			if (array.Length == 3)
			{
				text3 = array[2];
			}
			userContextKey = new UserContextKey(text, text2, text3);
			return true;
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0001856C File Offset: 0x0001676C
		internal static UserContextKey Parse(string keyString)
		{
			ArgumentValidator.ThrowIfNull("keyString", keyString);
			UserContextKey result;
			if (!UserContextKey.TryParse(keyString, out result))
			{
				throw new ArgumentException(string.Format("Invalid UserContextKey string - '{0}'", keyString), "keyString");
			}
			return result;
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x000185A5 File Offset: 0x000167A5
		internal static UserContextKey Create(string userContextId, string logonUniqueKey, string mailboxUniqueKey)
		{
			return new UserContextKey(userContextId, logonUniqueKey, mailboxUniqueKey);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x000185B0 File Offset: 0x000167B0
		internal static UserContextKey CreateFromCookie(UserContextCookie userContextCookie, HttpContext httpContext)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			if (httpContext.User == null)
			{
				throw new ArgumentNullException("httpContext.User");
			}
			if (httpContext.User.Identity == null)
			{
				throw new ArgumentNullException("httpContext.User.Identity");
			}
			SecurityIdentifier securityIdentifier = httpContext.User.Identity.GetSecurityIdentifier();
			if (securityIdentifier == null)
			{
				ExTraceGlobals.UserContextCallTracer.TraceDebug(0L, "UserContextKey.CreateFromCookie: current user has no security identifier.");
				return null;
			}
			string text = securityIdentifier.ToString();
			if (userContextCookie == null)
			{
				throw new ArgumentNullException("userContextCookie");
			}
			return new UserContextKey(userContextCookie.UserContextId, text, userContextCookie.MailboxUniqueKey);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0001864C File Offset: 0x0001684C
		internal static UserContextKey CreateFromCookie(UserContextCookie userContextCookie, SecurityIdentifier sid)
		{
			string text = sid.ToString();
			return new UserContextKey(userContextCookie.UserContextId, text, userContextCookie.MailboxUniqueKey);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00018674 File Offset: 0x00016874
		internal static UserContextKey CreateNew(OwaIdentity logonIdentity, OwaIdentity mailboxIdentity, HttpContext httpContext)
		{
			if (logonIdentity == null)
			{
				throw new ArgumentNullException("logonIdentity");
			}
			string uniqueId = logonIdentity.UniqueId;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("userContextLogonIdentityName=<PII>{0}</PII>", logonIdentity.SafeGetRenderableName());
			if (logonIdentity.UserSid != null)
			{
				stringBuilder.AppendFormat("userContextLogonIdentitySid=<PII>{0}</PII>", logonIdentity.UserSid.ToString());
			}
			string text = null;
			if (mailboxIdentity != null)
			{
				text = mailboxIdentity.UniqueId;
				stringBuilder.AppendFormat("userContextMbIdentityName=<PII>{0}</PII>", mailboxIdentity.SafeGetRenderableName());
				if (mailboxIdentity.UserSid != null)
				{
					stringBuilder.AppendFormat("userContextMbIdentitySid=<PII>{0}</PII>", mailboxIdentity.UserSid.ToString());
				}
			}
			try
			{
				string text2 = stringBuilder.ToString();
				if (LiveIdAuthenticationModule.IdentityTracingEnabled && !string.IsNullOrWhiteSpace(text2))
				{
					httpContext.Response.AppendToLog(text2);
				}
			}
			catch (Exception)
			{
			}
			return UserContextKey.Create(UserContextUtilities.GetNewGuid(), uniqueId, text);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001875C File Offset: 0x0001695C
		internal static UserContextKey CreateNew(SecurityIdentifier sid)
		{
			string text = null;
			string text2 = sid.ToString();
			return UserContextKey.Create(UserContextUtilities.GetNewGuid(), text2, text);
		}

		// Token: 0x04000434 RID: 1076
		private string userContextId;

		// Token: 0x04000435 RID: 1077
		private string logonUniqueKey;

		// Token: 0x04000436 RID: 1078
		private string mailboxUniqueKey;

		// Token: 0x04000437 RID: 1079
		private string keyString;
	}
}
