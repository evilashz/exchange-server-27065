using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.Provisioning;

namespace Microsoft.Exchange.Management.ProvisioningTasks
{
	// Token: 0x02000CDB RID: 3291
	public abstract class GetProvisioningPolicyBase<TIdentity, TDataObject> : GetMultitenancySystemConfigurationObjectTask<TIdentity, TDataObject> where TIdentity : ProvisioningPolicyIdParameter, new() where TDataObject : ADProvisioningPolicy, new()
	{
		// Token: 0x1700276A RID: 10090
		// (get) Token: 0x06007EE3 RID: 32483 RVA: 0x00206530 File Offset: 0x00204730
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
