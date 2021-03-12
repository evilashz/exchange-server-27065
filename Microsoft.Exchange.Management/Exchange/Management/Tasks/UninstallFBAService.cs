using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000D2 RID: 210
	[Cmdlet("Uninstall", "FBAService")]
	[LocDescription(Strings.IDs.UninstallFBAServiceTask)]
	public class UninstallFBAService : ManageFBAService
	{
		// Token: 0x06000650 RID: 1616 RVA: 0x0001B0D0 File Offset: 0x000192D0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
