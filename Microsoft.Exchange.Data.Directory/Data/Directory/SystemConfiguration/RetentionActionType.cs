using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000415 RID: 1045
	public enum RetentionActionType
	{
		// Token: 0x04001FBD RID: 8125
		[LocDescription(DirectoryStrings.IDs.MoveToDeletedItems)]
		MoveToDeletedItems = 1,
		// Token: 0x04001FBE RID: 8126
		[LocDescription(DirectoryStrings.IDs.MoveToFolder)]
		MoveToFolder,
		// Token: 0x04001FBF RID: 8127
		[LocDescription(DirectoryStrings.IDs.SoftDelete)]
		DeleteAndAllowRecovery,
		// Token: 0x04001FC0 RID: 8128
		[LocDescription(DirectoryStrings.IDs.PermanentlyDelete)]
		PermanentlyDelete,
		// Token: 0x04001FC1 RID: 8129
		[LocDescription(DirectoryStrings.IDs.Tag)]
		MarkAsPastRetentionLimit,
		// Token: 0x04001FC2 RID: 8130
		[LocDescription(DirectoryStrings.IDs.MoveToArchive)]
		MoveToArchive
	}
}
