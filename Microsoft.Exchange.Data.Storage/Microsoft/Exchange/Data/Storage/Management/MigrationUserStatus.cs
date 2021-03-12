using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A45 RID: 2629
	[Serializable]
	public enum MigrationUserStatus
	{
		// Token: 0x040036B8 RID: 14008
		[LocDescription(ServerStrings.IDs.MigrationUserStatusQueued)]
		Queued,
		// Token: 0x040036B9 RID: 14009
		[LocDescription(ServerStrings.IDs.MigrationUserStatusSyncing)]
		Syncing,
		// Token: 0x040036BA RID: 14010
		[LocDescription(ServerStrings.IDs.MigrationUserStatusFailed)]
		Failed,
		// Token: 0x040036BB RID: 14011
		[LocDescription(ServerStrings.IDs.MigrationUserStatusSynced)]
		Synced,
		// Token: 0x040036BC RID: 14012
		[LocDescription(ServerStrings.IDs.MigrationUserStatusIncrementalFailed)]
		IncrementalFailed,
		// Token: 0x040036BD RID: 14013
		[LocDescription(ServerStrings.IDs.MigrationUserStatusCompleting)]
		Completing,
		// Token: 0x040036BE RID: 14014
		[LocDescription(ServerStrings.IDs.MigrationUserStatusCompleted)]
		Completed,
		// Token: 0x040036BF RID: 14015
		[LocDescription(ServerStrings.IDs.MigrationUserStatusCompletionFailed)]
		CompletionFailed,
		// Token: 0x040036C0 RID: 14016
		[LocDescription(ServerStrings.IDs.MigrationUserStatusCorrupted)]
		Corrupted,
		// Token: 0x040036C1 RID: 14017
		[LocDescription(ServerStrings.IDs.MigrationUserStatusProvisioning)]
		Provisioning,
		// Token: 0x040036C2 RID: 14018
		[LocDescription(ServerStrings.IDs.MigrationUserStatusProvisionUpdating)]
		ProvisionUpdating,
		// Token: 0x040036C3 RID: 14019
		[LocDescription(ServerStrings.IDs.MigrationUserStatusCompletionSynced)]
		CompletionSynced,
		// Token: 0x040036C4 RID: 14020
		[LocDescription(ServerStrings.IDs.MigrationUserStatusValidating)]
		Validating,
		// Token: 0x040036C5 RID: 14021
		[LocDescription(ServerStrings.IDs.MigrationUserStatusIncrementalSyncing)]
		IncrementalSyncing,
		// Token: 0x040036C6 RID: 14022
		[LocDescription(ServerStrings.IDs.MigrationUserStatusIncrementalSynced)]
		IncrementalSynced,
		// Token: 0x040036C7 RID: 14023
		[LocDescription(ServerStrings.IDs.MigrationUserStatusCompletedWithWarning)]
		CompletedWithWarnings,
		// Token: 0x040036C8 RID: 14024
		[LocDescription(ServerStrings.IDs.MigrationUserStatusStopped)]
		Stopped,
		// Token: 0x040036C9 RID: 14025
		[LocDescription(ServerStrings.IDs.MigrationUserStatusIncrementalStopped)]
		IncrementalStopped,
		// Token: 0x040036CA RID: 14026
		[LocDescription(ServerStrings.IDs.MigrationUserStatusStarting)]
		Starting,
		// Token: 0x040036CB RID: 14027
		[LocDescription(ServerStrings.IDs.MigrationUserStatusStopping)]
		Stopping,
		// Token: 0x040036CC RID: 14028
		[LocDescription(ServerStrings.IDs.MigrationUserStatusRemoving)]
		Removing
	}
}
