using System;
using Microsoft.Exchange.EDiscovery.Export;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch
{
	// Token: 0x02000006 RID: 6
	internal class MailboxExportMetadata : IExportMetadata
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000024EF File Offset: 0x000006EF
		public MailboxExportMetadata(string exportId, string exportName, bool includeDuplicates, bool includeUnsearchableItems, DateTime startTime) : this(exportId, exportName, includeDuplicates, includeUnsearchableItems, startTime, null)
		{
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024FF File Offset: 0x000006FF
		public MailboxExportMetadata(string exportId, string exportName, bool includeDuplicates, bool includeUnsearchableItems, DateTime startTime, string language)
		{
			this.ExportId = exportId;
			this.ExportName = exportName;
			this.IncludeDuplicates = includeDuplicates;
			this.IncludeUnsearchableItems = includeUnsearchableItems;
			this.ExportStartTime = startTime;
			this.IncludeSearchableItems = true;
			this.Language = language;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000253B File Offset: 0x0000073B
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002543 File Offset: 0x00000743
		public ulong EstimateBytes { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000254C File Offset: 0x0000074C
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002554 File Offset: 0x00000754
		public int EstimateItems { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000255D File Offset: 0x0000075D
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002565 File Offset: 0x00000765
		public string ExportId { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000256E File Offset: 0x0000076E
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002576 File Offset: 0x00000776
		public string ExportName { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000257F File Offset: 0x0000077F
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002587 File Offset: 0x00000787
		public DateTime ExportStartTime { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002590 File Offset: 0x00000790
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002598 File Offset: 0x00000798
		public bool IncludeDuplicates { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000025A1 File Offset: 0x000007A1
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000025A9 File Offset: 0x000007A9
		public bool RemoveRms { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000025B2 File Offset: 0x000007B2
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000025BA File Offset: 0x000007BA
		public bool IncludeUnsearchableItems { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000025C3 File Offset: 0x000007C3
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000025CB File Offset: 0x000007CB
		public bool IncludeSearchableItems { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000025D4 File Offset: 0x000007D4
		// (set) Token: 0x06000032 RID: 50 RVA: 0x000025DC File Offset: 0x000007DC
		public string Language { get; private set; }
	}
}
