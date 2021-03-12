using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.AuditLogSearch.Messages
{
	// Token: 0x02000012 RID: 18
	public static class MSExchangeAuditLogSearchEventLogConstants
	{
		// Token: 0x0400006C RID: 108
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceletException = new ExEventLog.EventTuple(3221229473U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006D RID: 109
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerException = new ExEventLog.EventTuple(3221229474U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006E RID: 110
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AuditLogSearchCompletedSuccessfully = new ExEventLog.EventTuple(1073745827U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006F RID: 111
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AuditLogSearchCompletedWithErrors = new ExEventLog.EventTuple(2147487652U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000070 RID: 112
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AuditLogSearchReadConfigError = new ExEventLog.EventTuple(2147487653U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000071 RID: 113
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AuditLogSearchServiceletStarted = new ExEventLog.EventTuple(1073745830U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000072 RID: 114
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AuditLogSearchServiceletEnded = new ExEventLog.EventTuple(1073745831U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000073 RID: 115
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AuditLogSearchStarted = new ExEventLog.EventTuple(1073745832U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000074 RID: 116
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AuditLogSearchEnded = new ExEventLog.EventTuple(1073745833U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000075 RID: 117
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TransientException = new ExEventLog.EventTuple(2147487658U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000013 RID: 19
		private enum Category : short
		{
			// Token: 0x04000077 RID: 119
			General = 1
		}

		// Token: 0x02000014 RID: 20
		internal enum Message : uint
		{
			// Token: 0x04000079 RID: 121
			ServiceletException = 3221229473U,
			// Token: 0x0400007A RID: 122
			WorkerException,
			// Token: 0x0400007B RID: 123
			AuditLogSearchCompletedSuccessfully = 1073745827U,
			// Token: 0x0400007C RID: 124
			AuditLogSearchCompletedWithErrors = 2147487652U,
			// Token: 0x0400007D RID: 125
			AuditLogSearchReadConfigError,
			// Token: 0x0400007E RID: 126
			AuditLogSearchServiceletStarted = 1073745830U,
			// Token: 0x0400007F RID: 127
			AuditLogSearchServiceletEnded,
			// Token: 0x04000080 RID: 128
			AuditLogSearchStarted,
			// Token: 0x04000081 RID: 129
			AuditLogSearchEnded,
			// Token: 0x04000082 RID: 130
			TransientException = 2147487658U
		}
	}
}
