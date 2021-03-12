using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001FF RID: 511
	[Serializable]
	public sealed class PublicFolderMoveRequestStatistics : RequestStatisticsBase
	{
		// Token: 0x06001969 RID: 6505 RVA: 0x000348C1 File Offset: 0x00032AC1
		public PublicFolderMoveRequestStatistics()
		{
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x000348C9 File Offset: 0x00032AC9
		internal PublicFolderMoveRequestStatistics(SimpleProviderPropertyBag propertyBag) : base(propertyBag)
		{
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x000348D2 File Offset: 0x00032AD2
		internal PublicFolderMoveRequestStatistics(TransactionalRequestJob requestJob) : this((SimpleProviderPropertyBag)requestJob.propertyBag)
		{
			base.CopyNonSchematizedPropertiesFrom(requestJob);
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x000348EC File Offset: 0x00032AEC
		internal PublicFolderMoveRequestStatistics(RequestJobXML requestJob) : this((SimpleProviderPropertyBag)requestJob.propertyBag)
		{
			base.CopyNonSchematizedPropertiesFrom(requestJob);
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x0600196D RID: 6509 RVA: 0x00034906 File Offset: 0x00032B06
		// (set) Token: 0x0600196E RID: 6510 RVA: 0x0003490E File Offset: 0x00032B0E
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

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x0600196F RID: 6511 RVA: 0x00034917 File Offset: 0x00032B17
		// (set) Token: 0x06001970 RID: 6512 RVA: 0x0003491F File Offset: 0x00032B1F
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

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06001971 RID: 6513 RVA: 0x00034928 File Offset: 0x00032B28
		public new RequestState StatusDetail
		{
			get
			{
				return base.StatusDetail;
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06001972 RID: 6514 RVA: 0x00034930 File Offset: 0x00032B30
		// (set) Token: 0x06001973 RID: 6515 RVA: 0x00034938 File Offset: 0x00032B38
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

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06001974 RID: 6516 RVA: 0x00034941 File Offset: 0x00032B41
		// (set) Token: 0x06001975 RID: 6517 RVA: 0x00034949 File Offset: 0x00032B49
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

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06001976 RID: 6518 RVA: 0x00034952 File Offset: 0x00032B52
		// (set) Token: 0x06001977 RID: 6519 RVA: 0x0003495A File Offset: 0x00032B5A
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

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06001978 RID: 6520 RVA: 0x00034963 File Offset: 0x00032B63
		// (set) Token: 0x06001979 RID: 6521 RVA: 0x0003496B File Offset: 0x00032B6B
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

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x0600197A RID: 6522 RVA: 0x00034974 File Offset: 0x00032B74
		// (set) Token: 0x0600197B RID: 6523 RVA: 0x0003497C File Offset: 0x00032B7C
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

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x0600197C RID: 6524 RVA: 0x00034985 File Offset: 0x00032B85
		// (set) Token: 0x0600197D RID: 6525 RVA: 0x0003498D File Offset: 0x00032B8D
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

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x0600197E RID: 6526 RVA: 0x00034996 File Offset: 0x00032B96
		// (set) Token: 0x0600197F RID: 6527 RVA: 0x0003499E File Offset: 0x00032B9E
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

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06001980 RID: 6528 RVA: 0x000349A7 File Offset: 0x00032BA7
		// (set) Token: 0x06001981 RID: 6529 RVA: 0x000349AF File Offset: 0x00032BAF
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

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06001982 RID: 6530 RVA: 0x000349B8 File Offset: 0x00032BB8
		// (set) Token: 0x06001983 RID: 6531 RVA: 0x000349C0 File Offset: 0x00032BC0
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

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06001984 RID: 6532 RVA: 0x000349C9 File Offset: 0x00032BC9
		// (set) Token: 0x06001985 RID: 6533 RVA: 0x000349D6 File Offset: 0x00032BD6
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

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06001986 RID: 6534 RVA: 0x000349E4 File Offset: 0x00032BE4
		// (set) Token: 0x06001987 RID: 6535 RVA: 0x000349EC File Offset: 0x00032BEC
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

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06001988 RID: 6536 RVA: 0x000349F5 File Offset: 0x00032BF5
		public ADObjectId SourceMailbox
		{
			get
			{
				return base.SourceUserId;
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06001989 RID: 6537 RVA: 0x000349FD File Offset: 0x00032BFD
		// (set) Token: 0x0600198A RID: 6538 RVA: 0x00034A0A File Offset: 0x00032C0A
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

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x0600198B RID: 6539 RVA: 0x00034A18 File Offset: 0x00032C18
		// (set) Token: 0x0600198C RID: 6540 RVA: 0x00034A20 File Offset: 0x00032C20
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

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x0600198D RID: 6541 RVA: 0x00034A29 File Offset: 0x00032C29
		public ADObjectId TargetMailbox
		{
			get
			{
				return base.TargetUserId;
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x0600198E RID: 6542 RVA: 0x00034A31 File Offset: 0x00032C31
		private new string RemoteGlobalCatalog
		{
			get
			{
				return base.RemoteGlobalCatalog;
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x0600198F RID: 6543 RVA: 0x00034A39 File Offset: 0x00032C39
		// (set) Token: 0x06001990 RID: 6544 RVA: 0x00034A41 File Offset: 0x00032C41
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

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06001991 RID: 6545 RVA: 0x00034A4A File Offset: 0x00032C4A
		private new string RemoteCredentialUsername
		{
			get
			{
				return base.RemoteCredentialUsername;
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06001992 RID: 6546 RVA: 0x00034A52 File Offset: 0x00032C52
		private new string RemoteDatabaseName
		{
			get
			{
				return base.RemoteDatabaseName;
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06001993 RID: 6547 RVA: 0x00034A5A File Offset: 0x00032C5A
		private new Guid? RemoteDatabaseGuid
		{
			get
			{
				return base.RemoteDatabaseGuid;
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06001994 RID: 6548 RVA: 0x00034A62 File Offset: 0x00032C62
		// (set) Token: 0x06001995 RID: 6549 RVA: 0x00034A6A File Offset: 0x00032C6A
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

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06001996 RID: 6550 RVA: 0x00034A73 File Offset: 0x00032C73
		// (set) Token: 0x06001997 RID: 6551 RVA: 0x00034A7B File Offset: 0x00032C7B
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

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06001998 RID: 6552 RVA: 0x00034A84 File Offset: 0x00032C84
		// (set) Token: 0x06001999 RID: 6553 RVA: 0x00034A8C File Offset: 0x00032C8C
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

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x0600199A RID: 6554 RVA: 0x00034A95 File Offset: 0x00032C95
		// (set) Token: 0x0600199B RID: 6555 RVA: 0x00034A9D File Offset: 0x00032C9D
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

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x0600199C RID: 6556 RVA: 0x00034AA6 File Offset: 0x00032CA6
		// (set) Token: 0x0600199D RID: 6557 RVA: 0x00034AAE File Offset: 0x00032CAE
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

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x0600199E RID: 6558 RVA: 0x00034AB7 File Offset: 0x00032CB7
		public DateTime? QueuedTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Creation);
			}
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x0600199F RID: 6559 RVA: 0x00034AC5 File Offset: 0x00032CC5
		public DateTime? StartTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Start);
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x060019A0 RID: 6560 RVA: 0x00034AD3 File Offset: 0x00032CD3
		public DateTime? LastUpdateTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.LastUpdate);
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x060019A1 RID: 6561 RVA: 0x00034AE1 File Offset: 0x00032CE1
		public DateTime? InitialSeedingCompletedTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.InitialSeedingCompleted);
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x060019A2 RID: 6562 RVA: 0x00034AEF File Offset: 0x00032CEF
		public DateTime? FinalSyncTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.FinalSync);
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x060019A3 RID: 6563 RVA: 0x00034AFD File Offset: 0x00032CFD
		public DateTime? CompletionTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Completion);
			}
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x060019A4 RID: 6564 RVA: 0x00034B0B File Offset: 0x00032D0B
		public DateTime? SuspendedTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Suspended);
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x060019A5 RID: 6565 RVA: 0x00034B19 File Offset: 0x00032D19
		public EnhancedTimeSpan? OverallDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.OverallMove).Duration);
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x060019A6 RID: 6566 RVA: 0x00034B36 File Offset: 0x00032D36
		public EnhancedTimeSpan? TotalFinalizationDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Finalization).Duration);
			}
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x060019A7 RID: 6567 RVA: 0x00034B54 File Offset: 0x00032D54
		public EnhancedTimeSpan? TotalSuspendedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Suspended).Duration);
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x060019A8 RID: 6568 RVA: 0x00034B72 File Offset: 0x00032D72
		public EnhancedTimeSpan? TotalFailedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Failed).Duration);
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x060019A9 RID: 6569 RVA: 0x00034B90 File Offset: 0x00032D90
		public EnhancedTimeSpan? TotalQueuedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Queued).Duration);
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x060019AA RID: 6570 RVA: 0x00034BAD File Offset: 0x00032DAD
		public EnhancedTimeSpan? TotalInProgressDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.InProgress).Duration);
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x060019AB RID: 6571 RVA: 0x00034BCA File Offset: 0x00032DCA
		public EnhancedTimeSpan? TotalStalledDueToCIDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToCI).Duration);
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x060019AC RID: 6572 RVA: 0x00034BE8 File Offset: 0x00032DE8
		public EnhancedTimeSpan? TotalStalledDueToHADuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToHA).Duration);
			}
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x060019AD RID: 6573 RVA: 0x00034C06 File Offset: 0x00032E06
		public EnhancedTimeSpan? TotalStalledDueToMailboxLockedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToMailboxLock).Duration);
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x060019AE RID: 6574 RVA: 0x00034C24 File Offset: 0x00032E24
		public EnhancedTimeSpan? TotalStalledDueToReadThrottle
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadThrottle).Duration);
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x060019AF RID: 6575 RVA: 0x00034C42 File Offset: 0x00032E42
		public EnhancedTimeSpan? TotalStalledDueToWriteThrottle
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteThrottle).Duration);
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x060019B0 RID: 6576 RVA: 0x00034C60 File Offset: 0x00032E60
		public EnhancedTimeSpan? TotalStalledDueToReadCpu
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadCpu).Duration);
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x060019B1 RID: 6577 RVA: 0x00034C7E File Offset: 0x00032E7E
		public EnhancedTimeSpan? TotalStalledDueToWriteCpu
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteCpu).Duration);
			}
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x060019B2 RID: 6578 RVA: 0x00034C9C File Offset: 0x00032E9C
		public EnhancedTimeSpan? TotalStalledDueToReadUnknown
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadUnknown).Duration);
			}
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x060019B3 RID: 6579 RVA: 0x00034CBA File Offset: 0x00032EBA
		public EnhancedTimeSpan? TotalStalledDueToWriteUnknown
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteUnknown).Duration);
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x060019B4 RID: 6580 RVA: 0x00034CD8 File Offset: 0x00032ED8
		public EnhancedTimeSpan? TotalTransientFailureDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.TransientFailure).Duration);
			}
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x060019B5 RID: 6581 RVA: 0x00034CF6 File Offset: 0x00032EF6
		public EnhancedTimeSpan? TotalIdleDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Idle).Duration);
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x060019B6 RID: 6582 RVA: 0x00034D14 File Offset: 0x00032F14
		// (set) Token: 0x060019B7 RID: 6583 RVA: 0x00034D1C File Offset: 0x00032F1C
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

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x060019B8 RID: 6584 RVA: 0x00034D25 File Offset: 0x00032F25
		// (set) Token: 0x060019B9 RID: 6585 RVA: 0x00034D32 File Offset: 0x00032F32
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

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x060019BA RID: 6586 RVA: 0x00034D41 File Offset: 0x00032F41
		// (set) Token: 0x060019BB RID: 6587 RVA: 0x00034D49 File Offset: 0x00032F49
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

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x060019BC RID: 6588 RVA: 0x00034D52 File Offset: 0x00032F52
		public override ByteQuantifiedSize? BytesTransferred
		{
			get
			{
				return base.BytesTransferred;
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x060019BD RID: 6589 RVA: 0x00034D5A File Offset: 0x00032F5A
		public override ByteQuantifiedSize? BytesTransferredPerMinute
		{
			get
			{
				return base.BytesTransferredPerMinute;
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x060019BE RID: 6590 RVA: 0x00034D62 File Offset: 0x00032F62
		public override ulong? ItemsTransferred
		{
			get
			{
				return base.ItemsTransferred;
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x060019BF RID: 6591 RVA: 0x00034D6A File Offset: 0x00032F6A
		// (set) Token: 0x060019C0 RID: 6592 RVA: 0x00034D72 File Offset: 0x00032F72
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

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x060019C1 RID: 6593 RVA: 0x00034D7B File Offset: 0x00032F7B
		// (set) Token: 0x060019C2 RID: 6594 RVA: 0x00034D83 File Offset: 0x00032F83
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

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x060019C3 RID: 6595 RVA: 0x00034D8C File Offset: 0x00032F8C
		// (set) Token: 0x060019C4 RID: 6596 RVA: 0x00034D94 File Offset: 0x00032F94
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

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x060019C5 RID: 6597 RVA: 0x00034D9D File Offset: 0x00032F9D
		// (set) Token: 0x060019C6 RID: 6598 RVA: 0x00034DA5 File Offset: 0x00032FA5
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

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x060019C7 RID: 6599 RVA: 0x00034DAE File Offset: 0x00032FAE
		// (set) Token: 0x060019C8 RID: 6600 RVA: 0x00034DB6 File Offset: 0x00032FB6
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

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x060019C9 RID: 6601 RVA: 0x00034DBF File Offset: 0x00032FBF
		// (set) Token: 0x060019CA RID: 6602 RVA: 0x00034DC7 File Offset: 0x00032FC7
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

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x060019CB RID: 6603 RVA: 0x00034DD0 File Offset: 0x00032FD0
		// (set) Token: 0x060019CC RID: 6604 RVA: 0x00034DD8 File Offset: 0x00032FD8
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

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x060019CD RID: 6605 RVA: 0x00034DE1 File Offset: 0x00032FE1
		// (set) Token: 0x060019CE RID: 6606 RVA: 0x00034DE9 File Offset: 0x00032FE9
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

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x060019CF RID: 6607 RVA: 0x00034DF2 File Offset: 0x00032FF2
		public DateTime? FailureTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Failure);
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x060019D0 RID: 6608 RVA: 0x00034E01 File Offset: 0x00033001
		public override bool IsValid
		{
			get
			{
				return base.IsValid;
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x060019D1 RID: 6609 RVA: 0x00034E09 File Offset: 0x00033009
		// (set) Token: 0x060019D2 RID: 6610 RVA: 0x00034E11 File Offset: 0x00033011
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

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x060019D3 RID: 6611 RVA: 0x00034E1A File Offset: 0x0003301A
		// (set) Token: 0x060019D4 RID: 6612 RVA: 0x00034E22 File Offset: 0x00033022
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

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x060019D5 RID: 6613 RVA: 0x00034E2B File Offset: 0x0003302B
		// (set) Token: 0x060019D6 RID: 6614 RVA: 0x00034E33 File Offset: 0x00033033
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

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x060019D7 RID: 6615 RVA: 0x00034E3C File Offset: 0x0003303C
		// (set) Token: 0x060019D8 RID: 6616 RVA: 0x00034E44 File Offset: 0x00033044
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

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x060019D9 RID: 6617 RVA: 0x00034E4D File Offset: 0x0003304D
		// (set) Token: 0x060019DA RID: 6618 RVA: 0x00034E55 File Offset: 0x00033055
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

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x060019DB RID: 6619 RVA: 0x00034E63 File Offset: 0x00033063
		public new string DiagnosticInfo
		{
			get
			{
				return base.DiagnosticInfo;
			}
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x060019DC RID: 6620 RVA: 0x00034E6B File Offset: 0x0003306B
		// (set) Token: 0x060019DD RID: 6621 RVA: 0x00034E73 File Offset: 0x00033073
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

		// Token: 0x060019DE RID: 6622 RVA: 0x00034E7C File Offset: 0x0003307C
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

		// Token: 0x060019DF RID: 6623 RVA: 0x00034EF4 File Offset: 0x000330F4
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
			if (!requestJob.SourceIsLocal && !requestJob.ValidateOutlookAnywhereParams())
			{
				return;
			}
			if (!requestJob.TargetIsLocal && !requestJob.ValidateOutlookAnywhereParams())
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
	}
}
