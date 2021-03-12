using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Provisioning;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ProvisioningTasks
{
	// Token: 0x02000CE8 RID: 3304
	[Cmdlet("Set", "RecipientTemplateProvisioningPolicy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetRecipientTemplateProvisioningPolicy : SetProvisioningPolicyTaskBase<ProvisioningPolicyIdParameter, RecipientTemplateProvisioningPolicy>
	{
		// Token: 0x1700277B RID: 10107
		// (get) Token: 0x06007F08 RID: 32520 RVA: 0x00206A99 File Offset: 0x00204C99
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetRecipientTemplateProvisioningPolicy(this.DataObject.Identity.ToString());
			}
		}

		// Token: 0x1700277C RID: 10108
		// (get) Token: 0x06007F09 RID: 32521 RVA: 0x00206AB0 File Offset: 0x00204CB0
		// (set) Token: 0x06007F0A RID: 32522 RVA: 0x00206AC7 File Offset: 0x00204CC7
		[Parameter(Mandatory = false)]
		public OrganizationalUnitIdParameter DefaultDistributionListOU
		{
			get
			{
				return (OrganizationalUnitIdParameter)base.Fields["DefaultDistributionListOU"];
			}
			set
			{
				base.Fields["DefaultDistributionListOU"] = value;
			}
		}

		// Token: 0x06007F0B RID: 32523 RVA: 0x00206ADC File Offset: 0x00204CDC
		protected override void ResolveLocalSecondaryIdentities()
		{
			base.ResolveLocalSecondaryIdentities();
			if (this.DefaultDistributionListOU != null)
			{
				this.defaultOU = RecipientTaskHelper.ResolveOrganizationalUnitInOrganization(this.DefaultDistributionListOU, this.ConfigurationSession, null, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ExchangeOrganizationalUnit>), ExchangeErrorCategory.Client, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.ThrowTerminatingError));
			}
		}

		// Token: 0x06007F0C RID: 32524 RVA: 0x00206B38 File Offset: 0x00204D38
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			ExchangeOrganizationalUnit exchangeOrganizationalUnit = null;
			if (base.Fields.IsModified("DefaultDistributionListOU"))
			{
				this.DataObject.DefaultDistributionListOU = ((this.defaultOU == null) ? null : this.defaultOU.Id);
				exchangeOrganizationalUnit = this.defaultOU;
			}
			else if (this.DataObject.IsChanged(RecipientTemplateProvisioningPolicySchema.DefaultDistributionListOU) && this.DataObject.DefaultDistributionListOU != null)
			{
				exchangeOrganizationalUnit = RecipientTaskHelper.ResolveOrganizationalUnitInOrganization(new OrganizationalUnitIdParameter(this.DataObject.DefaultDistributionListOU), this.ConfigurationSession, null, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ExchangeOrganizationalUnit>), ExchangeErrorCategory.Client, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError));
			}
			if (exchangeOrganizationalUnit != null)
			{
				OrganizationId organizationId = OrganizationId.ForestWideOrgId;
				if (this.ConfigurationSession is ITenantConfigurationSession)
				{
					organizationId = TaskHelper.ResolveOrganizationId(this.DataObject.Id, ADProvisioningPolicy.RdnContainer, (ITenantConfigurationSession)this.ConfigurationSession);
				}
				RecipientTaskHelper.IsOrgnizationalUnitInOrganization(this.ConfigurationSession, organizationId, exchangeOrganizationalUnit, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04003E47 RID: 15943
		private ExchangeOrganizationalUnit defaultOU;
	}
}
