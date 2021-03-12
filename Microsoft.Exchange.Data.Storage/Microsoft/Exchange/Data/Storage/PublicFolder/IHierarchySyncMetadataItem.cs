using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.Data.Storage.PublicFolder
{
	// Token: 0x02000939 RID: 2361
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IHierarchySyncMetadataItem : IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x17001864 RID: 6244
		// (get) Token: 0x060057E7 RID: 22503
		// (set) Token: 0x060057E8 RID: 22504
		ExDateTime LastAttemptedSyncTime { get; set; }

		// Token: 0x17001865 RID: 6245
		// (get) Token: 0x060057E9 RID: 22505
		// (set) Token: 0x060057EA RID: 22506
		ExDateTime LastFailedSyncTime { get; set; }

		// Token: 0x17001866 RID: 6246
		// (get) Token: 0x060057EB RID: 22507
		// (set) Token: 0x060057EC RID: 22508
		ExDateTime LastSuccessfulSyncTime { get; set; }

		// Token: 0x17001867 RID: 6247
		// (get) Token: 0x060057ED RID: 22509
		// (set) Token: 0x060057EE RID: 22510
		ExDateTime FirstFailedSyncTimeAfterLastSuccess { get; set; }

		// Token: 0x17001868 RID: 6248
		// (get) Token: 0x060057EF RID: 22511
		// (set) Token: 0x060057F0 RID: 22512
		string LastSyncFailure { get; set; }

		// Token: 0x17001869 RID: 6249
		// (get) Token: 0x060057F1 RID: 22513
		// (set) Token: 0x060057F2 RID: 22514
		int NumberOfAttemptsAfterLastSuccess { get; set; }

		// Token: 0x1700186A RID: 6250
		// (get) Token: 0x060057F3 RID: 22515
		// (set) Token: 0x060057F4 RID: 22516
		int NumberOfBatchesExecuted { get; set; }

		// Token: 0x1700186B RID: 6251
		// (get) Token: 0x060057F5 RID: 22517
		// (set) Token: 0x060057F6 RID: 22518
		int NumberOfFoldersSynced { get; set; }

		// Token: 0x1700186C RID: 6252
		// (get) Token: 0x060057F7 RID: 22519
		// (set) Token: 0x060057F8 RID: 22520
		int NumberOfFoldersToBeSynced { get; set; }

		// Token: 0x1700186D RID: 6253
		// (get) Token: 0x060057F9 RID: 22521
		// (set) Token: 0x060057FA RID: 22522
		int BatchSize { get; set; }

		// Token: 0x060057FB RID: 22523
		void SetPartiallyCommittedFolderIds(IdSet value);

		// Token: 0x060057FC RID: 22524
		IdSet GetPartiallyCommittedFolderIds();

		// Token: 0x060057FD RID: 22525
		Stream GetSyncStateReadStream();

		// Token: 0x060057FE RID: 22526
		Stream GetSyncStateOverrideStream();

		// Token: 0x060057FF RID: 22527
		Stream GetFinalJobSyncStateReadStream();

		// Token: 0x06005800 RID: 22528
		Stream GetFinalJobSyncStateWriteStream(bool overrideIfExisting);

		// Token: 0x06005801 RID: 22529
		void CommitSyncStateForCompletedBatch();

		// Token: 0x06005802 RID: 22530
		void ClearSyncState();
	}
}
