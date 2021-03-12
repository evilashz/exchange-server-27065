using System;

namespace Microsoft.Exchange.Rpc.Cluster
{
	// Token: 0x02000139 RID: 313
	[Flags]
	internal enum RpcGetDatabaseCopyStatusFlags : uint
	{
		// Token: 0x04000A91 RID: 2705
		None = 0U,
		// Token: 0x04000A92 RID: 2706
		CollectConnectionStatus = 1U,
		// Token: 0x04000A93 RID: 2707
		CollectExtendedErrorInfo = 2U,
		// Token: 0x04000A94 RID: 2708
		UseServerSideCaching = 4U
	}
}
