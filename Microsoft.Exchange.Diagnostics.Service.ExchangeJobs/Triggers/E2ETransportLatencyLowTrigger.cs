using System;
using System.Collections.Generic;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Triggers
{
	// Token: 0x0200004B RID: 75
	public class E2ETransportLatencyLowTrigger : TransportOverThresholdGatedTrigger
	{
		// Token: 0x06000162 RID: 354 RVA: 0x0000AA48 File Offset: 0x00008C48
		public E2ETransportLatencyLowTrigger(IJob job) : base(job, "MSExchangeTransport End To End Latency\\(total - low\\)\\\\Percentile80$", 50.0, new PerfLogCounterTrigger.TriggerConfiguration("E2ETransportLatencyLowTrigger", 450.0, 900.0, TimeSpan.FromMinutes(5.0), TimeSpan.FromMinutes(5.0), TimeSpan.FromMinutes(5.0), 0), E2ETransportLatencyLowTrigger.additionalContext, E2ETransportLatencyLowTrigger.excludedInstances)
		{
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000AABB File Offset: 0x00008CBB
		protected override DiagnosticMeasurement AdditionalDiagnosticMeasurement(PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
			return DiagnosticMeasurement.GetMeasure(context.Counter.MachineName, E2ETransportLatencyLowTrigger.gatingCounter.ObjectName, E2ETransportLatencyLowTrigger.gatingCounter.CounterName, E2ETransportLatencyLowTrigger.gatingCounter.InstanceName);
		}

		// Token: 0x0400017E RID: 382
		private static readonly DiagnosticMeasurement gatingCounter = DiagnosticMeasurement.GetMeasure("MSExchangeTransport End To End Latency", "Percentile80Samples", "Total - Low");

		// Token: 0x0400017F RID: 383
		private static readonly HashSet<string> excludedInstances = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000180 RID: 384
		private static readonly HashSet<DiagnosticMeasurement> additionalContext = new HashSet<DiagnosticMeasurement>(DiagnosticMeasurement.CounterFilterComparer.Comparer)
		{
			E2ETransportLatencyLowTrigger.gatingCounter
		};
	}
}
