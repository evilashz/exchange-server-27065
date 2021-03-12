using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200027C RID: 636
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Uninstall", "GatewayRole", SupportsShouldProcess = true)]
	public sealed class UninstallGatewayRole : ManageGatewayRole
	{
		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001774 RID: 6004 RVA: 0x000636EA File Offset: 0x000618EA
		protected override LocalizedString Description
		{
			get
			{
				return Strings.UninstallGatewayRoleDescription;
			}
		}
	}
}
