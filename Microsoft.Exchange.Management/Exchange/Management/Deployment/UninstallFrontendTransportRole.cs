using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200027B RID: 635
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Uninstall", "FrontendTransportRole", SupportsShouldProcess = true)]
	public sealed class UninstallFrontendTransportRole : ManageRole
	{
		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06001772 RID: 6002 RVA: 0x000636DB File Offset: 0x000618DB
		protected override LocalizedString Description
		{
			get
			{
				return Strings.UninstallFrontendTransportRoleDescription;
			}
		}
	}
}
