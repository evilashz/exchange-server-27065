using System;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000F4 RID: 244
	[Flags]
	internal enum CommandOptions
	{
		// Token: 0x040006CF RID: 1743
		None = 0,
		// Token: 0x040006D0 RID: 1744
		ConvertParentFolderIdToPublicFolderId = 1,
		// Token: 0x040006D1 RID: 1745
		ConvertFolderIdToPublicFolderId = 2
	}
}
