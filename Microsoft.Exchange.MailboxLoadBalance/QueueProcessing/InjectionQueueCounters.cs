using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.MailboxLoadBalance.QueueProcessing
{
	// Token: 0x020000DC RID: 220
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class InjectionQueueCounters : IQueueCounters
	{
		// Token: 0x060006CF RID: 1743 RVA: 0x000134F8 File Offset: 0x000116F8
		public InjectionQueueCounters(string queueName)
		{
			MailboxLoadBalanceMultiInstancePerformanceCountersInstance instance = MailboxLoadBalanceMultiInstancePerformanceCounters.GetInstance(queueName);
			this.ExecutionRateCounter = new PerfCounterWithAverageRate(null, instance.InjectionRate, instance.InjectionRateBase, 1, TimeSpan.FromHours(1.0));
			this.IncomingRequestRateCounter = new PerfCounterWithAverageRate(null, instance.InjectionRequestRate, instance.InjectionRequestRateBase, 1, TimeSpan.FromHours(1.0));
			this.QueueLengthCounter = instance.InjectionQueueLength;
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0001356C File Offset: 0x0001176C
		// (set) Token: 0x060006D1 RID: 1745 RVA: 0x00013574 File Offset: 0x00011774
		public PerfCounterWithAverageRate IncomingRequestRateCounter { get; private set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0001357D File Offset: 0x0001177D
		// (set) Token: 0x060006D3 RID: 1747 RVA: 0x00013585 File Offset: 0x00011785
		public PerfCounterWithAverageRate ExecutionRateCounter { get; private set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x0001358E File Offset: 0x0001178E
		// (set) Token: 0x060006D5 RID: 1749 RVA: 0x00013596 File Offset: 0x00011796
		public ExPerformanceCounter QueueLengthCounter { get; private set; }
	}
}
