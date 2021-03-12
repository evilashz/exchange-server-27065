using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001E6 RID: 486
	internal enum QueryNotificationType
	{
		// Token: 0x04000D88 RID: 3464
		QueryResultChanged = 1,
		// Token: 0x04000D89 RID: 3465
		Error,
		// Token: 0x04000D8A RID: 3466
		RowAdded,
		// Token: 0x04000D8B RID: 3467
		RowDeleted,
		// Token: 0x04000D8C RID: 3468
		RowModified,
		// Token: 0x04000D8D RID: 3469
		SortDone,
		// Token: 0x04000D8E RID: 3470
		RestrictDone,
		// Token: 0x04000D8F RID: 3471
		SetColumnDone,
		// Token: 0x04000D90 RID: 3472
		Reload
	}
}
