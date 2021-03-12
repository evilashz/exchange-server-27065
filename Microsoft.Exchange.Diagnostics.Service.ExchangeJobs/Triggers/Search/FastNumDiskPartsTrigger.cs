using System;
using System.Collections.Generic;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Triggers.Search
{
	// Token: 0x0200003D RID: 61
	public class FastNumDiskPartsTrigger : PerInstanceTrigger
	{
		// Token: 0x06000132 RID: 306 RVA: 0x00009CAC File Offset: 0x00007EAC
		public FastNumDiskPartsTrigger(IJob job) : base(job, "Search Fs\\(.+?\\.Single\\)\\\\NumDiskParts", new PerfLogCounterTrigger.TriggerConfiguration("FastNumDiskPartsTrigger", 100.0, double.MaxValue, TimeSpan.FromMinutes(5.0), TimeSpan.FromHours(4.0), TimeSpan.FromDays(1.0), 0), new HashSet<DiagnosticMeasurement>(), new HashSet<string>())
		{
		}
	}
}
