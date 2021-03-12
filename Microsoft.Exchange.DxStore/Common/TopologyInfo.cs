using System;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000048 RID: 72
	public class TopologyInfo
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600025C RID: 604 RVA: 0x000045A7 File Offset: 0x000027A7
		// (set) Token: 0x0600025D RID: 605 RVA: 0x000045AF File Offset: 0x000027AF
		public bool IsConfigured { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600025E RID: 606 RVA: 0x000045B8 File Offset: 0x000027B8
		// (set) Token: 0x0600025F RID: 607 RVA: 0x000045C0 File Offset: 0x000027C0
		public bool IsAllMembersVersionCompatible { get; set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000260 RID: 608 RVA: 0x000045C9 File Offset: 0x000027C9
		// (set) Token: 0x06000261 RID: 609 RVA: 0x000045D1 File Offset: 0x000027D1
		public string Name { get; set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000262 RID: 610 RVA: 0x000045DA File Offset: 0x000027DA
		// (set) Token: 0x06000263 RID: 611 RVA: 0x000045E2 File Offset: 0x000027E2
		public string[] Members { get; set; }
	}
}
