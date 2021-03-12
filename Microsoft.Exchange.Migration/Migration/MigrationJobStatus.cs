using System;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000072 RID: 114
	internal enum MigrationJobStatus
	{
		// Token: 0x040002A4 RID: 676
		Created,
		// Token: 0x040002A5 RID: 677
		SyncInitializing,
		// Token: 0x040002A6 RID: 678
		SyncStarting,
		// Token: 0x040002A7 RID: 679
		SyncCompleting,
		// Token: 0x040002A8 RID: 680
		SyncCompleted,
		// Token: 0x040002A9 RID: 681
		CompletionInitializing,
		// Token: 0x040002AA RID: 682
		CompletionStarting,
		// Token: 0x040002AB RID: 683
		Completing,
		// Token: 0x040002AC RID: 684
		Completed,
		// Token: 0x040002AD RID: 685
		Failed,
		// Token: 0x040002AE RID: 686
		Removed,
		// Token: 0x040002AF RID: 687
		Removing,
		// Token: 0x040002B0 RID: 688
		ProvisionStarting,
		// Token: 0x040002B1 RID: 689
		Validating,
		// Token: 0x040002B2 RID: 690
		Stopped,
		// Token: 0x040002B3 RID: 691
		Corrupted
	}
}
