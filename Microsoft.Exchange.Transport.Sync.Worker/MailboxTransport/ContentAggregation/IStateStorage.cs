using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000205 RID: 517
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IStateStorage : INativeStateStorage, ISimpleStateStorage, IDisposeTrackable, IDisposable
	{
		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001166 RID: 4454
		bool IsDirty { get; }

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001167 RID: 4455
		SyncProgress SyncProgress { get; }

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001168 RID: 4456
		bool ForceRecoverySyncNext { get; }

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001169 RID: 4457
		bool InitialSyncDone { get; }

		// Token: 0x0600116A RID: 4458
		Exception Commit(bool commitState, MailboxSession mailboxSession, EventHandler<RoundtripCompleteEventArgs> roundtripComplete);

		// Token: 0x0600116B RID: 4459
		void ReloadForRetry(EventHandler<RoundtripCompleteEventArgs> roundtripComplete);

		// Token: 0x0600116C RID: 4460
		void SetSyncProgress(SyncProgress progress);

		// Token: 0x0600116D RID: 4461
		bool ShouldPromoteItemTransientException(string cloudId, SyncTransientException exception);

		// Token: 0x0600116E RID: 4462
		bool ShouldPromoteItemTransientException(StoreObjectId nativeId, SyncTransientException exception);

		// Token: 0x0600116F RID: 4463
		bool ShouldPromoteFolderTransientException(string cloudId, SyncTransientException exception);

		// Token: 0x06001170 RID: 4464
		bool ShouldPromoteFolderTransientException(StoreObjectId nativeId, SyncTransientException exception);

		// Token: 0x06001171 RID: 4465
		bool TryAddFailedItem(string cloudId, string cloudFolderId);

		// Token: 0x06001172 RID: 4466
		bool TryRemoveFailedItem(string cloudId);

		// Token: 0x06001173 RID: 4467
		bool TryAddFailedFolder(string cloudId, string cloudFolderId);

		// Token: 0x06001174 RID: 4468
		bool TryRemoveFailedFolder(string cloudId);

		// Token: 0x06001175 RID: 4469
		void MarkInitialSyncDone();
	}
}
