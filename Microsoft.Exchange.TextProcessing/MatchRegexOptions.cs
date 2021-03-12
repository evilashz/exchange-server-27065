using System;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x0200003B RID: 59
	[Flags]
	internal enum MatchRegexOptions : byte
	{
		// Token: 0x0400013D RID: 317
		None = 0,
		// Token: 0x0400013E RID: 318
		Compiled = 1,
		// Token: 0x0400013F RID: 319
		ExplicitCaptures = 2,
		// Token: 0x04000140 RID: 320
		Cached = 4,
		// Token: 0x04000141 RID: 321
		Primed = 8,
		// Token: 0x04000142 RID: 322
		LazyOptimize = 16,
		// Token: 0x04000143 RID: 323
		CultureInvariant = 32
	}
}
