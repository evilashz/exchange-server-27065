using System;

namespace Microsoft.Exchange.Data.ContentTypes.Internal
{
	// Token: 0x020000CC RID: 204
	[Flags]
	internal enum ContentLineNodeType
	{
		// Token: 0x040006D3 RID: 1747
		DocumentStart = 0,
		// Token: 0x040006D4 RID: 1748
		ComponentStart = 1,
		// Token: 0x040006D5 RID: 1749
		ComponentEnd = 2,
		// Token: 0x040006D6 RID: 1750
		Parameter = 4,
		// Token: 0x040006D7 RID: 1751
		Property = 8,
		// Token: 0x040006D8 RID: 1752
		BeforeComponentStart = 16,
		// Token: 0x040006D9 RID: 1753
		BeforeComponentEnd = 32,
		// Token: 0x040006DA RID: 1754
		DocumentEnd = 64
	}
}
