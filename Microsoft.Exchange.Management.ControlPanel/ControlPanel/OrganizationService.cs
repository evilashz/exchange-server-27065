using System;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000255 RID: 597
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class OrganizationService : DataSourceService, IOrganizationService, IAsyncService
	{
		// Token: 0x060028AD RID: 10413 RVA: 0x00080108 File Offset: 0x0007E308
		[PrincipalPermission(SecurityAction.Demand, Role = "Enable-OrganizationCustomization@C:OrganizationConfig")]
		public PowerShellResults EnableOrganizationCustomization()
		{
			LocalSession localSession = LocalSession.Current;
			RbacSettings.AddSessionToCache(localSession.CacheKeys[0], localSession, false, false);
			return base.InvokeAsync(new PSCommand().AddCommand("Enable-OrganizationCustomization"), delegate(PowerShellResults results)
			{
				if (results != null && results.ErrorRecords.IsNullOrEmpty())
				{
					LocalSession.Current.FlushCache();
				}
			});
		}

		// Token: 0x0400207B RID: 8315
		internal const string EnableOrganizationCustomizationCmdlet = "Enable-OrganizationCustomization";

		// Token: 0x0400207C RID: 8316
		internal const string WriteScope = "@C:OrganizationConfig";

		// Token: 0x0400207D RID: 8317
		internal const string EnableOrganizationCustomizationRole = "Enable-OrganizationCustomization@C:OrganizationConfig";
	}
}
