using System;
using System.Diagnostics;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x02000030 RID: 48
	internal sealed class StoreSessionManager
	{
		// Token: 0x06000185 RID: 389 RVA: 0x0000C014 File Offset: 0x0000A214
		private StoreSessionManager()
		{
			SearchConfig searchConfig = SearchConfig.Instance;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				this.storeSessionCache = new StoreSessionCache(currentProcess.ProcessName, searchConfig.MaxNumberOfMailboxSessions, searchConfig.MaxNumberOfMailboxSessionsPerMailbox, TimeSpan.FromMinutes((double)searchConfig.CacheTimeoutMinutes));
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000C078 File Offset: 0x0000A278
		internal static StoreSessionCache InnerStoreSessionCache
		{
			get
			{
				return StoreSessionManager.instance.storeSessionCache;
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000C084 File Offset: 0x0000A284
		internal static StoreSession GetStoreSessionFromCache(MdbItemIdentity identity, bool localOnly)
		{
			return StoreSessionManager.GetStoreSessionFromCache(identity, false, false, localOnly);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000C090 File Offset: 0x0000A290
		internal static StoreSession GetWritableStoreSessionFromCache(MdbItemIdentity identity, bool isMoveDestination, bool wantSessionForRMS)
		{
			StoreSessionCacheKey cacheKey = new StoreSessionCacheKey(identity.GetMdbGuid(MdbItemIdentity.Location.FastCatalog), identity.MailboxGuid, isMoveDestination);
			return StoreSessionManager.instance.storeSessionCache.GetWritableStoreSession(cacheKey, identity, wantSessionForRMS);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000C0C4 File Offset: 0x0000A2C4
		internal static StoreSession GetStoreSessionFromCache(MdbItemIdentity identity, bool isMoveDestination, bool wantSessionForRMS, bool localOnly)
		{
			StoreSessionCacheKey cacheKey = new StoreSessionCacheKey(identity.GetMdbGuid(MdbItemIdentity.Location.FastCatalog), identity.MailboxGuid, isMoveDestination);
			return StoreSessionManager.instance.storeSessionCache.GetStoreSession(cacheKey, identity, wantSessionForRMS, localOnly);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000C0F8 File Offset: 0x0000A2F8
		internal static void ReturnStoreSessionToCache(ref StoreSession storeSession, bool discard)
		{
			if (storeSession == null)
			{
				return;
			}
			if (!discard)
			{
				StoreSessionManager.instance.storeSessionCache.ReturnStoreSession(ref storeSession);
				return;
			}
			storeSession.Dispose();
			storeSession = null;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000C120 File Offset: 0x0000A320
		internal static bool IsSessionUsableForRMS(StoreSession storeSession)
		{
			return !string.IsNullOrEmpty(storeSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
		}

		// Token: 0x04000109 RID: 265
		private static readonly StoreSessionManager instance = new StoreSessionManager();

		// Token: 0x0400010A RID: 266
		private readonly StoreSessionCache storeSessionCache;
	}
}
