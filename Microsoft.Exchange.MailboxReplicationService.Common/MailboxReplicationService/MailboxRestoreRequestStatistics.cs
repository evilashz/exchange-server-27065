using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001FC RID: 508
	[Serializable]
	public sealed class MailboxRestoreRequestStatistics : RequestStatisticsBase
	{
		// Token: 0x0600178B RID: 6027 RVA: 0x00032787 File Offset: 0x00030987
		public MailboxRestoreRequestStatistics()
		{
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x0003278F File Offset: 0x0003098F
		internal MailboxRestoreRequestStatistics(SimpleProviderPropertyBag propertyBag) : base(propertyBag)
		{
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x00032798 File Offset: 0x00030998
		internal MailboxRestoreRequestStatistics(TransactionalRequestJob requestJob) : this((SimpleProviderPropertyBag)requestJob.propertyBag)
		{
			base.CopyNonSchematizedPropertiesFrom(requestJob);
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x000327B2 File Offset: 0x000309B2
		internal MailboxRestoreRequestStatistics(RequestJobXML requestJob) : this((SimpleProviderPropertyBag)requestJob.propertyBag)
		{
			base.CopyNonSchematizedPropertiesFrom(requestJob);
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x0600178F RID: 6031 RVA: 0x000327CC File Offset: 0x000309CC
		// (set) Token: 0x06001790 RID: 6032 RVA: 0x000327D4 File Offset: 0x000309D4
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

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06001791 RID: 6033 RVA: 0x000327DD File Offset: 0x000309DD
		// (set) Token: 0x06001792 RID: 6034 RVA: 0x000327E5 File Offset: 0x000309E5
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

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06001793 RID: 6035 RVA: 0x000327EE File Offset: 0x000309EE
		public new RequestState StatusDetail
		{
			get
			{
				return base.StatusDetail;
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06001794 RID: 6036 RVA: 0x000327F6 File Offset: 0x000309F6
		// (set) Token: 0x06001795 RID: 6037 RVA: 0x000327FE File Offset: 0x000309FE
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

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06001796 RID: 6038 RVA: 0x00032807 File Offset: 0x00030A07
		// (set) Token: 0x06001797 RID: 6039 RVA: 0x0003280F File Offset: 0x00030A0F
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

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06001798 RID: 6040 RVA: 0x00032818 File Offset: 0x00030A18
		// (set) Token: 0x06001799 RID: 6041 RVA: 0x00032820 File Offset: 0x00030A20
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

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x00032829 File Offset: 0x00030A29
		// (set) Token: 0x0600179B RID: 6043 RVA: 0x00032831 File Offset: 0x00030A31
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

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x0600179C RID: 6044 RVA: 0x0003283A File Offset: 0x00030A3A
		// (set) Token: 0x0600179D RID: 6045 RVA: 0x00032842 File Offset: 0x00030A42
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

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x0600179E RID: 6046 RVA: 0x0003284B File Offset: 0x00030A4B
		// (set) Token: 0x0600179F RID: 6047 RVA: 0x00032853 File Offset: 0x00030A53
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

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x0003285C File Offset: 0x00030A5C
		// (set) Token: 0x060017A1 RID: 6049 RVA: 0x00032864 File Offset: 0x00030A64
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

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x060017A2 RID: 6050 RVA: 0x0003286D File Offset: 0x00030A6D
		// (set) Token: 0x060017A3 RID: 6051 RVA: 0x00032875 File Offset: 0x00030A75
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

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x060017A4 RID: 6052 RVA: 0x0003287E File Offset: 0x00030A7E
		// (set) Token: 0x060017A5 RID: 6053 RVA: 0x00032886 File Offset: 0x00030A86
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

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x060017A6 RID: 6054 RVA: 0x0003288F File Offset: 0x00030A8F
		// (set) Token: 0x060017A7 RID: 6055 RVA: 0x00032897 File Offset: 0x00030A97
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

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x060017A8 RID: 6056 RVA: 0x000328A0 File Offset: 0x00030AA0
		// (set) Token: 0x060017A9 RID: 6057 RVA: 0x000328AD File Offset: 0x00030AAD
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

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x060017AA RID: 6058 RVA: 0x000328BB File Offset: 0x00030ABB
		// (set) Token: 0x060017AB RID: 6059 RVA: 0x000328C3 File Offset: 0x00030AC3
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

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x000328CC File Offset: 0x00030ACC
		// (set) Token: 0x060017AD RID: 6061 RVA: 0x000328D4 File Offset: 0x00030AD4
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

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x000328DD File Offset: 0x00030ADD
		// (set) Token: 0x060017AF RID: 6063 RVA: 0x000328E5 File Offset: 0x00030AE5
		public new MailboxRestoreType? MailboxRestoreFlags
		{
			get
			{
				return base.MailboxRestoreFlags;
			}
			internal set
			{
				base.MailboxRestoreFlags = value;
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x060017B0 RID: 6064 RVA: 0x000328EE File Offset: 0x00030AEE
		// (set) Token: 0x060017B1 RID: 6065 RVA: 0x000328F6 File Offset: 0x00030AF6
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

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x060017B2 RID: 6066 RVA: 0x000328FF File Offset: 0x00030AFF
		// (set) Token: 0x060017B3 RID: 6067 RVA: 0x00032907 File Offset: 0x00030B07
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

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x060017B4 RID: 6068 RVA: 0x00032910 File Offset: 0x00030B10
		// (set) Token: 0x060017B5 RID: 6069 RVA: 0x00032918 File Offset: 0x00030B18
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

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x060017B6 RID: 6070 RVA: 0x00032921 File Offset: 0x00030B21
		// (set) Token: 0x060017B7 RID: 6071 RVA: 0x00032929 File Offset: 0x00030B29
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

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x060017B8 RID: 6072 RVA: 0x00032932 File Offset: 0x00030B32
		// (set) Token: 0x060017B9 RID: 6073 RVA: 0x0003293F File Offset: 0x00030B3F
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

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x060017BA RID: 6074 RVA: 0x0003294D File Offset: 0x00030B4D
		// (set) Token: 0x060017BB RID: 6075 RVA: 0x00032955 File Offset: 0x00030B55
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

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x0003295E File Offset: 0x00030B5E
		// (set) Token: 0x060017BD RID: 6077 RVA: 0x00032966 File Offset: 0x00030B66
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

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x0003296F File Offset: 0x00030B6F
		// (set) Token: 0x060017BF RID: 6079 RVA: 0x00032977 File Offset: 0x00030B77
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

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x060017C0 RID: 6080 RVA: 0x00032980 File Offset: 0x00030B80
		// (set) Token: 0x060017C1 RID: 6081 RVA: 0x00032988 File Offset: 0x00030B88
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

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x060017C2 RID: 6082 RVA: 0x00032991 File Offset: 0x00030B91
		// (set) Token: 0x060017C3 RID: 6083 RVA: 0x00032999 File Offset: 0x00030B99
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

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x060017C4 RID: 6084 RVA: 0x000329A2 File Offset: 0x00030BA2
		// (set) Token: 0x060017C5 RID: 6085 RVA: 0x000329AA File Offset: 0x00030BAA
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

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x060017C6 RID: 6086 RVA: 0x000329B3 File Offset: 0x00030BB3
		// (set) Token: 0x060017C7 RID: 6087 RVA: 0x000329BB File Offset: 0x00030BBB
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

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x060017C8 RID: 6088 RVA: 0x000329C4 File Offset: 0x00030BC4
		// (set) Token: 0x060017C9 RID: 6089 RVA: 0x000329CC File Offset: 0x00030BCC
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

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x000329D5 File Offset: 0x00030BD5
		// (set) Token: 0x060017CB RID: 6091 RVA: 0x000329DD File Offset: 0x00030BDD
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

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x060017CC RID: 6092 RVA: 0x000329E6 File Offset: 0x00030BE6
		// (set) Token: 0x060017CD RID: 6093 RVA: 0x000329EE File Offset: 0x00030BEE
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

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x000329F7 File Offset: 0x00030BF7
		// (set) Token: 0x060017CF RID: 6095 RVA: 0x000329FF File Offset: 0x00030BFF
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

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x00032A08 File Offset: 0x00030C08
		// (set) Token: 0x060017D1 RID: 6097 RVA: 0x00032A10 File Offset: 0x00030C10
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

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x060017D2 RID: 6098 RVA: 0x00032A19 File Offset: 0x00030C19
		// (set) Token: 0x060017D3 RID: 6099 RVA: 0x00032A21 File Offset: 0x00030C21
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

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x060017D4 RID: 6100 RVA: 0x00032A2A File Offset: 0x00030C2A
		public DateTime? QueuedTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Creation);
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x060017D5 RID: 6101 RVA: 0x00032A38 File Offset: 0x00030C38
		public DateTime? StartTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Start);
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x00032A46 File Offset: 0x00030C46
		public DateTime? LastUpdateTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.LastUpdate);
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x060017D7 RID: 6103 RVA: 0x00032A54 File Offset: 0x00030C54
		public DateTime? CompletionTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Completion);
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x00032A62 File Offset: 0x00030C62
		public DateTime? SuspendedTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Suspended);
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x060017D9 RID: 6105 RVA: 0x00032A70 File Offset: 0x00030C70
		public EnhancedTimeSpan? OverallDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.OverallMove).Duration);
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x00032A8D File Offset: 0x00030C8D
		public EnhancedTimeSpan? TotalSuspendedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Suspended).Duration);
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x060017DB RID: 6107 RVA: 0x00032AAB File Offset: 0x00030CAB
		public EnhancedTimeSpan? TotalFailedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Failed).Duration);
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x060017DC RID: 6108 RVA: 0x00032AC9 File Offset: 0x00030CC9
		public EnhancedTimeSpan? TotalQueuedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Queued).Duration);
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x060017DD RID: 6109 RVA: 0x00032AE6 File Offset: 0x00030CE6
		public EnhancedTimeSpan? TotalInProgressDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.InProgress).Duration);
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x060017DE RID: 6110 RVA: 0x00032B03 File Offset: 0x00030D03
		public EnhancedTimeSpan? TotalStalledDueToCIDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToCI).Duration);
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x060017DF RID: 6111 RVA: 0x00032B21 File Offset: 0x00030D21
		public EnhancedTimeSpan? TotalStalledDueToHADuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToHA).Duration);
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x060017E0 RID: 6112 RVA: 0x00032B3F File Offset: 0x00030D3F
		public EnhancedTimeSpan? TotalStalledDueToMailboxLockedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToMailboxLock).Duration);
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x060017E1 RID: 6113 RVA: 0x00032B5D File Offset: 0x00030D5D
		public EnhancedTimeSpan? TotalStalledDueToReadThrottle
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadThrottle).Duration);
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x060017E2 RID: 6114 RVA: 0x00032B7B File Offset: 0x00030D7B
		public EnhancedTimeSpan? TotalStalledDueToWriteThrottle
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteThrottle).Duration);
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x060017E3 RID: 6115 RVA: 0x00032B99 File Offset: 0x00030D99
		public EnhancedTimeSpan? TotalStalledDueToReadCpu
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadCpu).Duration);
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x060017E4 RID: 6116 RVA: 0x00032BB7 File Offset: 0x00030DB7
		public EnhancedTimeSpan? TotalStalledDueToWriteCpu
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteCpu).Duration);
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x060017E5 RID: 6117 RVA: 0x00032BD5 File Offset: 0x00030DD5
		public EnhancedTimeSpan? TotalStalledDueToReadUnknown
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadUnknown).Duration);
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x060017E6 RID: 6118 RVA: 0x00032BF3 File Offset: 0x00030DF3
		public EnhancedTimeSpan? TotalStalledDueToWriteUnknown
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteUnknown).Duration);
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x00032C11 File Offset: 0x00030E11
		public EnhancedTimeSpan? TotalTransientFailureDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.TransientFailure).Duration);
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x060017E8 RID: 6120 RVA: 0x00032C2F File Offset: 0x00030E2F
		public EnhancedTimeSpan? TotalIdleDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Idle).Duration);
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x00032C4D File Offset: 0x00030E4D
		// (set) Token: 0x060017EA RID: 6122 RVA: 0x00032C55 File Offset: 0x00030E55
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

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x00032C5E File Offset: 0x00030E5E
		// (set) Token: 0x060017EC RID: 6124 RVA: 0x00032C6B File Offset: 0x00030E6B
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

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x00032C7A File Offset: 0x00030E7A
		// (set) Token: 0x060017EE RID: 6126 RVA: 0x00032C82 File Offset: 0x00030E82
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

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x060017EF RID: 6127 RVA: 0x00032C8B File Offset: 0x00030E8B
		public override ByteQuantifiedSize? BytesTransferred
		{
			get
			{
				return base.BytesTransferred;
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x00032C93 File Offset: 0x00030E93
		public override ByteQuantifiedSize? BytesTransferredPerMinute
		{
			get
			{
				return base.BytesTransferredPerMinute;
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x060017F1 RID: 6129 RVA: 0x00032C9B File Offset: 0x00030E9B
		public override ulong? ItemsTransferred
		{
			get
			{
				return base.ItemsTransferred;
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x00032CA3 File Offset: 0x00030EA3
		// (set) Token: 0x060017F3 RID: 6131 RVA: 0x00032CAB File Offset: 0x00030EAB
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

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x060017F4 RID: 6132 RVA: 0x00032CB4 File Offset: 0x00030EB4
		// (set) Token: 0x060017F5 RID: 6133 RVA: 0x00032CBC File Offset: 0x00030EBC
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

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x060017F6 RID: 6134 RVA: 0x00032CC5 File Offset: 0x00030EC5
		// (set) Token: 0x060017F7 RID: 6135 RVA: 0x00032CCD File Offset: 0x00030ECD
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

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x00032CD6 File Offset: 0x00030ED6
		// (set) Token: 0x060017F9 RID: 6137 RVA: 0x00032CDE File Offset: 0x00030EDE
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

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x060017FA RID: 6138 RVA: 0x00032CE7 File Offset: 0x00030EE7
		// (set) Token: 0x060017FB RID: 6139 RVA: 0x00032CEF File Offset: 0x00030EEF
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

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x00032CF8 File Offset: 0x00030EF8
		// (set) Token: 0x060017FD RID: 6141 RVA: 0x00032D00 File Offset: 0x00030F00
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

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x060017FE RID: 6142 RVA: 0x00032D09 File Offset: 0x00030F09
		// (set) Token: 0x060017FF RID: 6143 RVA: 0x00032D11 File Offset: 0x00030F11
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

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06001800 RID: 6144 RVA: 0x00032D1A File Offset: 0x00030F1A
		// (set) Token: 0x06001801 RID: 6145 RVA: 0x00032D22 File Offset: 0x00030F22
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

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x00032D2B File Offset: 0x00030F2B
		public DateTime? FailureTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Failure);
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x00032D3A File Offset: 0x00030F3A
		public override bool IsValid
		{
			get
			{
				return base.IsValid;
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x00032D42 File Offset: 0x00030F42
		// (set) Token: 0x06001805 RID: 6149 RVA: 0x00032D4A File Offset: 0x00030F4A
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

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x00032D53 File Offset: 0x00030F53
		// (set) Token: 0x06001807 RID: 6151 RVA: 0x00032D5B File Offset: 0x00030F5B
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

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x00032D64 File Offset: 0x00030F64
		// (set) Token: 0x06001809 RID: 6153 RVA: 0x00032D6C File Offset: 0x00030F6C
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

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x0600180A RID: 6154 RVA: 0x00032D75 File Offset: 0x00030F75
		// (set) Token: 0x0600180B RID: 6155 RVA: 0x00032D7D File Offset: 0x00030F7D
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

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x0600180C RID: 6156 RVA: 0x00032D86 File Offset: 0x00030F86
		// (set) Token: 0x0600180D RID: 6157 RVA: 0x00032D8E File Offset: 0x00030F8E
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

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x00032D9C File Offset: 0x00030F9C
		public new string DiagnosticInfo
		{
			get
			{
				return base.DiagnosticInfo;
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x00032DA4 File Offset: 0x00030FA4
		// (set) Token: 0x06001810 RID: 6160 RVA: 0x00032DAC File Offset: 0x00030FAC
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

		// Token: 0x06001811 RID: 6161 RVA: 0x00032DB5 File Offset: 0x00030FB5
		public override string ToString()
		{
			return this.Name ?? base.ToString();
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x00032DC8 File Offset: 0x00030FC8
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
				MailboxRestoreRequestStatistics.LoadAdditionalPropertiesFromUser(requestJob);
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
			MailboxRestoreRequestStatistics.LoadAdditionalPropertiesFromUser(requestJob);
			if (!requestJob.ValidateRequestIndexEntries())
			{
				return;
			}
			requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.Valid);
			requestJob.ValidationMessage = LocalizedString.Empty;
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x00032EF4 File Offset: 0x000310F4
		private static void LoadAdditionalPropertiesFromUser(RequestJobBase requestJob)
		{
			if (requestJob.TargetUser != null)
			{
				requestJob.TargetAlias = requestJob.TargetUser.Alias;
				requestJob.TargetExchangeGuid = (requestJob.TargetIsArchive ? requestJob.TargetUser.ArchiveGuid : requestJob.TargetUser.ExchangeGuid);
				requestJob.TargetDatabase = ADObjectIdResolutionHelper.ResolveDN(requestJob.TargetIsArchive ? requestJob.TargetUser.ArchiveDatabase : requestJob.TargetUser.Database);
				requestJob.TargetUserId = requestJob.TargetUser.Id;
			}
		}
	}
}
