using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ThrottlingService.Client
{
	// Token: 0x02000009 RID: 9
	internal static class ThrottlingClientEventLogConstants
	{
		// Token: 0x04000031 RID: 49
		public const string EventSource = "MSExchangeThrottlingClient";

		// Token: 0x04000032 RID: 50
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CreateRpcClientFailure = new ExEventLog.EventTuple(3221488617U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000033 RID: 51
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RpcClientOperationFailure = new ExEventLog.EventTuple(3221488618U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000034 RID: 52
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RpcRequestBypassed = new ExEventLog.EventTuple(3221488619U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000035 RID: 53
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RpcRequestTimedout = new ExEventLog.EventTuple(3221488620U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000036 RID: 54
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxThrottled = new ExEventLog.EventTuple(1074004973U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0200000A RID: 10
		private enum Category : short
		{
			// Token: 0x04000038 RID: 56
			General = 1
		}

		// Token: 0x0200000B RID: 11
		internal enum Message : uint
		{
			// Token: 0x0400003A RID: 58
			CreateRpcClientFailure = 3221488617U,
			// Token: 0x0400003B RID: 59
			RpcClientOperationFailure,
			// Token: 0x0400003C RID: 60
			RpcRequestBypassed,
			// Token: 0x0400003D RID: 61
			RpcRequestTimedout,
			// Token: 0x0400003E RID: 62
			MailboxThrottled = 1074004973U
		}
	}
}
