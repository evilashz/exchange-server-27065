using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001DF RID: 479
	[Cmdlet("Install", "AdminToolsRole", SupportsShouldProcess = true)]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class InstallAdminToolsRole : ManageAdminToolsRole
	{
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x000490B0 File Offset: 0x000472B0
		protected override LocalizedString Description
		{
			get
			{
				return Strings.InstallAdminToolsRoleDescription;
			}
		}
	}
}
