using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000641 RID: 1601
	[Cmdlet("Uninstall", "ProcessUtilizationManagerService")]
	public class UninstallProcessUtilizationManagerService : ManageProcessUtilizationManagerService
	{
		// Token: 0x0600381D RID: 14365 RVA: 0x000E8423 File Offset: 0x000E6623
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
