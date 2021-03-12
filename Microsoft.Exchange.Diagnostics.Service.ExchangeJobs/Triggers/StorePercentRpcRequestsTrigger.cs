using System;
using System.Collections.Generic;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Triggers
{
	// Token: 0x02000041 RID: 65
	public class StorePercentRpcRequestsTrigger : PerInstanceTrigger
	{
		// Token: 0x0600013F RID: 319 RVA: 0x0000A250 File Offset: 0x00008450
		public StorePercentRpcRequestsTrigger(IJob job) : base(job, "MSExchangeIS Store\\(.+?\\)\\\\% RPC Requests", new PerfLogCounterTrigger.TriggerConfiguration("StorePercentRpcRequestsTrigger", double.NaN, 90.0, TimeSpan.FromMinutes(5.0), TimeSpan.FromMinutes(10.0), TimeSpan.FromMinutes(5.0), 0), new HashSet<DiagnosticMeasurement>(), StorePercentRpcRequestsTrigger.excludedInstances)
		{
		}

		// Token: 0x0400016D RID: 365
		private static readonly HashSet<string> excludedInstances = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"_Total"
		};
	}
}
