using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants.Diagnostics;
using Microsoft.Exchange.Assistants.EventLog;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200008E RID: 142
	internal sealed class TimeBasedDatabaseWindowJob : TimeBasedDatabaseJob
	{
		// Token: 0x0600045C RID: 1116 RVA: 0x000165B1 File Offset: 0x000147B1
		public TimeBasedDatabaseWindowJob(TimeBasedDatabaseDriver driver, List<MailboxData> queue, int notInterestingCount, int filteredCount, int failedFilteringCount, int totalOnDatabaseCount, PoisonMailboxControl poisonControl, PerformanceCountersPerDatabaseInstance databaseCounters) : base(driver, queue, poisonControl, databaseCounters)
		{
			this.notInterestingMailboxCount = notInterestingCount;
			this.filteredMailboxCount = filteredCount;
			this.failedFilteringMailboxCount = failedFilteringCount;
			this.totalOnDatabaseCount = totalOnDatabaseCount;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x000165E0 File Offset: 0x000147E0
		protected override void LogJobBegin(int initialPendingQueueCount)
		{
			base.StartTime = DateTime.UtcNow;
			base.LogEvent(AssistantsEventLogConstants.Tuple_TimeWindowBegin, null, new object[]
			{
				base.Assistant.Name,
				base.DatabaseInfo.DisplayName,
				initialPendingQueueCount
			});
			AssistantsLog.LogBeginJob(base.Assistant.NonLocalizedName, base.DatabaseInfo.DisplayName, initialPendingQueueCount);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00016654 File Offset: 0x00014854
		protected override void LogJobEnd(int initialPendingQueueCount, int mailboxesProcessedSuccessfullyCount, int mailboxesProcessedFailureCount, int mailboxesFailedToOpenStoreSessionCount, int mailboxesProcessedSeparatelyCount, int mailboxesRetriedCount)
		{
			int num = initialPendingQueueCount - mailboxesProcessedSuccessfullyCount - mailboxesProcessedFailureCount - mailboxesFailedToOpenStoreSessionCount - mailboxesProcessedSeparatelyCount + mailboxesRetriedCount;
			base.EndTime = DateTime.UtcNow;
			AssistantEndWorkCycleCheckpointStatistics assistantEndWorkCycleCheckpointStatistics = new AssistantEndWorkCycleCheckpointStatistics
			{
				DatabaseName = base.DatabaseInfo.DisplayName,
				StartTime = base.StartTime,
				EndTime = base.EndTime,
				TotalMailboxCount = initialPendingQueueCount,
				ProcessedMailboxCount = mailboxesProcessedSuccessfullyCount,
				MailboxErrorCount = mailboxesProcessedFailureCount,
				FailedToOpenStoreSessionCount = mailboxesFailedToOpenStoreSessionCount,
				RetriedMailboxCount = mailboxesRetriedCount,
				MailboxesProcessedSeparatelyCount = mailboxesProcessedSeparatelyCount,
				MailboxRemainingCount = num
			};
			AssistantsLog.LogEndJobEvent(base.Assistant.NonLocalizedName, assistantEndWorkCycleCheckpointStatistics.FormatCustomData());
			if (mailboxesProcessedSuccessfullyCount == 0 && initialPendingQueueCount != 0)
			{
				base.LogEvent(AssistantsEventLogConstants.Tuple_DatabaseNotProcessedInTimeWindow, null, new object[]
				{
					base.Assistant.Name,
					base.DatabaseInfo.DisplayName,
					mailboxesProcessedFailureCount,
					mailboxesFailedToOpenStoreSessionCount,
					mailboxesRetriedCount,
					num
				});
				return;
			}
			base.LogEvent(AssistantsEventLogConstants.Tuple_TimeWindowEnd, null, new object[]
			{
				base.Assistant.Name,
				base.DatabaseInfo.DisplayName,
				base.EndTime.Subtract(base.StartTime),
				mailboxesProcessedSuccessfullyCount,
				mailboxesProcessedFailureCount,
				mailboxesProcessedSeparatelyCount,
				mailboxesFailedToOpenStoreSessionCount,
				mailboxesRetriedCount,
				num
			});
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x000167F0 File Offset: 0x000149F0
		public new DiagnosticsSummaryJobWindow GetJobDiagnosticsSummary()
		{
			return new DiagnosticsSummaryJobWindow(this.totalOnDatabaseCount, base.InterestingMailboxCount, this.notInterestingMailboxCount, this.filteredMailboxCount, this.failedFilteringMailboxCount, base.OnDemandMailboxCount, base.StartTime, base.EndTime, base.GetJobDiagnosticsSummary());
		}

		// Token: 0x0400027B RID: 635
		private readonly int totalOnDatabaseCount;

		// Token: 0x0400027C RID: 636
		private readonly int notInterestingMailboxCount;

		// Token: 0x0400027D RID: 637
		private readonly int filteredMailboxCount;

		// Token: 0x0400027E RID: 638
		private readonly int failedFilteringMailboxCount;
	}
}
