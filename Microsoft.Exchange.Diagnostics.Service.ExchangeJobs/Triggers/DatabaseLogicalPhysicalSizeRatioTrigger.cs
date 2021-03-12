using System;
using System.Collections.Generic;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Triggers
{
	// Token: 0x0200003E RID: 62
	public class DatabaseLogicalPhysicalSizeRatioTrigger : PerInstanceTrigger
	{
		// Token: 0x06000134 RID: 308 RVA: 0x00009D44 File Offset: 0x00007F44
		public DatabaseLogicalPhysicalSizeRatioTrigger(IJob job) : base(job, string.Format("MSExchangeIS Store\\(.+?\\)\\\\{0}", "Logical To Physical Size Ratio"), new PerfLogCounterTrigger.TriggerConfiguration("DatabaseLogicalPhysicalSizeRatioTrigger", double.NaN, 0.9, TimeSpan.FromMinutes(5.0), TimeSpan.FromMinutes(10.0), TimeSpan.FromMinutes(5.0), 1), new HashSet<DiagnosticMeasurement>(), DatabaseLogicalPhysicalSizeRatioTrigger.excludedInstances)
		{
		}

		// Token: 0x04000162 RID: 354
		private const double LogicalPhysicalRatioThreshold = 0.9;

		// Token: 0x04000163 RID: 355
		private static readonly HashSet<string> excludedInstances = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"_Total"
		};
	}
}
