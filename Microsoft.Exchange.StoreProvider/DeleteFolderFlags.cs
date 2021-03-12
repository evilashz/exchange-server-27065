using System;

namespace Microsoft.Mapi
{
	// Token: 0x020001BF RID: 447
	[Flags]
	internal enum DeleteFolderFlags
	{
		// Token: 0x040005CB RID: 1483
		None = 0,
		// Token: 0x040005CC RID: 1484
		DeleteMessages = 1,
		// Token: 0x040005CD RID: 1485
		DelSubFolders = 4,
		// Token: 0x040005CE RID: 1486
		ForceHardDelete = 16
	}
}
