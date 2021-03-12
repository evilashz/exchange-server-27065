using System;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Triggers
{
	// Token: 0x02000035 RID: 53
	public class DraPendingReplicationTrigger : PerfLogCounterTrigger
	{
		// Token: 0x06000120 RID: 288 RVA: 0x000094BC File Offset: 0x000076BC
		public DraPendingReplicationTrigger(IJob job) : base(job, "DirectoryServices\\(NTDS\\)\\\\DRA Pending Replication Operations", new PerfLogCounterTrigger.TriggerConfiguration("DraPendingReplicationTrigger", double.NaN, Configuration.GetConfigDouble("DirectoryDRAPendingReplicationTriggerThreshold", double.MinValue, double.MaxValue, 50.0), TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(30.0), TimeSpan.FromMinutes(30.0), 0))
		{
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00009538 File Offset: 0x00007738
		protected override void OnThresholdEvent(PerfLogLine line, PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
		}

		// Token: 0x04000143 RID: 323
		private const string ThresholdConfigAttributeName = "DirectoryDRAPendingReplicationTriggerThreshold";

		// Token: 0x04000144 RID: 324
		private const double DefaultTriggerThreshold = 50.0;

		// Token: 0x04000145 RID: 325
		private const string PerfCounterName = "DirectoryServices\\(NTDS\\)\\\\DRA Pending Replication Operations";

		// Token: 0x04000146 RID: 326
		private const string TriggerPrefix = "DraPendingReplicationTrigger";
	}
}
