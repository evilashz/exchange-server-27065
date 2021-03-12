using System;

namespace Microsoft.Exchange.Data.Storage.ActiveMonitoring
{
	// Token: 0x02000320 RID: 800
	internal enum ActiveMonitoringGenericRpcCommandId
	{
		// Token: 0x0400154A RID: 5450
		Unknown,
		// Token: 0x0400154B RID: 5451
		GetLastBugcheckStatus,
		// Token: 0x0400154C RID: 5452
		GetRecoveryActionStatus,
		// Token: 0x0400154D RID: 5453
		GetMonitorHealthStatus,
		// Token: 0x0400154E RID: 5454
		SetServerMonitor,
		// Token: 0x0400154F RID: 5455
		RequestObserver,
		// Token: 0x04001550 RID: 5456
		CancelObserver,
		// Token: 0x04001551 RID: 5457
		ObserverHeartbeat,
		// Token: 0x04001552 RID: 5458
		UpdateHealthStatus,
		// Token: 0x04001553 RID: 5459
		GetServerComponentStatus,
		// Token: 0x04001554 RID: 5460
		GetMonitoringItemIdentity,
		// Token: 0x04001555 RID: 5461
		GetMonitoringItemHelp,
		// Token: 0x04001556 RID: 5462
		InvokeMonitoringProbe,
		// Token: 0x04001557 RID: 5463
		GetRecoveryActionQuotaInfo,
		// Token: 0x04001558 RID: 5464
		LogCrimsonEvent,
		// Token: 0x04001559 RID: 5465
		LockHealthSetEscalationStateIfRequired,
		// Token: 0x0400155A RID: 5466
		SetHealthSetEscalationState,
		// Token: 0x0400155B RID: 5467
		GetThrottlingStatistics,
		// Token: 0x0400155C RID: 5468
		UpdateRecoveryActionEntry,
		// Token: 0x0400155D RID: 5469
		SetThrottlingInProgress,
		// Token: 0x0400155E RID: 5470
		SetWorkerProcessInfo,
		// Token: 0x0400155F RID: 5471
		GetCrimsonMostRecentResultInfo
	}
}
