using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000FB RID: 251
	[Flags]
	public enum ConversationSortOrder
	{
		// Token: 0x040005EE RID: 1518
		Chronological = 1,
		// Token: 0x040005EF RID: 1519
		Tree = 2,
		// Token: 0x040005F0 RID: 1520
		NewestOnTop = 4,
		// Token: 0x040005F1 RID: 1521
		NewestOnBottom = 8,
		// Token: 0x040005F2 RID: 1522
		ChronologicalNewestOnTop = 5,
		// Token: 0x040005F3 RID: 1523
		ChronologicalNewestOnBottom = 9,
		// Token: 0x040005F4 RID: 1524
		TreeNewestOnBottom = 10
	}
}
