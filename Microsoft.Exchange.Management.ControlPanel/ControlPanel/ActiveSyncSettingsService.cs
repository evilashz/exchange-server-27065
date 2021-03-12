using System;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000301 RID: 769
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class ActiveSyncSettingsService : DataSourceService, IActiveSyncSettingsService, IEditObjectService<ActiveSyncSettings, SetActiveSyncSettings>, IGetObjectService<ActiveSyncSettings>
	{
		// Token: 0x06002E2C RID: 11820 RVA: 0x0008C63E File Offset: 0x0008A83E
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-ActiveSyncOrganizationSettings?Identity@C:OrganizationConfig")]
		public PowerShellResults<ActiveSyncSettings> GetObject(Identity identity)
		{
			return base.GetObject<ActiveSyncSettings>("Get-ActiveSyncOrganizationSettings");
		}

		// Token: 0x06002E2D RID: 11821 RVA: 0x0008C64C File Offset: 0x0008A84C
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-ActiveSyncOrganizationSettings?Identity@C:OrganizationConfig+Set-ActiveSyncOrganizationSettings?Identity@C:OrganizationConfig")]
		public PowerShellResults<ActiveSyncSettings> SetObject(Identity identity, SetActiveSyncSettings properties)
		{
			properties.FaultIfNull();
			properties.IgnoreNullOrEmpty = false;
			PowerShellResults<ActiveSyncSettings> powerShellResults = base.Invoke<ActiveSyncSettings>(new PSCommand().AddCommand("Set-ActiveSyncOrganizationSettings").AddParameters(properties));
			if (powerShellResults.Succeeded)
			{
				PowerShellResults<ActiveSyncSettings> @object = this.GetObject(null);
				if (@object != null)
				{
					powerShellResults.MergeAll(@object);
				}
			}
			return powerShellResults;
		}

		// Token: 0x0400227B RID: 8827
		private const string Noun = "ActiveSyncOrganizationSettings";

		// Token: 0x0400227C RID: 8828
		internal const string GetCmdlet = "Get-ActiveSyncOrganizationSettings";

		// Token: 0x0400227D RID: 8829
		internal const string SetCmdlet = "Set-ActiveSyncOrganizationSettings";

		// Token: 0x0400227E RID: 8830
		internal const string ReadScope = "@C:OrganizationConfig";

		// Token: 0x0400227F RID: 8831
		internal const string WriteScope = "@C:OrganizationConfig";

		// Token: 0x04002280 RID: 8832
		private const string GetObjectRole = "Get-ActiveSyncOrganizationSettings?Identity@C:OrganizationConfig";

		// Token: 0x04002281 RID: 8833
		private const string SetObjectRole = "Get-ActiveSyncOrganizationSettings?Identity@C:OrganizationConfig+Set-ActiveSyncOrganizationSettings?Identity@C:OrganizationConfig";
	}
}
