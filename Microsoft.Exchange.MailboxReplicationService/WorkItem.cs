using System;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000AD RID: 173
	internal class WorkItem
	{
		// Token: 0x060008D2 RID: 2258 RVA: 0x0003C14A File Offset: 0x0003A34A
		public WorkItem(TimeSpan delay, Action callback) : this(delay, callback, WorkloadType.Unknown)
		{
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0003C158 File Offset: 0x0003A358
		public WorkItem(TimeSpan delay, Action callback, WorkloadType workloadType)
		{
			this.Callback = callback;
			this.ScheduledRunTime = ((delay == TimeSpan.Zero) ? ExDateTime.MinValue : ExDateTime.UtcNow.Add(delay));
			this.WorkloadType = workloadType;
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x0003C1A1 File Offset: 0x0003A3A1
		// (set) Token: 0x060008D5 RID: 2261 RVA: 0x0003C1A9 File Offset: 0x0003A3A9
		public Action Callback { get; private set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x0003C1B2 File Offset: 0x0003A3B2
		// (set) Token: 0x060008D7 RID: 2263 RVA: 0x0003C1BA File Offset: 0x0003A3BA
		public WorkloadType WorkloadType { get; private set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x0003C1C3 File Offset: 0x0003A3C3
		// (set) Token: 0x060008D9 RID: 2265 RVA: 0x0003C1CB File Offset: 0x0003A3CB
		public ExDateTime ScheduledRunTime { get; set; }
	}
}
