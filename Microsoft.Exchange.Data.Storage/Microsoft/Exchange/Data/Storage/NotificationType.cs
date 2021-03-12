using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001E5 RID: 485
	[Flags]
	internal enum NotificationType
	{
		// Token: 0x04000D7E RID: 3454
		NewMail = 1,
		// Token: 0x04000D7F RID: 3455
		Created = 2,
		// Token: 0x04000D80 RID: 3456
		Deleted = 4,
		// Token: 0x04000D81 RID: 3457
		Modified = 8,
		// Token: 0x04000D82 RID: 3458
		Moved = 16,
		// Token: 0x04000D83 RID: 3459
		Copied = 32,
		// Token: 0x04000D84 RID: 3460
		SearchComplete = 64,
		// Token: 0x04000D85 RID: 3461
		Query = 128,
		// Token: 0x04000D86 RID: 3462
		ConnectionDropped = 256
	}
}
