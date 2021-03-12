using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ServiceHost.SyncMigrationServicelet;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000109 RID: 265
	internal class ReportMigrationJobProcessor : MigrationJobProcessorBase
	{
		// Token: 0x06000DCB RID: 3531 RVA: 0x00038E5F File Offset: 0x0003705F
		public ReportMigrationJobProcessor(MigrationJob migrationObject, IMigrationDataProvider dataProvider) : base(migrationObject, dataProvider)
		{
			this.ReportData = new MigrationJobReportingCursor(ReportingStageEnum.Unknown);
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x00038E8C File Offset: 0x0003708C
		protected override Func<int?, IEnumerable<StoreObjectId>>[] ProcessableChildObjectQueries
		{
			get
			{
				return new Func<int?, IEnumerable<StoreObjectId>>[]
				{
					(int? maxCount) => MigrationJobItem.GetAllIds(this.DataProvider, this.MigrationObject, maxCount)
				};
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x00038EB0 File Offset: 0x000370B0
		protected override int? MaxChildObjectsToProcessCount
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x00038EC6 File Offset: 0x000370C6
		// (set) Token: 0x06000DCF RID: 3535 RVA: 0x00038ECE File Offset: 0x000370CE
		private IMigrationDataProvider ReportProvider { get; set; }

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x00038ED7 File Offset: 0x000370D7
		// (set) Token: 0x06000DD1 RID: 3537 RVA: 0x00038EDF File Offset: 0x000370DF
		private ReportMigrationJobProcessor.DisposableMigrationReportItem ReportItem { get; set; }

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x00038EE8 File Offset: 0x000370E8
		// (set) Token: 0x06000DD3 RID: 3539 RVA: 0x00038EF0 File Offset: 0x000370F0
		private MigrationReportCsvSchema CsvSchema { get; set; }

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x00038EF9 File Offset: 0x000370F9
		// (set) Token: 0x06000DD5 RID: 3541 RVA: 0x00038F01 File Offset: 0x00037101
		private MigrationJobReportingCursor ReportData { get; set; }

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x00038F0C File Offset: 0x0003710C
		private string LicensingHelpUrl
		{
			get
			{
				if (this.DataProvider.ADProvider.IsLicensingEnforced)
				{
					return string.Format(CultureInfo.InvariantCulture, MigrationJobReportingCursor.MoacHelpUrlFormat, new object[]
					{
						this.MigrationObject.AdminCulture.LCID
					});
				}
				return null;
			}
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x00038F5C File Offset: 0x0003715C
		internal void SendReportMail()
		{
			MigrationReportData migrationReportData = new MigrationReportData(this.ReportData, this.GetEmailSubject(), "BatchReport.htm", this.LicensingHelpUrl);
			IMigrationEmailHandler migrationEmailHandler = MigrationServiceFactory.Instance.CreateEmailHandler(this.DataProvider);
			using (IMigrationEmailMessageItem migrationEmailMessageItem = migrationEmailHandler.CreateEmailMessage())
			{
				bool includeReportLink = !this.ReportItem.TryCopyAttachment(migrationEmailMessageItem);
				if (!string.IsNullOrEmpty(migrationReportData.LicensingHelpUrl))
				{
					MigrationReportGenerator.ComposeAttachmentFromResource(migrationEmailMessageItem, "Information.gif", "Information");
				}
				string body = migrationReportData.ComposeBodyFromTemplate(this.GetTemplateData(includeReportLink));
				MultiValuedProperty<SmtpAddress> notificationEmails = this.MigrationObject.NotificationEmails;
				string emailSubject = migrationReportData.EmailSubject;
				ExTraceGlobals.FaultInjectionTracer.TraceTest(4209388861U);
				migrationEmailMessageItem.Send(notificationEmails, emailSubject, body);
			}
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x00039028 File Offset: 0x00037228
		protected override MigrationProcessorResponse ProcessChild(MigrationJobItem child)
		{
			if (MigrationJobReportingCursor.FailureStatuses.Contains(child.Status))
			{
				this.ReportData.MigrationErrorCount += child.CountSelf;
			}
			else
			{
				if (child.InitialSyncDuration != null)
				{
					this.ReportData.SyncDuration += child.InitialSyncDuration.Value;
				}
				if (child.ItemsSkipped > 0L)
				{
					this.ReportData.PartialMigrationCounts++;
				}
				this.ReportData.MigrationSuccessCount += child.CountSelf;
			}
			this.CsvSchema.WriteRow(this.ReportItem.AttachmentWriter, child);
			return MigrationJobItemProcessorResponse.Create(MigrationProcessorResult.Completed, null, null, null, null, null, false, null);
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x00039128 File Offset: 0x00037328
		protected override MigrationJobProcessorResponse ProcessObject()
		{
			this.CsvSchema = new MigrationReportCsvSchema(this.MigrationObject.IsProvisioningSupported);
			int count = MigrationReportItem.GetCount(this.ReportProvider, this.MigrationObject.JobId);
			int config = ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("MaxReportItemsPerJob");
			if (count >= config)
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "Attempting to removing {0} report items for a maximum of {1}", new object[]
				{
					count - config,
					config
				});
				foreach (MigrationReportItem migrationReportItem in MigrationReportItem.GetByJobId(this.ReportProvider, new Guid?(this.MigrationObject.JobId), count - config))
				{
					MigrationLogger.Log(MigrationEventType.Information, "Removing report {0}", new object[]
					{
						migrationReportItem.ReportName
					});
					migrationReportItem.Delete(this.ReportProvider);
				}
			}
			this.ReportItem = new ReportMigrationJobProcessor.DisposableMigrationReportItem(this.MigrationObject);
			this.ReportItem.Initialize(this.ReportProvider, this.CsvSchema);
			return MigrationJobProcessorResponse.Create(MigrationProcessorResult.Completed, null, null, null, null, null);
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x00039258 File Offset: 0x00037458
		protected override MigrationJobProcessorResponse ApplyResponse(MigrationJobProcessorResponse response)
		{
			if (response.Result == MigrationProcessorResult.Completed)
			{
				this.ReportItem.Save();
				MigrationReportSet reportSet = new MigrationReportSet((DateTime)this.ReportItem.CreationTime, this.ReportItem.EcpUrl, null);
				this.MigrationObject.SetReportUrls(this.DataProvider, reportSet);
				if (this.MigrationObject.NotificationEmails != null && this.MigrationObject.NotificationEmails.Count > 0)
				{
					this.SendReportMail();
				}
				this.MigrationObject.SetStatus(this.DataProvider, this.MigrationObject.Status, this.MigrationObject.State, new MigrationFlags?(this.MigrationObject.Flags & ~MigrationFlags.Report), null, null, null, null, null, null, true, null, response.ProcessingDuration);
				return MigrationJobProcessorResponse.Create(MigrationProcessorResult.Working, null, null, null, null, null);
			}
			return base.ApplyResponse(response);
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x00039352 File Offset: 0x00037552
		protected override void SetContext()
		{
			base.SetContext();
			this.ReportProvider = this.DataProvider.GetProviderForFolder(MigrationFolderName.SyncMigrationReports);
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0003936C File Offset: 0x0003756C
		protected override void RestoreContext()
		{
			if (this.ReportItem != null)
			{
				this.ReportItem.Dispose();
				this.ReportItem = null;
			}
			if (this.ReportProvider != null)
			{
				this.ReportProvider.Dispose();
				this.ReportProvider = null;
			}
			base.RestoreContext();
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x000393A8 File Offset: 0x000375A8
		private string GetLicensingWarningSection()
		{
			return MigrationJobReportingCursor.GetLicensingHtml(this.LicensingHelpUrl);
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x000393B8 File Offset: 0x000375B8
		private IDictionary<string, string> GetTemplateData(bool includeReportLink)
		{
			MigrationObjectsCount migrationErrorCount = this.ReportData.MigrationErrorCount;
			MigrationObjectsCount migrationSuccessCount = this.ReportData.MigrationSuccessCount;
			bool hasErrors = this.ReportData.HasErrors;
			Dictionary<string, string> dictionary = new Dictionary<string, string>(15);
			string text = JobSyncCompletingProcessor.GetStatisticsSummaryMessage(this.ReportData);
			if (includeReportLink)
			{
				dictionary.Add("{StatisticsReportLink}", this.ReportItem.EcpUrl);
			}
			else
			{
				dictionary.Add("{StatisticsReportLink}", string.Empty);
			}
			if (!string.IsNullOrEmpty(text))
			{
				text = string.Format(CultureInfo.InvariantCulture, "<br />{0}", new object[]
				{
					text
				});
			}
			dictionary.Add("{StatisticsSummaryMessage}", text);
			dictionary.Add("{MoacWarningSection}", this.GetLicensingWarningSection());
			dictionary.Add("{ReportHeader}", this.GetEmailSubject());
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
			dictionary.Add("{ExtraNotes}", string.Empty);
			dictionary.Add("{LabelStartedByUser}", Strings.LabelSubmittedByUser);
			dictionary.Add("{StartedByUser}", this.MigrationObject.SubmittedByUser);
			dictionary.Add("{LabelStartDateTime}", Strings.LabelStartDateTime);
			dictionary.Add("{LabelRunTime}", Strings.LabelRunTime);
			ExTimeZone userTimeZone = this.MigrationObject.UserTimeZone;
			if (this.MigrationObject.StartTime != null)
			{
				dictionary.Add("{StartDateTime}", userTimeZone.ConvertDateTime(this.MigrationObject.StartTime.Value).ToString("D", CultureInfo.CurrentCulture));
				TimeSpan timeSpan = ExDateTime.UtcNow - this.MigrationObject.StartTime.Value;
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

		// Token: 0x06000DDF RID: 3551 RVA: 0x00039674 File Offset: 0x00037874
		private string GetEmailSubject()
		{
			if (this.MigrationObject.IsCancelled)
			{
				switch (this.MigrationObject.JobCancellationStatus)
				{
				case JobCancellationStatus.NotCancelled:
					break;
				case JobCancellationStatus.CancelledByUserRequest:
					return Strings.MigrationBatchCancelledByUser(this.MigrationObject.JobName);
				case JobCancellationStatus.CancelledDueToHighFailureCount:
					return Strings.MigrationBatchCancelledBySystem(this.MigrationObject.JobName);
				default:
					throw new InvalidOperationException("Unsupported job cancellation status " + this.MigrationObject.JobCancellationStatus);
				}
			}
			if (this.ReportData.HasErrors)
			{
				return Strings.MigrationBatchReportMailErrorHeader(this.MigrationObject.JobName);
			}
			return Strings.MigrationBatchReportMailHeader(this.MigrationObject.JobName);
		}

		// Token: 0x040004F0 RID: 1264
		private const string ReportFileName = "MigrationReport.csv";

		// Token: 0x040004F1 RID: 1265
		private const string ReportTemplate = "BatchReport.htm";

		// Token: 0x0200010A RID: 266
		private class DisposableMigrationReportItem : MigrationReportItem, IDisposeTrackable, IDisposable
		{
			// Token: 0x06000DE1 RID: 3553 RVA: 0x00039732 File Offset: 0x00037932
			public DisposableMigrationReportItem(MigrationJob job) : base("MigrationReport.csv", new Guid?(job.JobId), job.MigrationType, MigrationReportType.BatchReport, job.IsStaged)
			{
				base.Version = 3L;
				this.disposed = false;
				this.disposeTracker = this.GetDisposeTracker();
			}

			// Token: 0x1700046B RID: 1131
			// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x00039772 File Offset: 0x00037972
			// (set) Token: 0x06000DE3 RID: 3555 RVA: 0x0003977A File Offset: 0x0003797A
			public StreamWriter AttachmentWriter { get; private set; }

			// Token: 0x1700046C RID: 1132
			// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x00039783 File Offset: 0x00037983
			// (set) Token: 0x06000DE5 RID: 3557 RVA: 0x0003978B File Offset: 0x0003798B
			public string EcpUrl { get; private set; }

			// Token: 0x1700046D RID: 1133
			// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x00039794 File Offset: 0x00037994
			// (set) Token: 0x06000DE7 RID: 3559 RVA: 0x0003979C File Offset: 0x0003799C
			public IMigrationMessageItem MessageItem { get; set; }

			// Token: 0x1700046E RID: 1134
			// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x000397A5 File Offset: 0x000379A5
			// (set) Token: 0x06000DE9 RID: 3561 RVA: 0x000397AD File Offset: 0x000379AD
			public IMigrationAttachment Attachment { get; set; }

			// Token: 0x06000DEA RID: 3562 RVA: 0x000397B8 File Offset: 0x000379B8
			public void Initialize(IMigrationDataProvider dataProvider, MigrationReportCsvSchema schema)
			{
				MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
				MigrationUtil.ThrowOnNullArgument(schema, "schema");
				this.MessageItem = dataProvider.CreateMessage();
				base.WriteToMessageItem(this.MessageItem, true);
				this.MessageItem.Save(SaveMode.NoConflictResolution);
				this.MessageItem.Load(MigrationHelper.ItemIdProperties);
				base.MessageId = this.MessageItem.Id;
				this.EcpUrl = base.GetUrl(dataProvider);
				this.Attachment = this.MessageItem.CreateAttachment(base.ReportName);
				this.AttachmentWriter = new StreamWriter(this.Attachment.Stream);
				schema.WriteHeader(this.AttachmentWriter);
			}

			// Token: 0x06000DEB RID: 3563 RVA: 0x00039868 File Offset: 0x00037A68
			public bool TryCopyAttachment(IMigrationEmailMessageItem message)
			{
				bool flag = MigrationJob.MigrationTypeSupportsProvisioning(base.MigrationType);
				if (flag)
				{
					return false;
				}
				using (IMigrationAttachment attachment = this.MessageItem.GetAttachment(base.ReportName, PropertyOpenMode.ReadOnly))
				{
					if (attachment.Size < ConfigBase<MigrationServiceConfigSchema>.GetConfig<long>("ReportMaxAttachmentSize"))
					{
						using (IMigrationAttachment migrationAttachment = message.CreateAttachment(base.ReportName))
						{
							attachment.Stream.Seek(0L, SeekOrigin.Begin);
							CsvSchema csvSchema = new MigrationReportCsvSchema(flag);
							using (StreamWriter streamWriter = new StreamWriter(migrationAttachment.Stream))
							{
								csvSchema.Copy(attachment.Stream, streamWriter);
							}
							migrationAttachment.Save(null);
						}
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000DEC RID: 3564 RVA: 0x00039948 File Offset: 0x00037B48
			public void Save()
			{
				this.AttachmentWriter.Flush();
				this.AttachmentWriter.Dispose();
				this.AttachmentWriter = null;
				this.Attachment.Save(null);
				base.ReportedTime = new ExDateTime?(ExDateTime.UtcNow);
				this.MessageItem[MigrationBatchMessageSchema.MigrationJobItemStateLastUpdated] = base.ReportedTime;
				base.WriteToMessageItem(this.MessageItem, true);
				this.MessageItem.Save(SaveMode.NoConflictResolution);
				this.MessageItem.Load(MigrationHelper.ItemIdProperties);
				base.MessageId = this.MessageItem.Id;
				base.CreationTime = this.MessageItem.CreationTime;
				this.AttachmentWriter = new StreamWriter(this.Attachment.Stream);
			}

			// Token: 0x06000DED RID: 3565 RVA: 0x00039A0A File Offset: 0x00037C0A
			public virtual DisposeTracker GetDisposeTracker()
			{
				return DisposeTracker.Get<ReportMigrationJobProcessor.DisposableMigrationReportItem>(this);
			}

			// Token: 0x06000DEE RID: 3566 RVA: 0x00039A12 File Offset: 0x00037C12
			public void SuppressDisposeTracker()
			{
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Suppress();
				}
			}

			// Token: 0x06000DEF RID: 3567 RVA: 0x00039A27 File Offset: 0x00037C27
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x06000DF0 RID: 3568 RVA: 0x00039A38 File Offset: 0x00037C38
			private void Dispose(bool disposing)
			{
				if (!this.disposed && disposing)
				{
					if (this.AttachmentWriter != null)
					{
						this.AttachmentWriter.Dispose();
						this.AttachmentWriter = null;
					}
					if (this.Attachment != null)
					{
						this.Attachment.Dispose();
						this.Attachment = null;
					}
					if (this.MessageItem != null)
					{
						this.MessageItem.Dispose();
						this.MessageItem = null;
					}
					if (this.disposeTracker != null)
					{
						this.disposeTracker.Dispose();
						this.disposeTracker = null;
					}
				}
				this.disposed = true;
			}

			// Token: 0x040004F6 RID: 1270
			private bool disposed;

			// Token: 0x040004F7 RID: 1271
			private DisposeTracker disposeTracker;
		}
	}
}
