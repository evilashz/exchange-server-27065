using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020000F4 RID: 244
	internal enum TenantConsistencyMode : byte
	{
		// Token: 0x04000531 RID: 1329
		ExpectOnlyLiveTenants,
		// Token: 0x04000532 RID: 1330
		IncludeRetiredTenants,
		// Token: 0x04000533 RID: 1331
		IgnoreRetiredTenants
	}
}
