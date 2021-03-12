using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.FailureItemEventLog
{
	// Token: 0x02000388 RID: 904
	public static class FailureItemEventLogConstants
	{
		// Token: 0x04000F7B RID: 3963
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcConfiguration9a = new ExEventLog.EventTuple(3221487717U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F7C RID: 3964
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtConfiguration9a = new ExEventLog.EventTuple(3221487718U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F7D RID: 3965
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcRepairable9a = new ExEventLog.EventTuple(3221487719U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F7E RID: 3966
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtRepairable9a = new ExEventLog.EventTuple(3221487720U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F7F RID: 3967
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_Space9a = new ExEventLog.EventTuple(3221487721U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F80 RID: 3968
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcIoHard9a = new ExEventLog.EventTuple(3221487722U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F81 RID: 3969
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtIoHard9a = new ExEventLog.EventTuple(3221487723U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F82 RID: 3970
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SourceCorruption9a = new ExEventLog.EventTuple(3221487724U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F83 RID: 3971
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcCorruption9a = new ExEventLog.EventTuple(3221487725U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F84 RID: 3972
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtCorruption9a = new ExEventLog.EventTuple(3221487726U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F85 RID: 3973
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcHard9a = new ExEventLog.EventTuple(3221487727U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F86 RID: 3974
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtHard9a = new ExEventLog.EventTuple(3221487728U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F87 RID: 3975
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcUnrecoverable9a = new ExEventLog.EventTuple(3221487729U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F88 RID: 3976
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtUnrecoverable9a = new ExEventLog.EventTuple(3221487730U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F89 RID: 3977
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcRemount9a = new ExEventLog.EventTuple(3221487731U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F8A RID: 3978
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtRemount9a = new ExEventLog.EventTuple(3221487732U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F8B RID: 3979
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtReseed9a = new ExEventLog.EventTuple(3221487733U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F8C RID: 3980
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcPerformance9a = new ExEventLog.EventTuple(3221487734U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F8D RID: 3981
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MoveLoad9a = new ExEventLog.EventTuple(3221487735U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F8E RID: 3982
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcMemory9a = new ExEventLog.EventTuple(3221487736U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F8F RID: 3983
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtMemory9a = new ExEventLog.EventTuple(3221487737U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F90 RID: 3984
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcCatalogReseed9a = new ExEventLog.EventTuple(3221487738U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F91 RID: 3985
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TgtCatalogReseed9a = new ExEventLog.EventTuple(3221487739U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000F92 RID: 3986
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AlertOnly9a = new ExEventLog.EventTuple(3221487740U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F93 RID: 3987
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnexpectedDismount9a = new ExEventLog.EventTuple(3221487742U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F94 RID: 3988
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcExceededMaxDatabases9a = new ExEventLog.EventTuple(3221487743U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F95 RID: 3989
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcGenericMountFailure9a = new ExEventLog.EventTuple(3221487744U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F96 RID: 3990
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PagePatchRequested9a = new ExEventLog.EventTuple(3221487745U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F97 RID: 3991
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PagePatchCompleted9a = new ExEventLog.EventTuple(1074004098U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F98 RID: 3992
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LostFlushDetected9a = new ExEventLog.EventTuple(3221487747U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F99 RID: 3993
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcSourceLogCorrupt9a = new ExEventLog.EventTuple(3221487748U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F9A RID: 3994
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcFailedToRepair9a = new ExEventLog.EventTuple(3221487749U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F9B RID: 3995
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LostFlushDbTimeTooOld9a = new ExEventLog.EventTuple(3221487750U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F9C RID: 3996
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtExceededMaxDatabases9a = new ExEventLog.EventTuple(3221487751U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F9D RID: 3997
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcLostFlushDbTimeTooNew9a = new ExEventLog.EventTuple(3221487752U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F9E RID: 3998
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtLostFlushDbTimeTooNew9a = new ExEventLog.EventTuple(3221487753U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000F9F RID: 3999
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcReseed9a = new ExEventLog.EventTuple(3221487754U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FA0 RID: 4000
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtPerformance9a = new ExEventLog.EventTuple(3221487755U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FA1 RID: 4001
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtFailedToRepair9a = new ExEventLog.EventTuple(3221487756U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FA2 RID: 4002
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnexpectedDismountMoveWasSkipped9a = new ExEventLog.EventTuple(1074004109U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FA3 RID: 4003
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtGenericMountFailure9a = new ExEventLog.EventTuple(3221487758U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FA4 RID: 4004
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcExceededMaxActiveDatabases9a = new ExEventLog.EventTuple(3221487759U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FA5 RID: 4005
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtExceededMaxActiveDatabases9a = new ExEventLog.EventTuple(3221487760U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FA6 RID: 4006
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcSourceLogCorruptOutsideRequiredRange9a = new ExEventLog.EventTuple(3221487761U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FA7 RID: 4007
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcGeneric9a = new ExEventLog.EventTuple(3221487762U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FA8 RID: 4008
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtGeneric9a = new ExEventLog.EventTuple(3221487763U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FA9 RID: 4009
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcLogCorruptionDetectedByESE9a = new ExEventLog.EventTuple(3221487764U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FAA RID: 4010
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtLogCorruptionDetectedByESE9a = new ExEventLog.EventTuple(3221487765U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FAB RID: 4011
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcFileSystemCorruption9a = new ExEventLog.EventTuple(3221487766U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FAC RID: 4012
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtFileSystemCorruption9a = new ExEventLog.EventTuple(3221487767U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FAD RID: 4013
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcHungIoLowThreshold9a = new ExEventLog.EventTuple(3221487768U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FAE RID: 4014
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtHungIoLowThreshold9a = new ExEventLog.EventTuple(3221487769U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FAF RID: 4015
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcHungIoMediumThreshold9a = new ExEventLog.EventTuple(3221487770U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FB0 RID: 4016
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtHungIoMediumThreshold9a = new ExEventLog.EventTuple(3221487771U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FB1 RID: 4017
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcHungIoExceededThresholdDoubleDisk9a = new ExEventLog.EventTuple(3221487772U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FB2 RID: 4018
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtHungIoExceededThresholdDoubleDisk9a = new ExEventLog.EventTuple(3221487773U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FB3 RID: 4019
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcHungIoExceededThreshold9a = new ExEventLog.EventTuple(3221487774U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FB4 RID: 4020
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtHungIoExceededThreshold9a = new ExEventLog.EventTuple(3221487775U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FB5 RID: 4021
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcHungIoCancelFailed9a = new ExEventLog.EventTuple(3221487776U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FB6 RID: 4022
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtHungIoCancelFailed9a = new ExEventLog.EventTuple(3221487777U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FB7 RID: 4023
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcHungIoCancelSucceeded9a = new ExEventLog.EventTuple(3221487778U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FB8 RID: 4024
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtHungIoCancelSucceeded9a = new ExEventLog.EventTuple(3221487779U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FB9 RID: 4025
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcHungStoreWorker9a = new ExEventLog.EventTuple(3221487780U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FBA RID: 4026
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtHungStoreWorker9a = new ExEventLog.EventTuple(3221487781U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FBB RID: 4027
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcUnaccessibleStoreWorker9a = new ExEventLog.EventTuple(3221487782U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FBC RID: 4028
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtUnaccessibleStoreWorker9a = new ExEventLog.EventTuple(3221487783U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FBD RID: 4029
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcMonitoredDatabaseFailed9a = new ExEventLog.EventTuple(3221487784U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FBE RID: 4030
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogGapFatal9a = new ExEventLog.EventTuple(3221487785U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FBF RID: 4031
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtMonitoredDatabaseFailed9a = new ExEventLog.EventTuple(3221487786U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FC0 RID: 4032
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcExceededDatabaseMaxSize9a = new ExEventLog.EventTuple(3221487787U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FC1 RID: 4033
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtExceededDatabaseMaxSize9a = new ExEventLog.EventTuple(3221487788U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FC2 RID: 4034
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtLowDiskSpaceStraggler9a = new ExEventLog.EventTuple(3221487789U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FC3 RID: 4035
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcLockedVolume9a = new ExEventLog.EventTuple(3221487790U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FC4 RID: 4036
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtLockedVolume9a = new ExEventLog.EventTuple(3221487791U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FC5 RID: 4037
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcConfiguration9b = new ExEventLog.EventTuple(3221487817U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FC6 RID: 4038
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtConfiguration9b = new ExEventLog.EventTuple(3221487818U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FC7 RID: 4039
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcRepairable9b = new ExEventLog.EventTuple(3221487819U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FC8 RID: 4040
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_Space9b = new ExEventLog.EventTuple(3221487821U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FC9 RID: 4041
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcIoHard9b = new ExEventLog.EventTuple(3221487822U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FCA RID: 4042
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtIoHard9b = new ExEventLog.EventTuple(3221487823U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FCB RID: 4043
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SourceCorruption9b = new ExEventLog.EventTuple(3221487824U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FCC RID: 4044
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcCorruption9b = new ExEventLog.EventTuple(3221487825U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FCD RID: 4045
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtCorruption9b = new ExEventLog.EventTuple(3221487826U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FCE RID: 4046
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcHard9b = new ExEventLog.EventTuple(3221487827U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FCF RID: 4047
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtHard9b = new ExEventLog.EventTuple(3221487828U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FD0 RID: 4048
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcUnrecoverable9b = new ExEventLog.EventTuple(3221487829U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FD1 RID: 4049
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtUnrecoverable9b = new ExEventLog.EventTuple(3221487830U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FD2 RID: 4050
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcRemount9b = new ExEventLog.EventTuple(3221487831U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FD3 RID: 4051
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtReseed9b = new ExEventLog.EventTuple(3221487833U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FD4 RID: 4052
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcPerformance9b = new ExEventLog.EventTuple(3221487834U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FD5 RID: 4053
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MoveLoad9b = new ExEventLog.EventTuple(3221487835U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FD6 RID: 4054
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcMemory9b = new ExEventLog.EventTuple(3221487836U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FD7 RID: 4055
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SrcCatalogReseed9b = new ExEventLog.EventTuple(3221487838U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000FD8 RID: 4056
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcSourceLogCorrupt9b = new ExEventLog.EventTuple(3221487840U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FD9 RID: 4057
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnexpectedDismount9b = new ExEventLog.EventTuple(3221487841U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FDA RID: 4058
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcExceededMaxDatabases9b = new ExEventLog.EventTuple(3221487842U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FDB RID: 4059
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtExceededMaxDatabases9b = new ExEventLog.EventTuple(3221487843U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FDC RID: 4060
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcFailedToRepair9b = new ExEventLog.EventTuple(3221487844U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FDD RID: 4061
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtFailedToRepair9b = new ExEventLog.EventTuple(3221487845U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FDE RID: 4062
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtLostFlushDbTimeTooNew9b = new ExEventLog.EventTuple(3221487846U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FDF RID: 4063
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcGenericMountFailure9b = new ExEventLog.EventTuple(3221487847U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FE0 RID: 4064
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AlertOnly9b = new ExEventLog.EventTuple(3221487848U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FE1 RID: 4065
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PagePatchRequested9b = new ExEventLog.EventTuple(3221487849U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FE2 RID: 4066
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LostFlushDetected9b = new ExEventLog.EventTuple(3221487850U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FE3 RID: 4067
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LostFlushDbTimeTooOld9b = new ExEventLog.EventTuple(3221487851U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FE4 RID: 4068
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcExceededMaxActiveDatabases9b = new ExEventLog.EventTuple(3221487852U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FE5 RID: 4069
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcSourceLogCorruptOutsideRequiredRange9b = new ExEventLog.EventTuple(3221487853U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FE6 RID: 4070
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcGeneric9b = new ExEventLog.EventTuple(3221487854U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FE7 RID: 4071
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtGeneric9b = new ExEventLog.EventTuple(3221487855U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FE8 RID: 4072
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcLogCorruptionDetectedByESE9b = new ExEventLog.EventTuple(3221487856U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FE9 RID: 4073
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtLogCorruptionDetectedByESE9b = new ExEventLog.EventTuple(3221487857U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FEA RID: 4074
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcFileSystemCorruption9b = new ExEventLog.EventTuple(3221487858U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FEB RID: 4075
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtFileSystemCorruption9b = new ExEventLog.EventTuple(3221487859U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FEC RID: 4076
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcHungIoLowThreshold9b = new ExEventLog.EventTuple(3221487860U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FED RID: 4077
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtHungIoLowThreshold9b = new ExEventLog.EventTuple(3221487861U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FEE RID: 4078
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcHungIoMediumThreshold9b = new ExEventLog.EventTuple(3221487862U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FEF RID: 4079
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtHungIoMediumThreshold9b = new ExEventLog.EventTuple(3221487863U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FF0 RID: 4080
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcHungIoExceededThresholdDoubleDisk9b = new ExEventLog.EventTuple(3221487864U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FF1 RID: 4081
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcHungIoExceededThreshold9b = new ExEventLog.EventTuple(3221487865U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FF2 RID: 4082
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcHungStoreWorker9b = new ExEventLog.EventTuple(3221487866U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FF3 RID: 4083
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcUnaccessibleStoreWorker9b = new ExEventLog.EventTuple(3221487868U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FF4 RID: 4084
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcMonitoredDatabaseFailed9b = new ExEventLog.EventTuple(3221487869U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FF5 RID: 4085
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogGapFatal9b = new ExEventLog.EventTuple(3221487870U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FF6 RID: 4086
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtMonitoredDatabaseFailed9b = new ExEventLog.EventTuple(3221487871U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FF7 RID: 4087
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcExceededDatabaseMaxSize9b = new ExEventLog.EventTuple(3221487872U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FF8 RID: 4088
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TgtLowDiskSpaceStraggler9b = new ExEventLog.EventTuple(3221487873U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000FF9 RID: 4089
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SrcLockedVolume9b = new ExEventLog.EventTuple(3221487874U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000389 RID: 905
		private enum Category : short
		{
			// Token: 0x04000FFB RID: 4091
			Database_recovery = 1,
			// Token: 0x04000FFC RID: 4092
			Ese,
			// Token: 0x04000FFD RID: 4093
			Store,
			// Token: 0x04000FFE RID: 4094
			Content_Indexing,
			// Token: 0x04000FFF RID: 4095
			Replay
		}

		// Token: 0x0200038A RID: 906
		internal enum Message : uint
		{
			// Token: 0x04001001 RID: 4097
			SrcConfiguration9a = 3221487717U,
			// Token: 0x04001002 RID: 4098
			TgtConfiguration9a,
			// Token: 0x04001003 RID: 4099
			SrcRepairable9a,
			// Token: 0x04001004 RID: 4100
			TgtRepairable9a,
			// Token: 0x04001005 RID: 4101
			Space9a,
			// Token: 0x04001006 RID: 4102
			SrcIoHard9a,
			// Token: 0x04001007 RID: 4103
			TgtIoHard9a,
			// Token: 0x04001008 RID: 4104
			SourceCorruption9a,
			// Token: 0x04001009 RID: 4105
			SrcCorruption9a,
			// Token: 0x0400100A RID: 4106
			TgtCorruption9a,
			// Token: 0x0400100B RID: 4107
			SrcHard9a,
			// Token: 0x0400100C RID: 4108
			TgtHard9a,
			// Token: 0x0400100D RID: 4109
			SrcUnrecoverable9a,
			// Token: 0x0400100E RID: 4110
			TgtUnrecoverable9a,
			// Token: 0x0400100F RID: 4111
			SrcRemount9a,
			// Token: 0x04001010 RID: 4112
			TgtRemount9a,
			// Token: 0x04001011 RID: 4113
			TgtReseed9a,
			// Token: 0x04001012 RID: 4114
			SrcPerformance9a,
			// Token: 0x04001013 RID: 4115
			MoveLoad9a,
			// Token: 0x04001014 RID: 4116
			SrcMemory9a,
			// Token: 0x04001015 RID: 4117
			TgtMemory9a,
			// Token: 0x04001016 RID: 4118
			SrcCatalogReseed9a,
			// Token: 0x04001017 RID: 4119
			TgtCatalogReseed9a,
			// Token: 0x04001018 RID: 4120
			AlertOnly9a,
			// Token: 0x04001019 RID: 4121
			UnexpectedDismount9a = 3221487742U,
			// Token: 0x0400101A RID: 4122
			SrcExceededMaxDatabases9a,
			// Token: 0x0400101B RID: 4123
			SrcGenericMountFailure9a,
			// Token: 0x0400101C RID: 4124
			PagePatchRequested9a,
			// Token: 0x0400101D RID: 4125
			PagePatchCompleted9a = 1074004098U,
			// Token: 0x0400101E RID: 4126
			LostFlushDetected9a = 3221487747U,
			// Token: 0x0400101F RID: 4127
			SrcSourceLogCorrupt9a,
			// Token: 0x04001020 RID: 4128
			SrcFailedToRepair9a,
			// Token: 0x04001021 RID: 4129
			LostFlushDbTimeTooOld9a,
			// Token: 0x04001022 RID: 4130
			TgtExceededMaxDatabases9a,
			// Token: 0x04001023 RID: 4131
			SrcLostFlushDbTimeTooNew9a,
			// Token: 0x04001024 RID: 4132
			TgtLostFlushDbTimeTooNew9a,
			// Token: 0x04001025 RID: 4133
			SrcReseed9a,
			// Token: 0x04001026 RID: 4134
			TgtPerformance9a,
			// Token: 0x04001027 RID: 4135
			TgtFailedToRepair9a,
			// Token: 0x04001028 RID: 4136
			UnexpectedDismountMoveWasSkipped9a = 1074004109U,
			// Token: 0x04001029 RID: 4137
			TgtGenericMountFailure9a = 3221487758U,
			// Token: 0x0400102A RID: 4138
			SrcExceededMaxActiveDatabases9a,
			// Token: 0x0400102B RID: 4139
			TgtExceededMaxActiveDatabases9a,
			// Token: 0x0400102C RID: 4140
			SrcSourceLogCorruptOutsideRequiredRange9a,
			// Token: 0x0400102D RID: 4141
			SrcGeneric9a,
			// Token: 0x0400102E RID: 4142
			TgtGeneric9a,
			// Token: 0x0400102F RID: 4143
			SrcLogCorruptionDetectedByESE9a,
			// Token: 0x04001030 RID: 4144
			TgtLogCorruptionDetectedByESE9a,
			// Token: 0x04001031 RID: 4145
			SrcFileSystemCorruption9a,
			// Token: 0x04001032 RID: 4146
			TgtFileSystemCorruption9a,
			// Token: 0x04001033 RID: 4147
			SrcHungIoLowThreshold9a,
			// Token: 0x04001034 RID: 4148
			TgtHungIoLowThreshold9a,
			// Token: 0x04001035 RID: 4149
			SrcHungIoMediumThreshold9a,
			// Token: 0x04001036 RID: 4150
			TgtHungIoMediumThreshold9a,
			// Token: 0x04001037 RID: 4151
			SrcHungIoExceededThresholdDoubleDisk9a,
			// Token: 0x04001038 RID: 4152
			TgtHungIoExceededThresholdDoubleDisk9a,
			// Token: 0x04001039 RID: 4153
			SrcHungIoExceededThreshold9a,
			// Token: 0x0400103A RID: 4154
			TgtHungIoExceededThreshold9a,
			// Token: 0x0400103B RID: 4155
			SrcHungIoCancelFailed9a,
			// Token: 0x0400103C RID: 4156
			TgtHungIoCancelFailed9a,
			// Token: 0x0400103D RID: 4157
			SrcHungIoCancelSucceeded9a,
			// Token: 0x0400103E RID: 4158
			TgtHungIoCancelSucceeded9a,
			// Token: 0x0400103F RID: 4159
			SrcHungStoreWorker9a,
			// Token: 0x04001040 RID: 4160
			TgtHungStoreWorker9a,
			// Token: 0x04001041 RID: 4161
			SrcUnaccessibleStoreWorker9a,
			// Token: 0x04001042 RID: 4162
			TgtUnaccessibleStoreWorker9a,
			// Token: 0x04001043 RID: 4163
			SrcMonitoredDatabaseFailed9a,
			// Token: 0x04001044 RID: 4164
			LogGapFatal9a,
			// Token: 0x04001045 RID: 4165
			TgtMonitoredDatabaseFailed9a,
			// Token: 0x04001046 RID: 4166
			SrcExceededDatabaseMaxSize9a,
			// Token: 0x04001047 RID: 4167
			TgtExceededDatabaseMaxSize9a,
			// Token: 0x04001048 RID: 4168
			TgtLowDiskSpaceStraggler9a,
			// Token: 0x04001049 RID: 4169
			SrcLockedVolume9a,
			// Token: 0x0400104A RID: 4170
			TgtLockedVolume9a,
			// Token: 0x0400104B RID: 4171
			SrcConfiguration9b = 3221487817U,
			// Token: 0x0400104C RID: 4172
			TgtConfiguration9b,
			// Token: 0x0400104D RID: 4173
			SrcRepairable9b,
			// Token: 0x0400104E RID: 4174
			Space9b = 3221487821U,
			// Token: 0x0400104F RID: 4175
			SrcIoHard9b,
			// Token: 0x04001050 RID: 4176
			TgtIoHard9b,
			// Token: 0x04001051 RID: 4177
			SourceCorruption9b,
			// Token: 0x04001052 RID: 4178
			SrcCorruption9b,
			// Token: 0x04001053 RID: 4179
			TgtCorruption9b,
			// Token: 0x04001054 RID: 4180
			SrcHard9b,
			// Token: 0x04001055 RID: 4181
			TgtHard9b,
			// Token: 0x04001056 RID: 4182
			SrcUnrecoverable9b,
			// Token: 0x04001057 RID: 4183
			TgtUnrecoverable9b,
			// Token: 0x04001058 RID: 4184
			SrcRemount9b,
			// Token: 0x04001059 RID: 4185
			TgtReseed9b = 3221487833U,
			// Token: 0x0400105A RID: 4186
			SrcPerformance9b,
			// Token: 0x0400105B RID: 4187
			MoveLoad9b,
			// Token: 0x0400105C RID: 4188
			SrcMemory9b,
			// Token: 0x0400105D RID: 4189
			SrcCatalogReseed9b = 3221487838U,
			// Token: 0x0400105E RID: 4190
			SrcSourceLogCorrupt9b = 3221487840U,
			// Token: 0x0400105F RID: 4191
			UnexpectedDismount9b,
			// Token: 0x04001060 RID: 4192
			SrcExceededMaxDatabases9b,
			// Token: 0x04001061 RID: 4193
			TgtExceededMaxDatabases9b,
			// Token: 0x04001062 RID: 4194
			SrcFailedToRepair9b,
			// Token: 0x04001063 RID: 4195
			TgtFailedToRepair9b,
			// Token: 0x04001064 RID: 4196
			TgtLostFlushDbTimeTooNew9b,
			// Token: 0x04001065 RID: 4197
			SrcGenericMountFailure9b,
			// Token: 0x04001066 RID: 4198
			AlertOnly9b,
			// Token: 0x04001067 RID: 4199
			PagePatchRequested9b,
			// Token: 0x04001068 RID: 4200
			LostFlushDetected9b,
			// Token: 0x04001069 RID: 4201
			LostFlushDbTimeTooOld9b,
			// Token: 0x0400106A RID: 4202
			SrcExceededMaxActiveDatabases9b,
			// Token: 0x0400106B RID: 4203
			SrcSourceLogCorruptOutsideRequiredRange9b,
			// Token: 0x0400106C RID: 4204
			SrcGeneric9b,
			// Token: 0x0400106D RID: 4205
			TgtGeneric9b,
			// Token: 0x0400106E RID: 4206
			SrcLogCorruptionDetectedByESE9b,
			// Token: 0x0400106F RID: 4207
			TgtLogCorruptionDetectedByESE9b,
			// Token: 0x04001070 RID: 4208
			SrcFileSystemCorruption9b,
			// Token: 0x04001071 RID: 4209
			TgtFileSystemCorruption9b,
			// Token: 0x04001072 RID: 4210
			SrcHungIoLowThreshold9b,
			// Token: 0x04001073 RID: 4211
			TgtHungIoLowThreshold9b,
			// Token: 0x04001074 RID: 4212
			SrcHungIoMediumThreshold9b,
			// Token: 0x04001075 RID: 4213
			TgtHungIoMediumThreshold9b,
			// Token: 0x04001076 RID: 4214
			SrcHungIoExceededThresholdDoubleDisk9b,
			// Token: 0x04001077 RID: 4215
			SrcHungIoExceededThreshold9b,
			// Token: 0x04001078 RID: 4216
			SrcHungStoreWorker9b,
			// Token: 0x04001079 RID: 4217
			SrcUnaccessibleStoreWorker9b = 3221487868U,
			// Token: 0x0400107A RID: 4218
			SrcMonitoredDatabaseFailed9b,
			// Token: 0x0400107B RID: 4219
			LogGapFatal9b,
			// Token: 0x0400107C RID: 4220
			TgtMonitoredDatabaseFailed9b,
			// Token: 0x0400107D RID: 4221
			SrcExceededDatabaseMaxSize9b,
			// Token: 0x0400107E RID: 4222
			TgtLowDiskSpaceStraggler9b,
			// Token: 0x0400107F RID: 4223
			SrcLockedVolume9b
		}
	}
}
