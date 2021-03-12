using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000356 RID: 854
	public enum TransportRecipientType
	{
		// Token: 0x04000AE1 RID: 2785
		Orig,
		// Token: 0x04000AE2 RID: 2786
		To,
		// Token: 0x04000AE3 RID: 2787
		Cc,
		// Token: 0x04000AE4 RID: 2788
		Bcc,
		// Token: 0x04000AE5 RID: 2789
		P1 = 268435456,
		// Token: 0x04000AE6 RID: 2790
		SubmittedTo = -2147483647,
		// Token: 0x04000AE7 RID: 2791
		SubmittedCc,
		// Token: 0x04000AE8 RID: 2792
		SubmittedBcc
	}
}
