using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000201 RID: 513
	[Serializable]
	public sealed class PublicFolderMailboxMigrationRequestStatistics : RequestStatisticsBase
	{
		// Token: 0x06001A55 RID: 6741 RVA: 0x000356E6 File Offset: 0x000338E6
		public PublicFolderMailboxMigrationRequestStatistics()
		{
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x000356EE File Offset: 0x000338EE
		internal PublicFolderMailboxMigrationRequestStatistics(SimpleProviderPropertyBag propertyBag) : base(propertyBag)
		{
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x000356F7 File Offset: 0x000338F7
		internal PublicFolderMailboxMigrationRequestStatistics(TransactionalRequestJob requestJob) : this((SimpleProviderPropertyBag)requestJob.propertyBag)
		{
			base.CopyNonSchematizedPropertiesFrom(requestJob);
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x00035711 File Offset: 0x00033911
		internal PublicFolderMailboxMigrationRequestStatistics(RequestJobXML requestJob) : this((SimpleProviderPropertyBag)requestJob.propertyBag)
		{
			base.CopyNonSchematizedPropertiesFrom(requestJob);
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06001A59 RID: 6745 RVA: 0x0003572B File Offset: 0x0003392B
		// (set) Token: 0x06001A5A RID: 6746 RVA: 0x00035733 File Offset: 0x00033933
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

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06001A5B RID: 6747 RVA: 0x0003573C File Offset: 0x0003393C
		// (set) Token: 0x06001A5C RID: 6748 RVA: 0x00035744 File Offset: 0x00033944
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

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06001A5D RID: 6749 RVA: 0x0003574D File Offset: 0x0003394D
		public new RequestState StatusDetail
		{
			get
			{
				return base.StatusDetail;
			}
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06001A5E RID: 6750 RVA: 0x00035755 File Offset: 0x00033955
		// (set) Token: 0x06001A5F RID: 6751 RVA: 0x0003575D File Offset: 0x0003395D
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

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06001A60 RID: 6752 RVA: 0x00035766 File Offset: 0x00033966
		// (set) Token: 0x06001A61 RID: 6753 RVA: 0x0003576E File Offset: 0x0003396E
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

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06001A62 RID: 6754 RVA: 0x00035777 File Offset: 0x00033977
		// (set) Token: 0x06001A63 RID: 6755 RVA: 0x0003577F File Offset: 0x0003397F
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

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06001A64 RID: 6756 RVA: 0x00035788 File Offset: 0x00033988
		// (set) Token: 0x06001A65 RID: 6757 RVA: 0x00035790 File Offset: 0x00033990
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

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06001A66 RID: 6758 RVA: 0x00035799 File Offset: 0x00033999
		// (set) Token: 0x06001A67 RID: 6759 RVA: 0x000357A1 File Offset: 0x000339A1
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

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06001A68 RID: 6760 RVA: 0x000357AA File Offset: 0x000339AA
		// (set) Token: 0x06001A69 RID: 6761 RVA: 0x000357B2 File Offset: 0x000339B2
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

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06001A6A RID: 6762 RVA: 0x000357BB File Offset: 0x000339BB
		// (set) Token: 0x06001A6B RID: 6763 RVA: 0x000357C3 File Offset: 0x000339C3
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

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06001A6C RID: 6764 RVA: 0x000357CC File Offset: 0x000339CC
		// (set) Token: 0x06001A6D RID: 6765 RVA: 0x000357D4 File Offset: 0x000339D4
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

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06001A6E RID: 6766 RVA: 0x000357DD File Offset: 0x000339DD
		// (set) Token: 0x06001A6F RID: 6767 RVA: 0x000357EA File Offset: 0x000339EA
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

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06001A70 RID: 6768 RVA: 0x000357F8 File Offset: 0x000339F8
		// (set) Token: 0x06001A71 RID: 6769 RVA: 0x00035800 File Offset: 0x00033A00
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

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06001A72 RID: 6770 RVA: 0x00035809 File Offset: 0x00033A09
		// (set) Token: 0x06001A73 RID: 6771 RVA: 0x00035811 File Offset: 0x00033A11
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

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06001A74 RID: 6772 RVA: 0x0003581A File Offset: 0x00033A1A
		// (set) Token: 0x06001A75 RID: 6773 RVA: 0x00035827 File Offset: 0x00033A27
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

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06001A76 RID: 6774 RVA: 0x00035835 File Offset: 0x00033A35
		// (set) Token: 0x06001A77 RID: 6775 RVA: 0x0003583D File Offset: 0x00033A3D
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

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06001A78 RID: 6776 RVA: 0x00035846 File Offset: 0x00033A46
		public ADObjectId TargetMailbox
		{
			get
			{
				return base.TargetUserId;
			}
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06001A79 RID: 6777 RVA: 0x0003584E File Offset: 0x00033A4E
		// (set) Token: 0x06001A7A RID: 6778 RVA: 0x00035856 File Offset: 0x00033A56
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

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06001A7B RID: 6779 RVA: 0x0003585F File Offset: 0x00033A5F
		private new string RemoteGlobalCatalog
		{
			get
			{
				return base.RemoteGlobalCatalog;
			}
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06001A7C RID: 6780 RVA: 0x00035867 File Offset: 0x00033A67
		// (set) Token: 0x06001A7D RID: 6781 RVA: 0x0003586F File Offset: 0x00033A6F
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

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06001A7E RID: 6782 RVA: 0x00035878 File Offset: 0x00033A78
		// (set) Token: 0x06001A7F RID: 6783 RVA: 0x00035880 File Offset: 0x00033A80
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

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06001A80 RID: 6784 RVA: 0x00035889 File Offset: 0x00033A89
		// (set) Token: 0x06001A81 RID: 6785 RVA: 0x00035891 File Offset: 0x00033A91
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

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06001A82 RID: 6786 RVA: 0x0003589A File Offset: 0x00033A9A
		// (set) Token: 0x06001A83 RID: 6787 RVA: 0x000358A2 File Offset: 0x00033AA2
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

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06001A84 RID: 6788 RVA: 0x000358AB File Offset: 0x00033AAB
		// (set) Token: 0x06001A85 RID: 6789 RVA: 0x000358B3 File Offset: 0x00033AB3
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

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06001A86 RID: 6790 RVA: 0x000358BC File Offset: 0x00033ABC
		// (set) Token: 0x06001A87 RID: 6791 RVA: 0x000358C4 File Offset: 0x00033AC4
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

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06001A88 RID: 6792 RVA: 0x000358CD File Offset: 0x00033ACD
		// (set) Token: 0x06001A89 RID: 6793 RVA: 0x000358D5 File Offset: 0x00033AD5
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

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06001A8A RID: 6794 RVA: 0x000358DE File Offset: 0x00033ADE
		// (set) Token: 0x06001A8B RID: 6795 RVA: 0x000358E6 File Offset: 0x00033AE6
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

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06001A8C RID: 6796 RVA: 0x000358EF File Offset: 0x00033AEF
		// (set) Token: 0x06001A8D RID: 6797 RVA: 0x000358F7 File Offset: 0x00033AF7
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

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06001A8E RID: 6798 RVA: 0x00035900 File Offset: 0x00033B00
		// (set) Token: 0x06001A8F RID: 6799 RVA: 0x00035908 File Offset: 0x00033B08
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

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06001A90 RID: 6800 RVA: 0x00035911 File Offset: 0x00033B11
		// (set) Token: 0x06001A91 RID: 6801 RVA: 0x00035919 File Offset: 0x00033B19
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

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06001A92 RID: 6802 RVA: 0x00035922 File Offset: 0x00033B22
		public DateTime? QueuedTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Creation);
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06001A93 RID: 6803 RVA: 0x00035930 File Offset: 0x00033B30
		public DateTime? StartTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Start);
			}
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06001A94 RID: 6804 RVA: 0x0003593E File Offset: 0x00033B3E
		public DateTime? LastUpdateTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.LastUpdate);
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06001A95 RID: 6805 RVA: 0x0003594C File Offset: 0x00033B4C
		public DateTime? InitialSeedingCompletedTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.InitialSeedingCompleted);
			}
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06001A96 RID: 6806 RVA: 0x0003595A File Offset: 0x00033B5A
		public DateTime? FinalSyncTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.FinalSync);
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06001A97 RID: 6807 RVA: 0x00035968 File Offset: 0x00033B68
		public DateTime? CompletionTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Completion);
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06001A98 RID: 6808 RVA: 0x00035976 File Offset: 0x00033B76
		public DateTime? SuspendedTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Suspended);
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06001A99 RID: 6809 RVA: 0x00035984 File Offset: 0x00033B84
		public EnhancedTimeSpan? OverallDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.OverallMove).Duration);
			}
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06001A9A RID: 6810 RVA: 0x000359A1 File Offset: 0x00033BA1
		public EnhancedTimeSpan? TotalFinalizationDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Finalization).Duration);
			}
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06001A9B RID: 6811 RVA: 0x000359BF File Offset: 0x00033BBF
		public EnhancedTimeSpan? TotalDataReplicationWaitDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.DataReplicationWait).Duration);
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06001A9C RID: 6812 RVA: 0x000359DD File Offset: 0x00033BDD
		public EnhancedTimeSpan? TotalSuspendedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Suspended).Duration);
			}
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06001A9D RID: 6813 RVA: 0x000359FB File Offset: 0x00033BFB
		public EnhancedTimeSpan? TotalFailedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Failed).Duration);
			}
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06001A9E RID: 6814 RVA: 0x00035A19 File Offset: 0x00033C19
		public EnhancedTimeSpan? TotalQueuedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Queued).Duration);
			}
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06001A9F RID: 6815 RVA: 0x00035A36 File Offset: 0x00033C36
		public EnhancedTimeSpan? TotalInProgressDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.InProgress).Duration);
			}
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06001AA0 RID: 6816 RVA: 0x00035A53 File Offset: 0x00033C53
		public EnhancedTimeSpan? TotalStalledDueToCIDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToCI).Duration);
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06001AA1 RID: 6817 RVA: 0x00035A71 File Offset: 0x00033C71
		public EnhancedTimeSpan? TotalStalledDueToHADuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToHA).Duration);
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x00035A8F File Offset: 0x00033C8F
		public EnhancedTimeSpan? TotalStalledDueToMailboxLockedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToMailboxLock).Duration);
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06001AA3 RID: 6819 RVA: 0x00035AAD File Offset: 0x00033CAD
		public EnhancedTimeSpan? TotalStalledDueToReadThrottle
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadThrottle).Duration);
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06001AA4 RID: 6820 RVA: 0x00035ACB File Offset: 0x00033CCB
		public EnhancedTimeSpan? TotalStalledDueToWriteThrottle
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteThrottle).Duration);
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06001AA5 RID: 6821 RVA: 0x00035AE9 File Offset: 0x00033CE9
		public EnhancedTimeSpan? TotalStalledDueToReadCpu
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadCpu).Duration);
			}
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06001AA6 RID: 6822 RVA: 0x00035B07 File Offset: 0x00033D07
		public EnhancedTimeSpan? TotalStalledDueToWriteCpu
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteCpu).Duration);
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06001AA7 RID: 6823 RVA: 0x00035B25 File Offset: 0x00033D25
		public EnhancedTimeSpan? TotalStalledDueToReadUnknown
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadUnknown).Duration);
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06001AA8 RID: 6824 RVA: 0x00035B43 File Offset: 0x00033D43
		public EnhancedTimeSpan? TotalStalledDueToWriteUnknown
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteUnknown).Duration);
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x00035B61 File Offset: 0x00033D61
		public EnhancedTimeSpan? TotalTransientFailureDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.TransientFailure).Duration);
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06001AAA RID: 6826 RVA: 0x00035B7F File Offset: 0x00033D7F
		public EnhancedTimeSpan? TotalIdleDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Idle).Duration);
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x00035B9D File Offset: 0x00033D9D
		// (set) Token: 0x06001AAC RID: 6828 RVA: 0x00035BA5 File Offset: 0x00033DA5
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

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06001AAD RID: 6829 RVA: 0x00035BAE File Offset: 0x00033DAE
		// (set) Token: 0x06001AAE RID: 6830 RVA: 0x00035BBB File Offset: 0x00033DBB
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

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x00035BCA File Offset: 0x00033DCA
		// (set) Token: 0x06001AB0 RID: 6832 RVA: 0x00035BD2 File Offset: 0x00033DD2
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

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x00035BDB File Offset: 0x00033DDB
		public override ByteQuantifiedSize? BytesTransferred
		{
			get
			{
				return base.BytesTransferred;
			}
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06001AB2 RID: 6834 RVA: 0x00035BE3 File Offset: 0x00033DE3
		public override ByteQuantifiedSize? BytesTransferredPerMinute
		{
			get
			{
				return base.BytesTransferredPerMinute;
			}
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x00035BEB File Offset: 0x00033DEB
		public override ulong? ItemsTransferred
		{
			get
			{
				return base.ItemsTransferred;
			}
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06001AB4 RID: 6836 RVA: 0x00035BF3 File Offset: 0x00033DF3
		// (set) Token: 0x06001AB5 RID: 6837 RVA: 0x00035BFB File Offset: 0x00033DFB
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

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06001AB6 RID: 6838 RVA: 0x00035C04 File Offset: 0x00033E04
		// (set) Token: 0x06001AB7 RID: 6839 RVA: 0x00035C0C File Offset: 0x00033E0C
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

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06001AB8 RID: 6840 RVA: 0x00035C15 File Offset: 0x00033E15
		// (set) Token: 0x06001AB9 RID: 6841 RVA: 0x00035C1D File Offset: 0x00033E1D
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

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06001ABA RID: 6842 RVA: 0x00035C26 File Offset: 0x00033E26
		// (set) Token: 0x06001ABB RID: 6843 RVA: 0x00035C2E File Offset: 0x00033E2E
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

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06001ABC RID: 6844 RVA: 0x00035C37 File Offset: 0x00033E37
		// (set) Token: 0x06001ABD RID: 6845 RVA: 0x00035C3F File Offset: 0x00033E3F
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

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06001ABE RID: 6846 RVA: 0x00035C48 File Offset: 0x00033E48
		// (set) Token: 0x06001ABF RID: 6847 RVA: 0x00035C50 File Offset: 0x00033E50
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

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06001AC0 RID: 6848 RVA: 0x00035C59 File Offset: 0x00033E59
		// (set) Token: 0x06001AC1 RID: 6849 RVA: 0x00035C61 File Offset: 0x00033E61
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

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06001AC2 RID: 6850 RVA: 0x00035C6A File Offset: 0x00033E6A
		// (set) Token: 0x06001AC3 RID: 6851 RVA: 0x00035C72 File Offset: 0x00033E72
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

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06001AC4 RID: 6852 RVA: 0x00035C7B File Offset: 0x00033E7B
		// (set) Token: 0x06001AC5 RID: 6853 RVA: 0x00035C83 File Offset: 0x00033E83
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

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06001AC6 RID: 6854 RVA: 0x00035C8C File Offset: 0x00033E8C
		public DateTime? FailureTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Failure);
			}
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06001AC7 RID: 6855 RVA: 0x00035C9B File Offset: 0x00033E9B
		public override bool IsValid
		{
			get
			{
				return base.IsValid;
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06001AC8 RID: 6856 RVA: 0x00035CA3 File Offset: 0x00033EA3
		// (set) Token: 0x06001AC9 RID: 6857 RVA: 0x00035CAB File Offset: 0x00033EAB
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

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06001ACA RID: 6858 RVA: 0x00035CB4 File Offset: 0x00033EB4
		// (set) Token: 0x06001ACB RID: 6859 RVA: 0x00035CBC File Offset: 0x00033EBC
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

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06001ACC RID: 6860 RVA: 0x00035CC5 File Offset: 0x00033EC5
		// (set) Token: 0x06001ACD RID: 6861 RVA: 0x00035CCD File Offset: 0x00033ECD
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

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06001ACE RID: 6862 RVA: 0x00035CD6 File Offset: 0x00033ED6
		// (set) Token: 0x06001ACF RID: 6863 RVA: 0x00035CDE File Offset: 0x00033EDE
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

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06001AD0 RID: 6864 RVA: 0x00035CE7 File Offset: 0x00033EE7
		// (set) Token: 0x06001AD1 RID: 6865 RVA: 0x00035CEF File Offset: 0x00033EEF
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

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06001AD2 RID: 6866 RVA: 0x00035CFD File Offset: 0x00033EFD
		public new string DiagnosticInfo
		{
			get
			{
				return base.DiagnosticInfo;
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06001AD3 RID: 6867 RVA: 0x00035D05 File Offset: 0x00033F05
		// (set) Token: 0x06001AD4 RID: 6868 RVA: 0x00035D0D File Offset: 0x00033F0D
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

		// Token: 0x06001AD5 RID: 6869 RVA: 0x00035D18 File Offset: 0x00033F18
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

		// Token: 0x06001AD6 RID: 6870 RVA: 0x00035D90 File Offset: 0x00033F90
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
