using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000605 RID: 1541
	[Cmdlet("Uninstall", "MonitoringCorrelationService")]
	[LocDescription(Strings.IDs.UninstallMonitoringCorrelationServiceTask)]
	public class UninstallMonitoringCorrelationService : ManageMonitoringCorrelationService
	{
		// Token: 0x060036CD RID: 14029 RVA: 0x000E31BF File Offset: 0x000E13BF
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
