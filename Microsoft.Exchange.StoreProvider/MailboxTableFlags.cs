using System;

namespace Microsoft.Mapi
{
	// Token: 0x02000040 RID: 64
	[Flags]
	internal enum MailboxTableFlags
	{
		// Token: 0x04000400 RID: 1024
		MailboxTableFlagsNone = 0,
		// Token: 0x04000401 RID: 1025
		IncludeSoftDeletedMailbox = 1,
		// Token: 0x04000402 RID: 1026
		MaintenanceItems = 3,
		// Token: 0x04000403 RID: 1027
		MaintenanceItemsWithDS = 4,
		// Token: 0x04000404 RID: 1028
		UrgentMaintenanceItems = 5
	}
}
