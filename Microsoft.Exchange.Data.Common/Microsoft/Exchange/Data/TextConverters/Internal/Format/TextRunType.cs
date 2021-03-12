using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Format
{
	// Token: 0x0200029B RID: 667
	internal enum TextRunType : ushort
	{
		// Token: 0x04002085 RID: 8325
		Invalid,
		// Token: 0x04002086 RID: 8326
		Skip = 0,
		// Token: 0x04002087 RID: 8327
		Markup = 4096,
		// Token: 0x04002088 RID: 8328
		NonSpace = 8192,
		// Token: 0x04002089 RID: 8329
		FirstShort = 12288,
		// Token: 0x0400208A RID: 8330
		InlineObject = 12288,
		// Token: 0x0400208B RID: 8331
		NbSp = 16384,
		// Token: 0x0400208C RID: 8332
		Space = 20480,
		// Token: 0x0400208D RID: 8333
		Tabulation = 24576,
		// Token: 0x0400208E RID: 8334
		NewLine = 28672,
		// Token: 0x0400208F RID: 8335
		BlockBoundary = 32768
	}
}
