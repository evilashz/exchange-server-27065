using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200005A RID: 90
	public interface IExportMetadata
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060006C7 RID: 1735
		string ExportName { get; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060006C8 RID: 1736
		string ExportId { get; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060006C9 RID: 1737
		DateTime ExportStartTime { get; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060006CA RID: 1738
		bool RemoveRms { get; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060006CB RID: 1739
		bool IncludeDuplicates { get; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060006CC RID: 1740
		bool IncludeUnsearchableItems { get; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060006CD RID: 1741
		bool IncludeSearchableItems { get; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060006CE RID: 1742
		int EstimateItems { get; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060006CF RID: 1743
		ulong EstimateBytes { get; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060006D0 RID: 1744
		string Language { get; }
	}
}
