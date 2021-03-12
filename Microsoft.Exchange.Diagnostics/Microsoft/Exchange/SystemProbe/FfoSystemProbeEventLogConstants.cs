using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.SystemProbe
{
	// Token: 0x0200020B RID: 523
	public static class FfoSystemProbeEventLogConstants
	{
		// Token: 0x04000B3E RID: 2878
		public const string EventSource = "FfoSystemProbe";

		// Token: 0x04000B3F RID: 2879
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SystemProbeStarted = new ExEventLog.EventTuple(1073742824U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B40 RID: 2880
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SystemProbeStopped = new ExEventLog.EventTuple(1073742825U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B41 RID: 2881
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SystemProbeConfigured = new ExEventLog.EventTuple(1073742826U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B42 RID: 2882
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SystemProbeFormatArgumentNullException = new ExEventLog.EventTuple(2147484651U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B43 RID: 2883
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SystemProbeFormatException = new ExEventLog.EventTuple(2147484652U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B44 RID: 2884
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SystemProbeAppendFileException = new ExEventLog.EventTuple(2147484653U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B45 RID: 2885
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SystemProbeLogPathNotConfigured = new ExEventLog.EventTuple(2147484654U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0200020C RID: 524
		private enum Category : short
		{
			// Token: 0x04000B47 RID: 2887
			General = 1
		}

		// Token: 0x0200020D RID: 525
		internal enum Message : uint
		{
			// Token: 0x04000B49 RID: 2889
			SystemProbeStarted = 1073742824U,
			// Token: 0x04000B4A RID: 2890
			SystemProbeStopped,
			// Token: 0x04000B4B RID: 2891
			SystemProbeConfigured,
			// Token: 0x04000B4C RID: 2892
			SystemProbeFormatArgumentNullException = 2147484651U,
			// Token: 0x04000B4D RID: 2893
			SystemProbeFormatException,
			// Token: 0x04000B4E RID: 2894
			SystemProbeAppendFileException,
			// Token: 0x04000B4F RID: 2895
			SystemProbeLogPathNotConfigured
		}
	}
}
