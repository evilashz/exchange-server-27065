using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000604 RID: 1540
	[LocDescription(Strings.IDs.InstallMonitoringCorrelationServiceTask)]
	[Cmdlet("Install", "MonitoringCorrelationService")]
	public class InstallMonitoringCorrelationService : ManageMonitoringCorrelationService
	{
		// Token: 0x060036CB RID: 14027 RVA: 0x000E31A5 File Offset: 0x000E13A5
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
