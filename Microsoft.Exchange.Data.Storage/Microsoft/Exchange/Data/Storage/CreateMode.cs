using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001ED RID: 493
	[Flags]
	internal enum CreateMode
	{
		// Token: 0x04000DC3 RID: 3523
		CreateNew = 0,
		// Token: 0x04000DC4 RID: 3524
		OpenIfExists = 1,
		// Token: 0x04000DC5 RID: 3525
		InstantSearch = 2,
		// Token: 0x04000DC6 RID: 3526
		OptimizedConversationSearch = 4,
		// Token: 0x04000DC7 RID: 3527
		OverrideFolderCreationBlock = 8,
		// Token: 0x04000DC8 RID: 3528
		CreatePublicFolderDumpster = 16
	}
}
