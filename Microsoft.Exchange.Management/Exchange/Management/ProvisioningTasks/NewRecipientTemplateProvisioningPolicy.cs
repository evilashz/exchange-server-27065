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
	// Token: 0x02000CE4 RID: 3300
	[Cmdlet("New", "RecipientTemplateProvisioningPolicy", SupportsShouldProcess = true)]
	public sealed class NewRecipientTemplateProvisioningPolicy : NewTemplateProvisioningPolicyTaskBase<RecipientTemplateProvisioningPolicy>
	{
		// Token: 0x17002774 RID: 10100
		// (get) Token: 0x06007EFB RID: 32507 RVA: 0x00206967 File Offset: 0x00204B67
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewRecipientTemplateProvisioningPolicy(this.DataObject.Name.ToString());
			}
		}

		// Token: 0x06007EFC RID: 32508 RVA: 0x00206980 File Offset: 0x00204B80
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (this.DataObject.DefaultDistributionListOU != null)
			{
				RecipientTaskHelper.ResolveOrganizationalUnitInOrganization(new OrganizationalUnitIdParameter(this.DataObject.DefaultDistributionListOU), (IConfigurationSession)base.DataSession, (base.CurrentOrganizationId != null) ? base.CurrentOrganizationId : OrganizationId.ForestWideOrgId, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ExchangeOrganizationalUnit>), ExchangeErrorCategory.Client, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError));
			}
			TaskLogger.LogExit();
		}
	}
}
