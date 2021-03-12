using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008D2 RID: 2258
	[LocDescription(Strings.IDs.InstallWatchDogServiceTask)]
	[Cmdlet("Install", "WatchDogService")]
	public sealed class InstallWatchDogService : ManageWatchDogService
	{
		// Token: 0x0600501D RID: 20509 RVA: 0x0014F749 File Offset: 0x0014D949
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
