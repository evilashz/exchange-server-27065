using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Provisioning;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ProvisioningTasks
{
	// Token: 0x02000CE1 RID: 3297
	[Cmdlet("Get", "RecipientEnforcementProvisioningPolicy", DefaultParameterSetName = "Identity")]
	public sealed class GetRecipientEnforcementProvisioningPolicy : GetProvisioningPolicyBase<RecipientEnforcementProvisioningPolicyIdParameter, RecipientEnforcementProvisioningPolicy>
	{
		// Token: 0x1700276E RID: 10094
		// (get) Token: 0x06007EEE RID: 32494 RVA: 0x0020678A File Offset: 0x0020498A
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

		// Token: 0x1700276F RID: 10095
		// (get) Token: 0x06007EEF RID: 32495 RVA: 0x0020679C File Offset: 0x0020499C
		// (set) Token: 0x06007EF0 RID: 32496 RVA: 0x002067A4 File Offset: 0x002049A4
		[Parameter(Mandatory = false)]
		public SwitchParameter Status { get; set; }

		// Token: 0x17002770 RID: 10096
		// (get) Token: 0x06007EF1 RID: 32497 RVA: 0x002067AD File Offset: 0x002049AD
		// (set) Token: 0x06007EF2 RID: 32498 RVA: 0x002067B5 File Offset: 0x002049B5
		[Parameter(Mandatory = false)]
		public SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x06007EF3 RID: 32499 RVA: 0x002067C0 File Offset: 0x002049C0
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			if (this.Status)
			{
				RecipientEnforcementProvisioningPolicy recipientEnforcementProvisioningPolicy = dataObject as RecipientEnforcementProvisioningPolicy;
				if (recipientEnforcementProvisioningPolicy != null)
				{
					ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, false);
					IConfigurationSession configSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(true, ConsistencyMode.PartiallyConsistent, sessionSettings, 74, "WriteResult", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\ProvisioningTasks\\Recipient\\GetRecipientEnforcementProvisioningPolicy.cs");
					recipientEnforcementProvisioningPolicy.MailboxCount = new int?(SystemAddressListMemberCount.GetCount(configSession, base.CurrentOrganizationId, "All Mailboxes(VLV)", false));
					recipientEnforcementProvisioningPolicy.MailUserCount = new int?(SystemAddressListMemberCount.GetCount(configSession, base.CurrentOrganizationId, "All Mail Users(VLV)", false));
					recipientEnforcementProvisioningPolicy.ContactCount = new int?(SystemAddressListMemberCount.GetCount(configSession, base.CurrentOrganizationId, "All Contacts(VLV)", false));
					recipientEnforcementProvisioningPolicy.DistributionListCount = new int?(SystemAddressListMemberCount.GetCount(configSession, base.CurrentOrganizationId, "All Groups(VLV)", false));
					try
					{
						recipientEnforcementProvisioningPolicy.TeamMailboxCount = new int?(SystemAddressListMemberCount.GetCount(configSession, base.CurrentOrganizationId, "TeamMailboxes(VLV)", false));
						recipientEnforcementProvisioningPolicy.PublicFolderMailboxCount = new int?(SystemAddressListMemberCount.GetCount(configSession, base.CurrentOrganizationId, "PublicFolderMailboxes(VLV)", false));
						recipientEnforcementProvisioningPolicy.MailPublicFolderCount = new int?(SystemAddressListMemberCount.GetCount(configSession, base.CurrentOrganizationId, "MailPublicFolders(VLV)", false));
					}
					catch (ADNoSuchObjectException)
					{
					}
				}
			}
			base.WriteResult(dataObject);
			TaskLogger.LogExit();
		}
	}
}
