using System;
using System.Collections.Generic;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200002B RID: 43
	public interface IResponderWorkBroker : IWorkBrokerBase
	{
		// Token: 0x0600031F RID: 799
		IDataAccessQuery<MonitorResult> GetMonitorResults(string alertMask, DateTime startTime, DateTime endTime);

		// Token: 0x06000320 RID: 800
		IDataAccessQuery<ResponderResult> GetResponderResults(ResponderDefinition definition, DateTime startTime);

		// Token: 0x06000321 RID: 801
		IDataAccessQuery<ProbeResult> GetProbeResult(int probeId, int resultId);

		// Token: 0x06000322 RID: 802
		IDataAccessQuery<ProbeResult> GetProbeResults(string sampleMask, DateTime startTime, DateTime endTime);

		// Token: 0x06000323 RID: 803
		IDataAccessQuery<ProbeResult> GetProbeResults(string scopeName, DateTime startTime);

		// Token: 0x06000324 RID: 804
		IDataAccessQuery<MonitorResult> GetLastSuccessfulMonitorResult(string alertMask, DateTime startTime, DateTime endTime);

		// Token: 0x06000325 RID: 805
		IDataAccessQuery<MonitorResult> GetLastSuccessfulMonitorResult(int workItemId);

		// Token: 0x06000326 RID: 806
		IDataAccessQuery<ResponderResult> GetLastSuccessfulResponderResult(ResponderDefinition definition);

		// Token: 0x06000327 RID: 807
		IDataAccessQuery<ResponderResult> GetLastSuccessfulResponderResult(ResponderDefinition definition, TimeSpan searchWindow);

		// Token: 0x06000328 RID: 808
		IDataAccessQuery<ResponderResult> GetLastSuccessfulRecoveryAttemptedResponderResult(ResponderDefinition definition, TimeSpan searchWindow);

		// Token: 0x06000329 RID: 809
		IDataAccessQuery<ResponderResult> GetLastSuccessfulRecoveryAttemptedResponderResultByName(ResponderDefinition definition, TimeSpan searchWindow);

		// Token: 0x0600032A RID: 810
		IDataAccessQuery<MonitorResult> GetSuccessfulMonitorResults(DateTime startTime, DateTime endTime);

		// Token: 0x0600032B RID: 811
		IDataAccessQuery<MonitorDefinition> GetMonitorDefinitions(DateTime startTime);

		// Token: 0x0600032C RID: 812
		IDataAccessQuery<MonitorDefinition> GetMonitorDefinition(int workItemId);

		// Token: 0x0600032D RID: 813
		IDataAccessQuery<TEntity> AsDataAccessQuery<TEntity>(IEnumerable<TEntity> query);
	}
}
