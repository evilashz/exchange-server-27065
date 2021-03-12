using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000243 RID: 579
	[Flags]
	public enum RecipientSoftDeletedStatusFlags
	{
		// Token: 0x04000D8D RID: 3469
		None = 0,
		// Token: 0x04000D8E RID: 3470
		Removed = 1,
		// Token: 0x04000D8F RID: 3471
		Disabled = 2,
		// Token: 0x04000D90 RID: 3472
		IncludeInGarbageCollection = 4,
		// Token: 0x04000D91 RID: 3473
		Inactive = 8
	}
}
