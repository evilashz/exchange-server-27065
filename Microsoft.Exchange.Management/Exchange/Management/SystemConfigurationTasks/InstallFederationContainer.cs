using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020002D5 RID: 725
	[Cmdlet("Install", "FederationContainer")]
	public sealed class InstallFederationContainer : NewMultitenancyFixedNameSystemConfigurationObjectTask<FederatedOrganizationId>
	{
		// Token: 0x0600194B RID: 6475 RVA: 0x0007117C File Offset: 0x0006F37C
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			FederatedOrganizationId federatedOrganizationId = null;
			IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
			ADObjectId orgContainerId = configurationSession.GetOrgContainerId();
			configurationSession.SessionSettings.IsSharedConfigChecked = true;
			FederatedOrganizationId[] array = configurationSession.Find<FederatedOrganizationId>(orgContainerId, QueryScope.SubTree, ADObject.ObjectClassFilter("msExchFedOrgId"), null, 1);
			if (array != null && array.Length == 1)
			{
				federatedOrganizationId = array[0];
			}
			if (federatedOrganizationId != null)
			{
				if (!federatedOrganizationId.Name.Equals("Federation", StringComparison.OrdinalIgnoreCase))
				{
					this.containerHasBeenRenamed = true;
				}
			}
			else
			{
				federatedOrganizationId = (FederatedOrganizationId)base.PrepareDataObject();
			}
			federatedOrganizationId.Name = "Federation";
			federatedOrganizationId.SetId((IConfigurationSession)base.DataSession, federatedOrganizationId.Name);
			TaskLogger.LogExit();
			return federatedOrganizationId;
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x00071224 File Offset: 0x0006F424
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.containerHasBeenRenamed || base.DataSession.Read<FederatedOrganizationId>(this.DataObject.Id) == null)
			{
				base.InternalProcessRecord();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04000B05 RID: 2821
		private bool containerHasBeenRenamed;
	}
}
