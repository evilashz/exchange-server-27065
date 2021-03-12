using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000277 RID: 631
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Uninstall", "ClientAccessRole", SupportsShouldProcess = true)]
	public sealed class UninstallClientAccessRole : ManageRole
	{
		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001764 RID: 5988 RVA: 0x000634F1 File Offset: 0x000616F1
		protected override LocalizedString Description
		{
			get
			{
				return Strings.UninstallClientAccessRoleDescription;
			}
		}
	}
}
