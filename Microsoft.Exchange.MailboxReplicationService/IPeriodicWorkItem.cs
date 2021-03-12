using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000AC RID: 172
	internal interface IPeriodicWorkItem
	{
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060008D0 RID: 2256
		// (set) Token: 0x060008D1 RID: 2257
		TimeSpan PeriodicInterval { get; set; }
	}
}
