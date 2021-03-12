using System;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000063 RID: 99
	public enum GetMailboxInfoTableFlags : uint
	{
		// Token: 0x040001BF RID: 447
		None,
		// Token: 0x040001C0 RID: 448
		IncludeSoftDeleted,
		// Token: 0x040001C1 RID: 449
		FinalCleanup,
		// Token: 0x040001C2 RID: 450
		MaintenanceItems,
		// Token: 0x040001C3 RID: 451
		MaintenanceItemsWithDS,
		// Token: 0x040001C4 RID: 452
		UrgentMaintenanceItems
	}
}
