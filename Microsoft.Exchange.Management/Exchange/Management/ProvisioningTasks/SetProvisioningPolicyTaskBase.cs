using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.Provisioning;

namespace Microsoft.Exchange.Management.ProvisioningTasks
{
	// Token: 0x02000CE0 RID: 3296
	public abstract class SetProvisioningPolicyTaskBase<TIdentity, TDataObject> : SetSystemConfigurationObjectTask<TIdentity, TDataObject> where TIdentity : IIdentityParameter, new() where TDataObject : ADProvisioningPolicy, new()
	{
	}
}
