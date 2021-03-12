using System;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020007CA RID: 1994
	internal enum TenantFullSyncState
	{
		// Token: 0x04004239 RID: 16953
		EnumerateLiveObjects,
		// Token: 0x0400423A RID: 16954
		EnumerateLinksInPage,
		// Token: 0x0400423B RID: 16955
		EnumerateDeletedObjects,
		// Token: 0x0400423C RID: 16956
		Complete,
		// Token: 0x0400423D RID: 16957
		EnumerateSoftDeletedObjects
	}
}
