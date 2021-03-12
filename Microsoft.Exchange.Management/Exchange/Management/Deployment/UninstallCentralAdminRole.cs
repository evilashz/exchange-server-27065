using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000276 RID: 630
	[Cmdlet("Uninstall", "CentralAdminRole", SupportsShouldProcess = true)]
	public sealed class UninstallCentralAdminRole : ManageRole
	{
		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001762 RID: 5986 RVA: 0x000634E2 File Offset: 0x000616E2
		protected override LocalizedString Description
		{
			get
			{
				return Strings.InstallCentralAdminRoleDescription;
			}
		}
	}
}
