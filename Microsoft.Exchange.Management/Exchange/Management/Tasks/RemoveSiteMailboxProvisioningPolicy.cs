using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000456 RID: 1110
	[Cmdlet("Remove", "SiteMailboxProvisioningPolicy", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveSiteMailboxProvisioningPolicy : RemoveMailboxPolicyBase<TeamMailboxProvisioningPolicy>
	{
		// Token: 0x06002752 RID: 10066 RVA: 0x0009B7B8 File Offset: 0x000999B8
		protected override bool HandleRemoveWithAssociatedUsers()
		{
			return true;
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06002753 RID: 10067 RVA: 0x0009B7BB File Offset: 0x000999BB
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveTeamMailboxProvisioningPolicy(this.Identity.ToString());
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06002754 RID: 10068 RVA: 0x0009B7CD File Offset: 0x000999CD
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Static;
			}
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x0009B7D0 File Offset: 0x000999D0
		protected override void InternalValidate()
		{
			SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrgState, new Task.ErrorLoggerDelegate(base.WriteError));
			((IConfigurationSession)base.DataSession).SessionSettings.IsSharedConfigChecked = true;
			base.InternalValidate();
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x0009B808 File Offset: 0x00099A08
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (SharedConfiguration.IsSharedConfiguration(base.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(base.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			if (base.DataObject.IsDefault)
			{
				base.WriteError(new CannotRemoveDefaultSiteMailboxProvisioningPolicyException(), ErrorCategory.InvalidOperation, this.Identity);
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}
	}
}
