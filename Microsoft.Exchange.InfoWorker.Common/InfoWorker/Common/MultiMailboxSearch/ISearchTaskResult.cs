using System;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001BA RID: 442
	internal interface ISearchTaskResult : ISearchResult
	{
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000BEE RID: 3054
		SearchType ResultType { get; }

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000BEF RID: 3055
		bool Success { get; }
	}
}
