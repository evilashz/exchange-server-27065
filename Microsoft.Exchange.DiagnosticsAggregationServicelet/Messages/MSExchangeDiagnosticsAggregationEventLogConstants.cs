using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.DiagnosticsAggregation.Messages
{
	// Token: 0x02000010 RID: 16
	public static class MSExchangeDiagnosticsAggregationEventLogConstants
	{
		// Token: 0x04000058 RID: 88
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiagnosticsAggregationServiceletIsDisabled = new ExEventLog.EventTuple(1073742825U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000059 RID: 89
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiagnosticsAggregationServiceUnexpectedException = new ExEventLog.EventTuple(3221226475U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005A RID: 90
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiagnosticsAggregationServiceletLoadFailed = new ExEventLog.EventTuple(3221226476U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005B RID: 91
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiagnosticsAggregationRehostingFailed = new ExEventLog.EventTuple(3221226478U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000011 RID: 17
		private enum Category : short
		{
			// Token: 0x0400005D RID: 93
			General = 1
		}

		// Token: 0x02000012 RID: 18
		internal enum Message : uint
		{
			// Token: 0x0400005F RID: 95
			DiagnosticsAggregationServiceletIsDisabled = 1073742825U,
			// Token: 0x04000060 RID: 96
			DiagnosticsAggregationServiceUnexpectedException = 3221226475U,
			// Token: 0x04000061 RID: 97
			DiagnosticsAggregationServiceletLoadFailed,
			// Token: 0x04000062 RID: 98
			DiagnosticsAggregationRehostingFailed = 3221226478U
		}
	}
}
