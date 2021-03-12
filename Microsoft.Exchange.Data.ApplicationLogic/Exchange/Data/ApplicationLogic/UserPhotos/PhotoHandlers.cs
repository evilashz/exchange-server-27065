using System;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001F3 RID: 499
	[Flags]
	internal enum PhotoHandlers
	{
		// Token: 0x040009B7 RID: 2487
		None = 0,
		// Token: 0x040009B8 RID: 2488
		FileSystem = 1,
		// Token: 0x040009B9 RID: 2489
		Mailbox = 2,
		// Token: 0x040009BA RID: 2490
		ActiveDirectory = 4,
		// Token: 0x040009BB RID: 2491
		Caching = 8,
		// Token: 0x040009BC RID: 2492
		Http = 16,
		// Token: 0x040009BD RID: 2493
		Private = 32,
		// Token: 0x040009BE RID: 2494
		RemoteForest = 64
	}
}
