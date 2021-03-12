using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000213 RID: 531
	[Flags]
	internal enum FolderQueryFlags
	{
		// Token: 0x04000F56 RID: 3926
		None = 0,
		// Token: 0x04000F57 RID: 3927
		SoftDeleted = 1,
		// Token: 0x04000F58 RID: 3928
		DeepTraversal = 2,
		// Token: 0x04000F59 RID: 3929
		SuppressNotificationsOnMyActions = 4,
		// Token: 0x04000F5A RID: 3930
		NoNotifications = 8
	}
}
