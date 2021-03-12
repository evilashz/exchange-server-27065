using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001B5 RID: 437
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("DisasterRecovery", "BridgeheadRole", SupportsShouldProcess = true)]
	public sealed class DisasterRecoveryBridgeheadRole : ManageBridgeheadRole
	{
		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000F5D RID: 3933 RVA: 0x000442C5 File Offset: 0x000424C5
		protected override LocalizedString Description
		{
			get
			{
				return Strings.DisasterRecoveryBridgeheadRoleDescription;
			}
		}
	}
}
