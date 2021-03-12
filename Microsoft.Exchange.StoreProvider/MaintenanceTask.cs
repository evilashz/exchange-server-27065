using System;

namespace Microsoft.Mapi
{
	// Token: 0x0200003C RID: 60
	internal enum MaintenanceTask
	{
		// Token: 0x040003D0 RID: 976
		FolderTombstones = 1,
		// Token: 0x040003D1 RID: 977
		FolderConflictAging,
		// Token: 0x040003D2 RID: 978
		SiteFolderCheck,
		// Token: 0x040003D3 RID: 979
		EventHistoryCleanup,
		// Token: 0x040003D4 RID: 980
		TombstonesMaintenance,
		// Token: 0x040003D5 RID: 981
		PurgeIndices,
		// Token: 0x040003D6 RID: 982
		PFExpiry,
		// Token: 0x040003D7 RID: 983
		UpdateServerVersions,
		// Token: 0x040003D8 RID: 984
		HardDeletes,
		// Token: 0x040003D9 RID: 985
		DeletedMailboxCleanup,
		// Token: 0x040003DA RID: 986
		ReReadMDBQuotas,
		// Token: 0x040003DB RID: 987
		ReReadAuditInfo,
		// Token: 0x040003DC RID: 988
		InvCachedDsInfo,
		// Token: 0x040003DD RID: 989
		DbSizeCheck,
		// Token: 0x040003DE RID: 990
		DeliveredToCleanup,
		// Token: 0x040003DF RID: 991
		FolderCleanup,
		// Token: 0x040003E0 RID: 992
		AgeOutAllFolders,
		// Token: 0x040003E1 RID: 993
		AgeOutAllViews,
		// Token: 0x040003E2 RID: 994
		AgeOutAllDVUEntries,
		// Token: 0x040003E3 RID: 995
		MdbHealthCheck,
		// Token: 0x040003E4 RID: 996
		QuarantinedMailboxCleanup,
		// Token: 0x040003E5 RID: 997
		RequestTimeoutTest,
		// Token: 0x040003E6 RID: 998
		DeletedCiFailedItemCleanup,
		// Token: 0x040003E7 RID: 999
		ISINTEGProvisionedFolder
	}
}
