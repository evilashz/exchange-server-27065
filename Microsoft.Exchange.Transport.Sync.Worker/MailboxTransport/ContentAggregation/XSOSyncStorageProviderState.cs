using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x0200022E RID: 558
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class XSOSyncStorageProviderState : NativeSyncStorageProviderState, ISyncSourceSession
	{
		// Token: 0x06001412 RID: 5138 RVA: 0x000496DF File Offset: 0x000478DF
		internal XSOSyncStorageProviderState(SyncMailboxSession syncMailboxSession, ISyncWorkerData subscription, INativeStateStorage stateStorage, SyncLogSession syncLogSession, bool underRecovery) : base(syncMailboxSession, subscription, stateStorage, syncLogSession, underRecovery)
		{
			this.bulkAutomaticLink = new BulkAutomaticLink(syncMailboxSession.MailboxSession);
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x000496FF File Offset: 0x000478FF
		string ISyncSourceSession.Protocol
		{
			get
			{
				base.CheckDisposed();
				return "LocalExchange";
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x0004970C File Offset: 0x0004790C
		string ISyncSourceSession.SessionId
		{
			get
			{
				base.CheckDisposed();
				return string.Empty;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x00049719 File Offset: 0x00047919
		string ISyncSourceSession.Server
		{
			get
			{
				base.CheckDisposed();
				return string.Empty;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x00049726 File Offset: 0x00047926
		internal BulkAutomaticLink BulkAutomaticLink
		{
			get
			{
				base.CheckDisposed();
				return this.bulkAutomaticLink;
			}
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x00049734 File Offset: 0x00047934
		internal override StoreObjectId EnsureInboxFolder(SyncChangeEntry change)
		{
			base.CheckDisposed();
			return base.EnsureDefaultFolder(DefaultFolderType.Inbox);
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x00049743 File Offset: 0x00047943
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.bulkAutomaticLink != null)
			{
				this.bulkAutomaticLink.Dispose();
				this.bulkAutomaticLink = null;
			}
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x00049762 File Offset: 0x00047962
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<XSOSyncStorageProviderState>(this);
		}

		// Token: 0x04000A87 RID: 2695
		private const string XSOComponentId = "LocalExchange";

		// Token: 0x04000A88 RID: 2696
		private BulkAutomaticLink bulkAutomaticLink;
	}
}
