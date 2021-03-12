using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Migration;
using Microsoft.Exchange.Migration.DataAccessLayer;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x020004EC RID: 1260
	[Cmdlet("Set", "MigrationBatch", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetMigrationBatch : SetMigrationObjectTaskBase<MigrationBatchIdParameter, MigrationBatch, MigrationBatch>
	{
		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06002C74 RID: 11380 RVA: 0x000B1C7F File Offset: 0x000AFE7F
		// (set) Token: 0x06002C75 RID: 11381 RVA: 0x000B1C96 File Offset: 0x000AFE96
		[Parameter(Mandatory = false)]
		public bool? AllowIncrementalSyncs
		{
			get
			{
				return (bool?)base.Fields["AllowIncrementalSyncs"];
			}
			set
			{
				base.Fields["AllowIncrementalSyncs"] = value;
			}
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06002C76 RID: 11382 RVA: 0x000B1CAE File Offset: 0x000AFEAE
		// (set) Token: 0x06002C77 RID: 11383 RVA: 0x000B1CC5 File Offset: 0x000AFEC5
		[Parameter(Mandatory = false)]
		public int? AutoRetryCount
		{
			get
			{
				return (int?)base.Fields["AutoRetryCount"];
			}
			set
			{
				base.Fields["AutoRetryCount"] = value;
			}
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06002C78 RID: 11384 RVA: 0x000B1CDD File Offset: 0x000AFEDD
		// (set) Token: 0x06002C79 RID: 11385 RVA: 0x000B1CF4 File Offset: 0x000AFEF4
		[Parameter(Mandatory = false)]
		public byte[] CSVData
		{
			get
			{
				return (byte[])base.Fields["dataBlob"];
			}
			set
			{
				base.Fields["dataBlob"] = value;
			}
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06002C7A RID: 11386 RVA: 0x000B1D07 File Offset: 0x000AFF07
		// (set) Token: 0x06002C7B RID: 11387 RVA: 0x000B1D28 File Offset: 0x000AFF28
		[Parameter(Mandatory = false)]
		public bool AllowUnknownColumnsInCsv
		{
			get
			{
				return (bool)(base.Fields["AllowUnknownColumnsInCsv"] ?? false);
			}
			set
			{
				base.Fields["AllowUnknownColumnsInCsv"] = value;
			}
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06002C7C RID: 11388 RVA: 0x000B1D40 File Offset: 0x000AFF40
		// (set) Token: 0x06002C7D RID: 11389 RVA: 0x000B1D57 File Offset: 0x000AFF57
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<SmtpAddress> NotificationEmails
		{
			get
			{
				return (MultiValuedProperty<SmtpAddress>)base.Fields["NotificationEmails"];
			}
			set
			{
				base.Fields["NotificationEmails"] = value;
			}
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06002C7E RID: 11390 RVA: 0x000B1D6A File Offset: 0x000AFF6A
		// (set) Token: 0x06002C7F RID: 11391 RVA: 0x000B1D90 File Offset: 0x000AFF90
		[Parameter(Mandatory = false)]
		public Unlimited<int> BadItemLimit
		{
			get
			{
				return (Unlimited<int>)(base.Fields["BadItemLimit"] ?? new Unlimited<int>(0));
			}
			set
			{
				base.Fields["BadItemLimit"] = value;
			}
		}

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x06002C80 RID: 11392 RVA: 0x000B1DA8 File Offset: 0x000AFFA8
		// (set) Token: 0x06002C81 RID: 11393 RVA: 0x000B1DCE File Offset: 0x000AFFCE
		[Parameter(Mandatory = false)]
		public Unlimited<int> LargeItemLimit
		{
			get
			{
				return (Unlimited<int>)(base.Fields["LargeItemLimit"] ?? new Unlimited<int>(0));
			}
			set
			{
				base.Fields["LargeItemLimit"] = value;
			}
		}

		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x06002C82 RID: 11394 RVA: 0x000B1DE6 File Offset: 0x000AFFE6
		// (set) Token: 0x06002C83 RID: 11395 RVA: 0x000B1DFD File Offset: 0x000AFFFD
		[Parameter(Mandatory = false)]
		public DateTime? StartAfter
		{
			get
			{
				return (DateTime?)base.Fields["StartAfter"];
			}
			set
			{
				base.Fields["StartAfter"] = value;
			}
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x06002C84 RID: 11396 RVA: 0x000B1E15 File Offset: 0x000B0015
		// (set) Token: 0x06002C85 RID: 11397 RVA: 0x000B1E2C File Offset: 0x000B002C
		[Parameter(Mandatory = false)]
		public DateTime? CompleteAfter
		{
			get
			{
				return (DateTime?)base.Fields["CompleteAfter"];
			}
			set
			{
				base.Fields["CompleteAfter"] = value;
			}
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06002C86 RID: 11398 RVA: 0x000B1E44 File Offset: 0x000B0044
		// (set) Token: 0x06002C87 RID: 11399 RVA: 0x000B1E5B File Offset: 0x000B005B
		[Parameter(Mandatory = false)]
		public TimeSpan? ReportInterval
		{
			get
			{
				return (TimeSpan?)base.Fields["ReportInterval"];
			}
			set
			{
				base.Fields["ReportInterval"] = value;
			}
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x06002C88 RID: 11400 RVA: 0x000B1E73 File Offset: 0x000B0073
		// (set) Token: 0x06002C89 RID: 11401 RVA: 0x000B1E94 File Offset: 0x000B0094
		[Parameter(Mandatory = false)]
		public bool UseAdvancedValidation
		{
			get
			{
				return (bool)(base.Fields["UseAdvancedValidation"] ?? false);
			}
			set
			{
				base.Fields["UseAdvancedValidation"] = value;
			}
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x06002C8A RID: 11402 RVA: 0x000B1EAC File Offset: 0x000B00AC
		// (set) Token: 0x06002C8B RID: 11403 RVA: 0x000B1EC3 File Offset: 0x000B00C3
		[Parameter(Mandatory = false)]
		public DatabaseIdParameter SourcePublicFolderDatabase
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["SourcePublicFolderDatabase"];
			}
			set
			{
				base.Fields["SourcePublicFolderDatabase"] = value;
			}
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x06002C8C RID: 11404 RVA: 0x000B1ED6 File Offset: 0x000B00D6
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.DataObject != null)
				{
					return Strings.ConfirmationMessageSetMigrationBatch(this.DataObject.Identity.ToString());
				}
				return Strings.ConfirmationMessageSetMigrationBatch(this.Identity.ToString());
			}
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x000B1F06 File Offset: 0x000B0106
		protected override bool IsObjectStateChanged()
		{
			return this.changed;
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x000B1F10 File Offset: 0x000B0110
		protected override IConfigDataProvider CreateSession()
		{
			MigrationLogger.Initialize();
			MigrationLogContext.Current.Source = "Set-MigrationBatch";
			MigrationLogContext.Current.Organization = base.CurrentOrganizationId.OrganizationalUnit;
			return MigrationBatchDataProvider.CreateDataProvider("SetMigrationBatch", base.TenantGlobalCatalogSession, null, this.partitionMailbox);
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x000B1F65 File Offset: 0x000B0165
		protected override void InternalStateReset()
		{
			this.DisposeSession();
			base.InternalStateReset();
		}

		// Token: 0x06002C90 RID: 11408 RVA: 0x000B1F74 File Offset: 0x000B0174
		protected override void InternalValidate()
		{
			MigrationBatchDataProvider migrationBatchDataProvider = (MigrationBatchDataProvider)base.DataSession;
			bool flag = migrationBatchDataProvider.MigrationSession.Config.IsSupported(MigrationFeature.PAW);
			migrationBatchDataProvider.MigrationJob = MigrationObjectTaskBase<MigrationBatchIdParameter>.GetAndValidateMigrationJob(this, (MigrationBatchDataProvider)base.DataSession, this.Identity, true, true);
			if (migrationBatchDataProvider.MigrationJob == null)
			{
				MigrationObjectTaskBase<MigrationBatchIdParameter>.WriteJobNotFoundError(this, this.Identity.RawIdentity);
			}
			LocalizedString? localizedString;
			if (!migrationBatchDataProvider.MigrationJob.SupportsSetting(out localizedString))
			{
				if (localizedString == null)
				{
					localizedString = new LocalizedString?(Strings.MigrationOperationFailed);
				}
				base.WriteError(new MigrationPermanentException(localizedString.Value));
				migrationBatchDataProvider.MigrationJob = null;
			}
			if (this.CSVData != null && !migrationBatchDataProvider.MigrationJob.SupportsAppendingUsers(out localizedString))
			{
				if (localizedString == null)
				{
					localizedString = new LocalizedString?(Strings.MigrationOperationFailed);
				}
				base.WriteError(new MigrationPermanentException(localizedString.Value));
				migrationBatchDataProvider.MigrationJob = null;
			}
			if (this.AllowIncrementalSyncs != null)
			{
				if (migrationBatchDataProvider.MigrationJob.Status == MigrationJobStatus.Stopped && !migrationBatchDataProvider.MigrationJob.AutoStop && this.AllowIncrementalSyncs.Value)
				{
					base.WriteError(new MigrationPermanentException(Strings.MigrationPleaseUseStartForReenablingIncremental));
				}
				else if (migrationBatchDataProvider.MigrationJob.Status == MigrationJobStatus.SyncCompleted && !this.AllowIncrementalSyncs.Value)
				{
					base.WriteError(new MigrationPermanentException(Strings.MigrationPleaseUseStopForDisablingIncremental));
				}
				if (migrationBatchDataProvider.MigrationJob.Status != MigrationJobStatus.Created && !MigrationJobStage.Sync.IsStatusSupported(migrationBatchDataProvider.MigrationJob.Status) && !MigrationJobStage.Incremental.IsStatusSupported(migrationBatchDataProvider.MigrationJob.Status))
				{
					base.WriteError(new MigrationPermanentException(Strings.MigrationAutoStopForInProgressOnly));
				}
				if (this.AllowIncrementalSyncs.Value != migrationBatchDataProvider.MigrationJob.AutoStop)
				{
					base.WriteError(new MigrationPermanentException(Strings.MigrationAutoStopAlreadySet));
				}
			}
			if (migrationBatchDataProvider.MigrationJob.MigrationType == MigrationType.ExchangeLocalMove && base.IsFieldSet("LargeItemLimit"))
			{
				base.WriteError(new MigrationPermanentException(Strings.MigrationNoLargeItemLimitForLocalBatches));
			}
			if (this.ReportInterval != null && !flag)
			{
				base.WriteError(new MigrationPermanentException(Strings.MigrationReportIntervalParameterInvalid));
			}
			if (base.IsFieldSet("SourcePublicFolderDatabase"))
			{
				this.ValidateSourcePublicFolderDatabase(migrationBatchDataProvider.MigrationJob);
			}
			this.ValidateSchedulingParameters(migrationBatchDataProvider.MigrationJob);
			base.InternalValidate();
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x000B220C File Offset: 0x000B040C
		protected override void InternalProcessRecord()
		{
			MigrationBatchDataProvider batchProvider = (MigrationBatchDataProvider)base.DataSession;
			bool flag = batchProvider.MigrationSession.Config.IsSupported(MigrationFeature.PAW);
			bool flag2 = false;
			bool updateEmails = false;
			bool flag3 = false;
			bool flag4 = false;
			if (flag && this.ReportInterval != null)
			{
				this.DataObject.ReportInterval = new TimeSpan?(this.ReportInterval.Value);
				flag2 = true;
			}
			if (base.Fields.IsModified("NotificationEmails"))
			{
				MultiValuedProperty<SmtpAddress> updatedNotificationEmails = MigrationObjectTaskBase<MigrationBatchIdParameter>.GetUpdatedNotificationEmails(this, base.TenantGlobalCatalogSession, this.NotificationEmails);
				if (updatedNotificationEmails != null && updatedNotificationEmails.Count != 0)
				{
					this.DataObject.NotificationEmails = updatedNotificationEmails;
					flag2 = true;
					updateEmails = true;
				}
			}
			if (base.Fields.IsModified("AllowUnknownColumnsInCsv"))
			{
				this.DataObject.AllowUnknownColumnsInCsv = this.AllowUnknownColumnsInCsv;
				flag2 = true;
			}
			if (this.CSVData != null)
			{
				this.InternalProcessCsv(batchProvider);
				flag2 = true;
			}
			if (this.AllowIncrementalSyncs != null)
			{
				if (!this.AllowIncrementalSyncs.Value)
				{
					this.DataObject.BatchFlags |= MigrationBatchFlags.AutoStop;
				}
				else
				{
					this.DataObject.BatchFlags &= ~MigrationBatchFlags.AutoStop;
				}
				flag2 = true;
			}
			if (base.IsFieldSet("UseAdvancedValidation"))
			{
				if (this.UseAdvancedValidation)
				{
					this.DataObject.BatchFlags |= MigrationBatchFlags.UseAdvancedValidation;
				}
				else
				{
					this.DataObject.BatchFlags &= ~MigrationBatchFlags.UseAdvancedValidation;
				}
				flag2 = true;
			}
			if (this.AutoRetryCount != null)
			{
				this.DataObject.AutoRetryCount = this.AutoRetryCount;
				flag2 = true;
			}
			if (base.IsFieldSet("BadItemLimit") && !this.BadItemLimit.Equals(this.DataObject.BadItemLimit))
			{
				this.DataObject.BadItemLimit = this.BadItemLimit;
				flag3 = true;
				flag2 = true;
			}
			if (base.IsFieldSet("LargeItemLimit") && !this.LargeItemLimit.Equals(this.DataObject.LargeItemLimit))
			{
				this.DataObject.LargeItemLimit = this.LargeItemLimit;
				flag3 = true;
				flag2 = true;
			}
			if (base.IsFieldSet("StartAfter"))
			{
				this.DataObject.StartAfter = this.StartAfter;
				this.DataObject.StartAfterUTC = (DateTime?)MigrationHelper.GetUniversalDateTime((ExDateTime?)this.StartAfter);
				flag3 = true;
				flag2 = true;
				flag4 = true;
			}
			if (base.IsFieldSet("CompleteAfter"))
			{
				DateTime? completeAfterUTC = (DateTime?)MigrationHelper.GetUniversalDateTime((ExDateTime?)this.CompleteAfter);
				this.DataObject.CompleteAfter = this.CompleteAfter;
				this.DataObject.CompleteAfterUTC = completeAfterUTC;
				flag3 = true;
				flag2 = true;
				flag4 = true;
			}
			if (base.IsFieldSet("SourcePublicFolderDatabase"))
			{
				this.DataObject.SourcePublicFolderDatabase = this.SourcePublicFolderDatabase.RawIdentity;
				flag2 = true;
				flag4 = true;
				flag3 = true;
			}
			if (flag3)
			{
				this.DataObject.SubscriptionSettingsModified = (DateTime)ExDateTime.UtcNow;
			}
			if (flag2)
			{
				MigrationHelper.RunUpdateOperation(delegate
				{
					batchProvider.MigrationJob.UpdateJob(batchProvider.MailboxProvider, updateEmails, this.DataObject);
				});
				batchProvider.MigrationJob.ReportData.Append(Strings.MigrationReportJobModifiedByUser(base.ExecutingUserIdentityName));
				this.changed = true;
				if (flag4)
				{
					MigrationObjectTaskBase<MigrationBatchIdParameter>.RegisterMigrationBatch(this, batchProvider.MailboxSession, base.CurrentOrganizationId, false, false);
				}
			}
			base.InternalProcessRecord();
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x000B2584 File Offset: 0x000B0784
		private void InternalProcessCsv(MigrationBatchDataProvider batchProvider)
		{
			MigrationCsvSchemaBase migrationCsvSchemaBase = MigrationCSVDataRowProvider.CreateCsvSchema(batchProvider.MigrationJob);
			if (migrationCsvSchemaBase == null)
			{
				base.WriteError(new MigrationPermanentException(Strings.MigrationCSVNotAllowed));
			}
			LocalizedException ex = MigrationObjectTaskBase<MigrationBatchIdParameter>.ProcessCsv(((MigrationBatchDataProvider)base.DataSession).MailboxProvider, this.DataObject, migrationCsvSchemaBase, this.CSVData);
			if (ex != null)
			{
				base.WriteError(ex);
			}
		}

		// Token: 0x06002C93 RID: 11411 RVA: 0x000B25E0 File Offset: 0x000B07E0
		private void ValidateSchedulingParameters(MigrationJob migrationJob)
		{
			DateTime? dateTime = null;
			DateTime? dateTime2 = null;
			bool flag = false;
			bool flag2 = false;
			MigrationType migrationType = migrationJob.MigrationType;
			if (migrationType <= MigrationType.ExchangeOutlookAnywhere)
			{
				if (migrationType != MigrationType.IMAP)
				{
					if (migrationType == MigrationType.ExchangeOutlookAnywhere)
					{
						ExchangeJobSubscriptionSettings exchangeJobSubscriptionSettings = migrationJob.SubscriptionSettings as ExchangeJobSubscriptionSettings;
						if (exchangeJobSubscriptionSettings != null)
						{
							dateTime = (DateTime?)exchangeJobSubscriptionSettings.StartAfter;
						}
						flag = true;
					}
				}
				else if (migrationJob.IsPAW)
				{
					IMAPPAWJobSubscriptionSettings imappawjobSubscriptionSettings = migrationJob.SubscriptionSettings as IMAPPAWJobSubscriptionSettings;
					if (imappawjobSubscriptionSettings != null)
					{
						dateTime = (DateTime?)imappawjobSubscriptionSettings.StartAfter;
						dateTime2 = (DateTime?)imappawjobSubscriptionSettings.CompleteAfter;
					}
					flag = true;
					flag2 = true;
				}
			}
			else if (migrationType == MigrationType.ExchangeRemoteMove || migrationType == MigrationType.ExchangeLocalMove)
			{
				MoveJobSubscriptionSettings moveJobSubscriptionSettings = migrationJob.SubscriptionSettings as MoveJobSubscriptionSettings;
				if (moveJobSubscriptionSettings != null)
				{
					dateTime = (DateTime?)moveJobSubscriptionSettings.StartAfter;
					dateTime2 = (DateTime?)moveJobSubscriptionSettings.CompleteAfter;
				}
				flag = true;
				flag2 = true;
			}
			if (base.IsFieldSet("StartAfter") && !flag)
			{
				base.WriteError(new LocalizedException(Strings.MigrationStartAfterIncorrectMigrationType));
			}
			if (base.IsFieldSet("CompleteAfter") && !flag2)
			{
				base.WriteError(new LocalizedException(Strings.MigrationCompleteAfterIncorrectMigrationType));
			}
			bool flag3 = !migrationJob.IsPAW && !migrationJob.AutoComplete;
			if (base.IsFieldSet("StartAfter") && flag3)
			{
				base.WriteError(new LocalizedException(Strings.MigrationStartAfterScheduledBatchesOnly));
			}
			if (base.IsFieldSet("CompleteAfter") && flag3)
			{
				base.WriteError(new LocalizedException(Strings.MigrationCompleteAfterScheduledBatchesOnly));
			}
			if (base.IsFieldSet("StartAfter"))
			{
				if (migrationJob.Status != MigrationJobStatus.Created && migrationJob.Status != MigrationJobStatus.Failed && !migrationJob.IsPAW)
				{
					base.WriteError(new LocalizedException(Strings.MigrationStartAfterIncorrectState(migrationJob.Status.ToString())));
				}
				if (this.StartAfter != null)
				{
					RequestTaskHelper.ValidateStartAfterTime(this.StartAfter.Value.ToUniversalTime(), new Task.TaskErrorLoggingDelegate(base.WriteError), DateTime.UtcNow);
				}
			}
			if (base.IsFieldSet("CompleteAfter"))
			{
				if (migrationJob.Status != MigrationJobStatus.Created && migrationJob.Status != MigrationJobStatus.Failed && migrationJob.Status != MigrationJobStatus.SyncInitializing && migrationJob.Status != MigrationJobStatus.SyncStarting && migrationJob.Status != MigrationJobStatus.SyncCompleting && migrationJob.Status != MigrationJobStatus.SyncCompleted && migrationJob.Status != MigrationJobStatus.ProvisionStarting && migrationJob.Status != MigrationJobStatus.Validating && migrationJob.Status != MigrationJobStatus.Stopped && !migrationJob.IsPAW)
				{
					base.WriteError(new LocalizedException(Strings.MigrationCompleteAfterIncorrectState));
				}
				if (this.CompleteAfter != null)
				{
					RequestTaskHelper.ValidateCompleteAfterTime(this.CompleteAfter.Value.ToUniversalTime(), new Task.TaskErrorLoggingDelegate(base.WriteError), DateTime.UtcNow);
				}
				if (dateTime2 != null && DateTime.UtcNow.AddHours(1.0) > dateTime2.Value.ToUniversalTime())
				{
					this.WriteWarning(Strings.MigrationSettingCompeteAfterWithCurrentCompleteAfterInLessThanOneHour);
				}
				if (dateTime2 != null && this.CompleteAfter != null && this.CompleteAfter.Value.ToUniversalTime() < dateTime2.Value.ToUniversalTime())
				{
					this.WriteWarning(Strings.MigrationCompleteAfterChangedToEarlierTime);
				}
			}
			if ((base.IsFieldSet("StartAfter") || base.IsFieldSet("CompleteAfter")) && (this.StartAfter != null || dateTime != null) && (this.CompleteAfter != null || dateTime2 != null))
			{
				DateTime? dateTime3 = this.StartAfter ?? dateTime;
				DateTime? dateTime4 = this.CompleteAfter ?? dateTime2;
				RequestTaskHelper.ValidateStartAfterComesBeforeCompleteAfter(new DateTime?(dateTime3.Value.ToUniversalTime()), new DateTime?(dateTime4.Value.ToUniversalTime()), new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x000B2A10 File Offset: 0x000B0C10
		private void ValidateSourcePublicFolderDatabase(MigrationJob job)
		{
			if (job.MigrationType != MigrationType.PublicFolder || job.JobDirection != MigrationBatchDirection.Local)
			{
				base.WriteError(new MigrationPermanentException(Strings.ErrorInvalidBatchParameter("SourcePublicFolderDatabase", job.MigrationType.ToString(), job.JobDirection.ToString())));
			}
			PublicFolderDatabase publicFolderDatabase = (PublicFolderDatabase)base.GetDataObject<PublicFolderDatabase>(this.SourcePublicFolderDatabase, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(this.SourcePublicFolderDatabase.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(this.SourcePublicFolderDatabase.ToString())));
			using (IMailbox mailbox = PublicFolderEndpoint.ConnectToLocalSourceDatabase(publicFolderDatabase.ExchangeObjectId))
			{
				mailbox.Disconnect();
			}
		}

		// Token: 0x04002056 RID: 8278
		private const string ParameterBadItemLimit = "BadItemLimit";

		// Token: 0x04002057 RID: 8279
		private const string ParameterLargeItemLimit = "LargeItemLimit";

		// Token: 0x04002058 RID: 8280
		private const string ParameterStartAfter = "StartAfter";

		// Token: 0x04002059 RID: 8281
		private const string ParameterCompleteAfter = "CompleteAfter";

		// Token: 0x0400205A RID: 8282
		private const string ParameterReportInterval = "ReportInterval";

		// Token: 0x0400205B RID: 8283
		private const string ParameterNotificationEmails = "NotificationEmails";

		// Token: 0x0400205C RID: 8284
		private const string ParameterAllowUnknownColumnsInCsv = "AllowUnknownColumnsInCsv";

		// Token: 0x0400205D RID: 8285
		private const string ParameterUseAdvancedValidation = "UseAdvancedValidation";

		// Token: 0x0400205E RID: 8286
		private const string ParameterNameSourcePublicFolderDatabase = "SourcePublicFolderDatabase";

		// Token: 0x0400205F RID: 8287
		private bool changed;
	}
}
