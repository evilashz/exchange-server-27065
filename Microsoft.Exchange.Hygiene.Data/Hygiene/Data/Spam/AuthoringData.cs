using System;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001D1 RID: 465
	public class AuthoringData
	{
		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x0600137A RID: 4986 RVA: 0x0003AD7B File Offset: 0x00038F7B
		// (set) Token: 0x0600137B RID: 4987 RVA: 0x0003AD83 File Offset: 0x00038F83
		public long GroupID { get; set; }

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x0600137C RID: 4988 RVA: 0x0003AD8C File Offset: 0x00038F8C
		// (set) Token: 0x0600137D RID: 4989 RVA: 0x0003AD94 File Offset: 0x00038F94
		public byte RuleTarget { get; set; }

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x0600137E RID: 4990 RVA: 0x0003AD9D File Offset: 0x00038F9D
		// (set) Token: 0x0600137F RID: 4991 RVA: 0x0003ADA5 File Offset: 0x00038FA5
		public string Regex { get; set; }

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x0003ADAE File Offset: 0x00038FAE
		// (set) Token: 0x06001381 RID: 4993 RVA: 0x0003ADB6 File Offset: 0x00038FB6
		public byte LanguageID { get; set; }

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x0003ADBF File Offset: 0x00038FBF
		// (set) Token: 0x06001383 RID: 4995 RVA: 0x0003ADC7 File Offset: 0x00038FC7
		public byte Category { get; set; }

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x0003ADD0 File Offset: 0x00038FD0
		// (set) Token: 0x06001385 RID: 4997 RVA: 0x0003ADD8 File Offset: 0x00038FD8
		public string Flags { get; set; }

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001386 RID: 4998 RVA: 0x0003ADE1 File Offset: 0x00038FE1
		// (set) Token: 0x06001387 RID: 4999 RVA: 0x0003ADE9 File Offset: 0x00038FE9
		public byte ActionID { get; set; }
	}
}
