using System;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C78 RID: 3192
	public enum SkippableMoveComponent
	{
		// Token: 0x04003C84 RID: 15492
		FolderRules,
		// Token: 0x04003C85 RID: 15493
		FolderACLs,
		// Token: 0x04003C86 RID: 15494
		FolderPromotedProperties,
		// Token: 0x04003C87 RID: 15495
		FolderViews,
		// Token: 0x04003C88 RID: 15496
		FolderRestrictions,
		// Token: 0x04003C89 RID: 15497
		ContentVerification,
		// Token: 0x04003C8A RID: 15498
		BlockFinalization,
		// Token: 0x04003C8B RID: 15499
		FailOnFirstBadItem,
		// Token: 0x04003C8C RID: 15500
		KnownCorruptions = 12,
		// Token: 0x04003C8D RID: 15501
		FailOnCorruptSyncState = 14
	}
}
