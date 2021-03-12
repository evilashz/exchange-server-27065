using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x0200004B RID: 75
	internal enum PerformInstantSearchMetaData
	{
		// Token: 0x040003DA RID: 986
		[DisplayName("IS", "AI")]
		ApplicationID,
		// Token: 0x040003DB RID: 987
		[DisplayName("IS", "SID")]
		SearchSessionID,
		// Token: 0x040003DC RID: 988
		[DisplayName("IS", "RID")]
		SearchRequestID,
		// Token: 0x040003DD RID: 989
		[DisplayName("IS", "WSR")]
		WaitOnSearchResults,
		// Token: 0x040003DE RID: 990
		[DisplayName("IS", "QO")]
		QueryOptions,
		// Token: 0x040003DF RID: 991
		[DisplayName("IS", "SC")]
		RequestedSuggestionsCount,
		// Token: 0x040003E0 RID: 992
		[DisplayName("IS", "SS")]
		SuggestionSources,
		// Token: 0x040003E1 RID: 993
		[DisplayName("IS", "RT")]
		RequestedResultTypes,
		// Token: 0x040003E2 RID: 994
		[DisplayName("IS", "FS")]
		FolderScope,
		// Token: 0x040003E3 RID: 995
		[DisplayName("IS", "QF")]
		QueryFilter,
		// Token: 0x040003E4 RID: 996
		[DisplayName("IS", "RFC")]
		AppliedRefinementFiltersCount,
		// Token: 0x040003E5 RID: 997
		[DisplayName("IS", "REC")]
		RequestedRefinersCount,
		// Token: 0x040003E6 RID: 998
		[DisplayName("IS", "RC")]
		RequestedResultsCount,
		// Token: 0x040003E7 RID: 999
		[DisplayName("IS", "IE")]
		InternalExecuteTime,
		// Token: 0x040003E8 RID: 1000
		[DisplayName("IS", "PSD")]
		PreSearchDuration,
		// Token: 0x040003E9 RID: 1001
		[DisplayName("IS", "TSD")]
		TotalSearchDuration
	}
}
