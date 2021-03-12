using System;

namespace System.Globalization
{
	// Token: 0x020003B0 RID: 944
	internal struct HebrewNumberParsingContext
	{
		// Token: 0x060031BC RID: 12732 RVA: 0x000BF8D7 File Offset: 0x000BDAD7
		public HebrewNumberParsingContext(int result)
		{
			this.state = HebrewNumber.HS.Start;
			this.result = result;
		}

		// Token: 0x040015D0 RID: 5584
		internal HebrewNumber.HS state;

		// Token: 0x040015D1 RID: 5585
		internal int result;
	}
}
