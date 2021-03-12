using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009CD RID: 2509
	internal class InstantSearchManager : DisposeTrackableBase
	{
		// Token: 0x060046ED RID: 18157 RVA: 0x000FC4AB File Offset: 0x000FA6AB
		public InstantSearchManager(Func<MailboxSession> createMailboxSession)
		{
			this.createMailboxSession = createMailboxSession;
		}

		// Token: 0x060046EE RID: 18158 RVA: 0x000FC4C8 File Offset: 0x000FA6C8
		public InstantSearchSession GetOrCreateSession(string sessionId, List<StoreId> folderIds, InstantSearchItemType itemType, long searchRequestId, SuggestionSourceType suggestionSource, out bool isNewSession)
		{
			isNewSession = false;
			InstantSearchSession result;
			lock (this.syncLock)
			{
				if (this.isDisposed)
				{
					result = null;
				}
				else if (this.currentSession == null)
				{
					this.CreateNewInstantSearchSession(sessionId, folderIds, itemType, searchRequestId, suggestionSource);
					isNewSession = true;
					result = this.currentSession;
				}
				else if (this.currentSession.SessionId == sessionId)
				{
					result = this.currentSession;
				}
				else
				{
					if (searchRequestId < this.currentSessionCreationRequestId)
					{
						throw new InstantSearchSessionExpiredException();
					}
					this.EndSearchSession(this.currentSession.SessionId, searchRequestId);
					this.CreateNewInstantSearchSession(sessionId, folderIds, itemType, searchRequestId, suggestionSource);
					isNewSession = true;
					result = this.currentSession;
				}
			}
			return result;
		}

		// Token: 0x060046EF RID: 18159 RVA: 0x000FC5A0 File Offset: 0x000FA7A0
		public EndInstantSearchSessionResponse EndSearchSession(string sessionId)
		{
			EndInstantSearchSessionResponse result;
			lock (this.syncLock)
			{
				result = this.EndSearchSession(sessionId, -1L);
			}
			return result;
		}

		// Token: 0x060046F0 RID: 18160 RVA: 0x000FC5E8 File Offset: 0x000FA7E8
		protected override void InternalDispose(bool isDisposing)
		{
			if (this.isDisposed)
			{
				return;
			}
			MailboxSession mailboxSession = null;
			try
			{
				lock (this.syncLock)
				{
					if (!this.isDisposed)
					{
						this.isDisposed = true;
						if (this.MailboxData != null)
						{
							mailboxSession = this.MailboxData.mailboxSession;
						}
						if (this.currentSession != null)
						{
							this.currentSession.BeginStopSession(-2L);
							this.currentSession = null;
						}
						this.MailboxData = null;
					}
				}
			}
			finally
			{
				if (mailboxSession != null)
				{
					lock (mailboxSession)
					{
						mailboxSession.Dispose();
					}
				}
			}
		}

		// Token: 0x060046F1 RID: 18161 RVA: 0x000FC6C4 File Offset: 0x000FA8C4
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<InstantSearchManager>(this);
		}

		// Token: 0x060046F2 RID: 18162 RVA: 0x000FC6CC File Offset: 0x000FA8CC
		private void CreateNewInstantSearchSession(string sessionId, List<StoreId> folderIds, InstantSearchItemType itemType, long searchRequestId, SuggestionSourceType suggestionSourceType)
		{
			if (this.MailboxData == null)
			{
				MailboxSession mailboxSession = this.createMailboxSession();
				this.MailboxData = new InstantSearchMailboxDataSnapshot(mailboxSession);
			}
			this.currentSessionCreationRequestId = searchRequestId;
			InstantSearchSession instantSearchSession = new InstantSearchSession(sessionId, this.MailboxData, folderIds, itemType, suggestionSourceType);
			instantSearchSession.BeginStartSession(searchRequestId);
			this.currentSession = instantSearchSession;
		}

		// Token: 0x060046F3 RID: 18163 RVA: 0x000FC724 File Offset: 0x000FA924
		private EndInstantSearchSessionResponse EndSearchSession(string sessionId, long searchRequestId)
		{
			EndInstantSearchSessionResponse result = null;
			if (this.currentSession != null && this.currentSession.SessionId == sessionId)
			{
				result = this.currentSession.BeginStopSession(searchRequestId);
				this.currentSession = null;
				this.currentSessionCreationRequestId = -1L;
			}
			return result;
		}

		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x060046F4 RID: 18164 RVA: 0x000FC773 File Offset: 0x000FA973
		// (set) Token: 0x060046F5 RID: 18165 RVA: 0x000FC77B File Offset: 0x000FA97B
		public InstantSearchMailboxDataSnapshot MailboxData { get; set; }

		// Token: 0x040028BA RID: 10426
		private readonly object syncLock = new object();

		// Token: 0x040028BB RID: 10427
		private readonly Func<MailboxSession> createMailboxSession;

		// Token: 0x040028BC RID: 10428
		private volatile bool isDisposed;

		// Token: 0x040028BD RID: 10429
		private volatile InstantSearchSession currentSession;

		// Token: 0x040028BE RID: 10430
		private long currentSessionCreationRequestId;
	}
}
