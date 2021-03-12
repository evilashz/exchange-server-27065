using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200031A RID: 794
	[Flags]
	internal enum QueryRowsFlags : byte
	{
		// Token: 0x04000A05 RID: 2565
		None = 0,
		// Token: 0x04000A06 RID: 2566
		DoNotAdvance = 1,
		// Token: 0x04000A07 RID: 2567
		SendMax = 2,
		// Token: 0x04000A08 RID: 2568
		Backwards = 4,
		// Token: 0x04000A09 RID: 2569
		ChainAlways = 8
	}
}
