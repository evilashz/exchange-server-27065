using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x0200030C RID: 780
	[Cmdlet("Uninstall", "EdgeSyncService")]
	[LocDescription(Strings.IDs.UninstallEdgeSyncServiceTask)]
	public class UninstallEdgeSyncService : ManageEdgeSyncService
	{
		// Token: 0x06001A66 RID: 6758 RVA: 0x0007508E File Offset: 0x0007328E
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
