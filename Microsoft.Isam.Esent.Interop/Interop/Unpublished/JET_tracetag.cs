using System;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x02000068 RID: 104
	public enum JET_tracetag
	{
		// Token: 0x04000207 RID: 519
		Null,
		// Token: 0x04000208 RID: 520
		Information,
		// Token: 0x04000209 RID: 521
		Errors,
		// Token: 0x0400020A RID: 522
		Asserts,
		// Token: 0x0400020B RID: 523
		API,
		// Token: 0x0400020C RID: 524
		InitTerm,
		// Token: 0x0400020D RID: 525
		BufferManager,
		// Token: 0x0400020E RID: 526
		BufferManagerHashedLatches,
		// Token: 0x0400020F RID: 527
		IO,
		// Token: 0x04000210 RID: 528
		Memory,
		// Token: 0x04000211 RID: 529
		VersionStore,
		// Token: 0x04000212 RID: 530
		VersionStoreOOM,
		// Token: 0x04000213 RID: 531
		VersionCleanup,
		// Token: 0x04000214 RID: 532
		Catalog,
		// Token: 0x04000215 RID: 533
		DDLRead,
		// Token: 0x04000216 RID: 534
		DDLWrite,
		// Token: 0x04000217 RID: 535
		DMLRead,
		// Token: 0x04000218 RID: 536
		DMLWrite,
		// Token: 0x04000219 RID: 537
		DMLConflicts,
		// Token: 0x0400021A RID: 538
		Instances,
		// Token: 0x0400021B RID: 539
		Databases,
		// Token: 0x0400021C RID: 540
		Sessions,
		// Token: 0x0400021D RID: 541
		Cursors,
		// Token: 0x0400021E RID: 542
		CursorNavigation,
		// Token: 0x0400021F RID: 543
		CursorPageRefs,
		// Token: 0x04000220 RID: 544
		Btree,
		// Token: 0x04000221 RID: 545
		Space,
		// Token: 0x04000222 RID: 546
		FCBs,
		// Token: 0x04000223 RID: 547
		Transactions,
		// Token: 0x04000224 RID: 548
		Logging,
		// Token: 0x04000225 RID: 549
		Recovery,
		// Token: 0x04000226 RID: 550
		Backup,
		// Token: 0x04000227 RID: 551
		Restore,
		// Token: 0x04000228 RID: 552
		OLD,
		// Token: 0x04000229 RID: 553
		Eventlog,
		// Token: 0x0400022A RID: 554
		BufferManagerMaintTasks,
		// Token: 0x0400022B RID: 555
		SpaceManagement,
		// Token: 0x0400022C RID: 556
		SpaceInternal,
		// Token: 0x0400022D RID: 557
		IOQueue,
		// Token: 0x0400022E RID: 558
		DiskVolumeManagement,
		// Token: 0x0400022F RID: 559
		Callbacks,
		// Token: 0x04000230 RID: 560
		IOProblems,
		// Token: 0x04000231 RID: 561
		Upgrade,
		// Token: 0x04000232 RID: 562
		RecoveryValidation,
		// Token: 0x04000233 RID: 563
		BufferManagerBufferCacheState,
		// Token: 0x04000234 RID: 564
		BufferManagerBufferDirtyState,
		// Token: 0x04000235 RID: 565
		TimerQueue,
		// Token: 0x04000236 RID: 566
		SortPerf,
		// Token: 0x04000237 RID: 567
		Max
	}
}
