using System;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x0200097D RID: 2429
	internal enum OperationType
	{
		// Token: 0x0400315B RID: 12635
		EndToEnd,
		// Token: 0x0400315C RID: 12636
		SharePointQuery,
		// Token: 0x0400315D RID: 12637
		AddFolder,
		// Token: 0x0400315E RID: 12638
		UpdateFolder,
		// Token: 0x0400315F RID: 12639
		AddFile,
		// Token: 0x04003160 RID: 12640
		UpdateFile,
		// Token: 0x04003161 RID: 12641
		MoveFile,
		// Token: 0x04003162 RID: 12642
		DeleteItem,
		// Token: 0x04003163 RID: 12643
		FolderLookupById,
		// Token: 0x04003164 RID: 12644
		FolderLookupByUri,
		// Token: 0x04003165 RID: 12645
		FileLookupById,
		// Token: 0x04003166 RID: 12646
		Throttle
	}
}
