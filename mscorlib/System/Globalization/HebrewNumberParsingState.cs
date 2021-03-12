using System;

namespace System.Globalization
{
	// Token: 0x020003B1 RID: 945
	internal enum HebrewNumberParsingState
	{
		// Token: 0x040015D3 RID: 5587
		InvalidHebrewNumber,
		// Token: 0x040015D4 RID: 5588
		NotHebrewDigit,
		// Token: 0x040015D5 RID: 5589
		FoundEndOfHebrewNumber,
		// Token: 0x040015D6 RID: 5590
		ContinueParsing
	}
}
