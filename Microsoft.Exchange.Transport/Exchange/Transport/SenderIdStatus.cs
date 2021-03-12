using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000442 RID: 1090
	internal enum SenderIdStatus : uint
	{
		// Token: 0x0400198D RID: 6541
		NEUTRAL = 1U,
		// Token: 0x0400198E RID: 6542
		PASS,
		// Token: 0x0400198F RID: 6543
		FAIL,
		// Token: 0x04001990 RID: 6544
		SOFTFAIL,
		// Token: 0x04001991 RID: 6545
		NONE,
		// Token: 0x04001992 RID: 6546
		TEMPERROR = 2147483654U,
		// Token: 0x04001993 RID: 6547
		PERMERROR
	}
}
