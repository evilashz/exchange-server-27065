using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Provisioning;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ProvisioningTasks
{
	// Token: 0x02000CE5 RID: 3301
	[Cmdlet("Remove", "RecipientEnforcementProvisioningPolicy", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveRecipientEnforcementProvisioningPolicy : RemoveSystemConfigurationObjectTask<RecipientEnforcementProvisioningPolicyIdParameter, RecipientEnforcementProvisioningPolicy>
	{
		// Token: 0x17002775 RID: 10101
		// (get) Token: 0x06007EFE RID: 32510 RVA: 0x00206A20 File Offset: 0x00204C20
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveRecipientEnforcementProvisioningPolicy(this.Identity.ToString());
			}
		}

		// Token: 0x17002776 RID: 10102
		// (get) Token: 0x06007EFF RID: 32511 RVA: 0x00206A32 File Offset: 0x00204C32
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Static;
			}
		}
	}
}
