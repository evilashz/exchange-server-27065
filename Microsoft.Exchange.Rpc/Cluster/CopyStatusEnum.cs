using System;

namespace Microsoft.Exchange.Rpc.Cluster
{
	// Token: 0x0200013B RID: 315
	internal enum CopyStatusEnum
	{
		// Token: 0x04000A9A RID: 2714
		Unknown,
		// Token: 0x04000A9B RID: 2715
		Initializing,
		// Token: 0x04000A9C RID: 2716
		Resynchronizing,
		// Token: 0x04000A9D RID: 2717
		DisconnectedAndResynchronizing,
		// Token: 0x04000A9E RID: 2718
		DisconnectedAndHealthy,
		// Token: 0x04000A9F RID: 2719
		Healthy,
		// Token: 0x04000AA0 RID: 2720
		Failed,
		// Token: 0x04000AA1 RID: 2721
		FailedAndSuspended,
		// Token: 0x04000AA2 RID: 2722
		Suspended,
		// Token: 0x04000AA3 RID: 2723
		Seeding,
		// Token: 0x04000AA4 RID: 2724
		Mounting,
		// Token: 0x04000AA5 RID: 2725
		Mounted,
		// Token: 0x04000AA6 RID: 2726
		Dismounting,
		// Token: 0x04000AA7 RID: 2727
		Dismounted,
		// Token: 0x04000AA8 RID: 2728
		NonExchangeReplication,
		// Token: 0x04000AA9 RID: 2729
		SeedingSource,
		// Token: 0x04000AAA RID: 2730
		Misconfigured
	}
}
