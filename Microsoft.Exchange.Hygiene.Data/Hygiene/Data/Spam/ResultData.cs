using System;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001ED RID: 493
	public class ResultData
	{
		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x060014BB RID: 5307 RVA: 0x000418D2 File Offset: 0x0003FAD2
		// (set) Token: 0x060014BC RID: 5308 RVA: 0x000418DA File Offset: 0x0003FADA
		public ResultType ResultType { get; set; }

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x060014BD RID: 5309 RVA: 0x000418E3 File Offset: 0x0003FAE3
		// (set) Token: 0x060014BE RID: 5310 RVA: 0x000418EB File Offset: 0x0003FAEB
		public int? Score { get; set; }

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x000418F4 File Offset: 0x0003FAF4
		// (set) Token: 0x060014C0 RID: 5312 RVA: 0x000418FC File Offset: 0x0003FAFC
		public string ResponseString { get; set; }
	}
}
