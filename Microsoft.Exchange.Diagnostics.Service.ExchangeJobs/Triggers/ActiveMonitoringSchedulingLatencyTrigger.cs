using System;
using System.Collections.Generic;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Triggers
{
	// Token: 0x0200003A RID: 58
	public class ActiveMonitoringSchedulingLatencyTrigger : PerInstanceTrigger
	{
		// Token: 0x0600012B RID: 299 RVA: 0x0000974C File Offset: 0x0000794C
		public ActiveMonitoringSchedulingLatencyTrigger(IJob job) : base(job, "MSExchangeWorkerTaskFramework\\(.+?\\)\\\\Scheduling Latency", new PerfLogCounterTrigger.TriggerConfiguration("ActiveMonitoringSchedulingLatencyTrigger", double.NaN, 100.0, TimeSpan.FromMinutes(5.0), TimeSpan.FromMinutes(60.0), TimeSpan.FromMinutes(5.0), 0), new HashSet<DiagnosticMeasurement>(), ActiveMonitoringSchedulingLatencyTrigger.excludedInstances)
		{
		}

		// Token: 0x04000156 RID: 342
		private const double SchedulingLatencyThreshold = 100.0;

		// Token: 0x04000157 RID: 343
		private static readonly HashSet<string> excludedInstances = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"_Total"
		};
	}
}
