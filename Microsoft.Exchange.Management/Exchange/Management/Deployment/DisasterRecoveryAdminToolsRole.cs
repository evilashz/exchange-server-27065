using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001B3 RID: 435
	[Cmdlet("DisasterRecovery", "AdminToolsRole", SupportsShouldProcess = true)]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class DisasterRecoveryAdminToolsRole : ManageAdminToolsRole
	{
		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x0004423A File Offset: 0x0004243A
		protected override LocalizedString Description
		{
			get
			{
				return Strings.DisasterRecoveryAdminToolsRoleDescription;
			}
		}
	}
}
