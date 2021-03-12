using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000157 RID: 343
	internal class LastLogInfo
	{
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x0003A5E0 File Offset: 0x000387E0
		// (set) Token: 0x06000D46 RID: 3398 RVA: 0x0003A5E8 File Offset: 0x000387E8
		public long ClusterLastLogGen { get; set; }

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x0003A5F1 File Offset: 0x000387F1
		// (set) Token: 0x06000D48 RID: 3400 RVA: 0x0003A5F9 File Offset: 0x000387F9
		public DateTime ClusterLastLogTime { get; set; }

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x0003A602 File Offset: 0x00038802
		// (set) Token: 0x06000D4A RID: 3402 RVA: 0x0003A60A File Offset: 0x0003880A
		public Exception ClusterLastLogException { get; set; }

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x0003A613 File Offset: 0x00038813
		// (set) Token: 0x06000D4C RID: 3404 RVA: 0x0003A61B File Offset: 0x0003881B
		public bool ClusterTimeIsMissing { get; set; }

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000D4D RID: 3405 RVA: 0x0003A624 File Offset: 0x00038824
		// (set) Token: 0x06000D4E RID: 3406 RVA: 0x0003A62C File Offset: 0x0003882C
		public long ReplLastLogGen { get; set; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x0003A635 File Offset: 0x00038835
		// (set) Token: 0x06000D50 RID: 3408 RVA: 0x0003A63D File Offset: 0x0003883D
		public DateTime ReplLastLogTime { get; set; }

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000D51 RID: 3409 RVA: 0x0003A646 File Offset: 0x00038846
		// (set) Token: 0x06000D52 RID: 3410 RVA: 0x0003A64E File Offset: 0x0003884E
		public bool IsStale { get; set; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x0003A657 File Offset: 0x00038857
		// (set) Token: 0x06000D54 RID: 3412 RVA: 0x0003A65F File Offset: 0x0003885F
		public DateTime StaleCheckTime { get; set; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x0003A668 File Offset: 0x00038868
		// (set) Token: 0x06000D56 RID: 3414 RVA: 0x0003A670 File Offset: 0x00038870
		public long LastLogGenToReport { get; set; }

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000D57 RID: 3415 RVA: 0x0003A679 File Offset: 0x00038879
		// (set) Token: 0x06000D58 RID: 3416 RVA: 0x0003A681 File Offset: 0x00038881
		public DateTime CollectionTime { get; set; }
	}
}
