using System;

namespace Microsoft.Exchange.Search.Query
{
	// Token: 0x02000017 RID: 23
	[Flags]
	public enum QuerySuggestionSources
	{
		// Token: 0x040000AF RID: 175
		None = 0,
		// Token: 0x040000B0 RID: 176
		RecentSearches = 1,
		// Token: 0x040000B1 RID: 177
		Spelling = 2,
		// Token: 0x040000B2 RID: 178
		Synonyms = 4,
		// Token: 0x040000B3 RID: 179
		Nicknames = 8,
		// Token: 0x040000B4 RID: 180
		TopN = 16,
		// Token: 0x040000B5 RID: 181
		Fuzzy = 26,
		// Token: 0x040000B6 RID: 182
		All = 31
	}
}
