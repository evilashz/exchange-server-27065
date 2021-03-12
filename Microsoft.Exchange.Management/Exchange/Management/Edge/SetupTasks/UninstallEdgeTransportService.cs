using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x0200030D RID: 781
	[Cmdlet("Uninstall", "EdgeTransportService")]
	[LocDescription(Strings.IDs.UninstallEdgeTransportServiceTask)]
	public class UninstallEdgeTransportService : ManageEdgeTransportService
	{
		// Token: 0x06001A68 RID: 6760 RVA: 0x000750A8 File Offset: 0x000732A8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
