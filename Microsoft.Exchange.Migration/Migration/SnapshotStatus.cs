using System;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200018B RID: 395
	internal enum SnapshotStatus
	{
		// Token: 0x04000665 RID: 1637
		InProgress,
		// Token: 0x04000666 RID: 1638
		Failed,
		// Token: 0x04000667 RID: 1639
		AutoSuspended,
		// Token: 0x04000668 RID: 1640
		Corrupted,
		// Token: 0x04000669 RID: 1641
		Removed,
		// Token: 0x0400066A RID: 1642
		CompletedWithWarning,
		// Token: 0x0400066B RID: 1643
		Finalized,
		// Token: 0x0400066C RID: 1644
		Suspended,
		// Token: 0x0400066D RID: 1645
		Synced
	}
}
