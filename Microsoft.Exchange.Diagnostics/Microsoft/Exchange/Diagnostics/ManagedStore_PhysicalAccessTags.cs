using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000298 RID: 664
	public struct ManagedStore_PhysicalAccessTags
	{
		// Token: 0x040011C0 RID: 4544
		public const int DbInteractionSummary = 0;

		// Token: 0x040011C1 RID: 4545
		public const int DbInteractionIntermediate = 1;

		// Token: 0x040011C2 RID: 4546
		public const int DbInteractionDetail = 2;

		// Token: 0x040011C3 RID: 4547
		public const int DbInitialization = 3;

		// Token: 0x040011C4 RID: 4548
		public const int DirtyObjects = 4;

		// Token: 0x040011C5 RID: 4549
		public const int DbIO = 5;

		// Token: 0x040011C6 RID: 4550
		public const int BadPlanDetection = 6;

		// Token: 0x040011C7 RID: 4551
		public const int CategorizedTableOperator = 15;

		// Token: 0x040011C8 RID: 4552
		public const int SnapshotOperation = 16;

		// Token: 0x040011C9 RID: 4553
		public const int FaultInjection = 20;

		// Token: 0x040011CA RID: 4554
		public const int JetInformation = 651;

		// Token: 0x040011CB RID: 4555
		public const int JetErrors = 652;

		// Token: 0x040011CC RID: 4556
		public const int JetAsserts = 653;

		// Token: 0x040011CD RID: 4557
		public const int JetAPI = 654;

		// Token: 0x040011CE RID: 4558
		public const int JetInitTerm = 655;

		// Token: 0x040011CF RID: 4559
		public const int JetBufferManager = 656;

		// Token: 0x040011D0 RID: 4560
		public const int JetBufferManagerHashedLatches = 657;

		// Token: 0x040011D1 RID: 4561
		public const int JetIO = 658;

		// Token: 0x040011D2 RID: 4562
		public const int JetMemory = 659;

		// Token: 0x040011D3 RID: 4563
		public const int JetVersionStore = 660;

		// Token: 0x040011D4 RID: 4564
		public const int JetVersionStoreOOM = 661;

		// Token: 0x040011D5 RID: 4565
		public const int JetVersionCleanup = 662;

		// Token: 0x040011D6 RID: 4566
		public const int JetCatalog = 663;

		// Token: 0x040011D7 RID: 4567
		public const int JetDDLRead = 664;

		// Token: 0x040011D8 RID: 4568
		public const int JetDDLWrite = 665;

		// Token: 0x040011D9 RID: 4569
		public const int JetDMLRead = 666;

		// Token: 0x040011DA RID: 4570
		public const int JetDMLWrite = 667;

		// Token: 0x040011DB RID: 4571
		public const int JetDMLConflicts = 668;

		// Token: 0x040011DC RID: 4572
		public const int JetInstances = 669;

		// Token: 0x040011DD RID: 4573
		public const int JetDatabases = 670;

		// Token: 0x040011DE RID: 4574
		public const int JetSessions = 671;

		// Token: 0x040011DF RID: 4575
		public const int JetCursors = 672;

		// Token: 0x040011E0 RID: 4576
		public const int JetCursorNavigation = 673;

		// Token: 0x040011E1 RID: 4577
		public const int JetCursorPageRefs = 674;

		// Token: 0x040011E2 RID: 4578
		public const int JetBtree = 675;

		// Token: 0x040011E3 RID: 4579
		public const int JetSpace = 676;

		// Token: 0x040011E4 RID: 4580
		public const int JetFCBs = 677;

		// Token: 0x040011E5 RID: 4581
		public const int JetTransactions = 678;

		// Token: 0x040011E6 RID: 4582
		public const int JetLogging = 679;

		// Token: 0x040011E7 RID: 4583
		public const int JetRecovery = 680;

		// Token: 0x040011E8 RID: 4584
		public const int JetBackup = 681;

		// Token: 0x040011E9 RID: 4585
		public const int JetRestore = 682;

		// Token: 0x040011EA RID: 4586
		public const int JetOLD = 683;

		// Token: 0x040011EB RID: 4587
		public const int JetEventlog = 684;

		// Token: 0x040011EC RID: 4588
		public const int JetBufferManagerMaintTasks = 685;

		// Token: 0x040011ED RID: 4589
		public const int JetSpaceManagement = 686;

		// Token: 0x040011EE RID: 4590
		public const int JetSpaceInternal = 687;

		// Token: 0x040011EF RID: 4591
		public const int JetIOQueue = 688;

		// Token: 0x040011F0 RID: 4592
		public const int JetDiskVolumeManagement = 689;

		// Token: 0x040011F1 RID: 4593
		public const int JetCallbacks = 690;

		// Token: 0x040011F2 RID: 4594
		public const int JetIOProblems = 691;

		// Token: 0x040011F3 RID: 4595
		public const int JetUpgrade = 692;

		// Token: 0x040011F4 RID: 4596
		public const int JetBufMgrCacheState = 693;

		// Token: 0x040011F5 RID: 4597
		public const int JetBufMgrDirtyState = 694;

		// Token: 0x040011F6 RID: 4598
		public static Guid guid = new Guid("40c22f16-f297-499a-b812-a5a352295610");
	}
}
