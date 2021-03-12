using System;
using System.Collections.Generic;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Triggers
{
	// Token: 0x02000048 RID: 72
	public abstract class TransportOverThresholdGatedTrigger : TransportGatedTrigger
	{
		// Token: 0x06000159 RID: 345 RVA: 0x0000A7AB File Offset: 0x000089AB
		protected TransportOverThresholdGatedTrigger(IJob job, string counterNamePattern, double gatingCounterThreshold, PerfLogCounterTrigger.TriggerConfiguration configuration, HashSet<DiagnosticMeasurement> additionalCounters, HashSet<string> excludedInstances) : base(job, counterNamePattern, gatingCounterThreshold, configuration, additionalCounters, excludedInstances)
		{
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000A7BC File Offset: 0x000089BC
		protected override bool ShouldTrigger(PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
			bool flag = false;
			bool flag2 = base.ShouldTrigger(context);
			if (flag2)
			{
				DiagnosticMeasurement diagnosticMeasurement = this.AdditionalDiagnosticMeasurement(context);
				ValueStatistics valueStatistics;
				if (diagnosticMeasurement != null && context.AdditionalData.TryGetValue(diagnosticMeasurement, out valueStatistics))
				{
					float? last = valueStatistics.Last;
					double gatingCounterThreshold = base.GatingCounterThreshold;
					if ((double)last.GetValueOrDefault() >= gatingCounterThreshold && last != null)
					{
						flag = true;
					}
				}
			}
			return flag2 && flag;
		}
	}
}
