using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000B0 RID: 176
	internal class JobCheck : UnthrottledWorkItem, IPeriodicWorkItem
	{
		// Token: 0x060008DF RID: 2271 RVA: 0x0003C215 File Offset: 0x0003A415
		public JobCheck(TimeSpan periodicInterval, Action callback) : base(TimeSpan.Zero, callback)
		{
			((IPeriodicWorkItem)this).PeriodicInterval = periodicInterval;
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x0003C22A File Offset: 0x0003A42A
		// (set) Token: 0x060008E1 RID: 2273 RVA: 0x0003C232 File Offset: 0x0003A432
		TimeSpan IPeriodicWorkItem.PeriodicInterval { get; set; }
	}
}
