using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001BF RID: 447
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("DisasterRecovery", "MonitoringRole", SupportsShouldProcess = true)]
	public sealed class DisasterRecoveryMonitoringRole : ManageMonitoringRole
	{
		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x000443EA File Offset: 0x000425EA
		protected override LocalizedString Description
		{
			get
			{
				return Strings.DisasterRecoveryMonitoringRoleDescription;
			}
		}
	}
}
