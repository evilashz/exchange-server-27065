using System;
using System.Collections.Generic;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Triggers
{
	// Token: 0x0200004E RID: 78
	public abstract class TransportUnderThresholdGatedTrigger : TransportGatedTrigger
	{
		// Token: 0x0600016A RID: 362 RVA: 0x0000ACDB File Offset: 0x00008EDB
		protected TransportUnderThresholdGatedTrigger(IJob job, string counterNamePattern, double gatingCounterThreshold, PerfLogCounterTrigger.TriggerConfiguration configuration, HashSet<DiagnosticMeasurement> additionalCounters, HashSet<string> excludedInstances) : base(job, counterNamePattern, gatingCounterThreshold, configuration, additionalCounters, excludedInstances)
		{
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000ACEC File Offset: 0x00008EEC
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
					if ((double)last.GetValueOrDefault() <= gatingCounterThreshold && last != null)
					{
						flag = true;
					}
				}
			}
			return flag2 && flag;
		}
	}
}
