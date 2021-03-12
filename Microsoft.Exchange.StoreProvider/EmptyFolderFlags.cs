using System;

namespace Microsoft.Mapi
{
	// Token: 0x020001BE RID: 446
	[Flags]
	internal enum EmptyFolderFlags
	{
		// Token: 0x040005C6 RID: 1478
		None = 0,
		// Token: 0x040005C7 RID: 1479
		DeleteAssociatedMessages = 8,
		// Token: 0x040005C8 RID: 1480
		ForceHardDelete = 16,
		// Token: 0x040005C9 RID: 1481
		Force = 32
	}
}
