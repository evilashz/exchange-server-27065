using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Online.BOX.UI.Shell;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000245 RID: 581
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class NavBar : DataSourceService, INavBarService
	{
		// Token: 0x06002852 RID: 10322 RVA: 0x0007E3E4 File Offset: 0x0007C5E4
		[PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
		[PrincipalPermission(SecurityAction.Demand, Role = "MyBaseOptions")]
		public PowerShellResults<NavBarPack> GetObject(Identity identity)
		{
			identity.FaultIfNull();
			bool showAdminFeature = identity.RawIdentity == "myorg";
			NavBarClientBase navBarClientBase = NavBarClientBase.Create(showAdminFeature, false, true);
			PowerShellResults<NavBarPack> powerShellResults = new PowerShellResults<NavBarPack>();
			if (navBarClientBase != null)
			{
				navBarClientBase.PrepareNavBarPack();
				NavBarPack navBarPack = navBarClientBase.GetNavBarPack();
				if (navBarPack != null)
				{
					powerShellResults.Output = new NavBarPack[]
					{
						navBarPack
					};
				}
			}
			return powerShellResults;
		}
	}
}
