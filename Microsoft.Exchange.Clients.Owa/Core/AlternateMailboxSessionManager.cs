using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000C1 RID: 193
	internal sealed class AlternateMailboxSessionManager : DisposeTrackableBase
	{
		// Token: 0x0600070B RID: 1803 RVA: 0x0003738B File Offset: 0x0003558B
		internal AlternateMailboxSessionManager(UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			this.userContext = userContext;
			this.alternateMailboxSessions = new Dictionary<Guid, MailboxSession>(5);
			this.exchangePrincipals = new Dictionary<string, ExchangePrincipal>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x000373C4 File Offset: 0x000355C4
		internal ExchangePrincipal GetExchangePrincipal(string mailboxOwnerLegacyDN)
		{
			if (string.IsNullOrEmpty(mailboxOwnerLegacyDN))
			{
				throw new ArgumentNullException("mailboxOwnerLegacyDN");
			}
			if (!Utilities.IsValidLegacyDN(mailboxOwnerLegacyDN))
			{
				throw new ArgumentException("mailboxOwnerLegacyDN is not a valid LegacyDN");
			}
			ExchangePrincipal exchangePrincipal = null;
			ADSessionSettings adSettings = Utilities.CreateScopedADSessionSettings(this.userContext.LogonIdentity.DomainName);
			if (!this.exchangePrincipals.TryGetValue(mailboxOwnerLegacyDN, out exchangePrincipal))
			{
				exchangePrincipal = ExchangePrincipal.FromLegacyDN(adSettings, mailboxOwnerLegacyDN, RemotingOptions.AllowCrossSite | RemotingOptions.AllowCrossPremise);
				if (!exchangePrincipal.MailboxInfo.IsArchive && !exchangePrincipal.MailboxInfo.IsAggregated)
				{
					throw new ArgumentException(string.Format("mailboxOwnerLegacyDN: {0} is not for archive or aggregated mailbox", mailboxOwnerLegacyDN));
				}
				this.exchangePrincipals.Add(mailboxOwnerLegacyDN, exchangePrincipal);
			}
			return exchangePrincipal;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00037464 File Offset: 0x00035664
		internal MailboxSession GetMailboxSession(IExchangePrincipal principal)
		{
			if (principal == null)
			{
				throw new ArgumentNullException("principal");
			}
			MailboxSession mailboxSession = null;
			if (principal.MailboxInfo.IsArchive || principal.MailboxInfo.IsAggregated)
			{
				this.alternateMailboxSessions.TryGetValue(principal.MailboxInfo.MailboxGuid, out mailboxSession);
				if (mailboxSession == null)
				{
					if (this.alternateMailboxSessions.Count == 5)
					{
						Guid key = Guid.Empty;
						using (Dictionary<Guid, MailboxSession>.Enumerator enumerator = this.alternateMailboxSessions.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								KeyValuePair<Guid, MailboxSession> keyValuePair = enumerator.Current;
								key = keyValuePair.Key;
							}
						}
						this.alternateMailboxSessions.Remove(key);
					}
					mailboxSession = this.CreateMailboxSession(principal);
					this.alternateMailboxSessions.Add(principal.MailboxInfo.MailboxGuid, mailboxSession);
				}
				Utilities.ReconnectStoreSession(mailboxSession, this.userContext);
				return mailboxSession;
			}
			throw new ArgumentException("principal is not for archive or alternate mailbox");
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0003755C File Offset: 0x0003575C
		internal void UpdateTimeZoneOnAllSessions(ExTimeZone timeZone)
		{
			foreach (MailboxSession mailboxSession in this.alternateMailboxSessions.Values)
			{
				Utilities.ReconnectStoreSession(mailboxSession, this.userContext);
				mailboxSession.ExTimeZone = timeZone;
			}
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x000375C0 File Offset: 0x000357C0
		internal void DisconnectAllSessions()
		{
			foreach (MailboxSession session in this.alternateMailboxSessions.Values)
			{
				Utilities.DisconnectStoreSession(session);
			}
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00037618 File Offset: 0x00035818
		internal void ClearAllSearchFolders()
		{
			foreach (MailboxSession mailboxSession in this.alternateMailboxSessions.Values)
			{
				Utilities.ReconnectStoreSession(mailboxSession, this.userContext);
				FolderSearch.ClearSearchFolders(mailboxSession);
			}
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0003767C File Offset: 0x0003587C
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing)
			{
				this.ReleaseAllSessions();
			}
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00037687 File Offset: 0x00035887
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AlternateMailboxSessionManager>(this);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00037690 File Offset: 0x00035890
		private MailboxSession CreateMailboxSession(IExchangePrincipal principal)
		{
			if (principal.MailboxInfo.IsArchive && !principal.IsCrossSiteAccessAllowed)
			{
				StackTrace stackTrace = new StackTrace();
				Exception exception = new OwaInvalidOperationException(string.Format("Archive Sessions should be allowed cross site, stack trace {0}", stackTrace.ToString()));
				ExWatson.SendReport(exception, ReportOptions.None, null);
			}
			MailboxSession mailboxSession = this.userContext.LogonIdentity.CreateMailboxSession(principal, Thread.CurrentThread.CurrentCulture, HttpContext.Current.Request);
			mailboxSession.ExTimeZone = this.userContext.MailboxSession.ExTimeZone;
			return mailboxSession;
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00037714 File Offset: 0x00035914
		private void ReleaseAllSessions()
		{
			foreach (MailboxSession mailboxSession in this.alternateMailboxSessions.Values)
			{
				Utilities.DisconnectStoreSession(mailboxSession);
				mailboxSession.Dispose();
			}
			this.alternateMailboxSessions.Clear();
		}

		// Token: 0x040004DD RID: 1245
		private const int AlternateSessionCountMaximum = 5;

		// Token: 0x040004DE RID: 1246
		private Dictionary<Guid, MailboxSession> alternateMailboxSessions;

		// Token: 0x040004DF RID: 1247
		private Dictionary<string, ExchangePrincipal> exchangePrincipals;

		// Token: 0x040004E0 RID: 1248
		private UserContext userContext;
	}
}
