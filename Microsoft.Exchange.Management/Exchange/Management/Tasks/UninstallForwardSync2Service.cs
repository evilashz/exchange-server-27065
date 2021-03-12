using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000383 RID: 899
	[LocDescription(Strings.IDs.UninstallForwardSyncServiceTask)]
	[Cmdlet("Uninstall", "ForwardSync2Service")]
	public class UninstallForwardSync2Service : ManageCentralAdminService
	{
		// Token: 0x06001F5B RID: 8027 RVA: 0x0008792B File Offset: 0x00085B2B
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
