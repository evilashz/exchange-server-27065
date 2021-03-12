using System;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x02000090 RID: 144
	public interface IPolicyConfigProviderManager
	{
		// Token: 0x0600037A RID: 890
		PolicyConfigProvider CreateForSyncEngine(Guid organizationId, string auxiliaryStore, bool enablePolicyApplication = true, ExecutionLog logProvider = null);
	}
}
