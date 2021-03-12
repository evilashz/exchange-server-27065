using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200021E RID: 542
	[Serializable]
	public sealed class SyncRequestStatistics : RequestStatisticsBase
	{
		// Token: 0x06001CCD RID: 7373 RVA: 0x0003C201 File Offset: 0x0003A401
		public SyncRequestStatistics()
		{
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x0003C209 File Offset: 0x0003A409
		internal SyncRequestStatistics(SimpleProviderPropertyBag propertyBag) : base(propertyBag)
		{
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x0003C212 File Offset: 0x0003A412
		internal SyncRequestStatistics(TransactionalRequestJob requestJob) : this((SimpleProviderPropertyBag)requestJob.propertyBag)
		{
			base.CopyNonSchematizedPropertiesFrom(requestJob);
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x0003C22C File Offset: 0x0003A42C
		internal SyncRequestStatistics(RequestJobXML requestJob) : this((SimpleProviderPropertyBag)requestJob.propertyBag)
		{
			base.CopyNonSchematizedPropertiesFrom(requestJob);
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06001CD1 RID: 7377 RVA: 0x0003C246 File Offset: 0x0003A446
		// (set) Token: 0x06001CD2 RID: 7378 RVA: 0x0003C24E File Offset: 0x0003A44E
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

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x0003C257 File Offset: 0x0003A457
		// (set) Token: 0x06001CD4 RID: 7380 RVA: 0x0003C25F File Offset: 0x0003A45F
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

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06001CD5 RID: 7381 RVA: 0x0003C268 File Offset: 0x0003A468
		public new RequestState StatusDetail
		{
			get
			{
				return base.StatusDetail;
			}
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06001CD6 RID: 7382 RVA: 0x0003C270 File Offset: 0x0003A470
		// (set) Token: 0x06001CD7 RID: 7383 RVA: 0x0003C278 File Offset: 0x0003A478
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

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06001CD8 RID: 7384 RVA: 0x0003C281 File Offset: 0x0003A481
		// (set) Token: 0x06001CD9 RID: 7385 RVA: 0x0003C289 File Offset: 0x0003A489
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

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06001CDA RID: 7386 RVA: 0x0003C292 File Offset: 0x0003A492
		// (set) Token: 0x06001CDB RID: 7387 RVA: 0x0003C29A File Offset: 0x0003A49A
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

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06001CDC RID: 7388 RVA: 0x0003C2A3 File Offset: 0x0003A4A3
		// (set) Token: 0x06001CDD RID: 7389 RVA: 0x0003C2AB File Offset: 0x0003A4AB
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

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06001CDE RID: 7390 RVA: 0x0003C2B4 File Offset: 0x0003A4B4
		// (set) Token: 0x06001CDF RID: 7391 RVA: 0x0003C2BC File Offset: 0x0003A4BC
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

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06001CE0 RID: 7392 RVA: 0x0003C2C5 File Offset: 0x0003A4C5
		// (set) Token: 0x06001CE1 RID: 7393 RVA: 0x0003C2CD File Offset: 0x0003A4CD
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

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06001CE2 RID: 7394 RVA: 0x0003C2D6 File Offset: 0x0003A4D6
		// (set) Token: 0x06001CE3 RID: 7395 RVA: 0x0003C2DE File Offset: 0x0003A4DE
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

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06001CE4 RID: 7396 RVA: 0x0003C2E7 File Offset: 0x0003A4E7
		// (set) Token: 0x06001CE5 RID: 7397 RVA: 0x0003C2EF File Offset: 0x0003A4EF
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

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06001CE6 RID: 7398 RVA: 0x0003C2F8 File Offset: 0x0003A4F8
		// (set) Token: 0x06001CE7 RID: 7399 RVA: 0x0003C300 File Offset: 0x0003A500
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

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06001CE8 RID: 7400 RVA: 0x0003C309 File Offset: 0x0003A509
		// (set) Token: 0x06001CE9 RID: 7401 RVA: 0x0003C311 File Offset: 0x0003A511
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

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06001CEA RID: 7402 RVA: 0x0003C31A File Offset: 0x0003A51A
		// (set) Token: 0x06001CEB RID: 7403 RVA: 0x0003C322 File Offset: 0x0003A522
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

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06001CEC RID: 7404 RVA: 0x0003C32B File Offset: 0x0003A52B
		// (set) Token: 0x06001CED RID: 7405 RVA: 0x0003C333 File Offset: 0x0003A533
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

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06001CEE RID: 7406 RVA: 0x0003C33C File Offset: 0x0003A53C
		// (set) Token: 0x06001CEF RID: 7407 RVA: 0x0003C344 File Offset: 0x0003A544
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

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x0003C34D File Offset: 0x0003A54D
		// (set) Token: 0x06001CF1 RID: 7409 RVA: 0x0003C35A File Offset: 0x0003A55A
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

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06001CF2 RID: 7410 RVA: 0x0003C368 File Offset: 0x0003A568
		// (set) Token: 0x06001CF3 RID: 7411 RVA: 0x0003C370 File Offset: 0x0003A570
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

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06001CF4 RID: 7412 RVA: 0x0003C379 File Offset: 0x0003A579
		// (set) Token: 0x06001CF5 RID: 7413 RVA: 0x0003C381 File Offset: 0x0003A581
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

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06001CF6 RID: 7414 RVA: 0x0003C38A File Offset: 0x0003A58A
		// (set) Token: 0x06001CF7 RID: 7415 RVA: 0x0003C392 File Offset: 0x0003A592
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

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06001CF8 RID: 7416 RVA: 0x0003C39B File Offset: 0x0003A59B
		// (set) Token: 0x06001CF9 RID: 7417 RVA: 0x0003C3A3 File Offset: 0x0003A5A3
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

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06001CFA RID: 7418 RVA: 0x0003C3AC File Offset: 0x0003A5AC
		// (set) Token: 0x06001CFB RID: 7419 RVA: 0x0003C3B4 File Offset: 0x0003A5B4
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

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06001CFC RID: 7420 RVA: 0x0003C3BD File Offset: 0x0003A5BD
		// (set) Token: 0x06001CFD RID: 7421 RVA: 0x0003C3C5 File Offset: 0x0003A5C5
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

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06001CFE RID: 7422 RVA: 0x0003C3CE File Offset: 0x0003A5CE
		public DateTime? StartAfter
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.StartAfter);
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06001CFF RID: 7423 RVA: 0x0003C3DD File Offset: 0x0003A5DD
		public DateTime? CompleteAfter
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.CompleteAfter);
			}
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06001D00 RID: 7424 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		// (set) Token: 0x06001D01 RID: 7425 RVA: 0x0003C3F4 File Offset: 0x0003A5F4
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

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06001D02 RID: 7426 RVA: 0x0003C3FD File Offset: 0x0003A5FD
		// (set) Token: 0x06001D03 RID: 7427 RVA: 0x0003C405 File Offset: 0x0003A605
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

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06001D04 RID: 7428 RVA: 0x0003C40E File Offset: 0x0003A60E
		// (set) Token: 0x06001D05 RID: 7429 RVA: 0x0003C416 File Offset: 0x0003A616
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

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06001D06 RID: 7430 RVA: 0x0003C41F File Offset: 0x0003A61F
		// (set) Token: 0x06001D07 RID: 7431 RVA: 0x0003C427 File Offset: 0x0003A627
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

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06001D08 RID: 7432 RVA: 0x0003C430 File Offset: 0x0003A630
		public DateTime? QueuedTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Creation);
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06001D09 RID: 7433 RVA: 0x0003C43E File Offset: 0x0003A63E
		public DateTime? StartTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Start);
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06001D0A RID: 7434 RVA: 0x0003C44C File Offset: 0x0003A64C
		public DateTime? LastUpdateTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.LastUpdate);
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06001D0B RID: 7435 RVA: 0x0003C45A File Offset: 0x0003A65A
		public DateTime? SuspendedTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Suspended);
			}
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06001D0C RID: 7436 RVA: 0x0003C468 File Offset: 0x0003A668
		public EnhancedTimeSpan? OverallDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.OverallMove).Duration);
			}
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06001D0D RID: 7437 RVA: 0x0003C485 File Offset: 0x0003A685
		public EnhancedTimeSpan? TotalSuspendedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Suspended).Duration);
			}
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06001D0E RID: 7438 RVA: 0x0003C4A3 File Offset: 0x0003A6A3
		public EnhancedTimeSpan? TotalFailedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Failed).Duration);
			}
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06001D0F RID: 7439 RVA: 0x0003C4C1 File Offset: 0x0003A6C1
		public EnhancedTimeSpan? TotalQueuedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Queued).Duration);
			}
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06001D10 RID: 7440 RVA: 0x0003C4DE File Offset: 0x0003A6DE
		public EnhancedTimeSpan? TotalInProgressDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.InProgress).Duration);
			}
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06001D11 RID: 7441 RVA: 0x0003C4FB File Offset: 0x0003A6FB
		public EnhancedTimeSpan? TotalStalledDueToCIDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToCI).Duration);
			}
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06001D12 RID: 7442 RVA: 0x0003C519 File Offset: 0x0003A719
		public EnhancedTimeSpan? TotalStalledDueToHADuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToHA).Duration);
			}
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06001D13 RID: 7443 RVA: 0x0003C537 File Offset: 0x0003A737
		public EnhancedTimeSpan? TotalStalledDueToMailboxLockedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToMailboxLock).Duration);
			}
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06001D14 RID: 7444 RVA: 0x0003C555 File Offset: 0x0003A755
		public EnhancedTimeSpan? TotalStalledDueToReadThrottle
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadThrottle).Duration);
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06001D15 RID: 7445 RVA: 0x0003C573 File Offset: 0x0003A773
		public EnhancedTimeSpan? TotalStalledDueToWriteThrottle
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteThrottle).Duration);
			}
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06001D16 RID: 7446 RVA: 0x0003C591 File Offset: 0x0003A791
		public EnhancedTimeSpan? TotalStalledDueToReadCpu
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadCpu).Duration);
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06001D17 RID: 7447 RVA: 0x0003C5AF File Offset: 0x0003A7AF
		public EnhancedTimeSpan? TotalStalledDueToWriteCpu
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteCpu).Duration);
			}
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06001D18 RID: 7448 RVA: 0x0003C5CD File Offset: 0x0003A7CD
		public EnhancedTimeSpan? TotalStalledDueToReadUnknown
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadUnknown).Duration);
			}
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06001D19 RID: 7449 RVA: 0x0003C5EB File Offset: 0x0003A7EB
		public EnhancedTimeSpan? TotalStalledDueToWriteUnknown
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteUnknown).Duration);
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06001D1A RID: 7450 RVA: 0x0003C609 File Offset: 0x0003A809
		public EnhancedTimeSpan? TotalTransientFailureDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.TransientFailure).Duration);
			}
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06001D1B RID: 7451 RVA: 0x0003C627 File Offset: 0x0003A827
		public EnhancedTimeSpan? TotalIdleDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Idle).Duration);
			}
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06001D1C RID: 7452 RVA: 0x0003C645 File Offset: 0x0003A845
		// (set) Token: 0x06001D1D RID: 7453 RVA: 0x0003C64D File Offset: 0x0003A84D
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

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06001D1E RID: 7454 RVA: 0x0003C656 File Offset: 0x0003A856
		// (set) Token: 0x06001D1F RID: 7455 RVA: 0x0003C663 File Offset: 0x0003A863
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

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06001D20 RID: 7456 RVA: 0x0003C672 File Offset: 0x0003A872
		// (set) Token: 0x06001D21 RID: 7457 RVA: 0x0003C67A File Offset: 0x0003A87A
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

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06001D22 RID: 7458 RVA: 0x0003C683 File Offset: 0x0003A883
		public override ByteQuantifiedSize? BytesTransferred
		{
			get
			{
				return base.BytesTransferred;
			}
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06001D23 RID: 7459 RVA: 0x0003C68B File Offset: 0x0003A88B
		public override ByteQuantifiedSize? BytesTransferredPerMinute
		{
			get
			{
				return base.BytesTransferredPerMinute;
			}
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x06001D24 RID: 7460 RVA: 0x0003C693 File Offset: 0x0003A893
		public override ulong? ItemsTransferred
		{
			get
			{
				return base.ItemsTransferred;
			}
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x06001D25 RID: 7461 RVA: 0x0003C69B File Offset: 0x0003A89B
		// (set) Token: 0x06001D26 RID: 7462 RVA: 0x0003C6A3 File Offset: 0x0003A8A3
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

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x06001D27 RID: 7463 RVA: 0x0003C6AC File Offset: 0x0003A8AC
		// (set) Token: 0x06001D28 RID: 7464 RVA: 0x0003C6B4 File Offset: 0x0003A8B4
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

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06001D29 RID: 7465 RVA: 0x0003C6BD File Offset: 0x0003A8BD
		// (set) Token: 0x06001D2A RID: 7466 RVA: 0x0003C6C5 File Offset: 0x0003A8C5
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

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x06001D2B RID: 7467 RVA: 0x0003C6CE File Offset: 0x0003A8CE
		// (set) Token: 0x06001D2C RID: 7468 RVA: 0x0003C6D6 File Offset: 0x0003A8D6
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

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06001D2D RID: 7469 RVA: 0x0003C6DF File Offset: 0x0003A8DF
		// (set) Token: 0x06001D2E RID: 7470 RVA: 0x0003C6E7 File Offset: 0x0003A8E7
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

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06001D2F RID: 7471 RVA: 0x0003C6F0 File Offset: 0x0003A8F0
		// (set) Token: 0x06001D30 RID: 7472 RVA: 0x0003C6F8 File Offset: 0x0003A8F8
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

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06001D31 RID: 7473 RVA: 0x0003C701 File Offset: 0x0003A901
		// (set) Token: 0x06001D32 RID: 7474 RVA: 0x0003C709 File Offset: 0x0003A909
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

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06001D33 RID: 7475 RVA: 0x0003C712 File Offset: 0x0003A912
		// (set) Token: 0x06001D34 RID: 7476 RVA: 0x0003C71A File Offset: 0x0003A91A
		public new int PoisonCount
		{
			get
			{
				return base.PoisonCount;
			}
			internal set
			{
				base.PoisonCount = value;
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06001D35 RID: 7477 RVA: 0x0003C723 File Offset: 0x0003A923
		public DateTime? FailureTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Failure);
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06001D36 RID: 7478 RVA: 0x0003C732 File Offset: 0x0003A932
		public override bool IsValid
		{
			get
			{
				return base.IsValid;
			}
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06001D37 RID: 7479 RVA: 0x0003C73A File Offset: 0x0003A93A
		// (set) Token: 0x06001D38 RID: 7480 RVA: 0x0003C742 File Offset: 0x0003A942
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

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06001D39 RID: 7481 RVA: 0x0003C74B File Offset: 0x0003A94B
		// (set) Token: 0x06001D3A RID: 7482 RVA: 0x0003C753 File Offset: 0x0003A953
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

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06001D3B RID: 7483 RVA: 0x0003C75C File Offset: 0x0003A95C
		// (set) Token: 0x06001D3C RID: 7484 RVA: 0x0003C764 File Offset: 0x0003A964
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

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06001D3D RID: 7485 RVA: 0x0003C76D File Offset: 0x0003A96D
		// (set) Token: 0x06001D3E RID: 7486 RVA: 0x0003C775 File Offset: 0x0003A975
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

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x06001D3F RID: 7487 RVA: 0x0003C77E File Offset: 0x0003A97E
		// (set) Token: 0x06001D40 RID: 7488 RVA: 0x0003C786 File Offset: 0x0003A986
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

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x06001D41 RID: 7489 RVA: 0x0003C794 File Offset: 0x0003A994
		// (set) Token: 0x06001D42 RID: 7490 RVA: 0x0003C79C File Offset: 0x0003A99C
		public string RemoteServerName
		{
			get
			{
				return base.RemoteHostName;
			}
			internal set
			{
				base.RemoteHostName = value;
			}
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x06001D43 RID: 7491 RVA: 0x0003C7A5 File Offset: 0x0003A9A5
		// (set) Token: 0x06001D44 RID: 7492 RVA: 0x0003C7AD File Offset: 0x0003A9AD
		public int RemoteServerPort
		{
			get
			{
				return base.RemoteHostPort;
			}
			internal set
			{
				base.RemoteHostPort = value;
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x06001D45 RID: 7493 RVA: 0x0003C7B6 File Offset: 0x0003A9B6
		// (set) Token: 0x06001D46 RID: 7494 RVA: 0x0003C7BE File Offset: 0x0003A9BE
		public new string SmtpServerName
		{
			get
			{
				return base.SmtpServerName;
			}
			internal set
			{
				base.SmtpServerName = value;
			}
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06001D47 RID: 7495 RVA: 0x0003C7C7 File Offset: 0x0003A9C7
		// (set) Token: 0x06001D48 RID: 7496 RVA: 0x0003C7CF File Offset: 0x0003A9CF
		public new int SmtpServerPort
		{
			get
			{
				return base.SmtpServerPort;
			}
			internal set
			{
				base.SmtpServerPort = value;
			}
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06001D49 RID: 7497 RVA: 0x0003C7D8 File Offset: 0x0003A9D8
		// (set) Token: 0x06001D4A RID: 7498 RVA: 0x0003C7E0 File Offset: 0x0003A9E0
		public new IMAPSecurityMechanism SecurityMechanism
		{
			get
			{
				return base.SecurityMechanism;
			}
			internal set
			{
				base.SecurityMechanism = value;
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x06001D4B RID: 7499 RVA: 0x0003C7E9 File Offset: 0x0003A9E9
		// (set) Token: 0x06001D4C RID: 7500 RVA: 0x0003C7F1 File Offset: 0x0003A9F1
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

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x06001D4D RID: 7501 RVA: 0x0003C7FA File Offset: 0x0003A9FA
		// (set) Token: 0x06001D4E RID: 7502 RVA: 0x0003C802 File Offset: 0x0003AA02
		public new SyncProtocol SyncProtocol
		{
			get
			{
				return base.SyncProtocol;
			}
			internal set
			{
				base.SyncProtocol = value;
			}
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06001D4F RID: 7503 RVA: 0x0003C80B File Offset: 0x0003AA0B
		// (set) Token: 0x06001D50 RID: 7504 RVA: 0x0003C813 File Offset: 0x0003AA13
		public new SmtpAddress EmailAddress
		{
			get
			{
				return base.EmailAddress;
			}
			internal set
			{
				base.EmailAddress = value;
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06001D51 RID: 7505 RVA: 0x0003C81C File Offset: 0x0003AA1C
		// (set) Token: 0x06001D52 RID: 7506 RVA: 0x0003C824 File Offset: 0x0003AA24
		public new TimeSpan IncrementalSyncInterval
		{
			get
			{
				return base.IncrementalSyncInterval;
			}
			internal set
			{
				base.IncrementalSyncInterval = value;
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x06001D53 RID: 7507 RVA: 0x0003C82D File Offset: 0x0003AA2D
		// (set) Token: 0x06001D54 RID: 7508 RVA: 0x0003C835 File Offset: 0x0003AA35
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

		// Token: 0x06001D55 RID: 7509 RVA: 0x0003C83E File Offset: 0x0003AA3E
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.Name) && !string.IsNullOrEmpty(this.TargetAlias))
			{
				return string.Format("{0}\\{1}", this.TargetAlias, this.Name);
			}
			return base.ToString();
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x0003C878 File Offset: 0x0003AA78
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
				SyncRequestStatistics.LoadAdditionalPropertiesFromUser(requestJob);
				requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.Valid);
				requestJob.ValidationMessage = LocalizedString.Empty;
				return;
			}
			SyncRequestStatistics.LoadAdditionalPropertiesFromUser(requestJob);
			if (!requestJob.ValidateRequestIndexEntries())
			{
				return;
			}
			requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.Valid);
			requestJob.ValidationMessage = LocalizedString.Empty;
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x0003C978 File Offset: 0x0003AB78
		private static void LoadAdditionalPropertiesFromUser(RequestJobBase requestJob)
		{
			if (requestJob.TargetUser != null)
			{
				requestJob.TargetAlias = requestJob.TargetUser.Alias;
				if (!requestJob.Flags.HasFlag(RequestFlags.TargetIsAggregatedMailbox))
				{
					requestJob.TargetExchangeGuid = requestJob.TargetUser.ExchangeGuid;
				}
				requestJob.TargetDatabase = ADObjectIdResolutionHelper.ResolveDN(requestJob.TargetIsArchive ? requestJob.TargetUser.ArchiveDatabase : requestJob.TargetUser.Database);
				requestJob.RecipientTypeDetails = requestJob.TargetUser.RecipientTypeDetails;
				requestJob.TargetUserId = requestJob.TargetUser.Id;
			}
		}
	}
}
