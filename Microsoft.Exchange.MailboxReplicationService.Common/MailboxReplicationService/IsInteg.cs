using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000131 RID: 305
	internal class IsInteg
	{
		// Token: 0x040005EA RID: 1514
		public static uint[] CorruptionTypesToFix = new uint[]
		{
			7U
		};

		// Token: 0x040005EB RID: 1515
		public static readonly PropTag[] JobPropTags = new PropTag[]
		{
			PropTag.IsIntegJobRequestGuid,
			PropTag.IsIntegJobGuid,
			PropTag.IsIntegJobMailboxGuid,
			PropTag.IsIntegJobFlags,
			PropTag.IsIntegJobTask,
			PropTag.IsIntegJobCreationTime,
			PropTag.IsIntegJobState,
			PropTag.IsIntegJobProgress,
			PropTag.IsIntegJobSource,
			PropTag.IsIntegJobPriority,
			PropTag.IsIntegJobTimeInServer,
			PropTag.IsIntegJobFinishTime,
			PropTag.IsIntegJobLastExecutionTime,
			PropTag.IsIntegJobCorruptionsDetected,
			PropTag.IsIntegJobCorruptionsFixed,
			PropTag.IsIntegJobCorruptions,
			PropTag.RtfSyncTrailingCount
		};

		// Token: 0x02000132 RID: 306
		public enum StoreIntegrityCheckOperation
		{
			// Token: 0x040005ED RID: 1517
			NewJob = 1,
			// Token: 0x040005EE RID: 1518
			GetJob,
			// Token: 0x040005EF RID: 1519
			RemoveJob,
			// Token: 0x040005F0 RID: 1520
			GetJobDetails
		}

		// Token: 0x02000133 RID: 307
		[Flags]
		public enum StoreIntegrityCheckRequestFlags : uint
		{
			// Token: 0x040005F2 RID: 1522
			None = 0U,
			// Token: 0x040005F3 RID: 1523
			DetectOnly = 1U,
			// Token: 0x040005F4 RID: 1524
			Force = 2U,
			// Token: 0x040005F5 RID: 1525
			SystemJob = 4U,
			// Token: 0x040005F6 RID: 1526
			Verbose = 2147483648U
		}

		// Token: 0x02000134 RID: 308
		[Flags]
		public enum IntegrityCheckQueryFlags : uint
		{
			// Token: 0x040005F8 RID: 1528
			None = 0U,
			// Token: 0x040005F9 RID: 1529
			QueryJob = 4U
		}

		// Token: 0x02000135 RID: 309
		public enum MailboxCorruptionType
		{
			// Token: 0x040005FB RID: 1531
			None,
			// Token: 0x040005FC RID: 1532
			SearchFolder,
			// Token: 0x040005FD RID: 1533
			FolderView,
			// Token: 0x040005FE RID: 1534
			AggregateCounts,
			// Token: 0x040005FF RID: 1535
			ProvisionedFolder,
			// Token: 0x04000600 RID: 1536
			ReplState,
			// Token: 0x04000601 RID: 1537
			MessagePtagCn,
			// Token: 0x04000602 RID: 1538
			MessageId,
			// Token: 0x04000603 RID: 1539
			RuleMessageClass = 100,
			// Token: 0x04000604 RID: 1540
			RestrictionFolder,
			// Token: 0x04000605 RID: 1541
			FolderACL,
			// Token: 0x04000606 RID: 1542
			UniqueMidIndex,
			// Token: 0x04000607 RID: 1543
			CorruptJunkRule,
			// Token: 0x04000608 RID: 1544
			MissingSpecialFolders,
			// Token: 0x04000609 RID: 1545
			DropAllLazyIndexes,
			// Token: 0x0400060A RID: 1546
			LockedMoveTarget = 4096,
			// Token: 0x0400060B RID: 1547
			Extension1 = 32768,
			// Token: 0x0400060C RID: 1548
			Extension2,
			// Token: 0x0400060D RID: 1549
			Extension3,
			// Token: 0x0400060E RID: 1550
			Extension4,
			// Token: 0x0400060F RID: 1551
			Extension5
		}

		// Token: 0x02000136 RID: 310
		public enum JobSource : short
		{
			// Token: 0x04000611 RID: 1553
			OnDemand,
			// Token: 0x04000612 RID: 1554
			Maintenance
		}

		// Token: 0x02000137 RID: 311
		public enum JobPriority : short
		{
			// Token: 0x04000614 RID: 1556
			High,
			// Token: 0x04000615 RID: 1557
			Normal,
			// Token: 0x04000616 RID: 1558
			Low
		}

		// Token: 0x02000138 RID: 312
		public enum JobState
		{
			// Token: 0x04000618 RID: 1560
			Initializing,
			// Token: 0x04000619 RID: 1561
			Queued,
			// Token: 0x0400061A RID: 1562
			Running,
			// Token: 0x0400061B RID: 1563
			Succeeded,
			// Token: 0x0400061C RID: 1564
			Failed
		}

		// Token: 0x02000139 RID: 313
		[Flags]
		public enum JobFlags : uint
		{
			// Token: 0x0400061E RID: 1566
			None = 0U,
			// Token: 0x0400061F RID: 1567
			DetectOnly = 1U,
			// Token: 0x04000620 RID: 1568
			Background = 2U,
			// Token: 0x04000621 RID: 1569
			OnDemand = 4U,
			// Token: 0x04000622 RID: 1570
			System = 8U,
			// Token: 0x04000623 RID: 1571
			Force = 16U,
			// Token: 0x04000624 RID: 1572
			Verbose = 2147483648U
		}

		// Token: 0x0200013A RID: 314
		public enum IsIntegOps
		{
			// Token: 0x04000626 RID: 1574
			None,
			// Token: 0x04000627 RID: 1575
			CreateTask,
			// Token: 0x04000628 RID: 1576
			QueryTask
		}
	}
}
