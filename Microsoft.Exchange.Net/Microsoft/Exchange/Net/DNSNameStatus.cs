using System;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BE6 RID: 3046
	internal enum DNSNameStatus : uint
	{
		// Token: 0x040038E4 RID: 14564
		Valid,
		// Token: 0x040038E5 RID: 14565
		InvalidCharacter = 9560U,
		// Token: 0x040038E6 RID: 14566
		NumericName,
		// Token: 0x040038E7 RID: 14567
		InvalidName = 123U,
		// Token: 0x040038E8 RID: 14568
		NonRFCName = 9556U
	}
}
