using System;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000AE RID: 174
	internal class UnthrottledWorkItem : WorkItem
	{
		// Token: 0x060008DA RID: 2266 RVA: 0x0003C1D4 File Offset: 0x0003A3D4
		public UnthrottledWorkItem(Action callback) : this(TimeSpan.Zero, callback)
		{
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0003C1E2 File Offset: 0x0003A3E2
		public UnthrottledWorkItem(TimeSpan delay, Action callback) : base(delay, callback, WorkloadType.MailboxReplicationServiceHighPriority)
		{
		}

		// Token: 0x04000346 RID: 838
		private const WorkloadType UnthrottledWorkloadType = WorkloadType.MailboxReplicationServiceHighPriority;
	}
}
