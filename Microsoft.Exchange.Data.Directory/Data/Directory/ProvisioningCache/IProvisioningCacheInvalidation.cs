using System;

namespace Microsoft.Exchange.Data.Directory.ProvisioningCache
{
	// Token: 0x020001E9 RID: 489
	internal interface IProvisioningCacheInvalidation
	{
		// Token: 0x060016F6 RID: 5878
		bool ShouldInvalidProvisioningCache(out OrganizationId orgId, out Guid[] keys);
	}
}
