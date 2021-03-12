using System;

namespace Microsoft.Mapi
{
	// Token: 0x02000030 RID: 48
	[Flags]
	internal enum ContentFlags
	{
		// Token: 0x04000330 RID: 816
		FullString = 0,
		// Token: 0x04000331 RID: 817
		SubString = 1,
		// Token: 0x04000332 RID: 818
		Prefix = 2,
		// Token: 0x04000333 RID: 819
		PrefixOnWords = 17,
		// Token: 0x04000334 RID: 820
		ExactPhrase = 32,
		// Token: 0x04000335 RID: 821
		IgnoreCase = 65536,
		// Token: 0x04000336 RID: 822
		IgnoreNonSpace = 131072,
		// Token: 0x04000337 RID: 823
		Loose = 262144
	}
}
