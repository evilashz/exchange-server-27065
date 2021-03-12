using System;

namespace Microsoft.Exchange.Rpc.Cluster
{
	// Token: 0x02000141 RID: 321
	internal enum SeederState
	{
		// Token: 0x04000ACC RID: 2764
		Unknown,
		// Token: 0x04000ACD RID: 2765
		SeedPrepared,
		// Token: 0x04000ACE RID: 2766
		SeedInProgress,
		// Token: 0x04000ACF RID: 2767
		SeedSuccessful,
		// Token: 0x04000AD0 RID: 2768
		SeedCancelled,
		// Token: 0x04000AD1 RID: 2769
		SeedFailed
	}
}
