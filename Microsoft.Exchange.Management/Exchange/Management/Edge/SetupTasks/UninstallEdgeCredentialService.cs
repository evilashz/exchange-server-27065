using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x020002FE RID: 766
	[Cmdlet("Uninstall", "EdgeCredentialService")]
	public class UninstallEdgeCredentialService : ManageEdgeCredentialService
	{
		// Token: 0x06001A37 RID: 6711 RVA: 0x000747F1 File Offset: 0x000729F1
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
