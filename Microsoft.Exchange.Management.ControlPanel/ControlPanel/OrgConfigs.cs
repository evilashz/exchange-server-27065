using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200052F RID: 1327
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class OrgConfigs : DataSourceService, IOrgConfigs, IEditObjectService<OrgConfig, SetOrgConfig>, IGetObjectService<OrgConfig>
	{
		// Token: 0x06003F01 RID: 16129 RVA: 0x000BD900 File Offset: 0x000BBB00
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-OrganizationConfig@C:OrganizationConfig")]
		public PowerShellResults<OrgConfig> GetObject(Identity identity)
		{
			PSCommand psCommand = new PSCommand().AddCommand("Get-OrganizationConfig");
			return base.Invoke<OrgConfig>(psCommand);
		}

		// Token: 0x06003F02 RID: 16130 RVA: 0x000BD928 File Offset: 0x000BBB28
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-OrganizationConfig@C:OrganizationConfig+Set-OrganizationConfig@C:OrganizationConfig")]
		public PowerShellResults<OrgConfig> SetObject(Identity identity, SetOrgConfig properties)
		{
			PowerShellResults<OrgConfig> powerShellResults = new PowerShellResults<OrgConfig>();
			properties.IgnoreNullOrEmpty = false;
			if (properties.Any<KeyValuePair<string, object>>())
			{
				PSCommand psCommand = new PSCommand().AddCommand("Set-OrganizationConfig");
				psCommand.AddParameters(properties);
				PowerShellResults<OrgConfig> results = base.Invoke<OrgConfig>(psCommand);
				powerShellResults.MergeAll(results);
			}
			if (powerShellResults.Succeeded)
			{
				powerShellResults.MergeAll(this.GetObject(identity));
			}
			return powerShellResults;
		}

		// Token: 0x040028BC RID: 10428
		internal const string GetCmdlet = "Get-OrganizationConfig";

		// Token: 0x040028BD RID: 10429
		internal const string SetCmdlet = "Set-OrganizationConfig";

		// Token: 0x040028BE RID: 10430
		private const string GetObjectRole = "Get-OrganizationConfig@C:OrganizationConfig";

		// Token: 0x040028BF RID: 10431
		private const string SetObjectRole = "Get-OrganizationConfig@C:OrganizationConfig+Set-OrganizationConfig@C:OrganizationConfig";
	}
}
