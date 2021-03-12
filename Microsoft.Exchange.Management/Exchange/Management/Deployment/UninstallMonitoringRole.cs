using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200027E RID: 638
	[Cmdlet("Uninstall", "MonitoringRole", SupportsShouldProcess = true)]
	public sealed class UninstallMonitoringRole : ManageMonitoringRole
	{
		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x00063708 File Offset: 0x00061908
		protected override LocalizedString Description
		{
			get
			{
				return Strings.UninstallMonitoringRoleDescription;
			}
		}
	}
}
