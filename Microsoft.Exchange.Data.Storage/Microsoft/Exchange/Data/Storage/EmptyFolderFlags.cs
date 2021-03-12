using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200024B RID: 587
	[Flags]
	internal enum EmptyFolderFlags
	{
		// Token: 0x040011A5 RID: 4517
		None = 0,
		// Token: 0x040011A6 RID: 4518
		DeleteAssociatedMessages = 1,
		// Token: 0x040011A7 RID: 4519
		Force = 2,
		// Token: 0x040011A8 RID: 4520
		HardDelete = 4
	}
}
