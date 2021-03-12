using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200001D RID: 29
	public interface ISearchResults
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000E9 RID: 233
		IEnumerable<IDictionary<string, object>> SearchResultItems { get; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000EA RID: 234
		IEnumerable<IDictionary<string, object>> UnsearchableItems { get; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000EB RID: 235
		int SearchResultItemsCount { get; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000EC RID: 236
		int ProcessedItemCount { get; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000ED RID: 237
		int UnsearchableItemsCount { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000EE RID: 238
		int ProcessedUnsearchableItemCount { get; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000EF RID: 239
		long TotalSize { get; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000F0 RID: 240
		string ItemIdKey { get; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000F1 RID: 241
		long DuplicateItemCount { get; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000F2 RID: 242
		long UnsearchableDuplicateItemCount { get; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000F3 RID: 243
		long ErrorItemCount { get; }

		// Token: 0x060000F4 RID: 244
		void IncrementErrorItemCount(string sourceId);

		// Token: 0x060000F5 RID: 245
		ISourceStatus GetSourceStatusBySourceId(string sourceId);
	}
}
