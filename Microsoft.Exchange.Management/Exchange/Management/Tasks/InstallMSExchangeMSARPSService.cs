using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000607 RID: 1543
	[Cmdlet("Install", "MSExchangeMSARPSService")]
	public class InstallMSExchangeMSARPSService : ManageMSARPSServiceService
	{
		// Token: 0x060036D2 RID: 14034 RVA: 0x000E32BD File Offset: 0x000E14BD
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
