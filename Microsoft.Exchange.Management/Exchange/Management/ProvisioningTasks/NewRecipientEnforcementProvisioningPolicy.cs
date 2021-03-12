using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Provisioning;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ProvisioningTasks
{
	// Token: 0x02000CE3 RID: 3299
	[Cmdlet("New", "RecipientEnforcementProvisioningPolicy", SupportsShouldProcess = true)]
	public sealed class NewRecipientEnforcementProvisioningPolicy : NewEnforcementProvisioningPolicyTaskBase<RecipientEnforcementProvisioningPolicy>
	{
		// Token: 0x17002771 RID: 10097
		// (get) Token: 0x06007EF6 RID: 32502 RVA: 0x00206934 File Offset: 0x00204B34
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewRecipientEnforcementProvisioningPolicy(this.DataObject.Name.ToString());
			}
		}

		// Token: 0x17002772 RID: 10098
		// (get) Token: 0x06007EF7 RID: 32503 RVA: 0x0020694B File Offset: 0x00204B4B
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Static;
			}
		}

		// Token: 0x17002773 RID: 10099
		// (get) Token: 0x06007EF8 RID: 32504 RVA: 0x0020694E File Offset: 0x00204B4E
		// (set) Token: 0x06007EF9 RID: 32505 RVA: 0x00206956 File Offset: 0x00204B56
		[Parameter(Mandatory = false)]
		public override SwitchParameter IgnoreDehydratedFlag { get; set; }
	}
}
