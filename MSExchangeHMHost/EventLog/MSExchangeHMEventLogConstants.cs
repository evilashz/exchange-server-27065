using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.ActiveMonitoring.EventLog
{
	// Token: 0x02000003 RID: 3
	public static class MSExchangeHMEventLogConstants
	{
		// Token: 0x04000010 RID: 16
		public const string EventSource = "MSExchangeHM";

		// Token: 0x04000011 RID: 17
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HealthManagerWorkerStarted = new ExEventLog.EventTuple(1074004969U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000012 RID: 18
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HealthManagerWorkerStopped = new ExEventLog.EventTuple(1074004970U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000013 RID: 19
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HealthManagerWorkerActivated = new ExEventLog.EventTuple(1074004971U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000014 RID: 20
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HealthManagerWorkerPaused = new ExEventLog.EventTuple(1074004972U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000015 RID: 21
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HealthManagerWorkerResumed = new ExEventLog.EventTuple(1074004973U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000016 RID: 22
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HealthManagerWorkerStopping = new ExEventLog.EventTuple(1074004974U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000017 RID: 23
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OpenSemaphoreFailed = new ExEventLog.EventTuple(3221488623U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000018 RID: 24
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerRestartOnDataAccessError = new ExEventLog.EventTuple(3221488624U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000019 RID: 25
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerRestartOnPoisonResult = new ExEventLog.EventTuple(3221488625U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001A RID: 26
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerRestartOnMaintenance = new ExEventLog.EventTuple(1074004978U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001B RID: 27
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerRestartOnUnknown = new ExEventLog.EventTuple(3221488627U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001C RID: 28
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerExitGracefully = new ExEventLog.EventTuple(1074004980U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001D RID: 29
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HealthManagerWorkerRetiring = new ExEventLog.EventTuple(1074004982U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001E RID: 30
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerExitWithException = new ExEventLog.EventTuple(1074004983U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001F RID: 31
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RpcServerFailedToStart = new ExEventLog.EventTuple(3221488632U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000020 RID: 32
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToGetIsOnlineState = new ExEventLog.EventTuple(3221488633U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000004 RID: 4
		private enum Category : short
		{
			// Token: 0x04000022 RID: 34
			Service = 1,
			// Token: 0x04000023 RID: 35
			Worker
		}

		// Token: 0x02000005 RID: 5
		internal enum Message : uint
		{
			// Token: 0x04000025 RID: 37
			HealthManagerWorkerStarted = 1074004969U,
			// Token: 0x04000026 RID: 38
			HealthManagerWorkerStopped,
			// Token: 0x04000027 RID: 39
			HealthManagerWorkerActivated,
			// Token: 0x04000028 RID: 40
			HealthManagerWorkerPaused,
			// Token: 0x04000029 RID: 41
			HealthManagerWorkerResumed,
			// Token: 0x0400002A RID: 42
			HealthManagerWorkerStopping,
			// Token: 0x0400002B RID: 43
			OpenSemaphoreFailed = 3221488623U,
			// Token: 0x0400002C RID: 44
			WorkerRestartOnDataAccessError,
			// Token: 0x0400002D RID: 45
			WorkerRestartOnPoisonResult,
			// Token: 0x0400002E RID: 46
			WorkerRestartOnMaintenance = 1074004978U,
			// Token: 0x0400002F RID: 47
			WorkerRestartOnUnknown = 3221488627U,
			// Token: 0x04000030 RID: 48
			WorkerExitGracefully = 1074004980U,
			// Token: 0x04000031 RID: 49
			HealthManagerWorkerRetiring = 1074004982U,
			// Token: 0x04000032 RID: 50
			WorkerExitWithException,
			// Token: 0x04000033 RID: 51
			RpcServerFailedToStart = 3221488632U,
			// Token: 0x04000034 RID: 52
			FailedToGetIsOnlineState
		}
	}
}
