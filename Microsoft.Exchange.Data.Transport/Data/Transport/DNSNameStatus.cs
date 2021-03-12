using System;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x020000B1 RID: 177
	internal enum DNSNameStatus : uint
	{
		// Token: 0x04000219 RID: 537
		Valid,
		// Token: 0x0400021A RID: 538
		InvalidCharacter = 9560U,
		// Token: 0x0400021B RID: 539
		NumericName,
		// Token: 0x0400021C RID: 540
		InvalidName = 123U,
		// Token: 0x0400021D RID: 541
		NonRFCName = 9556U
	}
}
