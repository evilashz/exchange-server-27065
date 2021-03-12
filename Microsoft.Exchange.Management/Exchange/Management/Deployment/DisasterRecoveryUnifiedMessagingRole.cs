using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001C2 RID: 450
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("DisasterRecovery", "UnifiedMessagingRole", SupportsShouldProcess = true)]
	public sealed class DisasterRecoveryUnifiedMessagingRole : InstallUnifiedMessagingRoleBase
	{
		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x000447F0 File Offset: 0x000429F0
		protected override LocalizedString Description
		{
			get
			{
				return Strings.DisasterRecoveryUnifiedMessagingRoleDescription;
			}
		}
	}
}
