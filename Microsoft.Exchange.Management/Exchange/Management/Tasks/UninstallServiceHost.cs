using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000765 RID: 1893
	[Cmdlet("Uninstall", "ServiceHost")]
	public class UninstallServiceHost : ManageServiceHost
	{
		// Token: 0x0600433D RID: 17213 RVA: 0x001141D8 File Offset: 0x001123D8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
