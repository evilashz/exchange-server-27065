using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200060A RID: 1546
	[Cmdlet("Uninstall", "MSExchangeMGMTService")]
	public class UninstallMSExchangeMGMTService : ManageMSExchangeMGMTService
	{
		// Token: 0x060036D8 RID: 14040 RVA: 0x000E3394 File Offset: 0x000E1594
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
