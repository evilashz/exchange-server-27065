using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200020C RID: 524
	[Flags]
	internal enum FuzzyLevel : uint
	{
		// Token: 0x04000675 RID: 1653
		FullString = 0U,
		// Token: 0x04000676 RID: 1654
		SubString = 1U,
		// Token: 0x04000677 RID: 1655
		Prefix = 2U,
		// Token: 0x04000678 RID: 1656
		PrefixOnWords = 16U,
		// Token: 0x04000679 RID: 1657
		ExactPhrase = 32U,
		// Token: 0x0400067A RID: 1658
		IgnoreCase = 65536U,
		// Token: 0x0400067B RID: 1659
		IgnoreNonSpace = 131072U,
		// Token: 0x0400067C RID: 1660
		Loose = 262144U
	}
}
