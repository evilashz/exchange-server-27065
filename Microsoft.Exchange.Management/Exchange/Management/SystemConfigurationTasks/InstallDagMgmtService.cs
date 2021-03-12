using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008CF RID: 2255
	[Cmdlet("Install", "DagMgmtService")]
	[LocDescription(Strings.IDs.InstallDagMgmtServiceTask)]
	public sealed class InstallDagMgmtService : ManageDagMgmtService
	{
		// Token: 0x06005016 RID: 20502 RVA: 0x0014F613 File Offset: 0x0014D813
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
