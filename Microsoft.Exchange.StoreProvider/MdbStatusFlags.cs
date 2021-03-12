using System;

namespace Microsoft.Mapi
{
	// Token: 0x020001F5 RID: 501
	[Flags]
	internal enum MdbStatusFlags
	{
		// Token: 0x040006D8 RID: 1752
		Offline = 0,
		// Token: 0x040006D9 RID: 1753
		Online = 1,
		// Token: 0x040006DA RID: 1754
		Backup = 2,
		// Token: 0x040006DB RID: 1755
		Isinteg = 4,
		// Token: 0x040006DC RID: 1756
		IsPublic = 8,
		// Token: 0x040006DD RID: 1757
		InRecoverySG = 16,
		// Token: 0x040006DE RID: 1758
		Maintenance = 32,
		// Token: 0x040006DF RID: 1759
		MountInProgress = 64,
		// Token: 0x040006E0 RID: 1760
		AttachedReadOnly = 128
	}
}
