using System;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006A4 RID: 1700
	[Flags]
	internal enum DocumentLibraryQueryOptions
	{
		// Token: 0x040025DA RID: 9690
		Folders = 1,
		// Token: 0x040025DB RID: 9691
		Files = 2,
		// Token: 0x040025DC RID: 9692
		FoldersAndFiles = 3
	}
}
