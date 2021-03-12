using System;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001E8 RID: 488
	public class ProcessorData : ICloneable
	{
		// Token: 0x06001479 RID: 5241 RVA: 0x00041304 File Offset: 0x0003F504
		public ProcessorData(ProcessorData copy)
		{
			this.CachingEnabled = copy.CachingEnabled;
			this.CaseSensitivityType = copy.CaseSensitivityType;
			this.Coefficient = copy.Coefficient;
			this.ExpectedResult = copy.ExpectedResult;
			this.Keywords = copy.Keywords;
			this.Name = copy.Name;
			this.Precondition = copy.Precondition;
			this.ProcessorID = copy.ProcessorID;
			this.ProcessorType = copy.ProcessorType;
			this.Target = copy.Target;
			this.Value = copy.Value;
			this.WordBoundary = copy.WordBoundary;
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x000413A7 File Offset: 0x0003F5A7
		public ProcessorData()
		{
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x0600147B RID: 5243 RVA: 0x000413AF File Offset: 0x0003F5AF
		// (set) Token: 0x0600147C RID: 5244 RVA: 0x000413B7 File Offset: 0x0003F5B7
		public long ProcessorID { get; set; }

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x000413C0 File Offset: 0x0003F5C0
		// (set) Token: 0x0600147E RID: 5246 RVA: 0x000413C8 File Offset: 0x0003F5C8
		public ProcessorType ProcessorType { get; set; }

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x0600147F RID: 5247 RVA: 0x000413D1 File Offset: 0x0003F5D1
		// (set) Token: 0x06001480 RID: 5248 RVA: 0x000413D9 File Offset: 0x0003F5D9
		public string Name { get; set; }

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x000413E2 File Offset: 0x0003F5E2
		// (set) Token: 0x06001482 RID: 5250 RVA: 0x000413EA File Offset: 0x0003F5EA
		public int? ExpectedResult { get; set; }

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x000413F3 File Offset: 0x0003F5F3
		// (set) Token: 0x06001484 RID: 5252 RVA: 0x000413FB File Offset: 0x0003F5FB
		public string WordBoundary { get; set; }

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x00041404 File Offset: 0x0003F604
		// (set) Token: 0x06001486 RID: 5254 RVA: 0x0004140C File Offset: 0x0003F60C
		public long? Precondition { get; set; }

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x00041415 File Offset: 0x0003F615
		// (set) Token: 0x06001488 RID: 5256 RVA: 0x0004141D File Offset: 0x0003F61D
		public string Value { get; set; }

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x00041426 File Offset: 0x0003F626
		// (set) Token: 0x0600148A RID: 5258 RVA: 0x0004142E File Offset: 0x0003F62E
		public string[] Keywords { get; set; }

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x00041437 File Offset: 0x0003F637
		// (set) Token: 0x0600148C RID: 5260 RVA: 0x0004143F File Offset: 0x0003F63F
		public string Target { get; set; }

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x0600148D RID: 5261 RVA: 0x00041448 File Offset: 0x0003F648
		// (set) Token: 0x0600148E RID: 5262 RVA: 0x00041450 File Offset: 0x0003F650
		public byte? CaseSensitivityType { get; set; }

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x00041459 File Offset: 0x0003F659
		// (set) Token: 0x06001490 RID: 5264 RVA: 0x00041461 File Offset: 0x0003F661
		public bool? CachingEnabled { get; set; }

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x0004146A File Offset: 0x0003F66A
		// (set) Token: 0x06001492 RID: 5266 RVA: 0x00041472 File Offset: 0x0003F672
		public double? Coefficient { get; set; }

		// Token: 0x06001493 RID: 5267 RVA: 0x0004147B File Offset: 0x0003F67B
		public object Clone()
		{
			return new ProcessorData(this);
		}
	}
}
