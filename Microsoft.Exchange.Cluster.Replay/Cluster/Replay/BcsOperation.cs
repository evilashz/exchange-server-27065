using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002BE RID: 702
	internal enum BcsOperation
	{
		// Token: 0x04000B1A RID: 2842
		HasDatabaseBeenMounted = 1,
		// Token: 0x04000B1B RID: 2843
		GetDatabaseCopies,
		// Token: 0x04000B1C RID: 2844
		DetermineServersToContact,
		// Token: 0x04000B1D RID: 2845
		GetCopyStatusRpc,
		// Token: 0x04000B1E RID: 2846
		BcsOverall
	}
}
