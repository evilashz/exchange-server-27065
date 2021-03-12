using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000022 RID: 34
	public class ProbeWorkBroker<TDataAccess> : TypedWorkBroker<ProbeDefinition, ProbeWorkItem, ProbeResult, TDataAccess>, IProbeWorkBroker, IWorkBrokerBase where TDataAccess : DataAccess, new()
	{
		// Token: 0x0600025F RID: 607 RVA: 0x0000B3BC File Offset: 0x000095BC
		public ProbeWorkBroker(WorkItemFactory factory) : base(factory)
		{
			if (Settings.EnableStreamInsightPush)
			{
				this.publisher = new DataInsightsPublisher();
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000B3D7 File Offset: 0x000095D7
		public IDataAccessQuery<ProbeResult> GetProbeResults(ProbeDefinition definition, DateTime startTime)
		{
			return base.GetResultsQuery(definition, startTime);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000B3FC File Offset: 0x000095FC
		public IDataAccessQuery<ProbeResult> GetProbeResults(string sampleMask, DateTime startTime, DateTime endTime)
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			IEnumerable<ProbeResult> query = from r in base.GetResultsQuery<ProbeResult>(sampleMask, startTime)
			where r.ExecutionEndTime <= endTime
			select r;
			return tdataAccess.AsDataAccessQuery<ProbeResult>(query);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000B444 File Offset: 0x00009644
		public Task<StatusEntryCollection> GetStatusEntries(string key, CancellationToken cancellationToken, TracingContext traceContext)
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			return tdataAccess.GetStatusEntries(key, cancellationToken, traceContext);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000B467 File Offset: 0x00009667
		public Task SaveStatusEntries(StatusEntryCollection entries, CancellationToken cancellationToken, TracingContext traceContext)
		{
			return base.SaveStatusEntriesInternal(entries, cancellationToken, traceContext);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000B472 File Offset: 0x00009672
		public void PublishResult(ProbeResult result)
		{
			this.PublishResult(result, result.TraceContext);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000B481 File Offset: 0x00009681
		internal override void PublishResult(WorkItemResult result, TracingContext traceContext)
		{
			base.PublishResult(result, traceContext);
			if (Settings.EnableStreamInsightPush)
			{
				this.publisher.PublishToInsightsEngine(result as ProbeResult);
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000B4A3 File Offset: 0x000096A3
		bool IWorkBrokerBase.IsLocal()
		{
			return base.IsLocal();
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000B4AB File Offset: 0x000096AB
		TimeSpan IWorkBrokerBase.get_DefaultResultWindow()
		{
			return base.DefaultResultWindow;
		}

		// Token: 0x04000247 RID: 583
		private DataInsightsPublisher publisher;
	}
}
