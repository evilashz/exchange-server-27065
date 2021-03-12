using System;

namespace Microsoft.Mapi
{
	// Token: 0x020001BD RID: 445
	[Flags]
	internal enum CopyFolderFlags
	{
		// Token: 0x040005C0 RID: 1472
		None = 0,
		// Token: 0x040005C1 RID: 1473
		FolderMove = 1,
		// Token: 0x040005C2 RID: 1474
		CopySubfolders = 16,
		// Token: 0x040005C3 RID: 1475
		UnicodeStrings = -2147483648,
		// Token: 0x040005C4 RID: 1476
		SendEntryId = 32
	}
}
