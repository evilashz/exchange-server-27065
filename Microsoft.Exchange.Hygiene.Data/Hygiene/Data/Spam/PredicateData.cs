using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001E2 RID: 482
	public class PredicateData
	{
		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x00040D84 File Offset: 0x0003EF84
		// (set) Token: 0x06001455 RID: 5205 RVA: 0x00040D8C File Offset: 0x0003EF8C
		public long? RuleID { get; set; }

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x00040D95 File Offset: 0x0003EF95
		// (set) Token: 0x06001457 RID: 5207 RVA: 0x00040D9D File Offset: 0x0003EF9D
		public PredicateType PredicateType { get; set; }

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x00040DA6 File Offset: 0x0003EFA6
		// (set) Token: 0x06001459 RID: 5209 RVA: 0x00040DAE File Offset: 0x0003EFAE
		public List<PredicateData> ChildPredicate { get; set; }

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x00040DB7 File Offset: 0x0003EFB7
		// (set) Token: 0x0600145B RID: 5211 RVA: 0x00040DBF File Offset: 0x0003EFBF
		public long? ProcessorID { get; set; }

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x00040DC8 File Offset: 0x0003EFC8
		// (set) Token: 0x0600145D RID: 5213 RVA: 0x00040DD0 File Offset: 0x0003EFD0
		public int? Sequence { get; set; }

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x00040DD9 File Offset: 0x0003EFD9
		// (set) Token: 0x0600145F RID: 5215 RVA: 0x00040DE1 File Offset: 0x0003EFE1
		public int? MinOccurs { get; set; }

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x00040DEA File Offset: 0x0003EFEA
		// (set) Token: 0x06001461 RID: 5217 RVA: 0x00040DF2 File Offset: 0x0003EFF2
		public int? MaxOccurs { get; set; }

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x00040DFB File Offset: 0x0003EFFB
		// (set) Token: 0x06001463 RID: 5219 RVA: 0x00040E03 File Offset: 0x0003F003
		public string Target { get; set; }

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x00040E0C File Offset: 0x0003F00C
		// (set) Token: 0x06001465 RID: 5221 RVA: 0x00040E14 File Offset: 0x0003F014
		public long? Value { get; set; }

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x00040E1D File Offset: 0x0003F01D
		// (set) Token: 0x06001467 RID: 5223 RVA: 0x00040E25 File Offset: 0x0003F025
		public NumericOperationType? Operation { get; set; }
	}
}
