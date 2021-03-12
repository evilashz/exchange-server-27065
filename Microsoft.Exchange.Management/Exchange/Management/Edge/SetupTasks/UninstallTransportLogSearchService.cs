using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x02000311 RID: 785
	[Cmdlet("Uninstall", "TransportLogSearchService")]
	public class UninstallTransportLogSearchService : ManageTransportLogSearchService
	{
		// Token: 0x06001A71 RID: 6769 RVA: 0x00075117 File Offset: 0x00073317
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
