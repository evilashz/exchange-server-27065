using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000748 RID: 1864
	[Cmdlet("Install", "RpcClientAccessService")]
	public class InstallRpcClientAccessService : ManageRpcClientAccessService
	{
		// Token: 0x06004200 RID: 16896 RVA: 0x0010D667 File Offset: 0x0010B867
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
