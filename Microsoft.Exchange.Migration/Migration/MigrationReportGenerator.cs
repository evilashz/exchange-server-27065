using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics.Components.ServiceHost.SyncMigrationServicelet;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000136 RID: 310
	internal class MigrationReportGenerator
	{
		// Token: 0x06000FA1 RID: 4001 RVA: 0x00042ACE File Offset: 0x00040CCE
		internal MigrationReportGenerator(IMigrationDataProvider dataProvider, MigrationJob migrationJob)
		{
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			MigrationUtil.ThrowOnNullArgument(migrationJob, "migrationJob");
			this.dataProvider = dataProvider;
			this.migrationJob = migrationJob;
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x00042AFC File Offset: 0x00040CFC
		internal MigrationReportGenerator(IMigrationDataProvider dataProvider, MigrationJob migrationJob, bool isBatchCompletionReport, string successReportFileName, string failureReportFileName) : this(dataProvider, migrationJob)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(successReportFileName, "successReportFileName");
			MigrationUtil.ThrowOnNullOrEmptyArgument(failureReportFileName, "failureReportFileName");
			this.supportAttachmentGeneration = true;
			MigrationReportItem migrationReportItem = MigrationReportGenerator.CreateReportItem(dataProvider, migrationJob, isBatchCompletionReport ? MigrationReportType.BatchSuccessReport : MigrationReportType.FinalizationSuccessReport, successReportFileName);
			this.successReportId = migrationReportItem.Identifier;
			MigrationReportItem migrationReportItem2 = MigrationReportGenerator.CreateReportItem(dataProvider, migrationJob, isBatchCompletionReport ? MigrationReportType.BatchFailureReport : MigrationReportType.FinalizationFailureReport, failureReportFileName);
			this.failureReportId = migrationReportItem2.Identifier;
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x00042B6A File Offset: 0x00040D6A
		internal MigrationReportId SuccessReportId
		{
			get
			{
				return this.successReportId;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06000FA4 RID: 4004 RVA: 0x00042B72 File Offset: 0x00040D72
		internal MigrationReportId FailureReportId
		{
			get
			{
				return this.failureReportId;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x00042B7A File Offset: 0x00040D7A
		internal bool SupportAttachmentGeneration
		{
			get
			{
				return this.supportAttachmentGeneration;
			}
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x00042B84 File Offset: 0x00040D84
		internal static MigrationReportGenerator CreateFromCursor(IMigrationDataProvider dataProvider, MigrationJob migrationJob, MigrationJobReportingCursor cursor)
		{
			return new MigrationReportGenerator(dataProvider, migrationJob)
			{
				successReportId = cursor.SuccessReportId,
				failureReportId = cursor.FailureReportId,
				supportAttachmentGeneration = true
			};
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x00042BBC File Offset: 0x00040DBC
		internal static void ComposeAttachmentFromResource(IMigrationEmailMessageItem reportItem, string resourceFileName, string resourceContentId)
		{
			using (Stream resourceStream = MigrationReportGenerator.GetResourceStream(resourceFileName))
			{
				if (resourceStream == null)
				{
					MigrationApplication.NotifyOfCriticalError(new FileNotFoundException(), "Unable to load resource " + resourceFileName);
				}
				else
				{
					using (IMigrationAttachment migrationAttachment = reportItem.CreateAttachment(resourceFileName))
					{
						Util.StreamHandler.CopyStreamData(resourceStream, migrationAttachment.Stream);
						migrationAttachment.Save(resourceContentId);
					}
				}
			}
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x00042C3C File Offset: 0x00040E3C
		internal static string GetTemplate(string templateName)
		{
			string result;
			using (Stream resourceStream = MigrationReportGenerator.GetResourceStream(templateName))
			{
				if (resourceStream == null)
				{
					MigrationApplication.NotifyOfCriticalError(new FileNotFoundException(), "Unable to load resource " + templateName);
					result = string.Empty;
				}
				else
				{
					using (StreamReader streamReader = new StreamReader(resourceStream))
					{
						result = streamReader.ReadToEnd();
					}
				}
			}
			return result;
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x00042D60 File Offset: 0x00040F60
		internal MigrationJobReportingCursor WriteReportData(MigrationJobReportingCursor initialCursorPosition, MigrationJobReportWriterDelegate reportWriter)
		{
			if (!this.SupportAttachmentGeneration)
			{
				throw new InvalidOperationException("This method cannot be invoked when the generator does not support attachment generation. Has the right constructor been used?");
			}
			MigrationJobReportingCursor reportingCursor = null;
			using (IMigrationDataProvider reportProvider = this.dataProvider.GetProviderForFolder(MigrationFolderName.SyncMigrationReports))
			{
				MigrationReportItem migrationReportItem = MigrationReportItem.Get(reportProvider, this.SuccessReportId);
				MigrationReportItem failureReportItem = MigrationReportItem.Get(reportProvider, this.FailureReportId);
				migrationReportItem.WriteStream(reportProvider, delegate(StreamWriter successWriter)
				{
					failureReportItem.WriteStream(reportProvider, delegate(StreamWriter failureWriter)
					{
						reportingCursor = reportWriter(initialCursorPosition, successWriter, failureWriter);
					});
				});
			}
			return reportingCursor;
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x00042E34 File Offset: 0x00041034
		internal void SendReportMail(MigrationReportData reportData)
		{
			IMigrationEmailHandler migrationEmailHandler = MigrationServiceFactory.Instance.CreateEmailHandler(this.dataProvider);
			using (IMigrationEmailMessageItem migrationEmailMessageItem = migrationEmailHandler.CreateEmailMessage())
			{
				if (reportData.IsIncludeFailureReportLink)
				{
					MigrationReportGenerator.ComposeAttachmentFromResource(migrationEmailMessageItem, "ErrorImage.gif", "ErrorImage");
				}
				if (!string.IsNullOrEmpty(reportData.LicensingHelpUrl))
				{
					MigrationReportGenerator.ComposeAttachmentFromResource(migrationEmailMessageItem, "Information.gif", "Information");
				}
				MultiValuedProperty<SmtpAddress> notificationEmails = this.migrationJob.NotificationEmails;
				string emailSubject = reportData.EmailSubject;
				string successReportLink = null;
				string failureReportLink = null;
				if (this.SupportAttachmentGeneration)
				{
					MigrationUtil.AssertOrThrow(reportData.ReportUrls != null, "report urls should be set by the time email is sent.  See GenerateUrls", new object[0]);
					if (!string.IsNullOrEmpty(reportData.ReportUrls.SuccessUrl))
					{
						successReportLink = string.Format(CultureInfo.CurrentCulture, "<a href=\"{0}\">{1}</a>", new object[]
						{
							reportData.ReportUrls.SuccessUrl,
							Strings.StatisticsReportLink
						});
					}
					if (!string.IsNullOrEmpty(reportData.ReportUrls.ErrorUrl))
					{
						failureReportLink = string.Format(CultureInfo.CurrentCulture, "<a href=\"{0}\">{1}</a>", new object[]
						{
							reportData.ReportUrls.ErrorUrl,
							Strings.ErrorReportLink
						});
					}
				}
				IEnumerable<KeyValuePair<string, string>> bodyData = reportData.TemplateDataGenerator(reportData.ReportingCursor, successReportLink, failureReportLink);
				string body = reportData.ComposeBodyFromTemplate(bodyData);
				ExTraceGlobals.FaultInjectionTracer.TraceTest(4209388861U);
				migrationEmailMessageItem.Send(notificationEmails, emailSubject, body);
			}
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x00042FC4 File Offset: 0x000411C4
		internal MigrationReportSet GenerateUrls(MigrationReportData reportData)
		{
			string successUrl = null;
			string errorUrl = null;
			ExDateTime? exDateTime = null;
			using (IMigrationDataProvider providerForFolder = this.dataProvider.GetProviderForFolder(MigrationFolderName.SyncMigrationReports))
			{
				if (reportData.IsIncludeSuccessReportLink)
				{
					MigrationReportItem migrationReportItem = MigrationReportItem.Get(providerForFolder, this.SuccessReportId);
					successUrl = migrationReportItem.GetUrl(providerForFolder);
					exDateTime = new ExDateTime?(migrationReportItem.CreationTime);
				}
				if (reportData.IsIncludeFailureReportLink)
				{
					MigrationReportItem migrationReportItem2 = MigrationReportItem.Get(providerForFolder, this.FailureReportId);
					errorUrl = migrationReportItem2.GetUrl(providerForFolder);
					if (exDateTime == null)
					{
						exDateTime = new ExDateTime?(migrationReportItem2.CreationTime);
					}
				}
			}
			if (exDateTime == null)
			{
				exDateTime = new ExDateTime?(ExDateTime.UtcNow);
			}
			reportData.ReportUrls = new MigrationReportSet((DateTime)exDateTime.Value, successUrl, errorUrl);
			this.migrationJob.SetReportUrls(this.dataProvider, reportData.ReportUrls);
			return reportData.ReportUrls;
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x000430B4 File Offset: 0x000412B4
		private static MigrationReportItem CreateReportItem(IMigrationDataProvider dataProvider, MigrationJob migrationJob, MigrationReportType reportType, string reportFileName)
		{
			MigrationReportItem result;
			using (IMigrationDataProvider providerForFolder = dataProvider.GetProviderForFolder(MigrationFolderName.SyncMigrationReports))
			{
				result = MigrationReportItem.Create(providerForFolder, new Guid?(migrationJob.JobId), migrationJob.MigrationType, migrationJob.IsStaged, reportType, reportFileName);
			}
			return result;
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x00043108 File Offset: 0x00041308
		private static Stream GetResourceStream(string resourceFile)
		{
			return Assembly.GetAssembly(typeof(JobSyncCompletingProcessor)).GetManifestResourceStream(resourceFile);
		}

		// Token: 0x04000584 RID: 1412
		internal const string ErrorImageName = "ErrorImage";

		// Token: 0x04000585 RID: 1413
		internal const string CsvExtension = ".csv";

		// Token: 0x04000586 RID: 1414
		internal const string ReportLinkUrlFormat = "<a href=\"{0}\">{1}</a>";

		// Token: 0x04000587 RID: 1415
		internal const string ErrorImageExtension = ".gif";

		// Token: 0x04000588 RID: 1416
		internal const string InformationImageExtension = ".gif";

		// Token: 0x04000589 RID: 1417
		internal const string InformationImageName = "Information";

		// Token: 0x0400058A RID: 1418
		private readonly IMigrationDataProvider dataProvider;

		// Token: 0x0400058B RID: 1419
		private readonly MigrationJob migrationJob;

		// Token: 0x0400058C RID: 1420
		private MigrationReportId successReportId;

		// Token: 0x0400058D RID: 1421
		private MigrationReportId failureReportId;

		// Token: 0x0400058E RID: 1422
		private bool supportAttachmentGeneration;
	}
}
