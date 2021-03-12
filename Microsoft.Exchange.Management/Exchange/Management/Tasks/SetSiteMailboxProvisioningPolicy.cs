using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000458 RID: 1112
	[Cmdlet("Set", "SiteMailboxProvisioningPolicy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetSiteMailboxProvisioningPolicy : SetMailboxPolicyBase<TeamMailboxProvisioningPolicy>
	{
		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x0600275C RID: 10076 RVA: 0x0009B8B1 File Offset: 0x00099AB1
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetTeamMailboxProvisioningPolicy(this.Identity.ToString());
			}
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x0600275D RID: 10077 RVA: 0x0009B8C3 File Offset: 0x00099AC3
		// (set) Token: 0x0600275E RID: 10078 RVA: 0x0009B8CB File Offset: 0x00099ACB
		[Parameter(Mandatory = false)]
		public SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x0600275F RID: 10079 RVA: 0x0009B8D4 File Offset: 0x00099AD4
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

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06002760 RID: 10080 RVA: 0x0009B8E6 File Offset: 0x00099AE6
		// (set) Token: 0x06002761 RID: 10081 RVA: 0x0009B90C File Offset: 0x00099B0C
		[Parameter(Mandatory = false)]
		public SwitchParameter IsDefault
		{
			get
			{
				return (SwitchParameter)(base.Fields["IsDefault"] ?? false);
			}
			set
			{
				base.Fields["IsDefault"] = value;
			}
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x0009B924 File Offset: 0x00099B24
		protected override void InternalValidate()
		{
			((IConfigurationSession)base.DataSession).SessionSettings.IsSharedConfigChecked = true;
			if (!this.IgnoreDehydratedFlag)
			{
				SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrgState, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (this.IsDefault)
			{
				this.DataObject.IsDefault = true;
				QueryFilter additionalFilter = new ComparisonFilter(ComparisonOperator.NotEqual, ADObjectSchema.Guid, this.DataObject.Id.ObjectGuid);
				this.otherDefaultPolicies = DefaultTeamMailboxProvisioningPolicyUtility.GetDefaultPolicies((IConfigurationSession)base.DataSession, additionalFilter);
				if (this.otherDefaultPolicies.Count > 0)
				{
					this.updateOtherDefaultPolicies = true;
				}
			}
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x0009B9E0 File Offset: 0x00099BE0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (SharedConfiguration.IsSharedConfiguration(this.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(this.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			if (this.updateOtherDefaultPolicies)
			{
				try
				{
					DefaultMailboxPolicyUtility<TeamMailboxProvisioningPolicy>.ClearDefaultPolicies(base.DataSession as IConfigurationSession, this.otherDefaultPolicies);
				}
				catch (DataSourceTransientException exception)
				{
					base.WriteError(exception, ErrorCategory.ReadError, null);
				}
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}
	}
}
