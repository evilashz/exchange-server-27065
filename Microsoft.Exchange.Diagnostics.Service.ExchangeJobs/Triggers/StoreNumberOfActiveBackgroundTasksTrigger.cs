using System;
using System.Collections.Generic;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Triggers
{
	// Token: 0x02000040 RID: 64
	public class StoreNumberOfActiveBackgroundTasksTrigger : PerInstanceTrigger
	{
		// Token: 0x0600013D RID: 317 RVA: 0x0000A1B8 File Offset: 0x000083B8
		public StoreNumberOfActiveBackgroundTasksTrigger(IJob job) : base(job, "MSExchangeIS Store\\(.+?\\)\\\\Number of active background tasks", new PerfLogCounterTrigger.TriggerConfiguration("StoreNumberOfActiveBackgroundTasksTrigger", double.NaN, 15.0, TimeSpan.FromMinutes(5.0), TimeSpan.FromMinutes(15.0), TimeSpan.FromMinutes(5.0), 0), new HashSet<DiagnosticMeasurement>(), StoreNumberOfActiveBackgroundTasksTrigger.excludedInstances)
		{
		}

		// Token: 0x0400016B RID: 363
		private const double NumberOfActiveBackgroundTasksThreshold = 15.0;

		// Token: 0x0400016C RID: 364
		private static readonly HashSet<string> excludedInstances = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"_Total"
		};
	}
}
