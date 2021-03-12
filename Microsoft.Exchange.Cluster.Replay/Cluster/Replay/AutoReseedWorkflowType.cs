using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200027F RID: 639
	internal enum AutoReseedWorkflowType
	{
		// Token: 0x040009EA RID: 2538
		FailedSuspendedCopyAutoReseed = 1,
		// Token: 0x040009EB RID: 2539
		CatalogAutoReseed,
		// Token: 0x040009EC RID: 2540
		FailedSuspendedCatalogRebuild,
		// Token: 0x040009ED RID: 2541
		HealthyCopyCompletedSeed,
		// Token: 0x040009EE RID: 2542
		FailedCopy,
		// Token: 0x040009EF RID: 2543
		ManualReseed,
		// Token: 0x040009F0 RID: 2544
		ManualResume,
		// Token: 0x040009F1 RID: 2545
		MountNeverMountedActive
	}
}
