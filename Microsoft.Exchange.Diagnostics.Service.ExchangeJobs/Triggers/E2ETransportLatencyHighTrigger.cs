using System;
using System.Collections.Generic;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Triggers
{
	// Token: 0x0200004A RID: 74
	public class E2ETransportLatencyHighTrigger : TransportOverThresholdGatedTrigger
	{
		// Token: 0x0600015F RID: 351 RVA: 0x0000A950 File Offset: 0x00008B50
		public E2ETransportLatencyHighTrigger(IJob job) : base(job, "MSExchangeTransport End To End Latency\\(total - high\\)\\\\Percentile80$", 50.0, new PerfLogCounterTrigger.TriggerConfiguration("E2ETransportLatencyHighTrigger", 45.0, 90.0, TimeSpan.FromMinutes(5.0), TimeSpan.FromMinutes(5.0), TimeSpan.FromMinutes(5.0), 0), E2ETransportLatencyHighTrigger.additionalContext, E2ETransportLatencyHighTrigger.excludedInstances)
		{
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000A9C3 File Offset: 0x00008BC3
		protected override DiagnosticMeasurement AdditionalDiagnosticMeasurement(PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
			return DiagnosticMeasurement.GetMeasure(context.Counter.MachineName, E2ETransportLatencyHighTrigger.gatingCounter.ObjectName, E2ETransportLatencyHighTrigger.gatingCounter.CounterName, E2ETransportLatencyHighTrigger.gatingCounter.InstanceName);
		}

		// Token: 0x0400017B RID: 379
		private static readonly DiagnosticMeasurement gatingCounter = DiagnosticMeasurement.GetMeasure("MSExchangeTransport End To End Latency", "Percentile80Samples", "Total - High");

		// Token: 0x0400017C RID: 380
		private static readonly HashSet<string> excludedInstances = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400017D RID: 381
		private static readonly HashSet<DiagnosticMeasurement> additionalContext = new HashSet<DiagnosticMeasurement>(DiagnosticMeasurement.CounterFilterComparer.Comparer)
		{
			E2ETransportLatencyHighTrigger.gatingCounter
		};
	}
}
