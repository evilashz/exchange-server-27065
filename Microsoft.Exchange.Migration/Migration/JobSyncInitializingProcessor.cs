using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000110 RID: 272
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class JobSyncInitializingProcessor : JobProcessor
	{
		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x0003B230 File Offset: 0x00039430
		protected virtual bool IsProvisioningSupported
		{
			get
			{
				return base.Job.IsProvisioningSupported;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000E44 RID: 3652 RVA: 0x0003B23D File Offset: 0x0003943D
		protected virtual bool IsValidationSupported
		{
			get
			{
				return base.SubscriptionHandler.SupportsAdvancedValidation;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x0003B24A File Offset: 0x0003944A
		protected virtual bool IsValidationEnabled
		{
			get
			{
				return base.Job.UseAdvancedValidation;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x0003B257 File Offset: 0x00039457
		// (set) Token: 0x06000E47 RID: 3655 RVA: 0x0003B25F File Offset: 0x0003945F
		protected virtual int UpdatesEncountered { get; set; }

		// Token: 0x06000E48 RID: 3656 RVA: 0x0003B268 File Offset: 0x00039468
		internal static JobSyncInitializingProcessor CreateProcessor(MigrationType type)
		{
			if (type <= MigrationType.ExchangeOutlookAnywhere)
			{
				if (type == MigrationType.IMAP)
				{
					return new IMAPJobSyncInitializingProcessor();
				}
				if (type == MigrationType.ExchangeOutlookAnywhere)
				{
					return new ExchangeJobSyncInitializingProcessor();
				}
			}
			else
			{
				if (type == MigrationType.ExchangeRemoteMove || type == MigrationType.ExchangeLocalMove)
				{
					return new MoveJobSyncInitializingProcessor();
				}
				if (type == MigrationType.PublicFolder)
				{
					return new PublicFolderJobSyncInitializingProcessor();
				}
			}
			throw new ArgumentException("Invalid MigrationType " + type);
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0003B2C6 File Offset: 0x000394C6
		internal override bool Validate()
		{
			return true;
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0003B2C9 File Offset: 0x000394C9
		internal override MigrationJobStatus GetNextStageStatus()
		{
			if (this.IsValidationSupported)
			{
				return MigrationJobStatus.Validating;
			}
			if (this.IsProvisioningSupported)
			{
				return MigrationJobStatus.ProvisionStarting;
			}
			return MigrationJobStatus.SyncStarting;
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x0003B2E2 File Offset: 0x000394E2
		internal override void OnComplete()
		{
			if (base.Job.DisallowExistingUsers)
			{
				base.Job.DisallowExistingUsers = false;
				base.Job.SaveBatchFlagsAndNotificationId(base.DataProvider);
			}
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0003B310 File Offset: 0x00039510
		protected sealed override LegacyMigrationJobProcessorResponse Process(bool scheduleNewWork)
		{
			this.AutoCancelIfTooManyErrors();
			return base.ProcessActions(scheduleNewWork, new Func<bool, LegacyMigrationJobProcessorResponse>[]
			{
				new Func<bool, LegacyMigrationJobProcessorResponse>(this.MoveReports),
				new Func<bool, LegacyMigrationJobProcessorResponse>(this.CreatePendingJobItems),
				new Func<bool, LegacyMigrationJobProcessorResponse>(this.ResumeJobItems),
				new Func<bool, LegacyMigrationJobProcessorResponse>(this.UpdateJobItems)
			});
		}

		// Token: 0x06000E4D RID: 3661
		protected abstract IMigrationDataRowProvider GetMigrationDataRowProvider();

		// Token: 0x06000E4E RID: 3662
		protected abstract void CreateNewJobItem(IMigrationDataRow dataRow);

		// Token: 0x06000E4F RID: 3663
		protected abstract MigrationBatchError ProcessExistingJobItem(MigrationJobItem jobItem, IMigrationDataRow dataRow);

		// Token: 0x06000E50 RID: 3664 RVA: 0x0003B370 File Offset: 0x00039570
		protected virtual MigrationBatchError HandleDuplicateMigrationDataRow(MigrationJobItem jobItem, IMigrationDataRow dataRow)
		{
			if (base.Job.BatchInputId == jobItem.BatchInputId && jobItem.CursorPosition == dataRow.CursorPosition)
			{
				MigrationLogger.Log(MigrationEventType.Information, "somehow we're reprocessing the same item {0}", new object[]
				{
					jobItem
				});
				return null;
			}
			LocalizedString locErrorString;
			if (string.Equals(jobItem.Identifier, dataRow.Identifier, StringComparison.OrdinalIgnoreCase))
			{
				if (jobItem.MigrationJobId != base.Job.JobId)
				{
					string text;
					try
					{
						text = base.Session.GetJobName(jobItem.MigrationJobId);
					}
					catch (MigrationJobNotFoundException)
					{
						text = null;
					}
					if (!string.IsNullOrEmpty(text))
					{
						locErrorString = Strings.UserDuplicateInOtherBatch(dataRow.Identifier, text);
					}
					else
					{
						locErrorString = Strings.UserDuplicateOrphanedFromBatch(dataRow.Identifier);
					}
				}
				else
				{
					locErrorString = Strings.UserDuplicateInCSV(dataRow.Identifier);
				}
			}
			else
			{
				locErrorString = Strings.UserAlreadyMigratedWithAlternateEmail(jobItem.Identifier);
			}
			return this.GetValidationError(dataRow, locErrorString);
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x0003B458 File Offset: 0x00039658
		protected virtual void HandleProvisioningCompletedEvent(ProvisionedObject provisionedObj, MigrationJobItem jobItem)
		{
			if (provisionedObj.Succeeded)
			{
				MigrationUserStatus value = MigrationUserStatus.Queued;
				jobItem.SetUserMailboxProperties(base.DataProvider, new MigrationUserStatus?(value), (MailboxData)provisionedObj.MailboxData, null, new ExDateTime?(ExDateTime.UtcNow));
				return;
			}
			jobItem.SetUserMailboxProperties(base.DataProvider, new MigrationUserStatus?(MigrationUserStatus.Failed), (MailboxData)provisionedObj.MailboxData, new ProvisioningFailedException(new LocalizedString(provisionedObj.Error)), null);
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x0003B4D4 File Offset: 0x000396D4
		protected virtual MigrationBatchError GetValidationError(IMigrationDataRow dataRow, LocalizedString locErrorString)
		{
			return new MigrationBatchError
			{
				RowIndex = -1,
				EmailAddress = dataRow.Identifier,
				LocalizedErrorMessage = locErrorString
			};
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0003B504 File Offset: 0x00039704
		protected virtual MigrationBatchError ValidateDataRow(IMigrationDataRow row)
		{
			InvalidDataRow invalidDataRow = row as InvalidDataRow;
			if (invalidDataRow != null)
			{
				return invalidDataRow.Error;
			}
			return null;
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x0003B524 File Offset: 0x00039724
		protected virtual LegacyMigrationJobProcessorResponse ResumeJobItems(bool scheduleNewWork)
		{
			return LegacyMigrationJobProcessorResponse.Create(MigrationProcessorResult.Completed, null);
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0003B540 File Offset: 0x00039740
		protected virtual LegacyMigrationJobProcessorResponse UpdateJobItems(bool scheduleNewWork)
		{
			return LegacyMigrationJobProcessorResponse.Create(MigrationProcessorResult.Completed, null);
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x0003B55C File Offset: 0x0003975C
		private LegacyMigrationJobProcessorResponse MoveReports(bool scheduleNewWork)
		{
			LegacyMigrationJobProcessorResponse legacyMigrationJobProcessorResponse = LegacyMigrationJobProcessorResponse.Create(MigrationProcessorResult.Completed, null);
			if (!base.Session.Config.IsSupported(MigrationFeature.MultiBatch))
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "single-batch behavior.  do not move reports to new job", new object[0]);
				return legacyMigrationJobProcessorResponse;
			}
			if (base.Job.OriginalJobId == null || base.Job.JobId == base.Job.OriginalJobId.Value)
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "batch ids match up, no need to move reports", new object[0]);
				return legacyMigrationJobProcessorResponse;
			}
			using (IMigrationDataProvider providerForFolder = base.DataProvider.GetProviderForFolder(MigrationFolderName.SyncMigrationReports))
			{
				foreach (MigrationReportItem migrationReportItem in MigrationReportItem.GetByJobId(providerForFolder, new Guid?(base.Job.OriginalJobId.Value), ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("SyncMigrationCancellationBatchSize")))
				{
					if (legacyMigrationJobProcessorResponse.Result == MigrationProcessorResult.Completed)
					{
						legacyMigrationJobProcessorResponse.Result = MigrationProcessorResult.Working;
					}
					MigrationLogger.Log(MigrationEventType.Information, "updating report {0} to point from {1} to {2}", new object[]
					{
						migrationReportItem.ReportName,
						migrationReportItem.JobId,
						base.Job.JobId
					});
					migrationReportItem.UpdateReportItem(providerForFolder, base.Job.JobId);
					legacyMigrationJobProcessorResponse.NumItemsProcessed++;
				}
			}
			return legacyMigrationJobProcessorResponse;
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x0003B710 File Offset: 0x00039910
		private LegacyMigrationJobProcessorResponse CreatePendingJobItems(bool scheduleNewWork)
		{
			LegacyMigrationJobProcessorResponse legacyMigrationJobProcessorResponse = LegacyMigrationJobProcessorResponse.Create(MigrationProcessorResult.Completed, null);
			legacyMigrationJobProcessorResponse.NumItemsProcessed = new int?(0);
			legacyMigrationJobProcessorResponse.NumItemsOutstanding = new int?(0);
			if (!base.Job.ShouldProcessDataRows || base.Job.IsDataRowProcessingDone())
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "Job {0} no more data to read, so creation is finished", new object[]
				{
					base.Job
				});
				return legacyMigrationJobProcessorResponse;
			}
			this.UpdatesEncountered = 0;
			legacyMigrationJobProcessorResponse.Result = MigrationProcessorResult.Working;
			List<MigrationBatchError> list = new List<MigrationBatchError>();
			IMigrationDataRow migrationDataRow = null;
			try
			{
				int config = ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("MaxRowsToProcessInOnePass");
				ExDateTime t = ExDateTime.UtcNow + ConfigBase<MigrationServiceConfigSchema>.GetConfig<TimeSpan>("MaxTimeToProcessInOnePass");
				IMigrationDataRowProvider migrationDataRowProvider = this.GetMigrationDataRowProvider();
				foreach (IMigrationDataRow migrationDataRow2 in migrationDataRowProvider.GetNextBatchItem(base.Job.LastCursorPosition, config))
				{
					MigrationBatchError migrationBatchError = this.ProcessDataRow(migrationDataRow2);
					if (migrationBatchError != null)
					{
						list.Add(migrationBatchError);
					}
					migrationDataRow = migrationDataRow2;
					legacyMigrationJobProcessorResponse.NumItemsProcessed++;
					if (legacyMigrationJobProcessorResponse.NumItemsProcessed.Value >= config || ExDateTime.UtcNow >= t)
					{
						break;
					}
				}
				if (migrationDataRow == null)
				{
					base.Job.SetDataRowProcessingDone(base.DataProvider, list, this.UpdatesEncountered);
				}
			}
			finally
			{
				string text = (migrationDataRow == null) ? null : migrationDataRow.CursorPosition.ToString(CultureInfo.InvariantCulture);
				if (text != null || list.Count > 0)
				{
					base.Job.UpdateLastProcessedRow(base.DataProvider, text, list, this.UpdatesEncountered);
				}
				MigrationLogger.Log(MigrationEventType.Verbose, "JobInitiatedProcessor.Process: Job {0} processed {1} rows in this pass", new object[]
				{
					base.Job,
					legacyMigrationJobProcessorResponse.NumItemsProcessed.Value
				});
			}
			return legacyMigrationJobProcessorResponse;
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x0003B91C File Offset: 0x00039B1C
		private MigrationBatchError ProcessDataRow(IMigrationDataRow row)
		{
			MigrationBatchError migrationBatchError = this.ValidateDataRow(row);
			if (migrationBatchError != null)
			{
				return migrationBatchError;
			}
			MigrationJobItem jobItemByEmailAddress = MigrationServiceHelper.GetJobItemByEmailAddress(base.DataProvider, row.Identifier, true);
			if (jobItemByEmailAddress == null)
			{
				this.CreateNewJobItem(row);
				return null;
			}
			if (jobItemByEmailAddress.MigrationType != base.Job.MigrationType)
			{
				return this.GetValidationError(row, Strings.MigrationUserAlreadyExistsInDifferentType(jobItemByEmailAddress.JobName, jobItemByEmailAddress.MigrationType.ToString()));
			}
			if (jobItemByEmailAddress.MigrationJobId != base.Job.JobId && (base.Job.BatchFlags & MigrationBatchFlags.DisallowExistingUsers) == MigrationBatchFlags.DisallowExistingUsers)
			{
				return this.HandleDuplicateMigrationDataRow(jobItemByEmailAddress, row);
			}
			if (!string.IsNullOrEmpty(base.Job.BatchInputId) && base.Job.BatchInputId.Equals(jobItemByEmailAddress.BatchInputId))
			{
				return this.HandleDuplicateMigrationDataRow(jobItemByEmailAddress, row);
			}
			return this.ProcessExistingJobItem(jobItemByEmailAddress, row);
		}
	}
}
