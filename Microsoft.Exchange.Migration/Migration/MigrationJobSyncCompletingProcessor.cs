using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200011B RID: 283
	internal class MigrationJobSyncCompletingProcessor : JobSyncCompletingProcessor
	{
		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x0003E266 File Offset: 0x0003C466
		protected override bool IsBatchSyncedReport
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x0003E269 File Offset: 0x0003C469
		protected override MigrationUserStatus[] JobItemStatesForSuccess
		{
			get
			{
				return MigrationJobSyncCompletingProcessor.JobItemStatusForSuccess;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x0003E270 File Offset: 0x0003C470
		protected override MigrationUserStatus[] JobItemStatesForFailure
		{
			get
			{
				return MigrationJob.JobItemStatusForBatchCompletionErrors;
			}
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x0003E278 File Offset: 0x0003C478
		internal static MigrationJobSyncCompletingProcessor CreateProcessor(MigrationType type)
		{
			if (type <= MigrationType.ExchangeOutlookAnywhere)
			{
				if (type == MigrationType.IMAP)
				{
					return new IMAPJobSyncCompletingProcessor();
				}
				if (type != MigrationType.ExchangeOutlookAnywhere)
				{
					goto IL_30;
				}
			}
			else if (type != MigrationType.ExchangeRemoteMove && type != MigrationType.ExchangeLocalMove && type != MigrationType.PublicFolder)
			{
				goto IL_30;
			}
			return new MigrationJobSyncCompletingProcessor();
			IL_30:
			throw new ArgumentException("Invalid MigrationType " + type);
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x0003E2CA File Offset: 0x0003C4CA
		internal override bool Validate()
		{
			return base.Job != null && base.Job.Status == MigrationJobStatus.SyncCompleting;
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0003E2E4 File Offset: 0x0003C4E4
		internal override MigrationJobStatus GetNextStageStatus()
		{
			if (base.Job.TryAutoRetryStartedJob(base.DataProvider))
			{
				return MigrationJobStatus.SyncInitializing;
			}
			return MigrationJobStatus.SyncCompleted;
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0003E2FC File Offset: 0x0003C4FC
		protected override LegacyMigrationJobProcessorResponse Process(bool scheduleNewWork)
		{
			MigrationJobReportingCursor migrationJobReportingCursor;
			return base.Process(out migrationJobReportingCursor);
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0003E311 File Offset: 0x0003C511
		protected override string GetTemplateName(bool areErrorsPresent)
		{
			return "BatchCompletedReport.htm";
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x0003E318 File Offset: 0x0003C518
		protected override string GetEmailSubject(bool errorsPresent)
		{
			if (base.Job.IsCancelled)
			{
				switch (base.Job.JobCancellationStatus)
				{
				case JobCancellationStatus.NotCancelled:
					break;
				case JobCancellationStatus.CancelledByUserRequest:
					return Strings.MigrationBatchCancelledByUser(base.Job.JobName);
				case JobCancellationStatus.CancelledDueToHighFailureCount:
					return Strings.MigrationBatchCancelledBySystem(base.Job.JobName);
				default:
					throw new InvalidOperationException("Unsupported job cancellation status " + base.Job.JobCancellationStatus);
				}
			}
			if (errorsPresent)
			{
				return Strings.MigrationBatchCompletionReportMailErrorHeader(base.Job.JobName);
			}
			return Strings.MigrationBatchCompletionReportMailHeader(base.Job.JobName);
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x0003E3CC File Offset: 0x0003C5CC
		protected override IDictionary<string, string> GetTemplateData(MigrationJobReportingCursor migrationReportData, string successReportLink, string failureReportLink)
		{
			MigrationObjectsCount migrationErrorCount = migrationReportData.MigrationErrorCount;
			MigrationObjectsCount migrationSuccessCount = migrationReportData.MigrationSuccessCount;
			bool hasErrors = migrationReportData.HasErrors;
			string text = JobSyncCompletingProcessor.GetStatisticsSummaryMessage(migrationReportData);
			Dictionary<string, string> dictionary = new Dictionary<string, string>(30);
			dictionary.Add("{StatisticsReportLink}", successReportLink);
			if (!string.IsNullOrEmpty(text))
			{
				text = string.Format(CultureInfo.InvariantCulture, "<br />{0}", new object[]
				{
					text
				});
			}
			dictionary.Add("{StatisticsSummaryMessage}", text);
			dictionary.Add("{ErrorReportLink}", failureReportLink);
			string licensingWarningSection = base.GetLicensingWarningSection();
			dictionary.Add("{MoacWarningSection}", licensingWarningSection);
			if (!hasErrors)
			{
				dictionary.Add("{imghtml}", string.Empty);
				dictionary.Add("{LabelCouldntMigrate}", string.Empty);
				dictionary.Add("{ErrorSummaryMessage}", string.Empty);
			}
			else
			{
				dictionary.Add("{imghtml}", "<img width=\"16\" height=\"16\" src=\"cid:ErrorImage\" />");
				dictionary.Add("{LabelCouldntMigrate}", Strings.LabelCouldntMigrate);
				dictionary.Add("{ErrorSummaryMessage}", migrationErrorCount.ToString());
			}
			if (base.Job.ShouldAutoRetryStartedJob)
			{
				dictionary.Add("{ExtraNotes}", Strings.LabelAutoRetry(base.Job.MaxAutoRunCount.Value - base.Job.AutoRunCount));
			}
			else
			{
				dictionary.Add("{ExtraNotes}", string.Empty);
			}
			dictionary.Add("{ReportHeader}", this.GetEmailSubject(hasErrors));
			dictionary.Add("{LabelStartedByUser}", Strings.LabelSubmittedByUser);
			dictionary.Add("{StartedByUser}", base.Job.SubmittedByUser);
			dictionary.Add("{LabelStartDateTime}", Strings.LabelStartDateTime);
			dictionary.Add("{LabelRunTime}", Strings.LabelRunTime);
			ExTimeZone userTimeZone = base.Job.UserTimeZone;
			if (base.Job.StartTime != null)
			{
				dictionary.Add("{StartDateTime}", userTimeZone.ConvertDateTime(base.Job.StartTime.Value).ToString("D", CultureInfo.CurrentCulture));
				TimeSpan timeSpan = ExDateTime.UtcNow - base.Job.StartTime.Value;
				dictionary.Add("{RunTime}", ItemStateTransitionHelper.LocalizeTimeSpan(timeSpan));
			}
			else
			{
				dictionary.Add("{StartDateTime}", string.Empty);
				dictionary.Add("{RunTime}", string.Empty);
			}
			MigrationObjectsCount migrationObjectsCount = migrationSuccessCount;
			dictionary.Add("{LabelSynced}", Strings.LabelSynced);
			dictionary.Add("{CompletedData}", migrationObjectsCount.ToString());
			MigrationObjectsCount migrationObjectsCount2 = migrationErrorCount + migrationObjectsCount;
			dictionary.Add("{LabelTotalRows}", Strings.LabelTotalRows);
			dictionary.Add("{TotalRows}", migrationObjectsCount2.ToString());
			dictionary.Add("{LabelLogMailFooter}", Strings.LabelLogMailFooter);
			return dictionary;
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x0003E6C0 File Offset: 0x0003C8C0
		protected override IEnumerable<MigrationJobItem> GetJobItemsToProcess(string startingIndex, int maxCount)
		{
			IEnumerable<MigrationJobItem> result;
			if (string.IsNullOrEmpty(startingIndex))
			{
				result = MigrationJobItem.GetAllSortedByIdentifier(base.DataProvider, base.Job, maxCount);
			}
			else
			{
				result = MigrationJobItem.GetNextJobItems(base.DataProvider, base.Job, startingIndex, maxCount);
			}
			return result;
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0003E6FF File Offset: 0x0003C8FF
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MigrationJobSyncCompletingProcessor>(this);
		}

		// Token: 0x04000516 RID: 1302
		private const string ReportTemplate = "BatchCompletedReport.htm";

		// Token: 0x04000517 RID: 1303
		private static readonly MigrationUserStatus[] JobItemStatusForSuccess = new MigrationUserStatus[]
		{
			MigrationUserStatus.Synced,
			MigrationUserStatus.IncrementalSyncing
		};
	}
}
