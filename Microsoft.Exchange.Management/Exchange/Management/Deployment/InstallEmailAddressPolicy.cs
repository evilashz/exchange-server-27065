using System;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001EA RID: 490
	[Cmdlet("Install", "EmailAddressPolicy")]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class InstallEmailAddressPolicy : NewMultitenancyFixedNameSystemConfigurationObjectTask<EmailAddressPolicyContainer>
	{
		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x060010A9 RID: 4265 RVA: 0x000499FF File Offset: 0x00047BFF
		// (set) Token: 0x060010AA RID: 4266 RVA: 0x00049A16 File Offset: 0x00047C16
		[Parameter(Mandatory = false)]
		public SmtpDomain DomainName
		{
			get
			{
				return (SmtpDomain)base.Fields["DomainName"];
			}
			set
			{
				base.Fields["DomainName"] = value;
			}
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x00049A2C File Offset: 0x00047C2C
		protected override IConfigurable PrepareDataObject()
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, "Recipient Policies");
			IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
			ADObjectId currentOrgContainerId = base.CurrentOrgContainerId;
			EmailAddressPolicyContainer[] array = configurationSession.Find<EmailAddressPolicyContainer>(currentOrgContainerId, QueryScope.SubTree, filter, null, 0);
			EmailAddressPolicyContainer emailAddressPolicyContainer;
			if (this.isContainerExisted = (array != null && array.Length > 0))
			{
				emailAddressPolicyContainer = array[0];
			}
			else
			{
				emailAddressPolicyContainer = (EmailAddressPolicyContainer)base.PrepareDataObject();
				emailAddressPolicyContainer.SetId(currentOrgContainerId.GetChildId("Recipient Policies"));
			}
			return emailAddressPolicyContainer;
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x00049AAC File Offset: 0x00047CAC
		protected override void InternalProcessRecord()
		{
			if (!this.isContainerExisted)
			{
				base.InternalProcessRecord();
			}
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, EmailAddressPolicy.DefaultName);
			IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
			ADObjectId currentOrgContainerId = base.CurrentOrgContainerId;
			EmailAddressPolicy[] array = configurationSession.Find<EmailAddressPolicy>(currentOrgContainerId, QueryScope.SubTree, filter, null, 0);
			if (array == null || array.Length == 0)
			{
				EmailAddressPolicy emailAddressPolicy = new EmailAddressPolicy();
				emailAddressPolicy.SetId(this.DataObject.Id.GetChildId(EmailAddressPolicy.DefaultName));
				emailAddressPolicy[EmailAddressPolicySchema.Enabled] = true;
				emailAddressPolicy.Priority = EmailAddressPolicyPriority.Lowest;
				if (Datacenter.GetExchangeSku() == Datacenter.ExchangeSku.Enterprise)
				{
					emailAddressPolicy.RecipientFilterApplied = true;
				}
				emailAddressPolicy.IncludedRecipients = new WellKnownRecipientType?(WellKnownRecipientType.AllRecipients);
				if (this.DomainName == null)
				{
					emailAddressPolicy.EnabledPrimarySMTPAddressTemplate = "@" + DNConvertor.FqdnFromDomainDistinguishedName(currentOrgContainerId.DomainId.DistinguishedName);
				}
				else
				{
					emailAddressPolicy.EnabledPrimarySMTPAddressTemplate = "@" + this.DomainName.ToString();
				}
				RecipientFilterHelper.StampE2003FilterMetadata(emailAddressPolicy, emailAddressPolicy.LdapRecipientFilter, EmailAddressPolicySchema.PurportedSearchUI);
				if (base.CurrentOrganizationId != null)
				{
					emailAddressPolicy.OrganizationId = base.CurrentOrganizationId;
				}
				else
				{
					emailAddressPolicy.OrganizationId = base.ExecutingUserOrganizationId;
				}
				configurationSession.Save(emailAddressPolicy);
			}
		}

		// Token: 0x04000780 RID: 1920
		private bool isContainerExisted;
	}
}
