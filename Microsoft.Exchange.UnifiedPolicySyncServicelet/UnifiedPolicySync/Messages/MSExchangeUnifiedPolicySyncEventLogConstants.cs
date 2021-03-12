using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.UnifiedPolicySync.Messages
{
	// Token: 0x02000002 RID: 2
	public static class MSExchangeUnifiedPolicySyncEventLogConstants
	{
		// Token: 0x04000001 RID: 1
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceletStarting = new ExEventLog.EventTuple(1073742825U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000002 RID: 2
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceletStarted = new ExEventLog.EventTuple(1073742826U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000003 RID: 3
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceletStopping = new ExEventLog.EventTuple(1073742827U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000004 RID: 4
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceletStopped = new ExEventLog.EventTuple(1073742828U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000005 RID: 5
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceletFailedToRegisterNotificationRpcEndpoint = new ExEventLog.EventTuple(3221226477U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000006 RID: 6
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceletFailedToLoadAppConfig = new ExEventLog.EventTuple(3221226478U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000007 RID: 7
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceletFailedToStartBecauseofGrayException = new ExEventLog.EventTuple(3221226479U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000003 RID: 3
		private enum Category : short
		{
			// Token: 0x04000009 RID: 9
			General = 1
		}

		// Token: 0x02000004 RID: 4
		internal enum Message : uint
		{
			// Token: 0x0400000B RID: 11
			ServiceletStarting = 1073742825U,
			// Token: 0x0400000C RID: 12
			ServiceletStarted,
			// Token: 0x0400000D RID: 13
			ServiceletStopping,
			// Token: 0x0400000E RID: 14
			ServiceletStopped,
			// Token: 0x0400000F RID: 15
			ServiceletFailedToRegisterNotificationRpcEndpoint = 3221226477U,
			// Token: 0x04000010 RID: 16
			ServiceletFailedToLoadAppConfig,
			// Token: 0x04000011 RID: 17
			ServiceletFailedToStartBecauseofGrayException
		}
	}
}
