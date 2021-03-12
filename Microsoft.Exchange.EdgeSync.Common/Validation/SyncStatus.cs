using System;

namespace Microsoft.Exchange.EdgeSync.Validation
{
	// Token: 0x02000040 RID: 64
	public enum SyncStatus
	{
		// Token: 0x0400010C RID: 268
		Synchronized = 1,
		// Token: 0x0400010D RID: 269
		NotSynchronized,
		// Token: 0x0400010E RID: 270
		NotStarted,
		// Token: 0x0400010F RID: 271
		InProgress,
		// Token: 0x04000110 RID: 272
		Skipped,
		// Token: 0x04000111 RID: 273
		DirectoryError,
		// Token: 0x04000112 RID: 274
		Expired
	}
}
