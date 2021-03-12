using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000253 RID: 595
	[Flags]
	internal enum StoreFolderFlags
	{
		// Token: 0x040011DE RID: 4574
		FolderIPM = 1,
		// Token: 0x040011DF RID: 4575
		FolderSearch = 2,
		// Token: 0x040011E0 RID: 4576
		FolderNormal = 4,
		// Token: 0x040011E1 RID: 4577
		FolderRules = 8
	}
}
