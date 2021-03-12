using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000749 RID: 1865
	[Cmdlet("Uninstall", "RpcClientAccessService")]
	public class UninstallRpcClientAccessService : ManageRpcClientAccessService
	{
		// Token: 0x06004202 RID: 16898 RVA: 0x0010D681 File Offset: 0x0010B881
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
