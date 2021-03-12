using System;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000137 RID: 311
	public interface ITenantInfoProviderFactory
	{
		// Token: 0x06000913 RID: 2323
		ITenantInfoProvider CreateTenantInfoProvider(TenantContext tenantContext);
	}
}
