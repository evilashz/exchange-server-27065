using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000601 RID: 1537
	[LocDescription(Strings.IDs.UninstallHealthManagerServiceTask)]
	[Cmdlet("Uninstall", "HealthManagerService")]
	public class UninstallHealthManagerService : ManageHealthManagerService
	{
		// Token: 0x060036C5 RID: 14021 RVA: 0x000E3090 File Offset: 0x000E1290
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			base.RemoveManagedAvailabilityServersUsgSidCache();
			TaskLogger.LogExit();
		}
	}
}
