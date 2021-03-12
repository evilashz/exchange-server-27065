using System;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Triggers
{
	// Token: 0x02000037 RID: 55
	public class LogicalDiskFreeMegabytesTrigger : PerfLogCounterTrigger
	{
		// Token: 0x06000124 RID: 292 RVA: 0x000095BC File Offset: 0x000077BC
		public LogicalDiskFreeMegabytesTrigger(IJob job) : base(job, "LogicalDisk\\(D:\\)\\\\Free Megabytes", new PerfLogCounterTrigger.TriggerConfiguration("LogicalDiskFreeMegabytesTrigger", double.NaN, Configuration.GetConfigDouble("DirectoryLogicalDiskFreeMegabytesTriggerThreshold", double.MinValue, double.MaxValue, 51200.0), TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(15.0), TimeSpan.FromMinutes(15.0), 1))
		{
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00009638 File Offset: 0x00007838
		protected override void OnThresholdEvent(PerfLogLine line, PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
		}

		// Token: 0x0400014B RID: 331
		private const string ThresholdConfigAttributeName = "DirectoryLogicalDiskFreeMegabytesTriggerThreshold";

		// Token: 0x0400014C RID: 332
		private const double DefaultTriggerThreshold = 51200.0;

		// Token: 0x0400014D RID: 333
		private const string PerfCounterName = "LogicalDisk\\(D:\\)\\\\Free Megabytes";

		// Token: 0x0400014E RID: 334
		private const string TriggerPrefix = "LogicalDiskFreeMegabytesTrigger";
	}
}
