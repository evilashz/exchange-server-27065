using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200038C RID: 908
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CalendarViewLatencyInformation
	{
		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x060027D4 RID: 10196 RVA: 0x0009F39C File Offset: 0x0009D59C
		// (set) Token: 0x060027D5 RID: 10197 RVA: 0x0009F3A4 File Offset: 0x0009D5A4
		public bool IsNewView { get; set; }

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x060027D6 RID: 10198 RVA: 0x0009F3AD File Offset: 0x0009D5AD
		// (set) Token: 0x060027D7 RID: 10199 RVA: 0x0009F3B5 File Offset: 0x0009D5B5
		public long ViewTime { get; set; }

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x060027D8 RID: 10200 RVA: 0x0009F3BE File Offset: 0x0009D5BE
		// (set) Token: 0x060027D9 RID: 10201 RVA: 0x0009F3C6 File Offset: 0x0009D5C6
		public long SingleItemTotalTime { get; set; }

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x060027DA RID: 10202 RVA: 0x0009F3CF File Offset: 0x0009D5CF
		// (set) Token: 0x060027DB RID: 10203 RVA: 0x0009F3D7 File Offset: 0x0009D5D7
		public long SingleItemQueryTime { get; set; }

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x060027DC RID: 10204 RVA: 0x0009F3E0 File Offset: 0x0009D5E0
		// (set) Token: 0x060027DD RID: 10205 RVA: 0x0009F3E8 File Offset: 0x0009D5E8
		public long SingleQuerySeekToTime { get; set; }

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x060027DE RID: 10206 RVA: 0x0009F3F1 File Offset: 0x0009D5F1
		// (set) Token: 0x060027DF RID: 10207 RVA: 0x0009F3F9 File Offset: 0x0009D5F9
		public long SingleItemGetRowsTime { get; set; }

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x060027E0 RID: 10208 RVA: 0x0009F402 File Offset: 0x0009D602
		// (set) Token: 0x060027E1 RID: 10209 RVA: 0x0009F40A File Offset: 0x0009D60A
		public int SingleItemQueryCount { get; set; }

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x060027E2 RID: 10210 RVA: 0x0009F413 File Offset: 0x0009D613
		// (set) Token: 0x060027E3 RID: 10211 RVA: 0x0009F41B File Offset: 0x0009D61B
		public long RecurringItemTotalTime { get; set; }

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x060027E4 RID: 10212 RVA: 0x0009F424 File Offset: 0x0009D624
		// (set) Token: 0x060027E5 RID: 10213 RVA: 0x0009F42C File Offset: 0x0009D62C
		public long RecurringItemQueryTime { get; set; }

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x060027E6 RID: 10214 RVA: 0x0009F435 File Offset: 0x0009D635
		// (set) Token: 0x060027E7 RID: 10215 RVA: 0x0009F43D File Offset: 0x0009D63D
		public long RecurringItemGetRowsTime { get; set; }

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x060027E8 RID: 10216 RVA: 0x0009F446 File Offset: 0x0009D646
		// (set) Token: 0x060027E9 RID: 10217 RVA: 0x0009F44E File Offset: 0x0009D64E
		public long RecurringExpansionTime { get; set; }

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x060027EA RID: 10218 RVA: 0x0009F457 File Offset: 0x0009D657
		// (set) Token: 0x060027EB RID: 10219 RVA: 0x0009F45F File Offset: 0x0009D65F
		public RecurringItemLatencyInformation MaxRecurringItemLatencyInformation { get; set; }

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x060027EC RID: 10220 RVA: 0x0009F468 File Offset: 0x0009D668
		// (set) Token: 0x060027ED RID: 10221 RVA: 0x0009F470 File Offset: 0x0009D670
		public long RecurringItemQueryCount { get; set; }

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x060027EE RID: 10222 RVA: 0x0009F479 File Offset: 0x0009D679
		// (set) Token: 0x060027EF RID: 10223 RVA: 0x0009F481 File Offset: 0x0009D681
		public long RecurringItemsNoInstancesInWindow { get; set; }
	}
}
