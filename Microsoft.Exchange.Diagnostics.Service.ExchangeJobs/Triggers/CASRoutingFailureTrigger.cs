using System;
using System.Collections.Generic;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Triggers
{
	// Token: 0x02000032 RID: 50
	public sealed class CASRoutingFailureTrigger : CASTriggerBase
	{
		// Token: 0x0600011A RID: 282 RVA: 0x0000925C File Offset: 0x0000745C
		public CASRoutingFailureTrigger(IJob job) : base(job, "MSExchange HttpProxy Per Site(.+?)\\\\Routing Failure Percentage", new PerfLogCounterTrigger.TriggerConfiguration("CASRoutingFailureTrigger", double.NaN, 50.0, TimeSpan.FromMinutes(5.0), TimeSpan.FromMinutes(15.0), TimeSpan.FromMinutes(5.0), 0), CASRoutingFailureTrigger.requestsPerSecondMeasurement, 0.4, CASRoutingFailureTrigger.additionalContext, CASRoutingFailureTrigger.excludedInstances)
		{
		}

		// Token: 0x0400013C RID: 316
		private static readonly DiagnosticMeasurement requestsPerSecondMeasurement = DiagnosticMeasurement.GetMeasure("MSExchange HttpProxy Per Site", "Failed Requests/Sec");

		// Token: 0x0400013D RID: 317
		private static readonly HashSet<string> excludedInstances = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400013E RID: 318
		private static readonly HashSet<DiagnosticMeasurement> additionalContext = new HashSet<DiagnosticMeasurement>(DiagnosticMeasurement.CounterFilterComparer.Comparer)
		{
			CASRoutingFailureTrigger.requestsPerSecondMeasurement
		};
	}
}
