using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Diagnostics.Service.Common
{
	// Token: 0x02000003 RID: 3
	public static class MSExchangeDiagnosticsEventLogConstants
	{
		// Token: 0x0400000F RID: 15
		public const string EventSource = "MSExchangeDiagnostics";

		// Token: 0x04000010 RID: 16
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigurationExceptionOnStartup = new ExEventLog.EventTuple(3221488617U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000011 RID: 17
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DirectoryCreationException = new ExEventLog.EventTuple(3221488618U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000012 RID: 18
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PerformanceCounterWarningTriggerEvent = new ExEventLog.EventTuple(2147746797U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000013 RID: 19
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PerformanceCounterErrorTriggerEvent = new ExEventLog.EventTuple(3221488622U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000014 RID: 20
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PerformanceCounterInformationTriggerEvent = new ExEventLog.EventTuple(1074005021U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000015 RID: 21
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PerformanceLogError = new ExEventLog.EventTuple(3221488623U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000016 RID: 22
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStarting = new ExEventLog.EventTuple(1074004976U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000017 RID: 23
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStarted = new ExEventLog.EventTuple(1074004992U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000018 RID: 24
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStopping = new ExEventLog.EventTuple(1074004977U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000019 RID: 25
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStopped = new ExEventLog.EventTuple(1074004993U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001A RID: 26
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RetentionAgentUnhandledException = new ExEventLog.EventTuple(3221488627U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400001B RID: 27
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RetentionAgentDataLossOccurred = new ExEventLog.EventTuple(3221488628U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001C RID: 28
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RetentionAgentPotentialDataLossWarning = new ExEventLog.EventTuple(2147746805U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001D RID: 29
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SqlOutputStreamUnhandledException = new ExEventLog.EventTuple(3221488630U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400001E RID: 30
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ActiveDirectoryUnavailable = new ExEventLog.EventTuple(3221488631U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001F RID: 31
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SqlOutputStreamConnectionStringFromAdNotFound = new ExEventLog.EventTuple(3221488632U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000020 RID: 32
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SqlOutputStreamConsecutiveRetriesForASpecifiedTime = new ExEventLog.EventTuple(3221488633U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000021 RID: 33
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SqlOutputStreamDecryptFailed = new ExEventLog.EventTuple(3221488634U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000022 RID: 34
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BadIisLogHeader = new ExEventLog.EventTuple(3221488636U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000023 RID: 35
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_IisLogLineNotProcessed = new ExEventLog.EventTuple(3221488637U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000024 RID: 36
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OwaServiceUnavailable = new ExEventLog.EventTuple(3221488638U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000025 RID: 37
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RelogManagerUnhandledException = new ExEventLog.EventTuple(3221488639U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000026 RID: 38
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OwaHttpStatus400 = new ExEventLog.EventTuple(3221488642U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000027 RID: 39
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OwaHttpStatus440 = new ExEventLog.EventTuple(3221488643U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000028 RID: 40
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OwaHttpStatus500 = new ExEventLog.EventTuple(3221488644U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000029 RID: 41
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OwaHttpStatusOther = new ExEventLog.EventTuple(3221488645U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400002A RID: 42
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JobManagerStarted = new ExEventLog.EventTuple(1074004998U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400002B RID: 43
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JobManagerNotStarted = new ExEventLog.EventTuple(1074004999U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400002C RID: 44
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JobManagerStartupFailures = new ExEventLog.EventTuple(3221488648U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400002D RID: 45
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JobCrashed = new ExEventLog.EventTuple(3221488649U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400002E RID: 46
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JobPoisoned = new ExEventLog.EventTuple(3221488650U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400002F RID: 47
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BingServicesLatencyAboveThreshold = new ExEventLog.EventTuple(3221488651U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000030 RID: 48
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BingServicesLatencyBelowThreshold = new ExEventLog.EventTuple(1074005004U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000031 RID: 49
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FindPlacesFailureAboveThreshold = new ExEventLog.EventTuple(3221488653U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000032 RID: 50
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FindPlacesFailureBelowThreshold = new ExEventLog.EventTuple(1074005006U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000033 RID: 51
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BitlockerStateDetectionError = new ExEventLog.EventTuple(3221488655U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000034 RID: 52
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BitlockerState = new ExEventLog.EventTuple(1074005008U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000035 RID: 53
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ConnectionStringManagerPartitionInvalid = new ExEventLog.EventTuple(3221488657U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000036 RID: 54
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ConnectionStringManagerUnableToConnect = new ExEventLog.EventTuple(3221488658U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000037 RID: 55
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AnalyzerNotAdded = new ExEventLog.EventTuple(1074005011U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000038 RID: 56
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WatsonCrashAlert = new ExEventLog.EventTuple(3221488660U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000039 RID: 57
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WatsonExceptionCrashAlert = new ExEventLog.EventTuple(3221488670U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400003A RID: 58
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OwaLyncFailureAboveThreshold = new ExEventLog.EventTuple(3221488664U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400003B RID: 59
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OwaLyncFailureBelowThreshold = new ExEventLog.EventTuple(1074005017U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400003C RID: 60
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OwaTooManyExceptionsEncountered = new ExEventLog.EventTuple(3221488717U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400003D RID: 61
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TooManyObjectsOpenedException = new ExEventLog.EventTuple(3221488718U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400003E RID: 62
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OwaLogonFailures = new ExEventLog.EventTuple(3221488719U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400003F RID: 63
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OwaHighLatencyLoadStartPage = new ExEventLog.EventTuple(3221488720U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000040 RID: 64
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OwaTooManyHttpErrorResponsesEncountered = new ExEventLog.EventTuple(3221488721U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000041 RID: 65
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OwaTooManyHttpErrorResponsesResolved = new ExEventLog.EventTuple(1074005074U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000042 RID: 66
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OabTooManyHttpErrorResponsesEncountered = new ExEventLog.EventTuple(3221489167U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000043 RID: 67
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OabTooManyHttpErrorResponsesResolved = new ExEventLog.EventTuple(1074005520U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000044 RID: 68
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OabTooManyExceptionsEncountered = new ExEventLog.EventTuple(3221489169U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000045 RID: 69
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OabFileLoadException = new ExEventLog.EventTuple(3221489170U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000046 RID: 70
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_IisAppPoolHttpErrorsDefaultEvent = new ExEventLog.EventTuple(3221488723U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000047 RID: 71
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_IisAppPoolTooManyHttpErrorResponsesEncounteredDefaultEvent = new ExEventLog.EventTuple(3221488724U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000048 RID: 72
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_IisAppPoolTooManyHttpErrorResponsesResolvedDefaultEvent = new ExEventLog.EventTuple(1074005077U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000049 RID: 73
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OwaStartPageFailures = new ExEventLog.EventTuple(3221488726U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004A RID: 74
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OwaStartPageFailuresResolved = new ExEventLog.EventTuple(1074005079U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004B RID: 75
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OwaActiveDatabaseAvailabilityBelowThreshold = new ExEventLog.EventTuple(3221488728U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004C RID: 76
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OwaActiveDatabaseAvailabilityBelowThresholdResolved = new ExEventLog.EventTuple(1074005081U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004D RID: 77
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OwaLogoffFailures = new ExEventLog.EventTuple(3221488730U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004E RID: 78
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OwaLogoffFailuresResolved = new ExEventLog.EventTuple(1074005083U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004F RID: 79
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TestEvent = new ExEventLog.EventTuple(1074003968U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000050 RID: 80
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RbaRequestsWithExceptionsReachedThreshold = new ExEventLog.EventTuple(3221488661U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000051 RID: 81
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RbaAtLeastOneExceptionReachedThreshold = new ExEventLog.EventTuple(3221488662U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000052 RID: 82
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RbaAllIsWell = new ExEventLog.EventTuple(1074005015U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000053 RID: 83
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CalSyncRequestsWithExceptionsReachedThreshold = new ExEventLog.EventTuple(3221488666U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000054 RID: 84
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CalSyncRequestsWithSyncFailuresReachedThreshold = new ExEventLog.EventTuple(3221488667U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000055 RID: 85
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CalSyncAllIsWell = new ExEventLog.EventTuple(1074005020U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000056 RID: 86
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AvailabilityServiceExceptionAboveThreshold = new ExEventLog.EventTuple(3221489272U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000057 RID: 87
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TxSyncPopRequestsWithExceptionsReachedThreshold = new ExEventLog.EventTuple(3221488676U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000058 RID: 88
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TxSyncIMAPRequestsWithExceptionsReachedThreshold = new ExEventLog.EventTuple(3221488677U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000059 RID: 89
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TxSyncDeltaSyncMailRequestsWithExceptionsReachedThreshold = new ExEventLog.EventTuple(3221488678U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400005A RID: 90
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TxSyncFacebookRequestsWithExceptionsReachedThreshold = new ExEventLog.EventTuple(3221488679U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400005B RID: 91
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TxSyncLinkedInRequestsWithExceptionsReachedThreshold = new ExEventLog.EventTuple(3221488680U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400005C RID: 92
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TxSyncPopIsWell = new ExEventLog.EventTuple(1074005033U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400005D RID: 93
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TxSyncIMAPIsWell = new ExEventLog.EventTuple(1074005034U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400005E RID: 94
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TxSyncDeltaSyncMailIsWell = new ExEventLog.EventTuple(1074005035U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400005F RID: 95
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TxSyncFacebookIsWell = new ExEventLog.EventTuple(1074005036U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000060 RID: 96
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TxSyncLinkedInIsWell = new ExEventLog.EventTuple(1074005037U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000061 RID: 97
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PowerShellTimeoutAuthzRequestAboveThreshold = new ExEventLog.EventTuple(3221489717U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000062 RID: 98
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PowerShellLiveIDTimeoutAuthzRequestAboveThreshold = new ExEventLog.EventTuple(3221489718U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000063 RID: 99
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PowerShellLegacyTimeoutAuthzRequestAboveThreshold = new ExEventLog.EventTuple(3221489719U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000064 RID: 100
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PowerShellUnhandledCmdletExceptionAboveThreshold = new ExEventLog.EventTuple(3221489720U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000065 RID: 101
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PowerShellLiveIDUnhandledCmdletExceptionAboveThreshold = new ExEventLog.EventTuple(3221489721U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000066 RID: 102
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PowerShellLegacyUnhandledCmdletExceptionAboveThreshold = new ExEventLog.EventTuple(3221489722U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000067 RID: 103
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PowerShellLongLatencyCmdletAboveThreshold = new ExEventLog.EventTuple(3221489723U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000068 RID: 104
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PowerShellLiveIDLongLatencyCmdletAboveThreshold = new ExEventLog.EventTuple(3221489724U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000069 RID: 105
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PowerShellLegacyLongLatencyCmdletAboveThreshold = new ExEventLog.EventTuple(3221489725U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006A RID: 106
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PowerShellAuthzErrorAboveThreshold = new ExEventLog.EventTuple(3221489726U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006B RID: 107
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PowerShellLiveIDAuthzErrorAboveThreshold = new ExEventLog.EventTuple(3221489727U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006C RID: 108
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PowerShellLegacyAuthzErrorAboveThreshold = new ExEventLog.EventTuple(3221489728U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006D RID: 109
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PSWSLongLatencyCmdletAboveThreshold = new ExEventLog.EventTuple(3221489729U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006E RID: 110
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PSWSUnhandledCmdletExceptionAboveThreshold = new ExEventLog.EventTuple(3221489730U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006F RID: 111
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PowerShellHttpGenericErrorAboveThreshold = new ExEventLog.EventTuple(3221489731U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000070 RID: 112
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PowerShellLiveIDHttpGenericErrorAboveThreshold = new ExEventLog.EventTuple(3221489732U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000071 RID: 113
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PowerShellLegacyHttpGenericErrorAboveThreshold = new ExEventLog.EventTuple(3221489733U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000072 RID: 114
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PowerShellHttpErrorResponseAboveThreshold = new ExEventLog.EventTuple(3221489734U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000073 RID: 115
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PowerShellLiveIDHttpErrorResponseAboveThreshold = new ExEventLog.EventTuple(3221489735U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000074 RID: 116
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PowerShellLegacyHttpErrorResponseAboveThreshold = new ExEventLog.EventTuple(3221489736U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000075 RID: 117
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TestApi1_TestCan1_EX = new ExEventLog.EventTuple(3221488686U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000076 RID: 118
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TestApi1_TestCan1_OK = new ExEventLog.EventTuple(1074005039U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000077 RID: 119
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TestApi2_TestCan2_EX = new ExEventLog.EventTuple(3221488688U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000078 RID: 120
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TestApi2_TestCan2_OK = new ExEventLog.EventTuple(1074005041U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000079 RID: 121
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FindPeople_BrowseInMailbox_EX = new ExEventLog.EventTuple(3221488690U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400007A RID: 122
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FindPeople_BrowseInMailbox_OK = new ExEventLog.EventTuple(1074005043U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400007B RID: 123
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FindPeople_HoverCard_EX = new ExEventLog.EventTuple(3221488692U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400007C RID: 124
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FindPeople_HoverCard_OK = new ExEventLog.EventTuple(1074005045U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400007D RID: 125
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FindPeople_RecipientCache_EX = new ExEventLog.EventTuple(3221488694U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400007E RID: 126
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FindPeople_RecipientCache_OK = new ExEventLog.EventTuple(1074005047U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400007F RID: 127
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TestApi4__EX = new ExEventLog.EventTuple(3221488696U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000080 RID: 128
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TestApi4__OK = new ExEventLog.EventTuple(1074005049U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000081 RID: 129
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CreateItem_CreateMessageForComposeSend_EX = new ExEventLog.EventTuple(3221489316U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000082 RID: 130
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CreateItem_CreateMessageForComposeSend_OK = new ExEventLog.EventTuple(1074005669U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000083 RID: 131
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CreateItem_MailComposeUpgrade_EX = new ExEventLog.EventTuple(3221489318U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000084 RID: 132
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CreateItem_MailComposeUpgrade_OK = new ExEventLog.EventTuple(1074005671U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000085 RID: 133
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CreateItem_CreateResponseSend_EX = new ExEventLog.EventTuple(3221489320U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000086 RID: 134
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CreateItem_CreateResponseSend_OK = new ExEventLog.EventTuple(1074005673U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000087 RID: 135
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UpdateItem_UpdateMessageForComposeSend_EX = new ExEventLog.EventTuple(3221489322U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000088 RID: 136
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UpdateItem_UpdateMessageForComposeSend_OK = new ExEventLog.EventTuple(1074005675U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000089 RID: 137
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CreateItem_CreateMessageForCompose_EX = new ExEventLog.EventTuple(3221489624U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400008A RID: 138
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CreateItem_CreateMessageForCompose_OK = new ExEventLog.EventTuple(1074005977U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400008B RID: 139
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CreateItem_CreateResponse_EX = new ExEventLog.EventTuple(3221489626U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400008C RID: 140
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CreateItem_CreateResponse_OK = new ExEventLog.EventTuple(1074005979U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400008D RID: 141
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UpdateItem_UpdateMessageForCompose_EX = new ExEventLog.EventTuple(3221489628U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400008E RID: 142
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UpdateItem_UpdateMessageForCompose_OK = new ExEventLog.EventTuple(1074005981U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400008F RID: 143
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GetWacAttachmentInfo__EX = new ExEventLog.EventTuple(3221488817U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000090 RID: 144
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GetWacAttachmentInfo__OK = new ExEventLog.EventTuple(1074005170U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000091 RID: 145
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WACMdbCacheUpdate__EX = new ExEventLog.EventTuple(3221488819U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000092 RID: 146
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WACMdbCacheUpdate__OK = new ExEventLog.EventTuple(1074005172U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000093 RID: 147
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WACCheckFile__EX = new ExEventLog.EventTuple(3221488821U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000094 RID: 148
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WACCheckFile__OK = new ExEventLog.EventTuple(1074005174U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000095 RID: 149
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WACGetFile__EX = new ExEventLog.EventTuple(3221488823U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000096 RID: 150
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WACGetFile__OK = new ExEventLog.EventTuple(1074005176U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000097 RID: 151
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WACCleanCobaltStore__EX = new ExEventLog.EventTuple(3221488825U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000098 RID: 152
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WACCleanCobaltStore__OK = new ExEventLog.EventTuple(1074005178U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000099 RID: 153
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WACPutFile__EX = new ExEventLog.EventTuple(3221488827U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400009A RID: 154
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WACPutFile__OK = new ExEventLog.EventTuple(1074005180U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400009B RID: 155
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WACCobalt__EX = new ExEventLog.EventTuple(3221488829U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400009C RID: 156
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WACCobalt__OK = new ExEventLog.EventTuple(1074005182U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400009D RID: 157
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WACAutoSave__EX = new ExEventLog.EventTuple(3221488831U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400009E RID: 158
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WACAutoSave__OK = new ExEventLog.EventTuple(1074005184U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400009F RID: 159
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GetWacIFrameUrl__EX = new ExEventLog.EventTuple(3221488833U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000A0 RID: 160
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GetWacIFrameUrl__OK = new ExEventLog.EventTuple(1074005186U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000A1 RID: 161
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WACBadRequest__EX = new ExEventLog.EventTuple(3221488835U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000A2 RID: 162
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WACBadRequest__OK = new ExEventLog.EventTuple(1074005188U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000A3 RID: 163
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WACLock__EX = new ExEventLog.EventTuple(3221488837U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000A4 RID: 164
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WACLock__OK = new ExEventLog.EventTuple(1074005190U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000A5 RID: 165
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WACUnlock__EX = new ExEventLog.EventTuple(3221488839U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000A6 RID: 166
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WACUnlock__OK = new ExEventLog.EventTuple(1074005192U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000A7 RID: 167
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WACCacheEntryExpired__EX = new ExEventLog.EventTuple(3221488841U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000A8 RID: 168
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WACCacheEntryExpired__OK = new ExEventLog.EventTuple(1074005194U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000A9 RID: 169
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WACRefreshLock__EX = new ExEventLog.EventTuple(3221488843U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000AA RID: 170
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WACRefreshLock__OK = new ExEventLog.EventTuple(1074005196U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000AB RID: 171
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CreateAttachment__EX = new ExEventLog.EventTuple(3221488845U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000AC RID: 172
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CreateAttachment__OK = new ExEventLog.EventTuple(1074005198U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000AD RID: 173
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CreateAttachmentFromAttachmentDataProvider__EX = new ExEventLog.EventTuple(3221488847U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000AE RID: 174
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CreateAttachmentFromAttachmentDataProvider__OK = new ExEventLog.EventTuple(1074005200U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000AF RID: 175
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CreateReferenceAttachmentFromLocalFile__EX = new ExEventLog.EventTuple(3221488849U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000B0 RID: 176
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CreateReferenceAttachmentFromLocalFile__OK = new ExEventLog.EventTuple(1074005202U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000B1 RID: 177
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CreateReferenceAttachmentFromAttachmentDataProvider__EX = new ExEventLog.EventTuple(3221488851U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000B2 RID: 178
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CreateReferenceAttachmentFromAttachmentDataProvider__OK = new ExEventLog.EventTuple(1074005204U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000B3 RID: 179
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GetAttachmentDataProviders__EX = new ExEventLog.EventTuple(3221488853U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000B4 RID: 180
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GetAttachmentDataProviders__OK = new ExEventLog.EventTuple(1074005206U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000B5 RID: 181
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GetAttachmentDataProviderItems__EX = new ExEventLog.EventTuple(3221488855U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000B6 RID: 182
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GetAttachmentDataProviderItems__OK = new ExEventLog.EventTuple(1074005208U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000B7 RID: 183
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GetAttachmentDataProviderRecentItems__EX = new ExEventLog.EventTuple(3221488857U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000B8 RID: 184
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GetAttachmentDataProviderRecentItems__OK = new ExEventLog.EventTuple(1074005210U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000B9 RID: 185
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SynchronizeWacAttachment__EX = new ExEventLog.EventTuple(3221488859U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000BA RID: 186
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SynchronizeWacAttachment__OK = new ExEventLog.EventTuple(1074005212U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000BB RID: 187
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GetFileAttachment__EX = new ExEventLog.EventTuple(3221488861U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000BC RID: 188
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GetFileAttachment__OK = new ExEventLog.EventTuple(1074005214U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000BD RID: 189
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FindConversation_BrowseAll_EX = new ExEventLog.EventTuple(3221489116U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000BE RID: 190
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FindConversation_BrowseAll_OK = new ExEventLog.EventTuple(1074005469U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000BF RID: 191
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FindConversation_BrowseUnread_EX = new ExEventLog.EventTuple(3221489118U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000C0 RID: 192
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FindConversation_BrowseUnread_OK = new ExEventLog.EventTuple(1074005471U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000C1 RID: 193
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FindConversation_BrowseNoClutterAll_EX = new ExEventLog.EventTuple(3221489126U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000C2 RID: 194
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FindConversation_BrowseNoClutterAll_OK = new ExEventLog.EventTuple(1074005479U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000C3 RID: 195
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FindConversation_BrowseNoClutterUnread_EX = new ExEventLog.EventTuple(3221489128U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000C4 RID: 196
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FindConversation_BrowseNoClutterUnread_OK = new ExEventLog.EventTuple(1074005481U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000C5 RID: 197
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GetConversationItems__EX = new ExEventLog.EventTuple(3221488700U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000C6 RID: 198
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GetConversationItems__OK = new ExEventLog.EventTuple(1074005053U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000C7 RID: 199
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FindItem_BrowseAll_EX = new ExEventLog.EventTuple(3221489146U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000C8 RID: 200
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FindItem_BrowseAll_OK = new ExEventLog.EventTuple(1074005499U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000C9 RID: 201
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FindItem_BrowseUnread_EX = new ExEventLog.EventTuple(3221489148U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000CA RID: 202
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FindItem_BrowseUnread_OK = new ExEventLog.EventTuple(1074005501U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000CB RID: 203
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FindItem_BrowseNoClutterAll_EX = new ExEventLog.EventTuple(3221489156U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000CC RID: 204
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FindItem_BrowseNoClutterAll_OK = new ExEventLog.EventTuple(1074005509U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000CD RID: 205
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FindItem_BrowseNoClutterUnread_EX = new ExEventLog.EventTuple(3221489158U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000CE RID: 206
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FindItem_BrowseNoClutterUnread_OK = new ExEventLog.EventTuple(1074005511U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000CF RID: 207
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GetItem_GetMailItem_EX = new ExEventLog.EventTuple(3221488704U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000D0 RID: 208
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GetItem_GetMailItem_OK = new ExEventLog.EventTuple(1074005057U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000D1 RID: 209
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GetExtensibilityContext_ExtLoadApps_EX = new ExEventLog.EventTuple(3221489017U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000D2 RID: 210
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GetExtensibilityContext_ExtLoadApps_OK = new ExEventLog.EventTuple(1074005370U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000D3 RID: 211
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CreateModernGroup_CreateModernGroupAction_EX = new ExEventLog.EventTuple(3221489019U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000D4 RID: 212
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CreateModernGroup_CreateModernGroupAction_OK = new ExEventLog.EventTuple(1074005372U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000D5 RID: 213
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CreateUnifiedGroup_CreateUnifiedGroupAction_EX = new ExEventLog.EventTuple(3221489041U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000D6 RID: 214
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CreateUnifiedGroup_CreateUnifiedGroupAction_OK = new ExEventLog.EventTuple(1074005394U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000D7 RID: 215
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AddMembersToUnifiedGroup_AddMembersToUnifiedGroupAction_EX = new ExEventLog.EventTuple(3221489043U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000D8 RID: 216
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AddMembersToUnifiedGroup_AddMembersToUnifiedGroupAction_OK = new ExEventLog.EventTuple(1074005396U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000D9 RID: 217
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GetModernGroup_GetModernGroupAction_EX = new ExEventLog.EventTuple(3221489021U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000DA RID: 218
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GetModernGroup_GetModernGroupAction_OK = new ExEventLog.EventTuple(1074005374U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000DB RID: 219
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GetModernGroups_GetModernGroupsAction_EX = new ExEventLog.EventTuple(3221489023U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000DC RID: 220
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GetModernGroups_GetModernGroupsAction_OK = new ExEventLog.EventTuple(1074005376U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000DD RID: 221
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GetModernGroupUnseenItems_GetModernGroupUnseenItemsAction_EX = new ExEventLog.EventTuple(3221489025U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000DE RID: 222
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GetModernGroupUnseenItems_GetModernGroupUnseenItemsAction_OK = new ExEventLog.EventTuple(1074005378U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000DF RID: 223
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PostModernGroupItem_QuickCompose_EX = new ExEventLog.EventTuple(3221489027U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000E0 RID: 224
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PostModernGroupItem_QuickCompose_OK = new ExEventLog.EventTuple(1074005380U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000E1 RID: 225
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PostModernGroupItem_QuickReply_EX = new ExEventLog.EventTuple(3221489045U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000E2 RID: 226
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PostModernGroupItem_QuickReply_OK = new ExEventLog.EventTuple(1074005398U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000E3 RID: 227
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UpdateModernGroup_UpdateModernGroupAction_EX = new ExEventLog.EventTuple(3221489029U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000E4 RID: 228
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UpdateModernGroup_UpdateModernGroupAction_OK = new ExEventLog.EventTuple(1074005382U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000E5 RID: 229
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GetConversationItems_ModernGroup_EX = new ExEventLog.EventTuple(3221489031U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000E6 RID: 230
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GetConversationItems_ModernGroup_OK = new ExEventLog.EventTuple(1074005384U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000E7 RID: 231
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FindConversation_ModernGroupAll_EX = new ExEventLog.EventTuple(3221489033U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000E8 RID: 232
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FindConversation_ModernGroupAll_OK = new ExEventLog.EventTuple(1074005386U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000E9 RID: 233
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FindConversation_SearchModernGroupAll_EX = new ExEventLog.EventTuple(3221489047U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000EA RID: 234
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FindConversation_SearchModernGroupAll_OK = new ExEventLog.EventTuple(1074005400U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000EB RID: 235
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SetModernGroupMembership_JoinModernGroup_EX = new ExEventLog.EventTuple(3221489035U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000EC RID: 236
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SetModernGroupMembership_JoinModernGroup_OK = new ExEventLog.EventTuple(1074005388U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000ED RID: 237
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SetModernGroupMembership_LeaveModernGroup_EX = new ExEventLog.EventTuple(3221489037U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000EE RID: 238
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SetModernGroupMembership_LeaveModernGroup_OK = new ExEventLog.EventTuple(1074005390U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000EF RID: 239
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RemoveModernGroup_RemoveModernGroupAction_EX = new ExEventLog.EventTuple(3221489039U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000F0 RID: 240
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RemoveModernGroup_RemoveModernGroupAction_OK = new ExEventLog.EventTuple(1074005392U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000F1 RID: 241
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GetPersonaPhoto__EX = new ExEventLog.EventTuple(3221489287U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000F2 RID: 242
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GetPersonaPhoto__OK = new ExEventLog.EventTuple(1074005640U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000F3 RID: 243
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UploadPhoto__EX = new ExEventLog.EventTuple(3221489312U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000F4 RID: 244
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UploadPhoto__OK = new ExEventLog.EventTuple(1074005665U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000F5 RID: 245
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UploadPhotoFromForm__EX = new ExEventLog.EventTuple(3221489314U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000F6 RID: 246
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UploadPhotoFromForm__OK = new ExEventLog.EventTuple(1074005667U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000F7 RID: 247
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ProcessSuiteStorage__EX = new ExEventLog.EventTuple(3221489417U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000F8 RID: 248
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ProcessSuiteStorage__OK = new ExEventLog.EventTuple(1074005770U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000F9 RID: 249
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SetNotificationSettings__EX = new ExEventLog.EventTuple(3221489419U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000FA RID: 250
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SetNotificationSettings__OK = new ExEventLog.EventTuple(1074005772U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000FB RID: 251
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TestApi1__EX = new ExEventLog.EventTuple(3221489324U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000FC RID: 252
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TestApi1__OK = new ExEventLog.EventTuple(1074005677U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000FD RID: 253
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TestApi2__EX = new ExEventLog.EventTuple(3221489326U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000FE RID: 254
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TestApi2__OK = new ExEventLog.EventTuple(1074005679U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000FF RID: 255
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GetUserPhoto__EX = new ExEventLog.EventTuple(3221489328U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000100 RID: 256
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GetUserPhoto__OK = new ExEventLog.EventTuple(1074005681U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000101 RID: 257
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FindPeople_PeopleIKnow_EX = new ExEventLog.EventTuple(3221489310U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000102 RID: 258
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FindPeople_PeopleIKnow_OK = new ExEventLog.EventTuple(1074005663U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000103 RID: 259
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FindPeople_BrowseInDirectory_EX = new ExEventLog.EventTuple(3221489517U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000104 RID: 260
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FindPeople_BrowseInDirectory_OK = new ExEventLog.EventTuple(1074005870U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000105 RID: 261
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FindPeople_PeopleModule_EX = new ExEventLog.EventTuple(3221489519U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000106 RID: 262
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FindPeople_PeopleModule_OK = new ExEventLog.EventTuple(1074005872U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000107 RID: 263
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FindPeople_ComposeForms_EX = new ExEventLog.EventTuple(3221489521U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000108 RID: 264
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FindPeople_ComposeForms_OK = new ExEventLog.EventTuple(1074005874U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000109 RID: 265
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GetPersona_PersonaCardreadOnlyRecipientWell_EX = new ExEventLog.EventTuple(3221489367U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400010A RID: 266
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GetPersona_PersonaCardreadOnlyRecipientWell_OK = new ExEventLog.EventTuple(1074005720U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400010B RID: 267
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GetPersona_PersonaCardreadWriteRecipientWell_EX = new ExEventLog.EventTuple(3221489369U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400010C RID: 268
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GetPersona_PersonaCardreadWriteRecipientWell_OK = new ExEventLog.EventTuple(1074005722U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400010D RID: 269
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GetPersona_PersonaCardsharePoint_EX = new ExEventLog.EventTuple(3221489371U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400010E RID: 270
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GetPersona_PersonaCardsharePoint_OK = new ExEventLog.EventTuple(1074005724U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400010F RID: 271
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GetPersona_PersonaCardpeopleHub_EX = new ExEventLog.EventTuple(3221489373U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000110 RID: 272
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GetPersona_PersonaCardpeopleHub_OK = new ExEventLog.EventTuple(1074005726U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000111 RID: 273
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GetPersona_HoverCardreadOnlyRecipientWell_EX = new ExEventLog.EventTuple(3221489377U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000112 RID: 274
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GetPersona_HoverCardreadOnlyRecipientWell_OK = new ExEventLog.EventTuple(1074005730U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000113 RID: 275
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GetPersona_HoverCardreadWriteRecipientWell_EX = new ExEventLog.EventTuple(3221489379U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000114 RID: 276
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GetPersona_HoverCardreadWriteRecipientWell_OK = new ExEventLog.EventTuple(1074005732U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000115 RID: 277
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PerfTraceCTQ_TestCtq1_EX = new ExEventLog.EventTuple(3221488706U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000116 RID: 278
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PerfTraceCTQ_TestCtq1_OK = new ExEventLog.EventTuple(1074005059U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000117 RID: 279
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PerfTraceCTQ_TestCtq2_EX = new ExEventLog.EventTuple(3221488708U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000118 RID: 280
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PerfTraceCTQ_TestCtq2_OK = new ExEventLog.EventTuple(1074005061U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000119 RID: 281
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PerfTraceCTQ_NavigateToPeople_EX = new ExEventLog.EventTuple(3221488710U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400011A RID: 282
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PerfTraceCTQ_NavigateToPeople_OK = new ExEventLog.EventTuple(1074005063U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400011B RID: 283
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PerfTraceCTQ_ShowPersonaCardCollapsed_EX = new ExEventLog.EventTuple(3221489437U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400011C RID: 284
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PerfTraceCTQ_ShowPersonaCardCollapsed_OK = new ExEventLog.EventTuple(1074005790U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400011D RID: 285
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PerfTraceCTQ_ShowPersonaCardExpanded_EX = new ExEventLog.EventTuple(3221489439U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400011E RID: 286
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PerfTraceCTQ_ShowPersonaCardExpanded_OK = new ExEventLog.EventTuple(1074005792U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400011F RID: 287
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PerfTraceCTQ_GetWacAttachmentInfo_EX = new ExEventLog.EventTuple(3221488917U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000120 RID: 288
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PerfTraceCTQ_GetWacAttachmentInfo_OK = new ExEventLog.EventTuple(1074005270U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000121 RID: 289
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PerfTraceCTQ_EditACopy_EX = new ExEventLog.EventTuple(3221488919U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000122 RID: 290
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PerfTraceCTQ_EditACopy_OK = new ExEventLog.EventTuple(1074005272U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000123 RID: 291
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PerfTraceCTQ_DocumentAttachmentPopOut_EX = new ExEventLog.EventTuple(3221488921U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000124 RID: 292
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PerfTraceCTQ_DocumentAttachmentPopOut_OK = new ExEventLog.EventTuple(1074005274U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000125 RID: 293
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PerfTraceCTQ_GetWacIFrameUrl_EX = new ExEventLog.EventTuple(3221488925U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000126 RID: 294
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PerfTraceCTQ_GetWacIFrameUrl_OK = new ExEventLog.EventTuple(1074005278U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000127 RID: 295
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_Ecp_EventLog_HttpUnhandledExceptionReachedThreshold = new ExEventLog.EventTuple(3221488927U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000128 RID: 296
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_Ecp_EventLog_LandingDefaultPageErrorReachedThreshold = new ExEventLog.EventTuple(3221488928U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000129 RID: 297
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SSLCertificateWarningEvent = new ExEventLog.EventTuple(2147746912U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400012A RID: 298
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SSLCertificateErrorEvent = new ExEventLog.EventTuple(3221488737U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400012B RID: 299
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ExtensibilityOmexWsRequestErrorReachedThreshold = new ExEventLog.EventTuple(3221489236U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400012C RID: 300
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OAuthPassiveMonitoringExceptionAboveThreshold = new ExEventLog.EventTuple(3221489286U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400012D RID: 301
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OABGenTenantOutOfSLA = new ExEventLog.EventTuple(3221489166U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400012E RID: 302
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxAuditingFailureAboveThreshold = new ExEventLog.EventTuple(3221490617U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400012F RID: 303
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AdminAuditingFailureAboveThreshold = new ExEventLog.EventTuple(3221490618U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000130 RID: 304
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SynchronousAuditSearchFailureAboveThreshold = new ExEventLog.EventTuple(3221490619U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000131 RID: 305
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AsynchronousAuditSearchFailureAboveThreshold = new ExEventLog.EventTuple(3221490620U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000132 RID: 306
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PFAssistantItemProcessor_EX = new ExEventLog.EventTuple(3221489516U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000133 RID: 307
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EscalateItem_MSExchangeDelivery_EX = new ExEventLog.EventTuple(3221489616U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000134 RID: 308
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_EscalateItem_MSExchangeDelivery_OK = new ExEventLog.EventTuple(1074005969U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000135 RID: 309
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EscalationGetter_MSExchangeDelivery_EX = new ExEventLog.EventTuple(3221489618U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000136 RID: 310
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_EscalationGetter_MSExchangeDelivery_OK = new ExEventLog.EventTuple(1074005971U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000137 RID: 311
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EscalateItem_MSExchangeOWAAppPool_EX = new ExEventLog.EventTuple(3221489620U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000138 RID: 312
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_EscalateItem_MSExchangeOWAAppPool_OK = new ExEventLog.EventTuple(1074005973U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000139 RID: 313
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EscalationGetter_MSExchangeOWAAppPool_EX = new ExEventLog.EventTuple(3221489622U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400013A RID: 314
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_EscalationGetter_MSExchangeOWAAppPool_OK = new ExEventLog.EventTuple(1074005975U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400013B RID: 315
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HxService_WindowsEventLog_EX = new ExEventLog.EventTuple(3221489404U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400013C RID: 316
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_HxService_WindowsEventLog_OK = new ExEventLog.EventTuple(1074005757U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400013D RID: 317
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HxService_AllEventLog_EX = new ExEventLog.EventTuple(3221489376U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400013E RID: 318
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_HxService_AllEventLog_OK = new ExEventLog.EventTuple(1074005729U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400013F RID: 319
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HxService_NoUserEvents_EX = new ExEventLog.EventTuple(3221489378U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000140 RID: 320
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_HxService_NoUserEvents_OK = new ExEventLog.EventTuple(1074005731U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000141 RID: 321
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HxMail_GetConversationPartsCommand_EX = new ExEventLog.EventTuple(3221489330U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000142 RID: 322
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_HxMail_GetConversationPartsCommand_OK = new ExEventLog.EventTuple(1074005683U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000143 RID: 323
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HxMail_BeginSyncCommand_EX = new ExEventLog.EventTuple(3221489356U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000144 RID: 324
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_HxMail_BeginSyncCommand_OK = new ExEventLog.EventTuple(1074005709U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000145 RID: 325
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HxCalendar_GetBasedSyncCommand_EX = new ExEventLog.EventTuple(3221489332U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000146 RID: 326
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_HxCalendar_GetBasedSyncCommand_OK = new ExEventLog.EventTuple(1074005685U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000147 RID: 327
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HxCalendar_CreateItemCommand_EX = new ExEventLog.EventTuple(3221489340U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000148 RID: 328
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_HxCalendar_CreateItemCommand_OK = new ExEventLog.EventTuple(1074005693U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000149 RID: 329
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HxCalendar_CalendarDataCommand_EX = new ExEventLog.EventTuple(3221489342U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400014A RID: 330
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_HxCalendar_CalendarDataCommand_OK = new ExEventLog.EventTuple(1074005695U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400014B RID: 331
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HxCalendar_CancelEventCommand_EX = new ExEventLog.EventTuple(3221489344U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400014C RID: 332
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_HxCalendar_CancelEventCommand_OK = new ExEventLog.EventTuple(1074005697U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400014D RID: 333
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HxCalendar_UpdateItemCommand_EX = new ExEventLog.EventTuple(3221489346U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400014E RID: 334
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_HxCalendar_UpdateItemCommand_OK = new ExEventLog.EventTuple(1074005699U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400014F RID: 335
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HxCalendar_GetFullItemCommand_EX = new ExEventLog.EventTuple(3221489348U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000150 RID: 336
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_HxCalendar_GetFullItemCommand_OK = new ExEventLog.EventTuple(1074005701U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000151 RID: 337
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HxCalendar_SyncCommand_EX = new ExEventLog.EventTuple(3221489350U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000152 RID: 338
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_HxCalendar_SyncCommand_OK = new ExEventLog.EventTuple(1074005703U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000153 RID: 339
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HxService_HttpStatusErrorCode_EX = new ExEventLog.EventTuple(3221489406U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000154 RID: 340
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_HxService_HttpStatusErrorCode_OK = new ExEventLog.EventTuple(1074005759U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000155 RID: 341
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PFAssistantSplitFailed = new ExEventLog.EventTuple(3221489526U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000156 RID: 342
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HxMail_SearchResolveNameCommand_EX = new ExEventLog.EventTuple(3221489352U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000157 RID: 343
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_HxCalendar_SearchResolveNameCommand_EX = new ExEventLog.EventTuple(1074005705U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000158 RID: 344
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HxMail_SyncRecipientsCommand_EX = new ExEventLog.EventTuple(3221489354U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000159 RID: 345
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_HxCalendar_SyncRecipientsCommand_EX = new ExEventLog.EventTuple(1074005707U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400015A RID: 346
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HxMail_SetupMailboxCommand_EX = new ExEventLog.EventTuple(3221489358U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400015B RID: 347
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_HxMail_SetupMailboxCommand_OK = new ExEventLog.EventTuple(1074005711U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400015C RID: 348
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HxMail_AddAccountCommand_EX = new ExEventLog.EventTuple(3221489360U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400015D RID: 349
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_HxMail_AddAccountCommand_OK = new ExEventLog.EventTuple(1074005713U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400015E RID: 350
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GetAggregatedAccount_GetAggregatedAccountAction_EX = new ExEventLog.EventTuple(3221489362U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400015F RID: 351
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GetAggregatedAccount_GetAggregatedAccountAction_OK = new ExEventLog.EventTuple(1074005715U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000160 RID: 352
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AddAggregatedAccount_AddAggregatedAccountAction_EX = new ExEventLog.EventTuple(3221489364U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000161 RID: 353
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AddAggregatedAccount_AddAggregatedAccountAction_OK = new ExEventLog.EventTuple(1074005717U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000162 RID: 354
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SendLinkClickedSignalToSP_SendLinkClickedSignalToSPAction_EX = new ExEventLog.EventTuple(3221489366U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000163 RID: 355
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SendLinkClickedSignalToSP_SendLinkClickedSignalToSPAction_OK = new ExEventLog.EventTuple(1074005719U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x02000004 RID: 4
		private enum Category : short
		{
			// Token: 0x04000165 RID: 357
			General = 1,
			// Token: 0x04000166 RID: 358
			Triggers
		}

		// Token: 0x02000005 RID: 5
		internal enum Message : uint
		{
			// Token: 0x04000168 RID: 360
			ConfigurationExceptionOnStartup = 3221488617U,
			// Token: 0x04000169 RID: 361
			DirectoryCreationException,
			// Token: 0x0400016A RID: 362
			PerformanceCounterWarningTriggerEvent = 2147746797U,
			// Token: 0x0400016B RID: 363
			PerformanceCounterErrorTriggerEvent = 3221488622U,
			// Token: 0x0400016C RID: 364
			PerformanceCounterInformationTriggerEvent = 1074005021U,
			// Token: 0x0400016D RID: 365
			PerformanceLogError = 3221488623U,
			// Token: 0x0400016E RID: 366
			ServiceStarting = 1074004976U,
			// Token: 0x0400016F RID: 367
			ServiceStarted = 1074004992U,
			// Token: 0x04000170 RID: 368
			ServiceStopping = 1074004977U,
			// Token: 0x04000171 RID: 369
			ServiceStopped = 1074004993U,
			// Token: 0x04000172 RID: 370
			RetentionAgentUnhandledException = 3221488627U,
			// Token: 0x04000173 RID: 371
			RetentionAgentDataLossOccurred,
			// Token: 0x04000174 RID: 372
			RetentionAgentPotentialDataLossWarning = 2147746805U,
			// Token: 0x04000175 RID: 373
			SqlOutputStreamUnhandledException = 3221488630U,
			// Token: 0x04000176 RID: 374
			ActiveDirectoryUnavailable,
			// Token: 0x04000177 RID: 375
			SqlOutputStreamConnectionStringFromAdNotFound,
			// Token: 0x04000178 RID: 376
			SqlOutputStreamConsecutiveRetriesForASpecifiedTime,
			// Token: 0x04000179 RID: 377
			SqlOutputStreamDecryptFailed,
			// Token: 0x0400017A RID: 378
			BadIisLogHeader = 3221488636U,
			// Token: 0x0400017B RID: 379
			IisLogLineNotProcessed,
			// Token: 0x0400017C RID: 380
			OwaServiceUnavailable,
			// Token: 0x0400017D RID: 381
			RelogManagerUnhandledException,
			// Token: 0x0400017E RID: 382
			OwaHttpStatus400 = 3221488642U,
			// Token: 0x0400017F RID: 383
			OwaHttpStatus440,
			// Token: 0x04000180 RID: 384
			OwaHttpStatus500,
			// Token: 0x04000181 RID: 385
			OwaHttpStatusOther,
			// Token: 0x04000182 RID: 386
			JobManagerStarted = 1074004998U,
			// Token: 0x04000183 RID: 387
			JobManagerNotStarted,
			// Token: 0x04000184 RID: 388
			JobManagerStartupFailures = 3221488648U,
			// Token: 0x04000185 RID: 389
			JobCrashed,
			// Token: 0x04000186 RID: 390
			JobPoisoned,
			// Token: 0x04000187 RID: 391
			BingServicesLatencyAboveThreshold,
			// Token: 0x04000188 RID: 392
			BingServicesLatencyBelowThreshold = 1074005004U,
			// Token: 0x04000189 RID: 393
			FindPlacesFailureAboveThreshold = 3221488653U,
			// Token: 0x0400018A RID: 394
			FindPlacesFailureBelowThreshold = 1074005006U,
			// Token: 0x0400018B RID: 395
			BitlockerStateDetectionError = 3221488655U,
			// Token: 0x0400018C RID: 396
			BitlockerState = 1074005008U,
			// Token: 0x0400018D RID: 397
			ConnectionStringManagerPartitionInvalid = 3221488657U,
			// Token: 0x0400018E RID: 398
			ConnectionStringManagerUnableToConnect,
			// Token: 0x0400018F RID: 399
			AnalyzerNotAdded = 1074005011U,
			// Token: 0x04000190 RID: 400
			WatsonCrashAlert = 3221488660U,
			// Token: 0x04000191 RID: 401
			WatsonExceptionCrashAlert = 3221488670U,
			// Token: 0x04000192 RID: 402
			OwaLyncFailureAboveThreshold = 3221488664U,
			// Token: 0x04000193 RID: 403
			OwaLyncFailureBelowThreshold = 1074005017U,
			// Token: 0x04000194 RID: 404
			OwaTooManyExceptionsEncountered = 3221488717U,
			// Token: 0x04000195 RID: 405
			TooManyObjectsOpenedException,
			// Token: 0x04000196 RID: 406
			OwaLogonFailures,
			// Token: 0x04000197 RID: 407
			OwaHighLatencyLoadStartPage,
			// Token: 0x04000198 RID: 408
			OwaTooManyHttpErrorResponsesEncountered,
			// Token: 0x04000199 RID: 409
			OwaTooManyHttpErrorResponsesResolved = 1074005074U,
			// Token: 0x0400019A RID: 410
			OabTooManyHttpErrorResponsesEncountered = 3221489167U,
			// Token: 0x0400019B RID: 411
			OabTooManyHttpErrorResponsesResolved = 1074005520U,
			// Token: 0x0400019C RID: 412
			OabTooManyExceptionsEncountered = 3221489169U,
			// Token: 0x0400019D RID: 413
			OabFileLoadException,
			// Token: 0x0400019E RID: 414
			IisAppPoolHttpErrorsDefaultEvent = 3221488723U,
			// Token: 0x0400019F RID: 415
			IisAppPoolTooManyHttpErrorResponsesEncounteredDefaultEvent,
			// Token: 0x040001A0 RID: 416
			IisAppPoolTooManyHttpErrorResponsesResolvedDefaultEvent = 1074005077U,
			// Token: 0x040001A1 RID: 417
			OwaStartPageFailures = 3221488726U,
			// Token: 0x040001A2 RID: 418
			OwaStartPageFailuresResolved = 1074005079U,
			// Token: 0x040001A3 RID: 419
			OwaActiveDatabaseAvailabilityBelowThreshold = 3221488728U,
			// Token: 0x040001A4 RID: 420
			OwaActiveDatabaseAvailabilityBelowThresholdResolved = 1074005081U,
			// Token: 0x040001A5 RID: 421
			OwaLogoffFailures = 3221488730U,
			// Token: 0x040001A6 RID: 422
			OwaLogoffFailuresResolved = 1074005083U,
			// Token: 0x040001A7 RID: 423
			TestEvent = 1074003968U,
			// Token: 0x040001A8 RID: 424
			RbaRequestsWithExceptionsReachedThreshold = 3221488661U,
			// Token: 0x040001A9 RID: 425
			RbaAtLeastOneExceptionReachedThreshold,
			// Token: 0x040001AA RID: 426
			RbaAllIsWell = 1074005015U,
			// Token: 0x040001AB RID: 427
			CalSyncRequestsWithExceptionsReachedThreshold = 3221488666U,
			// Token: 0x040001AC RID: 428
			CalSyncRequestsWithSyncFailuresReachedThreshold,
			// Token: 0x040001AD RID: 429
			CalSyncAllIsWell = 1074005020U,
			// Token: 0x040001AE RID: 430
			AvailabilityServiceExceptionAboveThreshold = 3221489272U,
			// Token: 0x040001AF RID: 431
			TxSyncPopRequestsWithExceptionsReachedThreshold = 3221488676U,
			// Token: 0x040001B0 RID: 432
			TxSyncIMAPRequestsWithExceptionsReachedThreshold,
			// Token: 0x040001B1 RID: 433
			TxSyncDeltaSyncMailRequestsWithExceptionsReachedThreshold,
			// Token: 0x040001B2 RID: 434
			TxSyncFacebookRequestsWithExceptionsReachedThreshold,
			// Token: 0x040001B3 RID: 435
			TxSyncLinkedInRequestsWithExceptionsReachedThreshold,
			// Token: 0x040001B4 RID: 436
			TxSyncPopIsWell = 1074005033U,
			// Token: 0x040001B5 RID: 437
			TxSyncIMAPIsWell,
			// Token: 0x040001B6 RID: 438
			TxSyncDeltaSyncMailIsWell,
			// Token: 0x040001B7 RID: 439
			TxSyncFacebookIsWell,
			// Token: 0x040001B8 RID: 440
			TxSyncLinkedInIsWell,
			// Token: 0x040001B9 RID: 441
			PowerShellTimeoutAuthzRequestAboveThreshold = 3221489717U,
			// Token: 0x040001BA RID: 442
			PowerShellLiveIDTimeoutAuthzRequestAboveThreshold,
			// Token: 0x040001BB RID: 443
			PowerShellLegacyTimeoutAuthzRequestAboveThreshold,
			// Token: 0x040001BC RID: 444
			PowerShellUnhandledCmdletExceptionAboveThreshold,
			// Token: 0x040001BD RID: 445
			PowerShellLiveIDUnhandledCmdletExceptionAboveThreshold,
			// Token: 0x040001BE RID: 446
			PowerShellLegacyUnhandledCmdletExceptionAboveThreshold,
			// Token: 0x040001BF RID: 447
			PowerShellLongLatencyCmdletAboveThreshold,
			// Token: 0x040001C0 RID: 448
			PowerShellLiveIDLongLatencyCmdletAboveThreshold,
			// Token: 0x040001C1 RID: 449
			PowerShellLegacyLongLatencyCmdletAboveThreshold,
			// Token: 0x040001C2 RID: 450
			PowerShellAuthzErrorAboveThreshold,
			// Token: 0x040001C3 RID: 451
			PowerShellLiveIDAuthzErrorAboveThreshold,
			// Token: 0x040001C4 RID: 452
			PowerShellLegacyAuthzErrorAboveThreshold,
			// Token: 0x040001C5 RID: 453
			PSWSLongLatencyCmdletAboveThreshold,
			// Token: 0x040001C6 RID: 454
			PSWSUnhandledCmdletExceptionAboveThreshold,
			// Token: 0x040001C7 RID: 455
			PowerShellHttpGenericErrorAboveThreshold,
			// Token: 0x040001C8 RID: 456
			PowerShellLiveIDHttpGenericErrorAboveThreshold,
			// Token: 0x040001C9 RID: 457
			PowerShellLegacyHttpGenericErrorAboveThreshold,
			// Token: 0x040001CA RID: 458
			PowerShellHttpErrorResponseAboveThreshold,
			// Token: 0x040001CB RID: 459
			PowerShellLiveIDHttpErrorResponseAboveThreshold,
			// Token: 0x040001CC RID: 460
			PowerShellLegacyHttpErrorResponseAboveThreshold,
			// Token: 0x040001CD RID: 461
			TestApi1_TestCan1_EX = 3221488686U,
			// Token: 0x040001CE RID: 462
			TestApi1_TestCan1_OK = 1074005039U,
			// Token: 0x040001CF RID: 463
			TestApi2_TestCan2_EX = 3221488688U,
			// Token: 0x040001D0 RID: 464
			TestApi2_TestCan2_OK = 1074005041U,
			// Token: 0x040001D1 RID: 465
			FindPeople_BrowseInMailbox_EX = 3221488690U,
			// Token: 0x040001D2 RID: 466
			FindPeople_BrowseInMailbox_OK = 1074005043U,
			// Token: 0x040001D3 RID: 467
			FindPeople_HoverCard_EX = 3221488692U,
			// Token: 0x040001D4 RID: 468
			FindPeople_HoverCard_OK = 1074005045U,
			// Token: 0x040001D5 RID: 469
			FindPeople_RecipientCache_EX = 3221488694U,
			// Token: 0x040001D6 RID: 470
			FindPeople_RecipientCache_OK = 1074005047U,
			// Token: 0x040001D7 RID: 471
			TestApi4__EX = 3221488696U,
			// Token: 0x040001D8 RID: 472
			TestApi4__OK = 1074005049U,
			// Token: 0x040001D9 RID: 473
			CreateItem_CreateMessageForComposeSend_EX = 3221489316U,
			// Token: 0x040001DA RID: 474
			CreateItem_CreateMessageForComposeSend_OK = 1074005669U,
			// Token: 0x040001DB RID: 475
			CreateItem_MailComposeUpgrade_EX = 3221489318U,
			// Token: 0x040001DC RID: 476
			CreateItem_MailComposeUpgrade_OK = 1074005671U,
			// Token: 0x040001DD RID: 477
			CreateItem_CreateResponseSend_EX = 3221489320U,
			// Token: 0x040001DE RID: 478
			CreateItem_CreateResponseSend_OK = 1074005673U,
			// Token: 0x040001DF RID: 479
			UpdateItem_UpdateMessageForComposeSend_EX = 3221489322U,
			// Token: 0x040001E0 RID: 480
			UpdateItem_UpdateMessageForComposeSend_OK = 1074005675U,
			// Token: 0x040001E1 RID: 481
			CreateItem_CreateMessageForCompose_EX = 3221489624U,
			// Token: 0x040001E2 RID: 482
			CreateItem_CreateMessageForCompose_OK = 1074005977U,
			// Token: 0x040001E3 RID: 483
			CreateItem_CreateResponse_EX = 3221489626U,
			// Token: 0x040001E4 RID: 484
			CreateItem_CreateResponse_OK = 1074005979U,
			// Token: 0x040001E5 RID: 485
			UpdateItem_UpdateMessageForCompose_EX = 3221489628U,
			// Token: 0x040001E6 RID: 486
			UpdateItem_UpdateMessageForCompose_OK = 1074005981U,
			// Token: 0x040001E7 RID: 487
			GetWacAttachmentInfo__EX = 3221488817U,
			// Token: 0x040001E8 RID: 488
			GetWacAttachmentInfo__OK = 1074005170U,
			// Token: 0x040001E9 RID: 489
			WACMdbCacheUpdate__EX = 3221488819U,
			// Token: 0x040001EA RID: 490
			WACMdbCacheUpdate__OK = 1074005172U,
			// Token: 0x040001EB RID: 491
			WACCheckFile__EX = 3221488821U,
			// Token: 0x040001EC RID: 492
			WACCheckFile__OK = 1074005174U,
			// Token: 0x040001ED RID: 493
			WACGetFile__EX = 3221488823U,
			// Token: 0x040001EE RID: 494
			WACGetFile__OK = 1074005176U,
			// Token: 0x040001EF RID: 495
			WACCleanCobaltStore__EX = 3221488825U,
			// Token: 0x040001F0 RID: 496
			WACCleanCobaltStore__OK = 1074005178U,
			// Token: 0x040001F1 RID: 497
			WACPutFile__EX = 3221488827U,
			// Token: 0x040001F2 RID: 498
			WACPutFile__OK = 1074005180U,
			// Token: 0x040001F3 RID: 499
			WACCobalt__EX = 3221488829U,
			// Token: 0x040001F4 RID: 500
			WACCobalt__OK = 1074005182U,
			// Token: 0x040001F5 RID: 501
			WACAutoSave__EX = 3221488831U,
			// Token: 0x040001F6 RID: 502
			WACAutoSave__OK = 1074005184U,
			// Token: 0x040001F7 RID: 503
			GetWacIFrameUrl__EX = 3221488833U,
			// Token: 0x040001F8 RID: 504
			GetWacIFrameUrl__OK = 1074005186U,
			// Token: 0x040001F9 RID: 505
			WACBadRequest__EX = 3221488835U,
			// Token: 0x040001FA RID: 506
			WACBadRequest__OK = 1074005188U,
			// Token: 0x040001FB RID: 507
			WACLock__EX = 3221488837U,
			// Token: 0x040001FC RID: 508
			WACLock__OK = 1074005190U,
			// Token: 0x040001FD RID: 509
			WACUnlock__EX = 3221488839U,
			// Token: 0x040001FE RID: 510
			WACUnlock__OK = 1074005192U,
			// Token: 0x040001FF RID: 511
			WACCacheEntryExpired__EX = 3221488841U,
			// Token: 0x04000200 RID: 512
			WACCacheEntryExpired__OK = 1074005194U,
			// Token: 0x04000201 RID: 513
			WACRefreshLock__EX = 3221488843U,
			// Token: 0x04000202 RID: 514
			WACRefreshLock__OK = 1074005196U,
			// Token: 0x04000203 RID: 515
			CreateAttachment__EX = 3221488845U,
			// Token: 0x04000204 RID: 516
			CreateAttachment__OK = 1074005198U,
			// Token: 0x04000205 RID: 517
			CreateAttachmentFromAttachmentDataProvider__EX = 3221488847U,
			// Token: 0x04000206 RID: 518
			CreateAttachmentFromAttachmentDataProvider__OK = 1074005200U,
			// Token: 0x04000207 RID: 519
			CreateReferenceAttachmentFromLocalFile__EX = 3221488849U,
			// Token: 0x04000208 RID: 520
			CreateReferenceAttachmentFromLocalFile__OK = 1074005202U,
			// Token: 0x04000209 RID: 521
			CreateReferenceAttachmentFromAttachmentDataProvider__EX = 3221488851U,
			// Token: 0x0400020A RID: 522
			CreateReferenceAttachmentFromAttachmentDataProvider__OK = 1074005204U,
			// Token: 0x0400020B RID: 523
			GetAttachmentDataProviders__EX = 3221488853U,
			// Token: 0x0400020C RID: 524
			GetAttachmentDataProviders__OK = 1074005206U,
			// Token: 0x0400020D RID: 525
			GetAttachmentDataProviderItems__EX = 3221488855U,
			// Token: 0x0400020E RID: 526
			GetAttachmentDataProviderItems__OK = 1074005208U,
			// Token: 0x0400020F RID: 527
			GetAttachmentDataProviderRecentItems__EX = 3221488857U,
			// Token: 0x04000210 RID: 528
			GetAttachmentDataProviderRecentItems__OK = 1074005210U,
			// Token: 0x04000211 RID: 529
			SynchronizeWacAttachment__EX = 3221488859U,
			// Token: 0x04000212 RID: 530
			SynchronizeWacAttachment__OK = 1074005212U,
			// Token: 0x04000213 RID: 531
			GetFileAttachment__EX = 3221488861U,
			// Token: 0x04000214 RID: 532
			GetFileAttachment__OK = 1074005214U,
			// Token: 0x04000215 RID: 533
			FindConversation_BrowseAll_EX = 3221489116U,
			// Token: 0x04000216 RID: 534
			FindConversation_BrowseAll_OK = 1074005469U,
			// Token: 0x04000217 RID: 535
			FindConversation_BrowseUnread_EX = 3221489118U,
			// Token: 0x04000218 RID: 536
			FindConversation_BrowseUnread_OK = 1074005471U,
			// Token: 0x04000219 RID: 537
			FindConversation_BrowseNoClutterAll_EX = 3221489126U,
			// Token: 0x0400021A RID: 538
			FindConversation_BrowseNoClutterAll_OK = 1074005479U,
			// Token: 0x0400021B RID: 539
			FindConversation_BrowseNoClutterUnread_EX = 3221489128U,
			// Token: 0x0400021C RID: 540
			FindConversation_BrowseNoClutterUnread_OK = 1074005481U,
			// Token: 0x0400021D RID: 541
			GetConversationItems__EX = 3221488700U,
			// Token: 0x0400021E RID: 542
			GetConversationItems__OK = 1074005053U,
			// Token: 0x0400021F RID: 543
			FindItem_BrowseAll_EX = 3221489146U,
			// Token: 0x04000220 RID: 544
			FindItem_BrowseAll_OK = 1074005499U,
			// Token: 0x04000221 RID: 545
			FindItem_BrowseUnread_EX = 3221489148U,
			// Token: 0x04000222 RID: 546
			FindItem_BrowseUnread_OK = 1074005501U,
			// Token: 0x04000223 RID: 547
			FindItem_BrowseNoClutterAll_EX = 3221489156U,
			// Token: 0x04000224 RID: 548
			FindItem_BrowseNoClutterAll_OK = 1074005509U,
			// Token: 0x04000225 RID: 549
			FindItem_BrowseNoClutterUnread_EX = 3221489158U,
			// Token: 0x04000226 RID: 550
			FindItem_BrowseNoClutterUnread_OK = 1074005511U,
			// Token: 0x04000227 RID: 551
			GetItem_GetMailItem_EX = 3221488704U,
			// Token: 0x04000228 RID: 552
			GetItem_GetMailItem_OK = 1074005057U,
			// Token: 0x04000229 RID: 553
			GetExtensibilityContext_ExtLoadApps_EX = 3221489017U,
			// Token: 0x0400022A RID: 554
			GetExtensibilityContext_ExtLoadApps_OK = 1074005370U,
			// Token: 0x0400022B RID: 555
			CreateModernGroup_CreateModernGroupAction_EX = 3221489019U,
			// Token: 0x0400022C RID: 556
			CreateModernGroup_CreateModernGroupAction_OK = 1074005372U,
			// Token: 0x0400022D RID: 557
			CreateUnifiedGroup_CreateUnifiedGroupAction_EX = 3221489041U,
			// Token: 0x0400022E RID: 558
			CreateUnifiedGroup_CreateUnifiedGroupAction_OK = 1074005394U,
			// Token: 0x0400022F RID: 559
			AddMembersToUnifiedGroup_AddMembersToUnifiedGroupAction_EX = 3221489043U,
			// Token: 0x04000230 RID: 560
			AddMembersToUnifiedGroup_AddMembersToUnifiedGroupAction_OK = 1074005396U,
			// Token: 0x04000231 RID: 561
			GetModernGroup_GetModernGroupAction_EX = 3221489021U,
			// Token: 0x04000232 RID: 562
			GetModernGroup_GetModernGroupAction_OK = 1074005374U,
			// Token: 0x04000233 RID: 563
			GetModernGroups_GetModernGroupsAction_EX = 3221489023U,
			// Token: 0x04000234 RID: 564
			GetModernGroups_GetModernGroupsAction_OK = 1074005376U,
			// Token: 0x04000235 RID: 565
			GetModernGroupUnseenItems_GetModernGroupUnseenItemsAction_EX = 3221489025U,
			// Token: 0x04000236 RID: 566
			GetModernGroupUnseenItems_GetModernGroupUnseenItemsAction_OK = 1074005378U,
			// Token: 0x04000237 RID: 567
			PostModernGroupItem_QuickCompose_EX = 3221489027U,
			// Token: 0x04000238 RID: 568
			PostModernGroupItem_QuickCompose_OK = 1074005380U,
			// Token: 0x04000239 RID: 569
			PostModernGroupItem_QuickReply_EX = 3221489045U,
			// Token: 0x0400023A RID: 570
			PostModernGroupItem_QuickReply_OK = 1074005398U,
			// Token: 0x0400023B RID: 571
			UpdateModernGroup_UpdateModernGroupAction_EX = 3221489029U,
			// Token: 0x0400023C RID: 572
			UpdateModernGroup_UpdateModernGroupAction_OK = 1074005382U,
			// Token: 0x0400023D RID: 573
			GetConversationItems_ModernGroup_EX = 3221489031U,
			// Token: 0x0400023E RID: 574
			GetConversationItems_ModernGroup_OK = 1074005384U,
			// Token: 0x0400023F RID: 575
			FindConversation_ModernGroupAll_EX = 3221489033U,
			// Token: 0x04000240 RID: 576
			FindConversation_ModernGroupAll_OK = 1074005386U,
			// Token: 0x04000241 RID: 577
			FindConversation_SearchModernGroupAll_EX = 3221489047U,
			// Token: 0x04000242 RID: 578
			FindConversation_SearchModernGroupAll_OK = 1074005400U,
			// Token: 0x04000243 RID: 579
			SetModernGroupMembership_JoinModernGroup_EX = 3221489035U,
			// Token: 0x04000244 RID: 580
			SetModernGroupMembership_JoinModernGroup_OK = 1074005388U,
			// Token: 0x04000245 RID: 581
			SetModernGroupMembership_LeaveModernGroup_EX = 3221489037U,
			// Token: 0x04000246 RID: 582
			SetModernGroupMembership_LeaveModernGroup_OK = 1074005390U,
			// Token: 0x04000247 RID: 583
			RemoveModernGroup_RemoveModernGroupAction_EX = 3221489039U,
			// Token: 0x04000248 RID: 584
			RemoveModernGroup_RemoveModernGroupAction_OK = 1074005392U,
			// Token: 0x04000249 RID: 585
			GetPersonaPhoto__EX = 3221489287U,
			// Token: 0x0400024A RID: 586
			GetPersonaPhoto__OK = 1074005640U,
			// Token: 0x0400024B RID: 587
			UploadPhoto__EX = 3221489312U,
			// Token: 0x0400024C RID: 588
			UploadPhoto__OK = 1074005665U,
			// Token: 0x0400024D RID: 589
			UploadPhotoFromForm__EX = 3221489314U,
			// Token: 0x0400024E RID: 590
			UploadPhotoFromForm__OK = 1074005667U,
			// Token: 0x0400024F RID: 591
			ProcessSuiteStorage__EX = 3221489417U,
			// Token: 0x04000250 RID: 592
			ProcessSuiteStorage__OK = 1074005770U,
			// Token: 0x04000251 RID: 593
			SetNotificationSettings__EX = 3221489419U,
			// Token: 0x04000252 RID: 594
			SetNotificationSettings__OK = 1074005772U,
			// Token: 0x04000253 RID: 595
			TestApi1__EX = 3221489324U,
			// Token: 0x04000254 RID: 596
			TestApi1__OK = 1074005677U,
			// Token: 0x04000255 RID: 597
			TestApi2__EX = 3221489326U,
			// Token: 0x04000256 RID: 598
			TestApi2__OK = 1074005679U,
			// Token: 0x04000257 RID: 599
			GetUserPhoto__EX = 3221489328U,
			// Token: 0x04000258 RID: 600
			GetUserPhoto__OK = 1074005681U,
			// Token: 0x04000259 RID: 601
			FindPeople_PeopleIKnow_EX = 3221489310U,
			// Token: 0x0400025A RID: 602
			FindPeople_PeopleIKnow_OK = 1074005663U,
			// Token: 0x0400025B RID: 603
			FindPeople_BrowseInDirectory_EX = 3221489517U,
			// Token: 0x0400025C RID: 604
			FindPeople_BrowseInDirectory_OK = 1074005870U,
			// Token: 0x0400025D RID: 605
			FindPeople_PeopleModule_EX = 3221489519U,
			// Token: 0x0400025E RID: 606
			FindPeople_PeopleModule_OK = 1074005872U,
			// Token: 0x0400025F RID: 607
			FindPeople_ComposeForms_EX = 3221489521U,
			// Token: 0x04000260 RID: 608
			FindPeople_ComposeForms_OK = 1074005874U,
			// Token: 0x04000261 RID: 609
			GetPersona_PersonaCardreadOnlyRecipientWell_EX = 3221489367U,
			// Token: 0x04000262 RID: 610
			GetPersona_PersonaCardreadOnlyRecipientWell_OK = 1074005720U,
			// Token: 0x04000263 RID: 611
			GetPersona_PersonaCardreadWriteRecipientWell_EX = 3221489369U,
			// Token: 0x04000264 RID: 612
			GetPersona_PersonaCardreadWriteRecipientWell_OK = 1074005722U,
			// Token: 0x04000265 RID: 613
			GetPersona_PersonaCardsharePoint_EX = 3221489371U,
			// Token: 0x04000266 RID: 614
			GetPersona_PersonaCardsharePoint_OK = 1074005724U,
			// Token: 0x04000267 RID: 615
			GetPersona_PersonaCardpeopleHub_EX = 3221489373U,
			// Token: 0x04000268 RID: 616
			GetPersona_PersonaCardpeopleHub_OK = 1074005726U,
			// Token: 0x04000269 RID: 617
			GetPersona_HoverCardreadOnlyRecipientWell_EX = 3221489377U,
			// Token: 0x0400026A RID: 618
			GetPersona_HoverCardreadOnlyRecipientWell_OK = 1074005730U,
			// Token: 0x0400026B RID: 619
			GetPersona_HoverCardreadWriteRecipientWell_EX = 3221489379U,
			// Token: 0x0400026C RID: 620
			GetPersona_HoverCardreadWriteRecipientWell_OK = 1074005732U,
			// Token: 0x0400026D RID: 621
			PerfTraceCTQ_TestCtq1_EX = 3221488706U,
			// Token: 0x0400026E RID: 622
			PerfTraceCTQ_TestCtq1_OK = 1074005059U,
			// Token: 0x0400026F RID: 623
			PerfTraceCTQ_TestCtq2_EX = 3221488708U,
			// Token: 0x04000270 RID: 624
			PerfTraceCTQ_TestCtq2_OK = 1074005061U,
			// Token: 0x04000271 RID: 625
			PerfTraceCTQ_NavigateToPeople_EX = 3221488710U,
			// Token: 0x04000272 RID: 626
			PerfTraceCTQ_NavigateToPeople_OK = 1074005063U,
			// Token: 0x04000273 RID: 627
			PerfTraceCTQ_ShowPersonaCardCollapsed_EX = 3221489437U,
			// Token: 0x04000274 RID: 628
			PerfTraceCTQ_ShowPersonaCardCollapsed_OK = 1074005790U,
			// Token: 0x04000275 RID: 629
			PerfTraceCTQ_ShowPersonaCardExpanded_EX = 3221489439U,
			// Token: 0x04000276 RID: 630
			PerfTraceCTQ_ShowPersonaCardExpanded_OK = 1074005792U,
			// Token: 0x04000277 RID: 631
			PerfTraceCTQ_GetWacAttachmentInfo_EX = 3221488917U,
			// Token: 0x04000278 RID: 632
			PerfTraceCTQ_GetWacAttachmentInfo_OK = 1074005270U,
			// Token: 0x04000279 RID: 633
			PerfTraceCTQ_EditACopy_EX = 3221488919U,
			// Token: 0x0400027A RID: 634
			PerfTraceCTQ_EditACopy_OK = 1074005272U,
			// Token: 0x0400027B RID: 635
			PerfTraceCTQ_DocumentAttachmentPopOut_EX = 3221488921U,
			// Token: 0x0400027C RID: 636
			PerfTraceCTQ_DocumentAttachmentPopOut_OK = 1074005274U,
			// Token: 0x0400027D RID: 637
			PerfTraceCTQ_GetWacIFrameUrl_EX = 3221488925U,
			// Token: 0x0400027E RID: 638
			PerfTraceCTQ_GetWacIFrameUrl_OK = 1074005278U,
			// Token: 0x0400027F RID: 639
			Ecp_EventLog_HttpUnhandledExceptionReachedThreshold = 3221488927U,
			// Token: 0x04000280 RID: 640
			Ecp_EventLog_LandingDefaultPageErrorReachedThreshold,
			// Token: 0x04000281 RID: 641
			SSLCertificateWarningEvent = 2147746912U,
			// Token: 0x04000282 RID: 642
			SSLCertificateErrorEvent = 3221488737U,
			// Token: 0x04000283 RID: 643
			ExtensibilityOmexWsRequestErrorReachedThreshold = 3221489236U,
			// Token: 0x04000284 RID: 644
			OAuthPassiveMonitoringExceptionAboveThreshold = 3221489286U,
			// Token: 0x04000285 RID: 645
			OABGenTenantOutOfSLA = 3221489166U,
			// Token: 0x04000286 RID: 646
			MailboxAuditingFailureAboveThreshold = 3221490617U,
			// Token: 0x04000287 RID: 647
			AdminAuditingFailureAboveThreshold,
			// Token: 0x04000288 RID: 648
			SynchronousAuditSearchFailureAboveThreshold,
			// Token: 0x04000289 RID: 649
			AsynchronousAuditSearchFailureAboveThreshold,
			// Token: 0x0400028A RID: 650
			PFAssistantItemProcessor_EX = 3221489516U,
			// Token: 0x0400028B RID: 651
			EscalateItem_MSExchangeDelivery_EX = 3221489616U,
			// Token: 0x0400028C RID: 652
			EscalateItem_MSExchangeDelivery_OK = 1074005969U,
			// Token: 0x0400028D RID: 653
			EscalationGetter_MSExchangeDelivery_EX = 3221489618U,
			// Token: 0x0400028E RID: 654
			EscalationGetter_MSExchangeDelivery_OK = 1074005971U,
			// Token: 0x0400028F RID: 655
			EscalateItem_MSExchangeOWAAppPool_EX = 3221489620U,
			// Token: 0x04000290 RID: 656
			EscalateItem_MSExchangeOWAAppPool_OK = 1074005973U,
			// Token: 0x04000291 RID: 657
			EscalationGetter_MSExchangeOWAAppPool_EX = 3221489622U,
			// Token: 0x04000292 RID: 658
			EscalationGetter_MSExchangeOWAAppPool_OK = 1074005975U,
			// Token: 0x04000293 RID: 659
			HxService_WindowsEventLog_EX = 3221489404U,
			// Token: 0x04000294 RID: 660
			HxService_WindowsEventLog_OK = 1074005757U,
			// Token: 0x04000295 RID: 661
			HxService_AllEventLog_EX = 3221489376U,
			// Token: 0x04000296 RID: 662
			HxService_AllEventLog_OK = 1074005729U,
			// Token: 0x04000297 RID: 663
			HxService_NoUserEvents_EX = 3221489378U,
			// Token: 0x04000298 RID: 664
			HxService_NoUserEvents_OK = 1074005731U,
			// Token: 0x04000299 RID: 665
			HxMail_GetConversationPartsCommand_EX = 3221489330U,
			// Token: 0x0400029A RID: 666
			HxMail_GetConversationPartsCommand_OK = 1074005683U,
			// Token: 0x0400029B RID: 667
			HxMail_BeginSyncCommand_EX = 3221489356U,
			// Token: 0x0400029C RID: 668
			HxMail_BeginSyncCommand_OK = 1074005709U,
			// Token: 0x0400029D RID: 669
			HxCalendar_GetBasedSyncCommand_EX = 3221489332U,
			// Token: 0x0400029E RID: 670
			HxCalendar_GetBasedSyncCommand_OK = 1074005685U,
			// Token: 0x0400029F RID: 671
			HxCalendar_CreateItemCommand_EX = 3221489340U,
			// Token: 0x040002A0 RID: 672
			HxCalendar_CreateItemCommand_OK = 1074005693U,
			// Token: 0x040002A1 RID: 673
			HxCalendar_CalendarDataCommand_EX = 3221489342U,
			// Token: 0x040002A2 RID: 674
			HxCalendar_CalendarDataCommand_OK = 1074005695U,
			// Token: 0x040002A3 RID: 675
			HxCalendar_CancelEventCommand_EX = 3221489344U,
			// Token: 0x040002A4 RID: 676
			HxCalendar_CancelEventCommand_OK = 1074005697U,
			// Token: 0x040002A5 RID: 677
			HxCalendar_UpdateItemCommand_EX = 3221489346U,
			// Token: 0x040002A6 RID: 678
			HxCalendar_UpdateItemCommand_OK = 1074005699U,
			// Token: 0x040002A7 RID: 679
			HxCalendar_GetFullItemCommand_EX = 3221489348U,
			// Token: 0x040002A8 RID: 680
			HxCalendar_GetFullItemCommand_OK = 1074005701U,
			// Token: 0x040002A9 RID: 681
			HxCalendar_SyncCommand_EX = 3221489350U,
			// Token: 0x040002AA RID: 682
			HxCalendar_SyncCommand_OK = 1074005703U,
			// Token: 0x040002AB RID: 683
			HxService_HttpStatusErrorCode_EX = 3221489406U,
			// Token: 0x040002AC RID: 684
			HxService_HttpStatusErrorCode_OK = 1074005759U,
			// Token: 0x040002AD RID: 685
			PFAssistantSplitFailed = 3221489526U,
			// Token: 0x040002AE RID: 686
			HxMail_SearchResolveNameCommand_EX = 3221489352U,
			// Token: 0x040002AF RID: 687
			HxCalendar_SearchResolveNameCommand_EX = 1074005705U,
			// Token: 0x040002B0 RID: 688
			HxMail_SyncRecipientsCommand_EX = 3221489354U,
			// Token: 0x040002B1 RID: 689
			HxCalendar_SyncRecipientsCommand_EX = 1074005707U,
			// Token: 0x040002B2 RID: 690
			HxMail_SetupMailboxCommand_EX = 3221489358U,
			// Token: 0x040002B3 RID: 691
			HxMail_SetupMailboxCommand_OK = 1074005711U,
			// Token: 0x040002B4 RID: 692
			HxMail_AddAccountCommand_EX = 3221489360U,
			// Token: 0x040002B5 RID: 693
			HxMail_AddAccountCommand_OK = 1074005713U,
			// Token: 0x040002B6 RID: 694
			GetAggregatedAccount_GetAggregatedAccountAction_EX = 3221489362U,
			// Token: 0x040002B7 RID: 695
			GetAggregatedAccount_GetAggregatedAccountAction_OK = 1074005715U,
			// Token: 0x040002B8 RID: 696
			AddAggregatedAccount_AddAggregatedAccountAction_EX = 3221489364U,
			// Token: 0x040002B9 RID: 697
			AddAggregatedAccount_AddAggregatedAccountAction_OK = 1074005717U,
			// Token: 0x040002BA RID: 698
			SendLinkClickedSignalToSP_SendLinkClickedSignalToSPAction_EX = 3221489366U,
			// Token: 0x040002BB RID: 699
			SendLinkClickedSignalToSP_SendLinkClickedSignalToSPAction_OK = 1074005719U
		}
	}
}
