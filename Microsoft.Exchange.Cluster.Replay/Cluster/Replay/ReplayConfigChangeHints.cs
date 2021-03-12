using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000104 RID: 260
	internal enum ReplayConfigChangeHints
	{
		// Token: 0x04000454 RID: 1108
		None,
		// Token: 0x04000455 RID: 1109
		PeriodicFullScan,
		// Token: 0x04000456 RID: 1110
		DbCopyAdded,
		// Token: 0x04000457 RID: 1111
		DbCopyRemoved,
		// Token: 0x04000458 RID: 1112
		AmAttemptCopyLastLogs,
		// Token: 0x04000459 RID: 1113
		AmPreMountCallback,
		// Token: 0x0400045A RID: 1114
		AmPreMountCallbackRI,
		// Token: 0x0400045B RID: 1115
		DbSeeder,
		// Token: 0x0400045C RID: 1116
		DbResumeBefore,
		// Token: 0x0400045D RID: 1117
		DbResumeAfter,
		// Token: 0x0400045E RID: 1118
		DbSuspendBefore,
		// Token: 0x0400045F RID: 1119
		DbSuspendAfter,
		// Token: 0x04000460 RID: 1120
		RunConfigUpdaterRpc,
		// Token: 0x04000461 RID: 1121
		AmMultiNodeReplicaNotifier,
		// Token: 0x04000462 RID: 1122
		MoveDatabasePathConfigChanged,
		// Token: 0x04000463 RID: 1123
		DbSyncSuspendResumeStateBefore,
		// Token: 0x04000464 RID: 1124
		AmStoreServiceStartDetected,
		// Token: 0x04000465 RID: 1125
		AmPreMountCallbackLogStreamReset,
		// Token: 0x04000466 RID: 1126
		LogReplayerHitLogCorruption,
		// Token: 0x04000467 RID: 1127
		LogCopierSkippingPastLog,
		// Token: 0x04000468 RID: 1128
		IncReseedRedirtiedDatabase,
		// Token: 0x04000469 RID: 1129
		IncReseedCompleted,
		// Token: 0x0400046A RID: 1130
		DisableReplayLag,
		// Token: 0x0400046B RID: 1131
		EnableReplayLag,
		// Token: 0x0400046C RID: 1132
		AutoReseed
	}
}
