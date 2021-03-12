using System;

namespace Microsoft.Mapi
{
	// Token: 0x02000088 RID: 136
	[Flags]
	internal enum MapiEventFlags
	{
		// Token: 0x0400053F RID: 1343
		None = 0,
		// Token: 0x04000540 RID: 1344
		FolderAssociated = 1,
		// Token: 0x04000541 RID: 1345
		Ancestor = 4,
		// Token: 0x04000542 RID: 1346
		Children = 8,
		// Token: 0x04000543 RID: 1347
		ContentOnly = 16,
		// Token: 0x04000544 RID: 1348
		SoftDeleted = 32,
		// Token: 0x04000545 RID: 1349
		Subfolder = 64,
		// Token: 0x04000546 RID: 1350
		ModifiedByMove = 128,
		// Token: 0x04000547 RID: 1351
		Source = 256,
		// Token: 0x04000548 RID: 1352
		Destination = 512,
		// Token: 0x04000549 RID: 1353
		ObjectClassTruncated = 1024,
		// Token: 0x0400054A RID: 1354
		SearchFolder = 2048
	}
}
