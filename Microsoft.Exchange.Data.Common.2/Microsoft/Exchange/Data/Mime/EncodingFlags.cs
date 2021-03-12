using System;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x0200002B RID: 43
	[Flags]
	public enum EncodingFlags : byte
	{
		// Token: 0x04000107 RID: 263
		None = 0,
		// Token: 0x04000108 RID: 264
		ForceReencode = 1,
		// Token: 0x04000109 RID: 265
		EnableRfc2231 = 2,
		// Token: 0x0400010A RID: 266
		QuoteDisplayNameBeforeRfc2047Encoding = 4,
		// Token: 0x0400010B RID: 267
		AllowUTF8 = 8
	}
}
