using System;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000053 RID: 83
	internal enum AmResourceState
	{
		// Token: 0x04000100 RID: 256
		Unknown = -1,
		// Token: 0x04000101 RID: 257
		Inherited,
		// Token: 0x04000102 RID: 258
		Initializing,
		// Token: 0x04000103 RID: 259
		Online,
		// Token: 0x04000104 RID: 260
		Offline,
		// Token: 0x04000105 RID: 261
		Failed,
		// Token: 0x04000106 RID: 262
		CannotComeOnlineOnAnyNode = 126,
		// Token: 0x04000107 RID: 263
		CannotComeOnlineOnThisNode,
		// Token: 0x04000108 RID: 264
		Pending,
		// Token: 0x04000109 RID: 265
		OnlinePending,
		// Token: 0x0400010A RID: 266
		OfflinePending
	}
}
