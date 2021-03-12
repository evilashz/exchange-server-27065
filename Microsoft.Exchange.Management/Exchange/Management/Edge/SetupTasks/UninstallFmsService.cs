using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x0200030E RID: 782
	[LocDescription(Strings.IDs.UninstallFmsServiceTask)]
	[Cmdlet("Uninstall", "FmsService")]
	public class UninstallFmsService : ManageFmsService
	{
		// Token: 0x06001A6A RID: 6762 RVA: 0x000750C2 File Offset: 0x000732C2
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
