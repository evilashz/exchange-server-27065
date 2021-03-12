using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A2B RID: 2603
	[Serializable]
	public enum MigrationFolderName
	{
		// Token: 0x04003628 RID: 13864
		[LocDescription(ServerStrings.IDs.MigrationFolderSyncMigration)]
		SyncMigration,
		// Token: 0x04003629 RID: 13865
		[LocDescription(ServerStrings.IDs.MigrationFolderSyncMigrationReports)]
		SyncMigrationReports,
		// Token: 0x0400362A RID: 13866
		[LocDescription(ServerStrings.IDs.MigrationFolderCorruptedItems)]
		CorruptedItems,
		// Token: 0x0400362B RID: 13867
		[LocDescription(ServerStrings.IDs.MigrationFolderSettings)]
		Settings,
		// Token: 0x0400362C RID: 13868
		[LocDescription(ServerStrings.IDs.MigrationFolderDrumTesting)]
		DrumTesting
	}
}
