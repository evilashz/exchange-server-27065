using System;

namespace Microsoft.Mapi
{
	// Token: 0x020001BC RID: 444
	[Flags]
	internal enum CreateFolderFlags
	{
		// Token: 0x040005B8 RID: 1464
		None = 0,
		// Token: 0x040005B9 RID: 1465
		OpenIfExists = 1,
		// Token: 0x040005BA RID: 1466
		InstantSearch = 2,
		// Token: 0x040005BB RID: 1467
		OptimizedConversationSearch = 4,
		// Token: 0x040005BC RID: 1468
		CreatePublicFolderDumpster = 8,
		// Token: 0x040005BD RID: 1469
		InternalAccess = 16,
		// Token: 0x040005BE RID: 1470
		UnicodeStrings = -2147483648
	}
}
