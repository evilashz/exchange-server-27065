using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001B7 RID: 439
	[Cmdlet("DisasterRecovery", "ClientAccessRole", SupportsShouldProcess = true)]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class DisasterRecoveryClientAccessRole : ManageRole
	{
		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x000442DB File Offset: 0x000424DB
		protected override LocalizedString Description
		{
			get
			{
				return Strings.DisasterRecoveryClientAccessRoleDescription;
			}
		}
	}
}
