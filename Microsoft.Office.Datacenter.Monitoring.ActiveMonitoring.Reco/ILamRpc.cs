using System;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000014 RID: 20
	public interface ILamRpc
	{
		// Token: 0x06000082 RID: 130
		RpcGetThrottlingStatisticsImpl.ThrottlingStatistics GetThrottlingStatistics(string serverName, RecoveryActionId actionId, string resourceName, int maxAllowedInOneHour, int maxAllowedInOneDay, bool isStopSearchWhenLimitExceeds, bool isCountFailedActions, int timeoutInMsec = 30000);

		// Token: 0x06000083 RID: 131
		void UpdateRecoveryActionEntry(string serverName, RecoveryActionEntry entry, int timeoutInMsec = 30000);

		// Token: 0x06000084 RID: 132
		RpcSetThrottlingInProgressImpl.Reply SetThrottlingInProgress(string serverName, RpcSetThrottlingInProgressImpl.ThrottlingProgressInfo progressInfo, bool isClear, bool isForce = false, int timeoutInMsec = 30000);

		// Token: 0x06000085 RID: 133
		void SetWorkerProcessInfo(string serverName, RpcSetWorkerProcessInfoImpl.WorkerProcessInfo info, int timeoutInMsec = 30000);
	}
}
