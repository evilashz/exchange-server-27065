using System;
using System.Collections.Generic;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Triggers
{
	// Token: 0x0200004D RID: 77
	public class TransportMessagesDeferredDuringCategorizationTrigger : TransportOverThresholdGatedTrigger
	{
		// Token: 0x06000168 RID: 360 RVA: 0x0000AC38 File Offset: 0x00008E38
		public TransportMessagesDeferredDuringCategorizationTrigger(IJob job) : base(job, "MSExchangeTransport Queues\\(_total\\)\\\\Messages Deferred during Categorization", 500.0, new PerfLogCounterTrigger.TriggerConfiguration("TransportMessagesDeferredDuringCategorizationTrigger", 5.0, 10.0, TimeSpan.FromMinutes(5.0), TimeSpan.FromMinutes(5.0), TimeSpan.FromMinutes(5.0), 0), TransportMessagesDeferredDuringCategorizationTrigger.additionalContext, TransportMessagesDeferredDuringCategorizationTrigger.excludedInstances)
		{
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000ACAB File Offset: 0x00008EAB
		protected override DiagnosticMeasurement AdditionalDiagnosticMeasurement(PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
			return DiagnosticMeasurement.GetMeasure(context.Counter.MachineName, TransportMessagesDeferredDuringCategorizationTrigger.gatingCounter.ObjectName, TransportMessagesDeferredDuringCategorizationTrigger.gatingCounter.CounterName, TransportMessagesDeferredDuringCategorizationTrigger.gatingCounter.InstanceName);
		}

		// Token: 0x04000184 RID: 388
		private static readonly DiagnosticMeasurement gatingCounter = DiagnosticMeasurement.GetMeasure("MSExchangeTransport Queues", "Messages Queued For Delivery", "_total");

		// Token: 0x04000185 RID: 389
		private static readonly HashSet<string> excludedInstances = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000186 RID: 390
		private static readonly HashSet<DiagnosticMeasurement> additionalContext = new HashSet<DiagnosticMeasurement>(DiagnosticMeasurement.CounterFilterComparer.Comparer)
		{
			TransportMessagesDeferredDuringCategorizationTrigger.gatingCounter
		};
	}
}
