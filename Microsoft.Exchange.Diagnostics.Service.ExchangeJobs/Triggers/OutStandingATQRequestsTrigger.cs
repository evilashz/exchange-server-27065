using System;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Triggers
{
	// Token: 0x02000038 RID: 56
	public class OutStandingATQRequestsTrigger : PerfLogCounterTrigger
	{
		// Token: 0x06000126 RID: 294 RVA: 0x0000963C File Offset: 0x0000783C
		public OutStandingATQRequestsTrigger(IJob job) : base(job, "DirectoryServices\\(NTDS\\)\\\\ATQ Outstanding Queued Requests", new PerfLogCounterTrigger.TriggerConfiguration("OutStandingATQRequestsTrigger", double.NaN, Configuration.GetConfigDouble("DirectoryOutstandingATQRequestsTriggerThreshold", double.MinValue, double.MaxValue, 2000.0), TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(15.0), TimeSpan.FromMinutes(15.0), 0))
		{
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000096B8 File Offset: 0x000078B8
		protected override void OnThresholdEvent(PerfLogLine line, PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
		}

		// Token: 0x0400014F RID: 335
		private const string ThresholdConfigAttributeName = "DirectoryOutstandingATQRequestsTriggerThreshold";

		// Token: 0x04000150 RID: 336
		private const double DefaultTriggerThreshold = 2000.0;

		// Token: 0x04000151 RID: 337
		private const string PerfCounterName = "DirectoryServices\\(NTDS\\)\\\\ATQ Outstanding Queued Requests";

		// Token: 0x04000152 RID: 338
		private const string TriggerPrefix = "OutStandingATQRequestsTrigger";
	}
}
