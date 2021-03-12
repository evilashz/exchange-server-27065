using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000666 RID: 1638
	public class SetMailboxServerCommand : SyntheticCommandWithPipelineInputNoOutput<MailboxServer>
	{
		// Token: 0x0600548D RID: 21645 RVA: 0x000850C1 File Offset: 0x000832C1
		private SetMailboxServerCommand() : base("Set-MailboxServer")
		{
		}

		// Token: 0x0600548E RID: 21646 RVA: 0x000850CE File Offset: 0x000832CE
		public SetMailboxServerCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600548F RID: 21647 RVA: 0x000850DD File Offset: 0x000832DD
		public virtual SetMailboxServerCommand SetParameters(SetMailboxServerCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005490 RID: 21648 RVA: 0x000850E7 File Offset: 0x000832E7
		public virtual SetMailboxServerCommand SetParameters(SetMailboxServerCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000667 RID: 1639
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700337A RID: 13178
			// (set) Token: 0x06005491 RID: 21649 RVA: 0x000850F1 File Offset: 0x000832F1
			public virtual MultiValuedProperty<ServerIdParameter> SubmissionServerOverrideList
			{
				set
				{
					base.PowerSharpParameters["SubmissionServerOverrideList"] = value;
				}
			}

			// Token: 0x1700337B RID: 13179
			// (set) Token: 0x06005492 RID: 21650 RVA: 0x00085104 File Offset: 0x00083304
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700337C RID: 13180
			// (set) Token: 0x06005493 RID: 21651 RVA: 0x00085117 File Offset: 0x00083317
			public virtual EnhancedTimeSpan? CalendarRepairWorkCycle
			{
				set
				{
					base.PowerSharpParameters["CalendarRepairWorkCycle"] = value;
				}
			}

			// Token: 0x1700337D RID: 13181
			// (set) Token: 0x06005494 RID: 21652 RVA: 0x0008512F File Offset: 0x0008332F
			public virtual EnhancedTimeSpan? CalendarRepairWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["CalendarRepairWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x1700337E RID: 13182
			// (set) Token: 0x06005495 RID: 21653 RVA: 0x00085147 File Offset: 0x00083347
			public virtual EnhancedTimeSpan? SharingPolicyWorkCycle
			{
				set
				{
					base.PowerSharpParameters["SharingPolicyWorkCycle"] = value;
				}
			}

			// Token: 0x1700337F RID: 13183
			// (set) Token: 0x06005496 RID: 21654 RVA: 0x0008515F File Offset: 0x0008335F
			public virtual EnhancedTimeSpan? SharingPolicyWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["SharingPolicyWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x17003380 RID: 13184
			// (set) Token: 0x06005497 RID: 21655 RVA: 0x00085177 File Offset: 0x00083377
			public virtual EnhancedTimeSpan? SharingSyncWorkCycle
			{
				set
				{
					base.PowerSharpParameters["SharingSyncWorkCycle"] = value;
				}
			}

			// Token: 0x17003381 RID: 13185
			// (set) Token: 0x06005498 RID: 21656 RVA: 0x0008518F File Offset: 0x0008338F
			public virtual EnhancedTimeSpan? SharingSyncWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["SharingSyncWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x17003382 RID: 13186
			// (set) Token: 0x06005499 RID: 21657 RVA: 0x000851A7 File Offset: 0x000833A7
			public virtual EnhancedTimeSpan? PublicFolderWorkCycle
			{
				set
				{
					base.PowerSharpParameters["PublicFolderWorkCycle"] = value;
				}
			}

			// Token: 0x17003383 RID: 13187
			// (set) Token: 0x0600549A RID: 21658 RVA: 0x000851BF File Offset: 0x000833BF
			public virtual EnhancedTimeSpan? PublicFolderWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["PublicFolderWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x17003384 RID: 13188
			// (set) Token: 0x0600549B RID: 21659 RVA: 0x000851D7 File Offset: 0x000833D7
			public virtual EnhancedTimeSpan? SiteMailboxWorkCycle
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxWorkCycle"] = value;
				}
			}

			// Token: 0x17003385 RID: 13189
			// (set) Token: 0x0600549C RID: 21660 RVA: 0x000851EF File Offset: 0x000833EF
			public virtual EnhancedTimeSpan? SiteMailboxWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x17003386 RID: 13190
			// (set) Token: 0x0600549D RID: 21661 RVA: 0x00085207 File Offset: 0x00083407
			public virtual EnhancedTimeSpan? ManagedFolderWorkCycle
			{
				set
				{
					base.PowerSharpParameters["ManagedFolderWorkCycle"] = value;
				}
			}

			// Token: 0x17003387 RID: 13191
			// (set) Token: 0x0600549E RID: 21662 RVA: 0x0008521F File Offset: 0x0008341F
			public virtual EnhancedTimeSpan? ManagedFolderWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["ManagedFolderWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x17003388 RID: 13192
			// (set) Token: 0x0600549F RID: 21663 RVA: 0x00085237 File Offset: 0x00083437
			public virtual EnhancedTimeSpan? MailboxAssociationReplicationWorkCycle
			{
				set
				{
					base.PowerSharpParameters["MailboxAssociationReplicationWorkCycle"] = value;
				}
			}

			// Token: 0x17003389 RID: 13193
			// (set) Token: 0x060054A0 RID: 21664 RVA: 0x0008524F File Offset: 0x0008344F
			public virtual EnhancedTimeSpan? MailboxAssociationReplicationWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["MailboxAssociationReplicationWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x1700338A RID: 13194
			// (set) Token: 0x060054A1 RID: 21665 RVA: 0x00085267 File Offset: 0x00083467
			public virtual EnhancedTimeSpan? GroupMailboxWorkCycle
			{
				set
				{
					base.PowerSharpParameters["GroupMailboxWorkCycle"] = value;
				}
			}

			// Token: 0x1700338B RID: 13195
			// (set) Token: 0x060054A2 RID: 21666 RVA: 0x0008527F File Offset: 0x0008347F
			public virtual EnhancedTimeSpan? GroupMailboxWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["GroupMailboxWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x1700338C RID: 13196
			// (set) Token: 0x060054A3 RID: 21667 RVA: 0x00085297 File Offset: 0x00083497
			public virtual EnhancedTimeSpan? TopNWorkCycle
			{
				set
				{
					base.PowerSharpParameters["TopNWorkCycle"] = value;
				}
			}

			// Token: 0x1700338D RID: 13197
			// (set) Token: 0x060054A4 RID: 21668 RVA: 0x000852AF File Offset: 0x000834AF
			public virtual EnhancedTimeSpan? TopNWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["TopNWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x1700338E RID: 13198
			// (set) Token: 0x060054A5 RID: 21669 RVA: 0x000852C7 File Offset: 0x000834C7
			public virtual EnhancedTimeSpan? UMReportingWorkCycle
			{
				set
				{
					base.PowerSharpParameters["UMReportingWorkCycle"] = value;
				}
			}

			// Token: 0x1700338F RID: 13199
			// (set) Token: 0x060054A6 RID: 21670 RVA: 0x000852DF File Offset: 0x000834DF
			public virtual EnhancedTimeSpan? UMReportingWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["UMReportingWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x17003390 RID: 13200
			// (set) Token: 0x060054A7 RID: 21671 RVA: 0x000852F7 File Offset: 0x000834F7
			public virtual EnhancedTimeSpan? InferenceTrainingWorkCycle
			{
				set
				{
					base.PowerSharpParameters["InferenceTrainingWorkCycle"] = value;
				}
			}

			// Token: 0x17003391 RID: 13201
			// (set) Token: 0x060054A8 RID: 21672 RVA: 0x0008530F File Offset: 0x0008350F
			public virtual EnhancedTimeSpan? InferenceTrainingWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["InferenceTrainingWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x17003392 RID: 13202
			// (set) Token: 0x060054A9 RID: 21673 RVA: 0x00085327 File Offset: 0x00083527
			public virtual EnhancedTimeSpan? DirectoryProcessorWorkCycle
			{
				set
				{
					base.PowerSharpParameters["DirectoryProcessorWorkCycle"] = value;
				}
			}

			// Token: 0x17003393 RID: 13203
			// (set) Token: 0x060054AA RID: 21674 RVA: 0x0008533F File Offset: 0x0008353F
			public virtual EnhancedTimeSpan? DirectoryProcessorWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["DirectoryProcessorWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x17003394 RID: 13204
			// (set) Token: 0x060054AB RID: 21675 RVA: 0x00085357 File Offset: 0x00083557
			public virtual EnhancedTimeSpan? OABGeneratorWorkCycle
			{
				set
				{
					base.PowerSharpParameters["OABGeneratorWorkCycle"] = value;
				}
			}

			// Token: 0x17003395 RID: 13205
			// (set) Token: 0x060054AC RID: 21676 RVA: 0x0008536F File Offset: 0x0008356F
			public virtual EnhancedTimeSpan? OABGeneratorWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["OABGeneratorWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x17003396 RID: 13206
			// (set) Token: 0x060054AD RID: 21677 RVA: 0x00085387 File Offset: 0x00083587
			public virtual EnhancedTimeSpan? InferenceDataCollectionWorkCycle
			{
				set
				{
					base.PowerSharpParameters["InferenceDataCollectionWorkCycle"] = value;
				}
			}

			// Token: 0x17003397 RID: 13207
			// (set) Token: 0x060054AE RID: 21678 RVA: 0x0008539F File Offset: 0x0008359F
			public virtual EnhancedTimeSpan? InferenceDataCollectionWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["InferenceDataCollectionWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x17003398 RID: 13208
			// (set) Token: 0x060054AF RID: 21679 RVA: 0x000853B7 File Offset: 0x000835B7
			public virtual EnhancedTimeSpan? PeopleRelevanceWorkCycle
			{
				set
				{
					base.PowerSharpParameters["PeopleRelevanceWorkCycle"] = value;
				}
			}

			// Token: 0x17003399 RID: 13209
			// (set) Token: 0x060054B0 RID: 21680 RVA: 0x000853CF File Offset: 0x000835CF
			public virtual EnhancedTimeSpan? PeopleRelevanceWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["PeopleRelevanceWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x1700339A RID: 13210
			// (set) Token: 0x060054B1 RID: 21681 RVA: 0x000853E7 File Offset: 0x000835E7
			public virtual EnhancedTimeSpan? SharePointSignalStoreWorkCycle
			{
				set
				{
					base.PowerSharpParameters["SharePointSignalStoreWorkCycle"] = value;
				}
			}

			// Token: 0x1700339B RID: 13211
			// (set) Token: 0x060054B2 RID: 21682 RVA: 0x000853FF File Offset: 0x000835FF
			public virtual EnhancedTimeSpan? SharePointSignalStoreWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["SharePointSignalStoreWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x1700339C RID: 13212
			// (set) Token: 0x060054B3 RID: 21683 RVA: 0x00085417 File Offset: 0x00083617
			public virtual EnhancedTimeSpan? PeopleCentricTriageWorkCycle
			{
				set
				{
					base.PowerSharpParameters["PeopleCentricTriageWorkCycle"] = value;
				}
			}

			// Token: 0x1700339D RID: 13213
			// (set) Token: 0x060054B4 RID: 21684 RVA: 0x0008542F File Offset: 0x0008362F
			public virtual EnhancedTimeSpan? PeopleCentricTriageWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["PeopleCentricTriageWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x1700339E RID: 13214
			// (set) Token: 0x060054B5 RID: 21685 RVA: 0x00085447 File Offset: 0x00083647
			public virtual EnhancedTimeSpan? MailboxProcessorWorkCycle
			{
				set
				{
					base.PowerSharpParameters["MailboxProcessorWorkCycle"] = value;
				}
			}

			// Token: 0x1700339F RID: 13215
			// (set) Token: 0x060054B6 RID: 21686 RVA: 0x0008545F File Offset: 0x0008365F
			public virtual EnhancedTimeSpan? StoreDsMaintenanceWorkCycle
			{
				set
				{
					base.PowerSharpParameters["StoreDsMaintenanceWorkCycle"] = value;
				}
			}

			// Token: 0x170033A0 RID: 13216
			// (set) Token: 0x060054B7 RID: 21687 RVA: 0x00085477 File Offset: 0x00083677
			public virtual EnhancedTimeSpan? StoreDsMaintenanceWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["StoreDsMaintenanceWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x170033A1 RID: 13217
			// (set) Token: 0x060054B8 RID: 21688 RVA: 0x0008548F File Offset: 0x0008368F
			public virtual EnhancedTimeSpan? StoreIntegrityCheckWorkCycle
			{
				set
				{
					base.PowerSharpParameters["StoreIntegrityCheckWorkCycle"] = value;
				}
			}

			// Token: 0x170033A2 RID: 13218
			// (set) Token: 0x060054B9 RID: 21689 RVA: 0x000854A7 File Offset: 0x000836A7
			public virtual EnhancedTimeSpan? StoreIntegrityCheckWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["StoreIntegrityCheckWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x170033A3 RID: 13219
			// (set) Token: 0x060054BA RID: 21690 RVA: 0x000854BF File Offset: 0x000836BF
			public virtual EnhancedTimeSpan? StoreMaintenanceWorkCycle
			{
				set
				{
					base.PowerSharpParameters["StoreMaintenanceWorkCycle"] = value;
				}
			}

			// Token: 0x170033A4 RID: 13220
			// (set) Token: 0x060054BB RID: 21691 RVA: 0x000854D7 File Offset: 0x000836D7
			public virtual EnhancedTimeSpan? StoreMaintenanceWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["StoreMaintenanceWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x170033A5 RID: 13221
			// (set) Token: 0x060054BC RID: 21692 RVA: 0x000854EF File Offset: 0x000836EF
			public virtual EnhancedTimeSpan? StoreScheduledIntegrityCheckWorkCycle
			{
				set
				{
					base.PowerSharpParameters["StoreScheduledIntegrityCheckWorkCycle"] = value;
				}
			}

			// Token: 0x170033A6 RID: 13222
			// (set) Token: 0x060054BD RID: 21693 RVA: 0x00085507 File Offset: 0x00083707
			public virtual EnhancedTimeSpan? StoreScheduledIntegrityCheckWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["StoreScheduledIntegrityCheckWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x170033A7 RID: 13223
			// (set) Token: 0x060054BE RID: 21694 RVA: 0x0008551F File Offset: 0x0008371F
			public virtual EnhancedTimeSpan? StoreUrgentMaintenanceWorkCycle
			{
				set
				{
					base.PowerSharpParameters["StoreUrgentMaintenanceWorkCycle"] = value;
				}
			}

			// Token: 0x170033A8 RID: 13224
			// (set) Token: 0x060054BF RID: 21695 RVA: 0x00085537 File Offset: 0x00083737
			public virtual EnhancedTimeSpan? StoreUrgentMaintenanceWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["StoreUrgentMaintenanceWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x170033A9 RID: 13225
			// (set) Token: 0x060054C0 RID: 21696 RVA: 0x0008554F File Offset: 0x0008374F
			public virtual EnhancedTimeSpan? JunkEmailOptionsCommitterWorkCycle
			{
				set
				{
					base.PowerSharpParameters["JunkEmailOptionsCommitterWorkCycle"] = value;
				}
			}

			// Token: 0x170033AA RID: 13226
			// (set) Token: 0x060054C1 RID: 21697 RVA: 0x00085567 File Offset: 0x00083767
			public virtual EnhancedTimeSpan? ProbeTimeBasedAssistantWorkCycle
			{
				set
				{
					base.PowerSharpParameters["ProbeTimeBasedAssistantWorkCycle"] = value;
				}
			}

			// Token: 0x170033AB RID: 13227
			// (set) Token: 0x060054C2 RID: 21698 RVA: 0x0008557F File Offset: 0x0008377F
			public virtual EnhancedTimeSpan? ProbeTimeBasedAssistantWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["ProbeTimeBasedAssistantWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x170033AC RID: 13228
			// (set) Token: 0x060054C3 RID: 21699 RVA: 0x00085597 File Offset: 0x00083797
			public virtual EnhancedTimeSpan? SearchIndexRepairTimeBasedAssistantWorkCycle
			{
				set
				{
					base.PowerSharpParameters["SearchIndexRepairTimeBasedAssistantWorkCycle"] = value;
				}
			}

			// Token: 0x170033AD RID: 13229
			// (set) Token: 0x060054C4 RID: 21700 RVA: 0x000855AF File Offset: 0x000837AF
			public virtual EnhancedTimeSpan? SearchIndexRepairTimeBasedAssistantWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["SearchIndexRepairTimeBasedAssistantWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x170033AE RID: 13230
			// (set) Token: 0x060054C5 RID: 21701 RVA: 0x000855C7 File Offset: 0x000837C7
			public virtual EnhancedTimeSpan? DarTaskStoreTimeBasedAssistantWorkCycle
			{
				set
				{
					base.PowerSharpParameters["DarTaskStoreTimeBasedAssistantWorkCycle"] = value;
				}
			}

			// Token: 0x170033AF RID: 13231
			// (set) Token: 0x060054C6 RID: 21702 RVA: 0x000855DF File Offset: 0x000837DF
			public virtual EnhancedTimeSpan? DarTaskStoreTimeBasedAssistantWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["DarTaskStoreTimeBasedAssistantWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x170033B0 RID: 13232
			// (set) Token: 0x060054C7 RID: 21703 RVA: 0x000855F7 File Offset: 0x000837F7
			public virtual ScheduleInterval SharingPolicySchedule
			{
				set
				{
					base.PowerSharpParameters["SharingPolicySchedule"] = value;
				}
			}

			// Token: 0x170033B1 RID: 13233
			// (set) Token: 0x060054C8 RID: 21704 RVA: 0x0008560F File Offset: 0x0008380F
			public virtual bool CalendarRepairMissingItemFixDisabled
			{
				set
				{
					base.PowerSharpParameters["CalendarRepairMissingItemFixDisabled"] = value;
				}
			}

			// Token: 0x170033B2 RID: 13234
			// (set) Token: 0x060054C9 RID: 21705 RVA: 0x00085627 File Offset: 0x00083827
			public virtual bool CalendarRepairLogEnabled
			{
				set
				{
					base.PowerSharpParameters["CalendarRepairLogEnabled"] = value;
				}
			}

			// Token: 0x170033B3 RID: 13235
			// (set) Token: 0x060054CA RID: 21706 RVA: 0x0008563F File Offset: 0x0008383F
			public virtual bool CalendarRepairLogSubjectLoggingEnabled
			{
				set
				{
					base.PowerSharpParameters["CalendarRepairLogSubjectLoggingEnabled"] = value;
				}
			}

			// Token: 0x170033B4 RID: 13236
			// (set) Token: 0x060054CB RID: 21707 RVA: 0x00085657 File Offset: 0x00083857
			public virtual LocalLongFullPath CalendarRepairLogPath
			{
				set
				{
					base.PowerSharpParameters["CalendarRepairLogPath"] = value;
				}
			}

			// Token: 0x170033B5 RID: 13237
			// (set) Token: 0x060054CC RID: 21708 RVA: 0x0008566A File Offset: 0x0008386A
			public virtual int CalendarRepairIntervalEndWindow
			{
				set
				{
					base.PowerSharpParameters["CalendarRepairIntervalEndWindow"] = value;
				}
			}

			// Token: 0x170033B6 RID: 13238
			// (set) Token: 0x060054CD RID: 21709 RVA: 0x00085682 File Offset: 0x00083882
			public virtual EnhancedTimeSpan CalendarRepairLogFileAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CalendarRepairLogFileAgeLimit"] = value;
				}
			}

			// Token: 0x170033B7 RID: 13239
			// (set) Token: 0x060054CE RID: 21710 RVA: 0x0008569A File Offset: 0x0008389A
			public virtual Unlimited<ByteQuantifiedSize> CalendarRepairLogDirectorySizeLimit
			{
				set
				{
					base.PowerSharpParameters["CalendarRepairLogDirectorySizeLimit"] = value;
				}
			}

			// Token: 0x170033B8 RID: 13240
			// (set) Token: 0x060054CF RID: 21711 RVA: 0x000856B2 File Offset: 0x000838B2
			public virtual CalendarRepairType CalendarRepairMode
			{
				set
				{
					base.PowerSharpParameters["CalendarRepairMode"] = value;
				}
			}

			// Token: 0x170033B9 RID: 13241
			// (set) Token: 0x060054D0 RID: 21712 RVA: 0x000856CA File Offset: 0x000838CA
			public virtual ScheduleInterval ManagedFolderAssistantSchedule
			{
				set
				{
					base.PowerSharpParameters["ManagedFolderAssistantSchedule"] = value;
				}
			}

			// Token: 0x170033BA RID: 13242
			// (set) Token: 0x060054D1 RID: 21713 RVA: 0x000856E2 File Offset: 0x000838E2
			public virtual LocalLongFullPath LogPathForManagedFolders
			{
				set
				{
					base.PowerSharpParameters["LogPathForManagedFolders"] = value;
				}
			}

			// Token: 0x170033BB RID: 13243
			// (set) Token: 0x060054D2 RID: 21714 RVA: 0x000856F5 File Offset: 0x000838F5
			public virtual EnhancedTimeSpan LogFileAgeLimitForManagedFolders
			{
				set
				{
					base.PowerSharpParameters["LogFileAgeLimitForManagedFolders"] = value;
				}
			}

			// Token: 0x170033BC RID: 13244
			// (set) Token: 0x060054D3 RID: 21715 RVA: 0x0008570D File Offset: 0x0008390D
			public virtual Unlimited<ByteQuantifiedSize> LogDirectorySizeLimitForManagedFolders
			{
				set
				{
					base.PowerSharpParameters["LogDirectorySizeLimitForManagedFolders"] = value;
				}
			}

			// Token: 0x170033BD RID: 13245
			// (set) Token: 0x060054D4 RID: 21716 RVA: 0x00085725 File Offset: 0x00083925
			public virtual Unlimited<ByteQuantifiedSize> LogFileSizeLimitForManagedFolders
			{
				set
				{
					base.PowerSharpParameters["LogFileSizeLimitForManagedFolders"] = value;
				}
			}

			// Token: 0x170033BE RID: 13246
			// (set) Token: 0x060054D5 RID: 21717 RVA: 0x0008573D File Offset: 0x0008393D
			public virtual MigrationEventType MigrationLogLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["MigrationLogLoggingLevel"] = value;
				}
			}

			// Token: 0x170033BF RID: 13247
			// (set) Token: 0x060054D6 RID: 21718 RVA: 0x00085755 File Offset: 0x00083955
			public virtual LocalLongFullPath MigrationLogFilePath
			{
				set
				{
					base.PowerSharpParameters["MigrationLogFilePath"] = value;
				}
			}

			// Token: 0x170033C0 RID: 13248
			// (set) Token: 0x060054D7 RID: 21719 RVA: 0x00085768 File Offset: 0x00083968
			public virtual EnhancedTimeSpan MigrationLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["MigrationLogMaxAge"] = value;
				}
			}

			// Token: 0x170033C1 RID: 13249
			// (set) Token: 0x060054D8 RID: 21720 RVA: 0x00085780 File Offset: 0x00083980
			public virtual ByteQuantifiedSize MigrationLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["MigrationLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170033C2 RID: 13250
			// (set) Token: 0x060054D9 RID: 21721 RVA: 0x00085798 File Offset: 0x00083998
			public virtual ByteQuantifiedSize MigrationLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["MigrationLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170033C3 RID: 13251
			// (set) Token: 0x060054DA RID: 21722 RVA: 0x000857B0 File Offset: 0x000839B0
			public virtual bool MAPIEncryptionRequired
			{
				set
				{
					base.PowerSharpParameters["MAPIEncryptionRequired"] = value;
				}
			}

			// Token: 0x170033C4 RID: 13252
			// (set) Token: 0x060054DB RID: 21723 RVA: 0x000857C8 File Offset: 0x000839C8
			public virtual bool RetentionLogForManagedFoldersEnabled
			{
				set
				{
					base.PowerSharpParameters["RetentionLogForManagedFoldersEnabled"] = value;
				}
			}

			// Token: 0x170033C5 RID: 13253
			// (set) Token: 0x060054DC RID: 21724 RVA: 0x000857E0 File Offset: 0x000839E0
			public virtual bool JournalingLogForManagedFoldersEnabled
			{
				set
				{
					base.PowerSharpParameters["JournalingLogForManagedFoldersEnabled"] = value;
				}
			}

			// Token: 0x170033C6 RID: 13254
			// (set) Token: 0x060054DD RID: 21725 RVA: 0x000857F8 File Offset: 0x000839F8
			public virtual bool FolderLogForManagedFoldersEnabled
			{
				set
				{
					base.PowerSharpParameters["FolderLogForManagedFoldersEnabled"] = value;
				}
			}

			// Token: 0x170033C7 RID: 13255
			// (set) Token: 0x060054DE RID: 21726 RVA: 0x00085810 File Offset: 0x00083A10
			public virtual bool SubjectLogForManagedFoldersEnabled
			{
				set
				{
					base.PowerSharpParameters["SubjectLogForManagedFoldersEnabled"] = value;
				}
			}

			// Token: 0x170033C8 RID: 13256
			// (set) Token: 0x060054DF RID: 21727 RVA: 0x00085828 File Offset: 0x00083A28
			public virtual AutoDatabaseMountDial AutoDatabaseMountDial
			{
				set
				{
					base.PowerSharpParameters["AutoDatabaseMountDial"] = value;
				}
			}

			// Token: 0x170033C9 RID: 13257
			// (set) Token: 0x060054E0 RID: 21728 RVA: 0x00085840 File Offset: 0x00083A40
			public virtual bool ForceGroupMetricsGeneration
			{
				set
				{
					base.PowerSharpParameters["ForceGroupMetricsGeneration"] = value;
				}
			}

			// Token: 0x170033CA RID: 13258
			// (set) Token: 0x060054E1 RID: 21729 RVA: 0x00085858 File Offset: 0x00083A58
			public virtual MultiValuedProperty<CultureInfo> Locale
			{
				set
				{
					base.PowerSharpParameters["Locale"] = value;
				}
			}

			// Token: 0x170033CB RID: 13259
			// (set) Token: 0x060054E2 RID: 21730 RVA: 0x0008586B File Offset: 0x00083A6B
			public virtual DatabaseCopyAutoActivationPolicyType DatabaseCopyAutoActivationPolicy
			{
				set
				{
					base.PowerSharpParameters["DatabaseCopyAutoActivationPolicy"] = value;
				}
			}

			// Token: 0x170033CC RID: 13260
			// (set) Token: 0x060054E3 RID: 21731 RVA: 0x00085883 File Offset: 0x00083A83
			public virtual bool DatabaseCopyActivationDisabledAndMoveNow
			{
				set
				{
					base.PowerSharpParameters["DatabaseCopyActivationDisabledAndMoveNow"] = value;
				}
			}

			// Token: 0x170033CD RID: 13261
			// (set) Token: 0x060054E4 RID: 21732 RVA: 0x0008589B File Offset: 0x00083A9B
			public virtual string FaultZone
			{
				set
				{
					base.PowerSharpParameters["FaultZone"] = value;
				}
			}

			// Token: 0x170033CE RID: 13262
			// (set) Token: 0x060054E5 RID: 21733 RVA: 0x000858AE File Offset: 0x00083AAE
			public virtual bool AutoDagServerConfigured
			{
				set
				{
					base.PowerSharpParameters["AutoDagServerConfigured"] = value;
				}
			}

			// Token: 0x170033CF RID: 13263
			// (set) Token: 0x060054E6 RID: 21734 RVA: 0x000858C6 File Offset: 0x00083AC6
			public virtual bool TransportSyncDispatchEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncDispatchEnabled"] = value;
				}
			}

			// Token: 0x170033D0 RID: 13264
			// (set) Token: 0x060054E7 RID: 21735 RVA: 0x000858DE File Offset: 0x00083ADE
			public virtual int MaxTransportSyncDispatchers
			{
				set
				{
					base.PowerSharpParameters["MaxTransportSyncDispatchers"] = value;
				}
			}

			// Token: 0x170033D1 RID: 13265
			// (set) Token: 0x060054E8 RID: 21736 RVA: 0x000858F6 File Offset: 0x00083AF6
			public virtual bool TransportSyncLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogEnabled"] = value;
				}
			}

			// Token: 0x170033D2 RID: 13266
			// (set) Token: 0x060054E9 RID: 21737 RVA: 0x0008590E File Offset: 0x00083B0E
			public virtual SyncLoggingLevel TransportSyncLogLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogLoggingLevel"] = value;
				}
			}

			// Token: 0x170033D3 RID: 13267
			// (set) Token: 0x060054EA RID: 21738 RVA: 0x00085926 File Offset: 0x00083B26
			public virtual LocalLongFullPath TransportSyncLogFilePath
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogFilePath"] = value;
				}
			}

			// Token: 0x170033D4 RID: 13268
			// (set) Token: 0x060054EB RID: 21739 RVA: 0x00085939 File Offset: 0x00083B39
			public virtual EnhancedTimeSpan TransportSyncLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogMaxAge"] = value;
				}
			}

			// Token: 0x170033D5 RID: 13269
			// (set) Token: 0x060054EC RID: 21740 RVA: 0x00085951 File Offset: 0x00083B51
			public virtual ByteQuantifiedSize TransportSyncLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170033D6 RID: 13270
			// (set) Token: 0x060054ED RID: 21741 RVA: 0x00085969 File Offset: 0x00083B69
			public virtual ByteQuantifiedSize TransportSyncLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170033D7 RID: 13271
			// (set) Token: 0x060054EE RID: 21742 RVA: 0x00085981 File Offset: 0x00083B81
			public virtual bool TransportSyncMailboxHealthLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxHealthLogEnabled"] = value;
				}
			}

			// Token: 0x170033D8 RID: 13272
			// (set) Token: 0x060054EF RID: 21743 RVA: 0x00085999 File Offset: 0x00083B99
			public virtual LocalLongFullPath TransportSyncMailboxHealthLogFilePath
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxHealthLogFilePath"] = value;
				}
			}

			// Token: 0x170033D9 RID: 13273
			// (set) Token: 0x060054F0 RID: 21744 RVA: 0x000859AC File Offset: 0x00083BAC
			public virtual EnhancedTimeSpan TransportSyncMailboxHealthLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxHealthLogMaxAge"] = value;
				}
			}

			// Token: 0x170033DA RID: 13274
			// (set) Token: 0x060054F1 RID: 21745 RVA: 0x000859C4 File Offset: 0x00083BC4
			public virtual ByteQuantifiedSize TransportSyncMailboxHealthLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxHealthLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170033DB RID: 13275
			// (set) Token: 0x060054F2 RID: 21746 RVA: 0x000859DC File Offset: 0x00083BDC
			public virtual ByteQuantifiedSize TransportSyncMailboxHealthLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxHealthLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170033DC RID: 13276
			// (set) Token: 0x060054F3 RID: 21747 RVA: 0x000859F4 File Offset: 0x00083BF4
			public virtual bool IsExcludedFromProvisioning
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromProvisioning"] = value;
				}
			}

			// Token: 0x170033DD RID: 13277
			// (set) Token: 0x060054F4 RID: 21748 RVA: 0x00085A0C File Offset: 0x00083C0C
			public virtual int? MaximumActiveDatabases
			{
				set
				{
					base.PowerSharpParameters["MaximumActiveDatabases"] = value;
				}
			}

			// Token: 0x170033DE RID: 13278
			// (set) Token: 0x060054F5 RID: 21749 RVA: 0x00085A24 File Offset: 0x00083C24
			public virtual int? MaximumPreferredActiveDatabases
			{
				set
				{
					base.PowerSharpParameters["MaximumPreferredActiveDatabases"] = value;
				}
			}

			// Token: 0x170033DF RID: 13279
			// (set) Token: 0x060054F6 RID: 21750 RVA: 0x00085A3C File Offset: 0x00083C3C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170033E0 RID: 13280
			// (set) Token: 0x060054F7 RID: 21751 RVA: 0x00085A54 File Offset: 0x00083C54
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170033E1 RID: 13281
			// (set) Token: 0x060054F8 RID: 21752 RVA: 0x00085A6C File Offset: 0x00083C6C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170033E2 RID: 13282
			// (set) Token: 0x060054F9 RID: 21753 RVA: 0x00085A84 File Offset: 0x00083C84
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170033E3 RID: 13283
			// (set) Token: 0x060054FA RID: 21754 RVA: 0x00085A9C File Offset: 0x00083C9C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000668 RID: 1640
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170033E4 RID: 13284
			// (set) Token: 0x060054FC RID: 21756 RVA: 0x00085ABC File Offset: 0x00083CBC
			public virtual MailboxServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170033E5 RID: 13285
			// (set) Token: 0x060054FD RID: 21757 RVA: 0x00085ACF File Offset: 0x00083CCF
			public virtual MultiValuedProperty<ServerIdParameter> SubmissionServerOverrideList
			{
				set
				{
					base.PowerSharpParameters["SubmissionServerOverrideList"] = value;
				}
			}

			// Token: 0x170033E6 RID: 13286
			// (set) Token: 0x060054FE RID: 21758 RVA: 0x00085AE2 File Offset: 0x00083CE2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170033E7 RID: 13287
			// (set) Token: 0x060054FF RID: 21759 RVA: 0x00085AF5 File Offset: 0x00083CF5
			public virtual EnhancedTimeSpan? CalendarRepairWorkCycle
			{
				set
				{
					base.PowerSharpParameters["CalendarRepairWorkCycle"] = value;
				}
			}

			// Token: 0x170033E8 RID: 13288
			// (set) Token: 0x06005500 RID: 21760 RVA: 0x00085B0D File Offset: 0x00083D0D
			public virtual EnhancedTimeSpan? CalendarRepairWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["CalendarRepairWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x170033E9 RID: 13289
			// (set) Token: 0x06005501 RID: 21761 RVA: 0x00085B25 File Offset: 0x00083D25
			public virtual EnhancedTimeSpan? SharingPolicyWorkCycle
			{
				set
				{
					base.PowerSharpParameters["SharingPolicyWorkCycle"] = value;
				}
			}

			// Token: 0x170033EA RID: 13290
			// (set) Token: 0x06005502 RID: 21762 RVA: 0x00085B3D File Offset: 0x00083D3D
			public virtual EnhancedTimeSpan? SharingPolicyWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["SharingPolicyWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x170033EB RID: 13291
			// (set) Token: 0x06005503 RID: 21763 RVA: 0x00085B55 File Offset: 0x00083D55
			public virtual EnhancedTimeSpan? SharingSyncWorkCycle
			{
				set
				{
					base.PowerSharpParameters["SharingSyncWorkCycle"] = value;
				}
			}

			// Token: 0x170033EC RID: 13292
			// (set) Token: 0x06005504 RID: 21764 RVA: 0x00085B6D File Offset: 0x00083D6D
			public virtual EnhancedTimeSpan? SharingSyncWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["SharingSyncWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x170033ED RID: 13293
			// (set) Token: 0x06005505 RID: 21765 RVA: 0x00085B85 File Offset: 0x00083D85
			public virtual EnhancedTimeSpan? PublicFolderWorkCycle
			{
				set
				{
					base.PowerSharpParameters["PublicFolderWorkCycle"] = value;
				}
			}

			// Token: 0x170033EE RID: 13294
			// (set) Token: 0x06005506 RID: 21766 RVA: 0x00085B9D File Offset: 0x00083D9D
			public virtual EnhancedTimeSpan? PublicFolderWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["PublicFolderWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x170033EF RID: 13295
			// (set) Token: 0x06005507 RID: 21767 RVA: 0x00085BB5 File Offset: 0x00083DB5
			public virtual EnhancedTimeSpan? SiteMailboxWorkCycle
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxWorkCycle"] = value;
				}
			}

			// Token: 0x170033F0 RID: 13296
			// (set) Token: 0x06005508 RID: 21768 RVA: 0x00085BCD File Offset: 0x00083DCD
			public virtual EnhancedTimeSpan? SiteMailboxWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x170033F1 RID: 13297
			// (set) Token: 0x06005509 RID: 21769 RVA: 0x00085BE5 File Offset: 0x00083DE5
			public virtual EnhancedTimeSpan? ManagedFolderWorkCycle
			{
				set
				{
					base.PowerSharpParameters["ManagedFolderWorkCycle"] = value;
				}
			}

			// Token: 0x170033F2 RID: 13298
			// (set) Token: 0x0600550A RID: 21770 RVA: 0x00085BFD File Offset: 0x00083DFD
			public virtual EnhancedTimeSpan? ManagedFolderWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["ManagedFolderWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x170033F3 RID: 13299
			// (set) Token: 0x0600550B RID: 21771 RVA: 0x00085C15 File Offset: 0x00083E15
			public virtual EnhancedTimeSpan? MailboxAssociationReplicationWorkCycle
			{
				set
				{
					base.PowerSharpParameters["MailboxAssociationReplicationWorkCycle"] = value;
				}
			}

			// Token: 0x170033F4 RID: 13300
			// (set) Token: 0x0600550C RID: 21772 RVA: 0x00085C2D File Offset: 0x00083E2D
			public virtual EnhancedTimeSpan? MailboxAssociationReplicationWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["MailboxAssociationReplicationWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x170033F5 RID: 13301
			// (set) Token: 0x0600550D RID: 21773 RVA: 0x00085C45 File Offset: 0x00083E45
			public virtual EnhancedTimeSpan? GroupMailboxWorkCycle
			{
				set
				{
					base.PowerSharpParameters["GroupMailboxWorkCycle"] = value;
				}
			}

			// Token: 0x170033F6 RID: 13302
			// (set) Token: 0x0600550E RID: 21774 RVA: 0x00085C5D File Offset: 0x00083E5D
			public virtual EnhancedTimeSpan? GroupMailboxWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["GroupMailboxWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x170033F7 RID: 13303
			// (set) Token: 0x0600550F RID: 21775 RVA: 0x00085C75 File Offset: 0x00083E75
			public virtual EnhancedTimeSpan? TopNWorkCycle
			{
				set
				{
					base.PowerSharpParameters["TopNWorkCycle"] = value;
				}
			}

			// Token: 0x170033F8 RID: 13304
			// (set) Token: 0x06005510 RID: 21776 RVA: 0x00085C8D File Offset: 0x00083E8D
			public virtual EnhancedTimeSpan? TopNWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["TopNWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x170033F9 RID: 13305
			// (set) Token: 0x06005511 RID: 21777 RVA: 0x00085CA5 File Offset: 0x00083EA5
			public virtual EnhancedTimeSpan? UMReportingWorkCycle
			{
				set
				{
					base.PowerSharpParameters["UMReportingWorkCycle"] = value;
				}
			}

			// Token: 0x170033FA RID: 13306
			// (set) Token: 0x06005512 RID: 21778 RVA: 0x00085CBD File Offset: 0x00083EBD
			public virtual EnhancedTimeSpan? UMReportingWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["UMReportingWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x170033FB RID: 13307
			// (set) Token: 0x06005513 RID: 21779 RVA: 0x00085CD5 File Offset: 0x00083ED5
			public virtual EnhancedTimeSpan? InferenceTrainingWorkCycle
			{
				set
				{
					base.PowerSharpParameters["InferenceTrainingWorkCycle"] = value;
				}
			}

			// Token: 0x170033FC RID: 13308
			// (set) Token: 0x06005514 RID: 21780 RVA: 0x00085CED File Offset: 0x00083EED
			public virtual EnhancedTimeSpan? InferenceTrainingWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["InferenceTrainingWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x170033FD RID: 13309
			// (set) Token: 0x06005515 RID: 21781 RVA: 0x00085D05 File Offset: 0x00083F05
			public virtual EnhancedTimeSpan? DirectoryProcessorWorkCycle
			{
				set
				{
					base.PowerSharpParameters["DirectoryProcessorWorkCycle"] = value;
				}
			}

			// Token: 0x170033FE RID: 13310
			// (set) Token: 0x06005516 RID: 21782 RVA: 0x00085D1D File Offset: 0x00083F1D
			public virtual EnhancedTimeSpan? DirectoryProcessorWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["DirectoryProcessorWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x170033FF RID: 13311
			// (set) Token: 0x06005517 RID: 21783 RVA: 0x00085D35 File Offset: 0x00083F35
			public virtual EnhancedTimeSpan? OABGeneratorWorkCycle
			{
				set
				{
					base.PowerSharpParameters["OABGeneratorWorkCycle"] = value;
				}
			}

			// Token: 0x17003400 RID: 13312
			// (set) Token: 0x06005518 RID: 21784 RVA: 0x00085D4D File Offset: 0x00083F4D
			public virtual EnhancedTimeSpan? OABGeneratorWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["OABGeneratorWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x17003401 RID: 13313
			// (set) Token: 0x06005519 RID: 21785 RVA: 0x00085D65 File Offset: 0x00083F65
			public virtual EnhancedTimeSpan? InferenceDataCollectionWorkCycle
			{
				set
				{
					base.PowerSharpParameters["InferenceDataCollectionWorkCycle"] = value;
				}
			}

			// Token: 0x17003402 RID: 13314
			// (set) Token: 0x0600551A RID: 21786 RVA: 0x00085D7D File Offset: 0x00083F7D
			public virtual EnhancedTimeSpan? InferenceDataCollectionWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["InferenceDataCollectionWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x17003403 RID: 13315
			// (set) Token: 0x0600551B RID: 21787 RVA: 0x00085D95 File Offset: 0x00083F95
			public virtual EnhancedTimeSpan? PeopleRelevanceWorkCycle
			{
				set
				{
					base.PowerSharpParameters["PeopleRelevanceWorkCycle"] = value;
				}
			}

			// Token: 0x17003404 RID: 13316
			// (set) Token: 0x0600551C RID: 21788 RVA: 0x00085DAD File Offset: 0x00083FAD
			public virtual EnhancedTimeSpan? PeopleRelevanceWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["PeopleRelevanceWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x17003405 RID: 13317
			// (set) Token: 0x0600551D RID: 21789 RVA: 0x00085DC5 File Offset: 0x00083FC5
			public virtual EnhancedTimeSpan? SharePointSignalStoreWorkCycle
			{
				set
				{
					base.PowerSharpParameters["SharePointSignalStoreWorkCycle"] = value;
				}
			}

			// Token: 0x17003406 RID: 13318
			// (set) Token: 0x0600551E RID: 21790 RVA: 0x00085DDD File Offset: 0x00083FDD
			public virtual EnhancedTimeSpan? SharePointSignalStoreWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["SharePointSignalStoreWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x17003407 RID: 13319
			// (set) Token: 0x0600551F RID: 21791 RVA: 0x00085DF5 File Offset: 0x00083FF5
			public virtual EnhancedTimeSpan? PeopleCentricTriageWorkCycle
			{
				set
				{
					base.PowerSharpParameters["PeopleCentricTriageWorkCycle"] = value;
				}
			}

			// Token: 0x17003408 RID: 13320
			// (set) Token: 0x06005520 RID: 21792 RVA: 0x00085E0D File Offset: 0x0008400D
			public virtual EnhancedTimeSpan? PeopleCentricTriageWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["PeopleCentricTriageWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x17003409 RID: 13321
			// (set) Token: 0x06005521 RID: 21793 RVA: 0x00085E25 File Offset: 0x00084025
			public virtual EnhancedTimeSpan? MailboxProcessorWorkCycle
			{
				set
				{
					base.PowerSharpParameters["MailboxProcessorWorkCycle"] = value;
				}
			}

			// Token: 0x1700340A RID: 13322
			// (set) Token: 0x06005522 RID: 21794 RVA: 0x00085E3D File Offset: 0x0008403D
			public virtual EnhancedTimeSpan? StoreDsMaintenanceWorkCycle
			{
				set
				{
					base.PowerSharpParameters["StoreDsMaintenanceWorkCycle"] = value;
				}
			}

			// Token: 0x1700340B RID: 13323
			// (set) Token: 0x06005523 RID: 21795 RVA: 0x00085E55 File Offset: 0x00084055
			public virtual EnhancedTimeSpan? StoreDsMaintenanceWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["StoreDsMaintenanceWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x1700340C RID: 13324
			// (set) Token: 0x06005524 RID: 21796 RVA: 0x00085E6D File Offset: 0x0008406D
			public virtual EnhancedTimeSpan? StoreIntegrityCheckWorkCycle
			{
				set
				{
					base.PowerSharpParameters["StoreIntegrityCheckWorkCycle"] = value;
				}
			}

			// Token: 0x1700340D RID: 13325
			// (set) Token: 0x06005525 RID: 21797 RVA: 0x00085E85 File Offset: 0x00084085
			public virtual EnhancedTimeSpan? StoreIntegrityCheckWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["StoreIntegrityCheckWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x1700340E RID: 13326
			// (set) Token: 0x06005526 RID: 21798 RVA: 0x00085E9D File Offset: 0x0008409D
			public virtual EnhancedTimeSpan? StoreMaintenanceWorkCycle
			{
				set
				{
					base.PowerSharpParameters["StoreMaintenanceWorkCycle"] = value;
				}
			}

			// Token: 0x1700340F RID: 13327
			// (set) Token: 0x06005527 RID: 21799 RVA: 0x00085EB5 File Offset: 0x000840B5
			public virtual EnhancedTimeSpan? StoreMaintenanceWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["StoreMaintenanceWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x17003410 RID: 13328
			// (set) Token: 0x06005528 RID: 21800 RVA: 0x00085ECD File Offset: 0x000840CD
			public virtual EnhancedTimeSpan? StoreScheduledIntegrityCheckWorkCycle
			{
				set
				{
					base.PowerSharpParameters["StoreScheduledIntegrityCheckWorkCycle"] = value;
				}
			}

			// Token: 0x17003411 RID: 13329
			// (set) Token: 0x06005529 RID: 21801 RVA: 0x00085EE5 File Offset: 0x000840E5
			public virtual EnhancedTimeSpan? StoreScheduledIntegrityCheckWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["StoreScheduledIntegrityCheckWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x17003412 RID: 13330
			// (set) Token: 0x0600552A RID: 21802 RVA: 0x00085EFD File Offset: 0x000840FD
			public virtual EnhancedTimeSpan? StoreUrgentMaintenanceWorkCycle
			{
				set
				{
					base.PowerSharpParameters["StoreUrgentMaintenanceWorkCycle"] = value;
				}
			}

			// Token: 0x17003413 RID: 13331
			// (set) Token: 0x0600552B RID: 21803 RVA: 0x00085F15 File Offset: 0x00084115
			public virtual EnhancedTimeSpan? StoreUrgentMaintenanceWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["StoreUrgentMaintenanceWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x17003414 RID: 13332
			// (set) Token: 0x0600552C RID: 21804 RVA: 0x00085F2D File Offset: 0x0008412D
			public virtual EnhancedTimeSpan? JunkEmailOptionsCommitterWorkCycle
			{
				set
				{
					base.PowerSharpParameters["JunkEmailOptionsCommitterWorkCycle"] = value;
				}
			}

			// Token: 0x17003415 RID: 13333
			// (set) Token: 0x0600552D RID: 21805 RVA: 0x00085F45 File Offset: 0x00084145
			public virtual EnhancedTimeSpan? ProbeTimeBasedAssistantWorkCycle
			{
				set
				{
					base.PowerSharpParameters["ProbeTimeBasedAssistantWorkCycle"] = value;
				}
			}

			// Token: 0x17003416 RID: 13334
			// (set) Token: 0x0600552E RID: 21806 RVA: 0x00085F5D File Offset: 0x0008415D
			public virtual EnhancedTimeSpan? ProbeTimeBasedAssistantWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["ProbeTimeBasedAssistantWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x17003417 RID: 13335
			// (set) Token: 0x0600552F RID: 21807 RVA: 0x00085F75 File Offset: 0x00084175
			public virtual EnhancedTimeSpan? SearchIndexRepairTimeBasedAssistantWorkCycle
			{
				set
				{
					base.PowerSharpParameters["SearchIndexRepairTimeBasedAssistantWorkCycle"] = value;
				}
			}

			// Token: 0x17003418 RID: 13336
			// (set) Token: 0x06005530 RID: 21808 RVA: 0x00085F8D File Offset: 0x0008418D
			public virtual EnhancedTimeSpan? SearchIndexRepairTimeBasedAssistantWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["SearchIndexRepairTimeBasedAssistantWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x17003419 RID: 13337
			// (set) Token: 0x06005531 RID: 21809 RVA: 0x00085FA5 File Offset: 0x000841A5
			public virtual EnhancedTimeSpan? DarTaskStoreTimeBasedAssistantWorkCycle
			{
				set
				{
					base.PowerSharpParameters["DarTaskStoreTimeBasedAssistantWorkCycle"] = value;
				}
			}

			// Token: 0x1700341A RID: 13338
			// (set) Token: 0x06005532 RID: 21810 RVA: 0x00085FBD File Offset: 0x000841BD
			public virtual EnhancedTimeSpan? DarTaskStoreTimeBasedAssistantWorkCycleCheckpoint
			{
				set
				{
					base.PowerSharpParameters["DarTaskStoreTimeBasedAssistantWorkCycleCheckpoint"] = value;
				}
			}

			// Token: 0x1700341B RID: 13339
			// (set) Token: 0x06005533 RID: 21811 RVA: 0x00085FD5 File Offset: 0x000841D5
			public virtual ScheduleInterval SharingPolicySchedule
			{
				set
				{
					base.PowerSharpParameters["SharingPolicySchedule"] = value;
				}
			}

			// Token: 0x1700341C RID: 13340
			// (set) Token: 0x06005534 RID: 21812 RVA: 0x00085FED File Offset: 0x000841ED
			public virtual bool CalendarRepairMissingItemFixDisabled
			{
				set
				{
					base.PowerSharpParameters["CalendarRepairMissingItemFixDisabled"] = value;
				}
			}

			// Token: 0x1700341D RID: 13341
			// (set) Token: 0x06005535 RID: 21813 RVA: 0x00086005 File Offset: 0x00084205
			public virtual bool CalendarRepairLogEnabled
			{
				set
				{
					base.PowerSharpParameters["CalendarRepairLogEnabled"] = value;
				}
			}

			// Token: 0x1700341E RID: 13342
			// (set) Token: 0x06005536 RID: 21814 RVA: 0x0008601D File Offset: 0x0008421D
			public virtual bool CalendarRepairLogSubjectLoggingEnabled
			{
				set
				{
					base.PowerSharpParameters["CalendarRepairLogSubjectLoggingEnabled"] = value;
				}
			}

			// Token: 0x1700341F RID: 13343
			// (set) Token: 0x06005537 RID: 21815 RVA: 0x00086035 File Offset: 0x00084235
			public virtual LocalLongFullPath CalendarRepairLogPath
			{
				set
				{
					base.PowerSharpParameters["CalendarRepairLogPath"] = value;
				}
			}

			// Token: 0x17003420 RID: 13344
			// (set) Token: 0x06005538 RID: 21816 RVA: 0x00086048 File Offset: 0x00084248
			public virtual int CalendarRepairIntervalEndWindow
			{
				set
				{
					base.PowerSharpParameters["CalendarRepairIntervalEndWindow"] = value;
				}
			}

			// Token: 0x17003421 RID: 13345
			// (set) Token: 0x06005539 RID: 21817 RVA: 0x00086060 File Offset: 0x00084260
			public virtual EnhancedTimeSpan CalendarRepairLogFileAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CalendarRepairLogFileAgeLimit"] = value;
				}
			}

			// Token: 0x17003422 RID: 13346
			// (set) Token: 0x0600553A RID: 21818 RVA: 0x00086078 File Offset: 0x00084278
			public virtual Unlimited<ByteQuantifiedSize> CalendarRepairLogDirectorySizeLimit
			{
				set
				{
					base.PowerSharpParameters["CalendarRepairLogDirectorySizeLimit"] = value;
				}
			}

			// Token: 0x17003423 RID: 13347
			// (set) Token: 0x0600553B RID: 21819 RVA: 0x00086090 File Offset: 0x00084290
			public virtual CalendarRepairType CalendarRepairMode
			{
				set
				{
					base.PowerSharpParameters["CalendarRepairMode"] = value;
				}
			}

			// Token: 0x17003424 RID: 13348
			// (set) Token: 0x0600553C RID: 21820 RVA: 0x000860A8 File Offset: 0x000842A8
			public virtual ScheduleInterval ManagedFolderAssistantSchedule
			{
				set
				{
					base.PowerSharpParameters["ManagedFolderAssistantSchedule"] = value;
				}
			}

			// Token: 0x17003425 RID: 13349
			// (set) Token: 0x0600553D RID: 21821 RVA: 0x000860C0 File Offset: 0x000842C0
			public virtual LocalLongFullPath LogPathForManagedFolders
			{
				set
				{
					base.PowerSharpParameters["LogPathForManagedFolders"] = value;
				}
			}

			// Token: 0x17003426 RID: 13350
			// (set) Token: 0x0600553E RID: 21822 RVA: 0x000860D3 File Offset: 0x000842D3
			public virtual EnhancedTimeSpan LogFileAgeLimitForManagedFolders
			{
				set
				{
					base.PowerSharpParameters["LogFileAgeLimitForManagedFolders"] = value;
				}
			}

			// Token: 0x17003427 RID: 13351
			// (set) Token: 0x0600553F RID: 21823 RVA: 0x000860EB File Offset: 0x000842EB
			public virtual Unlimited<ByteQuantifiedSize> LogDirectorySizeLimitForManagedFolders
			{
				set
				{
					base.PowerSharpParameters["LogDirectorySizeLimitForManagedFolders"] = value;
				}
			}

			// Token: 0x17003428 RID: 13352
			// (set) Token: 0x06005540 RID: 21824 RVA: 0x00086103 File Offset: 0x00084303
			public virtual Unlimited<ByteQuantifiedSize> LogFileSizeLimitForManagedFolders
			{
				set
				{
					base.PowerSharpParameters["LogFileSizeLimitForManagedFolders"] = value;
				}
			}

			// Token: 0x17003429 RID: 13353
			// (set) Token: 0x06005541 RID: 21825 RVA: 0x0008611B File Offset: 0x0008431B
			public virtual MigrationEventType MigrationLogLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["MigrationLogLoggingLevel"] = value;
				}
			}

			// Token: 0x1700342A RID: 13354
			// (set) Token: 0x06005542 RID: 21826 RVA: 0x00086133 File Offset: 0x00084333
			public virtual LocalLongFullPath MigrationLogFilePath
			{
				set
				{
					base.PowerSharpParameters["MigrationLogFilePath"] = value;
				}
			}

			// Token: 0x1700342B RID: 13355
			// (set) Token: 0x06005543 RID: 21827 RVA: 0x00086146 File Offset: 0x00084346
			public virtual EnhancedTimeSpan MigrationLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["MigrationLogMaxAge"] = value;
				}
			}

			// Token: 0x1700342C RID: 13356
			// (set) Token: 0x06005544 RID: 21828 RVA: 0x0008615E File Offset: 0x0008435E
			public virtual ByteQuantifiedSize MigrationLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["MigrationLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700342D RID: 13357
			// (set) Token: 0x06005545 RID: 21829 RVA: 0x00086176 File Offset: 0x00084376
			public virtual ByteQuantifiedSize MigrationLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["MigrationLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700342E RID: 13358
			// (set) Token: 0x06005546 RID: 21830 RVA: 0x0008618E File Offset: 0x0008438E
			public virtual bool MAPIEncryptionRequired
			{
				set
				{
					base.PowerSharpParameters["MAPIEncryptionRequired"] = value;
				}
			}

			// Token: 0x1700342F RID: 13359
			// (set) Token: 0x06005547 RID: 21831 RVA: 0x000861A6 File Offset: 0x000843A6
			public virtual bool RetentionLogForManagedFoldersEnabled
			{
				set
				{
					base.PowerSharpParameters["RetentionLogForManagedFoldersEnabled"] = value;
				}
			}

			// Token: 0x17003430 RID: 13360
			// (set) Token: 0x06005548 RID: 21832 RVA: 0x000861BE File Offset: 0x000843BE
			public virtual bool JournalingLogForManagedFoldersEnabled
			{
				set
				{
					base.PowerSharpParameters["JournalingLogForManagedFoldersEnabled"] = value;
				}
			}

			// Token: 0x17003431 RID: 13361
			// (set) Token: 0x06005549 RID: 21833 RVA: 0x000861D6 File Offset: 0x000843D6
			public virtual bool FolderLogForManagedFoldersEnabled
			{
				set
				{
					base.PowerSharpParameters["FolderLogForManagedFoldersEnabled"] = value;
				}
			}

			// Token: 0x17003432 RID: 13362
			// (set) Token: 0x0600554A RID: 21834 RVA: 0x000861EE File Offset: 0x000843EE
			public virtual bool SubjectLogForManagedFoldersEnabled
			{
				set
				{
					base.PowerSharpParameters["SubjectLogForManagedFoldersEnabled"] = value;
				}
			}

			// Token: 0x17003433 RID: 13363
			// (set) Token: 0x0600554B RID: 21835 RVA: 0x00086206 File Offset: 0x00084406
			public virtual AutoDatabaseMountDial AutoDatabaseMountDial
			{
				set
				{
					base.PowerSharpParameters["AutoDatabaseMountDial"] = value;
				}
			}

			// Token: 0x17003434 RID: 13364
			// (set) Token: 0x0600554C RID: 21836 RVA: 0x0008621E File Offset: 0x0008441E
			public virtual bool ForceGroupMetricsGeneration
			{
				set
				{
					base.PowerSharpParameters["ForceGroupMetricsGeneration"] = value;
				}
			}

			// Token: 0x17003435 RID: 13365
			// (set) Token: 0x0600554D RID: 21837 RVA: 0x00086236 File Offset: 0x00084436
			public virtual MultiValuedProperty<CultureInfo> Locale
			{
				set
				{
					base.PowerSharpParameters["Locale"] = value;
				}
			}

			// Token: 0x17003436 RID: 13366
			// (set) Token: 0x0600554E RID: 21838 RVA: 0x00086249 File Offset: 0x00084449
			public virtual DatabaseCopyAutoActivationPolicyType DatabaseCopyAutoActivationPolicy
			{
				set
				{
					base.PowerSharpParameters["DatabaseCopyAutoActivationPolicy"] = value;
				}
			}

			// Token: 0x17003437 RID: 13367
			// (set) Token: 0x0600554F RID: 21839 RVA: 0x00086261 File Offset: 0x00084461
			public virtual bool DatabaseCopyActivationDisabledAndMoveNow
			{
				set
				{
					base.PowerSharpParameters["DatabaseCopyActivationDisabledAndMoveNow"] = value;
				}
			}

			// Token: 0x17003438 RID: 13368
			// (set) Token: 0x06005550 RID: 21840 RVA: 0x00086279 File Offset: 0x00084479
			public virtual string FaultZone
			{
				set
				{
					base.PowerSharpParameters["FaultZone"] = value;
				}
			}

			// Token: 0x17003439 RID: 13369
			// (set) Token: 0x06005551 RID: 21841 RVA: 0x0008628C File Offset: 0x0008448C
			public virtual bool AutoDagServerConfigured
			{
				set
				{
					base.PowerSharpParameters["AutoDagServerConfigured"] = value;
				}
			}

			// Token: 0x1700343A RID: 13370
			// (set) Token: 0x06005552 RID: 21842 RVA: 0x000862A4 File Offset: 0x000844A4
			public virtual bool TransportSyncDispatchEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncDispatchEnabled"] = value;
				}
			}

			// Token: 0x1700343B RID: 13371
			// (set) Token: 0x06005553 RID: 21843 RVA: 0x000862BC File Offset: 0x000844BC
			public virtual int MaxTransportSyncDispatchers
			{
				set
				{
					base.PowerSharpParameters["MaxTransportSyncDispatchers"] = value;
				}
			}

			// Token: 0x1700343C RID: 13372
			// (set) Token: 0x06005554 RID: 21844 RVA: 0x000862D4 File Offset: 0x000844D4
			public virtual bool TransportSyncLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogEnabled"] = value;
				}
			}

			// Token: 0x1700343D RID: 13373
			// (set) Token: 0x06005555 RID: 21845 RVA: 0x000862EC File Offset: 0x000844EC
			public virtual SyncLoggingLevel TransportSyncLogLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogLoggingLevel"] = value;
				}
			}

			// Token: 0x1700343E RID: 13374
			// (set) Token: 0x06005556 RID: 21846 RVA: 0x00086304 File Offset: 0x00084504
			public virtual LocalLongFullPath TransportSyncLogFilePath
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogFilePath"] = value;
				}
			}

			// Token: 0x1700343F RID: 13375
			// (set) Token: 0x06005557 RID: 21847 RVA: 0x00086317 File Offset: 0x00084517
			public virtual EnhancedTimeSpan TransportSyncLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogMaxAge"] = value;
				}
			}

			// Token: 0x17003440 RID: 13376
			// (set) Token: 0x06005558 RID: 21848 RVA: 0x0008632F File Offset: 0x0008452F
			public virtual ByteQuantifiedSize TransportSyncLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003441 RID: 13377
			// (set) Token: 0x06005559 RID: 21849 RVA: 0x00086347 File Offset: 0x00084547
			public virtual ByteQuantifiedSize TransportSyncLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003442 RID: 13378
			// (set) Token: 0x0600555A RID: 21850 RVA: 0x0008635F File Offset: 0x0008455F
			public virtual bool TransportSyncMailboxHealthLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxHealthLogEnabled"] = value;
				}
			}

			// Token: 0x17003443 RID: 13379
			// (set) Token: 0x0600555B RID: 21851 RVA: 0x00086377 File Offset: 0x00084577
			public virtual LocalLongFullPath TransportSyncMailboxHealthLogFilePath
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxHealthLogFilePath"] = value;
				}
			}

			// Token: 0x17003444 RID: 13380
			// (set) Token: 0x0600555C RID: 21852 RVA: 0x0008638A File Offset: 0x0008458A
			public virtual EnhancedTimeSpan TransportSyncMailboxHealthLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxHealthLogMaxAge"] = value;
				}
			}

			// Token: 0x17003445 RID: 13381
			// (set) Token: 0x0600555D RID: 21853 RVA: 0x000863A2 File Offset: 0x000845A2
			public virtual ByteQuantifiedSize TransportSyncMailboxHealthLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxHealthLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003446 RID: 13382
			// (set) Token: 0x0600555E RID: 21854 RVA: 0x000863BA File Offset: 0x000845BA
			public virtual ByteQuantifiedSize TransportSyncMailboxHealthLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxHealthLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003447 RID: 13383
			// (set) Token: 0x0600555F RID: 21855 RVA: 0x000863D2 File Offset: 0x000845D2
			public virtual bool IsExcludedFromProvisioning
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromProvisioning"] = value;
				}
			}

			// Token: 0x17003448 RID: 13384
			// (set) Token: 0x06005560 RID: 21856 RVA: 0x000863EA File Offset: 0x000845EA
			public virtual int? MaximumActiveDatabases
			{
				set
				{
					base.PowerSharpParameters["MaximumActiveDatabases"] = value;
				}
			}

			// Token: 0x17003449 RID: 13385
			// (set) Token: 0x06005561 RID: 21857 RVA: 0x00086402 File Offset: 0x00084602
			public virtual int? MaximumPreferredActiveDatabases
			{
				set
				{
					base.PowerSharpParameters["MaximumPreferredActiveDatabases"] = value;
				}
			}

			// Token: 0x1700344A RID: 13386
			// (set) Token: 0x06005562 RID: 21858 RVA: 0x0008641A File Offset: 0x0008461A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700344B RID: 13387
			// (set) Token: 0x06005563 RID: 21859 RVA: 0x00086432 File Offset: 0x00084632
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700344C RID: 13388
			// (set) Token: 0x06005564 RID: 21860 RVA: 0x0008644A File Offset: 0x0008464A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700344D RID: 13389
			// (set) Token: 0x06005565 RID: 21861 RVA: 0x00086462 File Offset: 0x00084662
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700344E RID: 13390
			// (set) Token: 0x06005566 RID: 21862 RVA: 0x0008647A File Offset: 0x0008467A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
