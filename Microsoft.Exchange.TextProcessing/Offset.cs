using System;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x0200002D RID: 45
	public struct Offset
	{
		// Token: 0x060001B1 RID: 433 RVA: 0x0000D448 File Offset: 0x0000B648
		public Offset(int start, int end)
		{
			this.Start = start;
			this.End = end;
		}

		// Token: 0x04000105 RID: 261
		public int Start;

		// Token: 0x04000106 RID: 262
		public int End;
	}
}
