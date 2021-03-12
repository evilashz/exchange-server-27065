using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001F7 RID: 503
	[Serializable]
	public sealed class FolderMoveRequestStatistics : RequestStatisticsBase
	{
		// Token: 0x0600158A RID: 5514 RVA: 0x00030417 File Offset: 0x0002E617
		public FolderMoveRequestStatistics()
		{
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x0003041F File Offset: 0x0002E61F
		internal FolderMoveRequestStatistics(SimpleProviderPropertyBag propertyBag) : base(propertyBag)
		{
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x00030428 File Offset: 0x0002E628
		internal FolderMoveRequestStatistics(TransactionalRequestJob requestJob) : this((SimpleProviderPropertyBag)requestJob.propertyBag)
		{
			base.CopyNonSchematizedPropertiesFrom(requestJob);
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x00030442 File Offset: 0x0002E642
		internal FolderMoveRequestStatistics(RequestJobXML requestJob) : this((SimpleProviderPropertyBag)requestJob.propertyBag)
		{
			base.CopyNonSchematizedPropertiesFrom(requestJob);
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x0003045C File Offset: 0x0002E65C
		// (set) Token: 0x0600158F RID: 5519 RVA: 0x00030464 File Offset: 0x0002E664
		public new string Name
		{
			get
			{
				return base.Name;
			}
			internal set
			{
				base.Name = value;
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001590 RID: 5520 RVA: 0x0003046D File Offset: 0x0002E66D
		// (set) Token: 0x06001591 RID: 5521 RVA: 0x00030475 File Offset: 0x0002E675
		public new RequestStatus Status
		{
			get
			{
				return base.Status;
			}
			internal set
			{
				base.Status = value;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001592 RID: 5522 RVA: 0x0003047E File Offset: 0x0002E67E
		public new RequestState StatusDetail
		{
			get
			{
				return base.StatusDetail;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001593 RID: 5523 RVA: 0x00030486 File Offset: 0x0002E686
		// (set) Token: 0x06001594 RID: 5524 RVA: 0x0003048E File Offset: 0x0002E68E
		public new SyncStage SyncStage
		{
			get
			{
				return base.SyncStage;
			}
			internal set
			{
				base.SyncStage = value;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001595 RID: 5525 RVA: 0x00030497 File Offset: 0x0002E697
		// (set) Token: 0x06001596 RID: 5526 RVA: 0x0003049F File Offset: 0x0002E69F
		public new RequestFlags Flags
		{
			get
			{
				return base.Flags;
			}
			internal set
			{
				base.Flags = value;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001597 RID: 5527 RVA: 0x000304A8 File Offset: 0x0002E6A8
		// (set) Token: 0x06001598 RID: 5528 RVA: 0x000304B0 File Offset: 0x0002E6B0
		public new RequestStyle RequestStyle
		{
			get
			{
				return base.RequestStyle;
			}
			internal set
			{
				base.RequestStyle = value;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001599 RID: 5529 RVA: 0x000304B9 File Offset: 0x0002E6B9
		// (set) Token: 0x0600159A RID: 5530 RVA: 0x000304C1 File Offset: 0x0002E6C1
		public new RequestDirection Direction
		{
			get
			{
				return base.Direction;
			}
			internal set
			{
				base.Direction = value;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x0600159B RID: 5531 RVA: 0x000304CA File Offset: 0x0002E6CA
		// (set) Token: 0x0600159C RID: 5532 RVA: 0x000304D2 File Offset: 0x0002E6D2
		public new List<MoveFolderInfo> FolderList
		{
			get
			{
				return base.FolderList;
			}
			internal set
			{
				base.FolderList = value;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x0600159D RID: 5533 RVA: 0x000304DB File Offset: 0x0002E6DB
		// (set) Token: 0x0600159E RID: 5534 RVA: 0x000304E3 File Offset: 0x0002E6E3
		public new bool Protect
		{
			get
			{
				return base.Protect;
			}
			internal set
			{
				base.Protect = value;
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x0600159F RID: 5535 RVA: 0x000304EC File Offset: 0x0002E6EC
		// (set) Token: 0x060015A0 RID: 5536 RVA: 0x000304F4 File Offset: 0x0002E6F4
		public new RequestPriority Priority
		{
			get
			{
				return base.Priority;
			}
			internal set
			{
				base.Priority = value;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x060015A1 RID: 5537 RVA: 0x000304FD File Offset: 0x0002E6FD
		// (set) Token: 0x060015A2 RID: 5538 RVA: 0x00030505 File Offset: 0x0002E705
		public new RequestWorkloadType WorkloadType
		{
			get
			{
				return base.WorkloadType;
			}
			internal set
			{
				base.WorkloadType = value;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x060015A3 RID: 5539 RVA: 0x0003050E File Offset: 0x0002E70E
		// (set) Token: 0x060015A4 RID: 5540 RVA: 0x00030516 File Offset: 0x0002E716
		public new bool Suspend
		{
			get
			{
				return base.Suspend;
			}
			internal set
			{
				base.Suspend = value;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x060015A5 RID: 5541 RVA: 0x0003051F File Offset: 0x0002E71F
		// (set) Token: 0x060015A6 RID: 5542 RVA: 0x0003052C File Offset: 0x0002E72C
		public new ServerVersion SourceVersion
		{
			get
			{
				return new ServerVersion(base.SourceVersion);
			}
			set
			{
				base.SourceVersion = value.ToInt();
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x0003053A File Offset: 0x0002E73A
		// (set) Token: 0x060015A8 RID: 5544 RVA: 0x00030542 File Offset: 0x0002E742
		public new string SourceServer
		{
			get
			{
				return base.SourceServer;
			}
			internal set
			{
				base.SourceServer = value;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x060015A9 RID: 5545 RVA: 0x0003054B File Offset: 0x0002E74B
		public ADObjectId SourceMailbox
		{
			get
			{
				return base.SourceUserId;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x060015AA RID: 5546 RVA: 0x00030553 File Offset: 0x0002E753
		// (set) Token: 0x060015AB RID: 5547 RVA: 0x00030560 File Offset: 0x0002E760
		public new ServerVersion TargetVersion
		{
			get
			{
				return new ServerVersion(base.TargetVersion);
			}
			set
			{
				base.TargetVersion = value.ToInt();
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x060015AC RID: 5548 RVA: 0x0003056E File Offset: 0x0002E76E
		// (set) Token: 0x060015AD RID: 5549 RVA: 0x00030576 File Offset: 0x0002E776
		public new string TargetServer
		{
			get
			{
				return base.TargetServer;
			}
			internal set
			{
				base.TargetServer = value;
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x060015AE RID: 5550 RVA: 0x0003057F File Offset: 0x0002E77F
		public ADObjectId TargetMailbox
		{
			get
			{
				return base.TargetUserId;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x060015AF RID: 5551 RVA: 0x00030587 File Offset: 0x0002E787
		private new string RemoteGlobalCatalog
		{
			get
			{
				return base.RemoteGlobalCatalog;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x0003058F File Offset: 0x0002E78F
		// (set) Token: 0x060015B1 RID: 5553 RVA: 0x00030597 File Offset: 0x0002E797
		public new string BatchName
		{
			get
			{
				return base.BatchName;
			}
			internal set
			{
				base.BatchName = value;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x060015B2 RID: 5554 RVA: 0x000305A0 File Offset: 0x0002E7A0
		private new string RemoteCredentialUsername
		{
			get
			{
				return base.RemoteCredentialUsername;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x060015B3 RID: 5555 RVA: 0x000305A8 File Offset: 0x0002E7A8
		private new string RemoteDatabaseName
		{
			get
			{
				return base.RemoteDatabaseName;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x060015B4 RID: 5556 RVA: 0x000305B0 File Offset: 0x0002E7B0
		private new Guid? RemoteDatabaseGuid
		{
			get
			{
				return base.RemoteDatabaseGuid;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x060015B5 RID: 5557 RVA: 0x000305B8 File Offset: 0x0002E7B8
		// (set) Token: 0x060015B6 RID: 5558 RVA: 0x000305C0 File Offset: 0x0002E7C0
		public new Unlimited<int> BadItemLimit
		{
			get
			{
				return base.BadItemLimit;
			}
			internal set
			{
				base.BadItemLimit = value;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x000305C9 File Offset: 0x0002E7C9
		// (set) Token: 0x060015B8 RID: 5560 RVA: 0x000305D1 File Offset: 0x0002E7D1
		public new int BadItemsEncountered
		{
			get
			{
				return base.BadItemsEncountered;
			}
			internal set
			{
				base.BadItemsEncountered = value;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x060015B9 RID: 5561 RVA: 0x000305DA File Offset: 0x0002E7DA
		// (set) Token: 0x060015BA RID: 5562 RVA: 0x000305E2 File Offset: 0x0002E7E2
		public new Unlimited<int> LargeItemLimit
		{
			get
			{
				return base.LargeItemLimit;
			}
			internal set
			{
				base.LargeItemLimit = value;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x060015BB RID: 5563 RVA: 0x000305EB File Offset: 0x0002E7EB
		// (set) Token: 0x060015BC RID: 5564 RVA: 0x000305F3 File Offset: 0x0002E7F3
		public new int LargeItemsEncountered
		{
			get
			{
				return base.LargeItemsEncountered;
			}
			internal set
			{
				base.LargeItemsEncountered = value;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x000305FC File Offset: 0x0002E7FC
		// (set) Token: 0x060015BE RID: 5566 RVA: 0x00030604 File Offset: 0x0002E804
		public new bool AllowLargeItems
		{
			get
			{
				return base.AllowLargeItems;
			}
			internal set
			{
				base.AllowLargeItems = value;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x060015BF RID: 5567 RVA: 0x0003060D File Offset: 0x0002E80D
		public DateTime? QueuedTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Creation);
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x0003061B File Offset: 0x0002E81B
		public DateTime? StartTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Start);
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x060015C1 RID: 5569 RVA: 0x00030629 File Offset: 0x0002E829
		public DateTime? LastUpdateTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.LastUpdate);
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x060015C2 RID: 5570 RVA: 0x00030637 File Offset: 0x0002E837
		public DateTime? InitialSeedingCompletedTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.InitialSeedingCompleted);
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x00030645 File Offset: 0x0002E845
		public DateTime? FinalSyncTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.FinalSync);
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x060015C4 RID: 5572 RVA: 0x00030653 File Offset: 0x0002E853
		public DateTime? CompletionTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Completion);
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x060015C5 RID: 5573 RVA: 0x00030661 File Offset: 0x0002E861
		public DateTime? SuspendedTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Suspended);
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x060015C6 RID: 5574 RVA: 0x0003066F File Offset: 0x0002E86F
		public EnhancedTimeSpan? OverallDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.OverallMove).Duration);
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x060015C7 RID: 5575 RVA: 0x0003068C File Offset: 0x0002E88C
		public EnhancedTimeSpan? TotalFinalizationDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Finalization).Duration);
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x060015C8 RID: 5576 RVA: 0x000306AA File Offset: 0x0002E8AA
		public EnhancedTimeSpan? TotalSuspendedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Suspended).Duration);
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x060015C9 RID: 5577 RVA: 0x000306C8 File Offset: 0x0002E8C8
		public EnhancedTimeSpan? TotalFailedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Failed).Duration);
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x060015CA RID: 5578 RVA: 0x000306E6 File Offset: 0x0002E8E6
		public EnhancedTimeSpan? TotalQueuedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Queued).Duration);
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x060015CB RID: 5579 RVA: 0x00030703 File Offset: 0x0002E903
		public EnhancedTimeSpan? TotalInProgressDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.InProgress).Duration);
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x060015CC RID: 5580 RVA: 0x00030720 File Offset: 0x0002E920
		public EnhancedTimeSpan? TotalStalledDueToCIDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToCI).Duration);
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x060015CD RID: 5581 RVA: 0x0003073E File Offset: 0x0002E93E
		public EnhancedTimeSpan? TotalStalledDueToHADuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToHA).Duration);
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x060015CE RID: 5582 RVA: 0x0003075C File Offset: 0x0002E95C
		public EnhancedTimeSpan? TotalStalledDueToMailboxLockedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToMailboxLock).Duration);
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x060015CF RID: 5583 RVA: 0x0003077A File Offset: 0x0002E97A
		public EnhancedTimeSpan? TotalStalledDueToReadThrottle
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadThrottle).Duration);
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x060015D0 RID: 5584 RVA: 0x00030798 File Offset: 0x0002E998
		public EnhancedTimeSpan? TotalStalledDueToWriteThrottle
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteThrottle).Duration);
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x000307B6 File Offset: 0x0002E9B6
		public EnhancedTimeSpan? TotalStalledDueToReadCpu
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadCpu).Duration);
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x060015D2 RID: 5586 RVA: 0x000307D4 File Offset: 0x0002E9D4
		public EnhancedTimeSpan? TotalStalledDueToWriteCpu
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteCpu).Duration);
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x060015D3 RID: 5587 RVA: 0x000307F2 File Offset: 0x0002E9F2
		public EnhancedTimeSpan? TotalStalledDueToReadUnknown
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadUnknown).Duration);
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x00030810 File Offset: 0x0002EA10
		public EnhancedTimeSpan? TotalStalledDueToWriteUnknown
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteUnknown).Duration);
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x060015D5 RID: 5589 RVA: 0x0003082E File Offset: 0x0002EA2E
		public EnhancedTimeSpan? TotalTransientFailureDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.TransientFailure).Duration);
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x060015D6 RID: 5590 RVA: 0x0003084C File Offset: 0x0002EA4C
		public EnhancedTimeSpan? TotalIdleDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Idle).Duration);
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x0003086A File Offset: 0x0002EA6A
		// (set) Token: 0x060015D8 RID: 5592 RVA: 0x00030872 File Offset: 0x0002EA72
		public new string MRSServerName
		{
			get
			{
				return base.MRSServerName;
			}
			internal set
			{
				base.MRSServerName = value;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x060015D9 RID: 5593 RVA: 0x0003087B File Offset: 0x0002EA7B
		// (set) Token: 0x060015DA RID: 5594 RVA: 0x00030888 File Offset: 0x0002EA88
		public ByteQuantifiedSize EstimatedTransferSize
		{
			get
			{
				return new ByteQuantifiedSize(base.TotalMailboxSize);
			}
			internal set
			{
				base.TotalMailboxSize = value.ToBytes();
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x060015DB RID: 5595 RVA: 0x00030897 File Offset: 0x0002EA97
		// (set) Token: 0x060015DC RID: 5596 RVA: 0x0003089F File Offset: 0x0002EA9F
		public ulong EstimatedTransferItemCount
		{
			get
			{
				return base.TotalMailboxItemCount;
			}
			internal set
			{
				base.TotalMailboxItemCount = value;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x000308A8 File Offset: 0x0002EAA8
		public override ByteQuantifiedSize? BytesTransferred
		{
			get
			{
				return base.BytesTransferred;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x000308B0 File Offset: 0x0002EAB0
		public override ByteQuantifiedSize? BytesTransferredPerMinute
		{
			get
			{
				return base.BytesTransferredPerMinute;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x060015DF RID: 5599 RVA: 0x000308B8 File Offset: 0x0002EAB8
		public override ulong? ItemsTransferred
		{
			get
			{
				return base.ItemsTransferred;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x060015E0 RID: 5600 RVA: 0x000308C0 File Offset: 0x0002EAC0
		// (set) Token: 0x060015E1 RID: 5601 RVA: 0x000308C8 File Offset: 0x0002EAC8
		public new int PercentComplete
		{
			get
			{
				return base.PercentComplete;
			}
			internal set
			{
				base.PercentComplete = value;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x060015E2 RID: 5602 RVA: 0x000308D1 File Offset: 0x0002EAD1
		// (set) Token: 0x060015E3 RID: 5603 RVA: 0x000308D9 File Offset: 0x0002EAD9
		public new Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
		{
			get
			{
				return base.CompletedRequestAgeLimit;
			}
			internal set
			{
				base.CompletedRequestAgeLimit = value;
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x060015E4 RID: 5604 RVA: 0x000308E2 File Offset: 0x0002EAE2
		// (set) Token: 0x060015E5 RID: 5605 RVA: 0x000308EA File Offset: 0x0002EAEA
		public override LocalizedString PositionInQueue
		{
			get
			{
				return base.PositionInQueue;
			}
			internal set
			{
				base.PositionInQueue = value;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x060015E6 RID: 5606 RVA: 0x000308F3 File Offset: 0x0002EAF3
		// (set) Token: 0x060015E7 RID: 5607 RVA: 0x000308FB File Offset: 0x0002EAFB
		public new bool SuspendWhenReadyToComplete
		{
			get
			{
				return base.SuspendWhenReadyToComplete;
			}
			internal set
			{
				base.SuspendWhenReadyToComplete = value;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x00030904 File Offset: 0x0002EB04
		// (set) Token: 0x060015E9 RID: 5609 RVA: 0x0003090C File Offset: 0x0002EB0C
		public new int? FailureCode
		{
			get
			{
				return base.FailureCode;
			}
			internal set
			{
				base.FailureCode = value;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x00030915 File Offset: 0x0002EB15
		// (set) Token: 0x060015EB RID: 5611 RVA: 0x0003091D File Offset: 0x0002EB1D
		public new string FailureType
		{
			get
			{
				return base.FailureType;
			}
			internal set
			{
				base.FailureType = value;
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x060015EC RID: 5612 RVA: 0x00030926 File Offset: 0x0002EB26
		// (set) Token: 0x060015ED RID: 5613 RVA: 0x0003092E File Offset: 0x0002EB2E
		public new ExceptionSide? FailureSide
		{
			get
			{
				return base.FailureSide;
			}
			internal set
			{
				base.FailureSide = value;
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x00030937 File Offset: 0x0002EB37
		// (set) Token: 0x060015EF RID: 5615 RVA: 0x0003093F File Offset: 0x0002EB3F
		public new LocalizedString Message
		{
			get
			{
				return base.Message;
			}
			internal set
			{
				base.Message = value;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x00030948 File Offset: 0x0002EB48
		public DateTime? FailureTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Failure);
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x060015F1 RID: 5617 RVA: 0x00030957 File Offset: 0x0002EB57
		public override bool IsValid
		{
			get
			{
				return base.IsValid;
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x0003095F File Offset: 0x0002EB5F
		// (set) Token: 0x060015F3 RID: 5619 RVA: 0x00030967 File Offset: 0x0002EB67
		public new LocalizedString ValidationMessage
		{
			get
			{
				return base.ValidationMessage;
			}
			internal set
			{
				base.ValidationMessage = value;
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x00030970 File Offset: 0x0002EB70
		// (set) Token: 0x060015F5 RID: 5621 RVA: 0x00030978 File Offset: 0x0002EB78
		public new OrganizationId OrganizationId
		{
			get
			{
				return base.OrganizationId;
			}
			internal set
			{
				base.OrganizationId = value;
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x00030981 File Offset: 0x0002EB81
		// (set) Token: 0x060015F7 RID: 5623 RVA: 0x00030989 File Offset: 0x0002EB89
		public new Guid RequestGuid
		{
			get
			{
				return base.RequestGuid;
			}
			internal set
			{
				base.RequestGuid = value;
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x00030992 File Offset: 0x0002EB92
		// (set) Token: 0x060015F9 RID: 5625 RVA: 0x0003099A File Offset: 0x0002EB9A
		public new ADObjectId RequestQueue
		{
			get
			{
				return base.RequestQueue;
			}
			internal set
			{
				base.RequestQueue = value;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x000309A3 File Offset: 0x0002EBA3
		// (set) Token: 0x060015FB RID: 5627 RVA: 0x000309AB File Offset: 0x0002EBAB
		public new ObjectId Identity
		{
			get
			{
				return base.Identity;
			}
			internal set
			{
				base.Identity = (value as RequestJobObjectId);
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x060015FC RID: 5628 RVA: 0x000309B9 File Offset: 0x0002EBB9
		public new string DiagnosticInfo
		{
			get
			{
				return base.DiagnosticInfo;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x060015FD RID: 5629 RVA: 0x000309C1 File Offset: 0x0002EBC1
		// (set) Token: 0x060015FE RID: 5630 RVA: 0x000309C9 File Offset: 0x0002EBC9
		public override Report Report
		{
			get
			{
				return base.Report;
			}
			internal set
			{
				base.Report = value;
			}
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x000309D4 File Offset: 0x0002EBD4
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.Name) || (string.IsNullOrEmpty(base.TargetAlias) && string.IsNullOrEmpty(base.SourceAlias)))
			{
				return base.ToString();
			}
			if (!string.IsNullOrEmpty(base.TargetAlias))
			{
				return string.Format("{0}\\{1}", base.TargetAlias, this.Name);
			}
			return string.Format("{0}\\{1}", base.SourceAlias, this.Name);
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x00030A4C File Offset: 0x0002EC4C
		internal static void ValidateRequestJob(RequestJobBase requestJob)
		{
			if (requestJob.IsFake || requestJob.WorkItemQueueMdb == null)
			{
				requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.DataMissing);
				requestJob.ValidationMessage = MrsStrings.ValidationMoveRequestNotDeserialized;
				return;
			}
			if (requestJob.OriginatingMDBGuid != Guid.Empty && requestJob.OriginatingMDBGuid != requestJob.WorkItemQueueMdb.ObjectGuid)
			{
				requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.Orphaned);
				requestJob.ValidationMessage = MrsStrings.ValidationMoveRequestInWrongMDB(requestJob.OriginatingMDBGuid, requestJob.WorkItemQueueMdb.ObjectGuid);
				return;
			}
			if (requestJob.CancelRequest)
			{
				requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.Valid);
				requestJob.ValidationMessage = LocalizedString.Empty;
				return;
			}
			if (requestJob.Status == RequestStatus.Completed || requestJob.Status == RequestStatus.CompletedWithWarning)
			{
				FolderMoveRequestStatistics.LoadAdditionalPropertiesFromUser(requestJob);
				requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.Valid);
				requestJob.ValidationMessage = LocalizedString.Empty;
				return;
			}
			if (!requestJob.ValidateUser(requestJob.SourceUser, requestJob.SourceUserId))
			{
				return;
			}
			if (!requestJob.ValidateMailbox(requestJob.SourceUser, requestJob.SourceIsArchive))
			{
				return;
			}
			if (!requestJob.ValidateUser(requestJob.TargetUser, requestJob.TargetUserId))
			{
				return;
			}
			if (!requestJob.ValidateMailbox(requestJob.TargetUser, requestJob.TargetIsArchive))
			{
				return;
			}
			if (!requestJob.ValidateRequestIndexEntries())
			{
				return;
			}
			requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.Valid);
			requestJob.ValidationMessage = LocalizedString.Empty;
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x00030B9C File Offset: 0x0002ED9C
		private static void LoadAdditionalPropertiesFromUser(RequestJobBase requestJob)
		{
			if (requestJob.TargetUser != null)
			{
				requestJob.TargetAlias = requestJob.TargetUser.Alias;
				requestJob.TargetExchangeGuid = (requestJob.TargetIsArchive ? requestJob.TargetUser.ArchiveGuid : requestJob.TargetUser.ExchangeGuid);
				requestJob.TargetDatabase = ADObjectIdResolutionHelper.ResolveDN(requestJob.TargetIsArchive ? requestJob.TargetUser.ArchiveDatabase : requestJob.TargetUser.Database);
				requestJob.RecipientTypeDetails = requestJob.TargetUser.RecipientTypeDetails;
				requestJob.TargetUserId = requestJob.TargetUser.Id;
			}
		}
	}
}
