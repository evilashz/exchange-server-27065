using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001B6 RID: 438
	[Cmdlet("DisasterRecovery", "CafeRole", SupportsShouldProcess = true)]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class DisasterRecoveryCafeRole : ManageRole
	{
		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x000442CC File Offset: 0x000424CC
		protected override LocalizedString Description
		{
			get
			{
				return Strings.DisasterRecoveryCafeRoleDescription;
			}
		}
	}
}
