using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001FB RID: 507
	[Flags]
	internal enum AutoResponseSuppress
	{
		// Token: 0x04000E53 RID: 3667
		DR = 1,
		// Token: 0x04000E54 RID: 3668
		NDR = 2,
		// Token: 0x04000E55 RID: 3669
		RN = 4,
		// Token: 0x04000E56 RID: 3670
		NRN = 8,
		// Token: 0x04000E57 RID: 3671
		OOF = 16,
		// Token: 0x04000E58 RID: 3672
		AutoReply = 32,
		// Token: 0x04000E59 RID: 3673
		None = 0,
		// Token: 0x04000E5A RID: 3674
		All = -1
	}
}
