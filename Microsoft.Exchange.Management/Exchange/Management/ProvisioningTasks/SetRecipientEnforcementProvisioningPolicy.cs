using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Provisioning;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ProvisioningTasks
{
	// Token: 0x02000CE7 RID: 3303
	[Cmdlet("Set", "RecipientEnforcementProvisioningPolicy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetRecipientEnforcementProvisioningPolicy : SetProvisioningPolicyTaskBase<RecipientEnforcementProvisioningPolicyIdParameter, RecipientEnforcementProvisioningPolicy>
	{
		// Token: 0x17002778 RID: 10104
		// (get) Token: 0x06007F03 RID: 32515 RVA: 0x00206A57 File Offset: 0x00204C57
		// (set) Token: 0x06007F04 RID: 32516 RVA: 0x00206A5F File Offset: 0x00204C5F
		[Parameter(Mandatory = false)]
		public SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x17002779 RID: 10105
		// (get) Token: 0x06007F05 RID: 32517 RVA: 0x00206A68 File Offset: 0x00204C68
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetRecipientEnforcementProvisioningPolicy(this.DataObject.Identity.ToString());
			}
		}

		// Token: 0x1700277A RID: 10106
		// (get) Token: 0x06007F06 RID: 32518 RVA: 0x00206A7F File Offset: 0x00204C7F
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				if (!this.IgnoreDehydratedFlag)
				{
					return SharedTenantConfigurationMode.Static;
				}
				return SharedTenantConfigurationMode.NotShared;
			}
		}
	}
}
