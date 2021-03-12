using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Provisioning;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;

namespace Microsoft.Exchange.DefaultProvisioningAgent.PolicyEngine
{
	// Token: 0x02000035 RID: 53
	internal interface IPolicyDataProvider
	{
		// Token: 0x06000156 RID: 342
		IEnumerable<ADProvisioningPolicy> GetEffectiveProvisioningPolicy(OrganizationId organizationId, Type poType, ProvisioningPolicyType policyType, int maxResults, ProvisioningCache provisioningCache);

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000157 RID: 343
		string Source { get; }
	}
}
