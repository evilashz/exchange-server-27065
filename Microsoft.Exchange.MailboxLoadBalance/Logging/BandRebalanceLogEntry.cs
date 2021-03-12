using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.Logging
{
	// Token: 0x020000A0 RID: 160
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class BandRebalanceLogEntry
	{
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x0000F151 File Offset: 0x0000D351
		// (set) Token: 0x060005AC RID: 1452 RVA: 0x0000F159 File Offset: 0x0000D359
		public string SourceDatabase { get; set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0000F162 File Offset: 0x0000D362
		// (set) Token: 0x060005AE RID: 1454 RVA: 0x0000F16A File Offset: 0x0000D36A
		public string TargetDatabase { get; set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0000F173 File Offset: 0x0000D373
		// (set) Token: 0x060005B0 RID: 1456 RVA: 0x0000F17B File Offset: 0x0000D37B
		public long RebalanceUnits { get; set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0000F184 File Offset: 0x0000D384
		// (set) Token: 0x060005B2 RID: 1458 RVA: 0x0000F18C File Offset: 0x0000D38C
		public string BatchName { get; set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0000F195 File Offset: 0x0000D395
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x0000F19D File Offset: 0x0000D39D
		public string Metric { get; set; }
	}
}
