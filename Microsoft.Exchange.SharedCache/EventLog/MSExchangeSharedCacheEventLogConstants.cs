using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.SharedCache.EventLog
{
	// Token: 0x02000004 RID: 4
	public static class MSExchangeSharedCacheEventLogConstants
	{
		// Token: 0x0400000A RID: 10
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStarting = new ExEventLog.EventTuple(1073742825U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000B RID: 11
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStarted = new ExEventLog.EventTuple(1002U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000C RID: 12
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStopping = new ExEventLog.EventTuple(1073742827U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000D RID: 13
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStopped = new ExEventLog.EventTuple(1004U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000E RID: 14
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CacheRegistered = new ExEventLog.EventTuple(1005U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000F RID: 15
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnhandledException = new ExEventLog.EventTuple(3221226482U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000005 RID: 5
		private enum Category : short
		{
			// Token: 0x04000011 RID: 17
			General = 1
		}

		// Token: 0x02000006 RID: 6
		internal enum Message : uint
		{
			// Token: 0x04000013 RID: 19
			ServiceStarting = 1073742825U,
			// Token: 0x04000014 RID: 20
			ServiceStarted = 1002U,
			// Token: 0x04000015 RID: 21
			ServiceStopping = 1073742827U,
			// Token: 0x04000016 RID: 22
			ServiceStopped = 1004U,
			// Token: 0x04000017 RID: 23
			CacheRegistered,
			// Token: 0x04000018 RID: 24
			UnhandledException = 3221226482U
		}
	}
}
