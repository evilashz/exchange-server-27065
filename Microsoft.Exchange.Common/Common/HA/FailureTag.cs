using System;

namespace Microsoft.Exchange.Common.HA
{
	// Token: 0x02000011 RID: 17
	internal enum FailureTag : uint
	{
		// Token: 0x04000028 RID: 40
		NoOp,
		// Token: 0x04000029 RID: 41
		Configuration,
		// Token: 0x0400002A RID: 42
		Repairable,
		// Token: 0x0400002B RID: 43
		Space,
		// Token: 0x0400002C RID: 44
		IoHard,
		// Token: 0x0400002D RID: 45
		SourceCorruption,
		// Token: 0x0400002E RID: 46
		Corruption,
		// Token: 0x0400002F RID: 47
		Hard,
		// Token: 0x04000030 RID: 48
		Unrecoverable,
		// Token: 0x04000031 RID: 49
		Remount,
		// Token: 0x04000032 RID: 50
		Reseed,
		// Token: 0x04000033 RID: 51
		Performance,
		// Token: 0x04000034 RID: 52
		MoveLoad,
		// Token: 0x04000035 RID: 53
		Memory,
		// Token: 0x04000036 RID: 54
		CatalogReseed,
		// Token: 0x04000037 RID: 55
		AlertOnly,
		// Token: 0x04000038 RID: 56
		ExpectedDismount,
		// Token: 0x04000039 RID: 57
		UnexpectedDismount,
		// Token: 0x0400003A RID: 58
		ExceededMaxDatabases,
		// Token: 0x0400003B RID: 59
		GenericMountFailure,
		// Token: 0x0400003C RID: 60
		PagePatchRequested,
		// Token: 0x0400003D RID: 61
		PagePatchCompleted,
		// Token: 0x0400003E RID: 62
		LostFlushDetected,
		// Token: 0x0400003F RID: 63
		SourceLogCorruption,
		// Token: 0x04000040 RID: 64
		FailedToRepair,
		// Token: 0x04000041 RID: 65
		LostFlushDbTimeTooOld,
		// Token: 0x04000042 RID: 66
		LostFlushDbTimeTooNew,
		// Token: 0x04000043 RID: 67
		ExceededMaxActiveDatabases,
		// Token: 0x04000044 RID: 68
		SourceLogCorruptionOutsideRequiredRange,
		// Token: 0x04000045 RID: 69
		HungIoExceededThreshold,
		// Token: 0x04000046 RID: 70
		HungIoCancelSucceeded,
		// Token: 0x04000047 RID: 71
		HungIoCancelFailed,
		// Token: 0x04000048 RID: 72
		RecoveryRedoLogCorruption,
		// Token: 0x04000049 RID: 73
		ReplayFailedToPagePatch,
		// Token: 0x0400004A RID: 74
		FileSystemCorruption,
		// Token: 0x0400004B RID: 75
		HungIoLowThreshold,
		// Token: 0x0400004C RID: 76
		HungIoMediumThreshold,
		// Token: 0x0400004D RID: 77
		HungIoExceededThresholdDoubleDisk,
		// Token: 0x0400004E RID: 78
		HungStoreWorker,
		// Token: 0x0400004F RID: 79
		UnaccessibleStoreWorker,
		// Token: 0x04000050 RID: 80
		MonitoredDatabaseFailed,
		// Token: 0x04000051 RID: 81
		LogGapFatal,
		// Token: 0x04000052 RID: 82
		ExceededDatabaseMaxSize,
		// Token: 0x04000053 RID: 83
		LowDiskSpaceStraggler,
		// Token: 0x04000054 RID: 84
		LockedVolume
	}
}
