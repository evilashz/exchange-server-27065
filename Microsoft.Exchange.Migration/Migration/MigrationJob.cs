using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Management.Migration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Migration.DataAccessLayer;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200005B RID: 91
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationJob : MigrationMessagePersistableBase
	{
		// Token: 0x0600043E RID: 1086 RVA: 0x00010418 File Offset: 0x0000E618
		private MigrationJob()
		{
			this.currentSupportedVersion = 4L;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0001042F File Offset: 0x0000E62F
		private MigrationJob(MigrationType migrationType)
		{
			this.Initialize(migrationType);
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x00010445 File Offset: 0x0000E645
		public MigrationType MigrationType
		{
			get
			{
				return this.migrationJobType;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0001044D File Offset: 0x0000E64D
		// (set) Token: 0x06000442 RID: 1090 RVA: 0x00010455 File Offset: 0x0000E655
		public Guid JobId
		{
			get
			{
				return this.jobId;
			}
			private set
			{
				this.jobId = value;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x0001045E File Offset: 0x0000E65E
		// (set) Token: 0x06000444 RID: 1092 RVA: 0x00010466 File Offset: 0x0000E666
		public string JobName
		{
			get
			{
				return this.jobName;
			}
			private set
			{
				this.jobName = value;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x0001046F File Offset: 0x0000E66F
		public MigrationJobStatus Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x00010477 File Offset: 0x0000E677
		public MigrationState State
		{
			get
			{
				return this.StatusData.State;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x00010484 File Offset: 0x0000E684
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x0001048C File Offset: 0x0000E68C
		public MigrationStatusData<MigrationJobStatus> StatusData
		{
			get
			{
				return this.statusData;
			}
			protected set
			{
				this.statusData = value;
				if (this.statusData != null)
				{
					this.status = this.statusData.Status;
				}
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x000104AE File Offset: 0x0000E6AE
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x000104B6 File Offset: 0x0000E6B6
		public ExDateTime? LastScheduled
		{
			get
			{
				return this.lastScheduled;
			}
			private set
			{
				this.lastScheduled = value;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x000104BF File Offset: 0x0000E6BF
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x000104C7 File Offset: 0x0000E6C7
		public ExDateTime? NextProcessTime
		{
			get
			{
				return this.nextProcessTime;
			}
			private set
			{
				this.nextProcessTime = value;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x000104D0 File Offset: 0x0000E6D0
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x000104D8 File Offset: 0x0000E6D8
		public ExDateTime OriginalCreationTime
		{
			get
			{
				return this.originalCreationTime;
			}
			private set
			{
				this.originalCreationTime = value;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x000104E1 File Offset: 0x0000E6E1
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x000104E9 File Offset: 0x0000E6E9
		public ExDateTime? StartTime
		{
			get
			{
				return this.startTime;
			}
			private set
			{
				this.startTime = value;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x000104F2 File Offset: 0x0000E6F2
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x000104FA File Offset: 0x0000E6FA
		public ExDateTime? LastRestartTime { get; private set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00010503 File Offset: 0x0000E703
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x0001050B File Offset: 0x0000E70B
		public ExDateTime? FinalizeTime
		{
			get
			{
				return this.finalizeTime;
			}
			private set
			{
				this.finalizeTime = value;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00010514 File Offset: 0x0000E714
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x0001051C File Offset: 0x0000E71C
		public int LastFinalizationAttempt
		{
			get
			{
				return this.lastFinalizationAttempt;
			}
			private set
			{
				this.lastFinalizationAttempt = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00010525 File Offset: 0x0000E725
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x0001052D File Offset: 0x0000E72D
		public ExTimeZone UserTimeZone
		{
			get
			{
				return this.userTimeZone;
			}
			private set
			{
				this.userTimeZone = value;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x00010536 File Offset: 0x0000E736
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x0001053E File Offset: 0x0000E73E
		public string SubmittedByUser
		{
			get
			{
				return this.submittedByUser;
			}
			private set
			{
				this.submittedByUser = value;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00010548 File Offset: 0x0000E748
		public int TotalCount
		{
			get
			{
				int num = this.TotalRowCount;
				int validationWarningCount = this.ValidationWarningCount;
				int removedItemCount = this.RemovedItemCount;
				if (num < validationWarningCount + removedItemCount)
				{
					return 0;
				}
				return num - (validationWarningCount + removedItemCount);
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00010578 File Offset: 0x0000E778
		public int PendingCount
		{
			get
			{
				if (string.IsNullOrEmpty(this.BatchInputId))
				{
					return 0;
				}
				int num = this.TotalRowCount;
				int validationWarningCount = this.ValidationWarningCount;
				int totalItemCount = this.TotalItemCount;
				if (num < totalItemCount + validationWarningCount)
				{
					return 0;
				}
				return num - (totalItemCount + validationWarningCount);
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x000105B6 File Offset: 0x0000E7B6
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x000105BE File Offset: 0x0000E7BE
		public int TotalRowCount
		{
			get
			{
				return this.totalRowCount;
			}
			private set
			{
				this.totalRowCount = value;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x000105C7 File Offset: 0x0000E7C7
		public int TotalItemCount
		{
			get
			{
				return this.cachedItemCounts.GetCachedStatusCount(MigrationJob.AllJobItemsStatuses);
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x000105D9 File Offset: 0x0000E7D9
		public int SyncedItemCount
		{
			get
			{
				return this.cachedItemCounts.GetCachedStatusCount(MigrationJob.JobItemsStatusForSynced);
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x000105EB File Offset: 0x0000E7EB
		public int FinalizedItemCount
		{
			get
			{
				return this.cachedItemCounts.GetCachedStatusCount(MigrationJob.JobItemsStatusForFinalized);
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x000105FD File Offset: 0x0000E7FD
		public int ActiveItemCount
		{
			get
			{
				return this.cachedItemCounts.GetCachedStatusCount(MigrationJob.JobItemsStatusForActive);
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0001060F File Offset: 0x0000E80F
		public int ActiveInitialItemCount
		{
			get
			{
				return this.cachedItemCounts.GetCachedStatusCount(MigrationJob.JobItemsStatusForActiveInitial);
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x00010624 File Offset: 0x0000E824
		public int StartingItemCount
		{
			get
			{
				return this.cachedItemCounts.GetCachedStatusCount(new MigrationUserStatus[]
				{
					MigrationUserStatus.Starting
				});
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00010649 File Offset: 0x0000E849
		public int StoppedItemCount
		{
			get
			{
				return this.cachedItemCounts.GetCachedStatusCount(MigrationJob.JobItemsStatusForStopped);
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0001065B File Offset: 0x0000E85B
		public int FailedInitialItemCount
		{
			get
			{
				return this.cachedItemCounts.GetCachedStatusCount(MigrationJob.JobItemsStatusForFailedInitial);
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0001066D File Offset: 0x0000E86D
		public int FailedIncrementalItemCount
		{
			get
			{
				return this.cachedItemCounts.GetCachedStatusCount(MigrationJob.JobItemsStatusForFailedIncremental);
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0001067F File Offset: 0x0000E87F
		public int FailedOtherItemCount
		{
			get
			{
				return this.cachedItemCounts.GetCachedStatusCount(MigrationJob.JobItemsStatusForFailedOther);
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00010691 File Offset: 0x0000E891
		public int FailedFinalizationItemCount
		{
			get
			{
				return this.cachedItemCounts.GetCachedStatusCount(MigrationJob.JobItemsStatusForFailedFinalization);
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x000106A3 File Offset: 0x0000E8A3
		public int FailedItemCount
		{
			get
			{
				return this.FailedInitialItemCount + this.FailedIncrementalItemCount + this.FailedOtherItemCount + this.FailedFinalizationItemCount;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x000106C0 File Offset: 0x0000E8C0
		public int ProvisionedItemCount
		{
			get
			{
				return this.cachedItemCounts.GetCachedOtherCount("Provisioned");
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x000106D2 File Offset: 0x0000E8D2
		public int RemovedItemCount
		{
			get
			{
				return this.cachedItemCounts.GetCachedOtherCount("Removed");
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x000106E4 File Offset: 0x0000E8E4
		public int ReportSyncCompleteFailedItemCount
		{
			get
			{
				return this.FailedItemCount;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x000106EC File Offset: 0x0000E8EC
		public int ReportCompleteFailedItemCount
		{
			get
			{
				return this.FailedIncrementalItemCount + this.FailedFinalizationItemCount;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x000106FB File Offset: 0x0000E8FB
		public bool ShouldAutoRetryStartedJob
		{
			get
			{
				return this.ShouldAutoRetryJob(this.ReportSyncCompleteFailedItemCount);
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x00010709 File Offset: 0x0000E909
		public bool ShouldAutoRetryCompletedJob
		{
			get
			{
				return this.ShouldAutoRetryJob(this.ReportCompleteFailedItemCount);
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x00010717 File Offset: 0x0000E917
		public ExDateTime? LastSyncTime
		{
			get
			{
				return this.cachedItemCounts.GetCachedTimestamp("LastSync");
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x00010729 File Offset: 0x0000E929
		public bool HasCachedCounts
		{
			get
			{
				return !this.cachedItemCounts.IsEmpty;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00010739 File Offset: 0x0000E939
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x00010741 File Offset: 0x0000E941
		public ExDateTime? FullScanTime
		{
			get
			{
				return this.fullScanTime;
			}
			private set
			{
				this.fullScanTime = value;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0001074C File Offset: 0x0000E94C
		public bool ShouldLazyRescan
		{
			get
			{
				TimeSpan config = ConfigBase<MigrationServiceConfigSchema>.GetConfig<TimeSpan>("SyncMigrationLazyCountRescanPollingTimeout");
				return !(config < TimeSpan.Zero) && (this.fullScanTime == null || ExDateTime.UtcNow > this.fullScanTime.Value + config);
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0001079D File Offset: 0x0000E99D
		// (set) Token: 0x06000477 RID: 1143 RVA: 0x000107A5 File Offset: 0x0000E9A5
		public MultiValuedProperty<SmtpAddress> NotificationEmails
		{
			get
			{
				return this.notificationEmails;
			}
			private set
			{
				this.notificationEmails = value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x000107AE File Offset: 0x0000E9AE
		public bool IsCancelled
		{
			get
			{
				return this.JobCancellationStatus != JobCancellationStatus.NotCancelled;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x000107BC File Offset: 0x0000E9BC
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x000107C4 File Offset: 0x0000E9C4
		public JobCancellationStatus JobCancellationStatus
		{
			get
			{
				return this.jobCancellationStatus;
			}
			private set
			{
				this.jobCancellationStatus = value;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x000107CD File Offset: 0x0000E9CD
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x000107D5 File Offset: 0x0000E9D5
		public string LastCursorPosition
		{
			get
			{
				return this.lastCursorPosition;
			}
			private set
			{
				this.lastCursorPosition = value;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x000107E0 File Offset: 0x0000E9E0
		public ExDateTime? StateLastUpdated
		{
			get
			{
				if (this.statusData == null)
				{
					return null;
				}
				return this.statusData.StateLastUpdated;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0001080A File Offset: 0x0000EA0A
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x00010812 File Offset: 0x0000EA12
		public CultureInfo AdminCulture
		{
			get
			{
				return this.adminCulture;
			}
			private set
			{
				this.adminCulture = value;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0001081B File Offset: 0x0000EA1B
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x00010823 File Offset: 0x0000EA23
		public bool StatisticsEnabled
		{
			get
			{
				return this.statisticsEnabled;
			}
			private set
			{
				this.statisticsEnabled = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0001082C File Offset: 0x0000EA2C
		// (set) Token: 0x06000483 RID: 1155 RVA: 0x00010834 File Offset: 0x0000EA34
		public ADObjectId OwnerId
		{
			get
			{
				return this.ownerId;
			}
			private set
			{
				this.ownerId = value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0001083D File Offset: 0x0000EA3D
		// (set) Token: 0x06000485 RID: 1157 RVA: 0x00010845 File Offset: 0x0000EA45
		public Guid OwnerExchangeObjectId
		{
			get
			{
				return this.ownerExchangeObjectId;
			}
			private set
			{
				this.ownerExchangeObjectId = value;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0001084E File Offset: 0x0000EA4E
		public DelegatedPrincipal DelegatedAdminOwner
		{
			get
			{
				if (this.delegatedAdminOwner == null && this.OwnerId == null && this.OwnerExchangeObjectId == Guid.Empty)
				{
					DelegatedPrincipal.TryParseDelegatedString(this.DelegatedAdminOwnerId, out this.delegatedAdminOwner);
				}
				return this.delegatedAdminOwner;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0001088A File Offset: 0x0000EA8A
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x00010892 File Offset: 0x0000EA92
		public string DelegatedAdminOwnerId { get; private set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0001089B File Offset: 0x0000EA9B
		public bool UseAdvancedValidation
		{
			get
			{
				return this.GetBatchFlags(MigrationBatchFlags.UseAdvancedValidation);
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x000108A4 File Offset: 0x0000EAA4
		public bool AutoComplete
		{
			get
			{
				return this.GetBatchFlags(MigrationBatchFlags.AutoComplete);
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x000108AD File Offset: 0x0000EAAD
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x000108B6 File Offset: 0x0000EAB6
		public bool DisallowExistingUsers
		{
			get
			{
				return this.GetBatchFlags(MigrationBatchFlags.DisallowExistingUsers);
			}
			set
			{
				this.SetBatchFlags(MigrationBatchFlags.DisallowExistingUsers, value);
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x000108C0 File Offset: 0x0000EAC0
		// (set) Token: 0x0600048E RID: 1166 RVA: 0x000108CA File Offset: 0x0000EACA
		public bool AutoStop
		{
			get
			{
				return this.GetBatchFlags(MigrationBatchFlags.AutoStop);
			}
			private set
			{
				this.SetBatchFlags(MigrationBatchFlags.AutoStop, value);
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x000108D5 File Offset: 0x0000EAD5
		public bool SkipSettingTargetAddress
		{
			get
			{
				return this.GetShouldSkip(SkippableMigrationSteps.SettingTargetAddress);
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x000108DE File Offset: 0x0000EADE
		public bool IsProvisioningSupported
		{
			get
			{
				return MigrationJob.MigrationTypeSupportsProvisioning(this.MigrationType);
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x000108EB File Offset: 0x0000EAEB
		public bool UpdateSourceOnFinalization
		{
			get
			{
				return this.MigrationType == MigrationType.ExchangeOutlookAnywhere;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x000108F9 File Offset: 0x0000EAF9
		// (set) Token: 0x06000493 RID: 1171 RVA: 0x00010901 File Offset: 0x0000EB01
		public SubmittedByUserAdminType SubmittedByUserAdminType
		{
			get
			{
				return this.submittedByUserAdminType;
			}
			private set
			{
				this.submittedByUserAdminType = value;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x0001090A File Offset: 0x0000EB0A
		// (set) Token: 0x06000495 RID: 1173 RVA: 0x00010912 File Offset: 0x0000EB12
		public int PoisonCount { get; private set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x0001091C File Offset: 0x0000EB1C
		public override PropertyDefinition[] PropertyDefinitions
		{
			get
			{
				string key = this.MigrationType.ToString() + (this.IsPAW ? "PAW" : "non-PAW");
				PropertyDefinition[] array;
				if (!MigrationJob.PropertyDefinitionsHash.TryGetValue(key, out array))
				{
					array = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
					{
						MigrationJob.MigrationJobPropertyDefinition,
						this.SubscriptionSettingsPropertyDefinitions
					});
					MigrationJob.PropertyDefinitionsHash[key] = array;
				}
				return array;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0001098E File Offset: 0x0000EB8E
		// (set) Token: 0x06000498 RID: 1176 RVA: 0x00010996 File Offset: 0x0000EB96
		public MultiValuedProperty<MigrationReportSet> Reports { get; private set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0001099F File Offset: 0x0000EB9F
		// (set) Token: 0x0600049A RID: 1178 RVA: 0x000109A7 File Offset: 0x0000EBA7
		public bool IsStaged
		{
			get
			{
				return this.isStaged;
			}
			private set
			{
				this.isStaged = value;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x000109B0 File Offset: 0x0000EBB0
		public bool SupportsIncrementalSyncs
		{
			get
			{
				return !this.IsStaged || this.MigrationType != MigrationType.ExchangeOutlookAnywhere;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x000109C8 File Offset: 0x0000EBC8
		public bool SupportsRichRecipientType
		{
			get
			{
				return this.MigrationType == MigrationType.ExchangeOutlookAnywhere;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x000109D4 File Offset: 0x0000EBD4
		public bool SupportsMultiBatchFinalization
		{
			get
			{
				return this.MigrationType == MigrationType.ExchangeLocalMove || this.MigrationType == MigrationType.ExchangeRemoteMove || this.MigrationType == MigrationType.PSTImport || (this.MigrationType == MigrationType.IMAP && this.IsPAW) || this.MigrationType == MigrationType.XO1 || this.MigrationType == MigrationType.PublicFolder;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x00010A28 File Offset: 0x0000EC28
		public bool SupportsSyncTimeouts
		{
			get
			{
				return this.MigrationType != MigrationType.ExchangeLocalMove;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x00010A37 File Offset: 0x0000EC37
		public override PropertyDefinition[] InitializationPropertyDefinitions
		{
			get
			{
				return MigrationJob.MigrationJobTypeDefinition;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00010A3E File Offset: 0x0000EC3E
		public override long MaximumSupportedVersion
		{
			get
			{
				return 5L;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00010A42 File Offset: 0x0000EC42
		public override long MinimumSupportedVersion
		{
			get
			{
				return 4L;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00010A46 File Offset: 0x0000EC46
		public override long MinimumSupportedPersistableVersion
		{
			get
			{
				return 4L;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00010A4A File Offset: 0x0000EC4A
		public override long CurrentSupportedVersion
		{
			get
			{
				return this.currentSupportedVersion;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00010A52 File Offset: 0x0000EC52
		public bool IsPAW
		{
			get
			{
				return base.Version >= 5L;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00010A64 File Offset: 0x0000EC64
		public bool ShouldReport
		{
			get
			{
				if (this.Flags.HasFlag(MigrationFlags.Report))
				{
					return true;
				}
				if (this.ReportInterval == TimeSpan.Zero)
				{
					return false;
				}
				DateTime t = DateTime.UtcNow - this.ReportInterval;
				DateTime dateTime = (DateTime)this.OriginalCreationTime;
				if (this.StartTime != null && this.GetBatchFlags(MigrationBatchFlags.ReportInitial))
				{
					dateTime = (DateTime)this.StartTime.Value;
				}
				foreach (MigrationReportSet migrationReportSet in this.Reports)
				{
					if (migrationReportSet.CreationTimeUTC > dateTime)
					{
						dateTime = migrationReportSet.CreationTimeUTC;
					}
				}
				return dateTime < t;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x00010B48 File Offset: 0x0000ED48
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x00010B50 File Offset: 0x0000ED50
		public string TenantName { get; private set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x00010B59 File Offset: 0x0000ED59
		// (set) Token: 0x060004A9 RID: 1193 RVA: 0x00010B6C File Offset: 0x0000ED6C
		public MigrationBatchFlags BatchFlags
		{
			get
			{
				return base.ExtendedProperties.Get<MigrationBatchFlags>("MigrationBatchFlags", MigrationBatchFlags.None);
			}
			private set
			{
				base.ExtendedProperties.Set<MigrationBatchFlags>("MigrationBatchFlags", value);
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x00010B7F File Offset: 0x0000ED7F
		// (set) Token: 0x060004AB RID: 1195 RVA: 0x00010BA0 File Offset: 0x0000EDA0
		public TimeSpan? IncrementalSyncInterval
		{
			get
			{
				return new TimeSpan?(base.ExtendedProperties.Get<TimeSpan>("IncrementalSyncInterval", ConfigBase<MigrationServiceConfigSchema>.GetConfig<TimeSpan>("SyncMigrationPollingTimeout")));
			}
			private set
			{
				TimeSpan? timeSpan = value;
				if (timeSpan != null)
				{
					base.ExtendedProperties.Set<TimeSpan>("IncrementalSyncInterval", timeSpan.Value);
					return;
				}
				base.ExtendedProperties.Remove("IncrementalSyncInterval");
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x00010BE0 File Offset: 0x0000EDE0
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x00010BFC File Offset: 0x0000EDFC
		public TimeSpan ReportInterval
		{
			get
			{
				return base.ExtendedProperties.Get<TimeSpan>("ReportInterval", ConfigBase<MigrationServiceConfigSchema>.GetConfig<TimeSpan>("ReportInterval"));
			}
			private set
			{
				TimeSpan? timeSpan = new TimeSpan?(value);
				if (timeSpan != null)
				{
					base.ExtendedProperties.Set<TimeSpan>("ReportInterval", timeSpan.Value);
					return;
				}
				base.ExtendedProperties.Remove("ReportInterval");
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00010C44 File Offset: 0x0000EE44
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x00010C6A File Offset: 0x0000EE6A
		public ExDateTime? BatchLastUpdated
		{
			get
			{
				return base.ExtendedProperties.Get<ExDateTime?>("BatchLastUpdated", null);
			}
			private set
			{
				base.ExtendedProperties.Set<ExDateTime?>("BatchLastUpdated", value);
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00010C7D File Offset: 0x0000EE7D
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x00010C90 File Offset: 0x0000EE90
		public string BatchInputId
		{
			get
			{
				return base.ExtendedProperties.Get<string>("BatchInputId", null);
			}
			private set
			{
				base.ExtendedProperties.Set<string>("BatchInputId", value);
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00010CA4 File Offset: 0x0000EEA4
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x00010CCA File Offset: 0x0000EECA
		public Guid? OriginalJobId
		{
			get
			{
				return base.ExtendedProperties.Get<Guid?>("OriginalJobId", null);
			}
			private set
			{
				base.ExtendedProperties.Set<Guid?>("OriginalJobId", value);
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00010CE0 File Offset: 0x0000EEE0
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x00010D06 File Offset: 0x0000EF06
		public ExDateTime? InitialSyncDateTime
		{
			get
			{
				return base.ExtendedProperties.Get<ExDateTime?>("InitialSyncDateTime", null);
			}
			private set
			{
				base.ExtendedProperties.Set<ExDateTime?>("InitialSyncDateTime", value);
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00010D1C File Offset: 0x0000EF1C
		// (set) Token: 0x060004B7 RID: 1207 RVA: 0x00010D42 File Offset: 0x0000EF42
		public TimeSpan? InitialSyncDuration
		{
			get
			{
				return base.ExtendedProperties.Get<TimeSpan?>("InitialSyncDuration", null);
			}
			private set
			{
				base.ExtendedProperties.Set<TimeSpan?>("InitialSyncDuration", value);
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00010D55 File Offset: 0x0000EF55
		// (set) Token: 0x060004B9 RID: 1209 RVA: 0x00010D6C File Offset: 0x0000EF6C
		public TimeSpan ProcessingDuration
		{
			get
			{
				return base.ExtendedProperties.Get<TimeSpan>("ProcessingDuration", TimeSpan.Zero);
			}
			private set
			{
				base.ExtendedProperties.Set<TimeSpan>("ProcessingDuration", value);
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00010D7F File Offset: 0x0000EF7F
		// (set) Token: 0x060004BB RID: 1211 RVA: 0x00010D92 File Offset: 0x0000EF92
		public int ValidationWarningCount
		{
			get
			{
				return base.ExtendedProperties.Get<int>("ValidationWarningCount", 0);
			}
			private set
			{
				base.ExtendedProperties.Set<int>("ValidationWarningCount", value);
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x00010DA5 File Offset: 0x0000EFA5
		// (set) Token: 0x060004BD RID: 1213 RVA: 0x00010DB7 File Offset: 0x0000EFB7
		public string TargetDomainName
		{
			get
			{
				return base.ExtendedProperties.Get<string>("TargetDomainName");
			}
			set
			{
				base.ExtendedProperties.Set<string>("TargetDomainName", value);
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00010DCA File Offset: 0x0000EFCA
		// (set) Token: 0x060004BF RID: 1215 RVA: 0x00010DDC File Offset: 0x0000EFDC
		public int? MaxAutoRunCount
		{
			get
			{
				return base.ExtendedProperties.Get<int?>("MaxAutoRunCount");
			}
			private set
			{
				base.ExtendedProperties.Set<int?>("MaxAutoRunCount", value);
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x00010DEF File Offset: 0x0000EFEF
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x00010E02 File Offset: 0x0000F002
		public int AutoRunCount
		{
			get
			{
				return base.ExtendedProperties.Get<int>("AutoRunCount", 0);
			}
			private set
			{
				base.ExtendedProperties.Set<int>("AutoRunCount", value);
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x00010E15 File Offset: 0x0000F015
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x00010E28 File Offset: 0x0000F028
		public bool AllowUnknownColumnsInCsv
		{
			get
			{
				return base.ExtendedProperties.Get<bool>("AllowUnknownColumnsInCsv", false);
			}
			private set
			{
				base.ExtendedProperties.Set<bool>("AllowUnknownColumnsInCsv", value);
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00010E3B File Offset: 0x0000F03B
		// (set) Token: 0x060004C5 RID: 1221 RVA: 0x00010E43 File Offset: 0x0000F043
		public MigrationFlags Flags { get; private set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00010E4C File Offset: 0x0000F04C
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x00010E54 File Offset: 0x0000F054
		public MigrationStage Stage { get; private set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00010E5D File Offset: 0x0000F05D
		// (set) Token: 0x060004C9 RID: 1225 RVA: 0x00010E65 File Offset: 0x0000F065
		public MigrationWorkflow Workflow { get; private set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x00010E6E File Offset: 0x0000F06E
		// (set) Token: 0x060004CB RID: 1227 RVA: 0x00010E76 File Offset: 0x0000F076
		public MigrationBatchDirection JobDirection { get; private set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x00010E7F File Offset: 0x0000F07F
		// (set) Token: 0x060004CD RID: 1229 RVA: 0x00010E87 File Offset: 0x0000F087
		public MigrationEndpointBase SourceEndpoint { get; private set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00010E90 File Offset: 0x0000F090
		// (set) Token: 0x060004CF RID: 1231 RVA: 0x00010E98 File Offset: 0x0000F098
		public MigrationEndpointBase TargetEndpoint { get; private set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00010EA1 File Offset: 0x0000F0A1
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x00010EA9 File Offset: 0x0000F0A9
		public IJobSubscriptionSettings SubscriptionSettings { get; private set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00010EB2 File Offset: 0x0000F0B2
		// (set) Token: 0x060004D3 RID: 1235 RVA: 0x00010EBA File Offset: 0x0000F0BA
		public SkippableMigrationSteps SkipSteps { get; private set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00010EC3 File Offset: 0x0000F0C3
		// (set) Token: 0x060004D5 RID: 1237 RVA: 0x00010ED6 File Offset: 0x0000F0D6
		public string TroubleshooterNotes
		{
			get
			{
				return base.ExtendedProperties.Get<string>("TroubleshooterNotes", null);
			}
			private set
			{
				base.ExtendedProperties.Set<string>("TroubleshooterNotes", value);
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00010EE9 File Offset: 0x0000F0E9
		public bool ShouldProcessDataRows
		{
			get
			{
				return !this.IsStaged || !string.IsNullOrEmpty(this.BatchInputId);
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00010F04 File Offset: 0x0000F104
		public bool CompleteAfterMoveSyncCompleted
		{
			get
			{
				MoveJobSubscriptionSettings moveJobSubscriptionSettings = this.SubscriptionSettings as MoveJobSubscriptionSettings;
				return moveJobSubscriptionSettings != null && moveJobSubscriptionSettings.CompleteAfter != null && moveJobSubscriptionSettings.CompleteAfter.Value < ExDateTime.UtcNow;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00010F4C File Offset: 0x0000F14C
		public bool CompleteAfterMoveSyncNotCompleted
		{
			get
			{
				MoveJobSubscriptionSettings moveJobSubscriptionSettings = this.SubscriptionSettings as MoveJobSubscriptionSettings;
				return moveJobSubscriptionSettings != null && moveJobSubscriptionSettings.CompleteAfter != null && moveJobSubscriptionSettings.CompleteAfter.Value >= ExDateTime.UtcNow;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00010F94 File Offset: 0x0000F194
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x00010FBA File Offset: 0x0000F1BA
		internal Guid? NotificationId
		{
			get
			{
				return base.ExtendedProperties.Get<Guid?>("AsyncNotificationId", null);
			}
			set
			{
				base.ExtendedProperties.Set<Guid?>("AsyncNotificationId", value);
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00010FCD File Offset: 0x0000F1CD
		protected override Guid ReportGuid
		{
			get
			{
				return this.JobId;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00010FD5 File Offset: 0x0000F1D5
		private PropertyDefinition[] SubscriptionSettingsPropertyDefinitions
		{
			get
			{
				return JobSubscriptionSettingsBase.GetPropertyDefinitions(this.MigrationType, this.IsPAW);
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00010FE8 File Offset: 0x0000F1E8
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x00010FF0 File Offset: 0x0000F1F0
		private int MaxConcurrentMigrations { get; set; }

		// Token: 0x060004DF RID: 1247 RVA: 0x00010FFC File Offset: 0x0000F1FC
		public static MigrationJob Create(IMigrationDataProvider provider, IMigrationConfig config, MigrationBatch migrationBatch)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.ThrowOnNullArgument(migrationBatch, "migrationBatch");
			bool usePAW = config != null && config.IsSupported(MigrationFeature.PAW);
			MigrationType migrationType = migrationBatch.MigrationType;
			MigrationLogger.Log(MigrationEventType.Warning, "MigrationJob.Create: migrationBatch {0}", new object[]
			{
				MigrationLogger.PropertyBagToString(migrationBatch.propertyBag)
			});
			MigrationJob migrationJob = new MigrationJob(migrationType);
			migrationJob.TenantName = provider.TenantName;
			migrationJob.Initialize(migrationBatch, provider, usePAW);
			migrationJob.CreateInStore(provider, migrationBatch.CsvStream, migrationBatch.ValidationWarnings);
			MigrationLogger.Log(MigrationEventType.Warning, "MigrationJob.Create: job {0}", new object[]
			{
				migrationJob
			});
			return migrationJob;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001109F File Offset: 0x0000F29F
		public static MigrationJob GetSingleMigrationJob(IMigrationDataProvider provider)
		{
			return MigrationJob.GetUniqueJob(provider, null);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x000110A8 File Offset: 0x0000F2A8
		public static IEnumerable<MigrationJob> Get(IMigrationDataProvider provider, IMigrationConfig config)
		{
			return MigrationJob.GetJobs(provider, null);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x000110B1 File Offset: 0x0000F2B1
		public static IEnumerable<MigrationJob> GetByStatus(IMigrationDataProvider provider, IMigrationConfig config, MigrationJobStatus status)
		{
			return MigrationJob.GetJobs(provider, new MigrationEqualityFilter(MigrationBatchMessageSchema.MigrationUserStatus, status));
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x000110C9 File Offset: 0x0000F2C9
		public static MigrationJob GetUniqueByName(IMigrationDataProvider provider, string jobName)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.ThrowOnNullOrEmptyArgument(jobName, "jobName");
			return MigrationJob.GetUniqueJob(provider, new MigrationEqualityFilter(MigrationBatchMessageSchema.MigrationJobName, jobName, true));
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x000110F3 File Offset: 0x0000F2F3
		public static IEnumerable<MigrationJob> GetByName(IMigrationDataProvider provider, IMigrationConfig config, string jobName)
		{
			return MigrationJob.GetJobs(provider, new MigrationEqualityFilter(MigrationBatchMessageSchema.MigrationJobName, jobName));
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00011108 File Offset: 0x0000F308
		public static IEnumerable<MigrationJob> GetByEndpoint(IMigrationDataProvider provider, MigrationEndpointId endpointId)
		{
			MigrationUtil.ThrowOnNullArgument(endpointId, "endpointId");
			if (endpointId.Guid == Guid.Empty || MigrationEndpointId.Any.Equals(endpointId))
			{
				throw new ArgumentException("EndpointId should be a non-empty GUID for a specific endpoint.", "endpointId");
			}
			IEnumerable<MigrationJob> jobs = MigrationJob.GetJobs(provider, new MigrationEqualityFilter(MigrationBatchMessageSchema.MigrationJobSourceEndpoint, endpointId.Guid));
			IEnumerable<MigrationJob> jobs2 = MigrationJob.GetJobs(provider, new MigrationEqualityFilter(MigrationBatchMessageSchema.MigrationJobTargetEndpoint, endpointId.Guid));
			return jobs.Union(jobs2);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0001118E File Offset: 0x0000F38E
		public static MigrationJob GetUniqueByJobId(IMigrationDataProvider provider, Guid jobId)
		{
			return MigrationJob.GetUniqueJob(provider, new MigrationEqualityFilter(MigrationBatchMessageSchema.MigrationJobId, jobId));
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x000111A8 File Offset: 0x0000F3A8
		public static MigrationJob GetUniqueByBatchId(IMigrationDataProvider provider, MigrationBatchId batchId)
		{
			MigrationUtil.ThrowOnNullArgument(batchId, "batchId");
			MigrationUtil.AssertOrThrow(batchId.JobId != Guid.Empty || !string.IsNullOrWhiteSpace(batchId.Name), "At least one of Name or JobId must be present on the batchId.", new object[0]);
			MigrationJob migrationJob = null;
			if (batchId.JobId != Guid.Empty)
			{
				migrationJob = MigrationJob.GetUniqueByJobId(provider, batchId.JobId);
			}
			return migrationJob ?? MigrationJob.GetUniqueByName(provider, batchId.Name);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00011230 File Offset: 0x0000F430
		public static MigrationBatch GetMigrationBatch(IMigrationDataProvider provider, MigrationSession session, MigrationJob job)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.ThrowOnNullArgument(job, "job");
			bool supportsMultiBatchFinalization = job.SupportsMultiBatchFinalization;
			MigrationBatch migrationBatch = new MigrationBatch();
			migrationBatch.Identity = new MigrationBatchId(job.JobName, job.JobId);
			migrationBatch.MigrationType = job.MigrationType;
			migrationBatch.BatchDirection = job.JobDirection;
			migrationBatch.BatchFlags = job.BatchFlags;
			migrationBatch.AutoRetryCount = job.MaxAutoRunCount;
			migrationBatch.CurrentRetryCount = job.AutoRunCount;
			migrationBatch.SkipSteps = job.SkipSteps;
			migrationBatch.SubmittedByUser = job.SubmittedByUser;
			migrationBatch.OwnerId = job.OwnerId;
			migrationBatch.OwnerExchangeObjectId = job.OwnerExchangeObjectId;
			migrationBatch.UserTimeZone = new ExTimeZoneValue(job.UserTimeZone);
			ExDateTime value = MigrationHelper.GetUniversalDateTime(new ExDateTime?(job.CreationTime)).Value;
			ExDateTime? universalDateTime = MigrationHelper.GetUniversalDateTime(job.StartTime);
			ExDateTime? universalDateTime2 = MigrationHelper.GetUniversalDateTime(job.FinalizeTime);
			ExDateTime? universalDateTime3 = MigrationHelper.GetUniversalDateTime(job.LastSyncTime);
			ExDateTime? universalDateTime4 = MigrationHelper.GetUniversalDateTime(job.InitialSyncDateTime);
			migrationBatch.CreationDateTimeUTC = (DateTime)value;
			migrationBatch.StartDateTimeUTC = (DateTime?)universalDateTime;
			migrationBatch.FinalizedDateTimeUTC = (DateTime?)universalDateTime2;
			migrationBatch.LastSyncedDateTimeUTC = (DateTime?)universalDateTime3;
			migrationBatch.InitialSyncDateTimeUTC = (DateTime?)universalDateTime4;
			migrationBatch.InitialSyncDuration = job.InitialSyncDuration;
			migrationBatch.CreationDateTime = (DateTime)MigrationHelper.GetLocalizedDateTime(new ExDateTime?(value), job.UserTimeZone).Value;
			migrationBatch.StartDateTime = (DateTime?)MigrationHelper.GetLocalizedDateTime(universalDateTime, job.UserTimeZone);
			migrationBatch.FinalizedDateTime = (DateTime?)MigrationHelper.GetLocalizedDateTime(universalDateTime2, job.UserTimeZone);
			migrationBatch.LastSyncedDateTime = (DateTime?)MigrationHelper.GetLocalizedDateTime(universalDateTime3, job.UserTimeZone);
			migrationBatch.InitialSyncDateTime = (DateTime?)MigrationHelper.GetLocalizedDateTime(universalDateTime4, job.UserTimeZone);
			migrationBatch.Locale = job.AdminCulture;
			migrationBatch.OriginalBatchId = job.OriginalJobId;
			migrationBatch.Report = new Report(job.ReportData);
			MigrationJob.InitializeEndpointsForMigrationBatch(job, migrationBatch);
			migrationBatch.BatchDirection = job.JobDirection;
			migrationBatch.SupportedActions = MigrationBatchSupportedActions.None;
			if (job.IsPAW)
			{
				LocalizedString? localizedString;
				if (job.SupportsFlag(MigrationFlags.Remove, out localizedString))
				{
					migrationBatch.SupportedActions |= MigrationBatchSupportedActions.Remove;
				}
				if (job.SupportsFlag(MigrationFlags.Start, out localizedString))
				{
					migrationBatch.SupportedActions |= MigrationBatchSupportedActions.Start;
				}
				if (job.SupportsFlag(MigrationFlags.Stop, out localizedString))
				{
					migrationBatch.SupportedActions |= MigrationBatchSupportedActions.Stop;
				}
			}
			else
			{
				LocalizedString? localizedString;
				if (job.SupportsStarting(out localizedString))
				{
					migrationBatch.SupportedActions |= MigrationBatchSupportedActions.Start;
				}
				if (job.SupportsSetting(out localizedString))
				{
					migrationBatch.SupportedActions |= MigrationBatchSupportedActions.Set;
				}
				if (job.SupportsAppendingUsers(out localizedString))
				{
					migrationBatch.SupportedActions |= MigrationBatchSupportedActions.Append;
				}
				if (job.SupportsStopping(out localizedString))
				{
					migrationBatch.SupportedActions |= MigrationBatchSupportedActions.Stop;
				}
				if (job.SupportsRemoving(out localizedString))
				{
					migrationBatch.SupportedActions |= MigrationBatchSupportedActions.Remove;
				}
				if (job.SupportsCompleting(out localizedString))
				{
					migrationBatch.SupportedActions |= MigrationBatchSupportedActions.Complete;
				}
			}
			if (job.IsPAW)
			{
				migrationBatch.Flags = job.Flags;
			}
			ExDateTime? exDateTime = null;
			ExDateTime? exDateTime2 = null;
			MigrationType migrationType = job.MigrationType;
			if (migrationType <= MigrationType.ExchangeOutlookAnywhere)
			{
				if (migrationType != MigrationType.IMAP)
				{
					if (migrationType == MigrationType.ExchangeOutlookAnywhere)
					{
						ExchangeJobSubscriptionSettings exchangeJobSubscriptionSettings = job.SubscriptionSettings as ExchangeJobSubscriptionSettings;
						exDateTime = ((exchangeJobSubscriptionSettings != null) ? exchangeJobSubscriptionSettings.StartAfter : null);
					}
				}
				else
				{
					IMAPPAWJobSubscriptionSettings imappawjobSubscriptionSettings = job.SubscriptionSettings as IMAPPAWJobSubscriptionSettings;
					exDateTime = ((imappawjobSubscriptionSettings != null) ? imappawjobSubscriptionSettings.StartAfter : null);
					exDateTime2 = ((imappawjobSubscriptionSettings != null) ? imappawjobSubscriptionSettings.CompleteAfter : null);
				}
			}
			else if (migrationType == MigrationType.ExchangeRemoteMove || migrationType == MigrationType.ExchangeLocalMove)
			{
				MoveJobSubscriptionSettings moveJobSubscriptionSettings = job.SubscriptionSettings as MoveJobSubscriptionSettings;
				exDateTime = ((moveJobSubscriptionSettings != null) ? moveJobSubscriptionSettings.StartAfter : null);
				exDateTime2 = ((moveJobSubscriptionSettings != null) ? moveJobSubscriptionSettings.CompleteAfter : null);
			}
			if (exDateTime != null)
			{
				if (exDateTime.Value >= ExDateTime.UtcNow)
				{
					migrationBatch.StartDateTimeUTC = null;
					migrationBatch.StartDateTime = null;
				}
				else
				{
					migrationBatch.StartDateTimeUTC = (DateTime?)exDateTime;
					migrationBatch.StartDateTime = (DateTime?)MigrationHelper.GetLocalizedDateTime(exDateTime, job.UserTimeZone);
				}
			}
			bool flag = exDateTime != null && exDateTime.Value >= ExDateTime.UtcNow;
			bool flag2 = exDateTime2 != null && exDateTime2.Value >= ExDateTime.UtcNow;
			migrationBatch.Status = MigrationBatchStatus.Failed;
			switch (job.Status)
			{
			case MigrationJobStatus.Created:
				migrationBatch.Status = MigrationBatchStatus.Created;
				break;
			case MigrationJobStatus.SyncInitializing:
			case MigrationJobStatus.SyncStarting:
			case MigrationJobStatus.SyncCompleting:
			case MigrationJobStatus.ProvisionStarting:
			case MigrationJobStatus.Validating:
				migrationBatch.Status = MigrationBatchStatus.Syncing;
				break;
			case MigrationJobStatus.SyncCompleted:
				migrationBatch.Status = MigrationBatchStatus.Synced;
				break;
			case MigrationJobStatus.CompletionInitializing:
			case MigrationJobStatus.CompletionStarting:
			case MigrationJobStatus.Completing:
				if (supportsMultiBatchFinalization || job.IsPAW)
				{
					migrationBatch.Status = MigrationBatchStatus.Completing;
				}
				else
				{
					migrationBatch.Status = MigrationBatchStatus.Removing;
				}
				break;
			case MigrationJobStatus.Completed:
				if (supportsMultiBatchFinalization || job.IsPAW)
				{
					migrationBatch.Status = MigrationBatchStatus.Completed;
				}
				else
				{
					migrationBatch.Status = MigrationBatchStatus.Removing;
				}
				break;
			case MigrationJobStatus.Failed:
				migrationBatch.Status = MigrationBatchStatus.Failed;
				break;
			case MigrationJobStatus.Removing:
				migrationBatch.Status = MigrationBatchStatus.Removing;
				break;
			case MigrationJobStatus.Stopped:
				migrationBatch.Status = MigrationBatchStatus.Stopped;
				break;
			case MigrationJobStatus.Corrupted:
				migrationBatch.Status = MigrationBatchStatus.Corrupted;
				break;
			}
			if (job.IsPAW)
			{
				if (job.Flags.HasFlag(MigrationFlags.Start))
				{
					migrationBatch.Status = MigrationBatchStatus.Starting;
				}
				if (job.Flags.HasFlag(MigrationFlags.Stop))
				{
					migrationBatch.Status = MigrationBatchStatus.Stopping;
				}
				if (job.Flags.HasFlag(MigrationFlags.Remove))
				{
					migrationBatch.Status = MigrationBatchStatus.Removing;
				}
			}
			else if (job.IsCancelled && (MigrationJobStage.Sync.IsStatusSupported(job.Status) || MigrationJobStage.Completion.IsStatusSupported(job.Status) || MigrationJobStage.Incremental.IsStatusSupported(job.Status)))
			{
				migrationBatch.Status = MigrationBatchStatus.Stopping;
			}
			if (flag && (migrationBatch.Status == MigrationBatchStatus.Completing || migrationBatch.Status == MigrationBatchStatus.Syncing))
			{
				migrationBatch.Status = MigrationBatchStatus.Waiting;
			}
			else if (migrationBatch.Status == MigrationBatchStatus.Completing && job.AutoComplete)
			{
				migrationBatch.Status = MigrationBatchStatus.Syncing;
			}
			if (job.IsPAW)
			{
				if (migrationBatch.Status == MigrationBatchStatus.Syncing && job.Stage == MigrationStage.Processing && job.ActiveItemCount == 0 && (job.SyncedItemCount > 0 || job.FailedItemCount > 0) && job.PendingCount == 0 && (flag2 || !supportsMultiBatchFinalization))
				{
					migrationBatch.Status = MigrationBatchStatus.Synced;
					if (supportsMultiBatchFinalization)
					{
						migrationBatch.SupportedActions |= MigrationBatchSupportedActions.Complete;
					}
				}
				if (migrationBatch.Status == MigrationBatchStatus.Completed && (flag2 || !supportsMultiBatchFinalization))
				{
					migrationBatch.Status = MigrationBatchStatus.Synced;
				}
			}
			if (job.FailedItemCount > 0)
			{
				MigrationBatchStatus migrationBatchStatus = migrationBatch.Status;
				if (migrationBatchStatus != MigrationBatchStatus.Completed)
				{
					if (migrationBatchStatus == MigrationBatchStatus.Synced)
					{
						migrationBatch.Status = MigrationBatchStatus.SyncedWithErrors;
					}
				}
				else
				{
					migrationBatch.Status = MigrationBatchStatus.CompletedWithErrors;
				}
			}
			if (job.IsProvisioningSupported && job.Status == MigrationJobStatus.ProvisionStarting)
			{
				migrationBatch.IsProvisioning = true;
			}
			if (job.StatusData.LocalizedError != null)
			{
				migrationBatch.Message = job.StatusData.LocalizedError.Value;
			}
			if (!job.IsPAW)
			{
				migrationBatch.ValidationWarnings = MigrationHelper.ToMultiValuedProperty<MigrationBatchError>(from warning in job.GetValidationWarnings(provider)
				orderby warning.RowIndex
				select warning);
				migrationBatch.ValidationWarningCount = job.ValidationWarningCount;
			}
			if (job.IsPAW && job.HasCachedCounts && job.Stage == MigrationStage.Processing)
			{
				migrationBatch.TotalCount = job.TotalItemCount;
			}
			else
			{
				migrationBatch.TotalCount = job.TotalCount;
			}
			migrationBatch.ActiveCount = job.ActiveItemCount;
			migrationBatch.SyncedCount = job.SyncedItemCount;
			migrationBatch.FinalizedCount = job.FinalizedItemCount;
			migrationBatch.StoppedCount = job.StoppedItemCount;
			migrationBatch.FailedCount = job.FailedItemCount;
			migrationBatch.FailedInitialSyncCount = job.FailedInitialItemCount;
			migrationBatch.FailedIncrementalSyncCount = job.FailedIncrementalItemCount;
			migrationBatch.PendingCount = job.PendingCount;
			migrationBatch.ProvisionedCount = job.ProvisionedItemCount;
			migrationBatch.NotificationEmails = job.NotificationEmails;
			migrationBatch.Reports = job.Reports;
			IJobSubscriptionSettings subscriptionSettings = job.SubscriptionSettings;
			if (subscriptionSettings != null)
			{
				subscriptionSettings.WriteToBatch(migrationBatch);
				migrationBatch.SubscriptionSettingsModified = (DateTime)MigrationHelper.GetUniversalDateTime(new ExDateTime?(subscriptionSettings.LastModifiedTime)).Value;
			}
			return migrationBatch;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00011AB8 File Offset: 0x0000FCB8
		public static bool TryLoad(IMigrationDataProvider provider, StoreObjectId messageId, out MigrationJob migrationJob)
		{
			migrationJob = new MigrationJob();
			try
			{
				if (!migrationJob.TryLoad(provider, messageId))
				{
					migrationJob = null;
				}
			}
			catch (MigrationDataCorruptionException ex)
			{
				MigrationLogger.Log(MigrationEventType.Error, "Tried to get migration job but failed. marking job corrupt. {0}", new object[]
				{
					ex
				});
				if (migrationJob.Status != MigrationJobStatus.Failed)
				{
					MigrationStatusData<MigrationJobStatus> migrationStatusData;
					if (migrationJob.StatusData != null)
					{
						migrationStatusData = new MigrationStatusData<MigrationJobStatus>(migrationJob.StatusData);
					}
					else
					{
						migrationStatusData = new MigrationStatusData<MigrationJobStatus>(MigrationJob.StatusDataVersionMap[migrationJob.Version]);
					}
					migrationStatusData.SetFailedStatus(MigrationJobStatus.Failed, ex, "MigrationJob::TryLoad", null);
					migrationJob.StatusData = migrationStatusData;
					migrationJob.LogStatusEvent();
					MigrationFailureLog.LogFailureEvent(migrationJob, ex, MigrationFailureFlags.Corruption, null);
				}
			}
			catch (ObjectNotFoundException ex2)
			{
				MigrationLogger.Log(MigrationEventType.Warning, "Failed to load a migration job.  {0}", new object[]
				{
					ex2
				});
				migrationJob = null;
			}
			if (migrationJob != null)
			{
				migrationJob.TenantName = provider.TenantName;
			}
			return migrationJob != null;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00011BBC File Offset: 0x0000FDBC
		public static void DeleteAll(IMigrationDataProvider provider)
		{
			MigrationLogger.Log(MigrationEventType.Warning, "MigrationJob.DeleteAll", new object[0]);
			IEnumerable<StoreObjectId> enumerable = MigrationHelper.FindMessageIds(provider, MigrationJob.MessageClassEqualityFilter, null, null, null);
			enumerable = new List<StoreObjectId>(enumerable);
			foreach (StoreObjectId messageId in enumerable)
			{
				provider.RemoveMessage(messageId);
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00011C34 File Offset: 0x0000FE34
		public int GetItemCount(params MigrationUserStatus[] statuses)
		{
			return this.cachedItemCounts.GetCachedStatusCount(statuses ?? (this.IsPAW ? MigrationJob.AllUsedPAWJobItemStatuses : MigrationJob.AllJobItemsStatuses));
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00011C5C File Offset: 0x0000FE5C
		public bool SupportsStarting(out LocalizedString? errorMsg)
		{
			errorMsg = null;
			if (MigrationJobStage.Corrupted.IsStatusSupported(this.Status))
			{
				errorMsg = new LocalizedString?(Strings.CorruptedMigrationBatchCannotBeStarted);
				return false;
			}
			if (this.Status == MigrationJobStatus.Removed)
			{
				errorMsg = new LocalizedString?(Strings.RemovedMigrationJobCannotBeStarted);
				return false;
			}
			if (MigrationJobStage.Sync.IsStatusSupported(this.Status))
			{
				errorMsg = new LocalizedString?(this.IsCancelled ? Strings.StoppingMigrationJobCannotBeStarted : Strings.MigrationJobAlreadyStarted);
				return false;
			}
			if (MigrationJobStage.Completion.IsStatusSupported(this.Status))
			{
				errorMsg = new LocalizedString?(this.SupportsMultiBatchFinalization ? Strings.CompletingMigrationJobCannotBeStarted : Strings.RemovedMigrationJobCannotBeStarted);
				return false;
			}
			if (!this.AutoComplete && MigrationJobStage.Completed.IsStatusSupported(this.Status))
			{
				errorMsg = new LocalizedString?(Strings.CompletedMigrationJobCannotBeStartedMultiBatch);
				return false;
			}
			if (!MigrationJobStage.Incremental.IsStatusSupported(this.Status) && !MigrationJobStage.Completed.IsStatusSupported(this.Status))
			{
				return MigrationJobStage.Dormant.IsStatusSupported(this.Status);
			}
			MigrationUtil.AssertOrThrow(!MigrationJobStage.Completed.IsStatusSupported(this.Status) || this.AutoComplete, "expect either NOT completed or if completed that autocomplete is set", new object[0]);
			if (this.IsCancelled)
			{
				errorMsg = new LocalizedString?(Strings.StoppingMigrationJobCannotBeStarted);
				return false;
			}
			return this.SupportsRestarting(out errorMsg);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00011DC4 File Offset: 0x0000FFC4
		public bool SupportsStopping(out LocalizedString? errorMsg)
		{
			errorMsg = null;
			if (MigrationJobStage.Corrupted.IsStatusSupported(this.Status))
			{
				errorMsg = new LocalizedString?(Strings.CorruptedMigrationBatchCannotBeStopped);
				return false;
			}
			if (MigrationJobStage.Incremental.IsStatusSupported(this.Status) && this.MigrationType == MigrationType.IMAP)
			{
				errorMsg = new LocalizedString?(Strings.MigrationJobCannotBeStopped);
				return false;
			}
			if (this.Status == MigrationJobStatus.Stopped)
			{
				errorMsg = new LocalizedString?(Strings.MigrationJobAlreadyStopped);
				return false;
			}
			if (this.IsCancelled)
			{
				errorMsg = new LocalizedString?(Strings.MigrationJobAlreadyStopping);
				return false;
			}
			if (!MigrationJobStage.Incremental.IsStatusSupported(this.Status) && !MigrationJobStage.Sync.IsStatusSupported(this.Status))
			{
				errorMsg = new LocalizedString?(Strings.MigrationJobCannotBeStopped);
				return false;
			}
			return EnumValidator.IsValidValue<MigrationJobStatus>(this.Status);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00011EA0 File Offset: 0x000100A0
		public bool SupportsSetting(out LocalizedString? errorMsg)
		{
			errorMsg = null;
			MigrationJobStatus migrationJobStatus = this.Status;
			switch (migrationJobStatus)
			{
			case MigrationJobStatus.Completed:
				if (this.FailedItemCount > 0)
				{
					return true;
				}
				errorMsg = new LocalizedString?(Strings.CompletedMigrationJobCannotBeModified);
				return false;
			case MigrationJobStatus.Failed:
				break;
			case MigrationJobStatus.Removed:
				errorMsg = new LocalizedString?(Strings.RemovedMigrationJobCannotBeModified);
				return false;
			case MigrationJobStatus.Removing:
				errorMsg = new LocalizedString?(Strings.CompletedMigrationJobCannotBeModified);
				return false;
			default:
				if (migrationJobStatus == MigrationJobStatus.Corrupted)
				{
					errorMsg = new LocalizedString?(Strings.CorruptedMigrationBatchCannotBeModified);
					return false;
				}
				break;
			}
			return EnumValidator.IsValidValue<MigrationJobStatus>(this.Status);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00011F38 File Offset: 0x00010138
		public bool SupportsAppendingUsers(out LocalizedString? errorMsg)
		{
			errorMsg = null;
			if (!this.IsStaged || this.MigrationType == MigrationType.PublicFolder)
			{
				errorMsg = new LocalizedString?(Strings.MigrationJobDoesNotSupportAppendingUserCSV);
				return false;
			}
			if (MigrationJobStage.Corrupted.IsStatusSupported(this.Status))
			{
				errorMsg = new LocalizedString?(Strings.CorruptedMigrationBatchCannotBeModified);
				return false;
			}
			if (this.BatchInputId != null)
			{
				errorMsg = new LocalizedString?(Strings.MigrationJobAlreadyHasPendingCSV);
				return false;
			}
			if (MigrationJobStage.Completed.IsStatusSupported(this.Status))
			{
				errorMsg = new LocalizedString?(Strings.CompletedMigrationJobCannotBeModified);
				return false;
			}
			if (this.Status == MigrationJobStatus.Removed)
			{
				errorMsg = new LocalizedString?(Strings.RemovedMigrationJobCannotBeModified);
				return false;
			}
			if (MigrationJobStage.Completion.IsStatusSupported(this.Status))
			{
				errorMsg = new LocalizedString?(Strings.CompletingMigrationJobCannotBeAppendedTo);
				return false;
			}
			if (MigrationJobStage.Sync.IsStatusSupported(this.Status))
			{
				errorMsg = new LocalizedString?(Strings.SyncingMigrationJobCannotBeAppendedTo);
				return false;
			}
			return this.Status == MigrationJobStatus.Failed || this.Status == MigrationJobStatus.SyncCompleted || this.Status == MigrationJobStatus.Stopped;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00012058 File Offset: 0x00010258
		public bool SupportsRemovingUsers(out LocalizedString? errorMsg)
		{
			errorMsg = null;
			if (this.migrationJobType != MigrationType.PublicFolder && (MigrationJobStage.Dormant.IsStatusSupported(this.Status) || MigrationJobStage.Incremental.IsStatusSupported(this.Status) || MigrationJobStage.Corrupted.IsStatusSupported(this.Status) || MigrationJobStage.Completed.IsStatusSupported(this.Status)))
			{
				return true;
			}
			errorMsg = new LocalizedString?(Strings.RemovingMigrationUserBatchMustBeIdle);
			return false;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x000120D4 File Offset: 0x000102D4
		public bool SupportsCompleting(out LocalizedString? errorMsg)
		{
			errorMsg = null;
			if (MigrationJobStage.Corrupted.IsStatusSupported(this.Status))
			{
				errorMsg = new LocalizedString?(Strings.CorruptedMigrationBatchCannotBeCompleted);
				return false;
			}
			if (!this.SupportsMultiBatchFinalization)
			{
				errorMsg = new LocalizedString?(Strings.CompleteMigrationBatchNotSupported);
				return false;
			}
			bool flag = MigrationJobStage.Completed.IsStatusSupported(this.Status);
			if (this.Status != MigrationJobStatus.SyncCompleted && !flag)
			{
				errorMsg = new LocalizedString?(Strings.MigrationJobCannotBeCompleted);
				return false;
			}
			if (flag && this.FailedFinalizationItemCount == 0)
			{
				errorMsg = new LocalizedString?(Strings.MigrationJobCannotRetryCompletion);
				return false;
			}
			if (this.MigrationType == MigrationType.PublicFolder)
			{
				int cachedStatusCount = this.cachedItemCounts.GetCachedStatusCount(MigrationJobItem.PreventPublicFolderCompletionErrorStatuses);
				if (cachedStatusCount > 0)
				{
					errorMsg = new LocalizedString?(Strings.PublicFolderMigrationBatchCannotBeCompletedWithErrors);
					return false;
				}
			}
			return true;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x000121A8 File Offset: 0x000103A8
		public bool SupportsRemoving(out LocalizedString? errorMsg)
		{
			errorMsg = null;
			if (!MigrationJobStage.Incremental.IsStatusSupported(this.Status) && !MigrationJobStage.Completed.IsStatusSupported(this.Status) && !MigrationJobStage.Dormant.IsStatusSupported(this.Status) && !MigrationJobStage.Corrupted.IsStatusSupported(this.Status))
			{
				errorMsg = new LocalizedString?(Strings.MigrationJobCannotBeRemoved);
				return false;
			}
			return true;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00012218 File Offset: 0x00010418
		public bool SupportsFlag(MigrationFlags flag, out LocalizedString? errorMsg)
		{
			errorMsg = null;
			if (flag == MigrationFlags.Remove)
			{
				if (this.Flags.HasFlag(MigrationFlags.Remove))
				{
					errorMsg = new LocalizedString?(Strings.MigrationJobCannotBeRemoved);
					return false;
				}
				return true;
			}
			else if (flag == MigrationFlags.Start)
			{
				if ((this.State == MigrationState.Active || this.State == MigrationState.Waiting) && this.FailedItemCount == 0 && this.StoppedItemCount == 0 && !this.Flags.HasFlag(MigrationFlags.Stop))
				{
					if (!this.IsStaged)
					{
						return true;
					}
					errorMsg = new LocalizedString?(Strings.MigrationJobAlreadyStarted);
					return false;
				}
				else
				{
					if (this.State == MigrationState.Completed && this.FailedItemCount == 0 && this.StoppedItemCount == 0 && !this.Flags.HasFlag(MigrationFlags.Stop))
					{
						errorMsg = new LocalizedString?(Strings.MigrationJobCannotRetryCompletion);
						return false;
					}
					return true;
				}
			}
			else
			{
				if (flag != MigrationFlags.Stop)
				{
					return true;
				}
				if (this.State == MigrationState.Stopped && !this.Flags.HasFlag(MigrationFlags.Start))
				{
					errorMsg = new LocalizedString?(Strings.MigrationJobAlreadyStopped);
					return false;
				}
				if (this.Flags.HasFlag(MigrationFlags.Stop))
				{
					errorMsg = new LocalizedString?(Strings.MigrationJobAlreadyStopping);
					return false;
				}
				if (this.State == MigrationState.Corrupted)
				{
					errorMsg = new LocalizedString?(Strings.CorruptedMigrationBatchCannotBeStopped);
					return false;
				}
				if ((this.State == MigrationState.Completed && !this.Flags.HasFlag(MigrationFlags.Start)) || this.State == MigrationState.Failed)
				{
					errorMsg = new LocalizedString?(Strings.MigrationJobCannotBeStopped);
					return false;
				}
				return true;
			}
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x000123BC File Offset: 0x000105BC
		public void ClearTransientErrorCount(IMigrationDataProvider provider)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationLogger.Log(MigrationEventType.Information, "MigrationJob.ClearTransientError: job {0}", new object[]
			{
				this
			});
			MigrationStatusData<MigrationJobStatus> migrationStatusData = new MigrationStatusData<MigrationJobStatus>(this.StatusData);
			migrationStatusData.ClearTransientErrorCount();
			this.SetStatusData(provider, migrationStatusData, false);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00012408 File Offset: 0x00010608
		public void SetLastScheduled(IMigrationDataProvider provider, ExDateTime lastScheduled)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationLogger.Log(MigrationEventType.Verbose, "MigrationJob.SetLastScheduled: job {0} time {1}", new object[]
			{
				this,
				lastScheduled
			});
			using (IMigrationStoreObject migrationStoreObject = this.FindStoreObject(provider, base.StoreObjectId, MigrationJob.JobLastScheduledPropertyDefinition))
			{
				migrationStoreObject.OpenAsReadWrite();
				MigrationHelperBase.SetExDateTimeProperty(migrationStoreObject, MigrationBatchMessageSchema.MigrationJobItemSubscriptionLastChecked, new ExDateTime?(lastScheduled));
				migrationStoreObject.Save(SaveMode.ResolveConflicts);
			}
			this.LastScheduled = new ExDateTime?(lastScheduled);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0001249C File Offset: 0x0001069C
		public void SetMigrationFlags(IMigrationDataProvider provider, MigrationFlags flags)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationLogger.Log(MigrationEventType.Verbose, "MigrationJob.SetMigrationFlags: job {0} flags {1}", new object[]
			{
				this,
				flags
			});
			ExDateTime utcNow = ExDateTime.UtcNow;
			PropertyDefinition[] properties = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
			{
				MigrationJob.JobMigrationFlagsPropertyDefinition,
				MigrationPersistableBase.MigrationBaseDefinitions,
				new StorePropertyDefinition[]
				{
					MigrationBatchMessageSchema.MigrationJobPoisonCount,
					MigrationBatchMessageSchema.MigrationJobStartTime
				}
			});
			using (IMigrationStoreObject migrationStoreObject = this.FindStoreObject(provider, base.StoreObjectId, properties))
			{
				migrationStoreObject.OpenAsReadWrite();
				migrationStoreObject[MigrationBatchMessageSchema.MigrationFlags] = flags;
				migrationStoreObject[MigrationBatchMessageSchema.MigrationJobPoisonCount] = 0;
				if (flags.HasFlag(MigrationFlags.Start))
				{
					MigrationHelperBase.SetExDateTimeProperty(migrationStoreObject, MigrationBatchMessageSchema.MigrationJobStartTime, new ExDateTime?(utcNow));
					if (ConfigBase<MigrationServiceConfigSchema>.GetConfig<bool>("ReportInitial"))
					{
						this.SetBatchFlags(MigrationBatchFlags.ReportInitial, true);
						this.WriteExtendedPropertiesToMessageItem(migrationStoreObject);
					}
				}
				migrationStoreObject.Save(SaveMode.ResolveConflicts);
			}
			if (flags.HasFlag(MigrationFlags.Start))
			{
				this.StartTime = new ExDateTime?(utcNow);
			}
			this.Flags = flags;
			this.PoisonCount = 0;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x000125E4 File Offset: 0x000107E4
		public void SetStatus(IMigrationDataProvider provider, MigrationJobStatus status, MigrationState state, MigrationFlags? flags = null, MigrationStage? stage = null, TimeSpan? delayTime = null, LocalizedException exception = null, string lastProcessedRow = null, string batchInputId = null, MigrationCountCache.MigrationStatusChange statusChanges = null, bool clearPoison = true, MigrationBatchFlags? batchFlags = null, TimeSpan? processingDuration = null)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.AssertOrThrow(this.IsPAW, "we should only be running with a PAW job here!", new object[0]);
			string internalError = null;
			if (exception != null)
			{
				internalError = MigrationLogger.GetDiagnosticInfo(exception, null);
			}
			MigrationLogger.Log(MigrationEventType.Information, "MigrationJob.SetStatus: job {0}, status {1}", new object[]
			{
				this,
				status
			});
			MigrationStatusData<MigrationJobStatus> migrationStatusData = new MigrationStatusData<MigrationJobStatus>(this.StatusData);
			if (state == MigrationState.Failed || state == MigrationState.Corrupted)
			{
				migrationStatusData.SetFailedStatus(status, exception, internalError, new MigrationState?(state));
			}
			else if (exception != null)
			{
				migrationStatusData.SetTransientError(exception, new MigrationJobStatus?(status), new MigrationState?(state));
			}
			else
			{
				migrationStatusData.UpdateStatus(status, new MigrationState?(state));
			}
			MigrationCountCache migrationCountCache = null;
			bool flag = false;
			if (statusChanges != null)
			{
				migrationCountCache = this.cachedItemCounts.Clone();
				migrationCountCache.ApplyStatusChange(statusChanges);
				if (!migrationCountCache.IsValid)
				{
					migrationCountCache = null;
					flag = true;
					MigrationLogger.Log(MigrationEventType.Error, "MigrationJob.SetStatus: job {0}, count cache invalid!", new object[]
					{
						this
					});
				}
			}
			PropertyDefinition[] properties = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
			{
				MigrationPersistableBase.MigrationBaseDefinitions,
				MigrationStatusData<MigrationJobStatus>.StatusPropertyDefinition,
				MigrationJob.JobCountCacheDefinition,
				new PropertyDefinition[]
				{
					MigrationBatchMessageSchema.MigrationFlags,
					MigrationBatchMessageSchema.MigrationStage,
					MigrationBatchMessageSchema.MigrationNextProcessTime,
					MigrationBatchMessageSchema.MigrationJobCursorPosition,
					MigrationBatchMessageSchema.MigrationJobPoisonCount
				}
			});
			ExDateTime? exDateTime = null;
			if (delayTime != null)
			{
				exDateTime = new ExDateTime?(ExDateTime.UtcNow + delayTime.Value);
			}
			else
			{
				exDateTime = new ExDateTime?(ExDateTime.UtcNow);
			}
			if (batchFlags != null)
			{
				this.BatchFlags = batchFlags.Value;
			}
			using (IMigrationStoreObject migrationStoreObject = this.FindStoreObject(provider, base.StoreObjectId, properties))
			{
				migrationStoreObject.OpenAsReadWrite();
				migrationStatusData.WriteToMessageItem(migrationStoreObject, true);
				if (flags != null)
				{
					migrationStoreObject[MigrationBatchMessageSchema.MigrationFlags] = flags;
				}
				if (stage != null)
				{
					migrationStoreObject[MigrationBatchMessageSchema.MigrationStage] = stage.Value;
				}
				bool flag2 = string.Equals(lastProcessedRow, "EOF", StringComparison.OrdinalIgnoreCase);
				if (flag2)
				{
					this.BatchInputId = null;
				}
				if (batchInputId != null)
				{
					this.BatchInputId = batchInputId;
				}
				if (processingDuration != null)
				{
					this.ProcessingDuration += processingDuration.Value;
				}
				if (lastProcessedRow != null)
				{
					migrationStoreObject[MigrationBatchMessageSchema.MigrationJobCursorPosition] = lastProcessedRow;
				}
				if (exDateTime != null)
				{
					MigrationHelperBase.SetExDateTimeProperty(migrationStoreObject, MigrationBatchMessageSchema.MigrationNextProcessTime, new ExDateTime?(exDateTime.Value));
				}
				if (flag)
				{
					MigrationHelperBase.SetExDateTimeProperty(migrationStoreObject, MigrationBatchMessageSchema.MigrationJobCountCacheFullScanTime, null);
				}
				if (migrationCountCache != null)
				{
					migrationStoreObject[MigrationBatchMessageSchema.MigrationJobCountCache] = migrationCountCache.Serialize();
				}
				if (clearPoison)
				{
					migrationStoreObject[MigrationBatchMessageSchema.MigrationJobPoisonCount] = 0;
				}
				if (flag2 || batchInputId != null || batchFlags != null || processingDuration != null)
				{
					this.WriteExtendedPropertiesToMessageItem(migrationStoreObject);
				}
				migrationStoreObject.Save(SaveMode.NoConflictResolution);
			}
			this.StatusData = migrationStatusData;
			if (flags != null)
			{
				this.Flags = flags.Value;
			}
			if (stage != null)
			{
				this.Stage = stage.Value;
			}
			if (exDateTime != null)
			{
				this.NextProcessTime = new ExDateTime?(exDateTime.Value);
			}
			if (lastProcessedRow != null)
			{
				this.LastCursorPosition = lastProcessedRow;
			}
			if (flag)
			{
				this.FullScanTime = null;
			}
			if (migrationCountCache != null)
			{
				this.cachedItemCounts = migrationCountCache;
			}
			if (clearPoison)
			{
				this.PoisonCount = 0;
			}
			if (migrationStatusData.Status == MigrationJobStatus.Removed)
			{
				MigrationServiceFactory.Instance.GetAsyncNotificationAdapter().RemoveNotification(provider, this);
			}
			else
			{
				MigrationServiceFactory.Instance.GetAsyncNotificationAdapter().UpdateNotification(provider, this);
			}
			if (state == MigrationState.Failed || state == MigrationState.Corrupted)
			{
				base.ReportData.Append(Strings.UnknownMigrationBatchError, exception, ReportEntryFlags.Failure | ReportEntryFlags.Fatal | ReportEntryFlags.Target);
				provider.FlushReport(base.ReportData);
			}
			this.LogStatusEvent();
			if (exception != null)
			{
				MigrationFailureFlags failureFlags;
				switch (state)
				{
				case MigrationState.Failed:
					failureFlags = MigrationFailureFlags.Fatal;
					break;
				case MigrationState.Corrupted:
					failureFlags = MigrationFailureFlags.Corruption;
					break;
				default:
					failureFlags = MigrationFailureFlags.None;
					break;
				}
				MigrationFailureLog.LogFailureEvent(this, exception, failureFlags, null);
			}
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00012A0C File Offset: 0x00010C0C
		public void SetNextProcessTime(IMigrationDataProvider provider, ExDateTime nextProcessTime)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationLogger.Log(MigrationEventType.Verbose, "MigrationJob.SetNextProcessTime: job {0} time {1}", new object[]
			{
				this,
				nextProcessTime
			});
			using (IMigrationStoreObject migrationStoreObject = this.FindStoreObject(provider, base.StoreObjectId, new StorePropertyDefinition[]
			{
				MigrationBatchMessageSchema.MigrationNextProcessTime
			}))
			{
				migrationStoreObject.OpenAsReadWrite();
				MigrationHelperBase.SetExDateTimeProperty(migrationStoreObject, MigrationBatchMessageSchema.MigrationNextProcessTime, new ExDateTime?(nextProcessTime));
				migrationStoreObject.Save(SaveMode.ResolveConflicts);
			}
			this.NextProcessTime = new ExDateTime?(nextProcessTime);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00012AA8 File Offset: 0x00010CA8
		public void UpdateInitialSyncProperties(IMigrationDataProvider provider, TimeSpan initialSyncDuration)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationLogger.Log(MigrationEventType.Verbose, "MigrationJob.UpdateInitialSyncProperties: job {0}", new object[]
			{
				this
			});
			this.InitialSyncDateTime = new ExDateTime?(ExDateTime.UtcNow);
			this.InitialSyncDuration = new TimeSpan?(initialSyncDuration);
			using (IMigrationStoreObject migrationStoreObject = this.FindStoreObject(provider, base.StoreObjectId, MigrationPersistableBase.MigrationBaseDefinitions))
			{
				migrationStoreObject.OpenAsReadWrite();
				this.WriteExtendedPropertiesToMessageItem(migrationStoreObject);
				migrationStoreObject.Save(SaveMode.ResolveConflicts);
			}
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00012B38 File Offset: 0x00010D38
		public void SetJobStatus(IMigrationDataProvider provider, MigrationJobStatus jobStatus)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationLogger.Log(MigrationEventType.Information, "MigrationJob.SetJobStatus: job {0}, status {1}", new object[]
			{
				this,
				jobStatus
			});
			MigrationStatusData<MigrationJobStatus> migrationStatusData = new MigrationStatusData<MigrationJobStatus>(this.StatusData);
			migrationStatusData.UpdateStatus(jobStatus, null);
			this.SetStatusData(provider, migrationStatusData, true);
			this.LogStatusEvent();
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00012B9C File Offset: 0x00010D9C
		public void SetFailedStatus(IMigrationDataProvider provider, Exception exception)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.ThrowOnNullArgument(exception, "exception");
			string diagnosticInfo = MigrationLogger.GetDiagnosticInfo(exception, null);
			MigrationLogger.Log(MigrationEventType.Error, "MigrationJob.SetFailedStatus: job {0}, {1}", new object[]
			{
				this,
				diagnosticInfo
			});
			MigrationStatusData<MigrationJobStatus> migrationStatusData = new MigrationStatusData<MigrationJobStatus>(this.StatusData);
			migrationStatusData.SetFailedStatus((exception is MigrationDataCorruptionException) ? MigrationJobStatus.Corrupted : MigrationJobStatus.Failed, exception, diagnosticInfo, null);
			this.SetStatusData(provider, migrationStatusData, false);
			base.ReportData.Append(Strings.UnknownMigrationBatchError, exception, ReportEntryFlags.Failure | ReportEntryFlags.Fatal | ReportEntryFlags.Target);
			provider.FlushReport(base.ReportData);
			this.LogStatusEvent();
			MigrationFailureFlags migrationFailureFlags = (exception is MigrationDataCorruptionException) ? MigrationFailureFlags.Corruption : MigrationFailureFlags.None;
			MigrationFailureLog.LogFailureEvent(this, exception, MigrationFailureFlags.Fatal | migrationFailureFlags, null);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00012C58 File Offset: 0x00010E58
		public void SetTransientError(IMigrationDataProvider provider, Exception exception)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.ThrowOnNullArgument(exception, "exception");
			string diagnosticInfo = MigrationLogger.GetDiagnosticInfo(exception, null);
			MigrationLogger.Log(MigrationEventType.Error, "MigrationJob.SetTransientError: job {0}, {1}", new object[]
			{
				this,
				diagnosticInfo
			});
			MigrationStatusData<MigrationJobStatus> migrationStatusData = new MigrationStatusData<MigrationJobStatus>(this.StatusData);
			migrationStatusData.SetTransientError(exception, null, null);
			this.SetStatusData(provider, migrationStatusData, false);
			base.ReportData.Append(Strings.MigrationReportJobTransientError, exception, ReportEntryFlags.Failure | ReportEntryFlags.Target);
			provider.FlushReport(base.ReportData);
			MigrationFailureLog.LogFailureEvent(this, exception, MigrationFailureFlags.None, null);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00012CF8 File Offset: 0x00010EF8
		public void StopJob(IMigrationDataProvider provider, IMigrationConfig config, JobCancellationStatus jobCancellationReason)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			if (jobCancellationReason == JobCancellationStatus.NotCancelled)
			{
				throw new ArgumentException(string.Format("The job cancellation reason specified cannot be {0}", jobCancellationReason));
			}
			MigrationJob.CheckOperationIsSupported(new MigrationJob.SupportsActionDelegate(this.SupportsStopping), Strings.MigrationJobCannotBeStopped);
			base.CheckVersion();
			MigrationLogger.Log(MigrationEventType.Warning, "MigrationJob.CancelJob: job {0}", new object[]
			{
				this
			});
			using (IMigrationStoreObject migrationStoreObject = this.FindStoreObject(provider, base.StoreObjectId, MigrationJob.JobCancelPropertyDefinition))
			{
				migrationStoreObject.OpenAsReadWrite();
				migrationStoreObject[MigrationBatchMessageSchema.MigrationJobCancelledFlag] = true;
				migrationStoreObject[MigrationBatchMessageSchema.MigrationJobCancellationReason] = jobCancellationReason;
				this.WriteExtendedPropertiesToMessageItem(migrationStoreObject);
				migrationStoreObject.Save(SaveMode.NoConflictResolution);
			}
			this.JobCancellationStatus = jobCancellationReason;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00012DC8 File Offset: 0x00010FC8
		public void StartJob(IMigrationDataProvider provider, MultiValuedProperty<SmtpAddress> emails, MigrationBatchFlags batchFlags, TimeSpan? incrementalSyncInterval)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationJob.CheckOperationIsSupported(new MigrationJob.SupportsActionDelegate(this.SupportsStarting), Strings.CompletedMigrationJobCannotBeStarted);
			if (this.MigrationType == MigrationType.PublicFolder && !provider.ADProvider.CheckPublicFoldersLockedForMigration())
			{
				throw new PublicFolderMailboxesNotProvisionedForMigrationException();
			}
			base.CheckVersion();
			MigrationLogger.Log(MigrationEventType.Warning, "MigrationJob.StartJob: job {0}", new object[]
			{
				this
			});
			MigrationStatusData<MigrationJobStatus> migrationStatusData = new MigrationStatusData<MigrationJobStatus>(this.StatusData);
			migrationStatusData.UpdateStatus(MigrationJobStatus.SyncInitializing, null);
			this.BatchFlags = batchFlags;
			this.IncrementalSyncInterval = incrementalSyncInterval;
			this.AutoRunCount = 0;
			ExDateTime utcNow = ExDateTime.UtcNow;
			bool shouldProcessDataRows = this.ShouldProcessDataRows;
			string value = "";
			using (IMigrationMessageItem migrationMessageItem = base.FindMessageItem(provider, MigrationJob.JobStartPropertyDefinition))
			{
				migrationMessageItem.OpenAsReadWrite();
				migrationStatusData.WriteToMessageItem(migrationMessageItem, true);
				if (emails != null)
				{
					MigrationJob.SaveNotificationEmails(migrationMessageItem, emails);
				}
				MigrationHelperBase.SetExDateTimeProperty(migrationMessageItem, MigrationBatchMessageSchema.MigrationJobStartTime, new ExDateTime?(utcNow));
				MigrationHelperBase.SetExDateTimeProperty(migrationMessageItem, MigrationBatchMessageSchema.MigrationJobLastRestartTime, new ExDateTime?(utcNow));
				MigrationHelperBase.SetExDateTimeProperty(migrationMessageItem, MigrationBatchMessageSchema.MigrationNextProcessTime, new ExDateTime?(utcNow));
				if (this.AutoComplete)
				{
					MigrationHelperBase.SetExDateTimeProperty(migrationMessageItem, MigrationBatchMessageSchema.MigrationJobFinalizeTime, new ExDateTime?(utcNow));
				}
				migrationMessageItem[MigrationBatchMessageSchema.MigrationJobCancelledFlag] = false;
				migrationMessageItem[MigrationBatchMessageSchema.MigrationJobCancellationReason] = 0;
				if (shouldProcessDataRows)
				{
					migrationMessageItem[MigrationBatchMessageSchema.MigrationJobCursorPosition] = value;
					this.CreateValidationWarningAttachment(migrationMessageItem, null, true);
				}
				migrationMessageItem[MigrationBatchMessageSchema.MigrationJobPoisonCount] = 0;
				this.WriteExtendedPropertiesToMessageItem(migrationMessageItem);
				migrationMessageItem.Save(SaveMode.NoConflictResolution);
			}
			this.JobCancellationStatus = JobCancellationStatus.NotCancelled;
			this.StatusData = migrationStatusData;
			this.StartTime = new ExDateTime?(utcNow);
			this.LastRestartTime = new ExDateTime?(utcNow);
			this.NextProcessTime = new ExDateTime?(utcNow);
			if (this.AutoComplete)
			{
				this.FinalizeTime = new ExDateTime?(utcNow);
			}
			this.NotificationEmails = emails;
			this.PoisonCount = 0;
			if (shouldProcessDataRows)
			{
				this.LastCursorPosition = value;
			}
			MigrationServiceFactory.Instance.GetAsyncNotificationAdapter().UpdateNotification(provider, this);
			this.LogStatusEvent();
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00012FEC File Offset: 0x000111EC
		public void SetReportUrls(IMigrationDataProvider provider, MigrationReportSet reportSet)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			this.Reports.Add(reportSet);
			base.CheckVersion();
			using (IMigrationStoreObject migrationStoreObject = this.FindStoreObject(provider, base.StoreObjectId, MigrationJob.JobSetReportIDDefinition))
			{
				migrationStoreObject.OpenAsReadWrite();
				this.SaveReports(migrationStoreObject, this.Reports, true);
				migrationStoreObject.Save(SaveMode.NoConflictResolution);
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00013060 File Offset: 0x00011260
		public void FinalizeJob(IMigrationDataProvider provider, MultiValuedProperty<SmtpAddress> emails)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			LocalizedString? localizedString;
			if (!this.SupportsCompleting(out localizedString) && (this.Status != MigrationJobStatus.Failed || this.FinalizeTime == null))
			{
				if (localizedString == null)
				{
					MigrationLogger.Log(MigrationEventType.Information, "Cannot complete batch '{0}' but SupportCompleting didn't return a reason.", new object[]
					{
						this
					});
					localizedString = new LocalizedString?(Strings.MigrationJobCannotBeCompleted);
				}
				throw new MigrationPermanentException(localizedString.Value);
			}
			MigrationLogger.Log(MigrationEventType.Warning, "MigrationJob.FinalizeJob: job {0}", new object[]
			{
				this
			});
			this.AutoRunCount = 0;
			int num = 0;
			ExDateTime utcNow = ExDateTime.UtcNow;
			MigrationStatusData<MigrationJobStatus> migrationStatusData = new MigrationStatusData<MigrationJobStatus>(this.StatusData);
			if (this.MigrationType == MigrationType.PublicFolder && this.StatusData.Status == MigrationJobStatus.Completed)
			{
				migrationStatusData.UpdateStatus(MigrationJobStatus.CompletionStarting, null);
			}
			else
			{
				migrationStatusData.UpdateStatus(MigrationJobStatus.CompletionInitializing, null);
			}
			using (IMigrationStoreObject migrationStoreObject = this.FindStoreObject(provider, base.StoreObjectId, MigrationJob.JobFinalizePropertyDefinition))
			{
				migrationStoreObject.OpenAsReadWrite();
				migrationStatusData.WriteToMessageItem(migrationStoreObject, true);
				if (this.MigrationType == MigrationType.PublicFolder)
				{
					num = migrationStoreObject.GetValueOrDefault<int>(MigrationBatchMessageSchema.MigrationJobLastFinalizationAttempt, 0) + 1;
					migrationStoreObject[MigrationBatchMessageSchema.MigrationJobLastFinalizationAttempt] = num;
				}
				MigrationHelperBase.SetExDateTimeProperty(migrationStoreObject, MigrationBatchMessageSchema.MigrationJobFinalizeTime, new ExDateTime?(utcNow));
				MigrationHelperBase.SetExDateTimeProperty(migrationStoreObject, MigrationBatchMessageSchema.MigrationJobItemStateLastUpdated, new ExDateTime?(utcNow));
				MigrationHelperBase.SetExDateTimeProperty(migrationStoreObject, MigrationBatchMessageSchema.MigrationJobLastRestartTime, new ExDateTime?(utcNow));
				if (emails != null)
				{
					MigrationJob.SaveNotificationEmails(migrationStoreObject, emails);
				}
				migrationStoreObject[MigrationBatchMessageSchema.MigrationJobCancelledFlag] = false;
				migrationStoreObject[MigrationBatchMessageSchema.MigrationJobCancellationReason] = 0;
				migrationStoreObject[MigrationBatchMessageSchema.MigrationJobPoisonCount] = 0;
				this.WriteExtendedPropertiesToMessageItem(migrationStoreObject);
				migrationStoreObject.Save(SaveMode.NoConflictResolution);
			}
			this.StatusData = migrationStatusData;
			this.FinalizeTime = new ExDateTime?(utcNow);
			this.LastFinalizationAttempt = num;
			this.LastRestartTime = new ExDateTime?(utcNow);
			if (emails != null)
			{
				this.NotificationEmails = emails;
			}
			MigrationServiceFactory.Instance.GetAsyncNotificationAdapter().UpdateNotification(provider, this);
			this.LogStatusEvent();
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00013294 File Offset: 0x00011494
		public void UpdateCachedItemCounts(IMigrationDataProvider provider)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationLogger.Log(MigrationEventType.Verbose, "MigrationJob.UpdateCachedItemCounts: job {0}", new object[]
			{
				this
			});
			ExDateTime utcNow = ExDateTime.UtcNow;
			MigrationCountCache migrationCountCache = new MigrationCountCache();
			int num = this.TotalRowCount;
			MigrationUserStatus[] array;
			if (!this.IsPAW)
			{
				num -= this.RemovedItemCount;
				array = MigrationJob.AllJobItemsStatuses;
				int provisionedCount = MigrationJobItem.GetProvisionedCount(provider, this.JobId);
				migrationCountCache.SetCachedOtherCount("Provisioned", provisionedCount);
			}
			else
			{
				array = MigrationJob.AllUsedPAWJobItemStatuses;
			}
			foreach (MigrationUserStatus migrationUserStatus in array)
			{
				int itemCount = this.GetItemCount(provider, new MigrationUserStatus[]
				{
					migrationUserStatus
				});
				migrationCountCache.SetCachedStatusCount(migrationUserStatus, itemCount);
			}
			ExDateTime? oldestLastSyncSubscriptionTime = MigrationJobItem.GetOldestLastSyncSubscriptionTime(provider, this.MigrationType, this.JobId);
			migrationCountCache.SetCachedTimestamp("LastSync", oldestLastSyncSubscriptionTime);
			this.SetCountCache(provider, new ExDateTime?(utcNow), migrationCountCache, new int?(num));
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00013388 File Offset: 0x00011588
		public void IncrementRemovedUserCount(IMigrationDataProvider provider)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationLogger.Log(MigrationEventType.Verbose, "MigrationJob.UpdateCachedItemCounts: job {0}", new object[]
			{
				this
			});
			MigrationCountCache migrationCountCache = this.cachedItemCounts.Clone();
			migrationCountCache.IncrementCachedOtherCount("Removed", 1);
			this.SetCountCache(provider, null, migrationCountCache, null);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x000133EC File Offset: 0x000115EC
		public void SetCountCache(IMigrationDataProvider provider, ExDateTime? modifiedTime, MigrationCountCache newCountCache, int? totalCount)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			base.CheckVersion();
			using (IMigrationStoreObject migrationStoreObject = this.FindStoreObject(provider, base.StoreObjectId, MigrationJob.JobCountCacheDefinition))
			{
				migrationStoreObject.OpenAsReadWrite();
				migrationStoreObject[MigrationBatchMessageSchema.MigrationJobCountCache] = newCountCache.Serialize();
				MigrationHelperBase.SetExDateTimeProperty(migrationStoreObject, MigrationBatchMessageSchema.MigrationJobCountCacheFullScanTime, modifiedTime);
				if (totalCount != null)
				{
					migrationStoreObject[MigrationBatchMessageSchema.MigrationJobTotalRowCount] = totalCount.Value;
				}
				migrationStoreObject.Save(SaveMode.NoConflictResolution);
			}
			this.cachedItemCounts = newCountCache;
			this.FullScanTime = modifiedTime;
			if (totalCount != null)
			{
				this.TotalRowCount = totalCount.Value;
			}
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x000134A8 File Offset: 0x000116A8
		public void SaveBatchFlagsAndNotificationId(IMigrationDataProvider provider)
		{
			this.SaveExtendedProperties(provider);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x000134B4 File Offset: 0x000116B4
		public void RemoveJob(IMigrationDataProvider provider)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationJob.CheckOperationIsSupported(new MigrationJob.SupportsActionDelegate(this.SupportsRemoving), Strings.MigrationJobCannotBeRemoved);
			MigrationLogger.Log(MigrationEventType.Information, "MigrationJob.RemoveJob: job {0}", new object[]
			{
				this
			});
			MigrationStatusData<MigrationJobStatus> migrationStatusData = new MigrationStatusData<MigrationJobStatus>(this.StatusData);
			migrationStatusData.UpdateStatus(MigrationJobStatus.Removing, null);
			using (IMigrationStoreObject migrationStoreObject = this.FindStoreObject(provider, base.StoreObjectId, this.PropertyDefinitions))
			{
				migrationStoreObject.OpenAsReadWrite();
				migrationStatusData.WriteToMessageItem(migrationStoreObject, true);
				migrationStoreObject[MigrationBatchMessageSchema.MigrationJobPoisonCount] = 0;
				migrationStoreObject.Save(SaveMode.NoConflictResolution);
			}
			this.PoisonCount = 0;
			this.StatusData = migrationStatusData;
			MigrationServiceFactory.Instance.GetAsyncNotificationAdapter().RemoveNotification(provider, this);
			this.LogStatusEvent();
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00013594 File Offset: 0x00011794
		public void UpdateJob(IMigrationDataProvider provider, bool updateEmails, MigrationBatch batch)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.ThrowOnNullArgument(batch, "batch");
			MigrationJob.CheckOperationIsSupported(new MigrationJob.SupportsActionDelegate(this.SupportsSetting), Strings.CompletedMigrationJobCannotBeModified);
			MigrationLogger.Log(MigrationEventType.Information, "MigrationJob.Update: overriding batch flags {0} with {1}", new object[]
			{
				this.BatchFlags,
				batch.BatchFlags
			});
			if (batch.ReportInterval != null)
			{
				this.ReportInterval = batch.ReportInterval.Value;
			}
			IJobSubscriptionSettings jobSubscriptionSettings = JobSubscriptionSettingsBase.CreateFromBatch(batch, this.IsPAW);
			this.BatchFlags = batch.BatchFlags;
			this.MaxAutoRunCount = batch.AutoRetryCount;
			this.AllowUnknownColumnsInCsv = batch.AllowUnknownColumnsInCsv;
			MultiValuedProperty<SmtpAddress> multiValuedProperty = this.NotificationEmails;
			using (IMigrationMessageItem migrationMessageItem = base.FindMessageItem(provider, this.PropertyDefinitions))
			{
				migrationMessageItem.OpenAsReadWrite();
				if (updateEmails)
				{
					multiValuedProperty = batch.NotificationEmails;
					MigrationJob.SaveNotificationEmails(migrationMessageItem, multiValuedProperty);
				}
				if (batch.CsvStream != null)
				{
					batch.CsvStream.Seek(0L, SeekOrigin.Begin);
					MigrationLogger.Log(MigrationEventType.Information, "MigrationJob.Update: job {0}, overriding CSV attachment", new object[]
					{
						this
					});
					int validationWarningCount = this.ValidationWarningCount;
					this.CreateValidationWarningAttachment(migrationMessageItem, batch.ValidationWarnings, true);
					MigrationLogger.Log(MigrationEventType.Verbose, "MigrationJob.Update: job {0}, wrote {1} warnings (removed {2} warnings)", new object[]
					{
						this,
						this.ValidationWarningCount,
						validationWarningCount
					});
					using (IMigrationAttachment migrationAttachment = migrationMessageItem.CreateAttachment("Request.csv"))
					{
						this.SaveCsvStream(migrationMessageItem, migrationAttachment, batch.CsvStream);
					}
					this.TotalRowCount = this.TotalRowCount + batch.TotalCount - validationWarningCount;
					migrationMessageItem[MigrationBatchMessageSchema.MigrationJobTotalRowCount] = this.TotalRowCount;
				}
				migrationMessageItem[MigrationBatchMessageSchema.MigrationJobPoisonCount] = 0;
				if (jobSubscriptionSettings != null)
				{
					jobSubscriptionSettings.WriteExtendedProperties(base.ExtendedProperties);
					jobSubscriptionSettings.WriteToMessageItem(migrationMessageItem, true);
				}
				this.WriteExtendedPropertiesToMessageItem(migrationMessageItem);
				migrationMessageItem.Save(SaveMode.NoConflictResolution);
			}
			this.SubscriptionSettings = jobSubscriptionSettings;
			this.PoisonCount = 0;
			this.NotificationEmails = multiValuedProperty;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00013830 File Offset: 0x00011A30
		public void Delete(IMigrationDataProvider provider, bool force)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.AssertOrThrow(base.StoreObjectId != null, "Should only try to delete an item that's been persisted", new object[0]);
			if (!force && !this.IsPAW)
			{
				int count = MigrationJobItem.GetCount(provider, this.JobId, new MigrationUserStatus[0]);
				if (count > 0)
				{
					string format = string.Format("MigrationJob.Delete: job {0} still has items", this);
					MigrationLogger.Log(MigrationEventType.Error, format, new object[0]);
					throw new MigrationJobCannotBeDeletedWithPendingItemsException(count);
				}
			}
			this.StatusData.UpdateStatus(MigrationJobStatus.Removed, null);
			this.LogStatusEvent();
			MigrationLogger.Log(MigrationEventType.Warning, "MigrationJob.Delete: job {0}, force {1}", new object[]
			{
				this,
				force
			});
			provider.RemoveMessage(base.StoreObjectId);
			MigrationServiceFactory.Instance.GetAsyncNotificationAdapter().RemoveNotification(provider, this);
			CommonUtils.CatchKnownExceptions(delegate
			{
				provider.DeleteReport(this.ReportData);
			}, delegate(Exception exception)
			{
				MigrationLogger.Log(MigrationEventType.Warning, exception, "Failed to remove report for job '{0}'.", new object[]
				{
					this.JobId
				});
			});
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00013949 File Offset: 0x00011B49
		public bool IsDataRowProcessingDone()
		{
			return string.Equals(this.LastCursorPosition, "EOF", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001395C File Offset: 0x00011B5C
		public void SetDataRowProcessingDone(IMigrationDataProvider provider, ICollection<MigrationBatchError> warnings, int updatesEncountered)
		{
			this.UpdateLastProcessedRow(provider, "EOF", warnings, updatesEncountered);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001396C File Offset: 0x00011B6C
		public void UpdateLastProcessedRow(IMigrationDataProvider provider, string rowIndex, ICollection<MigrationBatchError> warnings, int updatesEncountered)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			int num = this.TotalRowCount - updatesEncountered;
			base.CheckVersion();
			using (IMigrationMessageItem migrationMessageItem = base.FindMessageItem(provider, this.PropertyDefinitions))
			{
				migrationMessageItem.OpenAsReadWrite();
				bool flag = string.Equals(rowIndex, "EOF", StringComparison.OrdinalIgnoreCase);
				bool flag2 = warnings != null && warnings.Count > 0;
				if (flag)
				{
					this.BatchInputId = null;
				}
				if (flag2)
				{
					IMigrationAttachment migrationAttachment = null;
					if (migrationMessageItem.TryGetAttachment("Errors.csv", PropertyOpenMode.Modify, out migrationAttachment))
					{
						using (migrationAttachment)
						{
							long num2 = migrationAttachment.Stream.Seek(0L, SeekOrigin.End);
							using (StreamWriter streamWriter = new StreamWriter(migrationAttachment.Stream))
							{
								if (num2 != 0L)
								{
									MigrationErrorCsvSchema.WriteErrors(streamWriter, warnings);
								}
								else
								{
									MigrationErrorCsvSchema.WriteHeaderAndErrors(streamWriter, warnings);
								}
								streamWriter.Flush();
							}
							migrationAttachment.Save(null);
						}
						this.ValidationWarningCount += warnings.Count;
					}
					else
					{
						this.CreateValidationWarningAttachment(migrationMessageItem, warnings, false);
						this.ValidationWarningCount = warnings.Count;
					}
				}
				if (rowIndex != null)
				{
					migrationMessageItem[MigrationBatchMessageSchema.MigrationJobCursorPosition] = rowIndex;
				}
				migrationMessageItem[MigrationBatchMessageSchema.MigrationJobTotalRowCount] = num;
				if (flag || flag2)
				{
					this.WriteExtendedPropertiesToMessageItem(migrationMessageItem);
				}
				migrationMessageItem.Save(SaveMode.NoConflictResolution);
			}
			this.TotalRowCount = num;
			if (rowIndex != null)
			{
				this.LastCursorPosition = rowIndex;
			}
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00013B18 File Offset: 0x00011D18
		public void UpdatePoisonCount(IMigrationDataProvider provider, int count)
		{
			using (IMigrationStoreObject migrationStoreObject = this.FindStoreObject(provider, base.StoreObjectId, this.PropertyDefinitions))
			{
				migrationStoreObject.OpenAsReadWrite();
				migrationStoreObject[MigrationBatchMessageSchema.MigrationJobPoisonCount] = count;
				migrationStoreObject.Save(SaveMode.NoConflictResolution);
				this.PoisonCount = count;
			}
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00013B7C File Offset: 0x00011D7C
		public bool TryAutoRetryStartedJob(IMigrationDataProvider provider)
		{
			return this.TryAutoRetryJob(provider, this.ReportSyncCompleteFailedItemCount);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00013B8B File Offset: 0x00011D8B
		public bool TryAutoRetryCompletedJob(IMigrationDataProvider provider)
		{
			return this.TryAutoRetryJob(provider, this.ReportCompleteFailedItemCount);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00013B9C File Offset: 0x00011D9C
		public IEnumerable<MigrationBatchError> GetValidationWarnings(IMigrationDataProvider provider)
		{
			IEnumerable<MigrationBatchError> result;
			try
			{
				using (IMigrationMessageItem migrationMessageItem = base.FindMessageItem(provider, this.PropertyDefinitions))
				{
					IMigrationAttachment migrationAttachment = null;
					if (migrationMessageItem.TryGetAttachment("Errors.csv", PropertyOpenMode.ReadOnly, out migrationAttachment))
					{
						MigrationUtil.AssertOrThrow(migrationAttachment != null, "attachment shouldn't be null if TryGetAttachment returns true", new object[0]);
						using (migrationAttachment)
						{
							return new List<MigrationBatchError>(MigrationErrorCsvSchema.ReadErrors(migrationAttachment.Stream));
						}
					}
					result = new List<MigrationBatchError>(0);
				}
			}
			catch (CsvValidationException ex)
			{
				result = new MigrationBatchError[]
				{
					new MigrationBatchError
					{
						EmailAddress = "ValidationWarningsAttachment",
						LocalizedErrorMessage = ex.LocalizedString
					}
				};
			}
			return result;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00013C74 File Offset: 0x00011E74
		public override XElement GetDiagnosticInfo(IMigrationDataProvider dataProvider, MigrationDiagnosticArgument argument)
		{
			XElement xelement = new XElement("MigrationJob", new object[]
			{
				new XAttribute("name", this.JobName),
				new XElement("migrationJobType", this.MigrationType),
				new XElement("id", this.JobId)
			});
			if (this.StatusData != null)
			{
				xelement.Add(this.StatusData.GetDiagnosticInfo(dataProvider, argument));
			}
			if (this.IsPAW)
			{
				xelement.Add(new object[]
				{
					new XElement("flags", this.Flags),
					new XElement("stage", this.Stage),
					new XElement("nextProcessTime", this.NextProcessTime ?? ExDateTime.MinValue),
					new XElement("workflow", this.Workflow.Serialize(false))
				});
			}
			xelement.Add(new object[]
			{
				new XElement("messageId", base.StoreObjectId),
				new XElement("cancelled", this.IsCancelled),
				new XElement("JobCancellationStatus", this.JobCancellationStatus),
				new XElement("orginallyCreated", this.OriginalCreationTime),
				new XElement("started", this.StartTime),
				new XElement("restarted", this.LastRestartTime),
				new XElement("finalized", this.FinalizeTime),
				new XElement("isStaged", this.IsStaged),
				new XElement("poisonCount", this.PoisonCount),
				new XElement("batchFlags", this.BatchFlags),
				new XElement("totalRowCount", this.TotalRowCount),
				new XElement("cachedCounts", this.cachedItemCounts.GetDiagnosticInfo(dataProvider, argument)),
				new XElement("fullScanTime", this.FullScanTime),
				new XElement("lastProcessedRowIndex", this.LastCursorPosition),
				new XElement("submittedBy", this.SubmittedByUser),
				new XElement("timeZone", this.UserTimeZone),
				new XElement("adminCulture", this.AdminCulture),
				new XElement("notificationEmails", this.NotificationEmails),
				new XElement("direction", this.JobDirection),
				new XElement("targetDomainName", this.TargetDomainName),
				new XElement("skipSteps", this.SkipSteps)
			});
			if (this.SourceEndpoint != null)
			{
				xelement.Add(new XElement("SourceEndpoint", this.SourceEndpoint.GetDiagnosticInfo(dataProvider, argument)));
			}
			if (this.TargetEndpoint != null)
			{
				xelement.Add(new XElement("TargetEndpoint", this.TargetEndpoint.GetDiagnosticInfo(dataProvider, argument)));
			}
			if (this.SubscriptionSettings != null)
			{
				xelement.Add(new XElement("SubscriptionSettings", this.SubscriptionSettings.GetDiagnosticInfo(dataProvider, argument)));
			}
			if (this.MigrationType == MigrationType.PublicFolder)
			{
				xelement.Add(new XElement("LastFinalizationAttempt", this.LastFinalizationAttempt));
			}
			if (!this.IsPAW && dataProvider != null && argument.HasArgument("verbose"))
			{
				XElement xelement2 = new XElement("ValidationWarnings", new XElement("count", this.ValidationWarningCount));
				foreach (MigrationBatchError content in this.GetValidationWarnings(dataProvider))
				{
					xelement2.Add(new XElement("ValidationWarning", content));
				}
				xelement.Add(xelement2);
			}
			if (argument.HasArgument("reports"))
			{
				using (IMigrationDataProvider providerForFolder = dataProvider.GetProviderForFolder(MigrationFolderName.SyncMigrationReports))
				{
					foreach (MigrationReportItem migrationReportItem in MigrationReportItem.GetByJobId(providerForFolder, new Guid?(this.JobId), 50))
					{
						xelement.Add(migrationReportItem.GetDiagnosticInfo(providerForFolder, argument));
					}
				}
			}
			base.GetDiagnosticInfo(dataProvider, argument, xelement);
			return xelement;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00014208 File Offset: 0x00012408
		public override string ToString()
		{
			if (this.IsPAW)
			{
				return string.Format("{0} ({1}) {2}:{3}:{4}:{5} {6}", new object[]
				{
					this.JobName,
					this.JobId,
					this.MigrationType,
					this.IsStaged ? "Staged" : "Cutover",
					base.Version,
					this.SubmittedByUser,
					this.Stage
				});
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}:{1}:{2}:{3}:{4}:{5}:{6}", new object[]
			{
				this.JobName,
				this.JobId,
				this.MigrationType,
				this.IsStaged ? "Staged" : "Cutover",
				base.Version,
				this.SubmittedByUser,
				this.StatusData
			});
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00014304 File Offset: 0x00012504
		public override void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			message[StoreObjectSchema.ItemClass] = MigrationBatchMessageSchema.MigrationJobClass;
			message[MigrationBatchMessageSchema.MigrationJobId] = this.JobId;
			message[MigrationBatchMessageSchema.MigrationJobName] = this.JobName;
			message[MigrationBatchMessageSchema.MigrationJobSubmittedBy] = this.SubmittedByUser;
			message[MigrationBatchMessageSchema.MigrationJobTotalRowCount] = this.TotalRowCount;
			message[MigrationBatchMessageSchema.MigrationJobCountCache] = this.cachedItemCounts.Serialize();
			MigrationHelperBase.SetExDateTimeProperty(message, MigrationBatchMessageSchema.MigrationJobCountCacheFullScanTime, this.FullScanTime);
			message[MigrationBatchMessageSchema.MigrationJobCancelledFlag] = this.IsCancelled;
			message[MigrationBatchMessageSchema.MigrationJobCursorPosition] = this.LastCursorPosition;
			message[MigrationBatchMessageSchema.MigrationJobAdminCulture] = this.AdminCulture.ToString();
			this.StatusData.WriteToMessageItem(message, loaded);
			if (this.OriginalCreationTime == ExDateTime.MinValue)
			{
				this.OriginalCreationTime = ExDateTime.UtcNow;
			}
			MigrationHelperBase.SetExDateTimeProperty(message, MigrationBatchMessageSchema.MigrationJobOriginalCreationTime, new ExDateTime?(this.OriginalCreationTime));
			MigrationHelperBase.SetExDateTimeProperty(message, MigrationBatchMessageSchema.MigrationJobStartTime, this.StartTime);
			MigrationHelperBase.SetExDateTimeProperty(message, MigrationBatchMessageSchema.MigrationJobLastRestartTime, this.LastRestartTime);
			if (this.UserTimeZone == null)
			{
				message[MigrationBatchMessageSchema.MigrationJobUserTimeZone] = ExTimeZone.CurrentTimeZone.Id;
			}
			else
			{
				message[MigrationBatchMessageSchema.MigrationJobUserTimeZone] = this.UserTimeZone.Id;
			}
			MigrationJob.SaveNotificationEmails(message, this.NotificationEmails);
			this.SaveReports(message, this.Reports, loaded);
			MigrationHelperBase.SetExDateTimeProperty(message, MigrationBatchMessageSchema.MigrationJobFinalizeTime, this.FinalizeTime);
			message[MigrationBatchMessageSchema.MigrationType] = this.MigrationType;
			message[MigrationBatchMessageSchema.MigrationSubmittedByUserAdminType] = this.SubmittedByUserAdminType;
			message[MigrationBatchMessageSchema.MigrationJobStatisticsEnabled] = this.StatisticsEnabled;
			message[MigrationBatchMessageSchema.MigrationJobCancellationReason] = 0;
			message[MigrationBatchMessageSchema.MigrationJobIsStaged] = this.IsStaged;
			if (this.IsPAW)
			{
				message[MigrationBatchMessageSchema.MigrationFlags] = this.Flags;
				message[MigrationBatchMessageSchema.MigrationStage] = this.Stage;
				message[MigrationBatchMessageSchema.MigrationWorkflow] = this.Workflow.Serialize(false);
			}
			else
			{
				message[MigrationBatchMessageSchema.MigrationJobSuppressErrors] = false;
			}
			message[MigrationBatchMessageSchema.MigrationExchangeObjectId] = this.OwnerExchangeObjectId;
			if (this.OwnerId != null)
			{
				message[MigrationBatchMessageSchema.MigrationJobOwnerId] = this.OwnerId.GetBytes();
			}
			else
			{
				message[MigrationBatchMessageSchema.MigrationJobDelegatedAdminOwnerId] = this.DelegatedAdminOwnerId;
			}
			MigrationHelperBase.SetExDateTimeProperty(message, MigrationBatchMessageSchema.MigrationJobItemSubscriptionLastChecked, this.LastScheduled);
			MigrationHelperBase.SetExDateTimeProperty(message, MigrationBatchMessageSchema.MigrationNextProcessTime, this.NextProcessTime);
			if (this.MigrationType == MigrationType.PublicFolder)
			{
				message[MigrationBatchMessageSchema.MigrationJobLastFinalizationAttempt] = this.LastFinalizationAttempt;
			}
			if (this.SourceEndpoint != null)
			{
				message[MigrationBatchMessageSchema.MigrationJobSourceEndpoint] = this.SourceEndpoint.Guid;
			}
			if (this.TargetEndpoint != null)
			{
				message[MigrationBatchMessageSchema.MigrationJobTargetEndpoint] = this.TargetEndpoint.Guid;
			}
			message[MigrationBatchMessageSchema.MigrationJobDirection] = this.JobDirection;
			message[MigrationBatchMessageSchema.MigrationJobSkipSteps] = this.SkipSteps;
			if (this.SubscriptionSettings != null)
			{
				this.SubscriptionSettings.WriteExtendedProperties(base.ExtendedProperties);
				this.SubscriptionSettings.WriteToMessageItem(message, loaded);
			}
			base.WriteToMessageItem(message, loaded);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00014688 File Offset: 0x00012888
		public void SetStatusData(IMigrationDataProvider provider, MigrationStatusData<MigrationJobStatus> newStatusData)
		{
			this.SetStatusData(provider, newStatusData, false);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00014694 File Offset: 0x00012894
		public void SetTroubleshooterNotes(IMigrationDataProvider provider, string notes)
		{
			this.TroubleshooterNotes = notes;
			using (IMigrationMessageItem migrationMessageItem = base.FindMessageItem(provider, MigrationPersistableBase.MigrationBaseDefinitions))
			{
				migrationMessageItem.OpenAsReadWrite();
				this.WriteExtendedPropertiesToMessageItem(migrationMessageItem);
				migrationMessageItem.Save(SaveMode.ResolveConflicts);
			}
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x000146E8 File Offset: 0x000128E8
		public override bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			MigrationUtil.ThrowOnNullArgument(message, "message");
			base.ReadFromMessageItem(message);
			if (this.IsPAW)
			{
				this.Flags = MigrationHelper.GetEnumProperty<MigrationFlags>(message, MigrationBatchMessageSchema.MigrationFlags);
				this.Stage = MigrationHelper.GetEnumProperty<MigrationStage>(message, MigrationBatchMessageSchema.MigrationStage);
				string content = (string)message[MigrationBatchMessageSchema.MigrationWorkflow];
				this.Workflow = MigrationWorkflow.Deserialize(content);
			}
			MigrationHelper.VerifyMigrationTypeEquality(this.MigrationType, (MigrationType)message[MigrationBatchMessageSchema.MigrationType]);
			this.LastCursorPosition = (string)message[MigrationBatchMessageSchema.MigrationJobCursorPosition];
			this.StatisticsEnabled = (bool)message[MigrationBatchMessageSchema.MigrationJobStatisticsEnabled];
			this.ownerExchangeObjectId = MigrationHelper.GetGuidProperty(message, MigrationBatchMessageSchema.MigrationExchangeObjectId, false);
			if (!MigrationHelper.TryGetADObjectId(message, MigrationBatchMessageSchema.MigrationJobOwnerId, out this.ownerId))
			{
				this.DelegatedAdminOwnerId = message.GetValueOrDefault<string>(MigrationBatchMessageSchema.MigrationJobDelegatedAdminOwnerId, string.Empty);
			}
			this.LastFinalizationAttempt = message.GetValueOrDefault<int>(MigrationBatchMessageSchema.MigrationJobLastFinalizationAttempt, 0);
			this.FinalizeTime = MigrationHelperBase.GetExDateTimeProperty(message, MigrationBatchMessageSchema.MigrationJobFinalizeTime);
			this.SubmittedByUserAdminType = MigrationHelper.GetEnumProperty<SubmittedByUserAdminType>(message, MigrationBatchMessageSchema.MigrationSubmittedByUserAdminType);
			this.JobCancellationStatus = MigrationHelper.GetEnumProperty<JobCancellationStatus>(message, MigrationBatchMessageSchema.MigrationJobCancellationReason);
			this.StatusData = MigrationStatusData<MigrationJobStatus>.Create(message, MigrationJob.StatusDataVersionMap[base.Version]);
			this.JobId = MigrationHelper.GetGuidProperty(message, MigrationBatchMessageSchema.MigrationJobId, true);
			this.JobName = (string)message[MigrationBatchMessageSchema.MigrationJobName];
			this.SubmittedByUser = message.GetValueOrDefault<string>(MigrationBatchMessageSchema.MigrationJobSubmittedBy, string.Empty);
			this.TotalRowCount = message.GetValueOrDefault<int>(MigrationBatchMessageSchema.MigrationJobTotalRowCount, 0);
			string serializedData = (string)message[MigrationBatchMessageSchema.MigrationJobCountCache];
			this.cachedItemCounts = MigrationCountCache.Deserialize(serializedData);
			this.FullScanTime = MigrationHelperBase.GetExDateTimeProperty(message, MigrationBatchMessageSchema.MigrationJobCountCacheFullScanTime);
			this.OriginalCreationTime = MigrationHelper.GetExDateTimePropertyOrDefault(message, MigrationBatchMessageSchema.MigrationJobOriginalCreationTime, message.CreationTime);
			this.StartTime = MigrationHelperBase.GetExDateTimeProperty(message, MigrationBatchMessageSchema.MigrationJobStartTime);
			this.LastRestartTime = MigrationHelperBase.GetExDateTimeProperty(message, MigrationBatchMessageSchema.MigrationJobLastRestartTime);
			this.UserTimeZone = MigrationHelper.GetExTimeZoneProperty(message, MigrationBatchMessageSchema.MigrationJobUserTimeZone);
			this.AdminCulture = MigrationHelper.GetCultureInfoPropertyOrDefault(message, MigrationBatchMessageSchema.MigrationJobAdminCulture);
			object property = MigrationHelper.GetProperty<object>(message, MigrationBatchMessageSchema.MigrationJobMaxConcurrentMigrations, false);
			if (property != null)
			{
				this.MaxConcurrentMigrations = (int)property;
			}
			this.PoisonCount = message.GetValueOrDefault<int>(MigrationBatchMessageSchema.MigrationJobPoisonCount, 0);
			this.IsStaged = message.GetValueOrDefault<bool>(MigrationBatchMessageSchema.MigrationJobIsStaged, true);
			this.LastScheduled = MigrationHelper.GetExDateTimePropertyOrNull(message, MigrationBatchMessageSchema.MigrationJobItemSubscriptionLastChecked);
			this.NextProcessTime = MigrationHelper.GetExDateTimePropertyOrNull(message, MigrationBatchMessageSchema.MigrationNextProcessTime);
			this.Reports = MigrationJob.LoadReports(message);
			this.NotificationEmails = MigrationJob.LoadNotificationEmails(message);
			this.JobDirection = message.GetValueOrDefault<MigrationBatchDirection>(MigrationBatchMessageSchema.MigrationJobDirection, MigrationBatchDirection.Onboarding);
			this.SkipSteps = message.GetValueOrDefault<SkippableMigrationSteps>(MigrationBatchMessageSchema.MigrationJobSkipSteps, SkippableMigrationSteps.None);
			this.SubscriptionSettings = JobSubscriptionSettingsBase.CreateFromMessage(message, this.MigrationType, base.ExtendedProperties, this.IsPAW);
			return true;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x000149C8 File Offset: 0x00012BC8
		public IEnumerable<MigrationJobItem> GetItemsByStatus(IMigrationDataProvider provider, MigrationUserStatus status, int? maxCount)
		{
			return MigrationJobItem.GetByStatus(provider, this, status, maxCount);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x000149D4 File Offset: 0x00012BD4
		public ExDateTime GetEffectiveFinalizationTime()
		{
			if (this.FinalizeTime != null)
			{
				return this.FinalizeTime.Value;
			}
			MigrationLogger.Log(MigrationEventType.Warning, "Job {0} has no finalized time, setting to last updated time", new object[]
			{
				this
			});
			if (this.StateLastUpdated != null)
			{
				return this.StateLastUpdated.Value;
			}
			MigrationLogger.Log(MigrationEventType.Error, "At finalized state job {0} should have a StateLastUpdated value set", new object[]
			{
				this
			});
			throw new MigrationDataCorruptionException("StateLastUpdated value should be set for finalized job " + this);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00014AE0 File Offset: 0x00012CE0
		internal static IEnumerable<StoreObjectId> GetIdsByState(IMigrationDataProvider provider, MigrationState state, ExDateTime? nextProcessTime = null, int? maxCount = null)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			List<QueryFilter> list = new List<QueryFilter>
			{
				new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, MigrationBatchMessageSchema.MigrationJobClass),
				new ComparisonFilter(ComparisonOperator.Equal, MigrationBatchMessageSchema.MigrationState, state)
			};
			List<PropertyDefinition> list2 = new List<PropertyDefinition>
			{
				StoreObjectSchema.ItemClass,
				MigrationBatchMessageSchema.MigrationState
			};
			if (nextProcessTime != null)
			{
				list.Add(new ComparisonFilter(ComparisonOperator.LessThanOrEqual, MigrationBatchMessageSchema.MigrationNextProcessTime, nextProcessTime));
				list2.Add(MigrationBatchMessageSchema.MigrationNextProcessTime);
			}
			return provider.FindMessageIds(QueryFilter.AndTogether(list.ToArray()), list2.ToArray(), MigrationJob.StateSort, delegate(IDictionary<PropertyDefinition, object> row)
			{
				if (!object.Equals(row[StoreObjectSchema.ItemClass], MigrationBatchMessageSchema.MigrationJobClass))
				{
					return MigrationRowSelectorResult.RejectRowStopProcessing;
				}
				if ((MigrationState)row[MigrationBatchMessageSchema.MigrationState] != state)
				{
					return MigrationRowSelectorResult.RejectRowStopProcessing;
				}
				if (nextProcessTime != null && ExDateTime.Compare((ExDateTime)row[MigrationBatchMessageSchema.MigrationNextProcessTime], nextProcessTime.Value) > 0)
				{
					return MigrationRowSelectorResult.RejectRowStopProcessing;
				}
				return MigrationRowSelectorResult.AcceptRow;
			}, maxCount);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00014C30 File Offset: 0x00012E30
		internal static IEnumerable<StoreObjectId> GetIdsWithFlagPresence(IMigrationDataProvider provider, bool present, int? maxCount = null)
		{
			List<QueryFilter> list = new List<QueryFilter>
			{
				new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, MigrationBatchMessageSchema.MigrationJobClass),
				new ComparisonFilter(present ? ComparisonOperator.NotEqual : ComparisonOperator.Equal, MigrationBatchMessageSchema.MigrationFlags, MigrationFlags.None),
				new ComparisonFilter(ComparisonOperator.NotEqual, MigrationBatchMessageSchema.MigrationState, MigrationState.Disabled)
			};
			List<PropertyDefinition> list2 = new List<PropertyDefinition>
			{
				StoreObjectSchema.ItemClass,
				MigrationBatchMessageSchema.MigrationFlags,
				MigrationBatchMessageSchema.MigrationState
			};
			return provider.FindMessageIds(QueryFilter.AndTogether(list.ToArray()), list2.ToArray(), MigrationJob.FlagSort, delegate(IDictionary<PropertyDefinition, object> row)
			{
				if (!object.Equals(row[StoreObjectSchema.ItemClass], MigrationBatchMessageSchema.MigrationJobClass))
				{
					return MigrationRowSelectorResult.RejectRowStopProcessing;
				}
				bool flag = (MigrationFlags)row[MigrationBatchMessageSchema.MigrationFlags] != MigrationFlags.None;
				if (present != flag)
				{
					return MigrationRowSelectorResult.RejectRowStopProcessing;
				}
				if ((MigrationState)row[MigrationBatchMessageSchema.MigrationState] == MigrationState.Disabled)
				{
					return MigrationRowSelectorResult.RejectRowContinueProcessing;
				}
				return MigrationRowSelectorResult.AcceptRow;
			}, maxCount);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00014CF8 File Offset: 0x00012EF8
		internal static bool MigrationTypeSupportsProvisioning(MigrationType migrationType)
		{
			if (migrationType <= MigrationType.ExchangeRemoteMove)
			{
				if (migrationType != MigrationType.IMAP && migrationType != MigrationType.ExchangeRemoteMove)
				{
					return true;
				}
			}
			else if (migrationType != MigrationType.ExchangeLocalMove && migrationType != MigrationType.PSTImport && migrationType != MigrationType.PublicFolder)
			{
				return true;
			}
			return false;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00014D2C File Offset: 0x00012F2C
		protected override bool InitializeFromMessageItem(IMigrationStoreObject message)
		{
			if (!base.InitializeFromMessageItem(message))
			{
				return false;
			}
			this.Initialize((MigrationType)message[MigrationBatchMessageSchema.MigrationType]);
			return true;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00014D50 File Offset: 0x00012F50
		protected override void LoadLinkedStoredObjects(IMigrationStoreObject item, IMigrationDataProvider dataProvider)
		{
			Guid guidProperty = MigrationHelper.GetGuidProperty(item, MigrationBatchMessageSchema.MigrationJobSourceEndpoint, false);
			Guid guidProperty2 = MigrationHelper.GetGuidProperty(item, MigrationBatchMessageSchema.MigrationJobTargetEndpoint, false);
			if (guidProperty != Guid.Empty)
			{
				this.SourceEndpoint = MigrationEndpointBase.Get(guidProperty, dataProvider);
			}
			if (guidProperty2 != Guid.Empty)
			{
				this.TargetEndpoint = MigrationEndpointBase.Get(guidProperty2, dataProvider);
			}
			if (this.TargetEndpoint != null || this.SourceEndpoint != null || this.JobDirection == MigrationBatchDirection.Local)
			{
				return;
			}
			if (this.TargetEndpoint == null && this.SourceEndpoint == null)
			{
				throw new MigrationDataCorruptionException("Either source or target endpoint must be set.");
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00014DE0 File Offset: 0x00012FE0
		private static MigrationJob GetUniqueJob(IMigrationDataProvider provider, MigrationEqualityFilter primaryFilter)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationJob migrationJob = null;
			foreach (MigrationJob migrationJob2 in MigrationJob.GetJobs(provider, primaryFilter))
			{
				if (migrationJob != null)
				{
					MigrationLogger.Log(MigrationEventType.Warning, "GetMigrationJob: delete {0} because of dup with {1}", new object[]
					{
						migrationJob2,
						migrationJob
					});
					try
					{
						provider.MoveMessageItems(new StoreObjectId[]
						{
							migrationJob2.StoreObjectId
						}, MigrationFolderName.CorruptedItems);
						continue;
					}
					catch (StoragePermanentException exception)
					{
						MigrationLogger.Log(MigrationEventType.Error, exception, "GetMigrationJob: couldn't delete migration job {0}", new object[]
						{
							migrationJob2
						});
						continue;
					}
				}
				migrationJob = migrationJob2;
			}
			return migrationJob;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00014EA4 File Offset: 0x000130A4
		private static IEnumerable<MigrationJob> GetJobs(IMigrationDataProvider provider, MigrationEqualityFilter primaryFilter)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationEqualityFilter[] secondaryFilters = null;
			if (primaryFilter == null)
			{
				primaryFilter = MigrationJob.MessageClassEqualityFilter;
			}
			else
			{
				secondaryFilters = new MigrationEqualityFilter[]
				{
					MigrationJob.MessageClassEqualityFilter
				};
			}
			IEnumerable<StoreObjectId> messageIds = MigrationHelper.FindMessageIds(provider, primaryFilter, secondaryFilters, MigrationJob.SortByCreationTime, null);
			return MigrationJob.Load(provider, messageIds);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x000150F8 File Offset: 0x000132F8
		private static IEnumerable<MigrationJob> Load(IMigrationDataProvider provider, IEnumerable<StoreObjectId> messageIds)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.ThrowOnNullArgument(messageIds, "messageIds");
			foreach (StoreObjectId messageId in messageIds)
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "MigrationJob.LoadFromMessageItem: Loading migration job from messageId: {0}.", new object[]
				{
					messageId
				});
				MigrationJob job;
				if (!MigrationJob.TryLoad(provider, messageId, out job))
				{
					throw new CouldNotLoadMigrationPersistedItemTransientException(messageId.ToHexEntryId());
				}
				yield return job;
			}
			yield break;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0001511C File Offset: 0x0001331C
		private static MultiValuedProperty<MigrationReportSet> LoadReports(IMigrationStoreObject message)
		{
			MultiValuedProperty<MigrationReportSet> multiValuedProperty = new MultiValuedProperty<MigrationReportSet>();
			string text = (string)message[MigrationBatchMessageSchema.MigrationReportSets];
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					using (StringReader stringReader = new StringReader(text))
					{
						using (XmlTextReader xmlTextReader = new XmlTextReader(stringReader))
						{
							if (xmlTextReader.MoveToContent() == XmlNodeType.Element && xmlTextReader.LocalName == "Reports")
							{
								MigrationReportSet item;
								while (xmlTextReader.ReadToFollowing("MigrationReportSet") && MigrationReportSet.TryCreate(xmlTextReader, out item))
								{
									multiValuedProperty.Add(item);
								}
							}
						}
					}
				}
				catch (XmlException innerException)
				{
					throw new MigrationDataCorruptionException("cannot read xml reports for job:" + text, innerException);
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x000151EC File Offset: 0x000133EC
		private static void SaveNotificationEmails(IMigrationStoreObject message, MultiValuedProperty<SmtpAddress> notificationEmails)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			if (notificationEmails != null && notificationEmails.Count > 0)
			{
				foreach (SmtpAddress smtpAddress in notificationEmails)
				{
					if (smtpAddress.Length > 0)
					{
						stringBuilder.Append(smtpAddress.ToString());
						stringBuilder.Append(MigrationBatchMessageSchema.ListSeparator[0]);
					}
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Remove(stringBuilder.Length - 1, 1);
				}
			}
			message[MigrationBatchMessageSchema.MigrationJobNotificationEmails] = stringBuilder.ToString();
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x000152A0 File Offset: 0x000134A0
		private static MultiValuedProperty<SmtpAddress> LoadNotificationEmails(IMigrationStoreObject message)
		{
			MultiValuedProperty<SmtpAddress> multiValuedProperty = new MultiValuedProperty<SmtpAddress>();
			string valueOrDefault = message.GetValueOrDefault<string>(MigrationBatchMessageSchema.MigrationJobNotificationEmails, string.Empty);
			if (!string.IsNullOrEmpty(valueOrDefault))
			{
				string[] array = valueOrDefault.Split(MigrationBatchMessageSchema.ListSeparator, StringSplitOptions.RemoveEmptyEntries);
				foreach (string smtpAddress in array)
				{
					SmtpAddress smtpAddress2 = MigrationHelper.GetSmtpAddress(smtpAddress, MigrationBatchMessageSchema.MigrationJobNotificationEmails);
					if (!multiValuedProperty.Contains(smtpAddress2))
					{
						multiValuedProperty.Add(smtpAddress2);
					}
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00015315 File Offset: 0x00013515
		private static void InitializeEndpointsForMigrationBatch(MigrationJob job, MigrationBatch batch)
		{
			if (job.SourceEndpoint != null)
			{
				batch.SourceEndpoint = job.SourceEndpoint;
			}
			if (job.TargetEndpoint != null)
			{
				batch.TargetEndpoint = job.TargetEndpoint;
			}
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0001534C File Offset: 0x0001354C
		private static void CheckOperationIsSupported(MigrationJob.SupportsActionDelegate supportsActionsDelegate, LocalizedString safetyErrorMessage)
		{
			LocalizedString? localizedString;
			if (!supportsActionsDelegate(out localizedString))
			{
				throw new MigrationTransientException(localizedString ?? safetyErrorMessage);
			}
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00015380 File Offset: 0x00013580
		private void SetRestartTime(IMigrationDataProvider provider)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			base.CheckVersion();
			ExDateTime utcNow = ExDateTime.UtcNow;
			using (IMigrationMessageItem migrationMessageItem = base.FindMessageItem(provider, MigrationJob.JobStartPropertyDefinition))
			{
				migrationMessageItem.OpenAsReadWrite();
				MigrationHelperBase.SetExDateTimeProperty(migrationMessageItem, MigrationBatchMessageSchema.MigrationJobLastRestartTime, new ExDateTime?(utcNow));
				this.AutoRunCount++;
				this.WriteExtendedPropertiesToMessageItem(migrationMessageItem);
				migrationMessageItem.Save(SaveMode.NoConflictResolution);
			}
			this.LastRestartTime = new ExDateTime?(utcNow);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0001540C File Offset: 0x0001360C
		private bool SupportsRestarting(out LocalizedString? errorMsg)
		{
			errorMsg = null;
			if (MigrationJobStage.Corrupted.IsStatusSupported(this.Status))
			{
				errorMsg = new LocalizedString?(Strings.CorruptedMigrationBatchCannotBeStarted);
				return false;
			}
			if (this.StartTime == null)
			{
				errorMsg = new LocalizedString?(Strings.CompletedMigrationJobCannotBeStarted);
				return false;
			}
			if (!this.IsStaged && this.migrationJobType != MigrationType.PublicFolder)
			{
				return true;
			}
			bool flag = this.BatchLastUpdated != null && this.BatchLastUpdated.Value > this.StartTime.Value;
			if (!flag)
			{
				int num = this.FailedItemCount + this.StoppedItemCount;
				flag = (num > 0);
				MigrationLogger.Log(MigrationEventType.Verbose, "can start job? failed + stopped count {0}", new object[]
				{
					num
				});
			}
			foreach (MigrationUserStatus migrationUserStatus in MigrationJob.JobItemStatusEligibleForJobStarting)
			{
				if (flag)
				{
					break;
				}
				int cachedStatusCount = this.cachedItemCounts.GetCachedStatusCount(new MigrationUserStatus[]
				{
					migrationUserStatus
				});
				flag = (cachedStatusCount > 0);
				MigrationLogger.Log(MigrationEventType.Verbose, "can start job? item {0} count {1}", new object[]
				{
					migrationUserStatus,
					cachedStatusCount
				});
			}
			if (!flag)
			{
				errorMsg = new LocalizedString?(Strings.CompletedMigrationJobCannotBeStarted);
				return false;
			}
			return true;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001556D File Offset: 0x0001376D
		private void SetBatchFlags(MigrationBatchFlags flagsToSet, bool enable)
		{
			if (enable)
			{
				this.BatchFlags |= flagsToSet;
				return;
			}
			this.BatchFlags &= ~flagsToSet;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00015590 File Offset: 0x00013790
		private bool GetBatchFlags(MigrationBatchFlags flagsToGet)
		{
			return (this.BatchFlags & flagsToGet) == flagsToGet;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0001559D File Offset: 0x0001379D
		private void SetShouldSkip(SkippableMigrationSteps stepsToSet, bool shouldSkip)
		{
			if (shouldSkip)
			{
				this.SkipSteps |= stepsToSet;
				return;
			}
			this.SkipSteps &= ~stepsToSet;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x000155C0 File Offset: 0x000137C0
		private bool GetShouldSkip(SkippableMigrationSteps stepsToGet)
		{
			return (this.SkipSteps & stepsToGet) == stepsToGet;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000155D0 File Offset: 0x000137D0
		private void SaveCsvStream(IMigrationMessageItem message, IMigrationAttachment attachment, Stream csvStream)
		{
			Util.StreamHandler.CopyStreamData(csvStream, attachment.Stream);
			attachment.Save(null);
			MigrationLogger.Log(MigrationEventType.Verbose, "the attachment last modified time: {0} and id {1}", new object[]
			{
				attachment.LastModifiedTime,
				attachment.Id
			});
			this.BatchLastUpdated = new ExDateTime?(ExDateTime.UtcNow);
			this.BatchInputId = Guid.NewGuid().ToString();
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00015644 File Offset: 0x00013844
		private void CreateValidationWarningAttachment(IMigrationMessageItem message, IEnumerable<MigrationBatchError> validationWarnings, bool clearExisting)
		{
			if (clearExisting)
			{
				message.DeleteAttachment("Errors.csv");
				this.ValidationWarningCount = 0;
			}
			int validationWarningCount = 0;
			using (IMigrationAttachment migrationAttachment = message.CreateAttachment("Errors.csv"))
			{
				using (StreamWriter streamWriter = new StreamWriter(migrationAttachment.Stream))
				{
					validationWarningCount = MigrationErrorCsvSchema.WriteHeaderAndErrors(streamWriter, validationWarnings);
					streamWriter.Flush();
				}
				migrationAttachment.Save(null);
			}
			this.ValidationWarningCount = validationWarningCount;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x000156D0 File Offset: 0x000138D0
		private void SaveReports(IMigrationStoreObject message, MultiValuedProperty<MigrationReportSet> reports, bool loaded)
		{
			XElement xelement = new XElement("Reports");
			if (reports != null && reports.Count > 0)
			{
				try
				{
					using (XmlWriter xmlWriter = xelement.CreateWriter())
					{
						foreach (MigrationReportSet migrationReportSet in reports)
						{
							migrationReportSet.WriteXml(xmlWriter);
						}
					}
				}
				catch (XmlException innerException)
				{
					throw new MigrationDataCorruptionException("cannot write xml reports for job", innerException);
				}
			}
			message[MigrationBatchMessageSchema.MigrationReportSets] = xelement.ToString();
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00015788 File Offset: 0x00013988
		private int GetItemCount(IMigrationDataProvider provider, params MigrationUserStatus[] statuses)
		{
			return MigrationJobItem.GetCount(provider, this.JobId, statuses);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00015798 File Offset: 0x00013998
		private bool ShouldAutoRetryJob(int failureCount)
		{
			return this.MaxAutoRunCount != null && this.AutoRunCount < this.MaxAutoRunCount.Value && failureCount > 0;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x000157D4 File Offset: 0x000139D4
		private bool TryAutoRetryJob(IMigrationDataProvider provider, int failureCount)
		{
			if (!this.ShouldAutoRetryJob(failureCount))
			{
				return false;
			}
			MigrationLogger.Log(MigrationEventType.Information, "Rerunning batch {0} runcount {1} of max {2} b.c. found errors {3}", new object[]
			{
				this,
				this.AutoRunCount,
				this.MaxAutoRunCount,
				failureCount
			});
			this.SetRestartTime(provider);
			base.ReportData.Append(Strings.MigrationReportJobAutomaticallyRestarting(failureCount, this.AutoRunCount, this.MaxAutoRunCount.Value));
			provider.FlushReport(base.ReportData);
			return true;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00015861 File Offset: 0x00013A61
		private void Initialize(MigrationType migrationType)
		{
			this.migrationJobType = migrationType;
			this.Reports = new MultiValuedProperty<MigrationReportSet>();
			this.cachedItemCounts = new MigrationCountCache();
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00015880 File Offset: 0x00013A80
		private void SetStatusData(IMigrationDataProvider provider, MigrationStatusData<MigrationJobStatus> newStatusData, bool clearLastCursorPosition)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.ThrowOnNullArgument(newStatusData, "statusData");
			PropertyDefinition[] properties = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
			{
				MigrationStatusData<MigrationJobStatus>.StatusPropertyDefinition,
				new PropertyDefinition[]
				{
					MigrationBatchMessageSchema.MigrationJobCursorPosition
				}
			});
			using (IMigrationStoreObject migrationStoreObject = this.FindStoreObject(provider, base.StoreObjectId, properties))
			{
				migrationStoreObject.OpenAsReadWrite();
				newStatusData.WriteToMessageItem(migrationStoreObject, true);
				if (clearLastCursorPosition)
				{
					migrationStoreObject[MigrationBatchMessageSchema.MigrationJobCursorPosition] = string.Empty;
				}
				migrationStoreObject.Save(SaveMode.NoConflictResolution);
			}
			this.StatusData = newStatusData;
			if (clearLastCursorPosition)
			{
				this.LastCursorPosition = string.Empty;
			}
			if (newStatusData.Status == MigrationJobStatus.Removed)
			{
				MigrationServiceFactory.Instance.GetAsyncNotificationAdapter().RemoveNotification(provider, this);
				return;
			}
			MigrationServiceFactory.Instance.GetAsyncNotificationAdapter().UpdateNotification(provider, this);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x000159E0 File Offset: 0x00013BE0
		private void CreateInStore(IMigrationDataProvider provider, Stream csvStream, IEnumerable<MigrationBatchError> validationWarnings)
		{
			this.CreateInStore(provider, delegate(IMigrationStoreObject storeObject)
			{
				IMigrationMessageItem migrationMessageItem = storeObject as IMigrationMessageItem;
				if (validationWarnings != null)
				{
					this.CreateValidationWarningAttachment(migrationMessageItem, validationWarnings, false);
				}
				if (csvStream != null)
				{
					using (IMigrationAttachment migrationAttachment = migrationMessageItem.CreateAttachment("Request.csv"))
					{
						this.SaveCsvStream(migrationMessageItem, migrationAttachment, csvStream);
					}
				}
			});
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00015A1C File Offset: 0x00013C1C
		private void Initialize(MigrationBatch migrationBatch, IMigrationDataProvider dataProvider, bool usePAW)
		{
			MigrationHelper.VerifyMigrationTypeEquality(this.MigrationType, migrationBatch.MigrationType);
			MigrationEndpointBase migrationEndpointBase = (migrationBatch.SourceEndpoint == null) ? null : MigrationEndpointBase.Get(migrationBatch.SourceEndpoint.Identity, dataProvider, false).First<MigrationEndpointBase>();
			MigrationEndpointBase migrationEndpointBase2 = (migrationBatch.TargetEndpoint == null) ? null : MigrationEndpointBase.Get(migrationBatch.TargetEndpoint.Identity, dataProvider, false).First<MigrationEndpointBase>();
			if (migrationEndpointBase == null && migrationEndpointBase2 == null && migrationBatch.BatchDirection != MigrationBatchDirection.Local)
			{
				throw new MigrationDataCorruptionException("Only local batches can have no endpoints.");
			}
			MigrationState? state;
			MigrationJobStatus migrationJobStatus;
			if (usePAW)
			{
				this.currentSupportedVersion = 5L;
				this.Flags = MigrationFlags.None;
				this.Stage = MigrationStage.Discovery;
				this.Workflow = MigrationServiceFactory.Instance.GetMigrationWorkflow(migrationBatch.MigrationType);
				if (migrationBatch.Flags.HasFlag(MigrationFlags.Stop))
				{
					state = new MigrationState?(MigrationState.Stopped);
					migrationJobStatus = MigrationJobStatus.Stopped;
				}
				else
				{
					state = new MigrationState?(MigrationState.Active);
					migrationJobStatus = MigrationJobStatus.SyncStarting;
					this.StartTime = new ExDateTime?(ExDateTime.UtcNow);
				}
			}
			else
			{
				this.currentSupportedVersion = 4L;
				state = null;
				migrationJobStatus = MigrationJobStatus.Created;
			}
			this.JobId = Guid.NewGuid();
			this.OriginalJobId = migrationBatch.OriginalBatchId;
			this.StatusData = new MigrationStatusData<MigrationJobStatus>(migrationJobStatus, MigrationJob.StatusDataVersionMap[this.currentSupportedVersion], state);
			this.JobName = migrationBatch.Identity.Name;
			this.SubmittedByUser = migrationBatch.SubmittedByUser;
			this.OwnerId = migrationBatch.OwnerId;
			this.OwnerExchangeObjectId = migrationBatch.OwnerExchangeObjectId;
			this.DelegatedAdminOwnerId = (migrationBatch.DelegatedAdminOwner ?? string.Empty);
			this.SubmittedByUserAdminType = migrationBatch.SubmittedByUserAdminType;
			this.NotificationEmails = migrationBatch.NotificationEmails;
			this.SkipSteps = migrationBatch.SkipSteps;
			this.TotalRowCount = migrationBatch.TotalCount;
			this.cachedItemCounts = new MigrationCountCache();
			this.FullScanTime = new ExDateTime?((ExDateTime)migrationBatch.OriginalCreationTime);
			this.OriginalCreationTime = (ExDateTime)migrationBatch.OriginalCreationTime;
			this.StatisticsEnabled = migrationBatch.OriginalStatisticsEnabled;
			this.IsStaged = (migrationBatch.CsvStream != null);
			if (migrationBatch.UserTimeZone != null)
			{
				this.UserTimeZone = migrationBatch.UserTimeZone.ExTimeZone;
			}
			else
			{
				this.UserTimeZone = ExTimeZone.CurrentTimeZone;
			}
			this.AdminCulture = (migrationBatch.Locale ?? CultureInfo.CurrentCulture);
			this.LastCursorPosition = "";
			this.BatchFlags = migrationBatch.BatchFlags;
			this.AllowUnknownColumnsInCsv = migrationBatch.AllowUnknownColumnsInCsv;
			this.SourceEndpoint = migrationEndpointBase;
			this.TargetEndpoint = migrationEndpointBase2;
			this.JobDirection = migrationBatch.BatchDirection;
			this.TargetDomainName = migrationBatch.TargetDomainName;
			this.SubscriptionSettings = JobSubscriptionSettingsBase.CreateFromBatch(migrationBatch, usePAW);
			this.NotificationId = MigrationServiceFactory.Instance.GetAsyncNotificationAdapter().CreateNotification(dataProvider, this);
			this.MaxAutoRunCount = migrationBatch.AutoRetryCount;
			if (migrationBatch.ReportInterval != null)
			{
				this.ReportInterval = migrationBatch.ReportInterval.Value;
			}
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00015CFB File Offset: 0x00013EFB
		private void LogStatusEvent()
		{
			if (MigrationServiceFactory.Instance.ShouldLog)
			{
				MigrationJobLog.LogStatusEvent(this);
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00015D0F File Offset: 0x00013F0F
		private void SaveRuntimeJobData(IMigrationDataProvider dataProvider)
		{
			this.SaveExtendedProperties(dataProvider);
		}

		// Token: 0x0400015E RID: 350
		public const int MaxErrorCollectionSize = 500;

		// Token: 0x0400015F RID: 351
		internal const string EndOfDataRow = "EOF";

		// Token: 0x04000160 RID: 352
		internal const string InitialCursorPosition = "";

		// Token: 0x04000161 RID: 353
		private const string ReportsRootSerializedTag = "Reports";

		// Token: 0x04000162 RID: 354
		private const long MigrationJobEndpointVersion = 4L;

		// Token: 0x04000163 RID: 355
		private const long MigrationJobPAWVersion = 5L;

		// Token: 0x04000164 RID: 356
		private const string MigrationBatchFlagsKey = "MigrationBatchFlags";

		// Token: 0x04000165 RID: 357
		private const string IncrementalSyncIntervalKey = "IncrementalSyncInterval";

		// Token: 0x04000166 RID: 358
		private const string ReportIntervalKey = "ReportInterval";

		// Token: 0x04000167 RID: 359
		private const string BatchLastUpdatedKey = "BatchLastUpdated";

		// Token: 0x04000168 RID: 360
		private const string BatchInputIdKey = "BatchInputId";

		// Token: 0x04000169 RID: 361
		private const string OriginalJobIdKey = "OriginalJobId";

		// Token: 0x0400016A RID: 362
		private const string InitialSyncDateTimeKey = "InitialSyncDateTime";

		// Token: 0x0400016B RID: 363
		private const string InitialSyncDurationKey = "InitialSyncDuration";

		// Token: 0x0400016C RID: 364
		private const string ProcessingDurationKey = "ProcessingDuration";

		// Token: 0x0400016D RID: 365
		private const string ValidationWarningCountKey = "ValidationWarningCount";

		// Token: 0x0400016E RID: 366
		private const string TroubleshooterNotesKey = "TroubleshooterNotes";

		// Token: 0x0400016F RID: 367
		private const string RuntimeDataKey = "RuntimeData";

		// Token: 0x04000170 RID: 368
		private const string AutoRunCountKey = "AutoRunCount";

		// Token: 0x04000171 RID: 369
		private const string MaxAutoRunCountKey = "MaxAutoRunCount";

		// Token: 0x04000172 RID: 370
		private const string AllowUnknownColumnsInCsvKey = "AllowUnknownColumnsInCsv";

		// Token: 0x04000173 RID: 371
		internal static readonly MigrationUserStatus[] JobItemStatusForBatchCompletionErrors = new MigrationUserStatus[]
		{
			MigrationUserStatus.Failed,
			MigrationUserStatus.IncrementalFailed,
			MigrationUserStatus.Corrupted,
			MigrationUserStatus.Stopped,
			MigrationUserStatus.IncrementalStopped
		};

		// Token: 0x04000174 RID: 372
		internal static readonly MigrationUserStatus[] JobItemsStatusForActive = MigrationUser.MapFromSummaryToStatus[MigrationUserStatusSummary.Active];

		// Token: 0x04000175 RID: 373
		internal static readonly MigrationUserStatus[] JobItemsStatusForStopped = MigrationUser.MapFromSummaryToStatus[MigrationUserStatusSummary.Stopped];

		// Token: 0x04000176 RID: 374
		internal static readonly MigrationUserStatus[] JobItemsStatusForSynced = MigrationUser.MapFromSummaryToStatus[MigrationUserStatusSummary.Synced];

		// Token: 0x04000177 RID: 375
		internal static readonly MigrationUserStatus[] JobItemsStatusForFinalized = MigrationUser.MapFromSummaryToStatus[MigrationUserStatusSummary.Completed];

		// Token: 0x04000178 RID: 376
		internal static readonly MigrationUserStatus[] JobItemsStatusForFailedInitial = new MigrationUserStatus[]
		{
			MigrationUserStatus.Failed
		};

		// Token: 0x04000179 RID: 377
		internal static readonly MigrationUserStatus[] JobItemsStatusForFailedIncremental = new MigrationUserStatus[]
		{
			MigrationUserStatus.IncrementalFailed
		};

		// Token: 0x0400017A RID: 378
		internal static readonly MigrationUserStatus[] JobItemsStatusForFailedFinalization = new MigrationUserStatus[]
		{
			MigrationUserStatus.CompletionFailed
		};

		// Token: 0x0400017B RID: 379
		internal static readonly MigrationUserStatus[] JobItemsStatusForFailedOther = new MigrationUserStatus[]
		{
			MigrationUserStatus.Corrupted,
			MigrationUserStatus.CompletedWithWarnings
		};

		// Token: 0x0400017C RID: 380
		internal static readonly MigrationUserStatus[] JobItemsStatusForActiveInitial = new MigrationUserStatus[]
		{
			MigrationUserStatus.Validating,
			MigrationUserStatus.Provisioning,
			MigrationUserStatus.ProvisionUpdating,
			MigrationUserStatus.Syncing
		};

		// Token: 0x0400017D RID: 381
		internal static readonly MigrationUserStatus[] AllJobItemsStatuses = (MigrationUserStatus[])Enum.GetValues(typeof(MigrationUserStatus));

		// Token: 0x0400017E RID: 382
		internal static readonly MigrationUserStatus[] AllUsedPAWJobItemStatuses = new MigrationUserStatus[]
		{
			MigrationUserStatus.Validating,
			MigrationUserStatus.Provisioning,
			MigrationUserStatus.ProvisionUpdating,
			MigrationUserStatus.Syncing,
			MigrationUserStatus.Synced,
			MigrationUserStatus.Completed,
			MigrationUserStatus.CompletedWithWarnings,
			MigrationUserStatus.Starting,
			MigrationUserStatus.Stopping,
			MigrationUserStatus.Stopped,
			MigrationUserStatus.Failed,
			MigrationUserStatus.Removing
		};

		// Token: 0x0400017F RID: 383
		private static readonly ConcurrentDictionary<string, PropertyDefinition[]> PropertyDefinitionsHash = new ConcurrentDictionary<string, PropertyDefinition[]>();

		// Token: 0x04000180 RID: 384
		private static readonly SortBy[] StateSort = new SortBy[]
		{
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
			new SortBy(MigrationBatchMessageSchema.MigrationState, SortOrder.Ascending),
			new SortBy(MigrationBatchMessageSchema.MigrationNextProcessTime, SortOrder.Ascending)
		};

		// Token: 0x04000181 RID: 385
		private static readonly SortBy[] FlagSort = new SortBy[]
		{
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
			new SortBy(MigrationBatchMessageSchema.MigrationFlags, SortOrder.Ascending),
			new SortBy(MigrationBatchMessageSchema.MigrationState, SortOrder.Ascending)
		};

		// Token: 0x04000182 RID: 386
		private static readonly Dictionary<long, long> StatusDataVersionMap = new Dictionary<long, long>
		{
			{
				4L,
				1L
			},
			{
				5L,
				2L
			}
		};

		// Token: 0x04000183 RID: 387
		private static readonly MigrationUserStatus[] JobItemStatusEligibleForJobStarting = new MigrationUserStatus[]
		{
			MigrationUserStatus.Queued,
			MigrationUserStatus.Provisioning,
			MigrationUserStatus.ProvisionUpdating
		};

		// Token: 0x04000184 RID: 388
		private static readonly PropertyDefinition[] MigrationJobPropertyDefinition = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
		{
			new PropertyDefinition[]
			{
				StoreObjectSchema.ItemClass,
				MigrationBatchMessageSchema.MigrationJobId,
				MigrationBatchMessageSchema.MigrationJobName,
				MigrationBatchMessageSchema.MigrationJobSubmittedBy,
				MigrationBatchMessageSchema.MigrationJobTotalRowCount,
				MigrationBatchMessageSchema.MigrationJobNotificationEmails,
				MigrationBatchMessageSchema.MigrationJobOriginalCreationTime,
				MigrationBatchMessageSchema.MigrationJobStartTime,
				MigrationBatchMessageSchema.MigrationJobUserTimeZone,
				MigrationBatchMessageSchema.MigrationJobCancelledFlag,
				MigrationBatchMessageSchema.MigrationJobItemRowIndex,
				MigrationBatchMessageSchema.MigrationJobAdminCulture,
				MigrationBatchMessageSchema.MigrationJobMaxConcurrentMigrations,
				MigrationBatchMessageSchema.MigrationJobCursorPosition,
				MigrationBatchMessageSchema.MigrationJobOwnerId,
				MigrationBatchMessageSchema.MigrationJobDelegatedAdminOwnerId,
				MigrationBatchMessageSchema.MigrationJobSuppressErrors,
				MigrationBatchMessageSchema.MigrationJobFinalizeTime,
				MigrationBatchMessageSchema.MigrationSubmittedByUserAdminType,
				MigrationBatchMessageSchema.MigrationJobStatisticsEnabled,
				MigrationBatchMessageSchema.MigrationJobCancellationReason,
				MigrationBatchMessageSchema.MigrationJobPoisonCount,
				MigrationBatchMessageSchema.MigrationSuccessReportUrl,
				MigrationBatchMessageSchema.MigrationErrorReportUrl,
				MigrationBatchMessageSchema.MigrationJobIsStaged,
				MigrationBatchMessageSchema.MigrationJobItemSubscriptionLastChecked,
				MigrationBatchMessageSchema.MigrationReportSets,
				MigrationBatchMessageSchema.MigrationJobSourceEndpoint,
				MigrationBatchMessageSchema.MigrationJobTargetEndpoint,
				MigrationBatchMessageSchema.MigrationJobDirection,
				MigrationBatchMessageSchema.MigrationJobSkipSteps,
				MigrationBatchMessageSchema.MigrationJobCountCache,
				MigrationBatchMessageSchema.MigrationJobCountCacheFullScanTime,
				MigrationBatchMessageSchema.MigrationJobLastRestartTime,
				MigrationBatchMessageSchema.MigrationJobIsRunning,
				MigrationBatchMessageSchema.MigrationNextProcessTime,
				MigrationBatchMessageSchema.MigrationFlags,
				MigrationBatchMessageSchema.MigrationStage,
				MigrationBatchMessageSchema.MigrationWorkflow,
				MigrationBatchMessageSchema.MigrationExchangeObjectId,
				MigrationBatchMessageSchema.MigrationJobLastFinalizationAttempt
			},
			MigrationStatusData<MigrationJobStatus>.StatusPropertyDefinition,
			MigrationPersistableBase.MigrationBaseDefinitions
		});

		// Token: 0x04000185 RID: 389
		private static readonly PropertyDefinition[] MigrationJobTypeDefinition = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
		{
			new PropertyDefinition[]
			{
				MigrationBatchMessageSchema.MigrationType
			},
			MigrationPersistableBase.VersionPropertyDefinitions
		});

		// Token: 0x04000186 RID: 390
		private static readonly MigrationEqualityFilter MessageClassEqualityFilter = new MigrationEqualityFilter(StoreObjectSchema.ItemClass, MigrationBatchMessageSchema.MigrationJobClass);

		// Token: 0x04000187 RID: 391
		private static readonly PropertyDefinition[] JobLastScheduledPropertyDefinition = new PropertyDefinition[]
		{
			MigrationBatchMessageSchema.MigrationJobItemSubscriptionLastChecked
		};

		// Token: 0x04000188 RID: 392
		private static readonly PropertyDefinition[] JobMigrationFlagsPropertyDefinition = new PropertyDefinition[]
		{
			MigrationBatchMessageSchema.MigrationFlags
		};

		// Token: 0x04000189 RID: 393
		private static readonly PropertyDefinition[] JobCancelPropertyDefinition = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
		{
			new PropertyDefinition[]
			{
				MigrationBatchMessageSchema.MigrationJobCancelledFlag,
				MigrationBatchMessageSchema.MigrationJobCancellationReason
			},
			MigrationPersistableBase.MigrationBaseDefinitions
		});

		// Token: 0x0400018A RID: 394
		private static readonly PropertyDefinition[] JobFinalizePropertyDefinition = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
		{
			new PropertyDefinition[]
			{
				MigrationBatchMessageSchema.MigrationJobFinalizeTime,
				MigrationBatchMessageSchema.MigrationJobNotificationEmails,
				MigrationBatchMessageSchema.MigrationJobCancelledFlag,
				MigrationBatchMessageSchema.MigrationJobCancellationReason,
				MigrationBatchMessageSchema.MigrationJobPoisonCount,
				MigrationBatchMessageSchema.MigrationJobLastFinalizationAttempt
			},
			MigrationStatusData<MigrationJobStatus>.StatusPropertyDefinition
		});

		// Token: 0x0400018B RID: 395
		private static readonly PropertyDefinition[] JobStartPropertyDefinition = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
		{
			new PropertyDefinition[]
			{
				MigrationBatchMessageSchema.MigrationJobItemRowIndex,
				MigrationBatchMessageSchema.MigrationJobNotificationEmails,
				MigrationBatchMessageSchema.MigrationJobFinalizeTime,
				MigrationBatchMessageSchema.MigrationJobCancelledFlag,
				MigrationBatchMessageSchema.MigrationJobCancellationReason,
				MigrationBatchMessageSchema.MigrationJobPoisonCount
			},
			MigrationStatusData<MigrationJobStatus>.StatusPropertyDefinition,
			MigrationPersistableBase.MigrationBaseDefinitions
		});

		// Token: 0x0400018C RID: 396
		private static readonly PropertyDefinition[] JobSetReportIDDefinition = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
		{
			new PropertyDefinition[]
			{
				MigrationBatchMessageSchema.MigrationErrorReportUrl,
				MigrationBatchMessageSchema.MigrationSuccessReportUrl,
				MigrationBatchMessageSchema.MigrationReportSets
			},
			MigrationPersistableBase.MigrationBaseDefinitions
		});

		// Token: 0x0400018D RID: 397
		private static readonly PropertyDefinition[] JobCountCacheDefinition = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
		{
			new PropertyDefinition[]
			{
				MigrationBatchMessageSchema.MigrationJobCountCache,
				MigrationBatchMessageSchema.MigrationJobCountCacheFullScanTime
			},
			MigrationPersistableBase.MigrationBaseDefinitions
		});

		// Token: 0x0400018E RID: 398
		private static readonly SortBy[] SortByCreationTime = new SortBy[]
		{
			new SortBy(StoreObjectSchema.CreationTime, SortOrder.Ascending)
		};

		// Token: 0x0400018F RID: 399
		private MigrationType migrationJobType;

		// Token: 0x04000190 RID: 400
		private Guid jobId;

		// Token: 0x04000191 RID: 401
		private string jobName;

		// Token: 0x04000192 RID: 402
		private ADObjectId ownerId;

		// Token: 0x04000193 RID: 403
		private Guid ownerExchangeObjectId;

		// Token: 0x04000194 RID: 404
		private DelegatedPrincipal delegatedAdminOwner;

		// Token: 0x04000195 RID: 405
		private SubmittedByUserAdminType submittedByUserAdminType;

		// Token: 0x04000196 RID: 406
		private MigrationStatusData<MigrationJobStatus> statusData;

		// Token: 0x04000197 RID: 407
		private MigrationJobStatus status;

		// Token: 0x04000198 RID: 408
		private ExDateTime originalCreationTime;

		// Token: 0x04000199 RID: 409
		private ExDateTime? startTime;

		// Token: 0x0400019A RID: 410
		private ExDateTime? finalizeTime;

		// Token: 0x0400019B RID: 411
		private int lastFinalizationAttempt;

		// Token: 0x0400019C RID: 412
		private ExTimeZone userTimeZone;

		// Token: 0x0400019D RID: 413
		private string submittedByUser;

		// Token: 0x0400019E RID: 414
		private int totalRowCount;

		// Token: 0x0400019F RID: 415
		private MigrationCountCache cachedItemCounts;

		// Token: 0x040001A0 RID: 416
		private ExDateTime? fullScanTime;

		// Token: 0x040001A1 RID: 417
		private MultiValuedProperty<SmtpAddress> notificationEmails;

		// Token: 0x040001A2 RID: 418
		private JobCancellationStatus jobCancellationStatus;

		// Token: 0x040001A3 RID: 419
		private string lastCursorPosition;

		// Token: 0x040001A4 RID: 420
		private CultureInfo adminCulture;

		// Token: 0x040001A5 RID: 421
		private bool statisticsEnabled = true;

		// Token: 0x040001A6 RID: 422
		private bool isStaged;

		// Token: 0x040001A7 RID: 423
		private ExDateTime? lastScheduled;

		// Token: 0x040001A8 RID: 424
		private ExDateTime? nextProcessTime;

		// Token: 0x040001A9 RID: 425
		private long currentSupportedVersion;

		// Token: 0x0200005C RID: 92
		// (Invoke) Token: 0x0600053A RID: 1338
		private delegate bool SupportsActionDelegate(out LocalizedString? errorMessage);
	}
}
