using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004FE RID: 1278
	public enum MRSRequestType
	{
		// Token: 0x040026AA RID: 9898
		Move,
		// Token: 0x040026AB RID: 9899
		Merge,
		// Token: 0x040026AC RID: 9900
		MailboxImport,
		// Token: 0x040026AD RID: 9901
		MailboxExport,
		// Token: 0x040026AE RID: 9902
		MailboxRestore,
		// Token: 0x040026AF RID: 9903
		PublicFolderMove = 6,
		// Token: 0x040026B0 RID: 9904
		PublicFolderMigration,
		// Token: 0x040026B1 RID: 9905
		Sync,
		// Token: 0x040026B2 RID: 9906
		MailboxRelocation,
		// Token: 0x040026B3 RID: 9907
		FolderMove,
		// Token: 0x040026B4 RID: 9908
		PublicFolderMailboxMigration
	}
}
