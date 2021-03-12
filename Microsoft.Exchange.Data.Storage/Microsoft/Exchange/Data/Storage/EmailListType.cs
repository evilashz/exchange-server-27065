using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001E1 RID: 481
	[Flags]
	internal enum EmailListType
	{
		// Token: 0x04000D6C RID: 3436
		None = 0,
		// Token: 0x04000D6D RID: 3437
		Email1 = 1,
		// Token: 0x04000D6E RID: 3438
		Email2 = 2,
		// Token: 0x04000D6F RID: 3439
		Email3 = 4,
		// Token: 0x04000D70 RID: 3440
		BusinessFax = 8,
		// Token: 0x04000D71 RID: 3441
		HomeFax = 16,
		// Token: 0x04000D72 RID: 3442
		OtherFax = 32
	}
}
