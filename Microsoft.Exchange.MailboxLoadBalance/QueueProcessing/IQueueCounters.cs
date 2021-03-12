using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.MailboxLoadBalance.QueueProcessing
{
	// Token: 0x020000DB RID: 219
	internal interface IQueueCounters
	{
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060006CC RID: 1740
		PerfCounterWithAverageRate IncomingRequestRateCounter { get; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060006CD RID: 1741
		PerfCounterWithAverageRate ExecutionRateCounter { get; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060006CE RID: 1742
		ExPerformanceCounter QueueLengthCounter { get; }
	}
}
