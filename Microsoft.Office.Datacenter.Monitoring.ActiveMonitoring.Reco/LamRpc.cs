using System;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000015 RID: 21
	public class LamRpc : ILamRpc
	{
		// Token: 0x06000086 RID: 134 RVA: 0x00003918 File Offset: 0x00001B18
		public RpcGetThrottlingStatisticsImpl.ThrottlingStatistics GetThrottlingStatistics(string serverName, RecoveryActionId actionId, string resourceName, int maxAllowedInOneHour, int maxAllowedInOneDay, bool isStopSearchWhenLimitExceeds, bool isCountFailedActions, int timeoutInMsec)
		{
			return RpcGetThrottlingStatisticsImpl.SendRequest(serverName, actionId, resourceName, maxAllowedInOneHour, maxAllowedInOneDay, isStopSearchWhenLimitExceeds, isCountFailedActions, 30000);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000393C File Offset: 0x00001B3C
		public void UpdateRecoveryActionEntry(string serverName, RecoveryActionEntry entry, int timeoutInMsec)
		{
			RpcUpdateRecoveryActionEntryImpl.SendRequest(serverName, entry, timeoutInMsec);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003946 File Offset: 0x00001B46
		public RpcSetThrottlingInProgressImpl.Reply SetThrottlingInProgress(string serverName, RpcSetThrottlingInProgressImpl.ThrottlingProgressInfo progressInfo, bool isClear, bool isForce, int timeoutInMsec)
		{
			return RpcSetThrottlingInProgressImpl.SendRequest(serverName, progressInfo, isClear, isForce, timeoutInMsec);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003954 File Offset: 0x00001B54
		public void SetWorkerProcessInfo(string serverName, RpcSetWorkerProcessInfoImpl.WorkerProcessInfo info, int timeoutInMsec = 30000)
		{
			RpcSetWorkerProcessInfoImpl.SendRequest(serverName, info, timeoutInMsec);
		}
	}
}
