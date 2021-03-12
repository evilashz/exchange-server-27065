using System;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x0200002D RID: 45
	[Flags]
	public enum DecodingFlags
	{
		// Token: 0x04000111 RID: 273
		None = 0,
		// Token: 0x04000112 RID: 274
		Rfc2047 = 1,
		// Token: 0x04000113 RID: 275
		Rfc2231 = 2,
		// Token: 0x04000114 RID: 276
		Jis = 4,
		// Token: 0x04000115 RID: 277
		Utf8 = 8,
		// Token: 0x04000116 RID: 278
		Dbcs = 16,
		// Token: 0x04000117 RID: 279
		AllEncodings = 65535,
		// Token: 0x04000118 RID: 280
		FallbackToRaw = 65536,
		// Token: 0x04000119 RID: 281
		AllowControlCharacters = 131072
	}
}
