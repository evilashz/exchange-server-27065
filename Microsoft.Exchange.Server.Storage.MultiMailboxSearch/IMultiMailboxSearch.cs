using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.FullTextIndex;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.MultiMailboxSearch
{
	// Token: 0x02000002 RID: 2
	internal interface IMultiMailboxSearch
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		// (set) Token: 0x06000002 RID: 2
		List<string> RefinersList { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3
		// (set) Token: 0x06000004 RID: 4
		List<string> ExtraFieldsList { get; set; }

		// Token: 0x06000005 RID: 5
		ErrorCode Search(Context context, MultiMailboxSearchCriteria criteria, out IList<FullTextIndexRow> results, out KeywordStatsResultRow statsResult, out Dictionary<string, List<RefinersResultRow>> refinersOutput);

		// Token: 0x06000006 RID: 6
		ErrorCode GetKeywordStatistics(Context context, MultiMailboxSearchCriteria[] criterias, out IList<KeywordStatsResultRow> results);
	}
}
