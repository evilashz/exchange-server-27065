using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001F9 RID: 505
	[Serializable]
	public sealed class MailboxImportRequestStatistics : RequestStatisticsBase
	{
		// Token: 0x06001689 RID: 5769 RVA: 0x00031458 File Offset: 0x0002F658
		public MailboxImportRequestStatistics()
		{
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x00031460 File Offset: 0x0002F660
		internal MailboxImportRequestStatistics(SimpleProviderPropertyBag propertyBag) : base(propertyBag)
		{
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00031469 File Offset: 0x0002F669
		internal MailboxImportRequestStatistics(TransactionalRequestJob requestJob) : this((SimpleProviderPropertyBag)requestJob.propertyBag)
		{
			base.CopyNonSchematizedPropertiesFrom(requestJob);
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x00031483 File Offset: 0x0002F683
		internal MailboxImportRequestStatistics(RequestJobXML requestJob) : this((SimpleProviderPropertyBag)requestJob.propertyBag)
		{
			base.CopyNonSchematizedPropertiesFrom(requestJob);
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x0003149D File Offset: 0x0002F69D
		// (set) Token: 0x0600168E RID: 5774 RVA: 0x000314A5 File Offset: 0x0002F6A5
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

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x0600168F RID: 5775 RVA: 0x000314AE File Offset: 0x0002F6AE
		// (set) Token: 0x06001690 RID: 5776 RVA: 0x000314B6 File Offset: 0x0002F6B6
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

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06001691 RID: 5777 RVA: 0x000314BF File Offset: 0x0002F6BF
		public new RequestState StatusDetail
		{
			get
			{
				return base.StatusDetail;
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06001692 RID: 5778 RVA: 0x000314C7 File Offset: 0x0002F6C7
		// (set) Token: 0x06001693 RID: 5779 RVA: 0x000314CF File Offset: 0x0002F6CF
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

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06001694 RID: 5780 RVA: 0x000314D8 File Offset: 0x0002F6D8
		// (set) Token: 0x06001695 RID: 5781 RVA: 0x000314E0 File Offset: 0x0002F6E0
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

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06001696 RID: 5782 RVA: 0x000314E9 File Offset: 0x0002F6E9
		// (set) Token: 0x06001697 RID: 5783 RVA: 0x000314F1 File Offset: 0x0002F6F1
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

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x000314FA File Offset: 0x0002F6FA
		// (set) Token: 0x06001699 RID: 5785 RVA: 0x00031502 File Offset: 0x0002F702
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

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x0600169A RID: 5786 RVA: 0x0003150B File Offset: 0x0002F70B
		// (set) Token: 0x0600169B RID: 5787 RVA: 0x00031513 File Offset: 0x0002F713
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

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x0600169C RID: 5788 RVA: 0x0003151C File Offset: 0x0002F71C
		// (set) Token: 0x0600169D RID: 5789 RVA: 0x00031524 File Offset: 0x0002F724
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

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x0003152D File Offset: 0x0002F72D
		// (set) Token: 0x0600169F RID: 5791 RVA: 0x00031535 File Offset: 0x0002F735
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

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x060016A0 RID: 5792 RVA: 0x0003153E File Offset: 0x0002F73E
		// (set) Token: 0x060016A1 RID: 5793 RVA: 0x00031546 File Offset: 0x0002F746
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

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x0003154F File Offset: 0x0002F74F
		// (set) Token: 0x060016A3 RID: 5795 RVA: 0x00031557 File Offset: 0x0002F757
		public new string FilePath
		{
			get
			{
				return base.FilePath;
			}
			internal set
			{
				base.FilePath = value;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x060016A4 RID: 5796 RVA: 0x00031560 File Offset: 0x0002F760
		// (set) Token: 0x060016A5 RID: 5797 RVA: 0x00031568 File Offset: 0x0002F768
		public new int? ContentCodePage
		{
			get
			{
				return base.ContentCodePage;
			}
			internal set
			{
				base.ContentCodePage = value;
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x060016A6 RID: 5798 RVA: 0x00031571 File Offset: 0x0002F771
		// (set) Token: 0x060016A7 RID: 5799 RVA: 0x00031579 File Offset: 0x0002F779
		public new string SourceRootFolder
		{
			get
			{
				return base.SourceRootFolder;
			}
			internal set
			{
				base.SourceRootFolder = value;
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x060016A8 RID: 5800 RVA: 0x00031582 File Offset: 0x0002F782
		// (set) Token: 0x060016A9 RID: 5801 RVA: 0x0003158A File Offset: 0x0002F78A
		public new string TargetAlias
		{
			get
			{
				return base.TargetAlias;
			}
			internal set
			{
				base.TargetAlias = value;
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x060016AA RID: 5802 RVA: 0x00031593 File Offset: 0x0002F793
		// (set) Token: 0x060016AB RID: 5803 RVA: 0x0003159B File Offset: 0x0002F79B
		public new bool TargetIsArchive
		{
			get
			{
				return base.TargetIsArchive;
			}
			internal set
			{
				base.TargetIsArchive = value;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x000315A4 File Offset: 0x0002F7A4
		// (set) Token: 0x060016AD RID: 5805 RVA: 0x000315AC File Offset: 0x0002F7AC
		public new Guid TargetExchangeGuid
		{
			get
			{
				return base.TargetExchangeGuid;
			}
			internal set
			{
				base.TargetExchangeGuid = value;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x000315B5 File Offset: 0x0002F7B5
		// (set) Token: 0x060016AF RID: 5807 RVA: 0x000315BD File Offset: 0x0002F7BD
		public new string TargetRootFolder
		{
			get
			{
				return base.TargetRootFolder;
			}
			internal set
			{
				base.TargetRootFolder = value;
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x060016B0 RID: 5808 RVA: 0x000315C6 File Offset: 0x0002F7C6
		// (set) Token: 0x060016B1 RID: 5809 RVA: 0x000315CE File Offset: 0x0002F7CE
		public new RecipientTypeDetails RecipientTypeDetails
		{
			get
			{
				return base.RecipientTypeDetails;
			}
			internal set
			{
				base.RecipientTypeDetails = value;
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x060016B2 RID: 5810 RVA: 0x000315D7 File Offset: 0x0002F7D7
		// (set) Token: 0x060016B3 RID: 5811 RVA: 0x000315E4 File Offset: 0x0002F7E4
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

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x060016B4 RID: 5812 RVA: 0x000315F2 File Offset: 0x0002F7F2
		// (set) Token: 0x060016B5 RID: 5813 RVA: 0x000315FA File Offset: 0x0002F7FA
		public new ADObjectId TargetDatabase
		{
			get
			{
				return base.TargetDatabase;
			}
			internal set
			{
				base.TargetDatabase = value;
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x00031603 File Offset: 0x0002F803
		// (set) Token: 0x060016B7 RID: 5815 RVA: 0x0003160B File Offset: 0x0002F80B
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

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x00031614 File Offset: 0x0002F814
		// (set) Token: 0x060016B9 RID: 5817 RVA: 0x0003161C File Offset: 0x0002F81C
		public ADObjectId TargetMailboxIdentity
		{
			get
			{
				return base.TargetUserId;
			}
			internal set
			{
				base.TargetUserId = value;
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x060016BA RID: 5818 RVA: 0x00031625 File Offset: 0x0002F825
		// (set) Token: 0x060016BB RID: 5819 RVA: 0x0003162D File Offset: 0x0002F82D
		public new string[] IncludeFolders
		{
			get
			{
				return base.IncludeFolders;
			}
			internal set
			{
				base.IncludeFolders = value;
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x060016BC RID: 5820 RVA: 0x00031636 File Offset: 0x0002F836
		// (set) Token: 0x060016BD RID: 5821 RVA: 0x0003163E File Offset: 0x0002F83E
		public new string[] ExcludeFolders
		{
			get
			{
				return base.ExcludeFolders;
			}
			internal set
			{
				base.ExcludeFolders = value;
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x060016BE RID: 5822 RVA: 0x00031647 File Offset: 0x0002F847
		// (set) Token: 0x060016BF RID: 5823 RVA: 0x0003164F File Offset: 0x0002F84F
		public new bool ExcludeDumpster
		{
			get
			{
				return base.ExcludeDumpster;
			}
			internal set
			{
				base.ExcludeDumpster = value;
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x060016C0 RID: 5824 RVA: 0x00031658 File Offset: 0x0002F858
		// (set) Token: 0x060016C1 RID: 5825 RVA: 0x00031660 File Offset: 0x0002F860
		public new ConflictResolutionOption? ConflictResolutionOption
		{
			get
			{
				return base.ConflictResolutionOption;
			}
			set
			{
				base.ConflictResolutionOption = value;
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x060016C2 RID: 5826 RVA: 0x00031669 File Offset: 0x0002F869
		// (set) Token: 0x060016C3 RID: 5827 RVA: 0x00031671 File Offset: 0x0002F871
		public new FAICopyOption? AssociatedMessagesCopyOption
		{
			get
			{
				return base.AssociatedMessagesCopyOption;
			}
			set
			{
				base.AssociatedMessagesCopyOption = value;
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x060016C4 RID: 5828 RVA: 0x0003167A File Offset: 0x0002F87A
		// (set) Token: 0x060016C5 RID: 5829 RVA: 0x00031682 File Offset: 0x0002F882
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

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x060016C6 RID: 5830 RVA: 0x0003168B File Offset: 0x0002F88B
		// (set) Token: 0x060016C7 RID: 5831 RVA: 0x00031693 File Offset: 0x0002F893
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

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x060016C8 RID: 5832 RVA: 0x0003169C File Offset: 0x0002F89C
		// (set) Token: 0x060016C9 RID: 5833 RVA: 0x000316A4 File Offset: 0x0002F8A4
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

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x060016CA RID: 5834 RVA: 0x000316AD File Offset: 0x0002F8AD
		// (set) Token: 0x060016CB RID: 5835 RVA: 0x000316B5 File Offset: 0x0002F8B5
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

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x060016CC RID: 5836 RVA: 0x000316BE File Offset: 0x0002F8BE
		// (set) Token: 0x060016CD RID: 5837 RVA: 0x000316C6 File Offset: 0x0002F8C6
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

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x060016CE RID: 5838 RVA: 0x000316CF File Offset: 0x0002F8CF
		public DateTime? QueuedTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Creation);
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x000316DD File Offset: 0x0002F8DD
		public DateTime? StartTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Start);
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x060016D0 RID: 5840 RVA: 0x000316EB File Offset: 0x0002F8EB
		public DateTime? LastUpdateTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.LastUpdate);
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x000316F9 File Offset: 0x0002F8F9
		public DateTime? CompletionTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Completion);
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x060016D2 RID: 5842 RVA: 0x00031707 File Offset: 0x0002F907
		public DateTime? SuspendedTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Suspended);
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x060016D3 RID: 5843 RVA: 0x00031715 File Offset: 0x0002F915
		public EnhancedTimeSpan? OverallDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.OverallMove).Duration);
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x060016D4 RID: 5844 RVA: 0x00031732 File Offset: 0x0002F932
		public EnhancedTimeSpan? TotalSuspendedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Suspended).Duration);
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x060016D5 RID: 5845 RVA: 0x00031750 File Offset: 0x0002F950
		public EnhancedTimeSpan? TotalFailedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Failed).Duration);
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x060016D6 RID: 5846 RVA: 0x0003176E File Offset: 0x0002F96E
		public EnhancedTimeSpan? TotalQueuedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Queued).Duration);
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x060016D7 RID: 5847 RVA: 0x0003178B File Offset: 0x0002F98B
		public EnhancedTimeSpan? TotalInProgressDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.InProgress).Duration);
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x060016D8 RID: 5848 RVA: 0x000317A8 File Offset: 0x0002F9A8
		public EnhancedTimeSpan? TotalStalledDueToCIDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToCI).Duration);
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x060016D9 RID: 5849 RVA: 0x000317C6 File Offset: 0x0002F9C6
		public EnhancedTimeSpan? TotalStalledDueToHADuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToHA).Duration);
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x060016DA RID: 5850 RVA: 0x000317E4 File Offset: 0x0002F9E4
		public EnhancedTimeSpan? TotalStalledDueToMailboxLockedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToMailboxLock).Duration);
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x060016DB RID: 5851 RVA: 0x00031802 File Offset: 0x0002FA02
		public EnhancedTimeSpan? TotalStalledDueToReadThrottle
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadThrottle).Duration);
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x00031820 File Offset: 0x0002FA20
		public EnhancedTimeSpan? TotalStalledDueToWriteThrottle
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteThrottle).Duration);
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x060016DD RID: 5853 RVA: 0x0003183E File Offset: 0x0002FA3E
		public EnhancedTimeSpan? TotalStalledDueToReadCpu
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadCpu).Duration);
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x060016DE RID: 5854 RVA: 0x0003185C File Offset: 0x0002FA5C
		public EnhancedTimeSpan? TotalStalledDueToWriteCpu
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteCpu).Duration);
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x060016DF RID: 5855 RVA: 0x0003187A File Offset: 0x0002FA7A
		public EnhancedTimeSpan? TotalStalledDueToReadUnknown
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadUnknown).Duration);
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x060016E0 RID: 5856 RVA: 0x00031898 File Offset: 0x0002FA98
		public EnhancedTimeSpan? TotalStalledDueToWriteUnknown
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteUnknown).Duration);
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x060016E1 RID: 5857 RVA: 0x000318B6 File Offset: 0x0002FAB6
		public EnhancedTimeSpan? TotalTransientFailureDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.TransientFailure).Duration);
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x060016E2 RID: 5858 RVA: 0x000318D4 File Offset: 0x0002FAD4
		public EnhancedTimeSpan? TotalIdleDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Idle).Duration);
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x060016E3 RID: 5859 RVA: 0x000318F2 File Offset: 0x0002FAF2
		// (set) Token: 0x060016E4 RID: 5860 RVA: 0x000318FA File Offset: 0x0002FAFA
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

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x060016E5 RID: 5861 RVA: 0x00031903 File Offset: 0x0002FB03
		// (set) Token: 0x060016E6 RID: 5862 RVA: 0x00031910 File Offset: 0x0002FB10
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

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x060016E7 RID: 5863 RVA: 0x0003191F File Offset: 0x0002FB1F
		// (set) Token: 0x060016E8 RID: 5864 RVA: 0x00031927 File Offset: 0x0002FB27
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

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x060016E9 RID: 5865 RVA: 0x00031930 File Offset: 0x0002FB30
		public override ByteQuantifiedSize? BytesTransferred
		{
			get
			{
				return base.BytesTransferred;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x00031938 File Offset: 0x0002FB38
		public override ByteQuantifiedSize? BytesTransferredPerMinute
		{
			get
			{
				return base.BytesTransferredPerMinute;
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x00031940 File Offset: 0x0002FB40
		public override ulong? ItemsTransferred
		{
			get
			{
				return base.ItemsTransferred;
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x060016EC RID: 5868 RVA: 0x00031948 File Offset: 0x0002FB48
		// (set) Token: 0x060016ED RID: 5869 RVA: 0x00031950 File Offset: 0x0002FB50
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

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x00031959 File Offset: 0x0002FB59
		// (set) Token: 0x060016EF RID: 5871 RVA: 0x00031961 File Offset: 0x0002FB61
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

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x060016F0 RID: 5872 RVA: 0x0003196A File Offset: 0x0002FB6A
		// (set) Token: 0x060016F1 RID: 5873 RVA: 0x00031972 File Offset: 0x0002FB72
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

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x060016F2 RID: 5874 RVA: 0x0003197B File Offset: 0x0002FB7B
		// (set) Token: 0x060016F3 RID: 5875 RVA: 0x00031983 File Offset: 0x0002FB83
		public RequestJobInternalFlags InternalFlags
		{
			get
			{
				return base.RequestJobInternalFlags;
			}
			internal set
			{
				base.RequestJobInternalFlags = value;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x060016F4 RID: 5876 RVA: 0x0003198C File Offset: 0x0002FB8C
		// (set) Token: 0x060016F5 RID: 5877 RVA: 0x00031994 File Offset: 0x0002FB94
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

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x060016F6 RID: 5878 RVA: 0x0003199D File Offset: 0x0002FB9D
		// (set) Token: 0x060016F7 RID: 5879 RVA: 0x000319A5 File Offset: 0x0002FBA5
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

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x060016F8 RID: 5880 RVA: 0x000319AE File Offset: 0x0002FBAE
		// (set) Token: 0x060016F9 RID: 5881 RVA: 0x000319B6 File Offset: 0x0002FBB6
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

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x060016FA RID: 5882 RVA: 0x000319BF File Offset: 0x0002FBBF
		// (set) Token: 0x060016FB RID: 5883 RVA: 0x000319C7 File Offset: 0x0002FBC7
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

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x060016FC RID: 5884 RVA: 0x000319D0 File Offset: 0x0002FBD0
		public DateTime? FailureTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Failure);
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x060016FD RID: 5885 RVA: 0x000319DF File Offset: 0x0002FBDF
		public override bool IsValid
		{
			get
			{
				return base.IsValid;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x060016FE RID: 5886 RVA: 0x000319E7 File Offset: 0x0002FBE7
		// (set) Token: 0x060016FF RID: 5887 RVA: 0x000319EF File Offset: 0x0002FBEF
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

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06001700 RID: 5888 RVA: 0x000319F8 File Offset: 0x0002FBF8
		// (set) Token: 0x06001701 RID: 5889 RVA: 0x00031A00 File Offset: 0x0002FC00
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

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06001702 RID: 5890 RVA: 0x00031A09 File Offset: 0x0002FC09
		// (set) Token: 0x06001703 RID: 5891 RVA: 0x00031A11 File Offset: 0x0002FC11
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

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06001704 RID: 5892 RVA: 0x00031A1A File Offset: 0x0002FC1A
		// (set) Token: 0x06001705 RID: 5893 RVA: 0x00031A22 File Offset: 0x0002FC22
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

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06001706 RID: 5894 RVA: 0x00031A2B File Offset: 0x0002FC2B
		// (set) Token: 0x06001707 RID: 5895 RVA: 0x00031A33 File Offset: 0x0002FC33
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

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06001708 RID: 5896 RVA: 0x00031A41 File Offset: 0x0002FC41
		public new string DiagnosticInfo
		{
			get
			{
				return base.DiagnosticInfo;
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06001709 RID: 5897 RVA: 0x00031A49 File Offset: 0x0002FC49
		// (set) Token: 0x0600170A RID: 5898 RVA: 0x00031A51 File Offset: 0x0002FC51
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

		// Token: 0x0600170B RID: 5899 RVA: 0x00031A5A File Offset: 0x0002FC5A
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.Name) && !string.IsNullOrEmpty(this.TargetAlias))
			{
				return string.Format("{0}\\{1}", this.TargetAlias, this.Name);
			}
			return base.ToString();
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x00031A94 File Offset: 0x0002FC94
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
				MailboxImportRequestStatistics.LoadAdditionalPropertiesFromUser(requestJob);
				requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.Valid);
				requestJob.ValidationMessage = LocalizedString.Empty;
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
			MailboxImportRequestStatistics.LoadAdditionalPropertiesFromUser(requestJob);
			if (!requestJob.ValidateRequestIndexEntries())
			{
				return;
			}
			requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.Valid);
			requestJob.ValidationMessage = LocalizedString.Empty;
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x00031BC0 File Offset: 0x0002FDC0
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
