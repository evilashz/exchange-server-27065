using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000608 RID: 1544
	[Cmdlet("Uninstall", "MSExchangeMSARPSService")]
	public class UninstallMSExchangeMSARPSService : ManageMSARPSServiceService
	{
		// Token: 0x060036D4 RID: 14036 RVA: 0x000E32D7 File Offset: 0x000E14D7
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
