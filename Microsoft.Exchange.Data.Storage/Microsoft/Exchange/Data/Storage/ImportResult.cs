using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000250 RID: 592
	internal enum ImportResult
	{
		// Token: 0x040011C6 RID: 4550
		Unknown,
		// Token: 0x040011C7 RID: 4551
		Success,
		// Token: 0x040011C8 RID: 4552
		SyncClientChangeNewer,
		// Token: 0x040011C9 RID: 4553
		SyncObjectDeleted,
		// Token: 0x040011CA RID: 4554
		SyncIgnore,
		// Token: 0x040011CB RID: 4555
		SyncConflict,
		// Token: 0x040011CC RID: 4556
		NotFound,
		// Token: 0x040011CD RID: 4557
		ObjectModified,
		// Token: 0x040011CE RID: 4558
		ObjectDeleted
	}
}
