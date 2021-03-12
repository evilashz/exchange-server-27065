using System;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006A1 RID: 1697
	[Flags]
	internal enum SharepointItemType
	{
		// Token: 0x040025B8 RID: 9656
		Web = 1,
		// Token: 0x040025B9 RID: 9657
		List = 2,
		// Token: 0x040025BA RID: 9658
		Item = 4,
		// Token: 0x040025BB RID: 9659
		DocumentLibrary = 10,
		// Token: 0x040025BC RID: 9660
		Document = 20,
		// Token: 0x040025BD RID: 9661
		Folder = 36
	}
}
