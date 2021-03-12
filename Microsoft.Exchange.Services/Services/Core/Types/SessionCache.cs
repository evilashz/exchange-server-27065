using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000889 RID: 2185
	internal sealed class SessionCache : IDisposable
	{
		// Token: 0x06003E97 RID: 16023 RVA: 0x000D9029 File Offset: 0x000D7229
		internal SessionCache(AppWideStoreSessionCache storeSessionBackingCache, CallContext callContext)
		{
			this.callContext = callContext;
			if (callContext.IsExternalUser)
			{
				this.storeSessionCache = new MethodWideStoreSessionCache(null, callContext);
				return;
			}
			this.storeSessionCache = new MethodWideStoreSessionCache(storeSessionBackingCache, callContext);
		}

		// Token: 0x06003E98 RID: 16024 RVA: 0x000D905B File Offset: 0x000D725B
		internal MailboxSession GetMailboxSessionBySmtpAddress(string smtpAddress)
		{
			return this.storeSessionCache.GetCachedMailboxSessionBySmtpAddress(smtpAddress, false);
		}

		// Token: 0x06003E99 RID: 16025 RVA: 0x000D906A File Offset: 0x000D726A
		internal MailboxSession GetMailboxSessionBySmtpAddress(string smtpAddress, bool archiveMailbox)
		{
			return this.storeSessionCache.GetCachedMailboxSessionBySmtpAddress(smtpAddress, archiveMailbox);
		}

		// Token: 0x06003E9A RID: 16026 RVA: 0x000D9079 File Offset: 0x000D7279
		internal MailboxSession GetMailboxSessionByMailboxId(MailboxId mailboxId)
		{
			return this.GetMailboxSessionByMailboxId(mailboxId, false);
		}

		// Token: 0x06003E9B RID: 16027 RVA: 0x000D9083 File Offset: 0x000D7283
		internal MailboxSession GetMailboxSessionByMailboxId(MailboxId mailboxId, bool unifiedLogon)
		{
			return this.storeSessionCache.GetCachedMailboxSession(mailboxId, unifiedLogon) as MailboxSession;
		}

		// Token: 0x06003E9C RID: 16028 RVA: 0x000D9097 File Offset: 0x000D7297
		internal MailboxSession GetMailboxIdentityMailboxSession()
		{
			return this.GetMailboxIdentityMailboxSession(false);
		}

		// Token: 0x06003E9D RID: 16029 RVA: 0x000D90A0 File Offset: 0x000D72A0
		internal MailboxSession GetMailboxIdentityMailboxSession(bool archiveMailbox)
		{
			return this.storeSessionCache.GetCachedMailboxSessionBySmtpAddress(string.IsNullOrEmpty(this.callContext.OwaExplicitLogonUser) ? null : this.callContext.OwaExplicitLogonUser, archiveMailbox);
		}

		// Token: 0x06003E9E RID: 16030 RVA: 0x000D90CE File Offset: 0x000D72CE
		internal SessionAndAuthZ GetSessionAndAuthZ(Guid mailboxGuid, bool unifiedLogon)
		{
			return this.storeSessionCache.GetSessionAndAuthZ(mailboxGuid, unifiedLogon);
		}

		// Token: 0x06003E9F RID: 16031 RVA: 0x000D90DD File Offset: 0x000D72DD
		internal SessionAndAuthZ GetSystemMailboxSessionAndAuthZ(MailboxId mailboxId)
		{
			return this.GetSystemMailboxSessionAndAuthZ(mailboxId, false);
		}

		// Token: 0x06003EA0 RID: 16032 RVA: 0x000D90E8 File Offset: 0x000D72E8
		internal SessionAndAuthZ GetSystemMailboxSessionAndAuthZ(MailboxId mailboxId, bool unifiedLogon)
		{
			if (mailboxId.MailboxGuid != null)
			{
				return this.storeSessionCache.GetCachedSystemMailboxSessionByMailboxGuid(new Guid(mailboxId.MailboxGuid), unifiedLogon);
			}
			ExTraceGlobals.SessionCacheTracer.TraceError<string>((long)this.GetHashCode(), "SessionCache.GetSystemMailboxSessionAndAuthZ. MailboxId for external user doesn't contain MailboxGuid. MailboxId.SmtpAddress: {0}", mailboxId.SmtpAddress ?? "<Null>");
			throw new ServiceAccessDeniedException();
		}

		// Token: 0x06003EA1 RID: 16033 RVA: 0x000D913F File Offset: 0x000D733F
		internal PublicFolderSession GetPublicFolderSession(Guid publicFolderMailboxGuid)
		{
			this.VerifyAccessingPrincipalIsNotNull();
			return this.storeSessionCache.GetCachedStoreSessionByMailboxGuid(publicFolderMailboxGuid) as PublicFolderSession;
		}

		// Token: 0x06003EA2 RID: 16034 RVA: 0x000D9158 File Offset: 0x000D7358
		internal PublicFolderSession GetPublicFolderSession(StoreId folderId)
		{
			this.VerifyAccessingPrincipalIsNotNull();
			return this.storeSessionCache.GetCachedPublicFolderSessionByStoreId(folderId) as PublicFolderSession;
		}

		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x06003EA3 RID: 16035 RVA: 0x000D9171 File Offset: 0x000D7371
		// (set) Token: 0x06003EA4 RID: 16036 RVA: 0x000D917E File Offset: 0x000D737E
		internal bool ReturnStoreSessionsToCache
		{
			get
			{
				return this.storeSessionCache.ReturnStoreSessionsToCache;
			}
			set
			{
				this.storeSessionCache.ReturnStoreSessionsToCache = value;
			}
		}

		// Token: 0x06003EA5 RID: 16037 RVA: 0x000D918C File Offset: 0x000D738C
		private void VerifyAccessingPrincipalIsNotNull()
		{
			if (this.callContext.AccessingPrincipal == null)
			{
				ExTraceGlobals.SessionCacheTracer.TraceDebug((long)this.GetHashCode(), "[SessionCache::VerifyAccessingPrincipalIsNotNull] Cannot obtain the organizationId whenwhen the accessing principal does not represent an Exchange Principal. (DCR E12:120711)");
				throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorPublicFolderUserMustHaveMailbox);
			}
		}

		// Token: 0x06003EA6 RID: 16038 RVA: 0x000D91C4 File Offset: 0x000D73C4
		public void Dispose()
		{
			ExTraceGlobals.SessionCacheTracer.TraceDebug<bool>((long)this.GetHashCode(), "SessionCache.Dispose() called, isDisposed: {0}", this.isDisposed);
			if (!this.isDisposed)
			{
				this.storeSessionCache.Dispose();
				this.isDisposed = true;
				ExTraceGlobals.SessionCacheTracer.TraceDebug((long)this.GetHashCode(), "SessionCache.Dispose(), Cache disposed.");
			}
		}

		// Token: 0x040023ED RID: 9197
		private bool isDisposed;

		// Token: 0x040023EE RID: 9198
		private MethodWideStoreSessionCache storeSessionCache;

		// Token: 0x040023EF RID: 9199
		private CallContext callContext;
	}
}
