using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200037F RID: 895
	[Cmdlet("Install", "ForwardSyncService")]
	[LocDescription(Strings.IDs.InstallForwardSyncServiceTask)]
	public class InstallForwardSyncService : ManageForwardSyncService
	{
		// Token: 0x06001F53 RID: 8019 RVA: 0x00087807 File Offset: 0x00085A07
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
