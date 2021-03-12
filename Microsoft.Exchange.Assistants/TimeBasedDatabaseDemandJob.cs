using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants.EventLog;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200008D RID: 141
	internal sealed class TimeBasedDatabaseDemandJob : TimeBasedDatabaseJob
	{
		// Token: 0x06000459 RID: 1113 RVA: 0x000164C0 File Offset: 0x000146C0
		public TimeBasedDatabaseDemandJob(TimeBasedDatabaseDriver driver, MailboxData mailboxData, PoisonMailboxControl poisonControl, PerformanceCountersPerDatabaseInstance databaseCounters) : base(driver, new List<MailboxData>(new MailboxData[]
		{
			mailboxData
		}), poisonControl, databaseCounters)
		{
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x000164E8 File Offset: 0x000146E8
		protected override void LogJobBegin(int initialPendingQueueCount)
		{
			base.StartTime = DateTime.UtcNow;
			base.LogEvent(AssistantsEventLogConstants.Tuple_TimeDemandJobBegin, null, new object[]
			{
				base.Assistant.Name,
				base.DatabaseInfo.DisplayName,
				initialPendingQueueCount
			});
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00016540 File Offset: 0x00014740
		protected override void LogJobEnd(int initialPendingQueueCount, int mailboxesProcessedSuccessfullyCount, int mailboxesProcessedFailureCount, int mailboxesFailedToOpenStoreSessionCount, int mailboxesProcessedSeparatelyCount, int mailboxesRetriedCount)
		{
			base.LogEvent(AssistantsEventLogConstants.Tuple_TimeDemandJobEnd, null, new object[]
			{
				base.Assistant.Name,
				base.DatabaseInfo.DisplayName,
				mailboxesProcessedSuccessfullyCount,
				initialPendingQueueCount,
				mailboxesProcessedFailureCount,
				mailboxesFailedToOpenStoreSessionCount,
				mailboxesRetriedCount
			});
		}
	}
}
