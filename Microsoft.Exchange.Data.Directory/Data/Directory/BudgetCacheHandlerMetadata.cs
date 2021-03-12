using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009A5 RID: 2469
	public class BudgetCacheHandlerMetadata
	{
		// Token: 0x1700283B RID: 10299
		// (get) Token: 0x060071F7 RID: 29175 RVA: 0x00179E30 File Offset: 0x00178030
		// (set) Token: 0x060071F8 RID: 29176 RVA: 0x00179E38 File Offset: 0x00178038
		public int TotalCount { get; set; }

		// Token: 0x1700283C RID: 10300
		// (get) Token: 0x060071F9 RID: 29177 RVA: 0x00179E41 File Offset: 0x00178041
		// (set) Token: 0x060071FA RID: 29178 RVA: 0x00179E49 File Offset: 0x00178049
		public int MatchingCount { get; set; }

		// Token: 0x1700283D RID: 10301
		// (get) Token: 0x060071FB RID: 29179 RVA: 0x00179E52 File Offset: 0x00178052
		// (set) Token: 0x060071FC RID: 29180 RVA: 0x00179E5A File Offset: 0x0017805A
		public int Efficiency { get; set; }

		// Token: 0x1700283E RID: 10302
		// (get) Token: 0x060071FD RID: 29181 RVA: 0x00179E63 File Offset: 0x00178063
		// (set) Token: 0x060071FE RID: 29182 RVA: 0x00179E6B File Offset: 0x0017806B
		public int NotThrottled { get; set; }

		// Token: 0x1700283F RID: 10303
		// (get) Token: 0x060071FF RID: 29183 RVA: 0x00179E74 File Offset: 0x00178074
		// (set) Token: 0x06007200 RID: 29184 RVA: 0x00179E7C File Offset: 0x0017807C
		public int InMicroDelay { get; set; }

		// Token: 0x17002840 RID: 10304
		// (get) Token: 0x06007201 RID: 29185 RVA: 0x00179E85 File Offset: 0x00178085
		// (set) Token: 0x06007202 RID: 29186 RVA: 0x00179E8D File Offset: 0x0017808D
		public int InCutoff { get; set; }

		// Token: 0x17002841 RID: 10305
		// (get) Token: 0x06007203 RID: 29187 RVA: 0x00179E96 File Offset: 0x00178096
		// (set) Token: 0x06007204 RID: 29188 RVA: 0x00179E9E File Offset: 0x0017809E
		public int ServiceAccountBudgets { get; set; }

		// Token: 0x17002842 RID: 10306
		// (get) Token: 0x06007205 RID: 29189 RVA: 0x00179EA7 File Offset: 0x001780A7
		// (set) Token: 0x06007206 RID: 29190 RVA: 0x00179EAF File Offset: 0x001780AF
		public List<BudgetHandlerMetadata> Budgets { get; set; }
	}
}
