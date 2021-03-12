using System;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x02000207 RID: 519
	public class SpamRuleData : RuleDataBase
	{
		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x0004359F File Offset: 0x0004179F
		// (set) Token: 0x0600159F RID: 5535 RVA: 0x000435A7 File Offset: 0x000417A7
		public PredicateData Predicate { get; set; }

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x000435B0 File Offset: 0x000417B0
		// (set) Token: 0x060015A1 RID: 5537 RVA: 0x000435B8 File Offset: 0x000417B8
		public ResultData Result { get; set; }

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x060015A2 RID: 5538 RVA: 0x000435C1 File Offset: 0x000417C1
		// (set) Token: 0x060015A3 RID: 5539 RVA: 0x000435C9 File Offset: 0x000417C9
		public int? AsfID { get; set; }

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x060015A4 RID: 5540 RVA: 0x000435D2 File Offset: 0x000417D2
		// (set) Token: 0x060015A5 RID: 5541 RVA: 0x000435DA File Offset: 0x000417DA
		public string ConditionMatchPhrase { get; set; }

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x060015A6 RID: 5542 RVA: 0x000435E3 File Offset: 0x000417E3
		// (set) Token: 0x060015A7 RID: 5543 RVA: 0x000435EB File Offset: 0x000417EB
		public string ConditionNotMatchPhrase { get; set; }

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x000435F4 File Offset: 0x000417F4
		// (set) Token: 0x060015A9 RID: 5545 RVA: 0x000435FC File Offset: 0x000417FC
		public AuthoringData AuthoringProperties { get; set; }
	}
}
