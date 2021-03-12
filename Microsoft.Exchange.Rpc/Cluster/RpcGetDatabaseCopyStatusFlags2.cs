using System;

namespace Microsoft.Exchange.Rpc.Cluster
{
	// Token: 0x0200013A RID: 314
	[Flags]
	internal enum RpcGetDatabaseCopyStatusFlags2 : uint
	{
		// Token: 0x04000A96 RID: 2710
		None = 0U,
		// Token: 0x04000A97 RID: 2711
		ReadThrough = 1U,
		// Token: 0x04000A98 RID: 2712
		TestOOMHandling = 32768U
	}
}
