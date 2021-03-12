using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Provisioning;

namespace Microsoft.Exchange.Management.ProvisioningTasks
{
	// Token: 0x02000CDF RID: 3295
	public abstract class NewEnforcementProvisioningPolicyTaskBase<TDataObject> : NewProvisioningPolicyTaskBase<TDataObject> where TDataObject : EnforcementProvisioningPolicy, new()
	{
		// Token: 0x1700276D RID: 10093
		// (get) Token: 0x06007EEB RID: 32491 RVA: 0x00206773 File Offset: 0x00204973
		protected override ADObjectId ContainerRdn
		{
			get
			{
				return EnforcementProvisioningPolicy.RdnEnforcementContainer;
			}
		}
	}
}
