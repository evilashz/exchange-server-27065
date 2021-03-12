using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000044 RID: 68
	[Cmdlet("Uninstall", "TransportSyncManagerService")]
	[LocDescription(Strings.IDs.UninstallTransportSyncManagerServiceTask)]
	public class UninstallTransportSyncManagerService : ManageTransportSyncManagerService
	{
		// Token: 0x0600028F RID: 655 RVA: 0x0000BEE8 File Offset: 0x0000A0E8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
