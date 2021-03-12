using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x02000040 RID: 64
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SessionManager : IDisposable
	{
		// Token: 0x06000258 RID: 600 RVA: 0x0000D998 File Offset: 0x0000BB98
		private SessionManager(SessionManager.SessionData ownerSession)
		{
			this.PrimarySession = ownerSession;
			this.cache = new Dictionary<ADObjectId, SessionManager.SessionData>(100);
			this.cache.Add(ownerSession.Id, ownerSession);
			this.disposeQueue = new Queue<SessionManager.SessionData>(100);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000D9D3 File Offset: 0x0000BBD3
		public SessionManager(MailboxSession ownerSession) : this(new SessionManager.SessionData(ownerSession))
		{
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000D9E1 File Offset: 0x0000BBE1
		public SessionManager(ExchangePrincipal principal, string clientInfoString) : this(SessionManager.OpenSession(principal, null, clientInfoString))
		{
			this.disposeQueue.Enqueue(this.PrimarySession);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000DA04 File Offset: 0x0000BC04
		private static SessionManager.SessionData OpenSession(ExchangePrincipal principal, ExTimeZone timeZone, string clientInfoString)
		{
			MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(principal, CultureInfo.InvariantCulture, clientInfoString);
			if (timeZone != null)
			{
				mailboxSession.ExTimeZone = timeZone;
			}
			return new SessionManager.SessionData(mailboxSession);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000DA30 File Offset: 0x0000BC30
		private SessionManager.SessionData NewSession(ExchangePrincipal principal)
		{
			if (this.disposeQueue.Count >= 100)
			{
				Globals.ValidatorTracer.TraceDebug<int>((long)this.GetHashCode(), "Session manager exceeded the cache limit of {0}", 100);
				SessionManager.SessionData sessionData = this.disposeQueue.Dequeue();
				this.cache.Remove(sessionData.Id);
				sessionData.Dispose();
			}
			SessionManager.SessionData sessionData2 = SessionManager.OpenSession(principal, this.PrimarySession.ExTimeZone, this.PrimarySession.ClientInfoString);
			this.disposeQueue.Enqueue(sessionData2);
			this.cache.Add(principal.ObjectId, sessionData2);
			return sessionData2;
		}

		// Token: 0x170000AC RID: 172
		public SessionManager.SessionData this[ExchangePrincipal principal]
		{
			get
			{
				SessionManager.SessionData result;
				if (this.cache.ContainsKey(principal.ObjectId))
				{
					result = this.cache[principal.ObjectId];
				}
				else
				{
					result = this.NewSession(principal);
				}
				return result;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000DB01 File Offset: 0x0000BD01
		// (set) Token: 0x0600025F RID: 607 RVA: 0x0000DB09 File Offset: 0x0000BD09
		public SessionManager.SessionData PrimarySession { get; private set; }

		// Token: 0x06000260 RID: 608 RVA: 0x0000DB12 File Offset: 0x0000BD12
		void IDisposable.Dispose()
		{
			while (this.disposeQueue.Count != 0)
			{
				this.disposeQueue.Dequeue().Dispose();
			}
		}

		// Token: 0x04000177 RID: 375
		private const int CacheLimit = 100;

		// Token: 0x04000178 RID: 376
		private Dictionary<ADObjectId, SessionManager.SessionData> cache;

		// Token: 0x04000179 RID: 377
		private Queue<SessionManager.SessionData> disposeQueue;

		// Token: 0x02000041 RID: 65
		[ClassAccessLevel(AccessLevel.Implementation)]
		internal class SessionData : IDisposable
		{
			// Token: 0x170000AE RID: 174
			// (get) Token: 0x06000261 RID: 609 RVA: 0x0000DB33 File Offset: 0x0000BD33
			// (set) Token: 0x06000262 RID: 610 RVA: 0x0000DB3B File Offset: 0x0000BD3B
			public MailboxSession Session { get; private set; }

			// Token: 0x170000AF RID: 175
			// (get) Token: 0x06000263 RID: 611 RVA: 0x0000DB44 File Offset: 0x0000BD44
			public ADObjectId Id
			{
				get
				{
					return this.Session.MailboxOwner.ObjectId;
				}
			}

			// Token: 0x170000B0 RID: 176
			// (get) Token: 0x06000264 RID: 612 RVA: 0x0000DB56 File Offset: 0x0000BD56
			public ExTimeZone ExTimeZone
			{
				get
				{
					return this.Session.ExTimeZone;
				}
			}

			// Token: 0x170000B1 RID: 177
			// (get) Token: 0x06000265 RID: 613 RVA: 0x0000DB63 File Offset: 0x0000BD63
			public string ClientInfoString
			{
				get
				{
					return this.Session.ClientInfoString;
				}
			}

			// Token: 0x06000266 RID: 614 RVA: 0x0000DB70 File Offset: 0x0000BD70
			public SessionData(MailboxSession session)
			{
				if (session == null)
				{
					throw new ArgumentNullException("session");
				}
				this.Session = session;
			}

			// Token: 0x06000267 RID: 615 RVA: 0x0000DB8D File Offset: 0x0000BD8D
			public void Dispose()
			{
				this.Session.Dispose();
			}
		}
	}
}
