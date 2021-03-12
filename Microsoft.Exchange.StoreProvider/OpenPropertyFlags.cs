using System;

namespace Microsoft.Mapi
{
	// Token: 0x02000201 RID: 513
	[Flags]
	internal enum OpenPropertyFlags
	{
		// Token: 0x040009E0 RID: 2528
		None = 0,
		// Token: 0x040009E1 RID: 2529
		BestAccess = 16,
		// Token: 0x040009E2 RID: 2530
		Create = 2,
		// Token: 0x040009E3 RID: 2531
		Modify = 1,
		// Token: 0x040009E4 RID: 2532
		DeferredErrors = 8,
		// Token: 0x040009E5 RID: 2533
		ReadOnly = 16
	}
}
