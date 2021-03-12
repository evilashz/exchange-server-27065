using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000272 RID: 626
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Uninstall", "BridgeheadRole", SupportsShouldProcess = true)]
	public sealed class UninstallBridgeheadRole : ManageRole
	{
		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x000634A6 File Offset: 0x000616A6
		protected override LocalizedString Description
		{
			get
			{
				return Strings.UninstallBridgeheadRoleDescription;
			}
		}
	}
}
