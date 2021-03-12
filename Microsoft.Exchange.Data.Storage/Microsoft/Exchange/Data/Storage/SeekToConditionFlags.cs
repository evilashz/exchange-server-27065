using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000218 RID: 536
	[Flags]
	internal enum SeekToConditionFlags
	{
		// Token: 0x04000F7B RID: 3963
		None = 0,
		// Token: 0x04000F7C RID: 3964
		AllowExtendedFilters = 1,
		// Token: 0x04000F7D RID: 3965
		AllowExtendedSeekReferences = 2,
		// Token: 0x04000F7E RID: 3966
		KeepCursorPositionWhenNoMatch = 4
	}
}
