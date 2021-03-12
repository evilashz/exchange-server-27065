using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.WorkloadManagement.EventLogs
{
	// Token: 0x0200004B RID: 75
	internal static class WorkloadManagementEventLogConstants
	{
		// Token: 0x04000184 RID: 388
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ClassificationPerformanceCounterInitializationFailure = new ExEventLog.EventTuple(3221225473U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000185 RID: 389
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkloadPerformanceCounterInitializationFailure = new ExEventLog.EventTuple(3221225474U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000186 RID: 390
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkloadManagementPerformanceCounterInitializationFailure = new ExEventLog.EventTuple(3221225475U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000187 RID: 391
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AdmissionControlRefreshFailure = new ExEventLog.EventTuple(3221225476U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000188 RID: 392
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_StaleResourceMonitor = new ExEventLog.EventTuple(1073741829U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000189 RID: 393
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GetPolicyFromThresholdFailure = new ExEventLog.EventTuple(3221225478U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400018A RID: 394
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CiMdbCopyMonitorFailure = new ExEventLog.EventTuple(3221225479U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400018B RID: 395
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CiMdbCopyStatusFailure = new ExEventLog.EventTuple(1073741832U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0200004C RID: 76
		private enum Category : short
		{
			// Token: 0x0400018D RID: 397
			Core = 1
		}

		// Token: 0x0200004D RID: 77
		internal enum Message : uint
		{
			// Token: 0x0400018F RID: 399
			ClassificationPerformanceCounterInitializationFailure = 3221225473U,
			// Token: 0x04000190 RID: 400
			WorkloadPerformanceCounterInitializationFailure,
			// Token: 0x04000191 RID: 401
			WorkloadManagementPerformanceCounterInitializationFailure,
			// Token: 0x04000192 RID: 402
			AdmissionControlRefreshFailure,
			// Token: 0x04000193 RID: 403
			StaleResourceMonitor = 1073741829U,
			// Token: 0x04000194 RID: 404
			GetPolicyFromThresholdFailure = 3221225478U,
			// Token: 0x04000195 RID: 405
			CiMdbCopyMonitorFailure,
			// Token: 0x04000196 RID: 406
			CiMdbCopyStatusFailure = 1073741832U
		}
	}
}
