using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008D0 RID: 2256
	[Cmdlet("Uninstall", "DagMgmtService")]
	[LocDescription(Strings.IDs.UninstallDagMgmtServiceTask)]
	public sealed class UninstallDagMgmtService : ManageDagMgmtService
	{
		// Token: 0x06005018 RID: 20504 RVA: 0x0014F62D File Offset: 0x0014D82D
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
