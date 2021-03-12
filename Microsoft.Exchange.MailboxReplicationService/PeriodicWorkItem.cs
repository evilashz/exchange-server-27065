using System;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000AF RID: 175
	internal class PeriodicWorkItem : WorkItem, IPeriodicWorkItem
	{
		// Token: 0x060008DC RID: 2268 RVA: 0x0003C1EE File Offset: 0x0003A3EE
		public PeriodicWorkItem(TimeSpan periodicInterval, Action callback) : base(TimeSpan.Zero, callback, WorkloadType.Unknown)
		{
			((IPeriodicWorkItem)this).PeriodicInterval = periodicInterval;
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x0003C204 File Offset: 0x0003A404
		// (set) Token: 0x060008DE RID: 2270 RVA: 0x0003C20C File Offset: 0x0003A40C
		TimeSpan IPeriodicWorkItem.PeriodicInterval { get; set; }
	}
}
