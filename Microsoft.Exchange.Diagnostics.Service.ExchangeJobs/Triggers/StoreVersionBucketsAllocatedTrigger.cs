using System;
using System.Collections.Generic;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Triggers
{
	// Token: 0x02000045 RID: 69
	public class StoreVersionBucketsAllocatedTrigger : PerInstanceTrigger
	{
		// Token: 0x0600014D RID: 333 RVA: 0x0000A5BC File Offset: 0x000087BC
		public StoreVersionBucketsAllocatedTrigger(IJob job) : base(job, "MSExchange Database ==> Instances\\(Information Store.+?\\)\\\\Version Buckets Allocated", new PerfLogCounterTrigger.TriggerConfiguration("StoreVersionBucketsAllocatedTrigger", double.NaN, 12000.0, TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(5.0), TimeSpan.FromMinutes(2.0), 0), new HashSet<DiagnosticMeasurement>(), StoreVersionBucketsAllocatedTrigger.excludedInstances)
		{
		}

		// Token: 0x04000173 RID: 371
		private const double VersionBucketsAllocatedThreshold = 12000.0;

		// Token: 0x04000174 RID: 372
		private static readonly HashSet<string> excludedInstances = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"_Total"
		};
	}
}
