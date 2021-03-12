using System;
using System.Collections.Generic;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Triggers
{
	// Token: 0x02000042 RID: 66
	public class StoreQuarantinedMailboxCountTrigger : PerInstanceTrigger
	{
		// Token: 0x06000141 RID: 321 RVA: 0x0000A2E8 File Offset: 0x000084E8
		public StoreQuarantinedMailboxCountTrigger(IJob job) : base(job, "MSExchangeIS Store\\(.+?\\)\\\\Quarantined Mailbox Count", new PerfLogCounterTrigger.TriggerConfiguration("StoreQuarantinedMailboxCountTrigger", double.NaN, 1.0, TimeSpan.FromMinutes(5.0), TimeSpan.FromMinutes(10.0), TimeSpan.FromMinutes(5.0), 0), new HashSet<DiagnosticMeasurement>(), StoreQuarantinedMailboxCountTrigger.excludedInstances)
		{
		}

		// Token: 0x0400016E RID: 366
		private static readonly HashSet<string> excludedInstances = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"_Total"
		};
	}
}
