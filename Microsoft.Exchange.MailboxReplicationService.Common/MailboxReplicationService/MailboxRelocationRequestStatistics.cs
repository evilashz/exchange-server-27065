using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001FA RID: 506
	public sealed class MailboxRelocationRequestStatistics : RequestStatisticsBase
	{
		// Token: 0x0600170E RID: 5902 RVA: 0x00031C5C File Offset: 0x0002FE5C
		public MailboxRelocationRequestStatistics()
		{
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x00031C64 File Offset: 0x0002FE64
		internal MailboxRelocationRequestStatistics(SimpleProviderPropertyBag propertyBag) : base(propertyBag)
		{
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x00031C6D File Offset: 0x0002FE6D
		internal MailboxRelocationRequestStatistics(TransactionalRequestJob requestJob) : this((SimpleProviderPropertyBag)requestJob.propertyBag)
		{
			base.CopyNonSchematizedPropertiesFrom(requestJob);
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x00031C87 File Offset: 0x0002FE87
		internal MailboxRelocationRequestStatistics(RequestJobXML requestJob) : this((SimpleProviderPropertyBag)requestJob.propertyBag)
		{
			base.CopyNonSchematizedPropertiesFrom(requestJob);
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06001712 RID: 5906 RVA: 0x00031CA1 File Offset: 0x0002FEA1
		// (set) Token: 0x06001713 RID: 5907 RVA: 0x00031CA9 File Offset: 0x0002FEA9
		public ADObjectId MailboxIdentity
		{
			get
			{
				return base.UserId;
			}
			internal set
			{
				base.UserId = value;
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06001714 RID: 5908 RVA: 0x00031CB2 File Offset: 0x0002FEB2
		// (set) Token: 0x06001715 RID: 5909 RVA: 0x00031CBA File Offset: 0x0002FEBA
		public new string DistinguishedName
		{
			get
			{
				return base.DistinguishedName;
			}
			internal set
			{
				base.DistinguishedName = value;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06001716 RID: 5910 RVA: 0x00031CC3 File Offset: 0x0002FEC3
		// (set) Token: 0x06001717 RID: 5911 RVA: 0x00031CCB File Offset: 0x0002FECB
		public new string DisplayName
		{
			get
			{
				return base.DisplayName;
			}
			internal set
			{
				base.DisplayName = value;
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06001718 RID: 5912 RVA: 0x00031CD4 File Offset: 0x0002FED4
		// (set) Token: 0x06001719 RID: 5913 RVA: 0x00031CDC File Offset: 0x0002FEDC
		public new string Alias
		{
			get
			{
				return base.Alias;
			}
			internal set
			{
				base.Alias = value;
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x0600171A RID: 5914 RVA: 0x00031CE5 File Offset: 0x0002FEE5
		// (set) Token: 0x0600171B RID: 5915 RVA: 0x00031CED File Offset: 0x0002FEED
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

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x0600171C RID: 5916 RVA: 0x00031CF6 File Offset: 0x0002FEF6
		// (set) Token: 0x0600171D RID: 5917 RVA: 0x00031CFE File Offset: 0x0002FEFE
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

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x0600171E RID: 5918 RVA: 0x00031D07 File Offset: 0x0002FF07
		public new RequestState StatusDetail
		{
			get
			{
				return base.StatusDetail;
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x0600171F RID: 5919 RVA: 0x00031D0F File Offset: 0x0002FF0F
		// (set) Token: 0x06001720 RID: 5920 RVA: 0x00031D17 File Offset: 0x0002FF17
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

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x00031D20 File Offset: 0x0002FF20
		// (set) Token: 0x06001722 RID: 5922 RVA: 0x00031D28 File Offset: 0x0002FF28
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

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x00031D31 File Offset: 0x0002FF31
		// (set) Token: 0x06001724 RID: 5924 RVA: 0x00031D39 File Offset: 0x0002FF39
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

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x00031D42 File Offset: 0x0002FF42
		// (set) Token: 0x06001726 RID: 5926 RVA: 0x00031D4A File Offset: 0x0002FF4A
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

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x00031D53 File Offset: 0x0002FF53
		// (set) Token: 0x06001728 RID: 5928 RVA: 0x00031D5B File Offset: 0x0002FF5B
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

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06001729 RID: 5929 RVA: 0x00031D64 File Offset: 0x0002FF64
		// (set) Token: 0x0600172A RID: 5930 RVA: 0x00031D6C File Offset: 0x0002FF6C
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

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x0600172B RID: 5931 RVA: 0x00031D75 File Offset: 0x0002FF75
		// (set) Token: 0x0600172C RID: 5932 RVA: 0x00031D7D File Offset: 0x0002FF7D
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

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x0600172D RID: 5933 RVA: 0x00031D86 File Offset: 0x0002FF86
		// (set) Token: 0x0600172E RID: 5934 RVA: 0x00031D9D File Offset: 0x0002FF9D
		public new ServerVersion SourceVersion
		{
			get
			{
				if (base.SourceVersion == 0)
				{
					return null;
				}
				return new ServerVersion(base.SourceVersion);
			}
			internal set
			{
				base.SourceVersion = ((value != null) ? value.ToInt() : 0);
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x0600172F RID: 5935 RVA: 0x00031DB7 File Offset: 0x0002FFB7
		// (set) Token: 0x06001730 RID: 5936 RVA: 0x00031DBF File Offset: 0x0002FFBF
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

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06001731 RID: 5937 RVA: 0x00031DC8 File Offset: 0x0002FFC8
		// (set) Token: 0x06001732 RID: 5938 RVA: 0x00031DD0 File Offset: 0x0002FFD0
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

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06001733 RID: 5939 RVA: 0x00031DD9 File Offset: 0x0002FFD9
		// (set) Token: 0x06001734 RID: 5940 RVA: 0x00031DF0 File Offset: 0x0002FFF0
		public new ServerVersion TargetVersion
		{
			get
			{
				if (base.TargetVersion == 0)
				{
					return null;
				}
				return new ServerVersion(base.TargetVersion);
			}
			set
			{
				base.TargetVersion = ((value != null) ? value.ToInt() : 0);
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06001735 RID: 5941 RVA: 0x00031E0A File Offset: 0x0003000A
		// (set) Token: 0x06001736 RID: 5942 RVA: 0x00031E12 File Offset: 0x00030012
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

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06001737 RID: 5943 RVA: 0x00031E1B File Offset: 0x0003001B
		// (set) Token: 0x06001738 RID: 5944 RVA: 0x00031E23 File Offset: 0x00030023
		public new Guid? TargetContainerGuid
		{
			get
			{
				return base.TargetContainerGuid;
			}
			internal set
			{
				base.TargetContainerGuid = value;
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06001739 RID: 5945 RVA: 0x00031E2C File Offset: 0x0003002C
		// (set) Token: 0x0600173A RID: 5946 RVA: 0x00031E34 File Offset: 0x00030034
		public new CrossTenantObjectId TargetUnifiedMailboxId
		{
			get
			{
				return base.TargetUnifiedMailboxId;
			}
			internal set
			{
				base.TargetUnifiedMailboxId = value;
			}
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x0600173B RID: 5947 RVA: 0x00031E3D File Offset: 0x0003003D
		// (set) Token: 0x0600173C RID: 5948 RVA: 0x00031E45 File Offset: 0x00030045
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

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x0600173D RID: 5949 RVA: 0x00031E4E File Offset: 0x0003004E
		// (set) Token: 0x0600173E RID: 5950 RVA: 0x00031E56 File Offset: 0x00030056
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

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x0600173F RID: 5951 RVA: 0x00031E5F File Offset: 0x0003005F
		// (set) Token: 0x06001740 RID: 5952 RVA: 0x00031E67 File Offset: 0x00030067
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

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06001741 RID: 5953 RVA: 0x00031E70 File Offset: 0x00030070
		// (set) Token: 0x06001742 RID: 5954 RVA: 0x00031E78 File Offset: 0x00030078
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

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06001743 RID: 5955 RVA: 0x00031E81 File Offset: 0x00030081
		public DateTime? QueuedTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Creation);
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06001744 RID: 5956 RVA: 0x00031E8F File Offset: 0x0003008F
		public DateTime? StartTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Start);
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06001745 RID: 5957 RVA: 0x00031E9D File Offset: 0x0003009D
		public DateTime? LastUpdateTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.LastUpdate);
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06001746 RID: 5958 RVA: 0x00031EAB File Offset: 0x000300AB
		public DateTime? InitialSeedingCompletedTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.InitialSeedingCompleted);
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06001747 RID: 5959 RVA: 0x00031EB9 File Offset: 0x000300B9
		public DateTime? FinalSyncTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.FinalSync);
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06001748 RID: 5960 RVA: 0x00031EC7 File Offset: 0x000300C7
		public DateTime? CompletionTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Completion);
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06001749 RID: 5961 RVA: 0x00031ED5 File Offset: 0x000300D5
		public DateTime? SuspendedTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Suspended);
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x0600174A RID: 5962 RVA: 0x00031EE3 File Offset: 0x000300E3
		public EnhancedTimeSpan? OverallDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.OverallMove).Duration);
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x0600174B RID: 5963 RVA: 0x00031F00 File Offset: 0x00030100
		public EnhancedTimeSpan? TotalFinalizationDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Finalization).Duration);
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x0600174C RID: 5964 RVA: 0x00031F1E File Offset: 0x0003011E
		public EnhancedTimeSpan? TotalDataReplicationWaitDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.DataReplicationWait).Duration);
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x0600174D RID: 5965 RVA: 0x00031F3C File Offset: 0x0003013C
		public EnhancedTimeSpan? TotalSuspendedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Suspended).Duration);
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x0600174E RID: 5966 RVA: 0x00031F5A File Offset: 0x0003015A
		public EnhancedTimeSpan? TotalFailedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Failed).Duration);
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x0600174F RID: 5967 RVA: 0x00031F78 File Offset: 0x00030178
		public EnhancedTimeSpan? TotalQueuedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Queued).Duration);
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06001750 RID: 5968 RVA: 0x00031F95 File Offset: 0x00030195
		public EnhancedTimeSpan? TotalInProgressDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.InProgress).Duration);
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06001751 RID: 5969 RVA: 0x00031FB2 File Offset: 0x000301B2
		public EnhancedTimeSpan? TotalStalledDueToCIDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToCI).Duration);
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06001752 RID: 5970 RVA: 0x00031FD0 File Offset: 0x000301D0
		public EnhancedTimeSpan? TotalStalledDueToHADuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToHA).Duration);
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06001753 RID: 5971 RVA: 0x00031FEE File Offset: 0x000301EE
		public EnhancedTimeSpan? TotalStalledDueToMailboxLockedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToMailboxLock).Duration);
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06001754 RID: 5972 RVA: 0x0003200C File Offset: 0x0003020C
		public EnhancedTimeSpan? TotalStalledDueToReadThrottle
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadThrottle).Duration);
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x0003202A File Offset: 0x0003022A
		public EnhancedTimeSpan? TotalStalledDueToWriteThrottle
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteThrottle).Duration);
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06001756 RID: 5974 RVA: 0x00032048 File Offset: 0x00030248
		public EnhancedTimeSpan? TotalStalledDueToReadCpu
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadCpu).Duration);
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06001757 RID: 5975 RVA: 0x00032066 File Offset: 0x00030266
		public EnhancedTimeSpan? TotalStalledDueToWriteCpu
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteCpu).Duration);
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06001758 RID: 5976 RVA: 0x00032084 File Offset: 0x00030284
		public EnhancedTimeSpan? TotalStalledDueToReadUnknown
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadUnknown).Duration);
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06001759 RID: 5977 RVA: 0x000320A2 File Offset: 0x000302A2
		public EnhancedTimeSpan? TotalStalledDueToWriteUnknown
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteUnknown).Duration);
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x000320C0 File Offset: 0x000302C0
		public EnhancedTimeSpan? TotalTransientFailureDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.TransientFailure).Duration);
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x0600175B RID: 5979 RVA: 0x000320DE File Offset: 0x000302DE
		public EnhancedTimeSpan? TotalProxyBackoffDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.ProxyBackoff).Duration);
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x0600175C RID: 5980 RVA: 0x000320FC File Offset: 0x000302FC
		public EnhancedTimeSpan? TotalIdleDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Idle).Duration);
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x0600175D RID: 5981 RVA: 0x0003211A File Offset: 0x0003031A
		// (set) Token: 0x0600175E RID: 5982 RVA: 0x00032122 File Offset: 0x00030322
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

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x0600175F RID: 5983 RVA: 0x0003212B File Offset: 0x0003032B
		// (set) Token: 0x06001760 RID: 5984 RVA: 0x00032138 File Offset: 0x00030338
		public new ByteQuantifiedSize TotalMailboxSize
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

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06001761 RID: 5985 RVA: 0x00032147 File Offset: 0x00030347
		// (set) Token: 0x06001762 RID: 5986 RVA: 0x0003214F File Offset: 0x0003034F
		public new ulong TotalMailboxItemCount
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

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06001763 RID: 5987 RVA: 0x00032158 File Offset: 0x00030358
		public override ByteQuantifiedSize? BytesTransferred
		{
			get
			{
				return base.BytesTransferred;
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06001764 RID: 5988 RVA: 0x00032160 File Offset: 0x00030360
		public override ByteQuantifiedSize? BytesTransferredPerMinute
		{
			get
			{
				return base.BytesTransferredPerMinute;
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06001765 RID: 5989 RVA: 0x00032168 File Offset: 0x00030368
		public override ulong? ItemsTransferred
		{
			get
			{
				return base.ItemsTransferred;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06001766 RID: 5990 RVA: 0x00032170 File Offset: 0x00030370
		// (set) Token: 0x06001767 RID: 5991 RVA: 0x00032178 File Offset: 0x00030378
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

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06001768 RID: 5992 RVA: 0x00032181 File Offset: 0x00030381
		// (set) Token: 0x06001769 RID: 5993 RVA: 0x00032189 File Offset: 0x00030389
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

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x0600176A RID: 5994 RVA: 0x00032192 File Offset: 0x00030392
		// (set) Token: 0x0600176B RID: 5995 RVA: 0x0003219A File Offset: 0x0003039A
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

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x0600176C RID: 5996 RVA: 0x000321A3 File Offset: 0x000303A3
		// (set) Token: 0x0600176D RID: 5997 RVA: 0x000321AB File Offset: 0x000303AB
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

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x0600176E RID: 5998 RVA: 0x000321B4 File Offset: 0x000303B4
		// (set) Token: 0x0600176F RID: 5999 RVA: 0x000321BC File Offset: 0x000303BC
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

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06001770 RID: 6000 RVA: 0x000321C5 File Offset: 0x000303C5
		// (set) Token: 0x06001771 RID: 6001 RVA: 0x000321CD File Offset: 0x000303CD
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

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06001772 RID: 6002 RVA: 0x000321D6 File Offset: 0x000303D6
		// (set) Token: 0x06001773 RID: 6003 RVA: 0x000321DE File Offset: 0x000303DE
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

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06001774 RID: 6004 RVA: 0x000321E7 File Offset: 0x000303E7
		// (set) Token: 0x06001775 RID: 6005 RVA: 0x000321EF File Offset: 0x000303EF
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

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06001776 RID: 6006 RVA: 0x000321F8 File Offset: 0x000303F8
		public DateTime? FailureTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Failure);
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06001777 RID: 6007 RVA: 0x00032207 File Offset: 0x00030407
		public override bool IsValid
		{
			get
			{
				return base.IsValid;
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x0003220F File Offset: 0x0003040F
		// (set) Token: 0x06001779 RID: 6009 RVA: 0x00032217 File Offset: 0x00030417
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

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x0600177A RID: 6010 RVA: 0x00032220 File Offset: 0x00030420
		// (set) Token: 0x0600177B RID: 6011 RVA: 0x00032228 File Offset: 0x00030428
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

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x0600177C RID: 6012 RVA: 0x00032231 File Offset: 0x00030431
		// (set) Token: 0x0600177D RID: 6013 RVA: 0x00032239 File Offset: 0x00030439
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

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x0600177E RID: 6014 RVA: 0x00032242 File Offset: 0x00030442
		// (set) Token: 0x0600177F RID: 6015 RVA: 0x0003224A File Offset: 0x0003044A
		public new ObjectId Identity
		{
			get
			{
				return base.Identity;
			}
			internal set
			{
				base.Identity = (RequestJobObjectId)value;
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06001780 RID: 6016 RVA: 0x00032258 File Offset: 0x00030458
		public new string DiagnosticInfo
		{
			get
			{
				return base.DiagnosticInfo;
			}
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06001781 RID: 6017 RVA: 0x00032260 File Offset: 0x00030460
		// (set) Token: 0x06001782 RID: 6018 RVA: 0x00032268 File Offset: 0x00030468
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

		// Token: 0x06001783 RID: 6019 RVA: 0x00032271 File Offset: 0x00030471
		public static bool IsTerminalState(RequestJobBase requestJobBase)
		{
			return requestJobBase.CancelRequest || MailboxRelocationRequestStatistics.IsTerminalState(requestJobBase.Status) || requestJobBase.SyncStage >= SyncStage.Cleanup;
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x00032297 File Offset: 0x00030497
		public static bool IsTerminalState(RequestStatus requestStatus)
		{
			return requestStatus == RequestStatus.Completed || requestStatus == RequestStatus.CompletedWithWarning;
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x000322A5 File Offset: 0x000304A5
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(base.Name) && !string.IsNullOrEmpty(this.Alias))
			{
				return string.Format("{0}\\{1}", this.Alias, base.Name);
			}
			return base.ToString();
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x000322E0 File Offset: 0x000304E0
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
			if (requestJob.OriginatingMDBGuid == Guid.Empty)
			{
				requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.Valid);
				requestJob.ValidationMessage = LocalizedString.Empty;
				return;
			}
			MailboxRelocationRequestStatistics.LoadAdditionalPropertiesFromUser(requestJob);
			if (MailboxRelocationRequestStatistics.IsTerminalState(requestJob))
			{
				requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.Valid);
				requestJob.ValidationMessage = LocalizedString.Empty;
				return;
			}
			if (!requestJob.ValidateUser(requestJob.User, requestJob.UserId))
			{
				return;
			}
			Guid guid;
			Guid guid2;
			RequestIndexEntryProvider.GetMoveGuids(requestJob.User, out guid, out guid2);
			if (guid != requestJob.ExchangeGuid)
			{
				MrsTracer.Common.Error("Orphaned RequestJob: mailbox guid does not match between AD {0} and workitem queue {1}.", new object[]
				{
					requestJob.User.ExchangeGuid,
					requestJob.ExchangeGuid
				});
				requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.DataMismatch);
				requestJob.ValidationMessage = MrsStrings.ValidationMailboxGuidsDontMatch(guid, requestJob.ExchangeGuid);
				return;
			}
			if (!MailboxRelocationRequestStatistics.ValidateNoOtherRequests(requestJob))
			{
				return;
			}
			if (CommonUtils.IsImplicitSplit(requestJob.Flags, requestJob.User))
			{
				requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.DataMismatch);
				requestJob.ValidationMessage = MrsStrings.ErrorImplicitSplit;
				return;
			}
			requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.Valid);
			requestJob.ValidationMessage = LocalizedString.Empty;
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x0003248C File Offset: 0x0003068C
		private static void LoadAdditionalPropertiesFromUser(RequestJobBase requestJob)
		{
			if (requestJob.User != null)
			{
				requestJob.Alias = requestJob.User.Alias;
				requestJob.DisplayName = requestJob.User.DisplayName;
				requestJob.RecipientTypeDetails = requestJob.User.RecipientTypeDetails;
				requestJob.UserId = requestJob.User.Id;
			}
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x00032558 File Offset: 0x00030758
		private static bool ValidateNoOtherRequests(RequestJobBase requestJobBase)
		{
			IEnumerable<RequestIndexId> source = from i in requestJobBase.IndexEntries
			select i.RequestIndexId into i
			where i.Location == RequestIndexLocation.Mailbox
			select i;
			if (source.Any((RequestIndexId i) => i.Mailbox.Equals(requestJobBase.UserId)))
			{
				string otherRequests = MailboxRequestIndexEntryHandler.GetOtherRequests(requestJobBase.User, new Guid?(requestJobBase.RequestGuid));
				if (!string.IsNullOrEmpty(otherRequests))
				{
					requestJobBase.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.DataMismatch);
					requestJobBase.ValidationMessage = MrsStrings.ValidationObjectInvolvedInMultipleRelocations(MrsStrings.Mailbox, otherRequests);
					return false;
				}
			}
			if (requestJobBase.User.UnifiedMailbox != null)
			{
				ADRecipient tempRecipient;
				if (ADRecipient.TryGetFromCrossTenantObjectId(requestJobBase.User.UnifiedMailbox, out tempRecipient).Succeeded && source.Any((RequestIndexId i) => i.Mailbox.Equals(tempRecipient.Id)))
				{
					string otherRequests = MailboxRequestIndexEntryHandler.GetOtherRequests((ADUser)tempRecipient, new Guid?(requestJobBase.RequestGuid));
					if (!string.IsNullOrEmpty(otherRequests))
					{
						requestJobBase.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.DataMismatch);
						requestJobBase.ValidationMessage = MrsStrings.ValidationObjectInvolvedInMultipleRelocations(MrsStrings.SourceContainer, otherRequests);
						return false;
					}
				}
			}
			if (requestJobBase.TargetUnifiedMailboxId != null)
			{
				ADRecipient tempRecipient;
				if (ADRecipient.TryGetFromCrossTenantObjectId(requestJobBase.TargetUnifiedMailboxId, out tempRecipient).Succeeded && source.Any((RequestIndexId i) => i.Mailbox.Equals(tempRecipient.Id)))
				{
					string otherRequests = MailboxRequestIndexEntryHandler.GetOtherRequests((ADUser)tempRecipient, new Guid?(requestJobBase.RequestGuid));
					if (!string.IsNullOrEmpty(otherRequests))
					{
						requestJobBase.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.DataMismatch);
						requestJobBase.ValidationMessage = MrsStrings.ValidationObjectInvolvedInMultipleRelocations(MrsStrings.TargetContainer, otherRequests);
						return false;
					}
				}
			}
			return true;
		}
	}
}
