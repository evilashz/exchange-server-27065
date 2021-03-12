using System;
using System.Collections.Generic;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Triggers
{
	// Token: 0x02000033 RID: 51
	public sealed class CASRoutingLatencyTrigger : CASTriggerBase
	{
		// Token: 0x0600011C RID: 284 RVA: 0x00009330 File Offset: 0x00007530
		public CASRoutingLatencyTrigger(IJob job) : base(job, string.Format("MSExchange HttpProxy Per Site(.+?)\\\\Routing Latency {0} Percentile", Configuration.GetConfigString("CASRoutingLatencyTriggerPercentile", "80th")), new PerfLogCounterTrigger.TriggerConfiguration("CASRoutingLatencyTrigger", double.NaN, 3000.0, TimeSpan.FromMinutes(5.0), TimeSpan.FromMinutes(15.0), TimeSpan.FromMinutes(5.0), 0), CASRoutingLatencyTrigger.requestsPerSecondMeasurement, 0.4, CASRoutingLatencyTrigger.additionalContext, CASRoutingLatencyTrigger.excludedInstances)
		{
		}

		// Token: 0x0400013F RID: 319
		private const string TriggerPrefix = "CASRoutingLatencyTrigger";

		// Token: 0x04000140 RID: 320
		private static readonly DiagnosticMeasurement requestsPerSecondMeasurement = DiagnosticMeasurement.GetMeasure("MSExchange HttpProxy Per Site", "Proxy Requests with Latency Data/Sec");

		// Token: 0x04000141 RID: 321
		private static readonly HashSet<string> excludedInstances = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"Unknown"
		};

		// Token: 0x04000142 RID: 322
		private static readonly HashSet<DiagnosticMeasurement> additionalContext = new HashSet<DiagnosticMeasurement>(DiagnosticMeasurement.CounterFilterComparer.Comparer)
		{
			CASRoutingLatencyTrigger.requestsPerSecondMeasurement
		};
	}
}
