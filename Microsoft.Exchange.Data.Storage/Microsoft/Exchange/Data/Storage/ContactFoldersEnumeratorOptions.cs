using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004A7 RID: 1191
	[Flags]
	internal enum ContactFoldersEnumeratorOptions
	{
		// Token: 0x04001C24 RID: 7204
		None = 0,
		// Token: 0x04001C25 RID: 7205
		SkipHiddenFolders = 1,
		// Token: 0x04001C26 RID: 7206
		SkipDeletedFolders = 2,
		// Token: 0x04001C27 RID: 7207
		IncludeParentFolder = 4
	}
}
