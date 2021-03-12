using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Extensibility.EventLog
{
	// Token: 0x0200006D RID: 109
	internal static class EdgeExtensibilityEventLogConstants
	{
		// Token: 0x04000433 RID: 1075
		public const string EventSource = "MSExchange Extensibility";

		// Token: 0x04000434 RID: 1076
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MExAgentTooSlow = new ExEventLog.EventTuple(2147746842U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000435 RID: 1077
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MExAgentFault = new ExEventLog.EventTuple(2147746843U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000436 RID: 1078
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MExAgentFactoryCreationFailure = new ExEventLog.EventTuple(3221488668U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000437 RID: 1079
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MExAgentInstanceCreationFailure = new ExEventLog.EventTuple(3221488669U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000438 RID: 1080
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MExAgentVersionMismatch = new ExEventLog.EventTuple(3221488670U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000439 RID: 1081
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MExAgentFactoryStartupDelay = new ExEventLog.EventTuple(2147746847U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400043A RID: 1082
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MExAgentCompletedTwice = new ExEventLog.EventTuple(3221488672U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400043B RID: 1083
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MExAgentDidNotCallResume = new ExEventLog.EventTuple(3221488673U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0200006E RID: 110
		private enum Category : short
		{
			// Token: 0x0400043D RID: 1085
			MExRuntime = 1
		}

		// Token: 0x0200006F RID: 111
		internal enum Message : uint
		{
			// Token: 0x0400043F RID: 1087
			MExAgentTooSlow = 2147746842U,
			// Token: 0x04000440 RID: 1088
			MExAgentFault,
			// Token: 0x04000441 RID: 1089
			MExAgentFactoryCreationFailure = 3221488668U,
			// Token: 0x04000442 RID: 1090
			MExAgentInstanceCreationFailure,
			// Token: 0x04000443 RID: 1091
			MExAgentVersionMismatch,
			// Token: 0x04000444 RID: 1092
			MExAgentFactoryStartupDelay = 2147746847U,
			// Token: 0x04000445 RID: 1093
			MExAgentCompletedTwice = 3221488672U,
			// Token: 0x04000446 RID: 1094
			MExAgentDidNotCallResume
		}
	}
}
