using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200010F RID: 271
	internal abstract class JobSyncCompletingProcessor : JobProcessor
	{
		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000E2F RID: 3631
		protected abstract bool IsBatchSyncedReport { get; }

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x0003ACCC File Offset: 0x00038ECC
		protected virtual string LicensingHelpUrl
		{
			get
			{
				if (base.DataProvider.ADProvider.IsLicensingEnforced)
				{
					return string.Format(CultureInfo.InvariantCulture, MigrationJobReportingCursor.MoacHelpUrlFormat, new object[]
					{
						base.Job.AdminCulture.LCID
					});
				}
				return null;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x0003AD58 File Offset: 0x00038F58
		protected virtual MigrationJobReportWriterDelegate ReportHeaderWriter
		{
			get
			{
				return delegate(MigrationJobReportingCursor cursor, StreamWriter successWriter, StreamWriter failureWriter)
				{
					MigrationSuccessReportCsvSchema.WriteHeader(successWriter, base.Job.MigrationType, this.IsBatchSyncedReport, base.Job.IsStaged);
					MigrationFailureReportCsvSchema.WriteHeader(failureWriter, base.Job.MigrationType, this.IsBatchSyncedReport);
					return null;
				};
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x0003ADB7 File Offset: 0x00038FB7
		protected virtual MigrationJobReportWriterDelegate ReportWriter
		{
			get
			{
				return delegate(MigrationJobReportingCursor currentCursor, StreamWriter successWriter, StreamWriter failureWriter)
				{
					switch (currentCursor.ReportingStage)
					{
					case ReportingStageEnum.ProcessingJobItems:
						return this.ProcessJobItemsForReporting(currentCursor, successWriter, failureWriter);
					case ReportingStageEnum.ProcessingValidationErrors:
						return this.ProcessValidationErrorsForReporting(currentCursor, failureWriter);
					case ReportingStageEnum.Completed:
						return currentCursor;
					default:
						throw new InvalidOperationException("This method should not be called with invalid reporting cursor stage " + currentCursor);
					}
				};
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000E33 RID: 3635 RVA: 0x0003ADC5 File Offset: 0x00038FC5
		protected virtual string SuccessReportFileName
		{
			get
			{
				return "MigrationStatistics.csv";
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000E34 RID: 3636 RVA: 0x0003ADCC File Offset: 0x00038FCC
		protected virtual string FailureReportFileName
		{
			get
			{
				return "MigrationErrors.csv";
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000E35 RID: 3637
		protected abstract MigrationUserStatus[] JobItemStatesForSuccess { get; }

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000E36 RID: 3638
		protected abstract MigrationUserStatus[] JobItemStatesForFailure { get; }

		// Token: 0x06000E37 RID: 3639 RVA: 0x0003ADD4 File Offset: 0x00038FD4
		public static string GetStatisticsSummaryMessage(MigrationJobReportingCursor cursor)
		{
			if (!cursor.AreSuccessfulMigrationsPresent)
			{
				return string.Empty;
			}
			int partialMigrationCounts = cursor.PartialMigrationCounts;
			if (partialMigrationCounts == 0)
			{
				return Strings.NoPartialMigrationSummaryMessage;
			}
			if (partialMigrationCounts == 1)
			{
				return Strings.PartialMigrationSummaryMessageSingular(partialMigrationCounts);
			}
			return Strings.PartialMigrationSummaryMessagePlural(partialMigrationCounts);
		}

		// Token: 0x06000E38 RID: 3640
		protected abstract string GetTemplateName(bool areErrorsPresent);

		// Token: 0x06000E39 RID: 3641
		protected abstract string GetEmailSubject(bool areErrorsPresent);

		// Token: 0x06000E3A RID: 3642
		protected abstract IDictionary<string, string> GetTemplateData(MigrationJobReportingCursor migrationReportData, string successReportLink, string failureReportLink);

		// Token: 0x06000E3B RID: 3643
		protected abstract IEnumerable<MigrationJobItem> GetJobItemsToProcess(string startingIndex, int maxCount);

		// Token: 0x06000E3C RID: 3644 RVA: 0x0003AE20 File Offset: 0x00039020
		protected LegacyMigrationJobProcessorResponse Process(out MigrationJobReportingCursor currentCursor)
		{
			LegacyMigrationJobProcessorResponse legacyMigrationJobProcessorResponse = LegacyMigrationJobProcessorResponse.Create(MigrationProcessorResult.Working, null);
			currentCursor = MigrationJobReportingCursor.Deserialize(base.Job.LastCursorPosition);
			MigrationReportGenerator migrationReportGenerator;
			if (currentCursor == null)
			{
				migrationReportGenerator = new MigrationReportGenerator(base.DataProvider, base.Job, this.IsBatchSyncedReport, this.SuccessReportFileName, this.FailureReportFileName);
				if (this.ReportHeaderWriter != null)
				{
					migrationReportGenerator.WriteReportData(null, this.ReportHeaderWriter);
				}
				currentCursor = new MigrationJobReportingCursor(ReportingStageEnum.ProcessingJobItems, string.Empty, migrationReportGenerator.SuccessReportId, migrationReportGenerator.FailureReportId);
				currentCursor.SyncDuration = new TimeSpan?(default(TimeSpan));
			}
			else
			{
				migrationReportGenerator = MigrationReportGenerator.CreateFromCursor(base.DataProvider, base.Job, currentCursor);
			}
			currentCursor = migrationReportGenerator.WriteReportData(currentCursor, this.ReportWriter);
			if (currentCursor.ReportingStage == ReportingStageEnum.Completed)
			{
				MigrationReportData reportData = new MigrationReportData(currentCursor, new MigrationJobTemplateDataGeneratorDelegate(this.GetTemplateData), this.GetEmailSubject(currentCursor.HasErrors), this.GetTemplateName(currentCursor.HasErrors), this.LicensingHelpUrl);
				migrationReportGenerator.GenerateUrls(reportData);
				if (base.Job.NotificationEmails != null && base.Job.NotificationEmails.Count > 0)
				{
					migrationReportGenerator.SendReportMail(reportData);
				}
				legacyMigrationJobProcessorResponse.Result = MigrationProcessorResult.Completed;
				base.Job.UpdateInitialSyncProperties(base.DataProvider, currentCursor.SyncDuration.Value);
			}
			else
			{
				base.Job.UpdateLastProcessedRow(base.DataProvider, currentCursor.Serialize(), null, 0);
			}
			return legacyMigrationJobProcessorResponse;
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0003AF98 File Offset: 0x00039198
		protected string GetLicensingWarningSection()
		{
			return MigrationJobReportingCursor.GetLicensingHtml(this.LicensingHelpUrl);
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0003AFA8 File Offset: 0x000391A8
		private MigrationJobReportingCursor ProcessJobItemsForReporting(MigrationJobReportingCursor currentCursor, StreamWriter successWriter, StreamWriter failureWriter)
		{
			int config = ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("SyncMigrationMaxJobItemsToProcessForReportGeneration");
			HashSet<MigrationUserStatus> hashSet = new HashSet<MigrationUserStatus>(this.JobItemStatesForSuccess);
			HashSet<MigrationUserStatus> hashSet2 = new HashSet<MigrationUserStatus>(this.JobItemStatesForFailure);
			List<MigrationJobItem> list = new List<MigrationJobItem>(config);
			List<MigrationJobItem> list2 = new List<MigrationJobItem>(config);
			MigrationJobItem migrationJobItem = null;
			IEnumerable<MigrationJobItem> jobItemsToProcess = this.GetJobItemsToProcess(currentCursor.CurrentCursorPosition, config);
			foreach (MigrationJobItem migrationJobItem2 in jobItemsToProcess)
			{
				migrationJobItem = migrationJobItem2;
				if (hashSet.Contains(migrationJobItem2.Status))
				{
					list.Add(migrationJobItem2);
					currentCursor.MigrationSuccessCount += migrationJobItem2.CountSelf;
					if (migrationJobItem2.InitialSyncDuration != null)
					{
						currentCursor.SyncDuration += migrationJobItem2.InitialSyncDuration.Value;
					}
					if (migrationJobItem2.ItemsSkipped > 0L)
					{
						currentCursor.PartialMigrationCounts++;
					}
				}
				else if (hashSet2.Contains(migrationJobItem2.Status))
				{
					list2.Add(migrationJobItem2);
					currentCursor.MigrationErrorCount += migrationJobItem2.CountSelf;
				}
				else
				{
					MigrationLogger.Log(MigrationEventType.Error, "ProcessJobItemsForReporting: found a job item that we're not processing {0}", new object[]
					{
						migrationJobItem2
					});
				}
			}
			MigrationSuccessReportCsvSchema.Write(successWriter, base.Job.MigrationType, list, this.IsBatchSyncedReport, base.Job.IsStaged);
			MigrationFailureReportCsvSchema.Write(failureWriter, base.Job.MigrationType, list2, this.IsBatchSyncedReport);
			MigrationJobReportingCursor nextCursor;
			if (migrationJobItem == null || string.Equals(migrationJobItem.Identifier, currentCursor.CurrentCursorPosition, StringComparison.OrdinalIgnoreCase))
			{
				if (this.IsBatchSyncedReport)
				{
					nextCursor = currentCursor.GetNextCursor(ReportingStageEnum.ProcessingValidationErrors, null);
				}
				else
				{
					nextCursor = currentCursor.GetNextCursor(ReportingStageEnum.Completed, null);
				}
			}
			else
			{
				nextCursor = currentCursor.GetNextCursor(ReportingStageEnum.ProcessingJobItems, migrationJobItem.Identifier);
			}
			return nextCursor;
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0003B1C8 File Offset: 0x000393C8
		private MigrationJobReportingCursor ProcessValidationErrorsForReporting(MigrationJobReportingCursor currentCursor, StreamWriter failureWriter)
		{
			IEnumerable<MigrationBatchError> validationWarnings = base.Job.GetValidationWarnings(base.DataProvider);
			MigrationFailureReportCsvSchema.Write(base.Job.MigrationType, failureWriter, validationWarnings, true);
			currentCursor.MigrationErrorCount += new MigrationObjectsCount(new int?(base.Job.ValidationWarningCount));
			return currentCursor.GetNextCursor(ReportingStageEnum.Completed, null);
		}

		// Token: 0x04000504 RID: 1284
		private const string SuccessReportFileNameString = "MigrationStatistics.csv";

		// Token: 0x04000505 RID: 1285
		private const string FailureReportFileNameString = "MigrationErrors.csv";
	}
}
