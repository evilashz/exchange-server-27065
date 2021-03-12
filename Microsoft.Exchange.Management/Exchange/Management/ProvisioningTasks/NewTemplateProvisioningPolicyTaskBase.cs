using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Provisioning;

namespace Microsoft.Exchange.Management.ProvisioningTasks
{
	// Token: 0x02000CDE RID: 3294
	public abstract class NewTemplateProvisioningPolicyTaskBase<TDataObject> : NewProvisioningPolicyTaskBase<TDataObject> where TDataObject : TemplateProvisioningPolicy, new()
	{
		// Token: 0x1700276C RID: 10092
		// (get) Token: 0x06007EE9 RID: 32489 RVA: 0x00206764 File Offset: 0x00204964
		protected override ADObjectId ContainerRdn
		{
			get
			{
				return TemplateProvisioningPolicy.RdnTemplateContainer;
			}
		}
	}
}
