using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ThrottlingService
{
	// Token: 0x0200000F RID: 15
	internal static class ThrottlingServiceEventLogConstants
	{
		// Token: 0x04000051 RID: 81
		public const string EventSource = "MSExchangeThrottling";

		// Token: 0x04000052 RID: 82
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RegisterRpcServerFailure = new ExEventLog.EventTuple(3221488617U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000053 RID: 83
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RpcSecurityDescriptorFailure = new ExEventLog.EventTuple(3221488618U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000054 RID: 84
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RemovePrivilegesFailure = new ExEventLog.EventTuple(3221488619U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000055 RID: 85
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxThrottled = new ExEventLog.EventTuple(1074004972U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x02000010 RID: 16
		private enum Category : short
		{
			// Token: 0x04000057 RID: 87
			General = 1
		}

		// Token: 0x02000011 RID: 17
		internal enum Message : uint
		{
			// Token: 0x04000059 RID: 89
			RegisterRpcServerFailure = 3221488617U,
			// Token: 0x0400005A RID: 90
			RpcSecurityDescriptorFailure,
			// Token: 0x0400005B RID: 91
			RemovePrivilegesFailure,
			// Token: 0x0400005C RID: 92
			MailboxThrottled = 1074004972U
		}
	}
}
