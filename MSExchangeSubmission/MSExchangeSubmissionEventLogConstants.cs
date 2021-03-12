using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.Submission
{
	// Token: 0x02000012 RID: 18
	internal static class MSExchangeSubmissionEventLogConstants
	{
		// Token: 0x0400007A RID: 122
		public const string EventSource = "MSExchangeSubmission";

		// Token: 0x0400007B RID: 123
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SubmissionServiceStartSuccess = new ExEventLog.EventTuple(263144U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400007C RID: 124
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SubmissionServiceStopSuccess = new ExEventLog.EventTuple(263145U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400007D RID: 125
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SubmissionServiceStartFailure = new ExEventLog.EventTuple(3221488618U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400007E RID: 126
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SubmissionServiceStopFailure = new ExEventLog.EventTuple(3221488619U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400007F RID: 127
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SubmissionHang = new ExEventLog.EventTuple(3221488644U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x02000013 RID: 19
		private enum Category : short
		{
			// Token: 0x04000081 RID: 129
			MSExchangeSubmission = 1
		}

		// Token: 0x02000014 RID: 20
		internal enum Message : uint
		{
			// Token: 0x04000083 RID: 131
			SubmissionServiceStartSuccess = 263144U,
			// Token: 0x04000084 RID: 132
			SubmissionServiceStopSuccess,
			// Token: 0x04000085 RID: 133
			SubmissionServiceStartFailure = 3221488618U,
			// Token: 0x04000086 RID: 134
			SubmissionServiceStopFailure,
			// Token: 0x04000087 RID: 135
			SubmissionHang = 3221488644U
		}
	}
}
