using System;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000136 RID: 310
	public interface ITenantInfoProvider : IDisposable
	{
		// Token: 0x06000911 RID: 2321
		void Save(TenantInfo tenantInfo);

		// Token: 0x06000912 RID: 2322
		TenantInfo Load();
	}
}
