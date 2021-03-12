using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200066D RID: 1645
	[Flags]
	internal enum DefaultFolderBehavior
	{
		// Token: 0x040024F4 RID: 9460
		None = 0,
		// Token: 0x040024F5 RID: 9461
		CanCreate = 1,
		// Token: 0x040024F6 RID: 9462
		CreateIfMissing = 3,
		// Token: 0x040024F7 RID: 9463
		CanNotRename = 4,
		// Token: 0x040024F8 RID: 9464
		AlwaysDeferInitialization = 32,
		// Token: 0x040024F9 RID: 9465
		CanHideFolderFromOutlook = 64,
		// Token: 0x040024FA RID: 9466
		RefreshIfMissing = 128
	}
}
