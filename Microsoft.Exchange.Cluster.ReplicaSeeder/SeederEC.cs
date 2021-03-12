using System;

// Token: 0x020000D8 RID: 216
public enum SeederEC : int
{
	// Token: 0x0400020E RID: 526
	EcSuccess,
	// Token: 0x0400020F RID: 527
	EcError,
	// Token: 0x04000210 RID: 528
	EcInvalidInput,
	// Token: 0x04000211 RID: 529
	EcOOMem,
	// Token: 0x04000212 RID: 530
	EcNotEnoughDisk,
	// Token: 0x04000213 RID: 531
	EcFailAcqRight,
	// Token: 0x04000214 RID: 532
	EcDirDoesNotExist,
	// Token: 0x04000215 RID: 533
	EcLogAlreadyExist,
	// Token: 0x04000216 RID: 534
	EcJtxAlreadyExist,
	// Token: 0x04000217 RID: 535
	EcDBNotFound,
	// Token: 0x04000218 RID: 536
	EcStoreNotOnline,
	// Token: 0x04000219 RID: 537
	EcNoOnlineEdb,
	// Token: 0x0400021A RID: 538
	EcSeedingCancelled,
	// Token: 0x0400021B RID: 539
	EcOverlappedWriteErr,
	// Token: 0x0400021C RID: 540
	EcMdbAlreadyExist,
	// Token: 0x0400021D RID: 541
	JetErrFileIOBeyondEOF = -4001,
	// Token: 0x0400021E RID: 542
	EcTestAborted = 15,
	// Token: 0x0400021F RID: 543
	EcTargetDbFileInUse,
	// Token: 0x04000220 RID: 544
	EcDeviceNotReady,
	// Token: 0x04000221 RID: 545
	EcCommunicationsError
}
