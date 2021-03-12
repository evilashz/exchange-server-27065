using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000229 RID: 553
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class TransportSyncStorageProviderState : NativeSyncStorageProviderState
	{
		// Token: 0x060013D3 RID: 5075 RVA: 0x0004435A File Offset: 0x0004255A
		internal TransportSyncStorageProviderState(SyncMailboxSession syncMailboxSession, ISyncWorkerData subscription, INativeStateStorage stateStorage, MailSubmitter mailSubmitter, SyncLogSession syncLogSession, bool underRecovery) : base(syncMailboxSession, subscription, stateStorage, syncLogSession, underRecovery)
		{
			this.mailSubmitter = mailSubmitter;
			this.verifiedExistingFolders = new Dictionary<string, string>(3);
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x0004437D File Offset: 0x0004257D
		internal MailSubmitter MailSubmitter
		{
			get
			{
				base.CheckDisposed();
				return this.mailSubmitter;
			}
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x0004438B File Offset: 0x0004258B
		internal override StoreObjectId EnsureInboxFolder(SyncChangeEntry change)
		{
			base.CheckDisposed();
			if (change.SchemaType == SchemaType.Folder)
			{
				return base.EnsureDefaultFolder(DefaultFolderType.Inbox);
			}
			return null;
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x000443A5 File Offset: 0x000425A5
		internal void AddExistingFolder(string cloudId, string folderHexId)
		{
			this.verifiedExistingFolders.Add(cloudId, folderHexId);
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x000443B4 File Offset: 0x000425B4
		internal bool TryGetExistingFolder(string cloudId, out string folderHexId)
		{
			return this.verifiedExistingFolders.TryGetValue(cloudId, out folderHexId);
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x000443C3 File Offset: 0x000425C3
		internal override bool IsInboxFolderId(StoreObjectId itemId)
		{
			base.CheckDisposed();
			return itemId == null || base.IsInboxFolderId(itemId);
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x000443D7 File Offset: 0x000425D7
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x000443D9 File Offset: 0x000425D9
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<TransportSyncStorageProviderState>(this);
		}

		// Token: 0x04000A7D RID: 2685
		private const int EstimatedFolderHexIdEntriesInCache = 3;

		// Token: 0x04000A7E RID: 2686
		private readonly MailSubmitter mailSubmitter;

		// Token: 0x04000A7F RID: 2687
		private readonly Dictionary<string, string> verifiedExistingFolders;
	}
}
