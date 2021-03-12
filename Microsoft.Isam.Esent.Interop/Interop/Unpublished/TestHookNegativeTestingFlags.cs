using System;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x0200006D RID: 109
	public enum TestHookNegativeTestingFlags
	{
		// Token: 0x04000246 RID: 582
		DeletingLogFiles = 1,
		// Token: 0x04000247 RID: 583
		CorruptingLogFiles,
		// Token: 0x04000248 RID: 584
		LockingCheckpointFile = 4,
		// Token: 0x04000249 RID: 585
		CorruptingDbHeaders = 8,
		// Token: 0x0400024A RID: 586
		CorruptingPagePgnos = 16,
		// Token: 0x0400024B RID: 587
		LeakStuff = 32,
		// Token: 0x0400024C RID: 588
		CorruptingWithLostFlush = 64,
		// Token: 0x0400024D RID: 589
		DisableTimeoutDeadlockDetection = 128,
		// Token: 0x0400024E RID: 590
		CorruptingPages = 256,
		// Token: 0x0400024F RID: 591
		DiskIoError = 512,
		// Token: 0x04000250 RID: 592
		InvalidApiUsage = 1024,
		// Token: 0x04000251 RID: 593
		InvalidUsage = 2048
	}
}
