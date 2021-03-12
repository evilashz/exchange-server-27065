using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02000915 RID: 2325
	internal class IOCConfigurationTask : SessionTask
	{
		// Token: 0x0600528C RID: 21132 RVA: 0x00153D00 File Offset: 0x00151F00
		public IOCConfigurationTask() : base(HybridStrings.IOCConfigurationTaskName, 2)
		{
		}

		// Token: 0x0600528D RID: 21133 RVA: 0x00153D13 File Offset: 0x00151F13
		public override bool CheckPrereqs(ITaskContext taskContext)
		{
			if (!base.CheckPrereqs(taskContext))
			{
				base.Logger.LogInformation(HybridStrings.HybridInfoTaskLogTemplate(base.Name, HybridStrings.HybridInfoBasePrereqsFailed));
				return false;
			}
			return true;
		}

		// Token: 0x0600528E RID: 21134 RVA: 0x00153D48 File Offset: 0x00151F48
		public override bool NeedsConfiguration(ITaskContext taskContext)
		{
			return !Configuration.RestrictIOCToSP1OrGreater(taskContext.HybridConfigurationObject.ServiceInstance) || (taskContext.OnPremisesSession.GetIntraOrganizationConfiguration().DeploymentIsCompleteIOCReady ?? false);
		}

		// Token: 0x0600528F RID: 21135 RVA: 0x00153D94 File Offset: 0x00151F94
		public override bool Configure(ITaskContext taskContext)
		{
			if (!base.Configure(taskContext))
			{
				return false;
			}
			string intraOrganizationConnectorName = this.GetIntraOrganizationConnectorName();
			IntraOrganizationConnector intraOrganizationConnector = taskContext.OnPremisesSession.GetIntraOrganizationConnector(intraOrganizationConnectorName);
			if (intraOrganizationConnector != null)
			{
				taskContext.OnPremisesSession.RemoveIntraOrganizationConnector(intraOrganizationConnectorName);
			}
			IntraOrganizationConnector intraOrganizationConnector2 = taskContext.TenantSession.GetIntraOrganizationConnector(intraOrganizationConnectorName);
			if (intraOrganizationConnector2 != null)
			{
				taskContext.TenantSession.RemoveIntraOrganizationConnector(intraOrganizationConnectorName);
			}
			IntraOrganizationConfiguration intraOrganizationConfiguration = taskContext.OnPremisesSession.GetIntraOrganizationConfiguration();
			IntraOrganizationConfiguration intraOrganizationConfiguration2 = taskContext.TenantSession.GetIntraOrganizationConfiguration();
			taskContext.OnPremisesSession.NewIntraOrganizationConnector(this.GetIntraOrganizationConnectorName(), intraOrganizationConfiguration2.OnlineDiscoveryEndpoint.ToString(), intraOrganizationConfiguration2.OnlineTargetAddress, true);
			taskContext.TenantSession.NewIntraOrganizationConnector(this.GetIntraOrganizationConnectorName(), intraOrganizationConfiguration.OnPremiseDiscoveryEndpoint.ToString(), intraOrganizationConfiguration2.OnPremiseTargetAddresses, true);
			if (!taskContext.Parameters.Get<bool>("_suppressOAuthWarning"))
			{
				base.AddLocalizedStringWarning(HybridStrings.WarningOAuthNeedsConfiguration(Configuration.OAuthConfigurationUrl(taskContext.HybridConfigurationObject.ServiceInstance)));
			}
			return true;
		}

		// Token: 0x06005290 RID: 21136 RVA: 0x00153E7C File Offset: 0x0015207C
		public override bool ValidateConfiguration(ITaskContext taskContext)
		{
			if (!base.ValidateConfiguration(taskContext))
			{
				return false;
			}
			string intraOrganizationConnectorName = this.GetIntraOrganizationConnectorName();
			IntraOrganizationConnector intraOrganizationConnector = taskContext.OnPremisesSession.GetIntraOrganizationConnector(intraOrganizationConnectorName);
			IntraOrganizationConnector intraOrganizationConnector2 = taskContext.TenantSession.GetIntraOrganizationConnector(intraOrganizationConnectorName);
			return intraOrganizationConnector != null && intraOrganizationConnector2 != null && string.Equals(intraOrganizationConnector.Name, intraOrganizationConnectorName, StringComparison.InvariantCultureIgnoreCase) && string.Equals(intraOrganizationConnector2.Name, intraOrganizationConnectorName, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x06005291 RID: 21137 RVA: 0x00153EDE File Offset: 0x001520DE
		private IOrganizationConfig GetOnPremOrgConfig()
		{
			if (this.onPremOrgConfig == null)
			{
				this.onPremOrgConfig = base.OnPremisesSession.GetOrganizationConfig();
			}
			return this.onPremOrgConfig;
		}

		// Token: 0x06005292 RID: 21138 RVA: 0x00153F00 File Offset: 0x00152100
		private string GetIntraOrganizationConnectorName()
		{
			return string.Format("HybridIOC - {0}", this.GetOnPremOrgConfig().Guid.ToString());
		}

		// Token: 0x04002FF8 RID: 12280
		private const string IOCNameTemplate = "HybridIOC - {0}";

		// Token: 0x04002FF9 RID: 12281
		private IOrganizationConfig onPremOrgConfig;
	}
}
