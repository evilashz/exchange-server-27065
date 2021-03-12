using System;
using System.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000205 RID: 517
	internal class OwaStoreObjectIdSessionHandle : DisposeTrackableBase
	{
		// Token: 0x06001172 RID: 4466 RVA: 0x0006971C File Offset: 0x0006791C
		private void InitializeDelegateSessionHandle(ExchangePrincipal exchangePrincipal, UserContext userContext)
		{
			try
			{
				this.delegateSessionHandle = userContext.GetDelegateSessionHandle(exchangePrincipal);
			}
			catch (MailboxInSiteFailoverException exception)
			{
				this.HandleOtherMailboxFailover(exception, exchangePrincipal.LegacyDn);
			}
			catch (MailboxCrossSiteFailoverException exception2)
			{
				this.HandleOtherMailboxFailover(exception2, exchangePrincipal.LegacyDn);
			}
			catch (NotSupportedWithServerVersionException innerException)
			{
				throw new OwaSharedFromOlderVersionException(LocalizedStrings.GetHtmlEncoded(1354015881), innerException);
			}
			catch (StoragePermanentException ex)
			{
				string message = string.Format(CultureInfo.InvariantCulture, "OwaStoreObjectIdSessionHandle Ctor. Unable to get GetDelegateSessionHandle with exchange principal from legacy DN {0}. Exception: {1}.", new object[]
				{
					exchangePrincipal.LegacyDn,
					ex.Message
				});
				ExTraceGlobals.CoreCallTracer.TraceDebug((long)this.GetHashCode(), message);
				userContext.DelegateSessionManager.RemoveInvalidExchangePrincipal(exchangePrincipal.LegacyDn);
			}
			catch (StorageTransientException ex2)
			{
				string message2 = string.Format(CultureInfo.InvariantCulture, "OwaStoreObjectIdSessionHandle Ctor. Unable to get GetDelegateSessionHandle with exchange principal from legacy DN {0}. Exception: {1}.", new object[]
				{
					exchangePrincipal.LegacyDn,
					ex2.Message
				});
				ExTraceGlobals.CoreCallTracer.TraceDebug((long)this.GetHashCode(), message2);
				userContext.DelegateSessionManager.RemoveInvalidExchangePrincipal(exchangePrincipal.LegacyDn);
			}
			if (this.delegateSessionHandle == null || this.Session == null)
			{
				throw new ObjectNotFoundException(ServerStrings.ErrorMissingMailboxOrPermission);
			}
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x0006987C File Offset: 0x00067A7C
		internal OwaStoreObjectIdSessionHandle(ExchangePrincipal exchangePrincipal, UserContext userContext)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				if (exchangePrincipal == null)
				{
					throw new ArgumentNullException("exchangePrincipal");
				}
				if (userContext == null)
				{
					throw new ArgumentNullException("userContext");
				}
				if (exchangePrincipal.MailboxInfo.IsArchive)
				{
					throw new ArgumentException("exchangePrincipal is archive mailbox");
				}
				if (exchangePrincipal.MailboxInfo.IsAggregated)
				{
					throw new ArgumentException("exchangePrincipal is aggregated mailbox");
				}
				this.owaStoreObjectId = null;
				this.userContext = userContext;
				this.owaStoreObjectIdType = OwaStoreObjectIdType.OtherUserMailboxObject;
				this.InitializeDelegateSessionHandle(exchangePrincipal, userContext);
				disposeGuard.Success();
			}
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x00069928 File Offset: 0x00067B28
		internal OwaStoreObjectIdSessionHandle(OwaStoreObjectId owaStoreObjectId, UserContext userContext)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				if (owaStoreObjectId == null)
				{
					throw new ArgumentNullException("owaStoreObjectId");
				}
				if (userContext == null)
				{
					throw new ArgumentNullException("userContext");
				}
				this.owaStoreObjectId = owaStoreObjectId;
				this.userContext = userContext;
				this.owaStoreObjectIdType = owaStoreObjectId.OwaStoreObjectIdType;
				if (owaStoreObjectId.OwaStoreObjectIdType == OwaStoreObjectIdType.OtherUserMailboxObject || owaStoreObjectId.OwaStoreObjectIdType == OwaStoreObjectIdType.GSCalendar)
				{
					ExchangePrincipal exchangePrincipal = null;
					if (!userContext.DelegateSessionManager.TryGetExchangePrincipal(owaStoreObjectId.MailboxOwnerLegacyDN, out exchangePrincipal))
					{
						throw new ObjectNotFoundException(ServerStrings.CannotFindExchangePrincipal);
					}
					this.InitializeDelegateSessionHandle(exchangePrincipal, userContext);
				}
				disposeGuard.Success();
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001175 RID: 4469 RVA: 0x000699E0 File Offset: 0x00067BE0
		public StoreSession Session
		{
			get
			{
				this.CheckDisposed("Session::get.");
				return this.GetCorrelatedSession(false);
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x000699F4 File Offset: 0x00067BF4
		public StoreSession SessionForFolderContent
		{
			get
			{
				this.CheckDisposed("SessionForFolderContent::get.");
				return this.GetSessionForFolderContent();
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001177 RID: 4471 RVA: 0x00069A07 File Offset: 0x00067C07
		public OwaStoreObjectIdType HandleType
		{
			get
			{
				return this.owaStoreObjectIdType;
			}
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00069A0F File Offset: 0x00067C0F
		private StoreSession GetSessionForFolderContent()
		{
			if (!Folder.IsFolderId(this.owaStoreObjectId.StoreObjectId))
			{
				throw new OwaNotSupportedException("Get session for folder content is not supported for non folder ids");
			}
			return this.GetCorrelatedSession(true);
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00069A38 File Offset: 0x00067C38
		private StoreSession GetCorrelatedSession(bool requireContentIfFolder)
		{
			switch (this.owaStoreObjectIdType)
			{
			case OwaStoreObjectIdType.MailBoxObject:
				return this.userContext.MailboxSession;
			case OwaStoreObjectIdType.PublicStoreFolder:
				if (requireContentIfFolder)
				{
					return this.userContext.GetContentAvailableSession(this.owaStoreObjectId.StoreObjectId);
				}
				return this.userContext.DefaultPublicFolderSession;
			case OwaStoreObjectIdType.PublicStoreItem:
				return this.userContext.GetContentAvailableSession(this.owaStoreObjectId.ParentFolderId);
			case OwaStoreObjectIdType.Conversation:
				return this.userContext.MailboxSession;
			case OwaStoreObjectIdType.OtherUserMailboxObject:
			case OwaStoreObjectIdType.GSCalendar:
				Utilities.ReconnectStoreSession(this.delegateSessionHandle.MailboxSession, this.userContext);
				return this.delegateSessionHandle.MailboxSession;
			case OwaStoreObjectIdType.ArchiveMailboxObject:
				return this.userContext.GetArchiveMailboxSession(this.owaStoreObjectId.MailboxOwnerLegacyDN);
			case OwaStoreObjectIdType.ArchiveConversation:
				return this.userContext.GetArchiveMailboxSession(this.owaStoreObjectId.MailboxOwnerLegacyDN);
			default:
				return null;
			}
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00069B1D File Offset: 0x00067D1D
		protected override void InternalDispose(bool disposing)
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "OwaStoreObjectIdSessionHandle::InternalDispose");
			if (disposing && !this.isDisposed && this.delegateSessionHandle != null)
			{
				this.delegateSessionHandle.Dispose();
				this.isDisposed = true;
			}
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00069B55 File Offset: 0x00067D55
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OwaStoreObjectIdSessionHandle>(this);
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00069B5D File Offset: 0x00067D5D
		private void CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "OwaStoreObjectIdSessionHandle::CheckDisposed");
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00069B89 File Offset: 0x00067D89
		private void HandleOtherMailboxFailover(Exception exception, string mailboxOwnerLegacyDN)
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug<string, string>((long)this.GetHashCode(), "OwaStoreObjectIdSessionHandle Ctor. Unable to get GetDelegateSessionHandle with exchange principal from legacy DN {0}. Exception: {1}.", mailboxOwnerLegacyDN, exception.Message);
			this.userContext.DelegateSessionManager.RemoveInvalidExchangePrincipal(mailboxOwnerLegacyDN);
			throw new OwaDelegatorMailboxFailoverException(mailboxOwnerLegacyDN, exception);
		}

		// Token: 0x04000BAD RID: 2989
		private OwaStoreObjectId owaStoreObjectId;

		// Token: 0x04000BAE RID: 2990
		private DelegateSessionHandle delegateSessionHandle;

		// Token: 0x04000BAF RID: 2991
		private UserContext userContext;

		// Token: 0x04000BB0 RID: 2992
		private OwaStoreObjectIdType owaStoreObjectIdType;

		// Token: 0x04000BB1 RID: 2993
		private bool isDisposed;
	}
}
