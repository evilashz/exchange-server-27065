using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200049A RID: 1178
	[Serializable]
	public class LegacyDatabase : ADLegacyVersionableObject
	{
		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x0600356E RID: 13678 RVA: 0x000D2D51 File Offset: 0x000D0F51
		internal override ADObjectSchema Schema
		{
			get
			{
				return LegacyDatabase.schema;
			}
		}

		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x0600356F RID: 13679 RVA: 0x000D2D58 File Offset: 0x000D0F58
		internal override string MostDerivedObjectClass
		{
			get
			{
				return LegacyDatabase.mostDerivedClass;
			}
		}

		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x06003570 RID: 13680 RVA: 0x000D2D60 File Offset: 0x000D0F60
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new OrFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, MailboxDatabase.MostDerivedClass),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, PublicFolderDatabase.MostDerivedClass)
				});
			}
		}

		// Token: 0x06003571 RID: 13681 RVA: 0x000D2DA0 File Offset: 0x000D0FA0
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (!base.ExchangeVersion.IsOlderThan(ExchangeObjectVersion.Exchange2007))
			{
				AsciiCharactersOnlyConstraint asciiCharactersOnlyConstraint = new AsciiCharactersOnlyConstraint();
				PropertyConstraintViolationError propertyConstraintViolationError = asciiCharactersOnlyConstraint.Validate(this.Name, LegacyDatabaseSchema.Name, null);
				if (propertyConstraintViolationError != null)
				{
					errors.Add(propertyConstraintViolationError);
				}
				propertyConstraintViolationError = asciiCharactersOnlyConstraint.Validate(this.EdbFilePath, LegacyDatabaseSchema.EdbFilePath, null);
				if (propertyConstraintViolationError != null)
				{
					errors.Add(propertyConstraintViolationError);
				}
			}
			if (null != this.EdbFilePath && this.EdbFilePath.IsPathInRootDirectory)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorEdbFilePathInRoot(this.EdbFilePath.PathName), this.Identity, string.Empty));
			}
			if (null == this.CopyEdbFilePath == this.HasLocalCopy)
			{
				if (null == this.CopyEdbFilePath)
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.ErrorProperty1EqValue1WhileProperty2EqValue2(LegacyDatabaseSchema.CopyEdbFilePath.Name, "null", LegacyDatabaseSchema.HasLocalCopy.Name, this.HasLocalCopy.ToString()), this.Identity, string.Empty));
				}
				else
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.ErrorProperty1NeValue1WhileProperty2EqValue2(LegacyDatabaseSchema.CopyEdbFilePath.Name, "null", LegacyDatabaseSchema.HasLocalCopy.Name, this.HasLocalCopy.ToString()), this.Identity, string.Empty));
				}
			}
			if (this.HasLocalCopy && null != this.CopyEdbFilePath && null != this.EdbFilePath)
			{
				string text = Path.GetFileName(this.EdbFilePath.PathName);
				string text2 = Path.GetFileName(this.CopyEdbFilePath.PathName);
				if (!string.Equals(text, text2, StringComparison.OrdinalIgnoreCase))
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.ErrorProductFileNameDifferentFromCopyFileName(text, text2), this.Identity, string.Empty));
				}
				text = Path.GetDirectoryName(this.EdbFilePath.PathName);
				text2 = Path.GetDirectoryName(this.CopyEdbFilePath.PathName);
				if (string.Equals(text, text2, StringComparison.OrdinalIgnoreCase))
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.ErrorProductFileDirectoryIdenticalWithCopyFileDirectory(text), this.Identity, string.Empty));
				}
				if (this.CopyEdbFilePath.IsPathInRootDirectory)
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.ErrorEdbFilePathInRoot(this.CopyEdbFilePath.PathName), this.Identity, string.Empty));
				}
			}
			if (base.Id.DomainId != null && base.Id.Depth - base.Id.DomainId.Depth < 8)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorInvalidDNDepth, this.Identity, string.Empty));
			}
		}

		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x06003572 RID: 13682 RVA: 0x000D3030 File Offset: 0x000D1230
		public ADObjectId AdministrativeGroup
		{
			get
			{
				return (ADObjectId)this[LegacyDatabaseSchema.AdministrativeGroup];
			}
		}

		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x06003573 RID: 13683 RVA: 0x000D3042 File Offset: 0x000D1242
		// (set) Token: 0x06003574 RID: 13684 RVA: 0x000D3054 File Offset: 0x000D1254
		[Parameter(Mandatory = false)]
		public bool AllowFileRestore
		{
			get
			{
				return (bool)this[LegacyDatabaseSchema.AllowFileRestore];
			}
			set
			{
				this[LegacyDatabaseSchema.AllowFileRestore] = value;
			}
		}

		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x06003575 RID: 13685 RVA: 0x000D3067 File Offset: 0x000D1267
		// (set) Token: 0x06003576 RID: 13686 RVA: 0x000D306F File Offset: 0x000D126F
		public bool? BackupInProgress
		{
			get
			{
				return this.databaseBackupInProgress;
			}
			internal set
			{
				this.databaseBackupInProgress = value;
			}
		}

		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x06003577 RID: 13687 RVA: 0x000D3078 File Offset: 0x000D1278
		// (set) Token: 0x06003578 RID: 13688 RVA: 0x000D308A File Offset: 0x000D128A
		public EdbFilePath CopyEdbFilePath
		{
			get
			{
				return (EdbFilePath)this[LegacyDatabaseSchema.CopyEdbFilePath];
			}
			internal set
			{
				this[LegacyDatabaseSchema.CopyEdbFilePath] = value;
			}
		}

		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x06003579 RID: 13689 RVA: 0x000D3098 File Offset: 0x000D1298
		// (set) Token: 0x0600357A RID: 13690 RVA: 0x000D30AA File Offset: 0x000D12AA
		public bool DatabaseCreated
		{
			get
			{
				return (bool)this[LegacyDatabaseSchema.DatabaseCreated];
			}
			internal set
			{
				this[LegacyDatabaseSchema.DatabaseCreated] = value;
			}
		}

		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x0600357B RID: 13691 RVA: 0x000D30BD File Offset: 0x000D12BD
		internal DeliveryMechanisms DeliveryMechanism
		{
			get
			{
				return (DeliveryMechanisms)this[LegacyDatabaseSchema.DeliveryMechanism];
			}
		}

		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x0600357C RID: 13692 RVA: 0x000D30CF File Offset: 0x000D12CF
		public string Description
		{
			get
			{
				return (string)this[LegacyDatabaseSchema.Description];
			}
		}

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x0600357D RID: 13693 RVA: 0x000D30E1 File Offset: 0x000D12E1
		// (set) Token: 0x0600357E RID: 13694 RVA: 0x000D30F3 File Offset: 0x000D12F3
		public EdbFilePath EdbFilePath
		{
			get
			{
				return (EdbFilePath)this[LegacyDatabaseSchema.EdbFilePath];
			}
			internal set
			{
				this[LegacyDatabaseSchema.EdbFilePath] = value;
			}
		}

		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x0600357F RID: 13695 RVA: 0x000D3101 File Offset: 0x000D1301
		// (set) Token: 0x06003580 RID: 13696 RVA: 0x000D3113 File Offset: 0x000D1313
		public string ExchangeLegacyDN
		{
			get
			{
				return (string)this[LegacyDatabaseSchema.ExchangeLegacyDN];
			}
			internal set
			{
				this[LegacyDatabaseSchema.ExchangeLegacyDN] = value;
			}
		}

		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x06003581 RID: 13697 RVA: 0x000D3121 File Offset: 0x000D1321
		// (set) Token: 0x06003582 RID: 13698 RVA: 0x000D3133 File Offset: 0x000D1333
		internal bool FixedFont
		{
			get
			{
				return (bool)this[LegacyDatabaseSchema.FixedFont];
			}
			set
			{
				this[LegacyDatabaseSchema.FixedFont] = value;
			}
		}

		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x06003583 RID: 13699 RVA: 0x000D3146 File Offset: 0x000D1346
		// (set) Token: 0x06003584 RID: 13700 RVA: 0x000D3158 File Offset: 0x000D1358
		public bool HasLocalCopy
		{
			get
			{
				return (bool)this[LegacyDatabaseSchema.HasLocalCopy];
			}
			internal set
			{
				this[LegacyDatabaseSchema.HasLocalCopy] = value;
			}
		}

		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x06003585 RID: 13701 RVA: 0x000D316B File Offset: 0x000D136B
		// (set) Token: 0x06003586 RID: 13702 RVA: 0x000D317D File Offset: 0x000D137D
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan DeletedItemRetention
		{
			get
			{
				return (EnhancedTimeSpan)this[LegacyDatabaseSchema.DeletedItemRetention];
			}
			set
			{
				this[LegacyDatabaseSchema.DeletedItemRetention] = value;
			}
		}

		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x06003587 RID: 13703 RVA: 0x000D3190 File Offset: 0x000D1390
		// (set) Token: 0x06003588 RID: 13704 RVA: 0x000D3198 File Offset: 0x000D1398
		public bool? SnapshotLastFullBackup
		{
			get
			{
				return this.snapshotLastFullBackup;
			}
			internal set
			{
				this.snapshotLastFullBackup = value;
			}
		}

		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x06003589 RID: 13705 RVA: 0x000D31A1 File Offset: 0x000D13A1
		// (set) Token: 0x0600358A RID: 13706 RVA: 0x000D31A9 File Offset: 0x000D13A9
		public bool? SnapshotLastIncrementalBackup
		{
			get
			{
				return this.snapshotLastIncrementalBackup;
			}
			internal set
			{
				this.snapshotLastIncrementalBackup = value;
			}
		}

		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x0600358B RID: 13707 RVA: 0x000D31B2 File Offset: 0x000D13B2
		// (set) Token: 0x0600358C RID: 13708 RVA: 0x000D31BA File Offset: 0x000D13BA
		public bool? SnapshotLastDifferentialBackup
		{
			get
			{
				return this.snapshotLastDifferentialBackup;
			}
			internal set
			{
				this.snapshotLastDifferentialBackup = value;
			}
		}

		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x0600358D RID: 13709 RVA: 0x000D31C3 File Offset: 0x000D13C3
		// (set) Token: 0x0600358E RID: 13710 RVA: 0x000D31CB File Offset: 0x000D13CB
		public bool? SnapshotLastCopyBackup
		{
			get
			{
				return this.snapshotLastCopyBackup;
			}
			internal set
			{
				this.snapshotLastCopyBackup = value;
			}
		}

		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x0600358F RID: 13711 RVA: 0x000D31D4 File Offset: 0x000D13D4
		// (set) Token: 0x06003590 RID: 13712 RVA: 0x000D31DC File Offset: 0x000D13DC
		public DateTime? LastFullBackup
		{
			get
			{
				return this.databaseLastFullBackup;
			}
			internal set
			{
				this.databaseLastFullBackup = value;
			}
		}

		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x06003591 RID: 13713 RVA: 0x000D31E5 File Offset: 0x000D13E5
		// (set) Token: 0x06003592 RID: 13714 RVA: 0x000D31ED File Offset: 0x000D13ED
		public DateTime? LastIncrementalBackup
		{
			get
			{
				return this.databaseLastIncrementalBackup;
			}
			internal set
			{
				this.databaseLastIncrementalBackup = value;
			}
		}

		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x06003593 RID: 13715 RVA: 0x000D31F6 File Offset: 0x000D13F6
		// (set) Token: 0x06003594 RID: 13716 RVA: 0x000D31FE File Offset: 0x000D13FE
		public DateTime? LastDifferentialBackup
		{
			get
			{
				return this.databaseLastDifferentialBackup;
			}
			internal set
			{
				this.databaseLastDifferentialBackup = value;
			}
		}

		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x06003595 RID: 13717 RVA: 0x000D3207 File Offset: 0x000D1407
		// (set) Token: 0x06003596 RID: 13718 RVA: 0x000D320F File Offset: 0x000D140F
		public DateTime? LastCopyBackup
		{
			get
			{
				return this.databaseLastCopyBackup;
			}
			internal set
			{
				this.databaseLastCopyBackup = value;
			}
		}

		// Token: 0x1700102F RID: 4143
		// (get) Token: 0x06003597 RID: 13719 RVA: 0x000D3218 File Offset: 0x000D1418
		// (set) Token: 0x06003598 RID: 13720 RVA: 0x000D322A File Offset: 0x000D142A
		[Parameter(Mandatory = false)]
		public Schedule MaintenanceSchedule
		{
			get
			{
				return (Schedule)this[LegacyDatabaseSchema.MaintenanceSchedule];
			}
			set
			{
				this[LegacyDatabaseSchema.MaintenanceSchedule] = value;
			}
		}

		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x06003599 RID: 13721 RVA: 0x000D3238 File Offset: 0x000D1438
		internal ScheduleMode MaintenanceScheduleMode
		{
			get
			{
				return (ScheduleMode)this[LegacyDatabaseSchema.MaintenanceScheduleMode];
			}
		}

		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x0600359A RID: 13722 RVA: 0x000D324A File Offset: 0x000D144A
		internal int MaxCachedViews
		{
			get
			{
				return (int)this[LegacyDatabaseSchema.MaxCachedViews];
			}
		}

		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x0600359B RID: 13723 RVA: 0x000D325C File Offset: 0x000D145C
		// (set) Token: 0x0600359C RID: 13724 RVA: 0x000D326E File Offset: 0x000D146E
		[Parameter(Mandatory = false)]
		public bool MountAtStartup
		{
			get
			{
				return (bool)this[LegacyDatabaseSchema.MountAtStartup];
			}
			set
			{
				this[LegacyDatabaseSchema.MountAtStartup] = value;
			}
		}

		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x0600359D RID: 13725 RVA: 0x000D3281 File Offset: 0x000D1481
		// (set) Token: 0x0600359E RID: 13726 RVA: 0x000D3289 File Offset: 0x000D1489
		public bool? Mounted
		{
			get
			{
				return this.databaseMounted;
			}
			internal set
			{
				this.databaseMounted = value;
			}
		}

		// Token: 0x17001034 RID: 4148
		// (get) Token: 0x0600359F RID: 13727 RVA: 0x000D3292 File Offset: 0x000D1492
		// (set) Token: 0x060035A0 RID: 13728 RVA: 0x000D329A File Offset: 0x000D149A
		internal bool? OnlineMaintenanceInProgress
		{
			get
			{
				return this.databaseOnlineMaintenanceInProgress;
			}
			set
			{
				this.databaseOnlineMaintenanceInProgress = value;
			}
		}

		// Token: 0x17001035 RID: 4149
		// (get) Token: 0x060035A1 RID: 13729 RVA: 0x000D32A3 File Offset: 0x000D14A3
		public ADObjectId Organization
		{
			get
			{
				return (ADObjectId)this[LegacyDatabaseSchema.Organization];
			}
		}

		// Token: 0x17001036 RID: 4150
		// (get) Token: 0x060035A2 RID: 13730 RVA: 0x000D32B5 File Offset: 0x000D14B5
		internal ScheduleMode QuotaNotificationMode
		{
			get
			{
				return (ScheduleMode)this[LegacyDatabaseSchema.QuotaNotificationMode];
			}
		}

		// Token: 0x17001037 RID: 4151
		// (get) Token: 0x060035A3 RID: 13731 RVA: 0x000D32C7 File Offset: 0x000D14C7
		// (set) Token: 0x060035A4 RID: 13732 RVA: 0x000D32D9 File Offset: 0x000D14D9
		[Parameter(Mandatory = false)]
		public Schedule QuotaNotificationSchedule
		{
			get
			{
				return (Schedule)this[LegacyDatabaseSchema.QuotaNotificationSchedule];
			}
			set
			{
				this[LegacyDatabaseSchema.QuotaNotificationSchedule] = value;
			}
		}

		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x060035A5 RID: 13733 RVA: 0x000D32E7 File Offset: 0x000D14E7
		// (set) Token: 0x060035A6 RID: 13734 RVA: 0x000D32F9 File Offset: 0x000D14F9
		internal bool RestoreInProgress
		{
			get
			{
				return (bool)this[LegacyDatabaseSchema.RestoreInProgress];
			}
			set
			{
				this[LegacyDatabaseSchema.RestoreInProgress] = value;
			}
		}

		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x060035A7 RID: 13735 RVA: 0x000D330C File Offset: 0x000D150C
		// (set) Token: 0x060035A8 RID: 13736 RVA: 0x000D331E File Offset: 0x000D151E
		[Parameter(Mandatory = false)]
		public bool RetainDeletedItemsUntilBackup
		{
			get
			{
				return (bool)this[LegacyDatabaseSchema.RetainDeletedItemsUntilBackup];
			}
			set
			{
				this[LegacyDatabaseSchema.RetainDeletedItemsUntilBackup] = value;
			}
		}

		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x060035A9 RID: 13737 RVA: 0x000D3331 File Offset: 0x000D1531
		// (set) Token: 0x060035AA RID: 13738 RVA: 0x000D3343 File Offset: 0x000D1543
		public ADObjectId Server
		{
			get
			{
				return (ADObjectId)this[LegacyDatabaseSchema.Server];
			}
			internal set
			{
				this[LegacyDatabaseSchema.Server] = value;
			}
		}

		// Token: 0x1700103B RID: 4155
		// (get) Token: 0x060035AB RID: 13739 RVA: 0x000D3351 File Offset: 0x000D1551
		public string ServerName
		{
			get
			{
				return (string)this[LegacyDatabaseSchema.ServerName];
			}
		}

		// Token: 0x1700103C RID: 4156
		// (get) Token: 0x060035AC RID: 13740 RVA: 0x000D3363 File Offset: 0x000D1563
		// (set) Token: 0x060035AD RID: 13741 RVA: 0x000D3375 File Offset: 0x000D1575
		internal bool SMimeSignatureEnabled
		{
			get
			{
				return (bool)this[LegacyDatabaseSchema.SMimeSignatureEnabled];
			}
			set
			{
				this[LegacyDatabaseSchema.SMimeSignatureEnabled] = value;
			}
		}

		// Token: 0x1700103D RID: 4157
		// (get) Token: 0x060035AE RID: 13742 RVA: 0x000D3388 File Offset: 0x000D1588
		public ADObjectId StorageGroup
		{
			get
			{
				return (ADObjectId)this[LegacyDatabaseSchema.StorageGroup];
			}
		}

		// Token: 0x1700103E RID: 4158
		// (get) Token: 0x060035AF RID: 13743 RVA: 0x000D339A File Offset: 0x000D159A
		public string StorageGroupName
		{
			get
			{
				return (string)this[LegacyDatabaseSchema.StorageGroupName];
			}
		}

		// Token: 0x1700103F RID: 4159
		// (get) Token: 0x060035B0 RID: 13744 RVA: 0x000D33AC File Offset: 0x000D15AC
		// (set) Token: 0x060035B1 RID: 13745 RVA: 0x000D33BE File Offset: 0x000D15BE
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> IssueWarningQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[LegacyDatabaseSchema.IssueWarningQuota];
			}
			set
			{
				this[LegacyDatabaseSchema.IssueWarningQuota] = value;
			}
		}

		// Token: 0x17001040 RID: 4160
		// (get) Token: 0x060035B2 RID: 13746 RVA: 0x000D33D1 File Offset: 0x000D15D1
		// (set) Token: 0x060035B3 RID: 13747 RVA: 0x000D33E3 File Offset: 0x000D15E3
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan EventHistoryRetentionPeriod
		{
			get
			{
				return (EnhancedTimeSpan)this[LegacyDatabaseSchema.EventHistoryRetentionPeriod];
			}
			set
			{
				this[LegacyDatabaseSchema.EventHistoryRetentionPeriod] = value;
			}
		}

		// Token: 0x17001041 RID: 4161
		// (get) Token: 0x060035B4 RID: 13748 RVA: 0x000D33F6 File Offset: 0x000D15F6
		// (set) Token: 0x060035B5 RID: 13749 RVA: 0x000D3408 File Offset: 0x000D1608
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public new string Name
		{
			get
			{
				return (string)this[LegacyDatabaseSchema.Name];
			}
			set
			{
				this[LegacyDatabaseSchema.Name] = value;
				this[ADConfigurationObjectSchema.AdminDisplayName] = value;
				this[LegacyDatabaseSchema.DisplayName] = value;
			}
		}

		// Token: 0x060035B6 RID: 13750 RVA: 0x000D342E File Offset: 0x000D162E
		internal Server GetServer()
		{
			return base.Session.Read<Server>(this.Server);
		}

		// Token: 0x060035B7 RID: 13751 RVA: 0x000D3441 File Offset: 0x000D1641
		internal StorageGroup GetStorageGroup()
		{
			return base.Session.Read<StorageGroup>(this.StorageGroup);
		}

		// Token: 0x060035B8 RID: 13752 RVA: 0x000D3454 File Offset: 0x000D1654
		internal override void StampPersistableDefaultValues()
		{
			if (!base.IsModified(LegacyDatabaseSchema.MaintenanceSchedule))
			{
				this.MaintenanceSchedule = Schedule.DailyFrom1AMTo5AM;
			}
			if (!base.IsModified(LegacyDatabaseSchema.QuotaNotificationSchedule))
			{
				this.QuotaNotificationSchedule = Schedule.Daily1AM;
			}
			if (!base.IsModified(LegacyDatabaseSchema.IssueWarningQuota))
			{
				this.IssueWarningQuota = new Unlimited<ByteQuantifiedSize>(ByteQuantifiedSize.FromMB(1945UL));
			}
			base.StampPersistableDefaultValues();
		}

		// Token: 0x060035B9 RID: 13753 RVA: 0x000D34BC File Offset: 0x000D16BC
		internal static ValidationError[] ValidateAscendingQuotas(PropertyBag propertyBag, ProviderPropertyDefinition[] ascendingQuotaDefinitions, ObjectId identity)
		{
			List<ValidationError> list = new List<ValidationError>(ascendingQuotaDefinitions.Length);
			ProviderPropertyDefinition providerPropertyDefinition = null;
			Unlimited<ByteQuantifiedSize> unlimited = default(Unlimited<ByteQuantifiedSize>);
			int num = 0;
			while (ascendingQuotaDefinitions.Length > num)
			{
				ProviderPropertyDefinition providerPropertyDefinition2 = ascendingQuotaDefinitions[num];
				Unlimited<ByteQuantifiedSize> unlimited2 = (Unlimited<ByteQuantifiedSize>)propertyBag[providerPropertyDefinition2];
				if (!unlimited2.IsUnlimited)
				{
					if (providerPropertyDefinition != null && 0 < unlimited.CompareTo(unlimited2))
					{
						if (propertyBag.IsChanged(providerPropertyDefinition))
						{
							list.Add(new ObjectValidationError(DirectoryStrings.ErrorProperty1GtProperty2(providerPropertyDefinition.Name, unlimited.ToString(), providerPropertyDefinition2.Name, unlimited2.ToString()), identity, string.Empty));
						}
						else
						{
							list.Add(new ObjectValidationError(DirectoryStrings.ErrorProperty1LtProperty2(providerPropertyDefinition2.Name, unlimited2.ToString(), providerPropertyDefinition.Name, unlimited.ToString()), identity, string.Empty));
						}
					}
					providerPropertyDefinition = providerPropertyDefinition2;
					unlimited = unlimited2;
				}
				num++;
			}
			return list.ToArray();
		}

		// Token: 0x060035BA RID: 13754 RVA: 0x000D35B8 File Offset: 0x000D17B8
		internal static void InternalAssertComparisonFilter(SinglePropertyFilter filter, PropertyDefinition propertyDefinition)
		{
			string name = propertyDefinition.Name;
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			if (comparisonFilter == null)
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(name, filter.GetType(), typeof(ComparisonFilter)));
			}
			object propertyValue = comparisonFilter.PropertyValue;
			if (propertyValue == null)
			{
				throw new ArgumentNullException("filter.PropertyValue");
			}
			Type type = propertyValue.GetType();
			if (type != propertyDefinition.Type)
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedPropertyValueType(name, type, propertyDefinition.Type));
			}
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x000D3630 File Offset: 0x000D1830
		internal static object AdministrativeGroupGetter(IPropertyBag propertyBag)
		{
			object result;
			try
			{
				result = ((ADObjectId)propertyBag[ADObjectSchema.Id]).AncestorDN(2);
			}
			catch (NullReferenceException ex)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("AdministrativeGroup", ex.Message), LegacyDatabaseSchema.AdministrativeGroup, propertyBag[ADObjectSchema.Id]), ex);
			}
			catch (InvalidOperationException ex2)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("AdministrativeGroup", ex2.Message), LegacyDatabaseSchema.AdministrativeGroup, propertyBag[ADObjectSchema.Id]), ex2);
			}
			return result;
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x000D36D0 File Offset: 0x000D18D0
		internal static QueryFilter HasLocalCopyFilterBuilder(SinglePropertyFilter filter)
		{
			LegacyDatabase.InternalAssertComparisonFilter(filter, LegacyDatabaseSchema.HasLocalCopy);
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			ComparisonOperator comparisonOperator = comparisonFilter.ComparisonOperator;
			if (comparisonOperator != ComparisonOperator.Equal && ComparisonOperator.NotEqual != comparisonOperator)
			{
				throw new ADFilterException(DirectoryStrings.ExceptionInvalidBitwiseComparison(LegacyDatabaseSchema.HasLocalCopy.Name, comparisonOperator.ToString()));
			}
			bool flag = (bool)comparisonFilter.PropertyValue;
			bool flag2 = ComparisonOperator.Equal == comparisonOperator;
			QueryFilter queryFilter = new BitMaskAndFilter(LegacyDatabaseSchema.HasLocalCopyValue, 1UL);
			if (flag ^ flag2)
			{
				queryFilter = new NotFilter(queryFilter);
			}
			return queryFilter;
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x000D374C File Offset: 0x000D194C
		internal static object HasLocalCopyGetter(IPropertyBag propertyBag)
		{
			object obj = propertyBag[LegacyDatabaseSchema.HasLocalCopyValue];
			if (obj != null)
			{
				return 0 != (1 & (int)obj);
			}
			return null;
		}

		// Token: 0x060035BE RID: 13758 RVA: 0x000D3780 File Offset: 0x000D1980
		internal static void HasLocalCopySetter(object value, IPropertyBag propertyBag)
		{
			object obj = propertyBag[LegacyDatabaseSchema.HasLocalCopyValue];
			int num = (obj == null) ? ((int)LegacyDatabaseSchema.HasLocalCopyValue.DefaultValue) : ((int)propertyBag[LegacyDatabaseSchema.HasLocalCopyValue]);
			propertyBag[LegacyDatabaseSchema.HasLocalCopyValue] = (((bool)value) ? (1 | num) : (-2 & num));
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x000D37DF File Offset: 0x000D19DF
		internal static object MaintenanceScheduleGetter(IPropertyBag propertyBag)
		{
			return propertyBag[LegacyDatabaseSchema.MaintenanceScheduleBitmaps];
		}

		// Token: 0x060035C0 RID: 13760 RVA: 0x000D37EC File Offset: 0x000D19EC
		internal static void MaintenanceScheduleSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[LegacyDatabaseSchema.MaintenanceScheduleBitmaps] = value;
			if (value == null)
			{
				propertyBag[LegacyDatabaseSchema.MaintenanceScheduleMode] = ScheduleMode.Never;
				return;
			}
			propertyBag[LegacyDatabaseSchema.MaintenanceScheduleMode] = ((Schedule)value).Mode;
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x000D382C File Offset: 0x000D1A2C
		internal static QueryFilter MountAtStartupFilterBuilder(SinglePropertyFilter filter)
		{
			LegacyDatabase.InternalAssertComparisonFilter(filter, LegacyDatabaseSchema.MountAtStartup);
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			return new ComparisonFilter(comparisonFilter.ComparisonOperator, LegacyDatabaseSchema.EdbOfflineAtStartup, !(bool)comparisonFilter.PropertyValue);
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x000D3870 File Offset: 0x000D1A70
		internal static object OrganizationGetter(IPropertyBag propertyBag)
		{
			object result;
			try
			{
				result = ((ADObjectId)propertyBag[ADObjectSchema.Id]).AncestorDN(4);
			}
			catch (NullReferenceException ex)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("Organization", ex.Message), LegacyDatabaseSchema.Organization, propertyBag[ADObjectSchema.Id]), ex);
			}
			catch (InvalidOperationException ex2)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("Organization", ex2.Message), LegacyDatabaseSchema.Organization, propertyBag[ADObjectSchema.Id]), ex2);
			}
			return result;
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x000D3910 File Offset: 0x000D1B10
		internal static object QuotaNotificationScheduleGetter(IPropertyBag propertyBag)
		{
			return propertyBag[LegacyDatabaseSchema.QuotaNotificationScheduleBitmaps];
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x000D391D File Offset: 0x000D1B1D
		internal static void QuotaNotificationScheduleSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[LegacyDatabaseSchema.QuotaNotificationScheduleBitmaps] = value;
			if (value == null)
			{
				propertyBag[LegacyDatabaseSchema.QuotaNotificationMode] = ScheduleMode.Never;
				return;
			}
			propertyBag[LegacyDatabaseSchema.QuotaNotificationMode] = ((Schedule)value).Mode;
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x000D395C File Offset: 0x000D1B5C
		internal static QueryFilter RetainDeletedItemsUntilBackupFilterBuilder(SinglePropertyFilter filter)
		{
			LegacyDatabase.InternalAssertComparisonFilter(filter, LegacyDatabaseSchema.RetainDeletedItemsUntilBackup);
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			return new ComparisonFilter(comparisonFilter.ComparisonOperator, LegacyDatabaseSchema.DelItemAfterBackupEnum, ((bool)comparisonFilter.PropertyValue) ? Microsoft.Exchange.Data.Directory.Recipient.DeletedItemRetention.RetainUntilBackupOrCustomPeriod : Microsoft.Exchange.Data.Directory.Recipient.DeletedItemRetention.RetainForCustomPeriod);
		}

		// Token: 0x060035C6 RID: 13766 RVA: 0x000D39A1 File Offset: 0x000D1BA1
		internal static object RetainDeletedItemsUntilBackupGetter(IPropertyBag propertyBag)
		{
			return Microsoft.Exchange.Data.Directory.Recipient.DeletedItemRetention.RetainUntilBackupOrCustomPeriod == (DeletedItemRetention)propertyBag[LegacyDatabaseSchema.DelItemAfterBackupEnum];
		}

		// Token: 0x060035C7 RID: 13767 RVA: 0x000D39BB File Offset: 0x000D1BBB
		internal static void RetainDeletedItemsUntilBackupSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[LegacyDatabaseSchema.DelItemAfterBackupEnum] = (((bool)value) ? Microsoft.Exchange.Data.Directory.Recipient.DeletedItemRetention.RetainUntilBackupOrCustomPeriod : Microsoft.Exchange.Data.Directory.Recipient.DeletedItemRetention.RetainForCustomPeriod);
		}

		// Token: 0x060035C8 RID: 13768 RVA: 0x000D39DC File Offset: 0x000D1BDC
		internal static object ServerNameGetter(IPropertyBag propertyBag)
		{
			ADObjectId adobjectId = (ADObjectId)propertyBag[LegacyDatabaseSchema.Server];
			if (adobjectId == null)
			{
				return LegacyDatabaseSchema.ServerName.DefaultValue;
			}
			return adobjectId.Name;
		}

		// Token: 0x060035C9 RID: 13769 RVA: 0x000D3A10 File Offset: 0x000D1C10
		internal static object StorageGroupGetter(IPropertyBag propertyBag)
		{
			ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.Id];
			if (adobjectId == null)
			{
				return LegacyDatabaseSchema.StorageGroup.DefaultValue;
			}
			return adobjectId.Parent;
		}

		// Token: 0x060035CA RID: 13770 RVA: 0x000D3A44 File Offset: 0x000D1C44
		internal static object StorageGroupNameGetter(IPropertyBag propertyBag)
		{
			ADObjectId adobjectId = (ADObjectId)LegacyDatabase.StorageGroupGetter(propertyBag);
			if (adobjectId == null)
			{
				return LegacyDatabaseSchema.StorageGroupName.DefaultValue;
			}
			return adobjectId.Name;
		}

		// Token: 0x060035CB RID: 13771 RVA: 0x000D3A71 File Offset: 0x000D1C71
		internal static object DatabaseNameGetter(IPropertyBag propertyBag)
		{
			return propertyBag[ADObjectSchema.RawName];
		}

		// Token: 0x060035CC RID: 13772 RVA: 0x000D3A7E File Offset: 0x000D1C7E
		internal static void DatabaseNameSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ADObjectSchema.RawName] = value;
		}

		// Token: 0x04002442 RID: 9282
		private static LegacyDatabaseSchema schema = ObjectSchema.GetInstance<LegacyDatabaseSchema>();

		// Token: 0x04002443 RID: 9283
		private static string mostDerivedClass = "msExchMDB";

		// Token: 0x04002444 RID: 9284
		private bool? databaseMounted;

		// Token: 0x04002445 RID: 9285
		private bool? databaseOnlineMaintenanceInProgress;

		// Token: 0x04002446 RID: 9286
		private bool? databaseBackupInProgress;

		// Token: 0x04002447 RID: 9287
		private bool? snapshotLastFullBackup;

		// Token: 0x04002448 RID: 9288
		private bool? snapshotLastIncrementalBackup;

		// Token: 0x04002449 RID: 9289
		private bool? snapshotLastDifferentialBackup;

		// Token: 0x0400244A RID: 9290
		private bool? snapshotLastCopyBackup;

		// Token: 0x0400244B RID: 9291
		private DateTime? databaseLastFullBackup;

		// Token: 0x0400244C RID: 9292
		private DateTime? databaseLastIncrementalBackup;

		// Token: 0x0400244D RID: 9293
		private DateTime? databaseLastDifferentialBackup;

		// Token: 0x0400244E RID: 9294
		private DateTime? databaseLastCopyBackup;
	}
}
