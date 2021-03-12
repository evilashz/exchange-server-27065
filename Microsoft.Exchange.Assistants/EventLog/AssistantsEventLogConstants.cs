using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Assistants.EventLog
{
	// Token: 0x02000002 RID: 2
	public static class AssistantsEventLogConstants
	{
		// Token: 0x04000001 RID: 1
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseManagerStartedPrivateDatabase = new ExEventLog.EventTuple(271145U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000002 RID: 2
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseManagerStoppedDatabase = new ExEventLog.EventTuple(271146U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000003 RID: 3
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AssistantFailedToProcessEvent = new ExEventLog.EventTuple(2147754795U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000004 RID: 4
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TimeBasedAssistantFailed = new ExEventLog.EventTuple(2147754796U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000005 RID: 5
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AssistantSkippingEvent = new ExEventLog.EventTuple(2147754798U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000006 RID: 6
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxSessionException = new ExEventLog.EventTuple(2147754801U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000007 RID: 7
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseManagerTransientFailure = new ExEventLog.EventTuple(2147754802U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000008 RID: 8
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PoisonEvent = new ExEventLog.EventTuple(3221496628U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000009 RID: 9
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CrashEvent = new ExEventLog.EventTuple(3221496629U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000A RID: 10
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MissingSystemMailbox = new ExEventLog.EventTuple(2147754807U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400000B RID: 11
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DeadMailbox = new ExEventLog.EventTuple(2147754808U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400000C RID: 12
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TimeWindowBegin = new ExEventLog.EventTuple(271161U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000D RID: 13
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TimeWindowEnd = new ExEventLog.EventTuple(271162U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000E RID: 14
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TimeBasedAssistantStartFailed = new ExEventLog.EventTuple(2147754812U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000F RID: 15
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TimeDemandJobBegin = new ExEventLog.EventTuple(271165U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000010 RID: 16
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TimeDemandJobEnd = new ExEventLog.EventTuple(271166U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000011 RID: 17
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TimeWindowBeginError = new ExEventLog.EventTuple(2147754815U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000012 RID: 18
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseManagerStartedPublicDatabase = new ExEventLog.EventTuple(271168U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000013 RID: 19
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SkippedMailboxes = new ExEventLog.EventTuple(271169U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000014 RID: 20
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OnDemandStartError = new ExEventLog.EventTuple(2147754818U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000015 RID: 21
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PoisonMailbox = new ExEventLog.EventTuple(3221496643U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000016 RID: 22
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CrashMailbox = new ExEventLog.EventTuple(3221496644U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000017 RID: 23
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GovernorFailure = new ExEventLog.EventTuple(2147754821U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000018 RID: 24
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GovernorRecovery = new ExEventLog.EventTuple(271174U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000019 RID: 25
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GovernorGiveUp = new ExEventLog.EventTuple(2147754823U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001A RID: 26
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GovernorRetry = new ExEventLog.EventTuple(271176U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001B RID: 27
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TimeDemandEmptyJob = new ExEventLog.EventTuple(2147754825U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001C RID: 28
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TimeHalt = new ExEventLog.EventTuple(271178U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001D RID: 29
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RpcError = new ExEventLog.EventTuple(3221496653U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001E RID: 30
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseGovernorFailure = new ExEventLog.EventTuple(2147754830U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001F RID: 31
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServerGovernorFailure = new ExEventLog.EventTuple(2147754831U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000020 RID: 32
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GrayException = new ExEventLog.EventTuple(1074013008U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000021 RID: 33
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GenericException = new ExEventLog.EventTuple(2147754833U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000022 RID: 34
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseNotProcessedInTimeWindow = new ExEventLog.EventTuple(2147754834U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000023 RID: 35
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkCycleCheckpointError = new ExEventLog.EventTuple(2147754835U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000024 RID: 36
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MaximumConcurrentThreads = new ExEventLog.EventTuple(2147754836U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000025 RID: 37
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseUnhealthy = new ExEventLog.EventTuple(2147754837U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000026 RID: 38
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ShutdownAssistantsThreadHanging = new ExEventLog.EventTuple(2147754901U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000027 RID: 39
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ShutdownAssistantsThreadHangPersisted = new ExEventLog.EventTuple(2147754902U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000028 RID: 40
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseStatusThreadResumed = new ExEventLog.EventTuple(2147754903U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000029 RID: 41
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxesWithDecayedWatermarks = new ExEventLog.EventTuple(271256U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400002A RID: 42
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RetryAssistantFailedToStart = new ExEventLog.EventTuple(2147754905U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400002B RID: 43
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AssistantFailedToStart = new ExEventLog.EventTuple(2147754906U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x02000003 RID: 3
		private enum Category : short
		{
			// Token: 0x0400002D RID: 45
			Assistants = 1
		}

		// Token: 0x02000004 RID: 4
		internal enum Message : uint
		{
			// Token: 0x0400002F RID: 47
			DatabaseManagerStartedPrivateDatabase = 271145U,
			// Token: 0x04000030 RID: 48
			DatabaseManagerStoppedDatabase,
			// Token: 0x04000031 RID: 49
			AssistantFailedToProcessEvent = 2147754795U,
			// Token: 0x04000032 RID: 50
			TimeBasedAssistantFailed,
			// Token: 0x04000033 RID: 51
			AssistantSkippingEvent = 2147754798U,
			// Token: 0x04000034 RID: 52
			MailboxSessionException = 2147754801U,
			// Token: 0x04000035 RID: 53
			DatabaseManagerTransientFailure,
			// Token: 0x04000036 RID: 54
			PoisonEvent = 3221496628U,
			// Token: 0x04000037 RID: 55
			CrashEvent,
			// Token: 0x04000038 RID: 56
			MissingSystemMailbox = 2147754807U,
			// Token: 0x04000039 RID: 57
			DeadMailbox,
			// Token: 0x0400003A RID: 58
			TimeWindowBegin = 271161U,
			// Token: 0x0400003B RID: 59
			TimeWindowEnd,
			// Token: 0x0400003C RID: 60
			TimeBasedAssistantStartFailed = 2147754812U,
			// Token: 0x0400003D RID: 61
			TimeDemandJobBegin = 271165U,
			// Token: 0x0400003E RID: 62
			TimeDemandJobEnd,
			// Token: 0x0400003F RID: 63
			TimeWindowBeginError = 2147754815U,
			// Token: 0x04000040 RID: 64
			DatabaseManagerStartedPublicDatabase = 271168U,
			// Token: 0x04000041 RID: 65
			SkippedMailboxes,
			// Token: 0x04000042 RID: 66
			OnDemandStartError = 2147754818U,
			// Token: 0x04000043 RID: 67
			PoisonMailbox = 3221496643U,
			// Token: 0x04000044 RID: 68
			CrashMailbox,
			// Token: 0x04000045 RID: 69
			GovernorFailure = 2147754821U,
			// Token: 0x04000046 RID: 70
			GovernorRecovery = 271174U,
			// Token: 0x04000047 RID: 71
			GovernorGiveUp = 2147754823U,
			// Token: 0x04000048 RID: 72
			GovernorRetry = 271176U,
			// Token: 0x04000049 RID: 73
			TimeDemandEmptyJob = 2147754825U,
			// Token: 0x0400004A RID: 74
			TimeHalt = 271178U,
			// Token: 0x0400004B RID: 75
			RpcError = 3221496653U,
			// Token: 0x0400004C RID: 76
			DatabaseGovernorFailure = 2147754830U,
			// Token: 0x0400004D RID: 77
			ServerGovernorFailure,
			// Token: 0x0400004E RID: 78
			GrayException = 1074013008U,
			// Token: 0x0400004F RID: 79
			GenericException = 2147754833U,
			// Token: 0x04000050 RID: 80
			DatabaseNotProcessedInTimeWindow,
			// Token: 0x04000051 RID: 81
			WorkCycleCheckpointError,
			// Token: 0x04000052 RID: 82
			MaximumConcurrentThreads,
			// Token: 0x04000053 RID: 83
			DatabaseUnhealthy,
			// Token: 0x04000054 RID: 84
			ShutdownAssistantsThreadHanging = 2147754901U,
			// Token: 0x04000055 RID: 85
			ShutdownAssistantsThreadHangPersisted,
			// Token: 0x04000056 RID: 86
			DatabaseStatusThreadResumed,
			// Token: 0x04000057 RID: 87
			MailboxesWithDecayedWatermarks = 271256U,
			// Token: 0x04000058 RID: 88
			RetryAssistantFailedToStart = 2147754905U,
			// Token: 0x04000059 RID: 89
			AssistantFailedToStart
		}
	}
}
