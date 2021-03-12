using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MigrationWorkflowService
{
	// Token: 0x02000025 RID: 37
	internal static class MWSEventLogConstants
	{
		// Token: 0x04000049 RID: 73
		public const string EventSource = "MSExchange Migration Workflow";

		// Token: 0x0400004A RID: 74
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStarted = new ExEventLog.EventTuple(263144U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004B RID: 75
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStopped = new ExEventLog.EventTuple(263145U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004C RID: 76
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceIsDisabled = new ExEventLog.EventTuple(2147746794U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004D RID: 77
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceFailedToStart = new ExEventLog.EventTuple(3221488619U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004E RID: 78
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AutomaticLoadBalanceStarting = new ExEventLog.EventTuple(3221488620U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004F RID: 79
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceletFailedToStart = new ExEventLog.EventTuple(2147746797U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000026 RID: 38
		private enum Category : short
		{
			// Token: 0x04000051 RID: 81
			Service = 1,
			// Token: 0x04000052 RID: 82
			LoadBalance
		}

		// Token: 0x02000027 RID: 39
		internal enum Message : uint
		{
			// Token: 0x04000054 RID: 84
			ServiceStarted = 263144U,
			// Token: 0x04000055 RID: 85
			ServiceStopped,
			// Token: 0x04000056 RID: 86
			ServiceIsDisabled = 2147746794U,
			// Token: 0x04000057 RID: 87
			ServiceFailedToStart = 3221488619U,
			// Token: 0x04000058 RID: 88
			AutomaticLoadBalanceStarting,
			// Token: 0x04000059 RID: 89
			ServiceletFailedToStart = 2147746797U
		}
	}
}
