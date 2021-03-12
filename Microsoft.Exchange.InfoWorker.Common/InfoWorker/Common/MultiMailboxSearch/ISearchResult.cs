using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001B9 RID: 441
	internal interface ISearchResult
	{
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000BE5 RID: 3045
		SortedResultPage PreviewResult { get; }

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000BE6 RID: 3046
		IDictionary<string, IKeywordHit> KeywordStatistics { get; }

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000BE7 RID: 3047
		Dictionary<string, List<IRefinerResult>> RefinersResult { get; }

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000BE8 RID: 3048
		List<Pair<MailboxInfo, Exception>> PreviewErrors { get; }

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000BE9 RID: 3049
		ByteQuantifiedSize TotalResultSize { get; }

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000BEA RID: 3050
		ulong TotalResultCount { get; }

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000BEB RID: 3051
		List<MailboxStatistics> MailboxStats { get; }

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000BEC RID: 3052
		IProtocolLog ProtocolLog { get; }

		// Token: 0x06000BED RID: 3053
		void MergeSearchResult(ISearchResult aggregator);
	}
}
