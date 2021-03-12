using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x0200005A RID: 90
	internal static class TransportSyncManagerEventLogConstants
	{
		// Token: 0x04000264 RID: 612
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NoActiveBridgeheadServerForContentAggregation = new ExEventLog.EventTuple(2147746805U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000265 RID: 613
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SyncManagerConfigLoadSucceeded = new ExEventLog.EventTuple(1074004983U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000266 RID: 614
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SyncManagerConfigLoadFailed = new ExEventLog.EventTuple(3221488632U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000267 RID: 615
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SyncManagerConfigUpdateSucceeded = new ExEventLog.EventTuple(1074004985U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000268 RID: 616
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SyncManagerConfigUpdateFailed = new ExEventLog.EventTuple(3221488634U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000269 RID: 617
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SyncManagerResourcePoolLimitReached = new ExEventLog.EventTuple(3221488636U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400026A RID: 618
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SyncManagerTokenWaitTimedout = new ExEventLog.EventTuple(3221488637U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400026B RID: 619
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SyncManagerReportingTransientException = new ExEventLog.EventTuple(2147746815U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400026C RID: 620
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SyncManagerReportingSubscriptionReadException = new ExEventLog.EventTuple(2147746816U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400026D RID: 621
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SyncManagerDispatchOutageDetected = new ExEventLog.EventTuple(3221488641U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400026E RID: 622
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStarted = new ExEventLog.EventTuple(1074004994U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400026F RID: 623
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStopped = new ExEventLog.EventTuple(1074004995U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0200005B RID: 91
		private enum Category : short
		{
			// Token: 0x04000271 RID: 625
			SyncManager = 1
		}

		// Token: 0x0200005C RID: 92
		internal enum Message : uint
		{
			// Token: 0x04000273 RID: 627
			NoActiveBridgeheadServerForContentAggregation = 2147746805U,
			// Token: 0x04000274 RID: 628
			SyncManagerConfigLoadSucceeded = 1074004983U,
			// Token: 0x04000275 RID: 629
			SyncManagerConfigLoadFailed = 3221488632U,
			// Token: 0x04000276 RID: 630
			SyncManagerConfigUpdateSucceeded = 1074004985U,
			// Token: 0x04000277 RID: 631
			SyncManagerConfigUpdateFailed = 3221488634U,
			// Token: 0x04000278 RID: 632
			SyncManagerResourcePoolLimitReached = 3221488636U,
			// Token: 0x04000279 RID: 633
			SyncManagerTokenWaitTimedout,
			// Token: 0x0400027A RID: 634
			SyncManagerReportingTransientException = 2147746815U,
			// Token: 0x0400027B RID: 635
			SyncManagerReportingSubscriptionReadException,
			// Token: 0x0400027C RID: 636
			SyncManagerDispatchOutageDetected = 3221488641U,
			// Token: 0x0400027D RID: 637
			ServiceStarted = 1074004994U,
			// Token: 0x0400027E RID: 638
			ServiceStopped
		}
	}
}
