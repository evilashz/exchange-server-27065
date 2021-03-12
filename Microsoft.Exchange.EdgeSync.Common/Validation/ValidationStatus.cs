using System;

namespace Microsoft.Exchange.EdgeSync.Validation
{
	// Token: 0x0200004F RID: 79
	public enum ValidationStatus
	{
		// Token: 0x04000155 RID: 341
		NoSyncConfigured,
		// Token: 0x04000156 RID: 342
		Normal,
		// Token: 0x04000157 RID: 343
		Warning,
		// Token: 0x04000158 RID: 344
		Failed,
		// Token: 0x04000159 RID: 345
		Inconclusive,
		// Token: 0x0400015A RID: 346
		FailedUrgent
	}
}
