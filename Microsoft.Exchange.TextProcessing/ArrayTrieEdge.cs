using System;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000024 RID: 36
	internal struct ArrayTrieEdge
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0000C57F File Offset: 0x0000A77F
		// (set) Token: 0x0600016C RID: 364 RVA: 0x0000C587 File Offset: 0x0000A787
		public int Index { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000C590 File Offset: 0x0000A790
		// (set) Token: 0x0600016E RID: 366 RVA: 0x0000C598 File Offset: 0x0000A798
		public char Character { get; set; }
	}
}
