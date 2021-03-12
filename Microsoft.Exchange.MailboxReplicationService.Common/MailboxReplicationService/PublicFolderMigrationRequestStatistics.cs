using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000200 RID: 512
	[Serializable]
	public sealed class PublicFolderMigrationRequestStatistics : RequestStatisticsBase
	{
		// Token: 0x060019E0 RID: 6624 RVA: 0x00034FDE File Offset: 0x000331DE
		public PublicFolderMigrationRequestStatistics()
		{
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x00034FE6 File Offset: 0x000331E6
		internal PublicFolderMigrationRequestStatistics(SimpleProviderPropertyBag propertyBag) : base(propertyBag)
		{
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x00034FEF File Offset: 0x000331EF
		internal PublicFolderMigrationRequestStatistics(TransactionalRequestJob requestJob) : this((SimpleProviderPropertyBag)requestJob.propertyBag)
		{
			base.CopyNonSchematizedPropertiesFrom(requestJob);
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x00035009 File Offset: 0x00033209
		internal PublicFolderMigrationRequestStatistics(RequestJobXML requestJob) : this((SimpleProviderPropertyBag)requestJob.propertyBag)
		{
			base.CopyNonSchematizedPropertiesFrom(requestJob);
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x060019E4 RID: 6628 RVA: 0x00035023 File Offset: 0x00033223
		// (set) Token: 0x060019E5 RID: 6629 RVA: 0x0003502B File Offset: 0x0003322B
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

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x060019E6 RID: 6630 RVA: 0x00035034 File Offset: 0x00033234
		// (set) Token: 0x060019E7 RID: 6631 RVA: 0x0003503C File Offset: 0x0003323C
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

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x060019E8 RID: 6632 RVA: 0x00035045 File Offset: 0x00033245
		public new RequestState StatusDetail
		{
			get
			{
				return base.StatusDetail;
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x060019E9 RID: 6633 RVA: 0x0003504D File Offset: 0x0003324D
		// (set) Token: 0x060019EA RID: 6634 RVA: 0x00035055 File Offset: 0x00033255
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

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x060019EB RID: 6635 RVA: 0x0003505E File Offset: 0x0003325E
		// (set) Token: 0x060019EC RID: 6636 RVA: 0x00035066 File Offset: 0x00033266
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

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x060019ED RID: 6637 RVA: 0x0003506F File Offset: 0x0003326F
		// (set) Token: 0x060019EE RID: 6638 RVA: 0x00035077 File Offset: 0x00033277
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

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x060019EF RID: 6639 RVA: 0x00035080 File Offset: 0x00033280
		// (set) Token: 0x060019F0 RID: 6640 RVA: 0x00035088 File Offset: 0x00033288
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

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x060019F1 RID: 6641 RVA: 0x00035091 File Offset: 0x00033291
		// (set) Token: 0x060019F2 RID: 6642 RVA: 0x00035099 File Offset: 0x00033299
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

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x060019F3 RID: 6643 RVA: 0x000350A2 File Offset: 0x000332A2
		// (set) Token: 0x060019F4 RID: 6644 RVA: 0x000350AA File Offset: 0x000332AA
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

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x060019F5 RID: 6645 RVA: 0x000350B3 File Offset: 0x000332B3
		// (set) Token: 0x060019F6 RID: 6646 RVA: 0x000350BB File Offset: 0x000332BB
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

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x060019F7 RID: 6647 RVA: 0x000350C4 File Offset: 0x000332C4
		// (set) Token: 0x060019F8 RID: 6648 RVA: 0x000350D1 File Offset: 0x000332D1
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

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x060019F9 RID: 6649 RVA: 0x000350DF File Offset: 0x000332DF
		// (set) Token: 0x060019FA RID: 6650 RVA: 0x000350E7 File Offset: 0x000332E7
		public new ADObjectId SourceDatabase
		{
			get
			{
				return base.SourceDatabase;
			}
			internal set
			{
				base.SourceDatabase = value;
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x060019FB RID: 6651 RVA: 0x000350F0 File Offset: 0x000332F0
		// (set) Token: 0x060019FC RID: 6652 RVA: 0x000350F8 File Offset: 0x000332F8
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

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x060019FD RID: 6653 RVA: 0x00035101 File Offset: 0x00033301
		// (set) Token: 0x060019FE RID: 6654 RVA: 0x00035109 File Offset: 0x00033309
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

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x060019FF RID: 6655 RVA: 0x00035112 File Offset: 0x00033312
		// (set) Token: 0x06001A00 RID: 6656 RVA: 0x0003511A File Offset: 0x0003331A
		public new string OutlookAnywhereHostName
		{
			get
			{
				return base.OutlookAnywhereHostName;
			}
			internal set
			{
				base.OutlookAnywhereHostName = value;
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06001A01 RID: 6657 RVA: 0x00035123 File Offset: 0x00033323
		// (set) Token: 0x06001A02 RID: 6658 RVA: 0x0003512B File Offset: 0x0003332B
		public new string RemoteCredentialUsername
		{
			get
			{
				return base.RemoteCredentialUsername;
			}
			internal set
			{
				base.RemoteCredentialUsername = value;
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06001A03 RID: 6659 RVA: 0x00035134 File Offset: 0x00033334
		// (set) Token: 0x06001A04 RID: 6660 RVA: 0x0003513C File Offset: 0x0003333C
		public new AuthenticationMethod? AuthenticationMethod
		{
			get
			{
				return base.AuthenticationMethod;
			}
			internal set
			{
				base.AuthenticationMethod = value;
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06001A05 RID: 6661 RVA: 0x00035145 File Offset: 0x00033345
		// (set) Token: 0x06001A06 RID: 6662 RVA: 0x0003514D File Offset: 0x0003334D
		public new string RemoteMailboxLegacyDN
		{
			get
			{
				return base.RemoteMailboxLegacyDN;
			}
			internal set
			{
				base.RemoteMailboxLegacyDN = value;
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06001A07 RID: 6663 RVA: 0x00035156 File Offset: 0x00033356
		// (set) Token: 0x06001A08 RID: 6664 RVA: 0x0003515E File Offset: 0x0003335E
		public new string RemoteMailboxServerLegacyDN
		{
			get
			{
				return base.RemoteMailboxServerLegacyDN;
			}
			internal set
			{
				base.RemoteMailboxServerLegacyDN = value;
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06001A09 RID: 6665 RVA: 0x00035167 File Offset: 0x00033367
		// (set) Token: 0x06001A0A RID: 6666 RVA: 0x0003516F File Offset: 0x0003336F
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

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06001A0B RID: 6667 RVA: 0x00035178 File Offset: 0x00033378
		// (set) Token: 0x06001A0C RID: 6668 RVA: 0x00035180 File Offset: 0x00033380
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

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06001A0D RID: 6669 RVA: 0x00035189 File Offset: 0x00033389
		// (set) Token: 0x06001A0E RID: 6670 RVA: 0x00035191 File Offset: 0x00033391
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

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06001A0F RID: 6671 RVA: 0x0003519A File Offset: 0x0003339A
		// (set) Token: 0x06001A10 RID: 6672 RVA: 0x000351A2 File Offset: 0x000333A2
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

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06001A11 RID: 6673 RVA: 0x000351AB File Offset: 0x000333AB
		// (set) Token: 0x06001A12 RID: 6674 RVA: 0x000351B3 File Offset: 0x000333B3
		public new List<FolderToMailboxMapping> FolderToMailboxMap
		{
			get
			{
				return base.FolderToMailboxMap;
			}
			internal set
			{
				base.FolderToMailboxMap = value;
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06001A13 RID: 6675 RVA: 0x000351BC File Offset: 0x000333BC
		public DateTime? QueuedTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Creation);
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06001A14 RID: 6676 RVA: 0x000351CA File Offset: 0x000333CA
		public DateTime? StartTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Start);
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06001A15 RID: 6677 RVA: 0x000351D8 File Offset: 0x000333D8
		public DateTime? LastUpdateTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.LastUpdate);
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06001A16 RID: 6678 RVA: 0x000351E6 File Offset: 0x000333E6
		public DateTime? InitialSeedingCompletedTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.InitialSeedingCompleted);
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06001A17 RID: 6679 RVA: 0x000351F4 File Offset: 0x000333F4
		public DateTime? FinalSyncTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.FinalSync);
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06001A18 RID: 6680 RVA: 0x00035202 File Offset: 0x00033402
		public DateTime? CompletionTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Completion);
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06001A19 RID: 6681 RVA: 0x00035210 File Offset: 0x00033410
		public DateTime? SuspendedTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Suspended);
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06001A1A RID: 6682 RVA: 0x0003521E File Offset: 0x0003341E
		public EnhancedTimeSpan? OverallDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.OverallMove).Duration);
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06001A1B RID: 6683 RVA: 0x0003523B File Offset: 0x0003343B
		public EnhancedTimeSpan? TotalFinalizationDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Finalization).Duration);
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06001A1C RID: 6684 RVA: 0x00035259 File Offset: 0x00033459
		public EnhancedTimeSpan? TotalDataReplicationWaitDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.DataReplicationWait).Duration);
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06001A1D RID: 6685 RVA: 0x00035277 File Offset: 0x00033477
		public EnhancedTimeSpan? TotalSuspendedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Suspended).Duration);
			}
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06001A1E RID: 6686 RVA: 0x00035295 File Offset: 0x00033495
		public EnhancedTimeSpan? TotalFailedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Failed).Duration);
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06001A1F RID: 6687 RVA: 0x000352B3 File Offset: 0x000334B3
		public EnhancedTimeSpan? TotalQueuedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Queued).Duration);
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06001A20 RID: 6688 RVA: 0x000352D0 File Offset: 0x000334D0
		public EnhancedTimeSpan? TotalInProgressDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.InProgress).Duration);
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06001A21 RID: 6689 RVA: 0x000352ED File Offset: 0x000334ED
		public EnhancedTimeSpan? TotalStalledDueToCIDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToCI).Duration);
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06001A22 RID: 6690 RVA: 0x0003530B File Offset: 0x0003350B
		public EnhancedTimeSpan? TotalStalledDueToHADuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToHA).Duration);
			}
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06001A23 RID: 6691 RVA: 0x00035329 File Offset: 0x00033529
		public EnhancedTimeSpan? TotalStalledDueToReadThrottle
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadThrottle).Duration);
			}
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06001A24 RID: 6692 RVA: 0x00035347 File Offset: 0x00033547
		public EnhancedTimeSpan? TotalStalledDueToWriteThrottle
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteThrottle).Duration);
			}
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06001A25 RID: 6693 RVA: 0x00035365 File Offset: 0x00033565
		public EnhancedTimeSpan? TotalStalledDueToReadCpu
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadCpu).Duration);
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06001A26 RID: 6694 RVA: 0x00035383 File Offset: 0x00033583
		public EnhancedTimeSpan? TotalStalledDueToWriteCpu
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteCpu).Duration);
			}
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06001A27 RID: 6695 RVA: 0x000353A1 File Offset: 0x000335A1
		public EnhancedTimeSpan? TotalStalledDueToReadUnknown
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadUnknown).Duration);
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06001A28 RID: 6696 RVA: 0x000353BF File Offset: 0x000335BF
		public EnhancedTimeSpan? TotalStalledDueToWriteUnknown
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteUnknown).Duration);
			}
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06001A29 RID: 6697 RVA: 0x000353DD File Offset: 0x000335DD
		public EnhancedTimeSpan? TotalTransientFailureDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.TransientFailure).Duration);
			}
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06001A2A RID: 6698 RVA: 0x000353FB File Offset: 0x000335FB
		public EnhancedTimeSpan? TotalIdleDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Idle).Duration);
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06001A2B RID: 6699 RVA: 0x00035419 File Offset: 0x00033619
		// (set) Token: 0x06001A2C RID: 6700 RVA: 0x00035421 File Offset: 0x00033621
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

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06001A2D RID: 6701 RVA: 0x0003542A File Offset: 0x0003362A
		// (set) Token: 0x06001A2E RID: 6702 RVA: 0x00035437 File Offset: 0x00033637
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

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06001A2F RID: 6703 RVA: 0x00035446 File Offset: 0x00033646
		// (set) Token: 0x06001A30 RID: 6704 RVA: 0x0003544E File Offset: 0x0003364E
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

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06001A31 RID: 6705 RVA: 0x00035457 File Offset: 0x00033657
		public override ByteQuantifiedSize? BytesTransferred
		{
			get
			{
				return base.BytesTransferred;
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06001A32 RID: 6706 RVA: 0x0003545F File Offset: 0x0003365F
		public override ByteQuantifiedSize? BytesTransferredPerMinute
		{
			get
			{
				return base.BytesTransferredPerMinute;
			}
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06001A33 RID: 6707 RVA: 0x00035467 File Offset: 0x00033667
		public override ulong? ItemsTransferred
		{
			get
			{
				return base.ItemsTransferred;
			}
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06001A34 RID: 6708 RVA: 0x0003546F File Offset: 0x0003366F
		// (set) Token: 0x06001A35 RID: 6709 RVA: 0x00035477 File Offset: 0x00033677
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

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06001A36 RID: 6710 RVA: 0x00035480 File Offset: 0x00033680
		// (set) Token: 0x06001A37 RID: 6711 RVA: 0x00035488 File Offset: 0x00033688
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

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06001A38 RID: 6712 RVA: 0x00035491 File Offset: 0x00033691
		// (set) Token: 0x06001A39 RID: 6713 RVA: 0x00035499 File Offset: 0x00033699
		public new bool PreventCompletion
		{
			get
			{
				return base.PreventCompletion;
			}
			internal set
			{
				base.PreventCompletion = value;
			}
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06001A3A RID: 6714 RVA: 0x000354A2 File Offset: 0x000336A2
		// (set) Token: 0x06001A3B RID: 6715 RVA: 0x000354AA File Offset: 0x000336AA
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

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06001A3C RID: 6716 RVA: 0x000354B3 File Offset: 0x000336B3
		// (set) Token: 0x06001A3D RID: 6717 RVA: 0x000354BB File Offset: 0x000336BB
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

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06001A3E RID: 6718 RVA: 0x000354C4 File Offset: 0x000336C4
		// (set) Token: 0x06001A3F RID: 6719 RVA: 0x000354CC File Offset: 0x000336CC
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

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06001A40 RID: 6720 RVA: 0x000354D5 File Offset: 0x000336D5
		// (set) Token: 0x06001A41 RID: 6721 RVA: 0x000354DD File Offset: 0x000336DD
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

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06001A42 RID: 6722 RVA: 0x000354E6 File Offset: 0x000336E6
		public DateTime? FailureTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Failure);
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06001A43 RID: 6723 RVA: 0x000354F5 File Offset: 0x000336F5
		public override bool IsValid
		{
			get
			{
				return base.IsValid;
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06001A44 RID: 6724 RVA: 0x000354FD File Offset: 0x000336FD
		// (set) Token: 0x06001A45 RID: 6725 RVA: 0x00035505 File Offset: 0x00033705
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

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06001A46 RID: 6726 RVA: 0x0003550E File Offset: 0x0003370E
		// (set) Token: 0x06001A47 RID: 6727 RVA: 0x00035516 File Offset: 0x00033716
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

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06001A48 RID: 6728 RVA: 0x0003551F File Offset: 0x0003371F
		// (set) Token: 0x06001A49 RID: 6729 RVA: 0x00035527 File Offset: 0x00033727
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

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06001A4A RID: 6730 RVA: 0x00035530 File Offset: 0x00033730
		// (set) Token: 0x06001A4B RID: 6731 RVA: 0x00035538 File Offset: 0x00033738
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

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06001A4C RID: 6732 RVA: 0x00035541 File Offset: 0x00033741
		// (set) Token: 0x06001A4D RID: 6733 RVA: 0x00035549 File Offset: 0x00033749
		public new Guid ExchangeGuid
		{
			get
			{
				return base.ExchangeGuid;
			}
			internal set
			{
				base.ExchangeGuid = value;
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06001A4E RID: 6734 RVA: 0x00035552 File Offset: 0x00033752
		// (set) Token: 0x06001A4F RID: 6735 RVA: 0x0003555A File Offset: 0x0003375A
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

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06001A50 RID: 6736 RVA: 0x00035568 File Offset: 0x00033768
		public new string DiagnosticInfo
		{
			get
			{
				return base.DiagnosticInfo;
			}
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06001A51 RID: 6737 RVA: 0x00035570 File Offset: 0x00033770
		// (set) Token: 0x06001A52 RID: 6738 RVA: 0x00035578 File Offset: 0x00033778
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

		// Token: 0x06001A53 RID: 6739 RVA: 0x00035584 File Offset: 0x00033784
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

		// Token: 0x06001A54 RID: 6740 RVA: 0x000355FC File Offset: 0x000337FC
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
