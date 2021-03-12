using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x020002FD RID: 765
	[Cmdlet("Install", "EdgeCredentialService")]
	public class InstallEdgeCredentialService : ManageEdgeCredentialService
	{
		// Token: 0x06001A35 RID: 6709 RVA: 0x000747D7 File Offset: 0x000729D7
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
