using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000062 RID: 98
	internal static class TransportSyncCommonEventLogConstants
	{
		// Token: 0x0400010A RID: 266
		public const string EventSource = "MSExchangeTransportSyncCommon";

		// Token: 0x0400010B RID: 267
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_VerificationEmailNotSent = new ExEventLog.EventTuple(3221487619U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400010C RID: 268
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PerformanceCounterAccessDenied = new ExEventLog.EventTuple(3221487620U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000063 RID: 99
		private enum Category : short
		{
			// Token: 0x0400010E RID: 270
			SyncCommon = 1
		}

		// Token: 0x02000064 RID: 100
		internal enum Message : uint
		{
			// Token: 0x04000110 RID: 272
			VerificationEmailNotSent = 3221487619U,
			// Token: 0x04000111 RID: 273
			PerformanceCounterAccessDenied
		}
	}
}
