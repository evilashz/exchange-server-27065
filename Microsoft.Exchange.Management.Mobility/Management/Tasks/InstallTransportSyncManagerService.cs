using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000043 RID: 67
	[LocDescription(Strings.IDs.InstallTransportSyncManagerServiceTask)]
	[Cmdlet("Install", "TransportSyncManagerService")]
	public class InstallTransportSyncManagerService : ManageTransportSyncManagerService
	{
		// Token: 0x0600028D RID: 653 RVA: 0x0000BECE File Offset: 0x0000A0CE
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
