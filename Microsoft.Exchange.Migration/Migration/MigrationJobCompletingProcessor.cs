using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000114 RID: 276
	internal class MigrationJobCompletingProcessor : JobSyncCompletingProcessor
	{
		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x0003C76C File Offset: 0x0003A96C
		protected override bool IsBatchSyncedReport
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x0003C76F File Offset: 0x0003A96F
		protected override MigrationUserStatus[] JobItemStatesForSuccess
		{
			get
			{
				return MigrationJobCompletingProcessor.JobItemStatusForSuccess;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x0003C776 File Offset: 0x0003A976
		protected override MigrationUserStatus[] JobItemStatesForFailure
		{
			get
			{
				return MigrationJobCompletingProcessor.JobItemsStatusForCompletionErrors;
			}
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0003C780 File Offset: 0x0003A980
		internal static MigrationJobCompletingProcessor CreateProcessor(MigrationType type, bool isStaged)
		{
			if (type <= MigrationType.BulkProvisioning)
			{
				if (type == MigrationType.IMAP || type == MigrationType.ExchangeOutlookAnywhere)
				{
					throw new NotSupportedException("IMAP/Exchange not supported in Completing state");
				}
				if (type == MigrationType.BulkProvisioning)
				{
					throw new NotSupportedException("Bulk Provisioning not supported in Completing state");
				}
			}
			else if (type == MigrationType.ExchangeRemoteMove || type == MigrationType.ExchangeLocalMove || type == MigrationType.PublicFolder)
			{
				return new MigrationJobCompletingProcessor();
			}
			throw new ArgumentException("Invalid MigrationType " + type);
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0003C7E4 File Offset: 0x0003A9E4
		internal override bool Validate()
		{
			return base.Job != null && base.Job.Status == MigrationJobStatus.Completing;
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0003C800 File Offset: 0x0003AA00
		internal override MigrationJobStatus GetNextStageStatus()
		{
			MigrationUtil.AssertOrThrow(this.hasErrors != null, "GetNextStageStatus should only be called after processing is completed for the processor.", new object[0]);
			if (!base.Job.TryAutoRetryCompletedJob(base.DataProvider))
			{
				return MigrationJobStatus.Completed;
			}
			if (base.Job.AutoComplete)
			{
				return MigrationJobStatus.SyncInitializing;
			}
			if (base.Job.MigrationType == MigrationType.PublicFolder)
			{
				return MigrationJobStatus.CompletionStarting;
			}
			return MigrationJobStatus.CompletionInitializing;
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x0003C864 File Offset: 0x0003AA64
		protected override LegacyMigrationJobProcessorResponse Process(bool scheduleNewWork)
		{
			MigrationJobReportingCursor migrationJobReportingCursor;
			LegacyMigrationJobProcessorResponse result = base.Process(out migrationJobReportingCursor);
			this.hasErrors = new bool?(migrationJobReportingCursor.HasErrors);
			return result;
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x0003C88C File Offset: 0x0003AA8C
		protected override string GetTemplateName(bool areErrorsPresent)
		{
			return "MigrationCompletedReport.htm";
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0003C893 File Offset: 0x0003AA93
		protected override string GetEmailSubject(bool areErrorsPresent)
		{
			if (!areErrorsPresent)
			{
				return Strings.MigrationFinalizationReportMailHeader(base.Job.JobName);
			}
			return Strings.MigrationFinalizationReportMailErrorHeader(base.Job.JobName);
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0003C8C4 File Offset: 0x0003AAC4
		protected override IDictionary<string, string> GetTemplateData(MigrationJobReportingCursor migrationReportData, string successReportLink, string failureReportLink)
		{
			MigrationObjectsCount migrationErrorCount = migrationReportData.MigrationErrorCount;
			MigrationObjectsCount migrationSuccessCount = migrationReportData.MigrationSuccessCount;
			bool flag = migrationReportData.HasErrors;
			Dictionary<string, string> dictionary = new Dictionary<string, string>(15);
			string text = JobSyncCompletingProcessor.GetStatisticsSummaryMessage(migrationReportData);
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
			if (!flag)
			{
				dictionary.Add("{ReportHeader}", Strings.MigrationFinalizationReportMailHeader(base.Job.JobName));
				dictionary.Add("{imghtml}", string.Empty);
				dictionary.Add("{ErrorSummaryMessage}", string.Empty);
			}
			else
			{
				dictionary.Add("{ReportHeader}", Strings.MigrationFinalizationReportMailErrorHeader(base.Job.JobName));
				dictionary.Add("{imghtml}", "<img width=\"16\" height=\"16\" src=\"cid:ErrorImage\" />");
				dictionary.Add("{ErrorSummaryMessage}", migrationErrorCount.ToString());
			}
			if (base.Job.ShouldAutoRetryCompletedJob)
			{
				dictionary.Add("{ExtraNotes}", Strings.LabelAutoRetry(base.Job.MaxAutoRunCount.Value - base.Job.AutoRunCount));
			}
			else
			{
				dictionary.Add("{ExtraNotes}", string.Empty);
			}
			dictionary.Add("{RetryFinalizationMessage}", (!flag) ? string.Empty : Strings.FinalizationErrorSummaryRetryMessage);
			MigrationObjectsCount migrationObjectsCount = migrationSuccessCount;
			dictionary.Add("{LabelCompletedMailboxes}", Strings.LabelSynced);
			dictionary.Add("{CompletedMailboxes}", migrationObjectsCount.ToString());
			MigrationObjectsCount migrationObjectsCount2 = migrationObjectsCount + migrationErrorCount;
			dictionary.Add("{LabelTotalMailboxes}", Strings.LabelTotalMailboxes);
			dictionary.Add("{TotalMailboxes}", migrationObjectsCount2.ToString());
			dictionary.Add("{LabelLogMailFooter}", Strings.LabelLogMailFooter);
			return dictionary;
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0003CAC4 File Offset: 0x0003ACC4
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

		// Token: 0x06000E7E RID: 3710 RVA: 0x0003CB03 File Offset: 0x0003AD03
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MigrationJobCompletingProcessor>(this);
		}

		// Token: 0x0400050C RID: 1292
		private const string ReportTemplate = "MigrationCompletedReport.htm";

		// Token: 0x0400050D RID: 1293
		internal static readonly MigrationUserStatus[] JobItemsStatusForCompletionErrors = new MigrationUserStatus[]
		{
			MigrationUserStatus.CompletionFailed,
			MigrationUserStatus.IncrementalFailed,
			MigrationUserStatus.CompletedWithWarnings,
			MigrationUserStatus.Corrupted,
			MigrationUserStatus.Stopped,
			MigrationUserStatus.IncrementalStopped,
			MigrationUserStatus.Failed
		};

		// Token: 0x0400050E RID: 1294
		private static readonly MigrationUserStatus[] JobItemStatusForSuccess = new MigrationUserStatus[]
		{
			MigrationUserStatus.Completed
		};

		// Token: 0x0400050F RID: 1295
		private bool? hasErrors;
	}
}
