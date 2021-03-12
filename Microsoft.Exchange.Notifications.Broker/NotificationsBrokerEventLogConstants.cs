using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000039 RID: 57
	internal static class NotificationsBrokerEventLogConstants
	{
		// Token: 0x04000103 RID: 259
		public const string EventSource = "MSExchange Notifications Broker";

		// Token: 0x04000104 RID: 260
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStarting = new ExEventLog.EventTuple(1073742824U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000105 RID: 261
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStarted = new ExEventLog.EventTuple(1001U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000106 RID: 262
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStopping = new ExEventLog.EventTuple(1073742826U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000107 RID: 263
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStopped = new ExEventLog.EventTuple(1003U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000108 RID: 264
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NotRunningAsLocalSystem = new ExEventLog.EventTuple(3221226476U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000109 RID: 265
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RpcRegisterInterfaceFailure = new ExEventLog.EventTuple(3221226477U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400010A RID: 266
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidSubscriptionsOnLoad = new ExEventLog.EventTuple(3221226478U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0200003A RID: 58
		private enum Category : short
		{
			// Token: 0x0400010C RID: 268
			Service = 1
		}

		// Token: 0x0200003B RID: 59
		internal enum Message : uint
		{
			// Token: 0x0400010E RID: 270
			ServiceStarting = 1073742824U,
			// Token: 0x0400010F RID: 271
			ServiceStarted = 1001U,
			// Token: 0x04000110 RID: 272
			ServiceStopping = 1073742826U,
			// Token: 0x04000111 RID: 273
			ServiceStopped = 1003U,
			// Token: 0x04000112 RID: 274
			NotRunningAsLocalSystem = 3221226476U,
			// Token: 0x04000113 RID: 275
			RpcRegisterInterfaceFailure,
			// Token: 0x04000114 RID: 276
			InvalidSubscriptionsOnLoad
		}
	}
}
