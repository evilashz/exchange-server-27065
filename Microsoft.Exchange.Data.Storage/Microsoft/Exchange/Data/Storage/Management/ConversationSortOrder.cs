using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009E5 RID: 2533
	[Flags]
	public enum ConversationSortOrder
	{
		// Token: 0x040033F2 RID: 13298
		Chronological = 1,
		// Token: 0x040033F3 RID: 13299
		Tree = 2,
		// Token: 0x040033F4 RID: 13300
		NewestOnTop = 4,
		// Token: 0x040033F5 RID: 13301
		NewestOnBottom = 8,
		// Token: 0x040033F6 RID: 13302
		ChronologicalNewestOnTop = 5,
		// Token: 0x040033F7 RID: 13303
		ChronologicalNewestOnBottom = 9,
		// Token: 0x040033F8 RID: 13304
		TreeNewestOnBottom = 10
	}
}
