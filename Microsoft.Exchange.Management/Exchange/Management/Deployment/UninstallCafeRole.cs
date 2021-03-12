using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000273 RID: 627
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Uninstall", "CafeRole", SupportsShouldProcess = true)]
	public sealed class UninstallCafeRole : ManageRole
	{
		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x0600175C RID: 5980 RVA: 0x000634B5 File Offset: 0x000616B5
		protected override LocalizedString Description
		{
			get
			{
				return Strings.UninstallCafeRoleDescription;
			}
		}
	}
}
