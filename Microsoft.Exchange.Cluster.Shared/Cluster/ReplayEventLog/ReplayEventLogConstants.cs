using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.ReplayEventLog
{
	// Token: 0x020000AF RID: 175
	public static class ReplayEventLogConstants
	{
		// Token: 0x0400035B RID: 859
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStarted = new ExEventLog.EventTuple(1074005969U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400035C RID: 860
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStopped = new ExEventLog.EventTuple(1074005970U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400035D RID: 861
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AllFacilitiesAreOnline = new ExEventLog.EventTuple(1074005971U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400035E RID: 862
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStarting = new ExEventLog.EventTuple(1074005972U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400035F RID: 863
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStopping = new ExEventLog.EventTuple(1074005973U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000360 RID: 864
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SourceInstanceStart = new ExEventLog.EventTuple(1074005976U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000361 RID: 865
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SourceInstanceStop = new ExEventLog.EventTuple(1074005977U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000362 RID: 866
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TargetInstanceStart = new ExEventLog.EventTuple(1074005978U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000363 RID: 867
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TargetInstanceStop = new ExEventLog.EventTuple(1074005979U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000364 RID: 868
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InstanceBroken = new ExEventLog.EventTuple(3221489628U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000365 RID: 869
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_E00LogMoved = new ExEventLog.EventTuple(1074005981U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000366 RID: 870
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CheckpointDeleted = new ExEventLog.EventTuple(1074005982U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000367 RID: 871
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VssInitFailed = new ExEventLog.EventTuple(3221489633U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000368 RID: 872
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterInitialize = new ExEventLog.EventTuple(1074005986U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000369 RID: 873
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterTerminate = new ExEventLog.EventTuple(1074005987U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400036A RID: 874
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterMetadata = new ExEventLog.EventTuple(1074005989U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400036B RID: 875
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterMetadataError = new ExEventLog.EventTuple(3221489638U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400036C RID: 876
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterBackup = new ExEventLog.EventTuple(1074005991U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400036D RID: 877
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterBackupError = new ExEventLog.EventTuple(3221489640U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400036E RID: 878
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterSnapshot = new ExEventLog.EventTuple(1074005993U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400036F RID: 879
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterSnapshotError = new ExEventLog.EventTuple(3221489642U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000370 RID: 880
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterFreeze = new ExEventLog.EventTuple(1074005995U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000371 RID: 881
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterFreezeError = new ExEventLog.EventTuple(3221489644U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000372 RID: 882
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterThaw = new ExEventLog.EventTuple(1074005997U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000373 RID: 883
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterThawError = new ExEventLog.EventTuple(3221489646U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000374 RID: 884
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterAbort = new ExEventLog.EventTuple(1074005999U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000375 RID: 885
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterAbortError = new ExEventLog.EventTuple(3221489648U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000376 RID: 886
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterBackupComplete = new ExEventLog.EventTuple(1074006001U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000377 RID: 887
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterBackupCompleteError = new ExEventLog.EventTuple(3221489650U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000378 RID: 888
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterPostSnapshot = new ExEventLog.EventTuple(1074006003U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000379 RID: 889
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterOnBackupShutdownError = new ExEventLog.EventTuple(3221489652U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400037A RID: 890
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterOnBackupShutdown = new ExEventLog.EventTuple(1074006005U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400037B RID: 891
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterBackupCompleteFailureWarning = new ExEventLog.EventTuple(2147747830U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400037C RID: 892
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterPostSnapshotError = new ExEventLog.EventTuple(3221489655U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400037D RID: 893
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterAddComponentsError = new ExEventLog.EventTuple(3221489656U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400037E RID: 894
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterAddDatabaseComponentError = new ExEventLog.EventTuple(3221489657U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400037F RID: 895
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterDbFileInfoError = new ExEventLog.EventTuple(3221489658U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000380 RID: 896
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterSetPrivateMetadataError = new ExEventLog.EventTuple(3221489659U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000381 RID: 897
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterCheckInstanceVolumeDependenciesError = new ExEventLog.EventTuple(3221489660U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000382 RID: 898
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterCheckDatabaseVolumeDependenciesError = new ExEventLog.EventTuple(3221489661U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000383 RID: 899
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterBackupCompleteLogsTruncated = new ExEventLog.EventTuple(1074006014U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000384 RID: 900
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterBackupCompleteNoTruncateRequested = new ExEventLog.EventTuple(1074006015U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000385 RID: 901
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterBackupCompleteWithFailure = new ExEventLog.EventTuple(3221489664U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000386 RID: 902
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterBackupCompleteUnknownGuid = new ExEventLog.EventTuple(3221489665U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000387 RID: 903
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterBackupCompleteWithFailureAndUnknownGuid = new ExEventLog.EventTuple(3221489666U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000388 RID: 904
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterRestoreFailed = new ExEventLog.EventTuple(3221489667U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000389 RID: 905
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSSnapshotWriter = new ExEventLog.EventTuple(1074006020U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400038A RID: 906
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OnlineDatabaseFailed = new ExEventLog.EventTuple(3221489669U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400038B RID: 907
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToCreateTempLogFile = new ExEventLog.EventTuple(3221489671U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400038C RID: 908
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ReseedRequired = new ExEventLog.EventTuple(3221489672U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400038D RID: 909
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_IncrementalReseedBlocked = new ExEventLog.EventTuple(3221489673U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400038E RID: 910
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_IncrementalReseedError = new ExEventLog.EventTuple(3221489674U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400038F RID: 911
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LogFileGapFound = new ExEventLog.EventTuple(3221489675U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000390 RID: 912
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigurationCheckerFailedTransient = new ExEventLog.EventTuple(3221489676U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000391 RID: 913
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AttemptCopyLastLogsFailed = new ExEventLog.EventTuple(3221489677U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000392 RID: 914
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NoDatabasesInReplica = new ExEventLog.EventTuple(3221489678U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000393 RID: 915
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidFilePath = new ExEventLog.EventTuple(3221489682U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000394 RID: 916
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LogFileCorruptError = new ExEventLog.EventTuple(3221489683U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000395 RID: 917
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LogInspectorFailed = new ExEventLog.EventTuple(3221489684U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000396 RID: 918
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CouldNotDeleteLogFile = new ExEventLog.EventTuple(3221489685U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000397 RID: 919
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FileCheckError = new ExEventLog.EventTuple(3221489686U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000398 RID: 920
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayCheckError = new ExEventLog.EventTuple(3221489687U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000399 RID: 921
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToCreateDirectory = new ExEventLog.EventTuple(3221489689U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400039A RID: 922
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NoDirectory = new ExEventLog.EventTuple(3221489690U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400039B RID: 923
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FilesystemCorrupt = new ExEventLog.EventTuple(3221489691U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400039C RID: 924
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SavedStateError = new ExEventLog.EventTuple(3221489693U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400039D RID: 925
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CouldNotCompareLogFiles = new ExEventLog.EventTuple(3221489697U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400039E RID: 926
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CouldNotCreateNetworkShare = new ExEventLog.EventTuple(3221489698U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400039F RID: 927
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RestoreDatabaseCopySuccessful = new ExEventLog.EventTuple(1074006053U, 5, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003A0 RID: 928
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RestoreDatabaseCopySuccessfulPathsChanged = new ExEventLog.EventTuple(1074006054U, 5, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003A1 RID: 929
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RestoreDatabaseCopyIncomplete = new ExEventLog.EventTuple(1074006055U, 5, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003A2 RID: 930
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RestoreDatabaseCopyIncompletePathsChanged = new ExEventLog.EventTuple(1074006056U, 5, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003A3 RID: 931
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RestoreDatabaseCopyFailed = new ExEventLog.EventTuple(1074006057U, 5, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003A4 RID: 932
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AutoMountReportNoLoss = new ExEventLog.EventTuple(1074006058U, 5, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003A5 RID: 933
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AutoMountReportMountWithDataLoss = new ExEventLog.EventTuple(2147747883U, 5, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003A6 RID: 934
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AutoMountReportMountNotAllowed = new ExEventLog.EventTuple(3221489708U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003A7 RID: 935
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AutoMountReportMountAfter = new ExEventLog.EventTuple(2147747885U, 5, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003A8 RID: 936
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AutoMountReportPublicFolderMountNotAllowed = new ExEventLog.EventTuple(2147747886U, 5, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003A9 RID: 937
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CouldNotReplayLogFile = new ExEventLog.EventTuple(3221489711U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003AA RID: 938
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ReseedCheckMissingLogfile = new ExEventLog.EventTuple(3221489712U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003AB RID: 939
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_IsamException = new ExEventLog.EventTuple(3221489713U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003AC RID: 940
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorCouldNotRecopyLogFile = new ExEventLog.EventTuple(3221489714U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003AD RID: 941
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseSubmitDumpsterMessages = new ExEventLog.EventTuple(1074006067U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003AE RID: 942
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReseederDeletedCheckpointFile = new ExEventLog.EventTuple(1074006068U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003AF RID: 943
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigurationCheckerFailedADError = new ExEventLog.EventTuple(3221489717U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003B0 RID: 944
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReseederDeletedTargetDatabaseFile = new ExEventLog.EventTuple(1074006070U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003B1 RID: 945
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ShipLogFailed = new ExEventLog.EventTuple(3221489720U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003B2 RID: 946
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReseederDeletedExistingLogs = new ExEventLog.EventTuple(1074006073U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003B3 RID: 947
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseNotPresentAfterReplay = new ExEventLog.EventTuple(3221489722U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003B4 RID: 948
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SeederPerfCountersLoadFailure = new ExEventLog.EventTuple(3221489725U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003B5 RID: 949
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterBackupDatabaseFullCopy = new ExEventLog.EventTuple(1074006078U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003B6 RID: 950
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterBackupDatabaseIncrementalDifferential = new ExEventLog.EventTuple(1074006079U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003B7 RID: 951
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterBackupDatabaseError = new ExEventLog.EventTuple(3221489728U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003B8 RID: 952
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseDumpsterRedeliveryRequired = new ExEventLog.EventTuple(1074006081U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003B9 RID: 953
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplicaInstanceLogCopied = new ExEventLog.EventTuple(1074006082U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003BA RID: 954
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplicaInstanceLogsReplayed = new ExEventLog.EventTuple(1074006083U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003BB RID: 955
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplicaInstanceStartIncrementalReseed = new ExEventLog.EventTuple(1074006084U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003BC RID: 956
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplicaInstanceFinishIncrementalReseed = new ExEventLog.EventTuple(1074006085U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003BD RID: 957
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterOrphanedBackupInstance = new ExEventLog.EventTuple(2147747910U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003BE RID: 958
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterMissingFile = new ExEventLog.EventTuple(3221489735U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003BF RID: 959
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HTTPListenerFailedToStart = new ExEventLog.EventTuple(3221489736U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003C0 RID: 960
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TcpListenerFailedToStart = new ExEventLog.EventTuple(3221489737U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003C1 RID: 961
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ScrConfigPathConflict = new ExEventLog.EventTuple(3221489741U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003C2 RID: 962
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MultiplePathNext = new ExEventLog.EventTuple(1074006094U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003C3 RID: 963
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NetworkPathNext = new ExEventLog.EventTuple(1074006095U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003C4 RID: 964
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogCopyFailedDueToDuplicateName = new ExEventLog.EventTuple(3221489744U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003C5 RID: 965
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ScrConfigExceedLimit = new ExEventLog.EventTuple(2147747921U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003C6 RID: 966
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ScrConfigConflictWithDb = new ExEventLog.EventTuple(2147747922U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003C7 RID: 967
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSReplicaBroken = new ExEventLog.EventTuple(3221489747U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003C8 RID: 968
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSReplicaSuspend = new ExEventLog.EventTuple(3221489748U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003C9 RID: 969
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RpcServerStarted = new ExEventLog.EventTuple(1074006101U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003CA RID: 970
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RpcServerStopped = new ExEventLog.EventTuple(1074006102U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003CB RID: 971
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RpcServerFailedToStart = new ExEventLog.EventTuple(3221489751U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003CC RID: 972
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LogTruncationOpenFailed = new ExEventLog.EventTuple(2147747928U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003CD RID: 973
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogTruncationSourceFailure = new ExEventLog.EventTuple(2147747929U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003CE RID: 974
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogTruncationLocalFailure = new ExEventLog.EventTuple(2147747930U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003CF RID: 975
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterException = new ExEventLog.EventTuple(3221489756U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003D0 RID: 976
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RpcServerFailedToFindExchangeServersUsg = new ExEventLog.EventTuple(3221489757U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003D1 RID: 977
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InstanceFailedToStart = new ExEventLog.EventTuple(3221489758U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003D2 RID: 978
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InstanceFailedToDeleteRegistryStateWarning = new ExEventLog.EventTuple(2147747935U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003D3 RID: 979
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NoDirectoryHostInaccessible = new ExEventLog.EventTuple(3221489761U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003D4 RID: 980
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ExceptionDuringCallback = new ExEventLog.EventTuple(3221489762U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003D5 RID: 981
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AlternateNetworkHadProblem = new ExEventLog.EventTuple(3221489763U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003D6 RID: 982
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LogCopierFoundNoLogsOnSource = new ExEventLog.EventTuple(3221489766U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003D7 RID: 983
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LogCopierFailedDueToSource = new ExEventLog.EventTuple(3221489767U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003D8 RID: 984
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LogCopierFailedDueToTarget = new ExEventLog.EventTuple(3221489768U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003D9 RID: 985
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LogCopierFailedToCommunicate = new ExEventLog.EventTuple(3221489769U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003DA RID: 986
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TPRExchangeListenerStarted = new ExEventLog.EventTuple(1074006122U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003DB RID: 987
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TPRManagerInitFailure = new ExEventLog.EventTuple(3221489771U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003DC RID: 988
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LogCopierErrorOnSource = new ExEventLog.EventTuple(3221489772U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003DD RID: 989
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplicaInstanceMadeProgress = new ExEventLog.EventTuple(1074006125U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003DE RID: 990
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LogCopierErrorOnSourceTriggerFailover = new ExEventLog.EventTuple(3221489774U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003DF RID: 991
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LogCopierIsStalledDueToSource = new ExEventLog.EventTuple(3221489775U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003E0 RID: 992
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CorruptLogRecoveryIsAttempted = new ExEventLog.EventTuple(3221489776U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003E1 RID: 993
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CorruptLogRecoveryFailedToSuspend = new ExEventLog.EventTuple(3221489777U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003E2 RID: 994
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CorruptLogRecoveryFailedToDismount = new ExEventLog.EventTuple(3221489778U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003E3 RID: 995
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LogCopierReceivedSourceSideError = new ExEventLog.EventTuple(3221489779U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003E4 RID: 996
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LogCopierBlockedByFullDisk = new ExEventLog.EventTuple(3221489780U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003E5 RID: 997
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LogCorruptionTriggersFailover = new ExEventLog.EventTuple(3221489781U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003E6 RID: 998
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LogCopierDisconnectedTooLong = new ExEventLog.EventTuple(3221489782U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003E7 RID: 999
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CorruptLogDetectedOnActive = new ExEventLog.EventTuple(3221489783U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003E8 RID: 1000
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorReadingLogOnActive = new ExEventLog.EventTuple(3221489784U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003E9 RID: 1001
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CorruptLogRepaired = new ExEventLog.EventTuple(2147747961U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003EA RID: 1002
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SlowIoDetected = new ExEventLog.EventTuple(2147747962U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003EB RID: 1003
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CorruptLogRecoveryIsImmediatelyAttempted = new ExEventLog.EventTuple(3221489787U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003EC RID: 1004
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ResumeFailedDuringFailureItemProcessing = new ExEventLog.EventTuple(3221489788U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003ED RID: 1005
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InspectorFixedCorruptLog = new ExEventLog.EventTuple(2147747965U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003EE RID: 1006
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LogCopierDetectsPossibleLogStreamReset = new ExEventLog.EventTuple(2147747966U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003EF RID: 1007
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LogCopierFailedToTransitOutOfBlockMode = new ExEventLog.EventTuple(2147747967U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003F0 RID: 1008
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InspectorDetectedCorruptLog = new ExEventLog.EventTuple(3221489792U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003F1 RID: 1009
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FatalIOErrorEncountered = new ExEventLog.EventTuple(3221489793U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003F2 RID: 1010
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PassiveMonitoredDBFailedToStart = new ExEventLog.EventTuple(3221489794U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003F3 RID: 1011
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ActiveMonitoredDBFailedToStart = new ExEventLog.EventTuple(3221489795U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003F4 RID: 1012
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSWriterMissingLogFileSignature = new ExEventLog.EventTuple(3221489796U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003F5 RID: 1013
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NetworkRoleChanged = new ExEventLog.EventTuple(2147747992U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003F6 RID: 1014
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RegistryReplicatorException = new ExEventLog.EventTuple(3221489817U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040003F7 RID: 1015
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ClusterApiHungAlert = new ExEventLog.EventTuple(2147748174U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003F8 RID: 1016
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_IncrementalReseedInitException = new ExEventLog.EventTuple(3221490760U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003F9 RID: 1017
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_IncrementalReseedFailedError = new ExEventLog.EventTuple(3221490761U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003FA RID: 1018
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_IncrementalReseedRetryableError = new ExEventLog.EventTuple(3221490762U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003FB RID: 1019
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_IncrementalReseedPrereqError = new ExEventLog.EventTuple(3221490763U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003FC RID: 1020
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_IncSeedingStarted = new ExEventLog.EventTuple(1074007116U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003FD RID: 1021
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_IncSeedingComplete = new ExEventLog.EventTuple(1074007117U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003FE RID: 1022
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_IncSeedingSourceReleased = new ExEventLog.EventTuple(1074007118U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040003FF RID: 1023
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmChangingRole = new ExEventLog.EventTuple(1074007119U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000400 RID: 1024
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmStoreServiceStarted = new ExEventLog.EventTuple(1074007120U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000401 RID: 1025
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmInitiatingNodeFailover = new ExEventLog.EventTuple(1074007121U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000402 RID: 1026
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmDatabaseMountFailed = new ExEventLog.EventTuple(3221490770U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000403 RID: 1027
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmDatabaseMounted = new ExEventLog.EventTuple(1074007124U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000404 RID: 1028
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmDetectedNodeStateChange = new ExEventLog.EventTuple(1074007125U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000405 RID: 1029
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmStartingAutoMount = new ExEventLog.EventTuple(1074007126U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000406 RID: 1030
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmIgnoringDatabaseMount = new ExEventLog.EventTuple(1074007127U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000407 RID: 1031
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmDatabaseDismounted = new ExEventLog.EventTuple(1074007129U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000408 RID: 1032
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmKnownError = new ExEventLog.EventTuple(3221490778U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000409 RID: 1033
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmUnknownCrticalError = new ExEventLog.EventTuple(3221490779U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400040A RID: 1034
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmErrorReadingConfiguration = new ExEventLog.EventTuple(3221490780U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400040B RID: 1035
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmCriticalErrorReadingConfiguration = new ExEventLog.EventTuple(3221490781U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400040C RID: 1036
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmMoveNotApplicableForDatabase = new ExEventLog.EventTuple(2147748958U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400040D RID: 1037
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmFailedToAutomountDatabase = new ExEventLog.EventTuple(3221490756U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400040E RID: 1038
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SuspendMarkedForDatabaseCopy = new ExEventLog.EventTuple(1074007110U, 5, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400040F RID: 1039
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ResumeMarkedForDatabaseCopy = new ExEventLog.EventTuple(1074007111U, 5, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000410 RID: 1040
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MountAllowedWithMountDialOverride = new ExEventLog.EventTuple(1074007135U, 5, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000411 RID: 1041
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MountNotAllowedWithMountDialOverride = new ExEventLog.EventTuple(3221490784U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000412 RID: 1042
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmDatabaseMoved = new ExEventLog.EventTuple(1074007137U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000413 RID: 1043
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmDatabaseMoveFailed = new ExEventLog.EventTuple(3221490786U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000414 RID: 1044
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DbSeedingRequired = new ExEventLog.EventTuple(2147748963U, 5, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000415 RID: 1045
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseExistsInADButRegistryDeleted = new ExEventLog.EventTuple(3221490788U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000416 RID: 1046
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmRpcServerStarted = new ExEventLog.EventTuple(1074007141U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000417 RID: 1047
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmRpcServerStopped = new ExEventLog.EventTuple(1074007142U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000418 RID: 1048
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmRpcServerFailedToStart = new ExEventLog.EventTuple(3221490791U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000419 RID: 1049
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmRpcServerFailedToFindExchangeServersUsg = new ExEventLog.EventTuple(3221490792U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400041A RID: 1050
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceFailedToStartAMFailure = new ExEventLog.EventTuple(3221490793U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400041B RID: 1051
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmDatabaseNotMountedServersDown = new ExEventLog.EventTuple(3221490794U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400041C RID: 1052
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmForceDismountingDatabases = new ExEventLog.EventTuple(2147748971U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400041D RID: 1053
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmDatabaseAcllComplete = new ExEventLog.EventTuple(1074007150U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400041E RID: 1054
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmDatabaseAcllFailed = new ExEventLog.EventTuple(3221490799U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400041F RID: 1055
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedMovePAM = new ExEventLog.EventTuple(3221490800U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000420 RID: 1056
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SuccMovePAM = new ExEventLog.EventTuple(1074007153U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000421 RID: 1057
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AmIgnoredMapiNetFailureBecauseNodeNotUp = new ExEventLog.EventTuple(2147748978U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000422 RID: 1058
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmKilledStoreToForceDismount = new ExEventLog.EventTuple(3221490803U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000423 RID: 1059
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmFailedToStopService = new ExEventLog.EventTuple(3221490804U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000424 RID: 1060
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmFailedToStartService = new ExEventLog.EventTuple(3221490805U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000425 RID: 1061
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AMDetectedMapiNetworkFailure = new ExEventLog.EventTuple(3221490806U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000426 RID: 1062
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AmIgnoredMapiNetFailureBecauseMapiLooksUp = new ExEventLog.EventTuple(2147748983U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000427 RID: 1063
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AmIgnoredMapiNetFailureBecauseADIsWorking = new ExEventLog.EventTuple(2147748984U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000428 RID: 1064
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AmIgnoredMapiNetFailureBecauseNotThePam = new ExEventLog.EventTuple(2147748985U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000429 RID: 1065
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PauseSuccessful = new ExEventLog.EventTuple(1074007970U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400042A RID: 1066
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StopFailed = new ExEventLog.EventTuple(3221491619U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400042B RID: 1067
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StartFailed = new ExEventLog.EventTuple(3221491620U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400042C RID: 1068
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PauseFailed = new ExEventLog.EventTuple(3221491622U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400042D RID: 1069
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CommandOK = new ExEventLog.EventTuple(3221491621U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400042E RID: 1070
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CommandFailed = new ExEventLog.EventTuple(3221491623U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400042F RID: 1071
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PowerEventOK = new ExEventLog.EventTuple(3221491624U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000430 RID: 1072
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PowerEventFailed = new ExEventLog.EventTuple(3221491625U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000431 RID: 1073
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SessionChangeFailed = new ExEventLog.EventTuple(3221491626U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000432 RID: 1074
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ShutdownOK = new ExEventLog.EventTuple(3221491627U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000433 RID: 1075
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ShutdownFailed = new ExEventLog.EventTuple(3221491628U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000434 RID: 1076
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CommandSuccessful = new ExEventLog.EventTuple(3221491629U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000435 RID: 1077
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ContinueSuccessful = new ExEventLog.EventTuple(3221491630U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000436 RID: 1078
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ContinueFailed = new ExEventLog.EventTuple(3221491631U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000437 RID: 1079
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToUnloadAppDomain = new ExEventLog.EventTuple(3221491632U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000438 RID: 1080
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PreShutdownOK = new ExEventLog.EventTuple(1074007985U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000439 RID: 1081
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PreShutdownFailed = new ExEventLog.EventTuple(3221491634U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400043A RID: 1082
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PreShutdownStart = new ExEventLog.EventTuple(1074007987U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400043B RID: 1083
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SeedManagerStarted = new ExEventLog.EventTuple(1074007988U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400043C RID: 1084
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SeedManagerStopped = new ExEventLog.EventTuple(1074007989U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400043D RID: 1085
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SeedInstancePrepareAdded = new ExEventLog.EventTuple(1074007990U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400043E RID: 1086
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SeedInstancePrepareSucceeded = new ExEventLog.EventTuple(1074007991U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400043F RID: 1087
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SeedInstancePrepareUnknownError = new ExEventLog.EventTuple(3221491640U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000440 RID: 1088
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SeedInstancePrepareFailed = new ExEventLog.EventTuple(3221491641U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000441 RID: 1089
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SeedInstanceInProgressFailed = new ExEventLog.EventTuple(3221491642U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000442 RID: 1090
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SeedInstanceCancelled = new ExEventLog.EventTuple(2147749819U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000443 RID: 1091
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SeedInstanceBeginSucceeded = new ExEventLog.EventTuple(1074007996U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000444 RID: 1092
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SeedInstanceBeginUnknownError = new ExEventLog.EventTuple(3221491645U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000445 RID: 1093
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SeedInstanceCancelRequestedByAdmin = new ExEventLog.EventTuple(1074007998U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000446 RID: 1094
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SeedInstanceCleanupRequestedByAdmin = new ExEventLog.EventTuple(1074007999U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000447 RID: 1095
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SeedInstanceCleanupConfigChanged = new ExEventLog.EventTuple(1074008000U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000448 RID: 1096
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SeedInstanceCleanupStale = new ExEventLog.EventTuple(1074008001U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000449 RID: 1097
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SeedInstancesStoppedServiceShutdown = new ExEventLog.EventTuple(2147749826U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400044A RID: 1098
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SeedInstanceDeletedExistingLogs = new ExEventLog.EventTuple(1074008003U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400044B RID: 1099
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SeedInstanceDeletedCheckpointFile = new ExEventLog.EventTuple(1074008004U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400044C RID: 1100
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SeedInstanceSuccess = new ExEventLog.EventTuple(1074008005U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400044D RID: 1101
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringClusterServiceCheckFailed = new ExEventLog.EventTuple(3221491654U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400044E RID: 1102
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringClusterServiceCheckPassed = new ExEventLog.EventTuple(1074008007U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400044F RID: 1103
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringActiveManagerCheckFailed = new ExEventLog.EventTuple(3221491656U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000450 RID: 1104
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringActiveManagerCheckPassed = new ExEventLog.EventTuple(1074008009U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000451 RID: 1105
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringReplayServiceCheckFailed = new ExEventLog.EventTuple(3221491658U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000452 RID: 1106
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringReplayServiceCheckPassed = new ExEventLog.EventTuple(1074008011U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000453 RID: 1107
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringDagMembersUpCheckFailed = new ExEventLog.EventTuple(3221491660U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000454 RID: 1108
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringDagMembersUpCheckPassed = new ExEventLog.EventTuple(1074008013U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000455 RID: 1109
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringClusterNetworkCheckFailed = new ExEventLog.EventTuple(3221491662U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000456 RID: 1110
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringClusterNetworkCheckWarning = new ExEventLog.EventTuple(2147749839U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000457 RID: 1111
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringClusterNetworkCheckPassed = new ExEventLog.EventTuple(1074008016U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000458 RID: 1112
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringFileShareQuorumCheckFailed = new ExEventLog.EventTuple(3221491665U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000459 RID: 1113
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringFileShareQuorumCheckPassed = new ExEventLog.EventTuple(1074008018U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400045A RID: 1114
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringQuorumGroupCheckFailed = new ExEventLog.EventTuple(3221491667U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400045B RID: 1115
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringQuorumGroupCheckPassed = new ExEventLog.EventTuple(1074008020U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400045C RID: 1116
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringTasksRpcListenerCheckFailed = new ExEventLog.EventTuple(3221491669U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400045D RID: 1117
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringTasksRpcListenerCheckPassed = new ExEventLog.EventTuple(1074008022U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400045E RID: 1118
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringHttpListenerCheckFailed = new ExEventLog.EventTuple(3221491671U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400045F RID: 1119
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringHttpListenerCheckPassed = new ExEventLog.EventTuple(1074008024U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000460 RID: 1120
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LogReplayMapiException = new ExEventLog.EventTuple(3221491673U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000461 RID: 1121
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseOperationLockIsTakingLongTime = new ExEventLog.EventTuple(2147749850U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000462 RID: 1122
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CiSeedInstanceSuccess = new ExEventLog.EventTuple(1074008027U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000463 RID: 1123
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SourceReplicaInstanceNotStarted = new ExEventLog.EventTuple(3221491676U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000464 RID: 1124
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TargetReplicaInstanceNotStarted = new ExEventLog.EventTuple(3221491677U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000465 RID: 1125
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SubmitDumpsterMessagesFailed = new ExEventLog.EventTuple(3221491681U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000466 RID: 1126
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ClusterDatabaseWriteFailed = new ExEventLog.EventTuple(3221491682U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000467 RID: 1127
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_IncSeedingTerminated = new ExEventLog.EventTuple(2147749859U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000468 RID: 1128
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AmRoleMonitoringError = new ExEventLog.EventTuple(3221491684U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000469 RID: 1129
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AutoMountReportDbInUseOnSource = new ExEventLog.EventTuple(2147749861U, 5, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400046A RID: 1130
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AutoMountReportDbInUseAcllInProgress = new ExEventLog.EventTuple(2147749862U, 5, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400046B RID: 1131
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmDatabaseDismountFailed = new ExEventLog.EventTuple(3221491687U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400046C RID: 1132
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmForceDismountMasterMismatch = new ExEventLog.EventTuple(2147749864U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400046D RID: 1133
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmDatabaseMountFailedGeneric = new ExEventLog.EventTuple(3221491689U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400046E RID: 1134
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmDatabaseDismountFailedGeneric = new ExEventLog.EventTuple(3221491690U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400046F RID: 1135
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmDatabaseMoveFailedGeneric = new ExEventLog.EventTuple(3221491691U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000470 RID: 1136
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NetworkReplicationDisabled = new ExEventLog.EventTuple(3221491692U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000471 RID: 1137
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringTcpListenerCheckFailed = new ExEventLog.EventTuple(3221491696U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000472 RID: 1138
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringTcpListenerCheckPassed = new ExEventLog.EventTuple(1074008049U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000473 RID: 1139
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NetworkMonitoringError = new ExEventLog.EventTuple(3221491698U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000474 RID: 1140
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SeedInstanceAnotherError = new ExEventLog.EventTuple(3221491699U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000475 RID: 1141
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ForceNewLogError = new ExEventLog.EventTuple(3221491700U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000476 RID: 1142
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ReadOnePageError = new ExEventLog.EventTuple(3221491701U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000477 RID: 1143
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ReadPageSizeError = new ExEventLog.EventTuple(3221491702U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000478 RID: 1144
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmDatabaseMoveUnspecifiedServerFailed = new ExEventLog.EventTuple(3221491703U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000479 RID: 1145
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VSSReplicaCopyUnhealthy = new ExEventLog.EventTuple(3221491704U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400047A RID: 1146
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_IncrementalReseedSourceDatabaseDismounted = new ExEventLog.EventTuple(3221491705U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400047B RID: 1147
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_IncrementalReseedSourceDatabaseMountRpcError = new ExEventLog.EventTuple(3221491706U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400047C RID: 1148
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DumpsterRedeliveryFailed = new ExEventLog.EventTuple(3221491707U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400047D RID: 1149
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NodeNotInCluster = new ExEventLog.EventTuple(3221491708U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400047E RID: 1150
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayRpcServerFailedToRegister = new ExEventLog.EventTuple(3221491709U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400047F RID: 1151
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AmRpcServerFailedToRegister = new ExEventLog.EventTuple(3221491710U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000480 RID: 1152
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceFailedToStartComponentFailure = new ExEventLog.EventTuple(3221491711U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000481 RID: 1153
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToStartRetriableComponent = new ExEventLog.EventTuple(3221491712U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000482 RID: 1154
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigUpdaterScanFailed = new ExEventLog.EventTuple(3221491713U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000483 RID: 1155
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigUpdaterFailedToFindConfig = new ExEventLog.EventTuple(3221491714U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000484 RID: 1156
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringTPRListenerCheckFailed = new ExEventLog.EventTuple(3221491715U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000485 RID: 1157
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringTPRListenerCheckPassed = new ExEventLog.EventTuple(1074008068U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000486 RID: 1158
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EsebcliTooManyApplications = new ExEventLog.EventTuple(3221491717U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000487 RID: 1159
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SeedingSourceBegin = new ExEventLog.EventTuple(1074008070U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000488 RID: 1160
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SeedingSourceEnd = new ExEventLog.EventTuple(1074008071U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000489 RID: 1161
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SeedingSourceCancel = new ExEventLog.EventTuple(2147749896U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400048A RID: 1162
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CheckConnectionToStoreFailed = new ExEventLog.EventTuple(3221491721U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400048B RID: 1163
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CheckDatabaseHeaderFailed = new ExEventLog.EventTuple(3221491722U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400048C RID: 1164
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PossibleSplitBrainDetected = new ExEventLog.EventTuple(3221491723U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400048D RID: 1165
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToCleanUpSingleIncReseedFile = new ExEventLog.EventTuple(3221491724U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400048E RID: 1166
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToCleanUpFile = new ExEventLog.EventTuple(3221491725U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400048F RID: 1167
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LogReplaySuspendedDueToCopyQ = new ExEventLog.EventTuple(2147749902U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000490 RID: 1168
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LogReplayResumedDueToCopyQ = new ExEventLog.EventTuple(1074008079U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000491 RID: 1169
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SyncSuspendResumeOperationFailed = new ExEventLog.EventTuple(3221491728U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000492 RID: 1170
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringDatabaseRedundancyCheckFailed = new ExEventLog.EventTuple(3221491729U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000493 RID: 1171
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringDatabaseRedundancyCheckPassed = new ExEventLog.EventTuple(1074008082U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000494 RID: 1172
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseRedistributionReport = new ExEventLog.EventTuple(1074008083U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000495 RID: 1173
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FqdnResolutionFailure = new ExEventLog.EventTuple(3221491732U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000496 RID: 1174
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LogReplayPatchFailedIsamException = new ExEventLog.EventTuple(3221491733U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000497 RID: 1175
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RedirtyDatabaseCreateTempLog = new ExEventLog.EventTuple(3221491734U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000498 RID: 1176
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LogReplayPatchFailedPrepareException = new ExEventLog.EventTuple(3221491735U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000499 RID: 1177
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToPublishFailureItem = new ExEventLog.EventTuple(3221491736U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400049A RID: 1178
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PeriodicOperationFailedRetrievingStatuses = new ExEventLog.EventTuple(2147749913U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400049B RID: 1179
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ProcessDiagnosticsTerminatingService = new ExEventLog.EventTuple(3221491738U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400049C RID: 1180
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GetBootTimeWithWmiFailure = new ExEventLog.EventTuple(3221491739U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400049D RID: 1181
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogRepairFailedDueToRetryLimit = new ExEventLog.EventTuple(3221491740U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400049E RID: 1182
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogRepairSuccess = new ExEventLog.EventTuple(2147749917U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400049F RID: 1183
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigurationCheckerFailedGeneric = new ExEventLog.EventTuple(3221491742U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040004A0 RID: 1184
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogFileCorruptOrGapFoundOutsideRequiredRange = new ExEventLog.EventTuple(3221491743U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004A1 RID: 1185
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MovingFilesToRestartLogStream = new ExEventLog.EventTuple(2147749920U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004A2 RID: 1186
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DeletedSkippedLogsDirectory = new ExEventLog.EventTuple(2147749921U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004A3 RID: 1187
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MissingFailureItemDetected = new ExEventLog.EventTuple(3221491746U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004A4 RID: 1188
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LogReplayPatchFailedOnLaggedCopy = new ExEventLog.EventTuple(3221491747U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040004A5 RID: 1189
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringDatabaseOneDatacenterCheckFailed = new ExEventLog.EventTuple(3221491749U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004A6 RID: 1190
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ActiveSeedingSourceBegin = new ExEventLog.EventTuple(1074008102U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004A7 RID: 1191
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ActiveSeedingSourceEnd = new ExEventLog.EventTuple(1074008103U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004A8 RID: 1192
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ActiveSeedingSourceCancel = new ExEventLog.EventTuple(2147749928U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004A9 RID: 1193
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogRepairNotPossible = new ExEventLog.EventTuple(3221491753U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004AA RID: 1194
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringDatabaseRedundancyServerCheckFailed = new ExEventLog.EventTuple(3221491754U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004AB RID: 1195
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringDatabaseRedundancyServerCheckPassed = new ExEventLog.EventTuple(1074008107U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004AC RID: 1196
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DetermineWorkerProcessIdFailed = new ExEventLog.EventTuple(3221491756U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040004AD RID: 1197
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DagNetworkConfigOld = new ExEventLog.EventTuple(1074008268U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040004AE RID: 1198
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DagNetworkConfigNew = new ExEventLog.EventTuple(1074008269U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040004AF RID: 1199
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NotifyActiveSendFailed = new ExEventLog.EventTuple(3221491927U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004B0 RID: 1200
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AmCheckRemoteSiteLocalServerSiteNull = new ExEventLog.EventTuple(2147750104U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040004B1 RID: 1201
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AmCheckRemoteSiteNotFound = new ExEventLog.EventTuple(1074008281U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040004B2 RID: 1202
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AmCheckRemoteSiteAlert = new ExEventLog.EventTuple(3221491930U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040004B3 RID: 1203
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AmCheckRemoteSiteDismount = new ExEventLog.EventTuple(3221491931U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040004B4 RID: 1204
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AmCheckRemoteSiteSucceeded = new ExEventLog.EventTuple(1074008284U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040004B5 RID: 1205
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AmCheckRemoteSiteDisabled = new ExEventLog.EventTuple(2147750109U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040004B6 RID: 1206
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiskHbFailedToStartWatchdogTimer = new ExEventLog.EventTuple(3221491934U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004B7 RID: 1207
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiskHbFailedToPrepareHeartbeatFile = new ExEventLog.EventTuple(3221491935U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004B8 RID: 1208
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiskHbEncounteredUnhandledException = new ExEventLog.EventTuple(3221491936U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004B9 RID: 1209
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiskHbFailedToDetermineBytesPerSector = new ExEventLog.EventTuple(3221491937U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004BA RID: 1210
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiskHbIoWriteTestFailed = new ExEventLog.EventTuple(3221491938U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004BB RID: 1211
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiskHbIoReadTestFailed = new ExEventLog.EventTuple(3221491939U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004BC RID: 1212
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiskHbIoLatencyExceeded = new ExEventLog.EventTuple(2147750116U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004BD RID: 1213
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiskHbFailedToStart = new ExEventLog.EventTuple(3221491941U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004BE RID: 1214
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiskHbFailedToStop = new ExEventLog.EventTuple(3221491942U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004BF RID: 1215
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AmTimeStampEntryMissingInOneOrMoreServers = new ExEventLog.EventTuple(1074008295U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040004C0 RID: 1216
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AutoReseedInstanceStarted = new ExEventLog.EventTuple(1074008296U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004C1 RID: 1217
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AutoReseedPrereqFailed = new ExEventLog.EventTuple(3221491945U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004C2 RID: 1218
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AutoReseedNoSpareDisk = new ExEventLog.EventTuple(3221491946U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004C3 RID: 1219
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AutoReseedFailed = new ExEventLog.EventTuple(3221491947U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004C4 RID: 1220
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AutoReseedSuccessful = new ExEventLog.EventTuple(1074008300U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004C5 RID: 1221
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AutoReseedSpareDisksReleased = new ExEventLog.EventTuple(1074008301U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004C6 RID: 1222
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringDatabaseOneDatacenterCheckSuccess = new ExEventLog.EventTuple(1074008302U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004C7 RID: 1223
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SeedInstanceStartedSetBroken = new ExEventLog.EventTuple(3221491951U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040004C8 RID: 1224
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LogReplayGenericError = new ExEventLog.EventTuple(3221491952U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040004C9 RID: 1225
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterMissingVersionStampError = new ExEventLog.EventTuple(3221491953U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004CA RID: 1226
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterBadVersionStampError = new ExEventLog.EventTuple(3221491954U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004CB RID: 1227
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterRestoreOptionsString = new ExEventLog.EventTuple(1074008307U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004CC RID: 1228
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterDbGuidMappingMismatchError = new ExEventLog.EventTuple(3221491956U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004CD RID: 1229
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterMultipleRetargettingError = new ExEventLog.EventTuple(3221491957U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004CE RID: 1230
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterOriginalLogfilePathMismatchError = new ExEventLog.EventTuple(3221491958U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004CF RID: 1231
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterOriginalSystemPathMismatchError = new ExEventLog.EventTuple(3221491959U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004D0 RID: 1232
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterDbRetargetMismatchError = new ExEventLog.EventTuple(3221491960U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004D1 RID: 1233
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterTargetSGLookupError = new ExEventLog.EventTuple(3221491961U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004D2 RID: 1234
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterTargetSGOnline = new ExEventLog.EventTuple(3221491962U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004D3 RID: 1235
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterRestoreToOriginalSG = new ExEventLog.EventTuple(1074008315U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004D4 RID: 1236
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterRestoreToAlternateSG = new ExEventLog.EventTuple(1074008316U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004D5 RID: 1237
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterTargetLogfilePathMismatchError = new ExEventLog.EventTuple(3221491965U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004D6 RID: 1238
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterTargetSystemPathMismatchError = new ExEventLog.EventTuple(3221491966U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004D7 RID: 1239
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterTargetLogfileBaseNameMismatchError = new ExEventLog.EventTuple(3221491967U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004D8 RID: 1240
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterSGRestoreInProgressError = new ExEventLog.EventTuple(3221491968U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004D9 RID: 1241
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterCircularLogDBRestore = new ExEventLog.EventTuple(3221491969U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004DA RID: 1242
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterTargetDbMismatchError = new ExEventLog.EventTuple(3221491970U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004DB RID: 1243
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterCannotOverwriteError = new ExEventLog.EventTuple(3221491971U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004DC RID: 1244
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterFindLogFilesError = new ExEventLog.EventTuple(3221491972U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004DD RID: 1245
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterLocationRestoreInProgressError = new ExEventLog.EventTuple(3221491973U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004DE RID: 1246
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterTargetLogfilePathInUseError = new ExEventLog.EventTuple(3221491974U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004DF RID: 1247
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterRestoreEnvSGMismatchError = new ExEventLog.EventTuple(3221491975U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004E0 RID: 1248
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterRestoreEnvLogfilePathMismatchError = new ExEventLog.EventTuple(3221491976U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004E1 RID: 1249
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterRestoreEnvLogfileBaseNameMismatchError = new ExEventLog.EventTuple(3221491977U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004E2 RID: 1250
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterRestoreEnvLogfileSignatureMismatchError = new ExEventLog.EventTuple(3221491978U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004E3 RID: 1251
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterRestoreEnvSystemPathMismatchError = new ExEventLog.EventTuple(3221491979U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004E4 RID: 1252
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterRestoreEnvCircularLogEnabledError = new ExEventLog.EventTuple(3221491980U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004E5 RID: 1253
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterRestoreEnvAlreadyRecoveredError = new ExEventLog.EventTuple(3221491981U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004E6 RID: 1254
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterRenameDbError = new ExEventLog.EventTuple(3221491982U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004E7 RID: 1255
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterRecoveryAfterRestore = new ExEventLog.EventTuple(1074008335U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004E8 RID: 1256
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterAdditionalRestoresPending = new ExEventLog.EventTuple(1074008336U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004E9 RID: 1257
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterNoDatabasesToRecover = new ExEventLog.EventTuple(1074008337U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004EA RID: 1258
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterDbToRecover = new ExEventLog.EventTuple(1074008338U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004EB RID: 1259
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterChkptNotDeleted = new ExEventLog.EventTuple(3221491987U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004EC RID: 1260
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterLogsNotDeleted = new ExEventLog.EventTuple(3221491988U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004ED RID: 1261
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplayServiceVSSWriterRestoreEnvNotDeleted = new ExEventLog.EventTuple(3221491989U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004EE RID: 1262
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringDatabaseAvailabilityCheckFailed = new ExEventLog.EventTuple(3221491990U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004EF RID: 1263
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringDatabaseAvailabilityCheckPassed = new ExEventLog.EventTuple(1074008343U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004F0 RID: 1264
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringDatabaseOneDatacenterAvailableCopyCheckFailed = new ExEventLog.EventTuple(3221491992U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004F1 RID: 1265
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringDatabaseOneDatacenterAvailableCopyCheckSuccess = new ExEventLog.EventTuple(1074008345U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004F2 RID: 1266
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ServerLocatorServiceStarted = new ExEventLog.EventTuple(1074008346U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040004F3 RID: 1267
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ServerLocatorServiceStopped = new ExEventLog.EventTuple(1074008347U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040004F4 RID: 1268
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ServerLocatorServiceFailedToStart = new ExEventLog.EventTuple(3221491996U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040004F5 RID: 1269
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringServerLocatorServiceCheckFailed = new ExEventLog.EventTuple(3221491997U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004F6 RID: 1270
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringServerLocatorServiceCheckPassed = new ExEventLog.EventTuple(1074008350U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004F7 RID: 1271
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ServerLocatorServiceCommunicationChannelFaulted = new ExEventLog.EventTuple(3221491999U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040004F8 RID: 1272
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ServerLocatorServiceRestartScheduled = new ExEventLog.EventTuple(1074008352U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040004F9 RID: 1273
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseDirectoryNotUnderMountPoint = new ExEventLog.EventTuple(3221492001U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040004FA RID: 1274
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiskHbStartingWatchdogTimer = new ExEventLog.EventTuple(1074008358U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004FB RID: 1275
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiskHbStoppingWatchdogTimer = new ExEventLog.EventTuple(1074008359U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004FC RID: 1276
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiskHbFailedToStopWatchdogTimer = new ExEventLog.EventTuple(3221492008U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004FD RID: 1277
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiskHbMinimumTimeNotElaspedFromLastReboot = new ExEventLog.EventTuple(2147750185U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004FE RID: 1278
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiskHbConfigChanged = new ExEventLog.EventTuple(2147750186U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040004FF RID: 1279
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiskHbTerminatingCurrentProcess = new ExEventLog.EventTuple(3221492011U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000500 RID: 1280
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiskHbFailedToTerminateCurrentProcess = new ExEventLog.EventTuple(3221492012U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000501 RID: 1281
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiskHbTriggeringImmediateBugcheck = new ExEventLog.EventTuple(3221492013U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000502 RID: 1282
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ServerLocatorServiceStartTimeout = new ExEventLog.EventTuple(3221492014U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000503 RID: 1283
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ProcessDiagnosticsTerminatingServiceNoDump = new ExEventLog.EventTuple(3221492015U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000504 RID: 1284
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ServerLocatorServiceAnotherProcessUsingPort = new ExEventLog.EventTuple(3221492016U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000505 RID: 1285
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ServerLocatorServiceServerForDatabaseNotFoundError = new ExEventLog.EventTuple(3221492017U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000506 RID: 1286
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ServerLocatorServiceGetAllError = new ExEventLog.EventTuple(3221492018U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000507 RID: 1287
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseSuspendedDueToLowSpace = new ExEventLog.EventTuple(3221492019U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000508 RID: 1288
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ServerLocatorServiceGetServerObjectError = new ExEventLog.EventTuple(3221492020U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000509 RID: 1289
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringMonitoringServiceCheckFailed = new ExEventLog.EventTuple(3221492021U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400050A RID: 1290
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitoringMonitoringServiceCheckPassed = new ExEventLog.EventTuple(1074008374U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x020000B0 RID: 176
		private enum Category : short
		{
			// Token: 0x0400050C RID: 1292
			Service = 1,
			// Token: 0x0400050D RID: 1293
			Exchange_VSS_Writer,
			// Token: 0x0400050E RID: 1294
			Move,
			// Token: 0x0400050F RID: 1295
			Upgrade,
			// Token: 0x04000510 RID: 1296
			Action,
			// Token: 0x04000511 RID: 1297
			ExRes
		}

		// Token: 0x020000B1 RID: 177
		internal enum Message : uint
		{
			// Token: 0x04000513 RID: 1299
			ServiceStarted = 1074005969U,
			// Token: 0x04000514 RID: 1300
			ServiceStopped,
			// Token: 0x04000515 RID: 1301
			AllFacilitiesAreOnline,
			// Token: 0x04000516 RID: 1302
			ServiceStarting,
			// Token: 0x04000517 RID: 1303
			ServiceStopping,
			// Token: 0x04000518 RID: 1304
			SourceInstanceStart = 1074005976U,
			// Token: 0x04000519 RID: 1305
			SourceInstanceStop,
			// Token: 0x0400051A RID: 1306
			TargetInstanceStart,
			// Token: 0x0400051B RID: 1307
			TargetInstanceStop,
			// Token: 0x0400051C RID: 1308
			InstanceBroken = 3221489628U,
			// Token: 0x0400051D RID: 1309
			E00LogMoved = 1074005981U,
			// Token: 0x0400051E RID: 1310
			CheckpointDeleted,
			// Token: 0x0400051F RID: 1311
			VssInitFailed = 3221489633U,
			// Token: 0x04000520 RID: 1312
			VSSWriterInitialize = 1074005986U,
			// Token: 0x04000521 RID: 1313
			VSSWriterTerminate,
			// Token: 0x04000522 RID: 1314
			VSSWriterMetadata = 1074005989U,
			// Token: 0x04000523 RID: 1315
			VSSWriterMetadataError = 3221489638U,
			// Token: 0x04000524 RID: 1316
			VSSWriterBackup = 1074005991U,
			// Token: 0x04000525 RID: 1317
			VSSWriterBackupError = 3221489640U,
			// Token: 0x04000526 RID: 1318
			VSSWriterSnapshot = 1074005993U,
			// Token: 0x04000527 RID: 1319
			VSSWriterSnapshotError = 3221489642U,
			// Token: 0x04000528 RID: 1320
			VSSWriterFreeze = 1074005995U,
			// Token: 0x04000529 RID: 1321
			VSSWriterFreezeError = 3221489644U,
			// Token: 0x0400052A RID: 1322
			VSSWriterThaw = 1074005997U,
			// Token: 0x0400052B RID: 1323
			VSSWriterThawError = 3221489646U,
			// Token: 0x0400052C RID: 1324
			VSSWriterAbort = 1074005999U,
			// Token: 0x0400052D RID: 1325
			VSSWriterAbortError = 3221489648U,
			// Token: 0x0400052E RID: 1326
			VSSWriterBackupComplete = 1074006001U,
			// Token: 0x0400052F RID: 1327
			VSSWriterBackupCompleteError = 3221489650U,
			// Token: 0x04000530 RID: 1328
			VSSWriterPostSnapshot = 1074006003U,
			// Token: 0x04000531 RID: 1329
			VSSWriterOnBackupShutdownError = 3221489652U,
			// Token: 0x04000532 RID: 1330
			VSSWriterOnBackupShutdown = 1074006005U,
			// Token: 0x04000533 RID: 1331
			VSSWriterBackupCompleteFailureWarning = 2147747830U,
			// Token: 0x04000534 RID: 1332
			VSSWriterPostSnapshotError = 3221489655U,
			// Token: 0x04000535 RID: 1333
			VSSWriterAddComponentsError,
			// Token: 0x04000536 RID: 1334
			VSSWriterAddDatabaseComponentError,
			// Token: 0x04000537 RID: 1335
			VSSWriterDbFileInfoError,
			// Token: 0x04000538 RID: 1336
			VSSWriterSetPrivateMetadataError,
			// Token: 0x04000539 RID: 1337
			VSSWriterCheckInstanceVolumeDependenciesError,
			// Token: 0x0400053A RID: 1338
			VSSWriterCheckDatabaseVolumeDependenciesError,
			// Token: 0x0400053B RID: 1339
			VSSWriterBackupCompleteLogsTruncated = 1074006014U,
			// Token: 0x0400053C RID: 1340
			VSSWriterBackupCompleteNoTruncateRequested,
			// Token: 0x0400053D RID: 1341
			VSSWriterBackupCompleteWithFailure = 3221489664U,
			// Token: 0x0400053E RID: 1342
			VSSWriterBackupCompleteUnknownGuid,
			// Token: 0x0400053F RID: 1343
			VSSWriterBackupCompleteWithFailureAndUnknownGuid,
			// Token: 0x04000540 RID: 1344
			ReplayServiceVSSWriterRestoreFailed,
			// Token: 0x04000541 RID: 1345
			VSSSnapshotWriter = 1074006020U,
			// Token: 0x04000542 RID: 1346
			OnlineDatabaseFailed = 3221489669U,
			// Token: 0x04000543 RID: 1347
			FailedToCreateTempLogFile = 3221489671U,
			// Token: 0x04000544 RID: 1348
			ReseedRequired,
			// Token: 0x04000545 RID: 1349
			IncrementalReseedBlocked,
			// Token: 0x04000546 RID: 1350
			IncrementalReseedError,
			// Token: 0x04000547 RID: 1351
			LogFileGapFound,
			// Token: 0x04000548 RID: 1352
			ConfigurationCheckerFailedTransient,
			// Token: 0x04000549 RID: 1353
			AttemptCopyLastLogsFailed,
			// Token: 0x0400054A RID: 1354
			NoDatabasesInReplica,
			// Token: 0x0400054B RID: 1355
			InvalidFilePath = 3221489682U,
			// Token: 0x0400054C RID: 1356
			LogFileCorruptError,
			// Token: 0x0400054D RID: 1357
			LogInspectorFailed,
			// Token: 0x0400054E RID: 1358
			CouldNotDeleteLogFile,
			// Token: 0x0400054F RID: 1359
			FileCheckError,
			// Token: 0x04000550 RID: 1360
			ReplayCheckError,
			// Token: 0x04000551 RID: 1361
			FailedToCreateDirectory = 3221489689U,
			// Token: 0x04000552 RID: 1362
			NoDirectory,
			// Token: 0x04000553 RID: 1363
			FilesystemCorrupt,
			// Token: 0x04000554 RID: 1364
			SavedStateError = 3221489693U,
			// Token: 0x04000555 RID: 1365
			CouldNotCompareLogFiles = 3221489697U,
			// Token: 0x04000556 RID: 1366
			CouldNotCreateNetworkShare,
			// Token: 0x04000557 RID: 1367
			RestoreDatabaseCopySuccessful = 1074006053U,
			// Token: 0x04000558 RID: 1368
			RestoreDatabaseCopySuccessfulPathsChanged,
			// Token: 0x04000559 RID: 1369
			RestoreDatabaseCopyIncomplete,
			// Token: 0x0400055A RID: 1370
			RestoreDatabaseCopyIncompletePathsChanged,
			// Token: 0x0400055B RID: 1371
			RestoreDatabaseCopyFailed,
			// Token: 0x0400055C RID: 1372
			AutoMountReportNoLoss,
			// Token: 0x0400055D RID: 1373
			AutoMountReportMountWithDataLoss = 2147747883U,
			// Token: 0x0400055E RID: 1374
			AutoMountReportMountNotAllowed = 3221489708U,
			// Token: 0x0400055F RID: 1375
			AutoMountReportMountAfter = 2147747885U,
			// Token: 0x04000560 RID: 1376
			AutoMountReportPublicFolderMountNotAllowed,
			// Token: 0x04000561 RID: 1377
			CouldNotReplayLogFile = 3221489711U,
			// Token: 0x04000562 RID: 1378
			ReseedCheckMissingLogfile,
			// Token: 0x04000563 RID: 1379
			IsamException,
			// Token: 0x04000564 RID: 1380
			ErrorCouldNotRecopyLogFile,
			// Token: 0x04000565 RID: 1381
			DatabaseSubmitDumpsterMessages = 1074006067U,
			// Token: 0x04000566 RID: 1382
			ReseederDeletedCheckpointFile,
			// Token: 0x04000567 RID: 1383
			ConfigurationCheckerFailedADError = 3221489717U,
			// Token: 0x04000568 RID: 1384
			ReseederDeletedTargetDatabaseFile = 1074006070U,
			// Token: 0x04000569 RID: 1385
			ShipLogFailed = 3221489720U,
			// Token: 0x0400056A RID: 1386
			ReseederDeletedExistingLogs = 1074006073U,
			// Token: 0x0400056B RID: 1387
			DatabaseNotPresentAfterReplay = 3221489722U,
			// Token: 0x0400056C RID: 1388
			SeederPerfCountersLoadFailure = 3221489725U,
			// Token: 0x0400056D RID: 1389
			VSSWriterBackupDatabaseFullCopy = 1074006078U,
			// Token: 0x0400056E RID: 1390
			VSSWriterBackupDatabaseIncrementalDifferential,
			// Token: 0x0400056F RID: 1391
			VSSWriterBackupDatabaseError = 3221489728U,
			// Token: 0x04000570 RID: 1392
			DatabaseDumpsterRedeliveryRequired = 1074006081U,
			// Token: 0x04000571 RID: 1393
			ReplicaInstanceLogCopied,
			// Token: 0x04000572 RID: 1394
			ReplicaInstanceLogsReplayed,
			// Token: 0x04000573 RID: 1395
			ReplicaInstanceStartIncrementalReseed,
			// Token: 0x04000574 RID: 1396
			ReplicaInstanceFinishIncrementalReseed,
			// Token: 0x04000575 RID: 1397
			VSSWriterOrphanedBackupInstance = 2147747910U,
			// Token: 0x04000576 RID: 1398
			VSSWriterMissingFile = 3221489735U,
			// Token: 0x04000577 RID: 1399
			HTTPListenerFailedToStart,
			// Token: 0x04000578 RID: 1400
			TcpListenerFailedToStart,
			// Token: 0x04000579 RID: 1401
			ScrConfigPathConflict = 3221489741U,
			// Token: 0x0400057A RID: 1402
			MultiplePathNext = 1074006094U,
			// Token: 0x0400057B RID: 1403
			NetworkPathNext,
			// Token: 0x0400057C RID: 1404
			LogCopyFailedDueToDuplicateName = 3221489744U,
			// Token: 0x0400057D RID: 1405
			ScrConfigExceedLimit = 2147747921U,
			// Token: 0x0400057E RID: 1406
			ScrConfigConflictWithDb,
			// Token: 0x0400057F RID: 1407
			VSSReplicaBroken = 3221489747U,
			// Token: 0x04000580 RID: 1408
			VSSReplicaSuspend,
			// Token: 0x04000581 RID: 1409
			RpcServerStarted = 1074006101U,
			// Token: 0x04000582 RID: 1410
			RpcServerStopped,
			// Token: 0x04000583 RID: 1411
			RpcServerFailedToStart = 3221489751U,
			// Token: 0x04000584 RID: 1412
			LogTruncationOpenFailed = 2147747928U,
			// Token: 0x04000585 RID: 1413
			LogTruncationSourceFailure,
			// Token: 0x04000586 RID: 1414
			LogTruncationLocalFailure,
			// Token: 0x04000587 RID: 1415
			VSSWriterException = 3221489756U,
			// Token: 0x04000588 RID: 1416
			RpcServerFailedToFindExchangeServersUsg,
			// Token: 0x04000589 RID: 1417
			InstanceFailedToStart,
			// Token: 0x0400058A RID: 1418
			InstanceFailedToDeleteRegistryStateWarning = 2147747935U,
			// Token: 0x0400058B RID: 1419
			NoDirectoryHostInaccessible = 3221489761U,
			// Token: 0x0400058C RID: 1420
			ExceptionDuringCallback,
			// Token: 0x0400058D RID: 1421
			AlternateNetworkHadProblem,
			// Token: 0x0400058E RID: 1422
			LogCopierFoundNoLogsOnSource = 3221489766U,
			// Token: 0x0400058F RID: 1423
			LogCopierFailedDueToSource,
			// Token: 0x04000590 RID: 1424
			LogCopierFailedDueToTarget,
			// Token: 0x04000591 RID: 1425
			LogCopierFailedToCommunicate,
			// Token: 0x04000592 RID: 1426
			TPRExchangeListenerStarted = 1074006122U,
			// Token: 0x04000593 RID: 1427
			TPRManagerInitFailure = 3221489771U,
			// Token: 0x04000594 RID: 1428
			LogCopierErrorOnSource,
			// Token: 0x04000595 RID: 1429
			ReplicaInstanceMadeProgress = 1074006125U,
			// Token: 0x04000596 RID: 1430
			LogCopierErrorOnSourceTriggerFailover = 3221489774U,
			// Token: 0x04000597 RID: 1431
			LogCopierIsStalledDueToSource,
			// Token: 0x04000598 RID: 1432
			CorruptLogRecoveryIsAttempted,
			// Token: 0x04000599 RID: 1433
			CorruptLogRecoveryFailedToSuspend,
			// Token: 0x0400059A RID: 1434
			CorruptLogRecoveryFailedToDismount,
			// Token: 0x0400059B RID: 1435
			LogCopierReceivedSourceSideError,
			// Token: 0x0400059C RID: 1436
			LogCopierBlockedByFullDisk,
			// Token: 0x0400059D RID: 1437
			LogCorruptionTriggersFailover,
			// Token: 0x0400059E RID: 1438
			LogCopierDisconnectedTooLong,
			// Token: 0x0400059F RID: 1439
			CorruptLogDetectedOnActive,
			// Token: 0x040005A0 RID: 1440
			ErrorReadingLogOnActive,
			// Token: 0x040005A1 RID: 1441
			CorruptLogRepaired = 2147747961U,
			// Token: 0x040005A2 RID: 1442
			SlowIoDetected,
			// Token: 0x040005A3 RID: 1443
			CorruptLogRecoveryIsImmediatelyAttempted = 3221489787U,
			// Token: 0x040005A4 RID: 1444
			ResumeFailedDuringFailureItemProcessing,
			// Token: 0x040005A5 RID: 1445
			InspectorFixedCorruptLog = 2147747965U,
			// Token: 0x040005A6 RID: 1446
			LogCopierDetectsPossibleLogStreamReset,
			// Token: 0x040005A7 RID: 1447
			LogCopierFailedToTransitOutOfBlockMode,
			// Token: 0x040005A8 RID: 1448
			InspectorDetectedCorruptLog = 3221489792U,
			// Token: 0x040005A9 RID: 1449
			FatalIOErrorEncountered,
			// Token: 0x040005AA RID: 1450
			PassiveMonitoredDBFailedToStart,
			// Token: 0x040005AB RID: 1451
			ActiveMonitoredDBFailedToStart,
			// Token: 0x040005AC RID: 1452
			VSSWriterMissingLogFileSignature,
			// Token: 0x040005AD RID: 1453
			NetworkRoleChanged = 2147747992U,
			// Token: 0x040005AE RID: 1454
			RegistryReplicatorException = 3221489817U,
			// Token: 0x040005AF RID: 1455
			ClusterApiHungAlert = 2147748174U,
			// Token: 0x040005B0 RID: 1456
			IncrementalReseedInitException = 3221490760U,
			// Token: 0x040005B1 RID: 1457
			IncrementalReseedFailedError,
			// Token: 0x040005B2 RID: 1458
			IncrementalReseedRetryableError,
			// Token: 0x040005B3 RID: 1459
			IncrementalReseedPrereqError,
			// Token: 0x040005B4 RID: 1460
			IncSeedingStarted = 1074007116U,
			// Token: 0x040005B5 RID: 1461
			IncSeedingComplete,
			// Token: 0x040005B6 RID: 1462
			IncSeedingSourceReleased,
			// Token: 0x040005B7 RID: 1463
			AmChangingRole,
			// Token: 0x040005B8 RID: 1464
			AmStoreServiceStarted,
			// Token: 0x040005B9 RID: 1465
			AmInitiatingNodeFailover,
			// Token: 0x040005BA RID: 1466
			AmDatabaseMountFailed = 3221490770U,
			// Token: 0x040005BB RID: 1467
			AmDatabaseMounted = 1074007124U,
			// Token: 0x040005BC RID: 1468
			AmDetectedNodeStateChange,
			// Token: 0x040005BD RID: 1469
			AmStartingAutoMount,
			// Token: 0x040005BE RID: 1470
			AmIgnoringDatabaseMount,
			// Token: 0x040005BF RID: 1471
			AmDatabaseDismounted = 1074007129U,
			// Token: 0x040005C0 RID: 1472
			AmKnownError = 3221490778U,
			// Token: 0x040005C1 RID: 1473
			AmUnknownCrticalError,
			// Token: 0x040005C2 RID: 1474
			AmErrorReadingConfiguration,
			// Token: 0x040005C3 RID: 1475
			AmCriticalErrorReadingConfiguration,
			// Token: 0x040005C4 RID: 1476
			AmMoveNotApplicableForDatabase = 2147748958U,
			// Token: 0x040005C5 RID: 1477
			AmFailedToAutomountDatabase = 3221490756U,
			// Token: 0x040005C6 RID: 1478
			SuspendMarkedForDatabaseCopy = 1074007110U,
			// Token: 0x040005C7 RID: 1479
			ResumeMarkedForDatabaseCopy,
			// Token: 0x040005C8 RID: 1480
			MountAllowedWithMountDialOverride = 1074007135U,
			// Token: 0x040005C9 RID: 1481
			MountNotAllowedWithMountDialOverride = 3221490784U,
			// Token: 0x040005CA RID: 1482
			AmDatabaseMoved = 1074007137U,
			// Token: 0x040005CB RID: 1483
			AmDatabaseMoveFailed = 3221490786U,
			// Token: 0x040005CC RID: 1484
			DbSeedingRequired = 2147748963U,
			// Token: 0x040005CD RID: 1485
			DatabaseExistsInADButRegistryDeleted = 3221490788U,
			// Token: 0x040005CE RID: 1486
			AmRpcServerStarted = 1074007141U,
			// Token: 0x040005CF RID: 1487
			AmRpcServerStopped,
			// Token: 0x040005D0 RID: 1488
			AmRpcServerFailedToStart = 3221490791U,
			// Token: 0x040005D1 RID: 1489
			AmRpcServerFailedToFindExchangeServersUsg,
			// Token: 0x040005D2 RID: 1490
			ServiceFailedToStartAMFailure,
			// Token: 0x040005D3 RID: 1491
			AmDatabaseNotMountedServersDown,
			// Token: 0x040005D4 RID: 1492
			AmForceDismountingDatabases = 2147748971U,
			// Token: 0x040005D5 RID: 1493
			AmDatabaseAcllComplete = 1074007150U,
			// Token: 0x040005D6 RID: 1494
			AmDatabaseAcllFailed = 3221490799U,
			// Token: 0x040005D7 RID: 1495
			FailedMovePAM,
			// Token: 0x040005D8 RID: 1496
			SuccMovePAM = 1074007153U,
			// Token: 0x040005D9 RID: 1497
			AmIgnoredMapiNetFailureBecauseNodeNotUp = 2147748978U,
			// Token: 0x040005DA RID: 1498
			AmKilledStoreToForceDismount = 3221490803U,
			// Token: 0x040005DB RID: 1499
			AmFailedToStopService,
			// Token: 0x040005DC RID: 1500
			AmFailedToStartService,
			// Token: 0x040005DD RID: 1501
			AMDetectedMapiNetworkFailure,
			// Token: 0x040005DE RID: 1502
			AmIgnoredMapiNetFailureBecauseMapiLooksUp = 2147748983U,
			// Token: 0x040005DF RID: 1503
			AmIgnoredMapiNetFailureBecauseADIsWorking,
			// Token: 0x040005E0 RID: 1504
			AmIgnoredMapiNetFailureBecauseNotThePam,
			// Token: 0x040005E1 RID: 1505
			PauseSuccessful = 1074007970U,
			// Token: 0x040005E2 RID: 1506
			StopFailed = 3221491619U,
			// Token: 0x040005E3 RID: 1507
			StartFailed,
			// Token: 0x040005E4 RID: 1508
			PauseFailed = 3221491622U,
			// Token: 0x040005E5 RID: 1509
			CommandOK = 3221491621U,
			// Token: 0x040005E6 RID: 1510
			CommandFailed = 3221491623U,
			// Token: 0x040005E7 RID: 1511
			PowerEventOK,
			// Token: 0x040005E8 RID: 1512
			PowerEventFailed,
			// Token: 0x040005E9 RID: 1513
			SessionChangeFailed,
			// Token: 0x040005EA RID: 1514
			ShutdownOK,
			// Token: 0x040005EB RID: 1515
			ShutdownFailed,
			// Token: 0x040005EC RID: 1516
			CommandSuccessful,
			// Token: 0x040005ED RID: 1517
			ContinueSuccessful,
			// Token: 0x040005EE RID: 1518
			ContinueFailed,
			// Token: 0x040005EF RID: 1519
			FailedToUnloadAppDomain,
			// Token: 0x040005F0 RID: 1520
			PreShutdownOK = 1074007985U,
			// Token: 0x040005F1 RID: 1521
			PreShutdownFailed = 3221491634U,
			// Token: 0x040005F2 RID: 1522
			PreShutdownStart = 1074007987U,
			// Token: 0x040005F3 RID: 1523
			SeedManagerStarted,
			// Token: 0x040005F4 RID: 1524
			SeedManagerStopped,
			// Token: 0x040005F5 RID: 1525
			SeedInstancePrepareAdded,
			// Token: 0x040005F6 RID: 1526
			SeedInstancePrepareSucceeded,
			// Token: 0x040005F7 RID: 1527
			SeedInstancePrepareUnknownError = 3221491640U,
			// Token: 0x040005F8 RID: 1528
			SeedInstancePrepareFailed,
			// Token: 0x040005F9 RID: 1529
			SeedInstanceInProgressFailed,
			// Token: 0x040005FA RID: 1530
			SeedInstanceCancelled = 2147749819U,
			// Token: 0x040005FB RID: 1531
			SeedInstanceBeginSucceeded = 1074007996U,
			// Token: 0x040005FC RID: 1532
			SeedInstanceBeginUnknownError = 3221491645U,
			// Token: 0x040005FD RID: 1533
			SeedInstanceCancelRequestedByAdmin = 1074007998U,
			// Token: 0x040005FE RID: 1534
			SeedInstanceCleanupRequestedByAdmin,
			// Token: 0x040005FF RID: 1535
			SeedInstanceCleanupConfigChanged,
			// Token: 0x04000600 RID: 1536
			SeedInstanceCleanupStale,
			// Token: 0x04000601 RID: 1537
			SeedInstancesStoppedServiceShutdown = 2147749826U,
			// Token: 0x04000602 RID: 1538
			SeedInstanceDeletedExistingLogs = 1074008003U,
			// Token: 0x04000603 RID: 1539
			SeedInstanceDeletedCheckpointFile,
			// Token: 0x04000604 RID: 1540
			SeedInstanceSuccess,
			// Token: 0x04000605 RID: 1541
			MonitoringClusterServiceCheckFailed = 3221491654U,
			// Token: 0x04000606 RID: 1542
			MonitoringClusterServiceCheckPassed = 1074008007U,
			// Token: 0x04000607 RID: 1543
			MonitoringActiveManagerCheckFailed = 3221491656U,
			// Token: 0x04000608 RID: 1544
			MonitoringActiveManagerCheckPassed = 1074008009U,
			// Token: 0x04000609 RID: 1545
			MonitoringReplayServiceCheckFailed = 3221491658U,
			// Token: 0x0400060A RID: 1546
			MonitoringReplayServiceCheckPassed = 1074008011U,
			// Token: 0x0400060B RID: 1547
			MonitoringDagMembersUpCheckFailed = 3221491660U,
			// Token: 0x0400060C RID: 1548
			MonitoringDagMembersUpCheckPassed = 1074008013U,
			// Token: 0x0400060D RID: 1549
			MonitoringClusterNetworkCheckFailed = 3221491662U,
			// Token: 0x0400060E RID: 1550
			MonitoringClusterNetworkCheckWarning = 2147749839U,
			// Token: 0x0400060F RID: 1551
			MonitoringClusterNetworkCheckPassed = 1074008016U,
			// Token: 0x04000610 RID: 1552
			MonitoringFileShareQuorumCheckFailed = 3221491665U,
			// Token: 0x04000611 RID: 1553
			MonitoringFileShareQuorumCheckPassed = 1074008018U,
			// Token: 0x04000612 RID: 1554
			MonitoringQuorumGroupCheckFailed = 3221491667U,
			// Token: 0x04000613 RID: 1555
			MonitoringQuorumGroupCheckPassed = 1074008020U,
			// Token: 0x04000614 RID: 1556
			MonitoringTasksRpcListenerCheckFailed = 3221491669U,
			// Token: 0x04000615 RID: 1557
			MonitoringTasksRpcListenerCheckPassed = 1074008022U,
			// Token: 0x04000616 RID: 1558
			MonitoringHttpListenerCheckFailed = 3221491671U,
			// Token: 0x04000617 RID: 1559
			MonitoringHttpListenerCheckPassed = 1074008024U,
			// Token: 0x04000618 RID: 1560
			LogReplayMapiException = 3221491673U,
			// Token: 0x04000619 RID: 1561
			DatabaseOperationLockIsTakingLongTime = 2147749850U,
			// Token: 0x0400061A RID: 1562
			CiSeedInstanceSuccess = 1074008027U,
			// Token: 0x0400061B RID: 1563
			SourceReplicaInstanceNotStarted = 3221491676U,
			// Token: 0x0400061C RID: 1564
			TargetReplicaInstanceNotStarted,
			// Token: 0x0400061D RID: 1565
			SubmitDumpsterMessagesFailed = 3221491681U,
			// Token: 0x0400061E RID: 1566
			ClusterDatabaseWriteFailed,
			// Token: 0x0400061F RID: 1567
			IncSeedingTerminated = 2147749859U,
			// Token: 0x04000620 RID: 1568
			AmRoleMonitoringError = 3221491684U,
			// Token: 0x04000621 RID: 1569
			AutoMountReportDbInUseOnSource = 2147749861U,
			// Token: 0x04000622 RID: 1570
			AutoMountReportDbInUseAcllInProgress,
			// Token: 0x04000623 RID: 1571
			AmDatabaseDismountFailed = 3221491687U,
			// Token: 0x04000624 RID: 1572
			AmForceDismountMasterMismatch = 2147749864U,
			// Token: 0x04000625 RID: 1573
			AmDatabaseMountFailedGeneric = 3221491689U,
			// Token: 0x04000626 RID: 1574
			AmDatabaseDismountFailedGeneric,
			// Token: 0x04000627 RID: 1575
			AmDatabaseMoveFailedGeneric,
			// Token: 0x04000628 RID: 1576
			NetworkReplicationDisabled,
			// Token: 0x04000629 RID: 1577
			MonitoringTcpListenerCheckFailed = 3221491696U,
			// Token: 0x0400062A RID: 1578
			MonitoringTcpListenerCheckPassed = 1074008049U,
			// Token: 0x0400062B RID: 1579
			NetworkMonitoringError = 3221491698U,
			// Token: 0x0400062C RID: 1580
			SeedInstanceAnotherError,
			// Token: 0x0400062D RID: 1581
			ForceNewLogError,
			// Token: 0x0400062E RID: 1582
			ReadOnePageError,
			// Token: 0x0400062F RID: 1583
			ReadPageSizeError,
			// Token: 0x04000630 RID: 1584
			AmDatabaseMoveUnspecifiedServerFailed,
			// Token: 0x04000631 RID: 1585
			VSSReplicaCopyUnhealthy,
			// Token: 0x04000632 RID: 1586
			IncrementalReseedSourceDatabaseDismounted,
			// Token: 0x04000633 RID: 1587
			IncrementalReseedSourceDatabaseMountRpcError,
			// Token: 0x04000634 RID: 1588
			DumpsterRedeliveryFailed,
			// Token: 0x04000635 RID: 1589
			NodeNotInCluster,
			// Token: 0x04000636 RID: 1590
			ReplayRpcServerFailedToRegister,
			// Token: 0x04000637 RID: 1591
			AmRpcServerFailedToRegister,
			// Token: 0x04000638 RID: 1592
			ServiceFailedToStartComponentFailure,
			// Token: 0x04000639 RID: 1593
			FailedToStartRetriableComponent,
			// Token: 0x0400063A RID: 1594
			ConfigUpdaterScanFailed,
			// Token: 0x0400063B RID: 1595
			ConfigUpdaterFailedToFindConfig,
			// Token: 0x0400063C RID: 1596
			MonitoringTPRListenerCheckFailed,
			// Token: 0x0400063D RID: 1597
			MonitoringTPRListenerCheckPassed = 1074008068U,
			// Token: 0x0400063E RID: 1598
			EsebcliTooManyApplications = 3221491717U,
			// Token: 0x0400063F RID: 1599
			SeedingSourceBegin = 1074008070U,
			// Token: 0x04000640 RID: 1600
			SeedingSourceEnd,
			// Token: 0x04000641 RID: 1601
			SeedingSourceCancel = 2147749896U,
			// Token: 0x04000642 RID: 1602
			CheckConnectionToStoreFailed = 3221491721U,
			// Token: 0x04000643 RID: 1603
			CheckDatabaseHeaderFailed,
			// Token: 0x04000644 RID: 1604
			PossibleSplitBrainDetected,
			// Token: 0x04000645 RID: 1605
			FailedToCleanUpSingleIncReseedFile,
			// Token: 0x04000646 RID: 1606
			FailedToCleanUpFile,
			// Token: 0x04000647 RID: 1607
			LogReplaySuspendedDueToCopyQ = 2147749902U,
			// Token: 0x04000648 RID: 1608
			LogReplayResumedDueToCopyQ = 1074008079U,
			// Token: 0x04000649 RID: 1609
			SyncSuspendResumeOperationFailed = 3221491728U,
			// Token: 0x0400064A RID: 1610
			MonitoringDatabaseRedundancyCheckFailed,
			// Token: 0x0400064B RID: 1611
			MonitoringDatabaseRedundancyCheckPassed = 1074008082U,
			// Token: 0x0400064C RID: 1612
			DatabaseRedistributionReport,
			// Token: 0x0400064D RID: 1613
			FqdnResolutionFailure = 3221491732U,
			// Token: 0x0400064E RID: 1614
			LogReplayPatchFailedIsamException,
			// Token: 0x0400064F RID: 1615
			RedirtyDatabaseCreateTempLog,
			// Token: 0x04000650 RID: 1616
			LogReplayPatchFailedPrepareException,
			// Token: 0x04000651 RID: 1617
			FailedToPublishFailureItem,
			// Token: 0x04000652 RID: 1618
			PeriodicOperationFailedRetrievingStatuses = 2147749913U,
			// Token: 0x04000653 RID: 1619
			ProcessDiagnosticsTerminatingService = 3221491738U,
			// Token: 0x04000654 RID: 1620
			GetBootTimeWithWmiFailure,
			// Token: 0x04000655 RID: 1621
			LogRepairFailedDueToRetryLimit,
			// Token: 0x04000656 RID: 1622
			LogRepairSuccess = 2147749917U,
			// Token: 0x04000657 RID: 1623
			ConfigurationCheckerFailedGeneric = 3221491742U,
			// Token: 0x04000658 RID: 1624
			LogFileCorruptOrGapFoundOutsideRequiredRange,
			// Token: 0x04000659 RID: 1625
			MovingFilesToRestartLogStream = 2147749920U,
			// Token: 0x0400065A RID: 1626
			DeletedSkippedLogsDirectory,
			// Token: 0x0400065B RID: 1627
			MissingFailureItemDetected = 3221491746U,
			// Token: 0x0400065C RID: 1628
			LogReplayPatchFailedOnLaggedCopy,
			// Token: 0x0400065D RID: 1629
			MonitoringDatabaseOneDatacenterCheckFailed = 3221491749U,
			// Token: 0x0400065E RID: 1630
			ActiveSeedingSourceBegin = 1074008102U,
			// Token: 0x0400065F RID: 1631
			ActiveSeedingSourceEnd,
			// Token: 0x04000660 RID: 1632
			ActiveSeedingSourceCancel = 2147749928U,
			// Token: 0x04000661 RID: 1633
			LogRepairNotPossible = 3221491753U,
			// Token: 0x04000662 RID: 1634
			MonitoringDatabaseRedundancyServerCheckFailed,
			// Token: 0x04000663 RID: 1635
			MonitoringDatabaseRedundancyServerCheckPassed = 1074008107U,
			// Token: 0x04000664 RID: 1636
			DetermineWorkerProcessIdFailed = 3221491756U,
			// Token: 0x04000665 RID: 1637
			DagNetworkConfigOld = 1074008268U,
			// Token: 0x04000666 RID: 1638
			DagNetworkConfigNew,
			// Token: 0x04000667 RID: 1639
			NotifyActiveSendFailed = 3221491927U,
			// Token: 0x04000668 RID: 1640
			AmCheckRemoteSiteLocalServerSiteNull = 2147750104U,
			// Token: 0x04000669 RID: 1641
			AmCheckRemoteSiteNotFound = 1074008281U,
			// Token: 0x0400066A RID: 1642
			AmCheckRemoteSiteAlert = 3221491930U,
			// Token: 0x0400066B RID: 1643
			AmCheckRemoteSiteDismount,
			// Token: 0x0400066C RID: 1644
			AmCheckRemoteSiteSucceeded = 1074008284U,
			// Token: 0x0400066D RID: 1645
			AmCheckRemoteSiteDisabled = 2147750109U,
			// Token: 0x0400066E RID: 1646
			DiskHbFailedToStartWatchdogTimer = 3221491934U,
			// Token: 0x0400066F RID: 1647
			DiskHbFailedToPrepareHeartbeatFile,
			// Token: 0x04000670 RID: 1648
			DiskHbEncounteredUnhandledException,
			// Token: 0x04000671 RID: 1649
			DiskHbFailedToDetermineBytesPerSector,
			// Token: 0x04000672 RID: 1650
			DiskHbIoWriteTestFailed,
			// Token: 0x04000673 RID: 1651
			DiskHbIoReadTestFailed,
			// Token: 0x04000674 RID: 1652
			DiskHbIoLatencyExceeded = 2147750116U,
			// Token: 0x04000675 RID: 1653
			DiskHbFailedToStart = 3221491941U,
			// Token: 0x04000676 RID: 1654
			DiskHbFailedToStop,
			// Token: 0x04000677 RID: 1655
			AmTimeStampEntryMissingInOneOrMoreServers = 1074008295U,
			// Token: 0x04000678 RID: 1656
			AutoReseedInstanceStarted,
			// Token: 0x04000679 RID: 1657
			AutoReseedPrereqFailed = 3221491945U,
			// Token: 0x0400067A RID: 1658
			AutoReseedNoSpareDisk,
			// Token: 0x0400067B RID: 1659
			AutoReseedFailed,
			// Token: 0x0400067C RID: 1660
			AutoReseedSuccessful = 1074008300U,
			// Token: 0x0400067D RID: 1661
			AutoReseedSpareDisksReleased,
			// Token: 0x0400067E RID: 1662
			MonitoringDatabaseOneDatacenterCheckSuccess,
			// Token: 0x0400067F RID: 1663
			SeedInstanceStartedSetBroken = 3221491951U,
			// Token: 0x04000680 RID: 1664
			LogReplayGenericError,
			// Token: 0x04000681 RID: 1665
			ReplayServiceVSSWriterMissingVersionStampError,
			// Token: 0x04000682 RID: 1666
			ReplayServiceVSSWriterBadVersionStampError,
			// Token: 0x04000683 RID: 1667
			ReplayServiceVSSWriterRestoreOptionsString = 1074008307U,
			// Token: 0x04000684 RID: 1668
			ReplayServiceVSSWriterDbGuidMappingMismatchError = 3221491956U,
			// Token: 0x04000685 RID: 1669
			ReplayServiceVSSWriterMultipleRetargettingError,
			// Token: 0x04000686 RID: 1670
			ReplayServiceVSSWriterOriginalLogfilePathMismatchError,
			// Token: 0x04000687 RID: 1671
			ReplayServiceVSSWriterOriginalSystemPathMismatchError,
			// Token: 0x04000688 RID: 1672
			ReplayServiceVSSWriterDbRetargetMismatchError,
			// Token: 0x04000689 RID: 1673
			ReplayServiceVSSWriterTargetSGLookupError,
			// Token: 0x0400068A RID: 1674
			ReplayServiceVSSWriterTargetSGOnline,
			// Token: 0x0400068B RID: 1675
			ReplayServiceVSSWriterRestoreToOriginalSG = 1074008315U,
			// Token: 0x0400068C RID: 1676
			ReplayServiceVSSWriterRestoreToAlternateSG,
			// Token: 0x0400068D RID: 1677
			ReplayServiceVSSWriterTargetLogfilePathMismatchError = 3221491965U,
			// Token: 0x0400068E RID: 1678
			ReplayServiceVSSWriterTargetSystemPathMismatchError,
			// Token: 0x0400068F RID: 1679
			ReplayServiceVSSWriterTargetLogfileBaseNameMismatchError,
			// Token: 0x04000690 RID: 1680
			ReplayServiceVSSWriterSGRestoreInProgressError,
			// Token: 0x04000691 RID: 1681
			ReplayServiceVSSWriterCircularLogDBRestore,
			// Token: 0x04000692 RID: 1682
			ReplayServiceVSSWriterTargetDbMismatchError,
			// Token: 0x04000693 RID: 1683
			ReplayServiceVSSWriterCannotOverwriteError,
			// Token: 0x04000694 RID: 1684
			ReplayServiceVSSWriterFindLogFilesError,
			// Token: 0x04000695 RID: 1685
			ReplayServiceVSSWriterLocationRestoreInProgressError,
			// Token: 0x04000696 RID: 1686
			ReplayServiceVSSWriterTargetLogfilePathInUseError,
			// Token: 0x04000697 RID: 1687
			ReplayServiceVSSWriterRestoreEnvSGMismatchError,
			// Token: 0x04000698 RID: 1688
			ReplayServiceVSSWriterRestoreEnvLogfilePathMismatchError,
			// Token: 0x04000699 RID: 1689
			ReplayServiceVSSWriterRestoreEnvLogfileBaseNameMismatchError,
			// Token: 0x0400069A RID: 1690
			ReplayServiceVSSWriterRestoreEnvLogfileSignatureMismatchError,
			// Token: 0x0400069B RID: 1691
			ReplayServiceVSSWriterRestoreEnvSystemPathMismatchError,
			// Token: 0x0400069C RID: 1692
			ReplayServiceVSSWriterRestoreEnvCircularLogEnabledError,
			// Token: 0x0400069D RID: 1693
			ReplayServiceVSSWriterRestoreEnvAlreadyRecoveredError,
			// Token: 0x0400069E RID: 1694
			ReplayServiceVSSWriterRenameDbError,
			// Token: 0x0400069F RID: 1695
			ReplayServiceVSSWriterRecoveryAfterRestore = 1074008335U,
			// Token: 0x040006A0 RID: 1696
			ReplayServiceVSSWriterAdditionalRestoresPending,
			// Token: 0x040006A1 RID: 1697
			ReplayServiceVSSWriterNoDatabasesToRecover,
			// Token: 0x040006A2 RID: 1698
			ReplayServiceVSSWriterDbToRecover,
			// Token: 0x040006A3 RID: 1699
			ReplayServiceVSSWriterChkptNotDeleted = 3221491987U,
			// Token: 0x040006A4 RID: 1700
			ReplayServiceVSSWriterLogsNotDeleted,
			// Token: 0x040006A5 RID: 1701
			ReplayServiceVSSWriterRestoreEnvNotDeleted,
			// Token: 0x040006A6 RID: 1702
			MonitoringDatabaseAvailabilityCheckFailed,
			// Token: 0x040006A7 RID: 1703
			MonitoringDatabaseAvailabilityCheckPassed = 1074008343U,
			// Token: 0x040006A8 RID: 1704
			MonitoringDatabaseOneDatacenterAvailableCopyCheckFailed = 3221491992U,
			// Token: 0x040006A9 RID: 1705
			MonitoringDatabaseOneDatacenterAvailableCopyCheckSuccess = 1074008345U,
			// Token: 0x040006AA RID: 1706
			ServerLocatorServiceStarted,
			// Token: 0x040006AB RID: 1707
			ServerLocatorServiceStopped,
			// Token: 0x040006AC RID: 1708
			ServerLocatorServiceFailedToStart = 3221491996U,
			// Token: 0x040006AD RID: 1709
			MonitoringServerLocatorServiceCheckFailed,
			// Token: 0x040006AE RID: 1710
			MonitoringServerLocatorServiceCheckPassed = 1074008350U,
			// Token: 0x040006AF RID: 1711
			ServerLocatorServiceCommunicationChannelFaulted = 3221491999U,
			// Token: 0x040006B0 RID: 1712
			ServerLocatorServiceRestartScheduled = 1074008352U,
			// Token: 0x040006B1 RID: 1713
			DatabaseDirectoryNotUnderMountPoint = 3221492001U,
			// Token: 0x040006B2 RID: 1714
			DiskHbStartingWatchdogTimer = 1074008358U,
			// Token: 0x040006B3 RID: 1715
			DiskHbStoppingWatchdogTimer,
			// Token: 0x040006B4 RID: 1716
			DiskHbFailedToStopWatchdogTimer = 3221492008U,
			// Token: 0x040006B5 RID: 1717
			DiskHbMinimumTimeNotElaspedFromLastReboot = 2147750185U,
			// Token: 0x040006B6 RID: 1718
			DiskHbConfigChanged,
			// Token: 0x040006B7 RID: 1719
			DiskHbTerminatingCurrentProcess = 3221492011U,
			// Token: 0x040006B8 RID: 1720
			DiskHbFailedToTerminateCurrentProcess,
			// Token: 0x040006B9 RID: 1721
			DiskHbTriggeringImmediateBugcheck,
			// Token: 0x040006BA RID: 1722
			ServerLocatorServiceStartTimeout,
			// Token: 0x040006BB RID: 1723
			ProcessDiagnosticsTerminatingServiceNoDump,
			// Token: 0x040006BC RID: 1724
			ServerLocatorServiceAnotherProcessUsingPort,
			// Token: 0x040006BD RID: 1725
			ServerLocatorServiceServerForDatabaseNotFoundError,
			// Token: 0x040006BE RID: 1726
			ServerLocatorServiceGetAllError,
			// Token: 0x040006BF RID: 1727
			DatabaseSuspendedDueToLowSpace,
			// Token: 0x040006C0 RID: 1728
			ServerLocatorServiceGetServerObjectError,
			// Token: 0x040006C1 RID: 1729
			MonitoringMonitoringServiceCheckFailed,
			// Token: 0x040006C2 RID: 1730
			MonitoringMonitoringServiceCheckPassed = 1074008374U
		}
	}
}
