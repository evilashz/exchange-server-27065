using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200024A RID: 586
	[Flags]
	internal enum DeleteFolderFlags
	{
		// Token: 0x040011A0 RID: 4512
		None = 0,
		// Token: 0x040011A1 RID: 4513
		DeleteMessages = 1,
		// Token: 0x040011A2 RID: 4514
		DeleteSubFolders = 4,
		// Token: 0x040011A3 RID: 4515
		HardDelete = 16
	}
}
