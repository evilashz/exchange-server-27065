using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Assistants.Diagnostics
{
	// Token: 0x0200009B RID: 155
	internal static class DiagnosticsFormatter
	{
		// Token: 0x0600048E RID: 1166 RVA: 0x000188A0 File Offset: 0x00016AA0
		public static XElement FormatRootElement()
		{
			return new XElement("MailboxAssistants");
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x000188B1 File Offset: 0x00016AB1
		public static XElement FormatAssistantRoot(string assistantRootName)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("assistantRootName", assistantRootName);
			return new XElement("MailboxAssistant", new XAttribute("Type", assistantRootName));
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x000188DD File Offset: 0x00016ADD
		public static XElement FormatDatabasesRoot()
		{
			return new XElement("MailboxDatabases");
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x000188EE File Offset: 0x00016AEE
		public static XElement FormatWorkcycleInfoElement(TimeSpan workcycle)
		{
			return new XElement("WorkcycleInterval", workcycle.ToString());
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0001890C File Offset: 0x00016B0C
		public static XElement FormatWorkcycleCheckpointInfoElement(TimeSpan workcycleCheckpoint)
		{
			return new XElement("WorkcycleCheckpointInterval", workcycleCheckpoint.ToString());
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0001892C File Offset: 0x00016B2C
		public static XElement FormatTimeBasedJobDatabaseStats(string dbName, Guid dbGuid, DiagnosticsSummaryDatabase dbSummary)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("dbName", dbName);
			ArgumentValidator.ThrowIfEmpty("dbGuid", dbGuid);
			ArgumentValidator.ThrowIfNull("dbSummary", dbSummary);
			XElement xelement = DiagnosticsFormatter.FormatTimeBasedJobDatabaseStatsCommon(dbName, dbGuid, dbSummary);
			XElement content = DiagnosticsFormatter.FormatTimeBasedJobMailboxStatsWindow("WindowJob", dbSummary.WindowJobStatistics);
			XElement content2 = DiagnosticsFormatter.FormatTimeBasedJobMailboxStats("OnDemandJobs", dbSummary.OnDemandJobsStatistics);
			xelement.Add(content);
			xelement.Add(content2);
			return xelement;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00018994 File Offset: 0x00016B94
		public static XElement FormatTimeBasedJobDatabaseStatsCommon(string dbName, Guid dbGuid, DiagnosticsSummaryDatabase dbSummary)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("dbName", dbName);
			ArgumentValidator.ThrowIfEmpty("dbGuid", dbGuid);
			ArgumentValidator.ThrowIfNull("dbSummary", dbSummary);
			XElement xelement = new XElement("MailboxDatabase", new XAttribute("Name", dbName));
			xelement.Add(new XElement("Guid", dbGuid));
			xelement.Add(new XElement("IsAssistantEnabled", dbSummary.IsAssistantEnabled.ToString().ToLower(CultureInfo.InvariantCulture)));
			xelement.Add(new XElement("StartTime", dbSummary.StartTime.ToString("O", CultureInfo.InvariantCulture)));
			return xelement;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00018A58 File Offset: 0x00016C58
		public static XElement FormatTimeBasedMailboxes(bool formatActive, string dbName, Guid dbGuid, DiagnosticsSummaryDatabase dbSummary, IEnumerable<Guid> mailboxCollection)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("dbName", dbName);
			ArgumentValidator.ThrowIfEmpty("dbGuid", dbGuid);
			ArgumentValidator.ThrowIfNull("dbSummary", dbSummary);
			ArgumentValidator.ThrowIfNull("mailboxCollection", mailboxCollection);
			string expandedName = formatActive ? "Running" : "Queued";
			XElement xelement = DiagnosticsFormatter.FormatTimeBasedJobDatabaseStatsCommon(dbName, dbGuid, dbSummary);
			XElement xelement2 = new XElement(expandedName);
			foreach (Guid guid in mailboxCollection)
			{
				XElement content = new XElement("MailboxGuid", guid);
				xelement2.Add(content);
			}
			xelement.Add(xelement2);
			return xelement;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00018B18 File Offset: 0x00016D18
		public static XElement FormatWindowJobHistory(string dbName, Guid dbGuid, DiagnosticsSummaryDatabase dbSummary, DiagnosticsSummaryJobWindow[] windowJobHistory)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("dbName", dbName);
			ArgumentValidator.ThrowIfEmpty("dbGuid", dbGuid);
			ArgumentValidator.ThrowIfNull("dbSummary", dbSummary);
			ArgumentValidator.ThrowIfNull("windowJobHistory", windowJobHistory);
			XElement xelement = DiagnosticsFormatter.FormatTimeBasedJobDatabaseStatsCommon(dbName, dbGuid, dbSummary);
			XElement xelement2 = new XElement("WindowJobs");
			foreach (XElement content in windowJobHistory.Select(new Func<DiagnosticsSummaryJobWindow, XElement>(DiagnosticsFormatter.FormatTimeBasedJobWindowHistoryEntry)))
			{
				xelement2.Add(content);
			}
			xelement.Add(xelement2);
			return xelement;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00018BC0 File Offset: 0x00016DC0
		public static XElement FormatErrorElement(Exception exception)
		{
			ArgumentValidator.ThrowIfNull("exception", exception);
			return DiagnosticsFormatter.FormatErrorElement(exception.Message);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00018BD8 File Offset: 0x00016DD8
		public static XElement FormatErrorElement(string message)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("message", message);
			XElement xelement = DiagnosticsFormatter.FormatRootElement();
			xelement.Add(new XElement("Error", message));
			return xelement;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00018C10 File Offset: 0x00016E10
		public static XElement FormatHelpElement(DiagnosticsArgument arguments)
		{
			ArgumentValidator.ThrowIfNull("arguments", arguments);
			XElement xelement = DiagnosticsFormatter.FormatRootElement();
			xelement.Add(new XElement("Help", "Supported arguments: " + arguments.GetSupportedArguments()));
			return xelement;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00018C54 File Offset: 0x00016E54
		private static XElement FormatTimeBasedJobMailboxStats(string jobElementName, DiagnosticsSummaryJob mbxSummary)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("jobElementName", jobElementName);
			ArgumentValidator.ThrowIfNull("mbxSummary", mbxSummary);
			XElement xelement = new XElement(jobElementName);
			xelement.Add(new XElement("ProcessingMailboxCount", mbxSummary.ProcessingCount));
			xelement.Add(new XElement("CompletedMailboxCount", mbxSummary.ProcessedSuccessfullyCount));
			xelement.Add(new XElement("FailedMailboxCount", mbxSummary.ProcessedFailureCount));
			xelement.Add(new XElement("FailedToOpenStoreSessionCount", mbxSummary.FailedToOpenStoreSessionCount));
			xelement.Add(new XElement("RetriedMailboxCount", mbxSummary.RetriedCount));
			xelement.Add(new XElement("QueuedMailboxCount", mbxSummary.QueuedCount));
			return xelement;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00018D44 File Offset: 0x00016F44
		private static XElement FormatTimeBasedJobMailboxStatsWindow(string jobElementName, DiagnosticsSummaryJobWindow mbxSummary)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("jobElementName", jobElementName);
			ArgumentValidator.ThrowIfNull("mbxSummary", mbxSummary);
			XElement xelement = DiagnosticsFormatter.FormatTimeBasedJobMailboxStats(jobElementName, mbxSummary.DiagnosticsSummaryJob);
			xelement.AddFirst(new XElement("FailedFilteringMailboxCount", mbxSummary.FailedFilteringCount));
			xelement.AddFirst(new XElement("FilteredMailboxCount", mbxSummary.FilteredMailboxCount));
			xelement.AddFirst(new XElement("NotInterestingMailboxCount", mbxSummary.NotInterestingCount));
			xelement.AddFirst(new XElement("InterestingMailboxCount", mbxSummary.InterestingCount));
			xelement.AddFirst(new XElement("TotalOnDatabaseMailboxCount", mbxSummary.TotalOnDatabaseCount));
			xelement.AddFirst(new XElement("StartTime", mbxSummary.StartTime.ToString("O", CultureInfo.InvariantCulture)));
			xelement.Add(new XElement("MovedToOnDemandMailboxCount", mbxSummary.ProcessedSeparatelyCount));
			return xelement;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00018E64 File Offset: 0x00017064
		private static XElement FormatTimeBasedJobWindowHistoryEntry(DiagnosticsSummaryJobWindow windowJob)
		{
			ArgumentValidator.ThrowIfNull("windowJob", windowJob);
			XElement xelement = new XElement("WindowJob");
			xelement.Add(new XElement("StartTime", windowJob.StartTime.ToString("O", CultureInfo.InvariantCulture)));
			xelement.Add(new XElement("EndTime", windowJob.EndTime.ToString("O", CultureInfo.InvariantCulture)));
			xelement.Add(new XElement("TotalOnDatabaseMailboxCount", windowJob.TotalOnDatabaseCount));
			xelement.Add(new XElement("InterestingMailboxCount", windowJob.InterestingCount));
			xelement.Add(new XElement("NotInterestingMailboxCount", windowJob.NotInterestingCount));
			xelement.Add(new XElement("FilteredMailboxCount", windowJob.FilteredMailboxCount));
			xelement.Add(new XElement("FailedFilteringMailboxCount", windowJob.FailedFilteringCount));
			xelement.Add(new XElement("CompletedMailboxCount", windowJob.DiagnosticsSummaryJob.ProcessedSuccessfullyCount));
			xelement.Add(new XElement("MovedToOnDemandMailboxCount", windowJob.ProcessedSeparatelyCount));
			xelement.Add(new XElement("FailedMailboxCount", windowJob.DiagnosticsSummaryJob.ProcessedFailureCount));
			xelement.Add(new XElement("FailedToOpenStoreSessionCount", windowJob.DiagnosticsSummaryJob.FailedToOpenStoreSessionCount));
			xelement.Add(new XElement("RetriedMailboxCount", windowJob.DiagnosticsSummaryJob.RetriedCount));
			return xelement;
		}

		// Token: 0x040002B4 RID: 692
		private const string MailboxAssistant = "MailboxAssistant";

		// Token: 0x040002B5 RID: 693
		private const string Databases = "MailboxDatabases";

		// Token: 0x040002B6 RID: 694
		private const string Database = "MailboxDatabase";

		// Token: 0x040002B7 RID: 695
		private const string WindowJob = "WindowJob";

		// Token: 0x040002B8 RID: 696
		private const string WindowJobs = "WindowJobs";

		// Token: 0x040002B9 RID: 697
		private const string OnDemandJobs = "OnDemandJobs";

		// Token: 0x040002BA RID: 698
		private const string Running = "Running";

		// Token: 0x040002BB RID: 699
		private const string Queued = "Queued";

		// Token: 0x040002BC RID: 700
		private const string NameAttr = "Name";

		// Token: 0x040002BD RID: 701
		private const string TypeAttr = "Type";

		// Token: 0x040002BE RID: 702
		private const string WorkcycleInterval = "WorkcycleInterval";

		// Token: 0x040002BF RID: 703
		private const string WorkcycleCheckpointInterval = "WorkcycleCheckpointInterval";

		// Token: 0x040002C0 RID: 704
		private const string TotalOnDatabaseCount = "TotalOnDatabaseMailboxCount";

		// Token: 0x040002C1 RID: 705
		private const string InterestingCount = "InterestingMailboxCount";

		// Token: 0x040002C2 RID: 706
		private const string NotInterestingCount = "NotInterestingMailboxCount";

		// Token: 0x040002C3 RID: 707
		private const string FilteredMailboxCount = "FilteredMailboxCount";

		// Token: 0x040002C4 RID: 708
		private const string FailedFilteringMailboxCount = "FailedFilteringMailboxCount";

		// Token: 0x040002C5 RID: 709
		private const string ProcessingCount = "ProcessingMailboxCount";

		// Token: 0x040002C6 RID: 710
		private const string CompletedMailboxCount = "CompletedMailboxCount";

		// Token: 0x040002C7 RID: 711
		private const string FailedMailboxCount = "FailedMailboxCount";

		// Token: 0x040002C8 RID: 712
		private const string FailedToOpenStoreSessionCount = "FailedToOpenStoreSessionCount";

		// Token: 0x040002C9 RID: 713
		private const string MovedToOnDemandMailboxCount = "MovedToOnDemandMailboxCount";

		// Token: 0x040002CA RID: 714
		private const string RetriedMailboxCount = "RetriedMailboxCount";

		// Token: 0x040002CB RID: 715
		private const string QueuedCount = "QueuedMailboxCount";

		// Token: 0x040002CC RID: 716
		private const string MailboxGuid = "MailboxGuid";

		// Token: 0x040002CD RID: 717
		private const string DatabaseGuid = "Guid";

		// Token: 0x040002CE RID: 718
		private const string IsAssistantEnabled = "IsAssistantEnabled";

		// Token: 0x040002CF RID: 719
		private const string StartTime = "StartTime";

		// Token: 0x040002D0 RID: 720
		private const string EndTime = "EndTime";

		// Token: 0x040002D1 RID: 721
		private const string Error = "Error";

		// Token: 0x040002D2 RID: 722
		private const string Help = "Help";

		// Token: 0x040002D3 RID: 723
		private const string SupportedAgrs = "Supported arguments: ";

		// Token: 0x040002D4 RID: 724
		private const string DateTimeFormat = "O";
	}
}
