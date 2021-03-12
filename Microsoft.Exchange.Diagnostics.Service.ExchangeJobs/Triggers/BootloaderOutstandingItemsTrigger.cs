using System;
using System.Collections.Generic;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Triggers
{
	// Token: 0x02000049 RID: 73
	public class BootloaderOutstandingItemsTrigger : TransportOverThresholdGatedTrigger
	{
		// Token: 0x0600015B RID: 347 RVA: 0x0000A824 File Offset: 0x00008A24
		public BootloaderOutstandingItemsTrigger(IJob job) : base(job, "Process\\(EdgeTransport\\)\\\\Elapsed Time$", 1.0, new PerfLogCounterTrigger.TriggerConfiguration("BootloaderOutstandingItemsTrigger", 1200.0, 1800.0, TimeSpan.FromMinutes(5.0), TimeSpan.FromMinutes(5.0), TimeSpan.FromMinutes(5.0), 0), new HashSet<DiagnosticMeasurement>(DiagnosticMeasurement.CounterFilterComparer.Comparer)
		{
			BootloaderOutstandingItemsTrigger.GatingCounter
		}, new HashSet<string>(StringComparer.OrdinalIgnoreCase))
		{
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000A8AF File Offset: 0x00008AAF
		protected override DiagnosticMeasurement AdditionalDiagnosticMeasurement(PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
			return DiagnosticMeasurement.GetMeasure(context.Counter.MachineName, BootloaderOutstandingItemsTrigger.GatingCounter.ObjectName, BootloaderOutstandingItemsTrigger.GatingCounter.CounterName, BootloaderOutstandingItemsTrigger.GatingCounter.InstanceName);
		}

		// Token: 0x04000177 RID: 375
		private const int OutstandingItemsTriggerThreshold = 1;

		// Token: 0x04000178 RID: 376
		private const int ProcessElapsedTriggerWarningThreshold = 1200;

		// Token: 0x04000179 RID: 377
		private const int ProcessElapsedTriggerErrorThreshold = 1800;

		// Token: 0x0400017A RID: 378
		private static readonly DiagnosticMeasurement GatingCounter = DiagnosticMeasurement.GetMeasure("MsExchangeTransport Database", "Bootloader Outstanding Items", "other");
	}
}
