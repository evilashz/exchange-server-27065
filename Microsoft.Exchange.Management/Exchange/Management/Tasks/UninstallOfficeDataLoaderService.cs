using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200076D RID: 1901
	[Cmdlet("Uninstall", "OfficeDataLoaderService")]
	public class UninstallOfficeDataLoaderService : ManageOfficeDataLoaderService
	{
		// Token: 0x06004354 RID: 17236 RVA: 0x0011467E File Offset: 0x0011287E
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
