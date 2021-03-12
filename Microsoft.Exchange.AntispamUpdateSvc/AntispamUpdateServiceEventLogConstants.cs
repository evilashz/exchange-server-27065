using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AntispamUpdate
{
	// Token: 0x02000006 RID: 6
	public static class AntispamUpdateServiceEventLogConstants
	{
		// Token: 0x04000018 RID: 24
		public const string EventSource = "MSExchange Anti-spam Update";

		// Token: 0x04000019 RID: 25
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStartSuccess = new ExEventLog.EventTuple(263144U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001A RID: 26
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStopSuccess = new ExEventLog.EventTuple(263145U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400001B RID: 27
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStartFailure = new ExEventLog.EventTuple(3221488618U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400001C RID: 28
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStopFailure = new ExEventLog.EventTuple(3221488619U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400001D RID: 29
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_StartScan = new ExEventLog.EventTuple(1074004972U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400001E RID: 30
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_StopScan = new ExEventLog.EventTuple(1074004973U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400001F RID: 31
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_StartDownload = new ExEventLog.EventTuple(1074004975U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000020 RID: 32
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_StopDownload = new ExEventLog.EventTuple(1074004976U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000021 RID: 33
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DownloadFailure = new ExEventLog.EventTuple(3221488625U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000022 RID: 34
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_StartInstall = new ExEventLog.EventTuple(1074004978U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000023 RID: 35
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_StopInstall = new ExEventLog.EventTuple(1074004979U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000024 RID: 36
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InstallFailure = new ExEventLog.EventTuple(3221488628U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000025 RID: 37
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PatchAvailable = new ExEventLog.EventTuple(1074004981U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000026 RID: 38
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MuOpt = new ExEventLog.EventTuple(3221488631U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000027 RID: 39
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MuOptFail = new ExEventLog.EventTuple(3221488632U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000028 RID: 40
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WuaFail = new ExEventLog.EventTuple(3221488633U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000029 RID: 41
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MuGetFail = new ExEventLog.EventTuple(3221488634U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x02000007 RID: 7
		private enum Category : short
		{
			// Token: 0x0400002B RID: 43
			Update_Service = 1
		}

		// Token: 0x02000008 RID: 8
		internal enum Message : uint
		{
			// Token: 0x0400002D RID: 45
			ServiceStartSuccess = 263144U,
			// Token: 0x0400002E RID: 46
			ServiceStopSuccess,
			// Token: 0x0400002F RID: 47
			ServiceStartFailure = 3221488618U,
			// Token: 0x04000030 RID: 48
			ServiceStopFailure,
			// Token: 0x04000031 RID: 49
			StartScan = 1074004972U,
			// Token: 0x04000032 RID: 50
			StopScan,
			// Token: 0x04000033 RID: 51
			StartDownload = 1074004975U,
			// Token: 0x04000034 RID: 52
			StopDownload,
			// Token: 0x04000035 RID: 53
			DownloadFailure = 3221488625U,
			// Token: 0x04000036 RID: 54
			StartInstall = 1074004978U,
			// Token: 0x04000037 RID: 55
			StopInstall,
			// Token: 0x04000038 RID: 56
			InstallFailure = 3221488628U,
			// Token: 0x04000039 RID: 57
			PatchAvailable = 1074004981U,
			// Token: 0x0400003A RID: 58
			MuOpt = 3221488631U,
			// Token: 0x0400003B RID: 59
			MuOptFail,
			// Token: 0x0400003C RID: 60
			WuaFail,
			// Token: 0x0400003D RID: 61
			MuGetFail
		}
	}
}
