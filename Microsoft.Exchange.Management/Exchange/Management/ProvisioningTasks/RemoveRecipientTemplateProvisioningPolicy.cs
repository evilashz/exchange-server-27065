using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Provisioning;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ProvisioningTasks
{
	// Token: 0x02000CE6 RID: 3302
	[Cmdlet("Remove", "RecipientTemplateProvisioningPolicy", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveRecipientTemplateProvisioningPolicy : RemoveSystemConfigurationObjectTask<ProvisioningPolicyIdParameter, RecipientTemplateProvisioningPolicy>
	{
		// Token: 0x17002777 RID: 10103
		// (get) Token: 0x06007F01 RID: 32513 RVA: 0x00206A3D File Offset: 0x00204C3D
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveRecipientTemplateProvisioningPolicy(this.Identity.ToString());
			}
		}
	}
}
