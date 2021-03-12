using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.MailboxLoadBalance.QueueProcessing
{
	// Token: 0x020000E2 RID: 226
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ProcessingQueueCounters : IQueueCounters
	{
		// Token: 0x060006F0 RID: 1776 RVA: 0x00013B5C File Offset: 0x00011D5C
		public ProcessingQueueCounters(string queueName)
		{
			MailboxLoadBalanceMultiInstancePerformanceCountersInstance instance = MailboxLoadBalanceMultiInstancePerformanceCounters.GetInstance(queueName);
			this.ExecutionRateCounter = new PerfCounterWithAverageRate(null, instance.ProcessingRate, instance.ProcessingRateBase, 1, TimeSpan.FromHours(1.0));
			this.IncomingRequestRateCounter = new PerfCounterWithAverageRate(null, instance.ProcessingRequestRate, instance.ProcessingRequestRateBase, 1, TimeSpan.FromHours(1.0));
			this.QueueLengthCounter = instance.ProcessingQueueLength;
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x00013BD0 File Offset: 0x00011DD0
		// (set) Token: 0x060006F2 RID: 1778 RVA: 0x00013BD8 File Offset: 0x00011DD8
		public PerfCounterWithAverageRate IncomingRequestRateCounter { get; private set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x00013BE1 File Offset: 0x00011DE1
		// (set) Token: 0x060006F4 RID: 1780 RVA: 0x00013BE9 File Offset: 0x00011DE9
		public PerfCounterWithAverageRate ExecutionRateCounter { get; private set; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x00013BF2 File Offset: 0x00011DF2
		// (set) Token: 0x060006F6 RID: 1782 RVA: 0x00013BFA File Offset: 0x00011DFA
		public ExPerformanceCounter QueueLengthCounter { get; private set; }
	}
}
