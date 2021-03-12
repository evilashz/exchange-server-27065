using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Provisioning;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x0200002E RID: 46
	internal class MailboxPlansProvisioningHandler : ProvisioningHandlerBase
	{
		// Token: 0x06000140 RID: 320 RVA: 0x000078D4 File Offset: 0x00005AD4
		public override IConfigurable ProvisionDefaultProperties(IConfigurable readOnlyIConfigurable)
		{
			if (base.TaskName != "New-Mailbox" && base.TaskName != "New-SyncMailbox" && base.TaskName != "Enable-Mailbox" && base.TaskName != "Undo-SoftDeletedMailbox" && base.TaskName != "Undo-SyncSoftDeletedMailbox")
			{
				return null;
			}
			string[] array = new string[]
			{
				"Arbitration",
				"Discovery",
				"AuditLog"
			};
			foreach (string key in array)
			{
				object obj = base.UserSpecifiedParameters[key];
				if (obj != null && (SwitchParameter)obj == true)
				{
					return null;
				}
			}
			Mailbox mailbox = readOnlyIConfigurable as Mailbox;
			ADUser aduser = null;
			if (base.UserSpecifiedParameters["MailboxPlan"] == null && (mailbox == null || mailbox.MailboxPlan == null))
			{
				aduser = this.FindMailboxPlan((mailbox == null) ? null : ((ADUser)mailbox.DataObject), null);
			}
			if (aduser == null)
			{
				return null;
			}
			ADUser aduser2 = new ADUser();
			if (aduser != null)
			{
				aduser2.MailboxPlan = aduser.Id;
				aduser2.MailboxPlanObject = aduser;
			}
			if (base.TaskName == "New-SyncMailbox")
			{
				return new SyncMailbox(aduser2);
			}
			return new Mailbox(aduser2);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00007A34 File Offset: 0x00005C34
		public override bool UpdateAffectedIConfigurable(IConfigurable writeableIConfigurable)
		{
			if (base.TaskName != "New-MoveRequest" && (base.TaskName != "Update-MovedMailbox" || base.UserSpecifiedParameters["MorphToMailUser"] != null || base.UserSpecifiedParameters["Credential"] != null))
			{
				return false;
			}
			ADPresentationObject adpresentationObject = writeableIConfigurable as ADPresentationObject;
			ADUser aduser;
			if (adpresentationObject != null)
			{
				aduser = (adpresentationObject.DataObject as ADUser);
			}
			else
			{
				aduser = (writeableIConfigurable as ADUser);
			}
			if (base.UserSpecifiedParameters["MailboxPlan"] == null && aduser != null && aduser.MailboxPlan == null)
			{
				if (aduser.RecipientTypeDetails != RecipientTypeDetails.UserMailbox)
				{
					return false;
				}
				ADUser aduser2 = this.FindMailboxPlan(aduser, aduser.OrganizationId);
				if (aduser2 != null && aduser.MailboxPlan != aduser2.Id)
				{
					aduser.MailboxPlan = aduser2.Id;
					aduser.SKUCapability = aduser2.SKUCapability;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00007B2C File Offset: 0x00005D2C
		private ADUser FindMailboxPlan(ADUser user, OrganizationId userOrgId)
		{
			OrganizationId organizationId = userOrgId ?? base.UserScope.CurrentOrganizationId;
			if (organizationId == OrganizationId.ForestWideOrgId)
			{
				base.LogMessage(Strings.WarningNoDefaultMailboxPlan);
				return null;
			}
			IRecipientSession session = this.GetRecipientSession(organizationId);
			Capability? skucapability = this.GetSKUCapability(user);
			ADUser result;
			if (skucapability != null)
			{
				result = this.FindMailboxPlanWithSKUCapability(skucapability.Value, session, organizationId);
			}
			else
			{
				result = base.ProvisioningCache.TryAddAndGetOrganizationData<ADUser>(CannedProvisioningCacheKeys.DefaultMailboxPlan, organizationId, () => this.FindDefaultMailboxPlan(session));
			}
			return result;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00007BE0 File Offset: 0x00005DE0
		private Capability? GetSKUCapability(ADUser user)
		{
			Capability? result = null;
			if (base.UserSpecifiedParameters["SKUCapability"] != null)
			{
				result = new Capability?((Capability)base.UserSpecifiedParameters["SKUCapability"]);
			}
			else if (user != null)
			{
				result = user.SKUCapability;
			}
			return result;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00007C30 File Offset: 0x00005E30
		private ADUser FindMailboxPlanWithSKUCapability(Capability skuCapability, IRecipientSession recipientSession, OrganizationId organizationId)
		{
			bool checkCurrentReleasePlanFirst = RecipientTaskHelper.IsOrganizationInPilot(DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.PartiallyConsistent, recipientSession.SessionSettings, 266, "FindMailboxPlanWithSKUCapability", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ProvisioningAgent\\MailboxPlansProvisioningHandler.cs"), organizationId);
			LocalizedString message;
			ADUser aduser = MailboxTaskHelper.FindMailboxPlanWithSKUCapability(skuCapability, recipientSession, out message, checkCurrentReleasePlanFirst);
			if (aduser == null)
			{
				throw new ProvisioningException(message);
			}
			return aduser;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00007C7C File Offset: 0x00005E7C
		private ADUser FindDefaultMailboxPlan(IRecipientSession session)
		{
			ADUser[] array = session.FindADUser(null, QueryScope.SubTree, MailboxTaskHelper.defaultMailboxPlanFilter, null, 2);
			if (array.Length == 1)
			{
				return array[0];
			}
			if (array.Length > 1)
			{
				throw new ProvisioningException(Strings.ErrorTooManyDefaultMailboxPlans);
			}
			array = session.FindADUser(null, QueryScope.SubTree, MailboxTaskHelper.mailboxPlanFilter, null, 2);
			if (array.Length == 0)
			{
				throw new ProvisioningException(Strings.ErrorNoMailboxPlan);
			}
			if (array.Length > 1)
			{
				throw new ProvisioningException(Strings.ErrorNoDefaultMailboxPlan);
			}
			base.LogMessage(Strings.WarningNoDefaultMailboxPlanUsingNonDefault);
			return array[0];
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00007D00 File Offset: 0x00005F00
		private IRecipientSession GetRecipientSession(OrganizationId userOrgId)
		{
			string domainController = (base.UserSpecifiedParameters["DomainController"] != null) ? base.UserSpecifiedParameters["DomainController"].ToString() : null;
			OrganizationId currentOrganizationId = userOrgId ?? base.UserScope.CurrentOrganizationId;
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), currentOrganizationId, null, false);
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(domainController, true, ConsistencyMode.FullyConsistent, sessionSettings, 329, "GetRecipientSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ProvisioningAgent\\MailboxPlansProvisioningHandler.cs");
		}

		// Token: 0x040000A2 RID: 162
		private const string MailboxParameterSetArbitration = "Arbitration";

		// Token: 0x040000A3 RID: 163
		private const string MailboxParameterSetDiscovery = "Discovery";

		// Token: 0x040000A4 RID: 164
		private const string MailboxParameterSetAuditLog = "AuditLog";
	}
}
