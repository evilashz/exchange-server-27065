using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000640 RID: 1600
	[Cmdlet("Install", "ProcessUtilizationManagerService")]
	public class InstallProcessUtilizationManagerService : ManageProcessUtilizationManagerService
	{
		// Token: 0x0600381B RID: 14363 RVA: 0x000E8409 File Offset: 0x000E6609
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
