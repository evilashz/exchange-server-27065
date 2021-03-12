using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002F0 RID: 752
	[Flags]
	internal enum LockTypeFlag
	{
		// Token: 0x0400094E RID: 2382
		None = 0,
		// Token: 0x0400094F RID: 2383
		LockWrite = 1,
		// Token: 0x04000950 RID: 2384
		LockExclusive = 2,
		// Token: 0x04000951 RID: 2385
		LockOnlyOnce = 4
	}
}
