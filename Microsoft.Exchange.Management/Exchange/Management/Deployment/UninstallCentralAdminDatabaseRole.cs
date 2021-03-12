using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000274 RID: 628
	[Cmdlet("Uninstall", "CentralAdminDatabaseRole", SupportsShouldProcess = true)]
	public sealed class UninstallCentralAdminDatabaseRole : ManageRole
	{
		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x000634C4 File Offset: 0x000616C4
		protected override LocalizedString Description
		{
			get
			{
				return Strings.InstallCentralAdminDatabaseRoleDescription;
			}
		}
	}
}
