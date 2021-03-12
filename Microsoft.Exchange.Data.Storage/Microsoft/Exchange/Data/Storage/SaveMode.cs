using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001E2 RID: 482
	internal enum SaveMode
	{
		// Token: 0x04000D74 RID: 3444
		ResolveConflicts,
		// Token: 0x04000D75 RID: 3445
		FailOnAnyConflict,
		// Token: 0x04000D76 RID: 3446
		NoConflictResolution,
		// Token: 0x04000D77 RID: 3447
		NoConflictResolutionForceSave
	}
}
