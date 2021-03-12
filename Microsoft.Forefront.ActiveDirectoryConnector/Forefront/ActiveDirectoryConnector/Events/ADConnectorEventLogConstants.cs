using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Forefront.ActiveDirectoryConnector.Events
{
	// Token: 0x0200000B RID: 11
	public static class ADConnectorEventLogConstants
	{
		// Token: 0x04000017 RID: 23
		public const string EventSource = "Filtering ADConnector";

		// Token: 0x04000018 RID: 24
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ADFilteringSettingWatcherStarted = new ExEventLog.EventTuple(1073743824U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000019 RID: 25
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ADFilteringSettingWatcherStartException = new ExEventLog.EventTuple(3221227473U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001A RID: 26
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ADFilteringSettingWatcherStopped = new ExEventLog.EventTuple(1073743834U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001B RID: 27
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ADFilteringSettingWatcherStopException = new ExEventLog.EventTuple(3221227483U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001C RID: 28
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ADFilteringSettingWatcherProcessId = new ExEventLog.EventTuple(1073743844U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001D RID: 29
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ADFilteringSettingWatcherGetProcessIdException = new ExEventLog.EventTuple(3221227493U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001E RID: 30
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ADFilteringSettingWatcherChangeHandlersRegistered = new ExEventLog.EventTuple(1073743854U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001F RID: 31
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ADFilteringSettingWatcherRegisterConfigurationChangeHandlersReadServerConfigFailed = new ExEventLog.EventTuple(3221227503U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000020 RID: 32
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ADFilteringSettingWatcherChangeHandlersUnRegistered = new ExEventLog.EventTuple(1073743864U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000021 RID: 33
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ADFilteringSettingWatcherServerConfigUpdateNotification = new ExEventLog.EventTuple(1073743874U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000022 RID: 34
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ADFilteringSettingWatcherServerConfigUpdateNoChanges = new ExEventLog.EventTuple(1073743875U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000023 RID: 35
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ADFilteringSettingWatcherServerConfigUpdateException = new ExEventLog.EventTuple(3221227524U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000024 RID: 36
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ADFilteringSettingWatcherReadServerConfigFailed = new ExEventLog.EventTuple(3221227532U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000025 RID: 37
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ADFilteringSettingWatcherGetFilteringSettingsFromServerConfigException = new ExEventLog.EventTuple(3221227533U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000026 RID: 38
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ADFilteringSettingWatcherSetFilteringSettingsToFips = new ExEventLog.EventTuple(1073743894U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000027 RID: 39
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ADFilteringSettingWatcherServerConfigUpdateErrorAddingSnapin = new ExEventLog.EventTuple(3221227543U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000028 RID: 40
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ADFilteringSettingWatcherServerConfigUpdateErrorSettingFilteringServiceSettings = new ExEventLog.EventTuple(3221227544U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0200000C RID: 12
		private enum Category : short
		{
			// Token: 0x0400002A RID: 42
			General = 1
		}

		// Token: 0x0200000D RID: 13
		internal enum Message : uint
		{
			// Token: 0x0400002C RID: 44
			ADFilteringSettingWatcherStarted = 1073743824U,
			// Token: 0x0400002D RID: 45
			ADFilteringSettingWatcherStartException = 3221227473U,
			// Token: 0x0400002E RID: 46
			ADFilteringSettingWatcherStopped = 1073743834U,
			// Token: 0x0400002F RID: 47
			ADFilteringSettingWatcherStopException = 3221227483U,
			// Token: 0x04000030 RID: 48
			ADFilteringSettingWatcherProcessId = 1073743844U,
			// Token: 0x04000031 RID: 49
			ADFilteringSettingWatcherGetProcessIdException = 3221227493U,
			// Token: 0x04000032 RID: 50
			ADFilteringSettingWatcherChangeHandlersRegistered = 1073743854U,
			// Token: 0x04000033 RID: 51
			ADFilteringSettingWatcherRegisterConfigurationChangeHandlersReadServerConfigFailed = 3221227503U,
			// Token: 0x04000034 RID: 52
			ADFilteringSettingWatcherChangeHandlersUnRegistered = 1073743864U,
			// Token: 0x04000035 RID: 53
			ADFilteringSettingWatcherServerConfigUpdateNotification = 1073743874U,
			// Token: 0x04000036 RID: 54
			ADFilteringSettingWatcherServerConfigUpdateNoChanges,
			// Token: 0x04000037 RID: 55
			ADFilteringSettingWatcherServerConfigUpdateException = 3221227524U,
			// Token: 0x04000038 RID: 56
			ADFilteringSettingWatcherReadServerConfigFailed = 3221227532U,
			// Token: 0x04000039 RID: 57
			ADFilteringSettingWatcherGetFilteringSettingsFromServerConfigException,
			// Token: 0x0400003A RID: 58
			ADFilteringSettingWatcherSetFilteringSettingsToFips = 1073743894U,
			// Token: 0x0400003B RID: 59
			ADFilteringSettingWatcherServerConfigUpdateErrorAddingSnapin = 3221227543U,
			// Token: 0x0400003C RID: 60
			ADFilteringSettingWatcherServerConfigUpdateErrorSettingFilteringServiceSettings
		}
	}
}
