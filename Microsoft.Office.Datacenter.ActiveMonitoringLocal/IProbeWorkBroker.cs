using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000021 RID: 33
	public interface IProbeWorkBroker : IWorkBrokerBase
	{
		// Token: 0x06000259 RID: 601
		IDataAccessQuery<ProbeResult> GetProbeResults(ProbeDefinition definition, DateTime startTime);

		// Token: 0x0600025A RID: 602
		IDataAccessQuery<ProbeResult> GetProbeResults(string sampleMask, DateTime startTime, DateTime endTime);

		// Token: 0x0600025B RID: 603
		Task<StatusEntryCollection> GetStatusEntries(string key, CancellationToken cancellationToken, TracingContext traceContext);

		// Token: 0x0600025C RID: 604
		Task SaveStatusEntries(StatusEntryCollection entries, CancellationToken cancellationToken, TracingContext traceContext);

		// Token: 0x0600025D RID: 605
		void PublishResult(ProbeResult result);

		// Token: 0x0600025E RID: 606
		IDataAccessQuery<TEntity> AsDataAccessQuery<TEntity>(IEnumerable<TEntity> query);
	}
}
