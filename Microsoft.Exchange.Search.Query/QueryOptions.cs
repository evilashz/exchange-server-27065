using System;

namespace Microsoft.Exchange.Search.Query
{
	// Token: 0x02000011 RID: 17
	[Flags]
	internal enum QueryOptions
	{
		// Token: 0x04000083 RID: 131
		None = 0,
		// Token: 0x04000084 RID: 132
		Suggestions = 1,
		// Token: 0x04000085 RID: 133
		SuggestionsPrimer = 2,
		// Token: 0x04000086 RID: 134
		Results = 4,
		// Token: 0x04000087 RID: 135
		Refiners = 8,
		// Token: 0x04000088 RID: 136
		SearchTerms = 16,
		// Token: 0x04000089 RID: 137
		ExplicitSearch = 32,
		// Token: 0x0400008A RID: 138
		AllowFuzzing = 64
	}
}
