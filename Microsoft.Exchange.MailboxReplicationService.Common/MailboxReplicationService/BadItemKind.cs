using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000CE RID: 206
	public enum BadItemKind
	{
		// Token: 0x040004AA RID: 1194
		MissingItem,
		// Token: 0x040004AB RID: 1195
		CorruptItem,
		// Token: 0x040004AC RID: 1196
		LargeItem,
		// Token: 0x040004AD RID: 1197
		CorruptSearchFolderCriteria,
		// Token: 0x040004AE RID: 1198
		CorruptFolderACL,
		// Token: 0x040004AF RID: 1199
		CorruptFolderRule,
		// Token: 0x040004B0 RID: 1200
		MissingFolder,
		// Token: 0x040004B1 RID: 1201
		MisplacedFolder,
		// Token: 0x040004B2 RID: 1202
		CorruptFolderProperty,
		// Token: 0x040004B3 RID: 1203
		CorruptFolderRestriction,
		// Token: 0x040004B4 RID: 1204
		CorruptInferenceProperties,
		// Token: 0x040004B5 RID: 1205
		CorruptMailboxSetting,
		// Token: 0x040004B6 RID: 1206
		FolderPropertyMismatch
	}
}
