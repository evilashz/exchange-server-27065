using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x02000302 RID: 770
	[LocDescription(Strings.IDs.InstallEdgeTransportServiceTask)]
	[Cmdlet("Install", "EdgeSyncService")]
	public class InstallEdgeSyncService : ManageEdgeSyncService
	{
		// Token: 0x06001A42 RID: 6722 RVA: 0x00074A60 File Offset: 0x00072C60
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
