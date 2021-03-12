using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.EventMessages;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Provisioning;
using Microsoft.Exchange.Provisioning.LoadBalancing;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x0200002D RID: 45
	internal class MailboxProvisioningHandler : ProvisioningHandlerBase
	{
		// Token: 0x0600013D RID: 317 RVA: 0x0000752C File Offset: 0x0000572C
		public override IConfigurable ProvisionDefaultProperties(IConfigurable readOnlyIConfigurable)
		{
			if (base.TaskName == "Enable-MailUser")
			{
				return null;
			}
			if (base.UserSpecifiedParameters["Database"] != null)
			{
				return null;
			}
			Mailbox mailbox = readOnlyIConfigurable as Mailbox;
			if (mailbox != null && mailbox.Database != null)
			{
				return null;
			}
			string text = null;
			if (base.UserSpecifiedParameters["DomainController"] != null)
			{
				text = (Fqdn)base.UserSpecifiedParameters["DomainController"];
			}
			bool isInitialProvisioning = base.UserSpecifiedParameters["isInitialProvisioning"] != null && ((SwitchParameter)base.UserSpecifiedParameters["isInitialProvisioning"]).ToBool();
			bool flag = base.UserSpecifiedParameters["TargetAllMDBs"] == null || ((SwitchParameter)base.UserSpecifiedParameters["TargetAllMDBs"]).ToBool();
			IMailboxProvisioningConstraint mailboxProvisioningConstraint = (base.UserSpecifiedParameters["MailboxProvisioningConstraint"] == null) ? mailbox.MailboxProvisioningConstraint : ((IMailboxProvisioningConstraint)base.UserSpecifiedParameters["MailboxProvisioningConstraint"]);
			LoadBalancingReport loadBalancingReport = new LoadBalancingReport();
			MailboxDatabaseWithLocationInfo mailboxDatabaseWithLocationInfo;
			if (base.TaskName == "New-SiteMailbox" || ((base.TaskName == "New-Mailbox" || base.TaskName == "Enable-Mailbox") && base.UserSpecifiedParameters["PublicFolder"] != null))
			{
				if ((base.TaskName == "New-TeamMailbox" || base.TaskName == "New-SiteMailbox") && !VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled)
				{
					mailboxDatabaseWithLocationInfo = PhysicalResourceLoadBalancing.FindDatabaseAndLocationForEnterpriseSiteMailbox(text, base.LogMessage, base.UserScope.CurrentScopeSet);
				}
				else
				{
					mailboxDatabaseWithLocationInfo = PhysicalResourceLoadBalancing.FindDatabaseAndLocation(text, base.LogMessage, base.UserScope.CurrentScopeSet, isInitialProvisioning, !flag, new int?(Server.E15MinVersion), mailboxProvisioningConstraint, null, ref loadBalancingReport);
				}
			}
			else
			{
				mailboxDatabaseWithLocationInfo = PhysicalResourceLoadBalancing.FindDatabaseAndLocation(text, base.LogMessage, base.UserScope.CurrentScopeSet, isInitialProvisioning, !flag, new int?(Server.E15MinVersion), mailboxProvisioningConstraint, null, ref loadBalancingReport);
			}
			if (mailboxDatabaseWithLocationInfo == null)
			{
				ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_LoadBalancingFailedToFindDatabase, new string[]
				{
					text,
					loadBalancingReport.ToString()
				});
				base.WriteError(new RecipientTaskException(Strings.ErrorLoadBalancingFailedToFindDatabase), ExchangeErrorCategory.ServerOperation);
				return null;
			}
			ADUser aduser = new ADUser();
			aduser.Database = mailboxDatabaseWithLocationInfo.MailboxDatabase.Id;
			aduser.DatabaseAndLocation = mailboxDatabaseWithLocationInfo;
			if (base.TaskName == "New-SyncMailbox")
			{
				return new SyncMailbox(aduser);
			}
			return new Mailbox(aduser);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000077DC File Offset: 0x000059DC
		public override bool UpdateAffectedIConfigurable(IConfigurable writeableIConfigurable)
		{
			if (base.TaskName == "Enable-MailUser" && base.UserSpecifiedParameters["ArchiveGuid"] != null)
			{
				MailUser mailUser = writeableIConfigurable as MailUser;
				if (mailUser != null && mailUser.ArchiveDatabase == null)
				{
					string text = null;
					if (base.UserSpecifiedParameters["DomainController"] != null)
					{
						text = (Fqdn)base.UserSpecifiedParameters["DomainController"];
					}
					LoadBalancingReport loadBalancingReport = new LoadBalancingReport();
					MailboxDatabaseWithLocationInfo mailboxDatabaseWithLocationInfo = PhysicalResourceLoadBalancing.FindDatabaseAndLocation(text, base.LogMessage, null, true, false, null, ref loadBalancingReport);
					if (mailboxDatabaseWithLocationInfo == null)
					{
						ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_LoadBalancingFailedToFindDatabase, new string[]
						{
							text,
							loadBalancingReport.ToString()
						});
						base.WriteError(new RecipientTaskException(Strings.ErrorLoadBalancingFailedToFindDatabase), ExchangeErrorCategory.ServerOperation);
						return false;
					}
					mailUser.ArchiveDatabase = mailboxDatabaseWithLocationInfo.MailboxDatabase.Id;
					return true;
				}
			}
			return false;
		}
	}
}
