using System;

namespace Microsoft.Mapi
{
	// Token: 0x020001F0 RID: 496
	internal enum TableStatus
	{
		// Token: 0x040006C0 RID: 1728
		Complete,
		// Token: 0x040006C1 RID: 1729
		QChanged = 7,
		// Token: 0x040006C2 RID: 1730
		Sorting = 9,
		// Token: 0x040006C3 RID: 1731
		Sort_Error,
		// Token: 0x040006C4 RID: 1732
		Setting_Cols,
		// Token: 0x040006C5 RID: 1733
		SetCol_Error = 13,
		// Token: 0x040006C6 RID: 1734
		Restricting,
		// Token: 0x040006C7 RID: 1735
		Restrict_Error
	}
}
