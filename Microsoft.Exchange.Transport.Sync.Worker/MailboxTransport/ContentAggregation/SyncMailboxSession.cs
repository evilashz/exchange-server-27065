using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;
using Microsoft.Exchange.Transport.Sync.Worker.Throttling;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000219 RID: 537
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SyncMailboxSession : DisposeTrackableBase
	{
		// Token: 0x0600132D RID: 4909 RVA: 0x000412EE File Offset: 0x0003F4EE
		public SyncMailboxSession(SyncLogSession syncLogSession)
		{
			this.storeWasRestartedAtLeastOnce = false;
			this.isMailboxSessionSet = false;
			this.syncLogSession = syncLogSession;
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0004130B File Offset: 0x0003F50B
		internal SyncMailboxSession(MailboxSession mailboxSession, SyncLogSession syncLogSession) : this(syncLogSession)
		{
			SyncUtilities.ThrowIfArgumentNull("mailboxSession", mailboxSession);
			this.mailboxSession = mailboxSession;
			this.isMailboxSessionSet = true;
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x0600132F RID: 4911 RVA: 0x0004132D File Offset: 0x0003F52D
		public MailboxSession MailboxSession
		{
			get
			{
				base.CheckDisposed();
				return this.mailboxSession;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001330 RID: 4912 RVA: 0x0004133B File Offset: 0x0003F53B
		public bool WasMailboxSessionOpened
		{
			get
			{
				base.CheckDisposed();
				return this.isMailboxSessionSet;
			}
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x00041358 File Offset: 0x0003F558
		public bool TryOpen(string legacyDN, Guid mailboxGuid, Guid databaseGuid, string mailboxServer, out OrganizationId organizationId, out ISyncException exception, out bool invalidState)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("legacyDN", legacyDN);
			SyncUtilities.ThrowIfGuidEmpty("mailboxGuid", mailboxGuid);
			SyncUtilities.ThrowIfGuidEmpty("databaseGuid", databaseGuid);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("mailboxServer", mailboxServer);
			if (this.MailboxSession != null)
			{
				organizationId = this.organizationId;
				exception = null;
				invalidState = false;
				return true;
			}
			Exception ex = null;
			try
			{
				exception = null;
				invalidState = false;
				MailboxSession mailboxSession = SyncUtilities.OpenMailboxSessionAndHaveCompleteExchangePrincipal(mailboxGuid, databaseGuid, (IExchangePrincipal exchangePrincipal) => SubscriptionManager.OpenMailbox(exchangePrincipal, ExchangeMailboxOpenType.AsTransport, SyncUtilities.WorkerClientInfoString));
				SyncStoreLoadManager.Instance.EnableLoadTrackingOnSession(mailboxSession);
				this.organizationId = mailboxSession.MailboxOwner.MailboxInfo.OrganizationId;
				this.SetMailboxSession(mailboxSession);
				organizationId = this.organizationId;
				return true;
			}
			catch (ObjectNotFoundException ex2)
			{
				ex = ex2;
			}
			catch (MailboxUnavailableException ex3)
			{
				ex = ex3;
			}
			this.syncLogSession.LogError((TSLID)306UL, "Did not find mailbox when trying to open it. Error:{0}", new object[]
			{
				ex
			});
			exception = SyncPermanentException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, new SubscriptionSyncException(null, ex));
			organizationId = null;
			invalidState = true;
			return false;
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x00041480 File Offset: 0x0003F680
		public void SetMailboxSession(MailboxSession toWrap)
		{
			base.CheckDisposed();
			SyncUtilities.ThrowIfArgumentNull("toWrap", toWrap);
			this.mailboxSession = toWrap;
			this.isMailboxSessionSet = true;
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x000414A1 File Offset: 0x0003F6A1
		public void SetOrganizationId(OrganizationId organizationId)
		{
			base.CheckDisposed();
			SyncUtilities.ThrowIfArgumentNull("organizationId", organizationId);
			this.organizationId = organizationId;
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x000414BC File Offset: 0x0003F6BC
		public bool EnsureConnectWithStatus()
		{
			base.CheckDisposed();
			this.EnsureMailboxSessionSet();
			if (this.mailboxSession.IsConnected)
			{
				this.syncLogSession.LogDebugging((TSLID)6UL, "EnsureConnectWithStatus::Mailbox session is already connected, storeWasRestartedAtLeastOnce:{0}", new object[]
				{
					this.storeWasRestartedAtLeastOnce
				});
			}
			else
			{
				bool flag = this.mailboxSession.ConnectWithStatus();
				if (flag)
				{
					this.storeWasRestartedAtLeastOnce = true;
				}
				this.syncLogSession.LogDebugging((TSLID)7UL, "EnsureConnectWithStatus::Just reconnected mailbox session, storeWasRestartedInLatestReconnect:{0}, storeWasRestartedAtLeastOnce: {1}", new object[]
				{
					flag,
					this.storeWasRestartedAtLeastOnce
				});
			}
			return this.storeWasRestartedAtLeastOnce;
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x00041563 File Offset: 0x0003F763
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.mailboxSession != null)
			{
				this.mailboxSession.Dispose();
				this.mailboxSession = null;
			}
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x00041582 File Offset: 0x0003F782
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SyncMailboxSession>(this);
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0004158A File Offset: 0x0003F78A
		private void EnsureMailboxSessionSet()
		{
		}

		// Token: 0x04000A1E RID: 2590
		private readonly SyncLogSession syncLogSession;

		// Token: 0x04000A1F RID: 2591
		private MailboxSession mailboxSession;

		// Token: 0x04000A20 RID: 2592
		private OrganizationId organizationId;

		// Token: 0x04000A21 RID: 2593
		private bool storeWasRestartedAtLeastOnce;

		// Token: 0x04000A22 RID: 2594
		private bool isMailboxSessionSet;
	}
}
