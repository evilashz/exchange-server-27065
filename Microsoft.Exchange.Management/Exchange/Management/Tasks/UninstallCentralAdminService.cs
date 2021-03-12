using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000DC RID: 220
	[Cmdlet("Uninstall", "CentralAdminService")]
	[LocDescription(Strings.IDs.UninstallCentralAdminServiceTask)]
	public class UninstallCentralAdminService : ManageCentralAdminService
	{
		// Token: 0x06000695 RID: 1685 RVA: 0x0001BDA5 File Offset: 0x00019FA5
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
