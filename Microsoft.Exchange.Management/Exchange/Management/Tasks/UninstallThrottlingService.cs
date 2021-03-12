using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000CFE RID: 3326
	[LocDescription(Strings.IDs.UninstallThrottlingServiceTask)]
	[Cmdlet("Uninstall", "ThrottlingService")]
	public sealed class UninstallThrottlingService : ManageThrottlingService
	{
		// Token: 0x06007FDC RID: 32732 RVA: 0x0020AFD2 File Offset: 0x002091D2
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
