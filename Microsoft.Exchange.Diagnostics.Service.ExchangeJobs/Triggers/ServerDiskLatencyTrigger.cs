using System;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Triggers
{
	// Token: 0x02000039 RID: 57
	public class ServerDiskLatencyTrigger : PerfLogCounterTrigger
	{
		// Token: 0x06000128 RID: 296 RVA: 0x000096BC File Offset: 0x000078BC
		public ServerDiskLatencyTrigger(IJob job) : base(job, "Logical Disk\\(_Total\\)\\\\Avg\\. Disk sec/Write", new PerfLogCounterTrigger.TriggerConfiguration("ServerDiskLatencyTrigger", double.NaN, 0.01, TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(15.0), TimeSpan.FromHours(24.0), 0))
		{
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000971C File Offset: 0x0000791C
		protected override void OnThresholdEvent(PerfLogLine line, PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
		}

		// Token: 0x04000153 RID: 339
		private const double TriggerThreshold = 0.01;

		// Token: 0x04000154 RID: 340
		private const string PerfCounterName = "Logical Disk\\(_Total\\)\\\\Avg\\. Disk sec/Write";

		// Token: 0x04000155 RID: 341
		private const string TriggerPrefix = "ServerDiskLatencyTrigger";
	}
}
