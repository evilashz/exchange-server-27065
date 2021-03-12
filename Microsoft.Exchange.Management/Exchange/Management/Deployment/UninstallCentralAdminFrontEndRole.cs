using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000275 RID: 629
	[Cmdlet("Uninstall", "CentralAdminFrontEndRole", SupportsShouldProcess = true)]
	public sealed class UninstallCentralAdminFrontEndRole : ManageRole
	{
		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001760 RID: 5984 RVA: 0x000634D3 File Offset: 0x000616D3
		protected override LocalizedString Description
		{
			get
			{
				return Strings.InstallCentralAdminFrontEndRoleDescription;
			}
		}
	}
}
