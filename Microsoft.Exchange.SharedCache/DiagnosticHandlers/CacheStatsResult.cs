using System;

namespace Microsoft.Exchange.SharedCache.DiagnosticHandlers
{
	// Token: 0x02000013 RID: 19
	public struct CacheStatsResult
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000036DE File Offset: 0x000018DE
		// (set) Token: 0x06000077 RID: 119 RVA: 0x000036E6 File Offset: 0x000018E6
		public string Name { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000036EF File Offset: 0x000018EF
		// (set) Token: 0x06000079 RID: 121 RVA: 0x000036F7 File Offset: 0x000018F7
		public Guid Guid { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003700 File Offset: 0x00001900
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00003708 File Offset: 0x00001908
		public string Type { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003711 File Offset: 0x00001911
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00003719 File Offset: 0x00001919
		public long NumberOfEntries { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003722 File Offset: 0x00001922
		// (set) Token: 0x0600007F RID: 127 RVA: 0x0000372A File Offset: 0x0000192A
		public string AverageLatency { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003733 File Offset: 0x00001933
		// (set) Token: 0x06000081 RID: 129 RVA: 0x0000373B File Offset: 0x0000193B
		public string DiskUsage { get; set; }
	}
}
