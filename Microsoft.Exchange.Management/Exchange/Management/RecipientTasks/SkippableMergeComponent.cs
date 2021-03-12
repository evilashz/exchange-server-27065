using System;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C82 RID: 3202
	public enum SkippableMergeComponent
	{
		// Token: 0x04003D20 RID: 15648
		FolderRules,
		// Token: 0x04003D21 RID: 15649
		FolderACLs,
		// Token: 0x04003D22 RID: 15650
		InitialConnectionValidation,
		// Token: 0x04003D23 RID: 15651
		FailOnFirstBadItem = 4,
		// Token: 0x04003D24 RID: 15652
		ContentVerification,
		// Token: 0x04003D25 RID: 15653
		KnownCorruptions,
		// Token: 0x04003D26 RID: 15654
		FailOnCorruptSyncState
	}
}
