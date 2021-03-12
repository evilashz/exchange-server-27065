using System;
using System.Collections.Generic;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Triggers
{
	// Token: 0x0200004C RID: 76
	public class MessagesCompletingCategorizationTrigger : TransportOverThresholdGatedTrigger
	{
		// Token: 0x06000165 RID: 357 RVA: 0x0000AB40 File Offset: 0x00008D40
		public MessagesCompletingCategorizationTrigger(IJob job) : base(job, "MSExchangeTransport Queues\\(_total\\)\\\\Messages Completing Categorization$", 0.0, new PerfLogCounterTrigger.TriggerConfiguration("MessagesCompletingCategorizationTrigger", 5.0, 1.0, TimeSpan.FromMinutes(5.0), TimeSpan.FromMinutes(5.0), TimeSpan.FromMinutes(5.0), 1), MessagesCompletingCategorizationTrigger.additionalContext, MessagesCompletingCategorizationTrigger.excludedInstances)
		{
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000ABB3 File Offset: 0x00008DB3
		protected override DiagnosticMeasurement AdditionalDiagnosticMeasurement(PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
			return DiagnosticMeasurement.GetMeasure(context.Counter.MachineName, MessagesCompletingCategorizationTrigger.gatingCounter.ObjectName, MessagesCompletingCategorizationTrigger.gatingCounter.CounterName, MessagesCompletingCategorizationTrigger.gatingCounter.InstanceName);
		}

		// Token: 0x04000181 RID: 385
		private static readonly DiagnosticMeasurement gatingCounter = DiagnosticMeasurement.GetMeasure("MSExchangeTransport Queues", "Messages Submitted Per Second", "_total");

		// Token: 0x04000182 RID: 386
		private static readonly HashSet<string> excludedInstances = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000183 RID: 387
		private static readonly HashSet<DiagnosticMeasurement> additionalContext = new HashSet<DiagnosticMeasurement>(DiagnosticMeasurement.CounterFilterComparer.Comparer)
		{
			MessagesCompletingCategorizationTrigger.gatingCounter
		};
	}
}
