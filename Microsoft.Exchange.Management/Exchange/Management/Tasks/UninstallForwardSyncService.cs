using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000380 RID: 896
	[LocDescription(Strings.IDs.UninstallForwardSyncServiceTask)]
	[Cmdlet("Uninstall", "ForwardSyncService")]
	public class UninstallForwardSyncService : ManageCentralAdminService
	{
		// Token: 0x06001F55 RID: 8021 RVA: 0x00087821 File Offset: 0x00085A21
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
