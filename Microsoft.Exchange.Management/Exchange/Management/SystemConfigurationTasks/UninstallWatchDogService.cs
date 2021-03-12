using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008D3 RID: 2259
	[Cmdlet("Uninstall", "WatchDogService")]
	[LocDescription(Strings.IDs.UninstallWatchDogServiceTask)]
	public sealed class UninstallWatchDogService : ManageWatchDogService
	{
		// Token: 0x0600501F RID: 20511 RVA: 0x0014F763 File Offset: 0x0014D963
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
