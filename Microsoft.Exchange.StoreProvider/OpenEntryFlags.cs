using System;

namespace Microsoft.Mapi
{
	// Token: 0x02000200 RID: 512
	[Flags]
	internal enum OpenEntryFlags
	{
		// Token: 0x040009D9 RID: 2521
		None = 0,
		// Token: 0x040009DA RID: 2522
		BestAccess = 16,
		// Token: 0x040009DB RID: 2523
		DeferredErrors = 8,
		// Token: 0x040009DC RID: 2524
		Modify = 1,
		// Token: 0x040009DD RID: 2525
		ShowSoftDeletes = 2,
		// Token: 0x040009DE RID: 2526
		DontThrowIfEntryIsMissing = 134217728
	}
}
