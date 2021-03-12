using System;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x0200002B RID: 43
	internal sealed class KeywordEntry
	{
		// Token: 0x060001A4 RID: 420 RVA: 0x0000D21C File Offset: 0x0000B41C
		public KeywordEntry(string keyword, long identifier)
		{
			this.Keyword = keyword;
			this.Identifier = identifier;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000D232 File Offset: 0x0000B432
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x0000D23A File Offset: 0x0000B43A
		public string Keyword { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000D243 File Offset: 0x0000B443
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x0000D24B File Offset: 0x0000B44B
		public long Identifier { get; set; }
	}
}
