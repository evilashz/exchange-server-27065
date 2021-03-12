using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000002 RID: 2
	public static class MSExchangeISEventLogConstants
	{
		// Token: 0x04000001 RID: 1
		public const string EventSource = "MSExchangeIS";

		// Token: 0x04000002 RID: 2
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InternalLogicError = new ExEventLog.EventTuple(3221488617U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000003 RID: 3
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnhandledException = new ExEventLog.EventTuple(3221488618U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000004 RID: 4
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LeakedException = new ExEventLog.EventTuple(3221488619U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000005 RID: 5
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_DuplicateRpcEndpoint = new ExEventLog.EventTuple(3221488620U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000006 RID: 6
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_DuplicateRpcMTEndpoint = new ExEventLog.EventTuple(3221488621U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000007 RID: 7
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_DuplicatePoolRpcEndpoint = new ExEventLog.EventTuple(3221488622U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000008 RID: 8
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_DuplicateAdminRpcEndpoint = new ExEventLog.EventTuple(3221488623U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000009 RID: 9
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxRetention = new ExEventLog.EventTuple(1074004976U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000A RID: 10
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxPurged = new ExEventLog.EventTuple(1074004977U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000B RID: 11
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxReconnected = new ExEventLog.EventTuple(1074004978U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000C RID: 12
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseBadVersion = new ExEventLog.EventTuple(3221488627U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000D RID: 13
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FullTextIndexException = new ExEventLog.EventTuple(3221488628U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000E RID: 14
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxPrequarantined = new ExEventLog.EventTuple(3221488629U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000F RID: 15
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxQuarantined = new ExEventLog.EventTuple(2147746806U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000010 RID: 16
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxUnquarantined = new ExEventLog.EventTuple(1074004983U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000011 RID: 17
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NamedPropertyMappingChange = new ExEventLog.EventTuple(3221488632U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000012 RID: 18
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RepidGuidMappingChange = new ExEventLog.EventTuple(3221488633U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000013 RID: 19
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceProcessTerminated = new ExEventLog.EventTuple(3221488634U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000014 RID: 20
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StartupFailureDueToADError = new ExEventLog.EventTuple(3221488635U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000015 RID: 21
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerProcessStarted = new ExEventLog.EventTuple(1074004988U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000016 RID: 22
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerProcessStopped = new ExEventLog.EventTuple(1074004989U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000017 RID: 23
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerProcessFailedToStart = new ExEventLog.EventTuple(3221488638U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000018 RID: 24
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceInitializationFailure = new ExEventLog.EventTuple(3221488639U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000019 RID: 25
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxLocaleIdChanged = new ExEventLog.EventTuple(1074004992U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001A RID: 26
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxGlobcntRolledOver = new ExEventLog.EventTuple(1074004993U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001B RID: 27
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxReplidExhaustionApproaching = new ExEventLog.EventTuple(2147746818U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001C RID: 28
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxReplidExhaustion = new ExEventLog.EventTuple(3221488643U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001D RID: 29
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TraceLoggerStarted = new ExEventLog.EventTuple(1074004996U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001E RID: 30
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TraceLoggerStopped = new ExEventLog.EventTuple(1074004997U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001F RID: 31
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TraceLoggerFailed = new ExEventLog.EventTuple(2147746822U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000020 RID: 32
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerProcessStopTimeout = new ExEventLog.EventTuple(3221488647U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000021 RID: 33
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StartTraceSessionFailed = new ExEventLog.EventTuple(3221488648U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000022 RID: 34
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TaskRequestFailed = new ExEventLog.EventTuple(1074005001U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000023 RID: 35
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TaskNonFatalDBException = new ExEventLog.EventTuple(3221488650U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000024 RID: 36
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TaskFatalDBException = new ExEventLog.EventTuple(3221488651U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000025 RID: 37
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseVersionTooOld = new ExEventLog.EventTuple(3221488652U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000026 RID: 38
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseVersionTooNew = new ExEventLog.EventTuple(3221488653U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000027 RID: 39
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseBadRequestedUpdgradeVersion = new ExEventLog.EventTuple(3221488654U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000028 RID: 40
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SchemaUpgradeFailure = new ExEventLog.EventTuple(3221488655U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000029 RID: 41
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SchemaUpgradeCommencing = new ExEventLog.EventTuple(1074005008U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400002A RID: 42
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SchemaUpgradeComplete = new ExEventLog.EventTuple(1074005009U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400002B RID: 43
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SchemaNoUpgradeFromCurrentVersionFailure = new ExEventLog.EventTuple(3221488658U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400002C RID: 44
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SchemaVersionPublicationComplete = new ExEventLog.EventTuple(1074005011U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400002D RID: 45
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SchemaVersionPublicationFailure = new ExEventLog.EventTuple(2147746836U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400002E RID: 46
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidAppConfig = new ExEventLog.EventTuple(3221488661U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400002F RID: 47
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CriticalBlockFailure = new ExEventLog.EventTuple(3221488662U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000030 RID: 48
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseGuidPatchRequired = new ExEventLog.EventTuple(1074005015U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000031 RID: 49
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseFileRestoreNotAllowed = new ExEventLog.EventTuple(3221488664U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000032 RID: 50
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TraceMaintenanceFailed = new ExEventLog.EventTuple(2147746841U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000033 RID: 51
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidMailboxGlobcntAllocation = new ExEventLog.EventTuple(3221488666U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000034 RID: 52
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MountedStoreNotInActiveDirectory = new ExEventLog.EventTuple(3221488667U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000035 RID: 53
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_TraceMessageFailed = new ExEventLog.EventTuple(3221488668U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000036 RID: 54
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_CannotDetectEnvironment = new ExEventLog.EventTuple(3221488669U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000037 RID: 55
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SchemaUpgradeQuarantined = new ExEventLog.EventTuple(1074005022U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000038 RID: 56
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SQLExceptionDetected = new ExEventLog.EventTuple(3221489617U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000039 RID: 57
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_IOExceptionDetected = new ExEventLog.EventTuple(3221489618U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400003A RID: 58
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FileDeleted = new ExEventLog.EventTuple(2147747795U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400003B RID: 59
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DirectoryDeleted = new ExEventLog.EventTuple(2147747796U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400003C RID: 60
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SQLExtremeLatency = new ExEventLog.EventTuple(3221489621U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400003D RID: 61
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetExceptionDetected = new ExEventLog.EventTuple(3221489622U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400003E RID: 62
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TableOperatorCommitTransaction = new ExEventLog.EventTuple(2147485655U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400003F RID: 63
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetFatalDatabaseException = new ExEventLog.EventTuple(3221489624U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000040 RID: 64
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidSubmitTime = new ExEventLog.EventTuple(2147749793U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000041 RID: 65
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PropertyPromotion = new ExEventLog.EventTuple(1074007970U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000042 RID: 66
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_IntegrityCheckStart = new ExEventLog.EventTuple(1074007971U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000043 RID: 67
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_IntegrityCheckEnd = new ExEventLog.EventTuple(1074007972U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000044 RID: 68
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_IntegrityCheckFailed = new ExEventLog.EventTuple(3221491621U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000045 RID: 69
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_IntegrityCheckMailboxStart = new ExEventLog.EventTuple(1074007974U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000046 RID: 70
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_IntegrityCheckCorruptionDetected = new ExEventLog.EventTuple(2147749799U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000047 RID: 71
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_IntegrityCheckMailboxEnd = new ExEventLog.EventTuple(1074007976U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000048 RID: 72
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_IntegrityCheckMailboxFailed = new ExEventLog.EventTuple(3221491625U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000049 RID: 73
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PossibleDuplicateMID = new ExEventLog.EventTuple(3221229482U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004A RID: 74
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CorruptSearchBacklink = new ExEventLog.EventTuple(3221229483U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004B RID: 75
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseMaintenancePreemptedByDbEngineBusy = new ExEventLog.EventTuple(2147487660U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004C RID: 76
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxMaintenancePreemptedByDbEngineBusy = new ExEventLog.EventTuple(2147487661U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004D RID: 77
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_IntegrityCheckConfigurationSkippedEntry = new ExEventLog.EventTuple(3221491630U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x0400004E RID: 78
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_IntegrityCheckScheduledNewCorruption = new ExEventLog.EventTuple(2147749807U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004F RID: 79
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CorruptDirectoryObjectDetected = new ExEventLog.EventTuple(3221492617U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000050 RID: 80
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnsupportedRecipientTypeDetected = new ExEventLog.EventTuple(3221492618U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000051 RID: 81
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PermanentADError = new ExEventLog.EventTuple(3221492619U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000052 RID: 82
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TransientADError = new ExEventLog.EventTuple(3221492620U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000053 RID: 83
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxStorageOverWarningLimit = new ExEventLog.EventTuple(2147746869U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000054 RID: 84
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxStorageOverQuotaLimit = new ExEventLog.EventTuple(2147746870U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000055 RID: 85
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxStorageShutoff = new ExEventLog.EventTuple(2147754320U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000056 RID: 86
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxOverDumpsterQuota = new ExEventLog.EventTuple(2147755815U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000057 RID: 87
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxOverDumpsterWarningQuota = new ExEventLog.EventTuple(2147755816U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000058 RID: 88
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ArchiveStorageOverWarningLimit = new ExEventLog.EventTuple(2147755817U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000059 RID: 89
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ArchiveStorageShutoff = new ExEventLog.EventTuple(2147754330U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005A RID: 90
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ArchiveOverDumpsterQuota = new ExEventLog.EventTuple(3221497642U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005B RID: 91
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ArchiveOverDumpsterWarningQuota = new ExEventLog.EventTuple(3221497643U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005C RID: 92
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToSendQuotaWarning = new ExEventLog.EventTuple(3221497644U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005D RID: 93
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MaxObjectsExceeded = new ExEventLog.EventTuple(3221497262U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400005E RID: 94
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MaintenanceProcessorIsIdle = new ExEventLog.EventTuple(2147755821U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005F RID: 95
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FolderStorageOverWarningLimit = new ExEventLog.EventTuple(2147755823U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000060 RID: 96
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FolderStorageShutoff = new ExEventLog.EventTuple(2147755824U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000061 RID: 97
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToSendPublicFolderQuotaWarning = new ExEventLog.EventTuple(3221497649U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000062 RID: 98
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToSendPerFolderQuotaWarning = new ExEventLog.EventTuple(3221497650U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000063 RID: 99
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NamedPropsQuotaError = new ExEventLog.EventTuple(3221497652U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000064 RID: 100
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxMessagesPerFolderCountWarningQuota = new ExEventLog.EventTuple(2147755829U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000065 RID: 101
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxMessagesPerFolderCountReceiveQuota = new ExEventLog.EventTuple(2147755830U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000066 RID: 102
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DumpsterMessagesPerFolderCountWarningQuota = new ExEventLog.EventTuple(2147755831U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000067 RID: 103
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DumpsterMessagesPerFolderCountReceiveQuota = new ExEventLog.EventTuple(2147755832U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000068 RID: 104
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FolderHierarchyChildrenCountWarningQuota = new ExEventLog.EventTuple(2147755833U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000069 RID: 105
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FolderHierarchyChildrenCountReceiveQuota = new ExEventLog.EventTuple(2147755834U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006A RID: 106
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FolderHierarchyDepthWarningQuota = new ExEventLog.EventTuple(2147755835U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006B RID: 107
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FolderHierarchyDepthReceiveQuota = new ExEventLog.EventTuple(2147755836U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006C RID: 108
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FoldersCountWarningQuota = new ExEventLog.EventTuple(2147755837U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006D RID: 109
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FoldersCountReceiveQuota = new ExEventLog.EventTuple(2147755838U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006E RID: 110
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LastLogUpdateFailed = new ExEventLog.EventTuple(3221527616U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400006F RID: 111
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LastLogWriterHung = new ExEventLog.EventTuple(3221527617U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000070 RID: 112
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TooManyRecoveryMDBsMounted = new ExEventLog.EventTuple(3221265474U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000071 RID: 113
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TooManyMDBsMounted = new ExEventLog.EventTuple(3221265475U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000072 RID: 114
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TooManyActiveMDBsMounted = new ExEventLog.EventTuple(3221265476U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000073 RID: 115
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TooManyDBsConfigured = new ExEventLog.EventTuple(3221265477U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000074 RID: 116
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NoDatabase = new ExEventLog.EventTuple(3221265478U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000075 RID: 117
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SetColumnsHasNonPromotedProperties = new ExEventLog.EventTuple(2147523655U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000076 RID: 118
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MountCompleted = new ExEventLog.EventTuple(1073781832U, 7, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000077 RID: 119
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DismountCompleted = new ExEventLog.EventTuple(1073781833U, 7, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000078 RID: 120
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ApproachingMaxDbSize = new ExEventLog.EventTuple(2147785802U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000079 RID: 121
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MaxDbSizeExceededDismountForced = new ExEventLog.EventTuple(3221527627U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400007A RID: 122
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseLogicalCorruption = new ExEventLog.EventTuple(3221527628U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400007B RID: 123
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UrgentTombstoneCleanupDispatched = new ExEventLog.EventTuple(2147785805U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400007C RID: 124
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LastLogUpdateTooAdvanced = new ExEventLog.EventTuple(2147785806U, 7, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400007D RID: 125
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LastLogUpdateLaggingBehind = new ExEventLog.EventTuple(2147785807U, 7, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400007E RID: 126
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NoActiveDatabase = new ExEventLog.EventTuple(3221265488U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400007F RID: 127
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UrgentTombstoneCleanupSummary = new ExEventLog.EventTuple(2147785809U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000080 RID: 128
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerMountCompleted = new ExEventLog.EventTuple(1073781842U, 7, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000081 RID: 129
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PassiveDatabaseAttachedReadOnly = new ExEventLog.EventTuple(1073781843U, 7, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000082 RID: 130
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PassiveDatabaseDetached = new ExEventLog.EventTuple(1073781844U, 7, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000083 RID: 131
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PassiveDatabaseAttachDetachException = new ExEventLog.EventTuple(3221265493U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000084 RID: 132
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedFinishLogReplayForTransitionToActive = new ExEventLog.EventTuple(3221265494U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000085 RID: 133
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RunningWithRepairedDatabase = new ExEventLog.EventTuple(3221265497U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000003 RID: 3
		private enum Category : short
		{
			// Token: 0x04000087 RID: 135
			General = 1,
			// Token: 0x04000088 RID: 136
			Physical_Access,
			// Token: 0x04000089 RID: 137
			Lazy_Indexing,
			// Token: 0x0400008A RID: 138
			Logical_Data_Model,
			// Token: 0x0400008B RID: 139
			Directory_Services,
			// Token: 0x0400008C RID: 140
			MAPI,
			// Token: 0x0400008D RID: 141
			High_Availability
		}

		// Token: 0x02000004 RID: 4
		internal enum Message : uint
		{
			// Token: 0x0400008F RID: 143
			InternalLogicError = 3221488617U,
			// Token: 0x04000090 RID: 144
			UnhandledException,
			// Token: 0x04000091 RID: 145
			LeakedException,
			// Token: 0x04000092 RID: 146
			DuplicateRpcEndpoint,
			// Token: 0x04000093 RID: 147
			DuplicateRpcMTEndpoint,
			// Token: 0x04000094 RID: 148
			DuplicatePoolRpcEndpoint,
			// Token: 0x04000095 RID: 149
			DuplicateAdminRpcEndpoint,
			// Token: 0x04000096 RID: 150
			MailboxRetention = 1074004976U,
			// Token: 0x04000097 RID: 151
			MailboxPurged,
			// Token: 0x04000098 RID: 152
			MailboxReconnected,
			// Token: 0x04000099 RID: 153
			DatabaseBadVersion = 3221488627U,
			// Token: 0x0400009A RID: 154
			FullTextIndexException,
			// Token: 0x0400009B RID: 155
			MailboxPrequarantined,
			// Token: 0x0400009C RID: 156
			MailboxQuarantined = 2147746806U,
			// Token: 0x0400009D RID: 157
			MailboxUnquarantined = 1074004983U,
			// Token: 0x0400009E RID: 158
			NamedPropertyMappingChange = 3221488632U,
			// Token: 0x0400009F RID: 159
			RepidGuidMappingChange,
			// Token: 0x040000A0 RID: 160
			ServiceProcessTerminated,
			// Token: 0x040000A1 RID: 161
			StartupFailureDueToADError,
			// Token: 0x040000A2 RID: 162
			WorkerProcessStarted = 1074004988U,
			// Token: 0x040000A3 RID: 163
			WorkerProcessStopped,
			// Token: 0x040000A4 RID: 164
			WorkerProcessFailedToStart = 3221488638U,
			// Token: 0x040000A5 RID: 165
			ServiceInitializationFailure,
			// Token: 0x040000A6 RID: 166
			MailboxLocaleIdChanged = 1074004992U,
			// Token: 0x040000A7 RID: 167
			MailboxGlobcntRolledOver,
			// Token: 0x040000A8 RID: 168
			MailboxReplidExhaustionApproaching = 2147746818U,
			// Token: 0x040000A9 RID: 169
			MailboxReplidExhaustion = 3221488643U,
			// Token: 0x040000AA RID: 170
			TraceLoggerStarted = 1074004996U,
			// Token: 0x040000AB RID: 171
			TraceLoggerStopped,
			// Token: 0x040000AC RID: 172
			TraceLoggerFailed = 2147746822U,
			// Token: 0x040000AD RID: 173
			WorkerProcessStopTimeout = 3221488647U,
			// Token: 0x040000AE RID: 174
			StartTraceSessionFailed,
			// Token: 0x040000AF RID: 175
			TaskRequestFailed = 1074005001U,
			// Token: 0x040000B0 RID: 176
			TaskNonFatalDBException = 3221488650U,
			// Token: 0x040000B1 RID: 177
			TaskFatalDBException,
			// Token: 0x040000B2 RID: 178
			DatabaseVersionTooOld,
			// Token: 0x040000B3 RID: 179
			DatabaseVersionTooNew,
			// Token: 0x040000B4 RID: 180
			DatabaseBadRequestedUpdgradeVersion,
			// Token: 0x040000B5 RID: 181
			SchemaUpgradeFailure,
			// Token: 0x040000B6 RID: 182
			SchemaUpgradeCommencing = 1074005008U,
			// Token: 0x040000B7 RID: 183
			SchemaUpgradeComplete,
			// Token: 0x040000B8 RID: 184
			SchemaNoUpgradeFromCurrentVersionFailure = 3221488658U,
			// Token: 0x040000B9 RID: 185
			SchemaVersionPublicationComplete = 1074005011U,
			// Token: 0x040000BA RID: 186
			SchemaVersionPublicationFailure = 2147746836U,
			// Token: 0x040000BB RID: 187
			InvalidAppConfig = 3221488661U,
			// Token: 0x040000BC RID: 188
			CriticalBlockFailure,
			// Token: 0x040000BD RID: 189
			DatabaseGuidPatchRequired = 1074005015U,
			// Token: 0x040000BE RID: 190
			DatabaseFileRestoreNotAllowed = 3221488664U,
			// Token: 0x040000BF RID: 191
			TraceMaintenanceFailed = 2147746841U,
			// Token: 0x040000C0 RID: 192
			InvalidMailboxGlobcntAllocation = 3221488666U,
			// Token: 0x040000C1 RID: 193
			MountedStoreNotInActiveDirectory,
			// Token: 0x040000C2 RID: 194
			TraceMessageFailed,
			// Token: 0x040000C3 RID: 195
			CannotDetectEnvironment,
			// Token: 0x040000C4 RID: 196
			SchemaUpgradeQuarantined = 1074005022U,
			// Token: 0x040000C5 RID: 197
			SQLExceptionDetected = 3221489617U,
			// Token: 0x040000C6 RID: 198
			IOExceptionDetected,
			// Token: 0x040000C7 RID: 199
			FileDeleted = 2147747795U,
			// Token: 0x040000C8 RID: 200
			DirectoryDeleted,
			// Token: 0x040000C9 RID: 201
			SQLExtremeLatency = 3221489621U,
			// Token: 0x040000CA RID: 202
			JetExceptionDetected,
			// Token: 0x040000CB RID: 203
			TableOperatorCommitTransaction = 2147485655U,
			// Token: 0x040000CC RID: 204
			JetFatalDatabaseException = 3221489624U,
			// Token: 0x040000CD RID: 205
			InvalidSubmitTime = 2147749793U,
			// Token: 0x040000CE RID: 206
			PropertyPromotion = 1074007970U,
			// Token: 0x040000CF RID: 207
			IntegrityCheckStart,
			// Token: 0x040000D0 RID: 208
			IntegrityCheckEnd,
			// Token: 0x040000D1 RID: 209
			IntegrityCheckFailed = 3221491621U,
			// Token: 0x040000D2 RID: 210
			IntegrityCheckMailboxStart = 1074007974U,
			// Token: 0x040000D3 RID: 211
			IntegrityCheckCorruptionDetected = 2147749799U,
			// Token: 0x040000D4 RID: 212
			IntegrityCheckMailboxEnd = 1074007976U,
			// Token: 0x040000D5 RID: 213
			IntegrityCheckMailboxFailed = 3221491625U,
			// Token: 0x040000D6 RID: 214
			PossibleDuplicateMID = 3221229482U,
			// Token: 0x040000D7 RID: 215
			CorruptSearchBacklink,
			// Token: 0x040000D8 RID: 216
			DatabaseMaintenancePreemptedByDbEngineBusy = 2147487660U,
			// Token: 0x040000D9 RID: 217
			MailboxMaintenancePreemptedByDbEngineBusy,
			// Token: 0x040000DA RID: 218
			IntegrityCheckConfigurationSkippedEntry = 3221491630U,
			// Token: 0x040000DB RID: 219
			IntegrityCheckScheduledNewCorruption = 2147749807U,
			// Token: 0x040000DC RID: 220
			CorruptDirectoryObjectDetected = 3221492617U,
			// Token: 0x040000DD RID: 221
			UnsupportedRecipientTypeDetected,
			// Token: 0x040000DE RID: 222
			PermanentADError,
			// Token: 0x040000DF RID: 223
			TransientADError,
			// Token: 0x040000E0 RID: 224
			MailboxStorageOverWarningLimit = 2147746869U,
			// Token: 0x040000E1 RID: 225
			MailboxStorageOverQuotaLimit,
			// Token: 0x040000E2 RID: 226
			MailboxStorageShutoff = 2147754320U,
			// Token: 0x040000E3 RID: 227
			MailboxOverDumpsterQuota = 2147755815U,
			// Token: 0x040000E4 RID: 228
			MailboxOverDumpsterWarningQuota,
			// Token: 0x040000E5 RID: 229
			ArchiveStorageOverWarningLimit,
			// Token: 0x040000E6 RID: 230
			ArchiveStorageShutoff = 2147754330U,
			// Token: 0x040000E7 RID: 231
			ArchiveOverDumpsterQuota = 3221497642U,
			// Token: 0x040000E8 RID: 232
			ArchiveOverDumpsterWarningQuota,
			// Token: 0x040000E9 RID: 233
			FailedToSendQuotaWarning,
			// Token: 0x040000EA RID: 234
			MaxObjectsExceeded = 3221497262U,
			// Token: 0x040000EB RID: 235
			MaintenanceProcessorIsIdle = 2147755821U,
			// Token: 0x040000EC RID: 236
			FolderStorageOverWarningLimit = 2147755823U,
			// Token: 0x040000ED RID: 237
			FolderStorageShutoff,
			// Token: 0x040000EE RID: 238
			FailedToSendPublicFolderQuotaWarning = 3221497649U,
			// Token: 0x040000EF RID: 239
			FailedToSendPerFolderQuotaWarning,
			// Token: 0x040000F0 RID: 240
			NamedPropsQuotaError = 3221497652U,
			// Token: 0x040000F1 RID: 241
			MailboxMessagesPerFolderCountWarningQuota = 2147755829U,
			// Token: 0x040000F2 RID: 242
			MailboxMessagesPerFolderCountReceiveQuota,
			// Token: 0x040000F3 RID: 243
			DumpsterMessagesPerFolderCountWarningQuota,
			// Token: 0x040000F4 RID: 244
			DumpsterMessagesPerFolderCountReceiveQuota,
			// Token: 0x040000F5 RID: 245
			FolderHierarchyChildrenCountWarningQuota,
			// Token: 0x040000F6 RID: 246
			FolderHierarchyChildrenCountReceiveQuota,
			// Token: 0x040000F7 RID: 247
			FolderHierarchyDepthWarningQuota,
			// Token: 0x040000F8 RID: 248
			FolderHierarchyDepthReceiveQuota,
			// Token: 0x040000F9 RID: 249
			FoldersCountWarningQuota,
			// Token: 0x040000FA RID: 250
			FoldersCountReceiveQuota,
			// Token: 0x040000FB RID: 251
			LastLogUpdateFailed = 3221527616U,
			// Token: 0x040000FC RID: 252
			LastLogWriterHung,
			// Token: 0x040000FD RID: 253
			TooManyRecoveryMDBsMounted = 3221265474U,
			// Token: 0x040000FE RID: 254
			TooManyMDBsMounted,
			// Token: 0x040000FF RID: 255
			TooManyActiveMDBsMounted,
			// Token: 0x04000100 RID: 256
			TooManyDBsConfigured,
			// Token: 0x04000101 RID: 257
			NoDatabase,
			// Token: 0x04000102 RID: 258
			SetColumnsHasNonPromotedProperties = 2147523655U,
			// Token: 0x04000103 RID: 259
			MountCompleted = 1073781832U,
			// Token: 0x04000104 RID: 260
			DismountCompleted,
			// Token: 0x04000105 RID: 261
			ApproachingMaxDbSize = 2147785802U,
			// Token: 0x04000106 RID: 262
			MaxDbSizeExceededDismountForced = 3221527627U,
			// Token: 0x04000107 RID: 263
			DatabaseLogicalCorruption,
			// Token: 0x04000108 RID: 264
			UrgentTombstoneCleanupDispatched = 2147785805U,
			// Token: 0x04000109 RID: 265
			LastLogUpdateTooAdvanced,
			// Token: 0x0400010A RID: 266
			LastLogUpdateLaggingBehind,
			// Token: 0x0400010B RID: 267
			NoActiveDatabase = 3221265488U,
			// Token: 0x0400010C RID: 268
			UrgentTombstoneCleanupSummary = 2147785809U,
			// Token: 0x0400010D RID: 269
			WorkerMountCompleted = 1073781842U,
			// Token: 0x0400010E RID: 270
			PassiveDatabaseAttachedReadOnly,
			// Token: 0x0400010F RID: 271
			PassiveDatabaseDetached,
			// Token: 0x04000110 RID: 272
			PassiveDatabaseAttachDetachException = 3221265493U,
			// Token: 0x04000111 RID: 273
			FailedFinishLogReplayForTransitionToActive,
			// Token: 0x04000112 RID: 274
			RunningWithRepairedDatabase = 3221265497U
		}
	}
}
