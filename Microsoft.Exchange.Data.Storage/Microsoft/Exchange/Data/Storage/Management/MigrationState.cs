using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A38 RID: 2616
	[Serializable]
	public enum MigrationState
	{
		// Token: 0x0400365C RID: 13916
		[LocDescription(ServerStrings.IDs.MigrationStateActive)]
		Active,
		// Token: 0x0400365D RID: 13917
		[LocDescription(ServerStrings.IDs.MigrationStateWaiting)]
		Waiting,
		// Token: 0x0400365E RID: 13918
		[LocDescription(ServerStrings.IDs.MigrationStateCompleted)]
		Completed,
		// Token: 0x0400365F RID: 13919
		[LocDescription(ServerStrings.IDs.MigrationStateStopped)]
		Stopped,
		// Token: 0x04003660 RID: 13920
		[LocDescription(ServerStrings.IDs.MigrationStateFailed)]
		Failed,
		// Token: 0x04003661 RID: 13921
		[LocDescription(ServerStrings.IDs.MigrationStateCorrupted)]
		Corrupted,
		// Token: 0x04003662 RID: 13922
		[LocDescription(ServerStrings.IDs.MigrationStateDisabled)]
		Disabled
	}
}
