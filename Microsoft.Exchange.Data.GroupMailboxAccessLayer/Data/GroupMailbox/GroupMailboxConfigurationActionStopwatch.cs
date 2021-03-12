using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000037 RID: 55
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GroupMailboxConfigurationActionStopwatch : IDisposable
	{
		// Token: 0x0600019C RID: 412 RVA: 0x0000C0B0 File Offset: 0x0000A2B0
		public GroupMailboxConfigurationActionStopwatch(GroupMailboxConfigurationReport report, GroupMailboxConfigurationAction actionType)
		{
			this.report = report;
			this.actionType = actionType;
			this.activityScope = ActivityContext.GetCurrentActivityScope();
			this.stopwatch = Stopwatch.StartNew();
			this.initialLatencyStatistics = this.CreateLatencyStatisticsSnapshot();
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000C0E8 File Offset: 0x0000A2E8
		public void Dispose()
		{
			this.stopwatch.Stop();
			this.report.IsConfigurationExecuted = true;
			LatencyStatistics value = this.CreateLatencyStatisticsSnapshot() - this.initialLatencyStatistics;
			this.report.ConfigurationActionLatencyStatistics[this.actionType] = value;
			GroupMailboxConfigurationActionStopwatch.Tracer.TraceDebug<GroupMailboxConfigurationAction, string>((long)this.GetHashCode(), "Completed {0} in {1}ms.", this.actionType, value.ElapsedTime.TotalMilliseconds.ToString("n0"));
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000C170 File Offset: 0x0000A370
		private LatencyStatistics CreateLatencyStatisticsSnapshot()
		{
			if (this.activityScope != null)
			{
				return new LatencyStatistics
				{
					ElapsedTime = this.stopwatch.Elapsed,
					ADLatency = new AggregatedOperationStatistics?(this.activityScope.TakeStatisticsSnapshot(AggregatedOperationType.ADCalls)),
					RpcLatency = new AggregatedOperationStatistics?(this.activityScope.TakeStatisticsSnapshot(AggregatedOperationType.StoreRPCs))
				};
			}
			return new LatencyStatistics
			{
				ElapsedTime = this.stopwatch.Elapsed
			};
		}

		// Token: 0x040000D7 RID: 215
		private static readonly Microsoft.Exchange.Diagnostics.Trace Tracer = ExTraceGlobals.GroupMailboxAccessLayerTracer;

		// Token: 0x040000D8 RID: 216
		private readonly Stopwatch stopwatch;

		// Token: 0x040000D9 RID: 217
		private readonly IActivityScope activityScope;

		// Token: 0x040000DA RID: 218
		private readonly LatencyStatistics initialLatencyStatistics;

		// Token: 0x040000DB RID: 219
		private readonly GroupMailboxConfigurationReport report;

		// Token: 0x040000DC RID: 220
		private readonly GroupMailboxConfigurationAction actionType;
	}
}
