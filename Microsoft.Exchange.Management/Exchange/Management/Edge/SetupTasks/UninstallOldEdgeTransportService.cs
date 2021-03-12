using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x02000310 RID: 784
	[LocDescription(Strings.IDs.UninstallOldEdgeTransportServiceTask)]
	[Cmdlet("Uninstall", "OldEdgeTransportService")]
	public class UninstallOldEdgeTransportService : ManageEdgeTransportService
	{
		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06001A6E RID: 6766 RVA: 0x000750F6 File Offset: 0x000732F6
		protected override string Name
		{
			get
			{
				return "EdgeTransportSvc";
			}
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x000750FD File Offset: 0x000732FD
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}

		// Token: 0x04000B7F RID: 2943
		private const string ServiceShortName = "EdgeTransportSvc";
	}
}
