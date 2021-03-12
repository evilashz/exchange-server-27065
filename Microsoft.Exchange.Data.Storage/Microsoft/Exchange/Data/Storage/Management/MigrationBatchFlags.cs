using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A1B RID: 2587
	[Flags]
	[Serializable]
	public enum MigrationBatchFlags
	{
		// Token: 0x040034F1 RID: 13553
		[LocDescription(ServerStrings.IDs.MigrationBatchFlagNone)]
		None = 0,
		// Token: 0x040034F2 RID: 13554
		[LocDescription(ServerStrings.IDs.MigrationBatchFlagDisallowExistingUsers)]
		DisallowExistingUsers = 1,
		// Token: 0x040034F3 RID: 13555
		[LocDescription(ServerStrings.IDs.MigrationBatchFlagForceNewMigration)]
		ForceNewMigration = 2,
		// Token: 0x040034F4 RID: 13556
		[LocDescription(ServerStrings.IDs.MigrationBatchFlagUseAdvancedValidation)]
		UseAdvancedValidation = 4,
		// Token: 0x040034F5 RID: 13557
		[LocDescription(ServerStrings.IDs.MigrationBatchAutoComplete)]
		AutoComplete = 8,
		// Token: 0x040034F6 RID: 13558
		[LocDescription(ServerStrings.IDs.MigrationBatchFlagAutoStop)]
		AutoStop = 16,
		// Token: 0x040034F7 RID: 13559
		[LocDescription(ServerStrings.IDs.MigrationBatchFlagDisableOnCopy)]
		DisableOnCopy = 32,
		// Token: 0x040034F8 RID: 13560
		[LocDescription(ServerStrings.IDs.MigrationBatchFlagReportInitial)]
		ReportInitial = 64
	}
}
