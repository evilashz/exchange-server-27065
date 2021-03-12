using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A46 RID: 2630
	[Serializable]
	public enum MigrationUserStatusSummary
	{
		// Token: 0x040036CE RID: 14030
		[LocDescription(ServerStrings.IDs.MigrationUserStatusSummaryActive)]
		Active = 1,
		// Token: 0x040036CF RID: 14031
		[LocDescription(ServerStrings.IDs.MigrationUserStatusSummaryFailed)]
		Failed,
		// Token: 0x040036D0 RID: 14032
		[LocDescription(ServerStrings.IDs.MigrationUserStatusSummarySynced)]
		Synced,
		// Token: 0x040036D1 RID: 14033
		[LocDescription(ServerStrings.IDs.MigrationUserStatusSummaryCompleted)]
		Completed,
		// Token: 0x040036D2 RID: 14034
		[LocDescription(ServerStrings.IDs.MigrationUserStatusSummaryStopped)]
		Stopped
	}
}
