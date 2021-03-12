using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002BD RID: 701
	internal enum MountDatabaseDirectOperation
	{
		// Token: 0x04000B0B RID: 2827
		AmPreMountCallback = 1,
		// Token: 0x04000B0C RID: 2828
		RegistryReplicatorCopy,
		// Token: 0x04000B0D RID: 2829
		StoreMount,
		// Token: 0x04000B0E RID: 2830
		PreMountQueuedOpStart,
		// Token: 0x04000B0F RID: 2831
		PreMountQueuedOpExecution,
		// Token: 0x04000B10 RID: 2832
		PreventMountIfNecessary,
		// Token: 0x04000B11 RID: 2833
		ResumeActiveCopy,
		// Token: 0x04000B12 RID: 2834
		UpdateLastLogGenOnMount,
		// Token: 0x04000B13 RID: 2835
		GetRunningReplicaInstance,
		// Token: 0x04000B14 RID: 2836
		ConfirmLogReset,
		// Token: 0x04000B15 RID: 2837
		LowestGenerationInDirectory,
		// Token: 0x04000B16 RID: 2838
		HighestGenerationInDirectory,
		// Token: 0x04000B17 RID: 2839
		GenerationAvailableInDirectory,
		// Token: 0x04000B18 RID: 2840
		UpdateLastLogGeneratedInClusDB
	}
}
