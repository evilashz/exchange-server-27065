using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A1E RID: 2590
	[Serializable]
	public enum MigrationBatchStatus
	{
		// Token: 0x040035A0 RID: 13728
		[LocDescription(ServerStrings.IDs.MigrationBatchStatusCreated)]
		Created,
		// Token: 0x040035A1 RID: 13729
		[LocDescription(ServerStrings.IDs.MigrationBatchStatusSyncing)]
		Syncing,
		// Token: 0x040035A2 RID: 13730
		[LocDescription(ServerStrings.IDs.MigrationBatchStatusStopping)]
		Stopping,
		// Token: 0x040035A3 RID: 13731
		[LocDescription(ServerStrings.IDs.MigrationBatchStatusStopped)]
		Stopped,
		// Token: 0x040035A4 RID: 13732
		[LocDescription(ServerStrings.IDs.MigrationBatchStatusCompleted)]
		Completed,
		// Token: 0x040035A5 RID: 13733
		[LocDescription(ServerStrings.IDs.MigrationBatchStatusFailed)]
		Failed,
		// Token: 0x040035A6 RID: 13734
		[LocDescription(ServerStrings.IDs.MigrationBatchStatusRemoving)]
		Removing,
		// Token: 0x040035A7 RID: 13735
		[LocDescription(ServerStrings.IDs.MigrationBatchStatusSynced)]
		Synced,
		// Token: 0x040035A8 RID: 13736
		[LocDescription(ServerStrings.IDs.MigrationBatchStatusIncrementalSyncing)]
		IncrementalSyncing,
		// Token: 0x040035A9 RID: 13737
		[LocDescription(ServerStrings.IDs.MigrationBatchStatusCompleting)]
		Completing,
		// Token: 0x040035AA RID: 13738
		[LocDescription(ServerStrings.IDs.MigrationBatchStatusCompletedWithErrors)]
		CompletedWithErrors,
		// Token: 0x040035AB RID: 13739
		[LocDescription(ServerStrings.IDs.MigrationBatchStatusSyncedWithErrors)]
		SyncedWithErrors,
		// Token: 0x040035AC RID: 13740
		[LocDescription(ServerStrings.IDs.MigrationBatchStatusCorrupted)]
		Corrupted,
		// Token: 0x040035AD RID: 13741
		[LocDescription(ServerStrings.IDs.MigrationBatchStatusWaiting)]
		Waiting,
		// Token: 0x040035AE RID: 13742
		[LocDescription(ServerStrings.IDs.MigrationBatchStatusStarting)]
		Starting
	}
}
