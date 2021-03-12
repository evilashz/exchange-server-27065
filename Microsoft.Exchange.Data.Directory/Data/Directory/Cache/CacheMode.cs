using System;

namespace Microsoft.Exchange.Data.Directory.Cache
{
	// Token: 0x02000098 RID: 152
	[Flags]
	internal enum CacheMode
	{
		// Token: 0x040002BD RID: 701
		Disabled = 0,
		// Token: 0x040002BE RID: 702
		Read = 2,
		// Token: 0x040002BF RID: 703
		SyncWrite = 4,
		// Token: 0x040002C0 RID: 704
		AsyncWrite = 8
	}
}
