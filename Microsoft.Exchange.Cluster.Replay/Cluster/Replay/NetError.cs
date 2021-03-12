using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200036C RID: 876
	internal enum NetError : uint
	{
		// Token: 0x04000EE1 RID: 3809
		NERR_Success,
		// Token: 0x04000EE2 RID: 3810
		NERR_BASE = 2100U,
		// Token: 0x04000EE3 RID: 3811
		NERR_UnknownDevDir = 2116U,
		// Token: 0x04000EE4 RID: 3812
		NERR_DuplicateShare = 2118U,
		// Token: 0x04000EE5 RID: 3813
		NERR_BufTooSmall = 2123U,
		// Token: 0x04000EE6 RID: 3814
		NERR_NetNameNotFound = 2310U
	}
}
