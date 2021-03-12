using System;
using System.Collections.Generic;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000012 RID: 18
	public interface IMonitorWorkBroker : IWorkBrokerBase
	{
		// Token: 0x060000CD RID: 205
		IDataAccessQuery<ProbeResult> GetProbeResults(string sampleMask, DateTime startTime, DateTime endTime);

		// Token: 0x060000CE RID: 206
		IDataAccessQuery<ProbeResult> GetProbeResult(int probeId, int resultId);

		// Token: 0x060000CF RID: 207
		IDataAccessQuery<MonitorResult> GetSuccessfulMonitorResults(MonitorDefinition definition, DateTime startTime);

		// Token: 0x060000D0 RID: 208
		IDataAccessQuery<MonitorResult> GetSuccessfulMonitorResults(Component component, DateTime startTime);

		// Token: 0x060000D1 RID: 209
		IDataAccessQuery<MonitorResult> GetSuccessfulMonitorResults(DateTime startTime, DateTime endTime);

		// Token: 0x060000D2 RID: 210
		IDataAccessQuery<MonitorResult> GetLastSuccessfulMonitorResult(MonitorDefinition definition);

		// Token: 0x060000D3 RID: 211
		IDataAccessQuery<MonitorResult> GetLastMonitorResult(MonitorDefinition definition, TimeSpan searchWindow);

		// Token: 0x060000D4 RID: 212
		IDataAccessQuery<MonitorDefinition> GetMonitorDefinitions(DateTime startTime);

		// Token: 0x060000D5 RID: 213
		IDataAccessQuery<TEntity> AsDataAccessQuery<TEntity>(IEnumerable<TEntity> query);
	}
}
