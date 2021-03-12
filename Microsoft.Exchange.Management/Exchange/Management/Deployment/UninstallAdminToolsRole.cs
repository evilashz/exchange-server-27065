using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000271 RID: 625
	[Cmdlet("Uninstall", "AdminToolsRole", SupportsShouldProcess = true)]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class UninstallAdminToolsRole : ManageAdminToolsRole
	{
		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001758 RID: 5976 RVA: 0x00063497 File Offset: 0x00061697
		protected override LocalizedString Description
		{
			get
			{
				return Strings.UninstallAdminToolsRoleDescription;
			}
		}
	}
}
