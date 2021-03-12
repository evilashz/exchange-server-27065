using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001FB RID: 507
	internal interface ISearchRpcClient
	{
		// Token: 0x06000D53 RID: 3411
		AggregatedSearchTaskResult Search(int refinerResultTrimCount);

		// Token: 0x06000D54 RID: 3412
		List<IKeywordHit> GetKeywordHits(List<string> keywordList);

		// Token: 0x06000D55 RID: 3413
		void Abort();
	}
}
