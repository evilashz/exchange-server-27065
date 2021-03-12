using System;

namespace Microsoft.Exchange.Search.Query
{
	// Token: 0x02000014 RID: 20
	public enum QueryExecutionStepType
	{
		// Token: 0x04000094 RID: 148
		InstantSearchRequest,
		// Token: 0x04000095 RID: 149
		AnalyzeQuery,
		// Token: 0x04000096 RID: 150
		CreateSearchFolder,
		// Token: 0x04000097 RID: 151
		GetSearchFolderView,
		// Token: 0x04000098 RID: 152
		QuerySearchFolder,
		// Token: 0x04000099 RID: 153
		GetHitHighlighiting,
		// Token: 0x0400009A RID: 154
		HitHighlightingCallback,
		// Token: 0x0400009B RID: 155
		GetSuggestions,
		// Token: 0x0400009C RID: 156
		SuggestionsCallback,
		// Token: 0x0400009D RID: 157
		GetSuggestionsPrimer,
		// Token: 0x0400009E RID: 158
		QueryResultsCallback,
		// Token: 0x0400009F RID: 159
		RefinersCallback,
		// Token: 0x040000A0 RID: 160
		GetRefiners,
		// Token: 0x040000A1 RID: 161
		GetFastResults,
		// Token: 0x040000A2 RID: 162
		ConvertFastResultsToPropertyBags,
		// Token: 0x040000A3 RID: 163
		TopNInitialization
	}
}
