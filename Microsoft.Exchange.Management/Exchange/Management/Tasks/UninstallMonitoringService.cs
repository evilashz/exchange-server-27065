using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020005FF RID: 1535
	[Cmdlet("Uninstall", "MonitoringService")]
	[LocDescription(Strings.IDs.UninstallMonitoringServiceTask)]
	public class UninstallMonitoringService : ManageMonitoringService
	{
		// Token: 0x060036BD RID: 14013 RVA: 0x000E2D0C File Offset: 0x000E0F0C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
