using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000737 RID: 1847
	[Serializable]
	public class MailboxServer : ADPresentationObject
	{
		// Token: 0x17001E2E RID: 7726
		// (get) Token: 0x06005893 RID: 22675 RVA: 0x0013B063 File Offset: 0x00139263
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return MailboxServer.schema;
			}
		}

		// Token: 0x06005894 RID: 22676 RVA: 0x0013B06A File Offset: 0x0013926A
		public MailboxServer()
		{
		}

		// Token: 0x06005895 RID: 22677 RVA: 0x0013B072 File Offset: 0x00139272
		public MailboxServer(Server dataObject) : base(dataObject)
		{
		}

		// Token: 0x17001E2F RID: 7727
		// (get) Token: 0x06005896 RID: 22678 RVA: 0x0013B07B File Offset: 0x0013927B
		public new string Name
		{
			get
			{
				return (string)this[ADObjectSchema.Name];
			}
		}

		// Token: 0x17001E30 RID: 7728
		// (get) Token: 0x06005897 RID: 22679 RVA: 0x0013B08D File Offset: 0x0013928D
		public LocalLongFullPath DataPath
		{
			get
			{
				return (LocalLongFullPath)this[MailboxServerSchema.DataPath];
			}
		}

		// Token: 0x17001E31 RID: 7729
		// (get) Token: 0x06005898 RID: 22680 RVA: 0x0013B09F File Offset: 0x0013929F
		// (set) Token: 0x06005899 RID: 22681 RVA: 0x0013B0B1 File Offset: 0x001392B1
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? CalendarRepairWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.CalendarRepairWorkCycle];
			}
			set
			{
				this[MailboxServerSchema.CalendarRepairWorkCycle] = value;
			}
		}

		// Token: 0x17001E32 RID: 7730
		// (get) Token: 0x0600589A RID: 22682 RVA: 0x0013B0C4 File Offset: 0x001392C4
		// (set) Token: 0x0600589B RID: 22683 RVA: 0x0013B0D6 File Offset: 0x001392D6
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? CalendarRepairWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.CalendarRepairWorkCycleCheckpoint];
			}
			set
			{
				this[MailboxServerSchema.CalendarRepairWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001E33 RID: 7731
		// (get) Token: 0x0600589C RID: 22684 RVA: 0x0013B0E9 File Offset: 0x001392E9
		// (set) Token: 0x0600589D RID: 22685 RVA: 0x0013B0FB File Offset: 0x001392FB
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? SharingPolicyWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.SharingPolicyWorkCycle];
			}
			set
			{
				this[MailboxServerSchema.SharingPolicyWorkCycle] = value;
			}
		}

		// Token: 0x17001E34 RID: 7732
		// (get) Token: 0x0600589E RID: 22686 RVA: 0x0013B10E File Offset: 0x0013930E
		// (set) Token: 0x0600589F RID: 22687 RVA: 0x0013B120 File Offset: 0x00139320
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? SharingPolicyWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.SharingPolicyWorkCycleCheckpoint];
			}
			set
			{
				this[MailboxServerSchema.SharingPolicyWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001E35 RID: 7733
		// (get) Token: 0x060058A0 RID: 22688 RVA: 0x0013B133 File Offset: 0x00139333
		// (set) Token: 0x060058A1 RID: 22689 RVA: 0x0013B145 File Offset: 0x00139345
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? SharingSyncWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.SharingSyncWorkCycle];
			}
			set
			{
				this[MailboxServerSchema.SharingSyncWorkCycle] = value;
			}
		}

		// Token: 0x17001E36 RID: 7734
		// (get) Token: 0x060058A2 RID: 22690 RVA: 0x0013B158 File Offset: 0x00139358
		// (set) Token: 0x060058A3 RID: 22691 RVA: 0x0013B16A File Offset: 0x0013936A
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? SharingSyncWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.SharingSyncWorkCycleCheckpoint];
			}
			set
			{
				this[MailboxServerSchema.SharingSyncWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001E37 RID: 7735
		// (get) Token: 0x060058A4 RID: 22692 RVA: 0x0013B17D File Offset: 0x0013937D
		// (set) Token: 0x060058A5 RID: 22693 RVA: 0x0013B18F File Offset: 0x0013938F
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? PublicFolderWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.PublicFolderWorkCycle];
			}
			set
			{
				this[MailboxServerSchema.PublicFolderWorkCycle] = value;
			}
		}

		// Token: 0x17001E38 RID: 7736
		// (get) Token: 0x060058A6 RID: 22694 RVA: 0x0013B1A2 File Offset: 0x001393A2
		// (set) Token: 0x060058A7 RID: 22695 RVA: 0x0013B1B4 File Offset: 0x001393B4
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? PublicFolderWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.PublicFolderWorkCycleCheckpoint];
			}
			set
			{
				this[MailboxServerSchema.PublicFolderWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001E39 RID: 7737
		// (get) Token: 0x060058A8 RID: 22696 RVA: 0x0013B1C7 File Offset: 0x001393C7
		// (set) Token: 0x060058A9 RID: 22697 RVA: 0x0013B1D9 File Offset: 0x001393D9
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? SiteMailboxWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.SiteMailboxWorkCycle];
			}
			set
			{
				this[MailboxServerSchema.SiteMailboxWorkCycle] = value;
			}
		}

		// Token: 0x17001E3A RID: 7738
		// (get) Token: 0x060058AA RID: 22698 RVA: 0x0013B1EC File Offset: 0x001393EC
		// (set) Token: 0x060058AB RID: 22699 RVA: 0x0013B1FE File Offset: 0x001393FE
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? SiteMailboxWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.SiteMailboxWorkCycleCheckpoint];
			}
			set
			{
				this[MailboxServerSchema.SiteMailboxWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001E3B RID: 7739
		// (get) Token: 0x060058AC RID: 22700 RVA: 0x0013B211 File Offset: 0x00139411
		// (set) Token: 0x060058AD RID: 22701 RVA: 0x0013B223 File Offset: 0x00139423
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? ManagedFolderWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.ManagedFolderWorkCycle];
			}
			set
			{
				this[MailboxServerSchema.ManagedFolderWorkCycle] = value;
			}
		}

		// Token: 0x17001E3C RID: 7740
		// (get) Token: 0x060058AE RID: 22702 RVA: 0x0013B236 File Offset: 0x00139436
		// (set) Token: 0x060058AF RID: 22703 RVA: 0x0013B248 File Offset: 0x00139448
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? ManagedFolderWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.ManagedFolderWorkCycleCheckpoint];
			}
			set
			{
				this[MailboxServerSchema.ManagedFolderWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001E3D RID: 7741
		// (get) Token: 0x060058B0 RID: 22704 RVA: 0x0013B25B File Offset: 0x0013945B
		// (set) Token: 0x060058B1 RID: 22705 RVA: 0x0013B26D File Offset: 0x0013946D
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? MailboxAssociationReplicationWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.MailboxAssociationReplicationWorkCycle];
			}
			set
			{
				this[MailboxServerSchema.MailboxAssociationReplicationWorkCycle] = value;
			}
		}

		// Token: 0x17001E3E RID: 7742
		// (get) Token: 0x060058B2 RID: 22706 RVA: 0x0013B280 File Offset: 0x00139480
		// (set) Token: 0x060058B3 RID: 22707 RVA: 0x0013B292 File Offset: 0x00139492
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? MailboxAssociationReplicationWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.MailboxAssociationReplicationWorkCycleCheckpoint];
			}
			set
			{
				this[MailboxServerSchema.MailboxAssociationReplicationWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001E3F RID: 7743
		// (get) Token: 0x060058B4 RID: 22708 RVA: 0x0013B2A5 File Offset: 0x001394A5
		// (set) Token: 0x060058B5 RID: 22709 RVA: 0x0013B2B7 File Offset: 0x001394B7
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? GroupMailboxWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.GroupMailboxWorkCycle];
			}
			set
			{
				this[MailboxServerSchema.GroupMailboxWorkCycle] = value;
			}
		}

		// Token: 0x17001E40 RID: 7744
		// (get) Token: 0x060058B6 RID: 22710 RVA: 0x0013B2CA File Offset: 0x001394CA
		// (set) Token: 0x060058B7 RID: 22711 RVA: 0x0013B2DC File Offset: 0x001394DC
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? GroupMailboxWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.GroupMailboxWorkCycleCheckpoint];
			}
			set
			{
				this[MailboxServerSchema.GroupMailboxWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001E41 RID: 7745
		// (get) Token: 0x060058B8 RID: 22712 RVA: 0x0013B2EF File Offset: 0x001394EF
		// (set) Token: 0x060058B9 RID: 22713 RVA: 0x0013B301 File Offset: 0x00139501
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? TopNWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.TopNWorkCycle];
			}
			set
			{
				this[MailboxServerSchema.TopNWorkCycle] = value;
			}
		}

		// Token: 0x17001E42 RID: 7746
		// (get) Token: 0x060058BA RID: 22714 RVA: 0x0013B314 File Offset: 0x00139514
		// (set) Token: 0x060058BB RID: 22715 RVA: 0x0013B326 File Offset: 0x00139526
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? TopNWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.TopNWorkCycleCheckpoint];
			}
			set
			{
				this[MailboxServerSchema.TopNWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001E43 RID: 7747
		// (get) Token: 0x060058BC RID: 22716 RVA: 0x0013B339 File Offset: 0x00139539
		// (set) Token: 0x060058BD RID: 22717 RVA: 0x0013B34B File Offset: 0x0013954B
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? UMReportingWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.UMReportingWorkCycle];
			}
			set
			{
				this[MailboxServerSchema.UMReportingWorkCycle] = value;
			}
		}

		// Token: 0x17001E44 RID: 7748
		// (get) Token: 0x060058BE RID: 22718 RVA: 0x0013B35E File Offset: 0x0013955E
		// (set) Token: 0x060058BF RID: 22719 RVA: 0x0013B370 File Offset: 0x00139570
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? UMReportingWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.UMReportingWorkCycleCheckpoint];
			}
			set
			{
				this[MailboxServerSchema.UMReportingWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001E45 RID: 7749
		// (get) Token: 0x060058C0 RID: 22720 RVA: 0x0013B383 File Offset: 0x00139583
		// (set) Token: 0x060058C1 RID: 22721 RVA: 0x0013B395 File Offset: 0x00139595
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? InferenceTrainingWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.InferenceTrainingWorkCycle];
			}
			set
			{
				this[MailboxServerSchema.InferenceTrainingWorkCycle] = value;
			}
		}

		// Token: 0x17001E46 RID: 7750
		// (get) Token: 0x060058C2 RID: 22722 RVA: 0x0013B3A8 File Offset: 0x001395A8
		// (set) Token: 0x060058C3 RID: 22723 RVA: 0x0013B3BA File Offset: 0x001395BA
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? InferenceTrainingWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.InferenceTrainingWorkCycleCheckpoint];
			}
			set
			{
				this[MailboxServerSchema.InferenceTrainingWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001E47 RID: 7751
		// (get) Token: 0x060058C4 RID: 22724 RVA: 0x0013B3CD File Offset: 0x001395CD
		// (set) Token: 0x060058C5 RID: 22725 RVA: 0x0013B3DF File Offset: 0x001395DF
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? DirectoryProcessorWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.DirectoryProcessorWorkCycle];
			}
			set
			{
				this[MailboxServerSchema.DirectoryProcessorWorkCycle] = value;
			}
		}

		// Token: 0x17001E48 RID: 7752
		// (get) Token: 0x060058C6 RID: 22726 RVA: 0x0013B3F2 File Offset: 0x001395F2
		// (set) Token: 0x060058C7 RID: 22727 RVA: 0x0013B404 File Offset: 0x00139604
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? DirectoryProcessorWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.DirectoryProcessorWorkCycleCheckpoint];
			}
			set
			{
				this[MailboxServerSchema.DirectoryProcessorWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001E49 RID: 7753
		// (get) Token: 0x060058C8 RID: 22728 RVA: 0x0013B417 File Offset: 0x00139617
		// (set) Token: 0x060058C9 RID: 22729 RVA: 0x0013B429 File Offset: 0x00139629
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? OABGeneratorWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.OABGeneratorWorkCycle];
			}
			set
			{
				this[MailboxServerSchema.OABGeneratorWorkCycle] = value;
			}
		}

		// Token: 0x17001E4A RID: 7754
		// (get) Token: 0x060058CA RID: 22730 RVA: 0x0013B43C File Offset: 0x0013963C
		// (set) Token: 0x060058CB RID: 22731 RVA: 0x0013B44E File Offset: 0x0013964E
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? OABGeneratorWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.OABGeneratorWorkCycleCheckpoint];
			}
			set
			{
				this[MailboxServerSchema.OABGeneratorWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001E4B RID: 7755
		// (get) Token: 0x060058CC RID: 22732 RVA: 0x0013B461 File Offset: 0x00139661
		// (set) Token: 0x060058CD RID: 22733 RVA: 0x0013B473 File Offset: 0x00139673
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? InferenceDataCollectionWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.InferenceDataCollectionWorkCycle];
			}
			set
			{
				this[MailboxServerSchema.InferenceDataCollectionWorkCycle] = value;
			}
		}

		// Token: 0x17001E4C RID: 7756
		// (get) Token: 0x060058CE RID: 22734 RVA: 0x0013B486 File Offset: 0x00139686
		// (set) Token: 0x060058CF RID: 22735 RVA: 0x0013B498 File Offset: 0x00139698
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? InferenceDataCollectionWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.InferenceDataCollectionWorkCycleCheckpoint];
			}
			set
			{
				this[MailboxServerSchema.InferenceDataCollectionWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001E4D RID: 7757
		// (get) Token: 0x060058D0 RID: 22736 RVA: 0x0013B4AB File Offset: 0x001396AB
		// (set) Token: 0x060058D1 RID: 22737 RVA: 0x0013B4BD File Offset: 0x001396BD
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? PeopleRelevanceWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.PeopleRelevanceWorkCycle];
			}
			set
			{
				this[MailboxServerSchema.PeopleRelevanceWorkCycle] = value;
			}
		}

		// Token: 0x17001E4E RID: 7758
		// (get) Token: 0x060058D2 RID: 22738 RVA: 0x0013B4D0 File Offset: 0x001396D0
		// (set) Token: 0x060058D3 RID: 22739 RVA: 0x0013B4E2 File Offset: 0x001396E2
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? PeopleRelevanceWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.PeopleRelevanceWorkCycleCheckpoint];
			}
			set
			{
				this[MailboxServerSchema.PeopleRelevanceWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001E4F RID: 7759
		// (get) Token: 0x060058D4 RID: 22740 RVA: 0x0013B4F5 File Offset: 0x001396F5
		// (set) Token: 0x060058D5 RID: 22741 RVA: 0x0013B507 File Offset: 0x00139707
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? SharePointSignalStoreWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.SharePointSignalStoreWorkCycle];
			}
			set
			{
				this[MailboxServerSchema.SharePointSignalStoreWorkCycle] = value;
			}
		}

		// Token: 0x17001E50 RID: 7760
		// (get) Token: 0x060058D6 RID: 22742 RVA: 0x0013B51A File Offset: 0x0013971A
		// (set) Token: 0x060058D7 RID: 22743 RVA: 0x0013B52C File Offset: 0x0013972C
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? SharePointSignalStoreWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.SharePointSignalStoreWorkCycleCheckpoint];
			}
			set
			{
				this[MailboxServerSchema.SharePointSignalStoreWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001E51 RID: 7761
		// (get) Token: 0x060058D8 RID: 22744 RVA: 0x0013B53F File Offset: 0x0013973F
		// (set) Token: 0x060058D9 RID: 22745 RVA: 0x0013B551 File Offset: 0x00139751
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? PeopleCentricTriageWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.PeopleCentricTriageWorkCycle];
			}
			set
			{
				this[MailboxServerSchema.PeopleCentricTriageWorkCycle] = value;
			}
		}

		// Token: 0x17001E52 RID: 7762
		// (get) Token: 0x060058DA RID: 22746 RVA: 0x0013B564 File Offset: 0x00139764
		// (set) Token: 0x060058DB RID: 22747 RVA: 0x0013B576 File Offset: 0x00139776
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? PeopleCentricTriageWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.PeopleCentricTriageWorkCycleCheckpoint];
			}
			set
			{
				this[MailboxServerSchema.PeopleCentricTriageWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001E53 RID: 7763
		// (get) Token: 0x060058DC RID: 22748 RVA: 0x0013B589 File Offset: 0x00139789
		// (set) Token: 0x060058DD RID: 22749 RVA: 0x0013B59B File Offset: 0x0013979B
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? MailboxProcessorWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.MailboxProcessorWorkCycle];
			}
			set
			{
				this[MailboxServerSchema.MailboxProcessorWorkCycle] = value;
			}
		}

		// Token: 0x17001E54 RID: 7764
		// (get) Token: 0x060058DE RID: 22750 RVA: 0x0013B5AE File Offset: 0x001397AE
		// (set) Token: 0x060058DF RID: 22751 RVA: 0x0013B5C0 File Offset: 0x001397C0
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? StoreDsMaintenanceWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.StoreDsMaintenanceWorkCycle];
			}
			set
			{
				this[MailboxServerSchema.StoreDsMaintenanceWorkCycle] = value;
			}
		}

		// Token: 0x17001E55 RID: 7765
		// (get) Token: 0x060058E0 RID: 22752 RVA: 0x0013B5D3 File Offset: 0x001397D3
		// (set) Token: 0x060058E1 RID: 22753 RVA: 0x0013B5E5 File Offset: 0x001397E5
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? StoreDsMaintenanceWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.StoreDsMaintenanceWorkCycleCheckpoint];
			}
			set
			{
				this[MailboxServerSchema.StoreDsMaintenanceWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001E56 RID: 7766
		// (get) Token: 0x060058E2 RID: 22754 RVA: 0x0013B5F8 File Offset: 0x001397F8
		// (set) Token: 0x060058E3 RID: 22755 RVA: 0x0013B60A File Offset: 0x0013980A
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? StoreIntegrityCheckWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.StoreIntegrityCheckWorkCycle];
			}
			set
			{
				this[MailboxServerSchema.StoreIntegrityCheckWorkCycle] = value;
			}
		}

		// Token: 0x17001E57 RID: 7767
		// (get) Token: 0x060058E4 RID: 22756 RVA: 0x0013B61D File Offset: 0x0013981D
		// (set) Token: 0x060058E5 RID: 22757 RVA: 0x0013B62F File Offset: 0x0013982F
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? StoreIntegrityCheckWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.StoreIntegrityCheckWorkCycleCheckpoint];
			}
			set
			{
				this[MailboxServerSchema.StoreIntegrityCheckWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001E58 RID: 7768
		// (get) Token: 0x060058E6 RID: 22758 RVA: 0x0013B642 File Offset: 0x00139842
		// (set) Token: 0x060058E7 RID: 22759 RVA: 0x0013B654 File Offset: 0x00139854
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? StoreMaintenanceWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.StoreMaintenanceWorkCycle];
			}
			set
			{
				this[MailboxServerSchema.StoreMaintenanceWorkCycle] = value;
			}
		}

		// Token: 0x17001E59 RID: 7769
		// (get) Token: 0x060058E8 RID: 22760 RVA: 0x0013B667 File Offset: 0x00139867
		// (set) Token: 0x060058E9 RID: 22761 RVA: 0x0013B679 File Offset: 0x00139879
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? StoreMaintenanceWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.StoreMaintenanceWorkCycleCheckpoint];
			}
			set
			{
				this[MailboxServerSchema.StoreMaintenanceWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001E5A RID: 7770
		// (get) Token: 0x060058EA RID: 22762 RVA: 0x0013B68C File Offset: 0x0013988C
		// (set) Token: 0x060058EB RID: 22763 RVA: 0x0013B69E File Offset: 0x0013989E
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? StoreScheduledIntegrityCheckWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.StoreScheduledIntegrityCheckWorkCycle];
			}
			set
			{
				this[MailboxServerSchema.StoreScheduledIntegrityCheckWorkCycle] = value;
			}
		}

		// Token: 0x17001E5B RID: 7771
		// (get) Token: 0x060058EC RID: 22764 RVA: 0x0013B6B1 File Offset: 0x001398B1
		// (set) Token: 0x060058ED RID: 22765 RVA: 0x0013B6C3 File Offset: 0x001398C3
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? StoreScheduledIntegrityCheckWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.StoreScheduledIntegrityCheckWorkCycleCheckpoint];
			}
			set
			{
				this[MailboxServerSchema.StoreScheduledIntegrityCheckWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001E5C RID: 7772
		// (get) Token: 0x060058EE RID: 22766 RVA: 0x0013B6D6 File Offset: 0x001398D6
		// (set) Token: 0x060058EF RID: 22767 RVA: 0x0013B6E8 File Offset: 0x001398E8
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? StoreUrgentMaintenanceWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.StoreUrgentMaintenanceWorkCycle];
			}
			set
			{
				this[MailboxServerSchema.StoreUrgentMaintenanceWorkCycle] = value;
			}
		}

		// Token: 0x17001E5D RID: 7773
		// (get) Token: 0x060058F0 RID: 22768 RVA: 0x0013B6FB File Offset: 0x001398FB
		// (set) Token: 0x060058F1 RID: 22769 RVA: 0x0013B70D File Offset: 0x0013990D
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? StoreUrgentMaintenanceWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.StoreUrgentMaintenanceWorkCycleCheckpoint];
			}
			set
			{
				this[MailboxServerSchema.StoreUrgentMaintenanceWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001E5E RID: 7774
		// (get) Token: 0x060058F2 RID: 22770 RVA: 0x0013B720 File Offset: 0x00139920
		// (set) Token: 0x060058F3 RID: 22771 RVA: 0x0013B732 File Offset: 0x00139932
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? JunkEmailOptionsCommitterWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.JunkEmailOptionsCommitterWorkCycle];
			}
			set
			{
				this[MailboxServerSchema.JunkEmailOptionsCommitterWorkCycle] = value;
			}
		}

		// Token: 0x17001E5F RID: 7775
		// (get) Token: 0x060058F4 RID: 22772 RVA: 0x0013B745 File Offset: 0x00139945
		// (set) Token: 0x060058F5 RID: 22773 RVA: 0x0013B757 File Offset: 0x00139957
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? ProbeTimeBasedAssistantWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.ProbeTimeBasedAssistantWorkCycle];
			}
			set
			{
				this[MailboxServerSchema.ProbeTimeBasedAssistantWorkCycle] = value;
			}
		}

		// Token: 0x17001E60 RID: 7776
		// (get) Token: 0x060058F6 RID: 22774 RVA: 0x0013B76A File Offset: 0x0013996A
		// (set) Token: 0x060058F7 RID: 22775 RVA: 0x0013B77C File Offset: 0x0013997C
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? ProbeTimeBasedAssistantWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.ProbeTimeBasedAssistantWorkCycleCheckpoint];
			}
			set
			{
				this[MailboxServerSchema.ProbeTimeBasedAssistantWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001E61 RID: 7777
		// (get) Token: 0x060058F8 RID: 22776 RVA: 0x0013B78F File Offset: 0x0013998F
		// (set) Token: 0x060058F9 RID: 22777 RVA: 0x0013B7A1 File Offset: 0x001399A1
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? SearchIndexRepairTimeBasedAssistantWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.SearchIndexRepairTimeBasedAssistantWorkCycle];
			}
			set
			{
				this[MailboxServerSchema.SearchIndexRepairTimeBasedAssistantWorkCycle] = value;
			}
		}

		// Token: 0x17001E62 RID: 7778
		// (get) Token: 0x060058FA RID: 22778 RVA: 0x0013B7B4 File Offset: 0x001399B4
		// (set) Token: 0x060058FB RID: 22779 RVA: 0x0013B7C6 File Offset: 0x001399C6
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? SearchIndexRepairTimeBasedAssistantWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.SearchIndexRepairTimeBasedAssistantWorkCycleCheckpoint];
			}
			set
			{
				this[MailboxServerSchema.SearchIndexRepairTimeBasedAssistantWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001E63 RID: 7779
		// (get) Token: 0x060058FC RID: 22780 RVA: 0x0013B7D9 File Offset: 0x001399D9
		// (set) Token: 0x060058FD RID: 22781 RVA: 0x0013B7EB File Offset: 0x001399EB
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? DarTaskStoreTimeBasedAssistantWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.DarTaskStoreTimeBasedAssistantWorkCycle];
			}
			set
			{
				this[MailboxServerSchema.DarTaskStoreTimeBasedAssistantWorkCycle] = value;
			}
		}

		// Token: 0x17001E64 RID: 7780
		// (get) Token: 0x060058FE RID: 22782 RVA: 0x0013B7FE File Offset: 0x001399FE
		// (set) Token: 0x060058FF RID: 22783 RVA: 0x0013B810 File Offset: 0x00139A10
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? DarTaskStoreTimeBasedAssistantWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[MailboxServerSchema.DarTaskStoreTimeBasedAssistantWorkCycleCheckpoint];
			}
			set
			{
				this[MailboxServerSchema.DarTaskStoreTimeBasedAssistantWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001E65 RID: 7781
		// (get) Token: 0x06005900 RID: 22784 RVA: 0x0013B823 File Offset: 0x00139A23
		// (set) Token: 0x06005901 RID: 22785 RVA: 0x0013B835 File Offset: 0x00139A35
		[Parameter(Mandatory = false)]
		public ScheduleInterval[] SharingPolicySchedule
		{
			get
			{
				return (ScheduleInterval[])this[MailboxServerSchema.SharingPolicySchedule];
			}
			set
			{
				this[MailboxServerSchema.SharingPolicySchedule] = value;
			}
		}

		// Token: 0x17001E66 RID: 7782
		// (get) Token: 0x06005902 RID: 22786 RVA: 0x0013B843 File Offset: 0x00139A43
		// (set) Token: 0x06005903 RID: 22787 RVA: 0x0013B855 File Offset: 0x00139A55
		[Parameter(Mandatory = false)]
		public bool CalendarRepairMissingItemFixDisabled
		{
			get
			{
				return (bool)this[MailboxServerSchema.CalendarRepairMissingItemFixDisabled];
			}
			set
			{
				this[MailboxServerSchema.CalendarRepairMissingItemFixDisabled] = value;
			}
		}

		// Token: 0x17001E67 RID: 7783
		// (get) Token: 0x06005904 RID: 22788 RVA: 0x0013B868 File Offset: 0x00139A68
		// (set) Token: 0x06005905 RID: 22789 RVA: 0x0013B87A File Offset: 0x00139A7A
		[Parameter(Mandatory = false)]
		public bool CalendarRepairLogEnabled
		{
			get
			{
				return (bool)this[MailboxServerSchema.CalendarRepairLogEnabled];
			}
			set
			{
				this[MailboxServerSchema.CalendarRepairLogEnabled] = value;
			}
		}

		// Token: 0x17001E68 RID: 7784
		// (get) Token: 0x06005906 RID: 22790 RVA: 0x0013B88D File Offset: 0x00139A8D
		// (set) Token: 0x06005907 RID: 22791 RVA: 0x0013B89F File Offset: 0x00139A9F
		[Parameter(Mandatory = false)]
		public bool CalendarRepairLogSubjectLoggingEnabled
		{
			get
			{
				return (bool)this[MailboxServerSchema.CalendarRepairLogSubjectLoggingEnabled];
			}
			set
			{
				this[MailboxServerSchema.CalendarRepairLogSubjectLoggingEnabled] = value;
			}
		}

		// Token: 0x17001E69 RID: 7785
		// (get) Token: 0x06005908 RID: 22792 RVA: 0x0013B8B2 File Offset: 0x00139AB2
		// (set) Token: 0x06005909 RID: 22793 RVA: 0x0013B8C4 File Offset: 0x00139AC4
		[Parameter(Mandatory = false)]
		public LocalLongFullPath CalendarRepairLogPath
		{
			get
			{
				return (LocalLongFullPath)this[MailboxServerSchema.CalendarRepairLogPath];
			}
			set
			{
				this[MailboxServerSchema.CalendarRepairLogPath] = value;
			}
		}

		// Token: 0x17001E6A RID: 7786
		// (get) Token: 0x0600590A RID: 22794 RVA: 0x0013B8D2 File Offset: 0x00139AD2
		// (set) Token: 0x0600590B RID: 22795 RVA: 0x0013B8E4 File Offset: 0x00139AE4
		[Parameter(Mandatory = false)]
		public int CalendarRepairIntervalEndWindow
		{
			get
			{
				return (int)this[MailboxServerSchema.CalendarRepairIntervalEndWindow];
			}
			set
			{
				this[MailboxServerSchema.CalendarRepairIntervalEndWindow] = value;
			}
		}

		// Token: 0x17001E6B RID: 7787
		// (get) Token: 0x0600590C RID: 22796 RVA: 0x0013B8F7 File Offset: 0x00139AF7
		// (set) Token: 0x0600590D RID: 22797 RVA: 0x0013B909 File Offset: 0x00139B09
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan CalendarRepairLogFileAgeLimit
		{
			get
			{
				return (EnhancedTimeSpan)this[MailboxServerSchema.CalendarRepairLogFileAgeLimit];
			}
			set
			{
				this[MailboxServerSchema.CalendarRepairLogFileAgeLimit] = value;
			}
		}

		// Token: 0x17001E6C RID: 7788
		// (get) Token: 0x0600590E RID: 22798 RVA: 0x0013B91C File Offset: 0x00139B1C
		// (set) Token: 0x0600590F RID: 22799 RVA: 0x0013B92E File Offset: 0x00139B2E
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> CalendarRepairLogDirectorySizeLimit
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxServerSchema.CalendarRepairLogDirectorySizeLimit];
			}
			set
			{
				this[MailboxServerSchema.CalendarRepairLogDirectorySizeLimit] = value;
			}
		}

		// Token: 0x17001E6D RID: 7789
		// (get) Token: 0x06005910 RID: 22800 RVA: 0x0013B941 File Offset: 0x00139B41
		// (set) Token: 0x06005911 RID: 22801 RVA: 0x0013B953 File Offset: 0x00139B53
		[Parameter(Mandatory = false)]
		public CalendarRepairType CalendarRepairMode
		{
			get
			{
				return (CalendarRepairType)this[MailboxServerSchema.CalendarRepairMode];
			}
			set
			{
				this[MailboxServerSchema.CalendarRepairMode] = value;
			}
		}

		// Token: 0x17001E6E RID: 7790
		// (get) Token: 0x06005912 RID: 22802 RVA: 0x0013B966 File Offset: 0x00139B66
		// (set) Token: 0x06005913 RID: 22803 RVA: 0x0013B978 File Offset: 0x00139B78
		[Parameter(Mandatory = false)]
		public ScheduleInterval[] ManagedFolderAssistantSchedule
		{
			get
			{
				return (ScheduleInterval[])this[MailboxServerSchema.ElcSchedule];
			}
			set
			{
				this[MailboxServerSchema.ElcSchedule] = value;
			}
		}

		// Token: 0x17001E6F RID: 7791
		// (get) Token: 0x06005914 RID: 22804 RVA: 0x0013B986 File Offset: 0x00139B86
		// (set) Token: 0x06005915 RID: 22805 RVA: 0x0013B998 File Offset: 0x00139B98
		[Parameter(Mandatory = false)]
		public LocalLongFullPath LogPathForManagedFolders
		{
			get
			{
				return (LocalLongFullPath)this[MailboxServerSchema.ElcAuditLogPath];
			}
			set
			{
				this[MailboxServerSchema.ElcAuditLogPath] = value;
			}
		}

		// Token: 0x17001E70 RID: 7792
		// (get) Token: 0x06005916 RID: 22806 RVA: 0x0013B9A6 File Offset: 0x00139BA6
		// (set) Token: 0x06005917 RID: 22807 RVA: 0x0013B9B8 File Offset: 0x00139BB8
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan LogFileAgeLimitForManagedFolders
		{
			get
			{
				return (EnhancedTimeSpan)this[MailboxServerSchema.ElcAuditLogFileAgeLimit];
			}
			set
			{
				this[MailboxServerSchema.ElcAuditLogFileAgeLimit] = value;
			}
		}

		// Token: 0x17001E71 RID: 7793
		// (get) Token: 0x06005918 RID: 22808 RVA: 0x0013B9CB File Offset: 0x00139BCB
		// (set) Token: 0x06005919 RID: 22809 RVA: 0x0013B9DD File Offset: 0x00139BDD
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> LogDirectorySizeLimitForManagedFolders
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxServerSchema.ElcAuditLogDirectorySizeLimit];
			}
			set
			{
				this[MailboxServerSchema.ElcAuditLogDirectorySizeLimit] = value;
			}
		}

		// Token: 0x17001E72 RID: 7794
		// (get) Token: 0x0600591A RID: 22810 RVA: 0x0013B9F0 File Offset: 0x00139BF0
		// (set) Token: 0x0600591B RID: 22811 RVA: 0x0013BA02 File Offset: 0x00139C02
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> LogFileSizeLimitForManagedFolders
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxServerSchema.ElcAuditLogFileSizeLimit];
			}
			set
			{
				this[MailboxServerSchema.ElcAuditLogFileSizeLimit] = value;
			}
		}

		// Token: 0x17001E73 RID: 7795
		// (get) Token: 0x0600591C RID: 22812 RVA: 0x0013BA15 File Offset: 0x00139C15
		// (set) Token: 0x0600591D RID: 22813 RVA: 0x0013BA27 File Offset: 0x00139C27
		[Parameter(Mandatory = false)]
		public MigrationEventType MigrationLogLoggingLevel
		{
			get
			{
				return (MigrationEventType)this[MailboxServerSchema.MigrationLogLoggingLevel];
			}
			set
			{
				this[MailboxServerSchema.MigrationLogLoggingLevel] = value;
			}
		}

		// Token: 0x17001E74 RID: 7796
		// (get) Token: 0x0600591E RID: 22814 RVA: 0x0013BA3A File Offset: 0x00139C3A
		// (set) Token: 0x0600591F RID: 22815 RVA: 0x0013BA4C File Offset: 0x00139C4C
		[Parameter(Mandatory = false)]
		public LocalLongFullPath MigrationLogFilePath
		{
			get
			{
				return (LocalLongFullPath)this[MailboxServerSchema.MigrationLogFilePath];
			}
			set
			{
				this[MailboxServerSchema.MigrationLogFilePath] = value;
			}
		}

		// Token: 0x17001E75 RID: 7797
		// (get) Token: 0x06005920 RID: 22816 RVA: 0x0013BA5A File Offset: 0x00139C5A
		// (set) Token: 0x06005921 RID: 22817 RVA: 0x0013BA6C File Offset: 0x00139C6C
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan MigrationLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[MailboxServerSchema.MigrationLogMaxAge];
			}
			set
			{
				this[MailboxServerSchema.MigrationLogMaxAge] = value;
			}
		}

		// Token: 0x17001E76 RID: 7798
		// (get) Token: 0x06005922 RID: 22818 RVA: 0x0013BA7F File Offset: 0x00139C7F
		// (set) Token: 0x06005923 RID: 22819 RVA: 0x0013BA91 File Offset: 0x00139C91
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize MigrationLogMaxDirectorySize
		{
			get
			{
				return (ByteQuantifiedSize)this[MailboxServerSchema.MigrationLogMaxDirectorySize];
			}
			set
			{
				this[MailboxServerSchema.MigrationLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17001E77 RID: 7799
		// (get) Token: 0x06005924 RID: 22820 RVA: 0x0013BAA4 File Offset: 0x00139CA4
		// (set) Token: 0x06005925 RID: 22821 RVA: 0x0013BAB6 File Offset: 0x00139CB6
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize MigrationLogMaxFileSize
		{
			get
			{
				return (ByteQuantifiedSize)this[MailboxServerSchema.MigrationLogMaxFileSize];
			}
			set
			{
				this[MailboxServerSchema.MigrationLogMaxFileSize] = value;
			}
		}

		// Token: 0x17001E78 RID: 7800
		// (get) Token: 0x06005926 RID: 22822 RVA: 0x0013BAC9 File Offset: 0x00139CC9
		// (set) Token: 0x06005927 RID: 22823 RVA: 0x0013BADB File Offset: 0x00139CDB
		[Parameter(Mandatory = false)]
		public bool MAPIEncryptionRequired
		{
			get
			{
				return (bool)this[MailboxServerSchema.MAPIEncryptionRequired];
			}
			set
			{
				this[MailboxServerSchema.MAPIEncryptionRequired] = value;
			}
		}

		// Token: 0x17001E79 RID: 7801
		// (get) Token: 0x06005928 RID: 22824 RVA: 0x0013BAEE File Offset: 0x00139CEE
		// (set) Token: 0x06005929 RID: 22825 RVA: 0x0013BB00 File Offset: 0x00139D00
		[Parameter(Mandatory = false)]
		public bool RetentionLogForManagedFoldersEnabled
		{
			get
			{
				return (bool)this[MailboxServerSchema.ExpirationAuditLogEnabled];
			}
			set
			{
				this[MailboxServerSchema.ExpirationAuditLogEnabled] = value;
			}
		}

		// Token: 0x17001E7A RID: 7802
		// (get) Token: 0x0600592A RID: 22826 RVA: 0x0013BB13 File Offset: 0x00139D13
		// (set) Token: 0x0600592B RID: 22827 RVA: 0x0013BB25 File Offset: 0x00139D25
		[Parameter(Mandatory = false)]
		public bool JournalingLogForManagedFoldersEnabled
		{
			get
			{
				return (bool)this[MailboxServerSchema.AutocopyAuditLogEnabled];
			}
			set
			{
				this[MailboxServerSchema.AutocopyAuditLogEnabled] = value;
			}
		}

		// Token: 0x17001E7B RID: 7803
		// (get) Token: 0x0600592C RID: 22828 RVA: 0x0013BB38 File Offset: 0x00139D38
		// (set) Token: 0x0600592D RID: 22829 RVA: 0x0013BB4A File Offset: 0x00139D4A
		[Parameter(Mandatory = false)]
		public bool FolderLogForManagedFoldersEnabled
		{
			get
			{
				return (bool)this[MailboxServerSchema.FolderAuditLogEnabled];
			}
			set
			{
				this[MailboxServerSchema.FolderAuditLogEnabled] = value;
			}
		}

		// Token: 0x17001E7C RID: 7804
		// (get) Token: 0x0600592E RID: 22830 RVA: 0x0013BB5D File Offset: 0x00139D5D
		// (set) Token: 0x0600592F RID: 22831 RVA: 0x0013BB6F File Offset: 0x00139D6F
		[Parameter(Mandatory = false)]
		public bool SubjectLogForManagedFoldersEnabled
		{
			get
			{
				return (bool)this[MailboxServerSchema.ElcSubjectLoggingEnabled];
			}
			set
			{
				this[MailboxServerSchema.ElcSubjectLoggingEnabled] = value;
			}
		}

		// Token: 0x17001E7D RID: 7805
		// (get) Token: 0x06005930 RID: 22832 RVA: 0x0013BB82 File Offset: 0x00139D82
		// (set) Token: 0x06005931 RID: 22833 RVA: 0x0013BB94 File Offset: 0x00139D94
		public MultiValuedProperty<ADObjectId> SubmissionServerOverrideList
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ServerSchema.SubmissionServerOverrideList];
			}
			set
			{
				this[ServerSchema.SubmissionServerOverrideList] = value;
			}
		}

		// Token: 0x17001E7E RID: 7806
		// (get) Token: 0x06005932 RID: 22834 RVA: 0x0013BBA2 File Offset: 0x00139DA2
		// (set) Token: 0x06005933 RID: 22835 RVA: 0x0013BBB4 File Offset: 0x00139DB4
		[Parameter(Mandatory = false)]
		public AutoDatabaseMountDial AutoDatabaseMountDial
		{
			get
			{
				return (AutoDatabaseMountDial)this[MailboxServerSchema.AutoDatabaseMountDialType];
			}
			set
			{
				this[MailboxServerSchema.AutoDatabaseMountDialType] = value;
			}
		}

		// Token: 0x17001E7F RID: 7807
		// (get) Token: 0x06005934 RID: 22836 RVA: 0x0013BBC7 File Offset: 0x00139DC7
		// (set) Token: 0x06005935 RID: 22837 RVA: 0x0013BBD9 File Offset: 0x00139DD9
		[Parameter(Mandatory = false)]
		public bool ForceGroupMetricsGeneration
		{
			get
			{
				return (bool)this[MailboxServerSchema.ForceGroupMetricsGeneration];
			}
			set
			{
				this[MailboxServerSchema.ForceGroupMetricsGeneration] = value;
			}
		}

		// Token: 0x17001E80 RID: 7808
		// (get) Token: 0x06005936 RID: 22838 RVA: 0x0013BBEC File Offset: 0x00139DEC
		public bool IsPhoneticSupportEnabled
		{
			get
			{
				return (bool)this[MailboxServerSchema.IsPhoneticSupportEnabled];
			}
		}

		// Token: 0x17001E81 RID: 7809
		// (get) Token: 0x06005937 RID: 22839 RVA: 0x0013BBFE File Offset: 0x00139DFE
		// (set) Token: 0x06005938 RID: 22840 RVA: 0x0013BC10 File Offset: 0x00139E10
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<CultureInfo> Locale
		{
			get
			{
				return (MultiValuedProperty<CultureInfo>)this[MailboxServerSchema.Locale];
			}
			set
			{
				this[MailboxServerSchema.Locale] = value;
			}
		}

		// Token: 0x17001E82 RID: 7810
		// (get) Token: 0x06005939 RID: 22841 RVA: 0x0013BC1E File Offset: 0x00139E1E
		public ADObjectId DatabaseAvailabilityGroup
		{
			get
			{
				return (ADObjectId)this[MailboxServerSchema.DatabaseAvailabilityGroup];
			}
		}

		// Token: 0x17001E83 RID: 7811
		// (get) Token: 0x0600593A RID: 22842 RVA: 0x0013BC30 File Offset: 0x00139E30
		// (set) Token: 0x0600593B RID: 22843 RVA: 0x0013BC42 File Offset: 0x00139E42
		[Parameter(Mandatory = false)]
		public DatabaseCopyAutoActivationPolicyType DatabaseCopyAutoActivationPolicy
		{
			get
			{
				return (DatabaseCopyAutoActivationPolicyType)this[MailboxServerSchema.DatabaseCopyAutoActivationPolicy];
			}
			set
			{
				this[MailboxServerSchema.DatabaseCopyAutoActivationPolicy] = (int)value;
			}
		}

		// Token: 0x17001E84 RID: 7812
		// (get) Token: 0x0600593C RID: 22844 RVA: 0x0013BC55 File Offset: 0x00139E55
		// (set) Token: 0x0600593D RID: 22845 RVA: 0x0013BC67 File Offset: 0x00139E67
		[Parameter(Mandatory = false)]
		public bool DatabaseCopyActivationDisabledAndMoveNow
		{
			get
			{
				return (bool)this[MailboxServerSchema.DatabaseCopyActivationDisabledAndMoveNow];
			}
			set
			{
				this[MailboxServerSchema.DatabaseCopyActivationDisabledAndMoveNow] = value;
			}
		}

		// Token: 0x17001E85 RID: 7813
		// (get) Token: 0x0600593E RID: 22846 RVA: 0x0013BC7A File Offset: 0x00139E7A
		// (set) Token: 0x0600593F RID: 22847 RVA: 0x0013BC8C File Offset: 0x00139E8C
		[Parameter(Mandatory = false)]
		public string FaultZone
		{
			get
			{
				return (string)this[MailboxServerSchema.FaultZone];
			}
			set
			{
				this[MailboxServerSchema.FaultZone] = value;
			}
		}

		// Token: 0x17001E86 RID: 7814
		// (get) Token: 0x06005940 RID: 22848 RVA: 0x0013BC9A File Offset: 0x00139E9A
		// (set) Token: 0x06005941 RID: 22849 RVA: 0x0013BCAC File Offset: 0x00139EAC
		internal ServerAutoDagFlags AutoDagFlags
		{
			get
			{
				return (ServerAutoDagFlags)this[ActiveDirectoryServerSchema.AutoDagFlags];
			}
			set
			{
				this[ActiveDirectoryServerSchema.AutoDagFlags] = value;
			}
		}

		// Token: 0x17001E87 RID: 7815
		// (get) Token: 0x06005942 RID: 22850 RVA: 0x0013BCBF File Offset: 0x00139EBF
		// (set) Token: 0x06005943 RID: 22851 RVA: 0x0013BCD1 File Offset: 0x00139ED1
		[Parameter(Mandatory = false)]
		public bool AutoDagServerConfigured
		{
			get
			{
				return (bool)this[MailboxServerSchema.AutoDagServerConfigured];
			}
			set
			{
				this[MailboxServerSchema.AutoDagServerConfigured] = value;
			}
		}

		// Token: 0x17001E88 RID: 7816
		// (get) Token: 0x06005944 RID: 22852 RVA: 0x0013BCE4 File Offset: 0x00139EE4
		// (set) Token: 0x06005945 RID: 22853 RVA: 0x0013BCF6 File Offset: 0x00139EF6
		[Parameter(Mandatory = false)]
		public bool TransportSyncDispatchEnabled
		{
			get
			{
				return (bool)this[MailboxServerSchema.TransportSyncDispatchEnabled];
			}
			set
			{
				this[MailboxServerSchema.TransportSyncDispatchEnabled] = value;
			}
		}

		// Token: 0x17001E89 RID: 7817
		// (get) Token: 0x06005946 RID: 22854 RVA: 0x0013BD09 File Offset: 0x00139F09
		// (set) Token: 0x06005947 RID: 22855 RVA: 0x0013BD1B File Offset: 0x00139F1B
		[Parameter(Mandatory = false)]
		public int MaxTransportSyncDispatchers
		{
			get
			{
				return (int)this[MailboxServerSchema.MaxTransportSyncDispatchers];
			}
			set
			{
				this[MailboxServerSchema.MaxTransportSyncDispatchers] = value;
			}
		}

		// Token: 0x17001E8A RID: 7818
		// (get) Token: 0x06005948 RID: 22856 RVA: 0x0013BD2E File Offset: 0x00139F2E
		// (set) Token: 0x06005949 RID: 22857 RVA: 0x0013BD40 File Offset: 0x00139F40
		[Parameter(Mandatory = false)]
		public bool TransportSyncLogEnabled
		{
			get
			{
				return (bool)this[MailboxServerSchema.TransportSyncLogEnabled];
			}
			set
			{
				this[MailboxServerSchema.TransportSyncLogEnabled] = value;
			}
		}

		// Token: 0x17001E8B RID: 7819
		// (get) Token: 0x0600594A RID: 22858 RVA: 0x0013BD53 File Offset: 0x00139F53
		// (set) Token: 0x0600594B RID: 22859 RVA: 0x0013BD65 File Offset: 0x00139F65
		[Parameter(Mandatory = false)]
		public SyncLoggingLevel TransportSyncLogLoggingLevel
		{
			get
			{
				return (SyncLoggingLevel)this[MailboxServerSchema.TransportSyncLogLoggingLevel];
			}
			set
			{
				this[MailboxServerSchema.TransportSyncLogLoggingLevel] = value;
			}
		}

		// Token: 0x17001E8C RID: 7820
		// (get) Token: 0x0600594C RID: 22860 RVA: 0x0013BD78 File Offset: 0x00139F78
		// (set) Token: 0x0600594D RID: 22861 RVA: 0x0013BD8A File Offset: 0x00139F8A
		[Parameter(Mandatory = false)]
		public LocalLongFullPath TransportSyncLogFilePath
		{
			get
			{
				return (LocalLongFullPath)this[MailboxServerSchema.TransportSyncLogFilePath];
			}
			set
			{
				this[MailboxServerSchema.TransportSyncLogFilePath] = value;
			}
		}

		// Token: 0x17001E8D RID: 7821
		// (get) Token: 0x0600594E RID: 22862 RVA: 0x0013BD98 File Offset: 0x00139F98
		// (set) Token: 0x0600594F RID: 22863 RVA: 0x0013BDAA File Offset: 0x00139FAA
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan TransportSyncLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[MailboxServerSchema.TransportSyncLogMaxAge];
			}
			set
			{
				this[MailboxServerSchema.TransportSyncLogMaxAge] = value;
			}
		}

		// Token: 0x17001E8E RID: 7822
		// (get) Token: 0x06005950 RID: 22864 RVA: 0x0013BDBD File Offset: 0x00139FBD
		// (set) Token: 0x06005951 RID: 22865 RVA: 0x0013BDCF File Offset: 0x00139FCF
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize TransportSyncLogMaxDirectorySize
		{
			get
			{
				return (ByteQuantifiedSize)this[MailboxServerSchema.TransportSyncLogMaxDirectorySize];
			}
			set
			{
				this[MailboxServerSchema.TransportSyncLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17001E8F RID: 7823
		// (get) Token: 0x06005952 RID: 22866 RVA: 0x0013BDE2 File Offset: 0x00139FE2
		// (set) Token: 0x06005953 RID: 22867 RVA: 0x0013BDF4 File Offset: 0x00139FF4
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize TransportSyncLogMaxFileSize
		{
			get
			{
				return (ByteQuantifiedSize)this[MailboxServerSchema.TransportSyncLogMaxFileSize];
			}
			set
			{
				this[MailboxServerSchema.TransportSyncLogMaxFileSize] = value;
			}
		}

		// Token: 0x17001E90 RID: 7824
		// (get) Token: 0x06005954 RID: 22868 RVA: 0x0013BE07 File Offset: 0x0013A007
		// (set) Token: 0x06005955 RID: 22869 RVA: 0x0013BE19 File Offset: 0x0013A019
		[Parameter(Mandatory = false)]
		public bool TransportSyncMailboxHealthLogEnabled
		{
			get
			{
				return (bool)this[MailboxServerSchema.TransportSyncMailboxHealthLogEnabled];
			}
			set
			{
				this[MailboxServerSchema.TransportSyncMailboxHealthLogEnabled] = value;
			}
		}

		// Token: 0x17001E91 RID: 7825
		// (get) Token: 0x06005956 RID: 22870 RVA: 0x0013BE2C File Offset: 0x0013A02C
		// (set) Token: 0x06005957 RID: 22871 RVA: 0x0013BE3E File Offset: 0x0013A03E
		[Parameter(Mandatory = false)]
		public LocalLongFullPath TransportSyncMailboxHealthLogFilePath
		{
			get
			{
				return (LocalLongFullPath)this[MailboxServerSchema.TransportSyncMailboxHealthLogFilePath];
			}
			set
			{
				this[MailboxServerSchema.TransportSyncMailboxHealthLogFilePath] = value;
			}
		}

		// Token: 0x17001E92 RID: 7826
		// (get) Token: 0x06005958 RID: 22872 RVA: 0x0013BE4C File Offset: 0x0013A04C
		// (set) Token: 0x06005959 RID: 22873 RVA: 0x0013BE5E File Offset: 0x0013A05E
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan TransportSyncMailboxHealthLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[MailboxServerSchema.TransportSyncMailboxHealthLogMaxAge];
			}
			set
			{
				this[MailboxServerSchema.TransportSyncMailboxHealthLogMaxAge] = value;
			}
		}

		// Token: 0x17001E93 RID: 7827
		// (get) Token: 0x0600595A RID: 22874 RVA: 0x0013BE71 File Offset: 0x0013A071
		// (set) Token: 0x0600595B RID: 22875 RVA: 0x0013BE83 File Offset: 0x0013A083
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize TransportSyncMailboxHealthLogMaxDirectorySize
		{
			get
			{
				return (ByteQuantifiedSize)this[MailboxServerSchema.TransportSyncMailboxHealthLogMaxDirectorySize];
			}
			set
			{
				this[MailboxServerSchema.TransportSyncMailboxHealthLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17001E94 RID: 7828
		// (get) Token: 0x0600595C RID: 22876 RVA: 0x0013BE96 File Offset: 0x0013A096
		// (set) Token: 0x0600595D RID: 22877 RVA: 0x0013BEA8 File Offset: 0x0013A0A8
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize TransportSyncMailboxHealthLogMaxFileSize
		{
			get
			{
				return (ByteQuantifiedSize)this[MailboxServerSchema.TransportSyncMailboxHealthLogMaxFileSize];
			}
			set
			{
				this[MailboxServerSchema.TransportSyncMailboxHealthLogMaxFileSize] = value;
			}
		}

		// Token: 0x17001E95 RID: 7829
		// (get) Token: 0x0600595E RID: 22878 RVA: 0x0013BEBB File Offset: 0x0013A0BB
		// (set) Token: 0x0600595F RID: 22879 RVA: 0x0013BECD File Offset: 0x0013A0CD
		[Parameter(Mandatory = false)]
		public bool IsExcludedFromProvisioning
		{
			get
			{
				return (bool)this[MailboxServerSchema.IsExcludedFromProvisioning];
			}
			set
			{
				this[MailboxServerSchema.IsExcludedFromProvisioning] = value;
			}
		}

		// Token: 0x17001E96 RID: 7830
		// (get) Token: 0x06005960 RID: 22880 RVA: 0x0013BEE0 File Offset: 0x0013A0E0
		// (set) Token: 0x06005961 RID: 22881 RVA: 0x0013BEF2 File Offset: 0x0013A0F2
		[Parameter(Mandatory = false)]
		public int? MaximumActiveDatabases
		{
			get
			{
				return (int?)this[MailboxServerSchema.MaxActiveMailboxDatabases];
			}
			set
			{
				this[MailboxServerSchema.MaxActiveMailboxDatabases] = value;
			}
		}

		// Token: 0x17001E97 RID: 7831
		// (get) Token: 0x06005962 RID: 22882 RVA: 0x0013BF05 File Offset: 0x0013A105
		// (set) Token: 0x06005963 RID: 22883 RVA: 0x0013BF17 File Offset: 0x0013A117
		[Parameter(Mandatory = false)]
		public int? MaximumPreferredActiveDatabases
		{
			get
			{
				return (int?)this[MailboxServerSchema.MaxPreferredActiveDatabases];
			}
			set
			{
				this[MailboxServerSchema.MaxPreferredActiveDatabases] = value;
			}
		}

		// Token: 0x17001E98 RID: 7832
		// (get) Token: 0x06005964 RID: 22884 RVA: 0x0013BF2A File Offset: 0x0013A12A
		public ServerVersion AdminDisplayVersion
		{
			get
			{
				return (ServerVersion)this[ExchangeServerSchema.AdminDisplayVersion];
			}
		}

		// Token: 0x17001E99 RID: 7833
		// (get) Token: 0x06005965 RID: 22885 RVA: 0x0013BF3C File Offset: 0x0013A13C
		public ServerRole ServerRole
		{
			get
			{
				ServerRole serverRole = (ServerRole)this[ExchangeServerSchema.CurrentServerRole];
				if (!this.IsE15OrLater)
				{
					return serverRole;
				}
				return ExchangeServer.ConvertE15ServerRoleToOutput(serverRole);
			}
		}

		// Token: 0x17001E9A RID: 7834
		// (get) Token: 0x06005966 RID: 22886 RVA: 0x0013BF6A File Offset: 0x0013A16A
		public int ExchangeLegacyServerRole
		{
			get
			{
				return (int)this[ExchangeServerSchema.ExchangeLegacyServerRole];
			}
		}

		// Token: 0x17001E9B RID: 7835
		// (get) Token: 0x06005967 RID: 22887 RVA: 0x0013BF7C File Offset: 0x0013A17C
		private bool IsE15OrLater
		{
			get
			{
				return (bool)this[ExchangeServerSchema.IsE15OrLater];
			}
		}

		// Token: 0x04003C08 RID: 15368
		private static MailboxServerSchema schema = ObjectSchema.GetInstance<MailboxServerSchema>();
	}
}
