using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x0200001F RID: 31
	internal sealed class MailboxSessionManager
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x000061E0 File Offset: 0x000043E0
		private MailboxSessionManager()
		{
			string instanceName = null;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				instanceName = currentProcess.ProcessName;
			}
			this.mailboxSessionCache = new MailboxSessionCache(instanceName, MailboxSessionManager.MaxNumberOfMailboxSessions, MailboxSessionManager.MaxNumberOfMailboxSessionsPerMailbox, MailboxSessionManager.CacheTimeoutMinutes);
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000FA RID: 250 RVA: 0x0000623C File Offset: 0x0000443C
		internal static MailboxSessionCache InnerMailboxSessionCache
		{
			get
			{
				return MailboxSessionManager.instance.mailboxSessionCache;
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00006248 File Offset: 0x00004448
		internal static MailboxSession GetUserMailboxSessionFromCache(ExchangePrincipal principal)
		{
			MailboxSessionCacheKey cacheKey = new MailboxSessionCacheKey(principal);
			return MailboxSessionManager.instance.mailboxSessionCache.GetMailboxSession(cacheKey, principal);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000626D File Offset: 0x0000446D
		internal static void ReturnMailboxSessionToCache(ref MailboxSession mailboxSession, bool discard)
		{
			if (!discard)
			{
				MailboxSessionManager.instance.mailboxSessionCache.ReturnMailboxSession(ref mailboxSession);
				return;
			}
			mailboxSession.Dispose();
			mailboxSession = null;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000628D File Offset: 0x0000448D
		internal static MailboxSession CreateMailboxSession(ExchangePrincipal principal)
		{
			if (principal == null)
			{
				throw new ArgumentException("Principal is null");
			}
			return MailboxSession.OpenAsSystemService(principal, CultureInfo.InvariantCulture, "Client=Management;Action=AdminLog");
		}

		// Token: 0x04000088 RID: 136
		private const string ClientInfoString = "Client=Management;Action=AdminLog";

		// Token: 0x04000089 RID: 137
		private static readonly int MaxNumberOfMailboxSessions = AdminAuditSettings.Instance.SessionCacheSize;

		// Token: 0x0400008A RID: 138
		private static readonly int MaxNumberOfMailboxSessionsPerMailbox = AdminAuditSettings.Instance.MaxNumberOfMailboxSessionsPerMailbox;

		// Token: 0x0400008B RID: 139
		private static readonly TimeSpan CacheTimeoutMinutes = AdminAuditSettings.Instance.SessionExpirationTime;

		// Token: 0x0400008C RID: 140
		private static readonly MailboxSessionManager instance = new MailboxSessionManager();

		// Token: 0x0400008D RID: 141
		private readonly MailboxSessionCache mailboxSessionCache;
	}
}
