using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200010D RID: 269
	[Flags]
	internal enum FolderMappingFlags
	{
		// Token: 0x04000581 RID: 1409
		None = 0,
		// Token: 0x04000582 RID: 1410
		Include = 1,
		// Token: 0x04000583 RID: 1411
		Exclude = 2,
		// Token: 0x04000584 RID: 1412
		Inherit = 4,
		// Token: 0x04000585 RID: 1413
		Root = 8,
		// Token: 0x04000586 RID: 1414
		InheritedInclude = 16,
		// Token: 0x04000587 RID: 1415
		InheritedExclude = 32,
		// Token: 0x04000588 RID: 1416
		InclusionFlags = 3,
		// Token: 0x04000589 RID: 1417
		InheritanceFlags = 4
	}
}
