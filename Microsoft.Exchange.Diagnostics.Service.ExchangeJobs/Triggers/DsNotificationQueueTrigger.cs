using System;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Triggers
{
	// Token: 0x02000036 RID: 54
	public class DsNotificationQueueTrigger : PerfLogCounterTrigger
	{
		// Token: 0x06000122 RID: 290 RVA: 0x0000953C File Offset: 0x0000773C
		public DsNotificationQueueTrigger(IJob job) : base(job, "DirectoryServices\\(NTDS\\)\\\\DS Notify Queue Size", new PerfLogCounterTrigger.TriggerConfiguration("DsNotificationQueueTrigger", double.NaN, Configuration.GetConfigDouble("DirectoryDSNotificationQueueTriggerThreshold", double.MinValue, double.MaxValue, 250000.0), TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(15.0), TimeSpan.FromMinutes(15.0), 0))
		{
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000095B8 File Offset: 0x000077B8
		protected override void OnThresholdEvent(PerfLogLine line, PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
		}

		// Token: 0x04000147 RID: 327
		private const string ThresholdConfigAttributeName = "DirectoryDSNotificationQueueTriggerThreshold";

		// Token: 0x04000148 RID: 328
		private const double DefaultTriggerThreshold = 250000.0;

		// Token: 0x04000149 RID: 329
		private const string PerfCounterName = "DirectoryServices\\(NTDS\\)\\\\DS Notify Queue Size";

		// Token: 0x0400014A RID: 330
		private const string TriggerPrefix = "DsNotificationQueueTrigger";
	}
}
