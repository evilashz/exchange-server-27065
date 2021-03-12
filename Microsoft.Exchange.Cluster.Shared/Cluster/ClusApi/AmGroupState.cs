using System;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000052 RID: 82
	internal enum AmGroupState
	{
		// Token: 0x040000F9 RID: 249
		Unknown = -1,
		// Token: 0x040000FA RID: 250
		Online,
		// Token: 0x040000FB RID: 251
		Offline,
		// Token: 0x040000FC RID: 252
		Failed,
		// Token: 0x040000FD RID: 253
		PartialOnline,
		// Token: 0x040000FE RID: 254
		Pending
	}
}
