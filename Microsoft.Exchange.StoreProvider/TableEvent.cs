using System;

namespace Microsoft.Mapi
{
	// Token: 0x02000252 RID: 594
	internal enum TableEvent
	{
		// Token: 0x04001078 RID: 4216
		TableChanged = 1,
		// Token: 0x04001079 RID: 4217
		TableError,
		// Token: 0x0400107A RID: 4218
		TableRowAdded,
		// Token: 0x0400107B RID: 4219
		TableRowDeleted,
		// Token: 0x0400107C RID: 4220
		TableRowModified,
		// Token: 0x0400107D RID: 4221
		TableSortDone,
		// Token: 0x0400107E RID: 4222
		TableRestrictDone,
		// Token: 0x0400107F RID: 4223
		TableSetColDone,
		// Token: 0x04001080 RID: 4224
		TableReload,
		// Token: 0x04001081 RID: 4225
		TableRowDeletedExtended
	}
}
