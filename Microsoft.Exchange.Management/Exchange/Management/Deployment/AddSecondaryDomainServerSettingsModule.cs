using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200016A RID: 362
	internal class AddSecondaryDomainServerSettingsModule : RunspaceServerSettingsInitModule
	{
		// Token: 0x06000D35 RID: 3381 RVA: 0x0003D077 File Offset: 0x0003B277
		public AddSecondaryDomainServerSettingsModule(TaskContext context) : base(context)
		{
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0003D080 File Offset: 0x0003B280
		protected override ADServerSettings GetCmdletADServerSettings()
		{
			PropertyBag fields = base.CurrentTaskContext.InvocationInfo.Fields;
			SwitchParameter switchParameter = fields.Contains("IsDatacenter") ? ((SwitchParameter)fields["IsDatacenter"]) : new SwitchParameter(false);
			bool flag = fields.Contains("DomainController");
			OrganizationIdParameter organizationIdParameter = (OrganizationIdParameter)fields["PrimaryOrganization"];
			PartitionId partitionId = (organizationIdParameter != null) ? ADAccountPartitionLocator.GetPartitionIdByAcceptedDomainName(organizationIdParameter.RawIdentity) : null;
			string value = null;
			ADServerSettings serverSettings = ExchangePropertyContainer.GetServerSettings(base.CurrentTaskContext.SessionState);
			if (serverSettings != null && partitionId != null)
			{
				value = serverSettings.PreferredGlobalCatalog(partitionId.ForestFQDN);
			}
			if (switchParameter && organizationIdParameter != null && string.IsNullOrEmpty(value) && partitionId != null && !flag)
			{
				if (this.domainBasedADServerSettings == null)
				{
					this.domainBasedADServerSettings = RunspaceServerSettings.CreateGcOnlyRunspaceServerSettings(organizationIdParameter.RawIdentity.ToLowerInvariant(), partitionId.ForestFQDN, false);
				}
				return this.domainBasedADServerSettings;
			}
			return base.GetCmdletADServerSettings();
		}

		// Token: 0x04000683 RID: 1667
		private ADServerSettings domainBasedADServerSettings;
	}
}
