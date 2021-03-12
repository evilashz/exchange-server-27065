using System;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001F4 RID: 500
	[Flags]
	internal enum AutoResponseSuppress
	{
		// Token: 0x04000B1D RID: 2845
		DR = 1,
		// Token: 0x04000B1E RID: 2846
		NDR = 2,
		// Token: 0x04000B1F RID: 2847
		RN = 4,
		// Token: 0x04000B20 RID: 2848
		NRN = 8,
		// Token: 0x04000B21 RID: 2849
		OOF = 16,
		// Token: 0x04000B22 RID: 2850
		AutoReply = 32
	}
}
