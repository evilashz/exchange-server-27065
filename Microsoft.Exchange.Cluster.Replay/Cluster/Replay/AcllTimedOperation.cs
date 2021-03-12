using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002BC RID: 700
	internal enum AcllTimedOperation
	{
		// Token: 0x04000AF4 RID: 2804
		AcllQueuedOpStart = 1,
		// Token: 0x04000AF5 RID: 2805
		AcllQueuedOpExecution,
		// Token: 0x04000AF6 RID: 2806
		AcquireSuspendLock,
		// Token: 0x04000AF7 RID: 2807
		EnsureTargetDismounted,
		// Token: 0x04000AF8 RID: 2808
		FileCheckerAtStart,
		// Token: 0x04000AF9 RID: 2809
		CleanUpTempIncReseedFiles,
		// Token: 0x04000AFA RID: 2810
		IsIncrementalReseedNecessary,
		// Token: 0x04000AFB RID: 2811
		AttemptFinalCopy,
		// Token: 0x04000AFC RID: 2812
		InspectFinalLogs,
		// Token: 0x04000AFD RID: 2813
		InspectE00Log,
		// Token: 0x04000AFE RID: 2814
		CheckRequiredLogFilesAtEnd,
		// Token: 0x04000AFF RID: 2815
		CopyLogsOverall,
		// Token: 0x04000B00 RID: 2816
		AcllEnterLogCopierWorkerLock,
		// Token: 0x04000B01 RID: 2817
		RunAcllInit,
		// Token: 0x04000B02 RID: 2818
		AcllLogCopierFirstInit,
		// Token: 0x04000B03 RID: 2819
		AcllInitWaitForReadCallback,
		// Token: 0x04000B04 RID: 2820
		ComputeLossAndMountAllowedOverall,
		// Token: 0x04000B05 RID: 2821
		SetLossVariables,
		// Token: 0x04000B06 RID: 2822
		ProtectUnboundedDataloss,
		// Token: 0x04000B07 RID: 2823
		MarkRedeliveryRequired,
		// Token: 0x04000B08 RID: 2824
		RollbackLastLogIfNecessary,
		// Token: 0x04000B09 RID: 2825
		SetE00LogGeneration
	}
}
