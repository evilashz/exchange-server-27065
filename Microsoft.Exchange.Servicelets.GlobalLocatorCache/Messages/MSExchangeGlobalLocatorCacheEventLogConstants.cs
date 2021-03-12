using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.GlobalLocatorCache.Messages
{
	// Token: 0x02000002 RID: 2
	public static class MSExchangeGlobalLocatorCacheEventLogConstants
	{
		// Token: 0x04000001 RID: 1
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GlobalLocatorCacheServiceStarting = new ExEventLog.EventTuple(1073742825U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000002 RID: 2
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GlobalLocatorCacheServiceStarted = new ExEventLog.EventTuple(1073742826U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000003 RID: 3
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GlobalLocatorCacheServiceStopped = new ExEventLog.EventTuple(1073742827U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000004 RID: 4
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GlobalLocatorCacheServiceFailedToRegisterEndpoint = new ExEventLog.EventTuple(3221226476U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000005 RID: 5
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GlobalLocatorCacheLoaded = new ExEventLog.EventTuple(1073742829U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000006 RID: 6
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GlobalLocatorCacheLoadFailed = new ExEventLog.EventTuple(3221226478U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000003 RID: 3
		private enum Category : short
		{
			// Token: 0x04000008 RID: 8
			General = 1
		}

		// Token: 0x02000004 RID: 4
		internal enum Message : uint
		{
			// Token: 0x0400000A RID: 10
			GlobalLocatorCacheServiceStarting = 1073742825U,
			// Token: 0x0400000B RID: 11
			GlobalLocatorCacheServiceStarted,
			// Token: 0x0400000C RID: 12
			GlobalLocatorCacheServiceStopped,
			// Token: 0x0400000D RID: 13
			GlobalLocatorCacheServiceFailedToRegisterEndpoint = 3221226476U,
			// Token: 0x0400000E RID: 14
			GlobalLocatorCacheLoaded = 1073742829U,
			// Token: 0x0400000F RID: 15
			GlobalLocatorCacheLoadFailed = 3221226478U
		}
	}
}
