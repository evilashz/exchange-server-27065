using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001F8 RID: 504
	[Serializable]
	public sealed class MailboxExportRequestStatistics : RequestStatisticsBase
	{
		// Token: 0x06001602 RID: 5634 RVA: 0x00030C38 File Offset: 0x0002EE38
		public MailboxExportRequestStatistics()
		{
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x00030C40 File Offset: 0x0002EE40
		internal MailboxExportRequestStatistics(SimpleProviderPropertyBag propertyBag) : base(propertyBag)
		{
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x00030C49 File Offset: 0x0002EE49
		internal MailboxExportRequestStatistics(TransactionalRequestJob requestJob) : this((SimpleProviderPropertyBag)requestJob.propertyBag)
		{
			base.CopyNonSchematizedPropertiesFrom(requestJob);
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x00030C63 File Offset: 0x0002EE63
		internal MailboxExportRequestStatistics(RequestJobXML requestJob) : this((SimpleProviderPropertyBag)requestJob.propertyBag)
		{
			base.CopyNonSchematizedPropertiesFrom(requestJob);
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001606 RID: 5638 RVA: 0x00030C7D File Offset: 0x0002EE7D
		// (set) Token: 0x06001607 RID: 5639 RVA: 0x00030C85 File Offset: 0x0002EE85
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

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x00030C8E File Offset: 0x0002EE8E
		// (set) Token: 0x06001609 RID: 5641 RVA: 0x00030C96 File Offset: 0x0002EE96
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

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x00030C9F File Offset: 0x0002EE9F
		public new RequestState StatusDetail
		{
			get
			{
				return base.StatusDetail;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x00030CA7 File Offset: 0x0002EEA7
		// (set) Token: 0x0600160C RID: 5644 RVA: 0x00030CAF File Offset: 0x0002EEAF
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

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x00030CB8 File Offset: 0x0002EEB8
		// (set) Token: 0x0600160E RID: 5646 RVA: 0x00030CC0 File Offset: 0x0002EEC0
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

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x00030CC9 File Offset: 0x0002EEC9
		// (set) Token: 0x06001610 RID: 5648 RVA: 0x00030CD1 File Offset: 0x0002EED1
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

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001611 RID: 5649 RVA: 0x00030CDA File Offset: 0x0002EEDA
		// (set) Token: 0x06001612 RID: 5650 RVA: 0x00030CE2 File Offset: 0x0002EEE2
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

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001613 RID: 5651 RVA: 0x00030CEB File Offset: 0x0002EEEB
		// (set) Token: 0x06001614 RID: 5652 RVA: 0x00030CF3 File Offset: 0x0002EEF3
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

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001615 RID: 5653 RVA: 0x00030CFC File Offset: 0x0002EEFC
		// (set) Token: 0x06001616 RID: 5654 RVA: 0x00030D04 File Offset: 0x0002EF04
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

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001617 RID: 5655 RVA: 0x00030D0D File Offset: 0x0002EF0D
		// (set) Token: 0x06001618 RID: 5656 RVA: 0x00030D15 File Offset: 0x0002EF15
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

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06001619 RID: 5657 RVA: 0x00030D1E File Offset: 0x0002EF1E
		// (set) Token: 0x0600161A RID: 5658 RVA: 0x00030D26 File Offset: 0x0002EF26
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

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x0600161B RID: 5659 RVA: 0x00030D2F File Offset: 0x0002EF2F
		// (set) Token: 0x0600161C RID: 5660 RVA: 0x00030D37 File Offset: 0x0002EF37
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

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x0600161D RID: 5661 RVA: 0x00030D40 File Offset: 0x0002EF40
		// (set) Token: 0x0600161E RID: 5662 RVA: 0x00030D48 File Offset: 0x0002EF48
		public new string SourceAlias
		{
			get
			{
				return base.SourceAlias;
			}
			internal set
			{
				base.SourceAlias = value;
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x0600161F RID: 5663 RVA: 0x00030D51 File Offset: 0x0002EF51
		// (set) Token: 0x06001620 RID: 5664 RVA: 0x00030D59 File Offset: 0x0002EF59
		public new bool SourceIsArchive
		{
			get
			{
				return base.SourceIsArchive;
			}
			internal set
			{
				base.SourceIsArchive = value;
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06001621 RID: 5665 RVA: 0x00030D62 File Offset: 0x0002EF62
		// (set) Token: 0x06001622 RID: 5666 RVA: 0x00030D6A File Offset: 0x0002EF6A
		public new Guid SourceExchangeGuid
		{
			get
			{
				return base.SourceExchangeGuid;
			}
			internal set
			{
				base.SourceExchangeGuid = value;
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001623 RID: 5667 RVA: 0x00030D73 File Offset: 0x0002EF73
		// (set) Token: 0x06001624 RID: 5668 RVA: 0x00030D7B File Offset: 0x0002EF7B
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

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06001625 RID: 5669 RVA: 0x00030D84 File Offset: 0x0002EF84
		// (set) Token: 0x06001626 RID: 5670 RVA: 0x00030D8C File Offset: 0x0002EF8C
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

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06001627 RID: 5671 RVA: 0x00030D95 File Offset: 0x0002EF95
		// (set) Token: 0x06001628 RID: 5672 RVA: 0x00030DA2 File Offset: 0x0002EFA2
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

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06001629 RID: 5673 RVA: 0x00030DB0 File Offset: 0x0002EFB0
		// (set) Token: 0x0600162A RID: 5674 RVA: 0x00030DB8 File Offset: 0x0002EFB8
		public ADObjectId SourceMailboxIdentity
		{
			get
			{
				return base.SourceUserId;
			}
			internal set
			{
				base.SourceUserId = value;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x0600162B RID: 5675 RVA: 0x00030DC1 File Offset: 0x0002EFC1
		// (set) Token: 0x0600162C RID: 5676 RVA: 0x00030DC9 File Offset: 0x0002EFC9
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

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x0600162D RID: 5677 RVA: 0x00030DD2 File Offset: 0x0002EFD2
		// (set) Token: 0x0600162E RID: 5678 RVA: 0x00030DDA File Offset: 0x0002EFDA
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

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x0600162F RID: 5679 RVA: 0x00030DE3 File Offset: 0x0002EFE3
		// (set) Token: 0x06001630 RID: 5680 RVA: 0x00030DEB File Offset: 0x0002EFEB
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

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001631 RID: 5681 RVA: 0x00030DF4 File Offset: 0x0002EFF4
		// (set) Token: 0x06001632 RID: 5682 RVA: 0x00030DFC File Offset: 0x0002EFFC
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

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06001633 RID: 5683 RVA: 0x00030E05 File Offset: 0x0002F005
		// (set) Token: 0x06001634 RID: 5684 RVA: 0x00030E0D File Offset: 0x0002F00D
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

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06001635 RID: 5685 RVA: 0x00030E16 File Offset: 0x0002F016
		// (set) Token: 0x06001636 RID: 5686 RVA: 0x00030E1E File Offset: 0x0002F01E
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

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06001637 RID: 5687 RVA: 0x00030E27 File Offset: 0x0002F027
		// (set) Token: 0x06001638 RID: 5688 RVA: 0x00030E2F File Offset: 0x0002F02F
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

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x00030E38 File Offset: 0x0002F038
		// (set) Token: 0x0600163A RID: 5690 RVA: 0x00030E40 File Offset: 0x0002F040
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

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x0600163B RID: 5691 RVA: 0x00030E49 File Offset: 0x0002F049
		// (set) Token: 0x0600163C RID: 5692 RVA: 0x00030E51 File Offset: 0x0002F051
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

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x0600163D RID: 5693 RVA: 0x00030E5A File Offset: 0x0002F05A
		// (set) Token: 0x0600163E RID: 5694 RVA: 0x00030E62 File Offset: 0x0002F062
		public new string ContentFilter
		{
			get
			{
				return base.ContentFilter;
			}
			internal set
			{
				base.ContentFilter = value;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x00030E6B File Offset: 0x0002F06B
		// (set) Token: 0x06001640 RID: 5696 RVA: 0x00030E78 File Offset: 0x0002F078
		public CultureInfo ContentFilterLanguage
		{
			get
			{
				return new CultureInfo(base.ContentFilterLCID);
			}
			internal set
			{
				base.ContentFilterLCID = value.LCID;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06001641 RID: 5697 RVA: 0x00030E86 File Offset: 0x0002F086
		// (set) Token: 0x06001642 RID: 5698 RVA: 0x00030E8E File Offset: 0x0002F08E
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

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06001643 RID: 5699 RVA: 0x00030E97 File Offset: 0x0002F097
		// (set) Token: 0x06001644 RID: 5700 RVA: 0x00030E9F File Offset: 0x0002F09F
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

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x00030EA8 File Offset: 0x0002F0A8
		// (set) Token: 0x06001646 RID: 5702 RVA: 0x00030EB0 File Offset: 0x0002F0B0
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

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06001647 RID: 5703 RVA: 0x00030EB9 File Offset: 0x0002F0B9
		// (set) Token: 0x06001648 RID: 5704 RVA: 0x00030EC1 File Offset: 0x0002F0C1
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

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x00030ECA File Offset: 0x0002F0CA
		public DateTime? QueuedTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Creation);
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x00030ED8 File Offset: 0x0002F0D8
		public DateTime? StartTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Start);
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x00030EE6 File Offset: 0x0002F0E6
		public DateTime? LastUpdateTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.LastUpdate);
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x00030EF4 File Offset: 0x0002F0F4
		public DateTime? CompletionTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Completion);
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x00030F02 File Offset: 0x0002F102
		public DateTime? SuspendedTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Suspended);
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x00030F10 File Offset: 0x0002F110
		public EnhancedTimeSpan? OverallDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.OverallMove).Duration);
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x00030F2D File Offset: 0x0002F12D
		public EnhancedTimeSpan? TotalSuspendedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Suspended).Duration);
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x00030F4B File Offset: 0x0002F14B
		public EnhancedTimeSpan? TotalFailedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Failed).Duration);
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x00030F69 File Offset: 0x0002F169
		public EnhancedTimeSpan? TotalQueuedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Queued).Duration);
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x00030F86 File Offset: 0x0002F186
		public EnhancedTimeSpan? TotalInProgressDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.InProgress).Duration);
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06001653 RID: 5715 RVA: 0x00030FA3 File Offset: 0x0002F1A3
		public EnhancedTimeSpan? TotalStalledDueToCIDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToCI).Duration);
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06001654 RID: 5716 RVA: 0x00030FC1 File Offset: 0x0002F1C1
		public EnhancedTimeSpan? TotalStalledDueToHADuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToHA).Duration);
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06001655 RID: 5717 RVA: 0x00030FDF File Offset: 0x0002F1DF
		public EnhancedTimeSpan? TotalStalledDueToMailboxLockedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToMailboxLock).Duration);
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06001656 RID: 5718 RVA: 0x00030FFD File Offset: 0x0002F1FD
		public EnhancedTimeSpan? TotalStalledDueToReadThrottle
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadThrottle).Duration);
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06001657 RID: 5719 RVA: 0x0003101B File Offset: 0x0002F21B
		public EnhancedTimeSpan? TotalStalledDueToWriteThrottle
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteThrottle).Duration);
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06001658 RID: 5720 RVA: 0x00031039 File Offset: 0x0002F239
		public EnhancedTimeSpan? TotalStalledDueToReadCpu
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadCpu).Duration);
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x00031057 File Offset: 0x0002F257
		public EnhancedTimeSpan? TotalStalledDueToWriteCpu
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteCpu).Duration);
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x00031075 File Offset: 0x0002F275
		public EnhancedTimeSpan? TotalStalledDueToReadUnknown
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadUnknown).Duration);
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x0600165B RID: 5723 RVA: 0x00031093 File Offset: 0x0002F293
		public EnhancedTimeSpan? TotalStalledDueToWriteUnknown
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteUnknown).Duration);
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x0600165C RID: 5724 RVA: 0x000310B1 File Offset: 0x0002F2B1
		public EnhancedTimeSpan? TotalTransientFailureDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.TransientFailure).Duration);
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x0600165D RID: 5725 RVA: 0x000310CF File Offset: 0x0002F2CF
		public EnhancedTimeSpan? TotalIdleDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Idle).Duration);
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x0600165E RID: 5726 RVA: 0x000310ED File Offset: 0x0002F2ED
		// (set) Token: 0x0600165F RID: 5727 RVA: 0x000310F5 File Offset: 0x0002F2F5
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

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06001660 RID: 5728 RVA: 0x000310FE File Offset: 0x0002F2FE
		// (set) Token: 0x06001661 RID: 5729 RVA: 0x0003110B File Offset: 0x0002F30B
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

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06001662 RID: 5730 RVA: 0x0003111A File Offset: 0x0002F31A
		// (set) Token: 0x06001663 RID: 5731 RVA: 0x00031122 File Offset: 0x0002F322
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

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06001664 RID: 5732 RVA: 0x0003112B File Offset: 0x0002F32B
		public override ByteQuantifiedSize? BytesTransferred
		{
			get
			{
				return base.BytesTransferred;
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06001665 RID: 5733 RVA: 0x00031133 File Offset: 0x0002F333
		public override ByteQuantifiedSize? BytesTransferredPerMinute
		{
			get
			{
				return base.BytesTransferredPerMinute;
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06001666 RID: 5734 RVA: 0x0003113B File Offset: 0x0002F33B
		public override ulong? ItemsTransferred
		{
			get
			{
				return base.ItemsTransferred;
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06001667 RID: 5735 RVA: 0x00031143 File Offset: 0x0002F343
		// (set) Token: 0x06001668 RID: 5736 RVA: 0x0003114B File Offset: 0x0002F34B
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

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x00031154 File Offset: 0x0002F354
		// (set) Token: 0x0600166A RID: 5738 RVA: 0x0003115C File Offset: 0x0002F35C
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

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x00031165 File Offset: 0x0002F365
		// (set) Token: 0x0600166C RID: 5740 RVA: 0x0003116D File Offset: 0x0002F36D
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

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x00031176 File Offset: 0x0002F376
		// (set) Token: 0x0600166E RID: 5742 RVA: 0x0003117E File Offset: 0x0002F37E
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

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x0600166F RID: 5743 RVA: 0x00031187 File Offset: 0x0002F387
		// (set) Token: 0x06001670 RID: 5744 RVA: 0x0003118F File Offset: 0x0002F38F
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

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06001671 RID: 5745 RVA: 0x00031198 File Offset: 0x0002F398
		// (set) Token: 0x06001672 RID: 5746 RVA: 0x000311A0 File Offset: 0x0002F3A0
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

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06001673 RID: 5747 RVA: 0x000311A9 File Offset: 0x0002F3A9
		// (set) Token: 0x06001674 RID: 5748 RVA: 0x000311B1 File Offset: 0x0002F3B1
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

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06001675 RID: 5749 RVA: 0x000311BA File Offset: 0x0002F3BA
		// (set) Token: 0x06001676 RID: 5750 RVA: 0x000311C2 File Offset: 0x0002F3C2
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

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x000311CB File Offset: 0x0002F3CB
		public DateTime? FailureTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Failure);
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06001678 RID: 5752 RVA: 0x000311DA File Offset: 0x0002F3DA
		public override bool IsValid
		{
			get
			{
				return base.IsValid;
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x000311E2 File Offset: 0x0002F3E2
		// (set) Token: 0x0600167A RID: 5754 RVA: 0x000311EA File Offset: 0x0002F3EA
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

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x000311F3 File Offset: 0x0002F3F3
		// (set) Token: 0x0600167C RID: 5756 RVA: 0x000311FB File Offset: 0x0002F3FB
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

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x0600167D RID: 5757 RVA: 0x00031204 File Offset: 0x0002F404
		// (set) Token: 0x0600167E RID: 5758 RVA: 0x0003120C File Offset: 0x0002F40C
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

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x0600167F RID: 5759 RVA: 0x00031215 File Offset: 0x0002F415
		// (set) Token: 0x06001680 RID: 5760 RVA: 0x0003121D File Offset: 0x0002F41D
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

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06001681 RID: 5761 RVA: 0x00031226 File Offset: 0x0002F426
		// (set) Token: 0x06001682 RID: 5762 RVA: 0x0003122E File Offset: 0x0002F42E
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

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x0003123C File Offset: 0x0002F43C
		public new string DiagnosticInfo
		{
			get
			{
				return base.DiagnosticInfo;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x00031244 File Offset: 0x0002F444
		// (set) Token: 0x06001685 RID: 5765 RVA: 0x0003124C File Offset: 0x0002F44C
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

		// Token: 0x06001686 RID: 5766 RVA: 0x00031255 File Offset: 0x0002F455
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.Name) && !string.IsNullOrEmpty(this.SourceAlias))
			{
				return string.Format("{0}\\{1}", this.SourceAlias, this.Name);
			}
			return base.ToString();
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x00031290 File Offset: 0x0002F490
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
				MailboxExportRequestStatistics.LoadAdditionalPropertiesFromUser(requestJob);
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
			MailboxExportRequestStatistics.LoadAdditionalPropertiesFromUser(requestJob);
			if (!requestJob.ValidateRequestIndexEntries())
			{
				return;
			}
			requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.Valid);
			requestJob.ValidationMessage = LocalizedString.Empty;
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x000313BC File Offset: 0x0002F5BC
		private static void LoadAdditionalPropertiesFromUser(RequestJobBase requestJob)
		{
			if (requestJob.SourceUser != null)
			{
				requestJob.SourceAlias = requestJob.SourceUser.Alias;
				requestJob.SourceExchangeGuid = (requestJob.SourceIsArchive ? requestJob.SourceUser.ArchiveGuid : requestJob.SourceUser.ExchangeGuid);
				requestJob.SourceDatabase = ADObjectIdResolutionHelper.ResolveDN(requestJob.SourceIsArchive ? requestJob.SourceUser.ArchiveDatabase : requestJob.SourceUser.Database);
				requestJob.RecipientTypeDetails = requestJob.SourceUser.RecipientTypeDetails;
				requestJob.SourceUserId = requestJob.SourceUser.Id;
			}
		}
	}
}
