using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000021 RID: 33
	public interface ISourceStatus
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000124 RID: 292
		int ProcessedItemCount { get; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000125 RID: 293
		int ItemCount { get; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000126 RID: 294
		long TotalSize { get; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000127 RID: 295
		int ProcessedUnsearchableItemCount { get; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000128 RID: 296
		int UnsearchableItemCount { get; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000129 RID: 297
		long DuplicateItemCount { get; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600012A RID: 298
		long UnsearchableDuplicateItemCount { get; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600012B RID: 299
		long ErrorItemCount { get; }

		// Token: 0x0600012C RID: 300
		bool IsSearchCompleted(bool includeSearchableItems, bool includeUnsearchableItems);

		// Token: 0x0600012D RID: 301
		bool IsExportCompleted(bool includeSearchableItems, bool includeUnsearchableItems);
	}
}
