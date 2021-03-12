using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020000FC RID: 252
	internal enum ConfigScopes
	{
		// Token: 0x04000563 RID: 1379
		None,
		// Token: 0x04000564 RID: 1380
		TenantLocal,
		// Token: 0x04000565 RID: 1381
		TenantSubTree,
		// Token: 0x04000566 RID: 1382
		Global,
		// Token: 0x04000567 RID: 1383
		Server,
		// Token: 0x04000568 RID: 1384
		Database,
		// Token: 0x04000569 RID: 1385
		AllTenants,
		// Token: 0x0400056A RID: 1386
		RootOrg
	}
}
