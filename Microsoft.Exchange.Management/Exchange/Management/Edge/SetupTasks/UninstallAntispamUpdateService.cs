using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x0200030F RID: 783
	[Cmdlet("Uninstall", "AntispamUpdateService")]
	[LocDescription(Strings.IDs.UninstallAntispamUpdateServiceTask)]
	public class UninstallAntispamUpdateService : ManageAntispamUpdateService
	{
		// Token: 0x06001A6C RID: 6764 RVA: 0x000750DC File Offset: 0x000732DC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
