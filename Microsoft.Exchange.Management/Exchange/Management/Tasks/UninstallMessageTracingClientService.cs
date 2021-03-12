using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000499 RID: 1177
	[Cmdlet("Uninstall", "MessageTracingClientService")]
	[LocDescription(Strings.IDs.UninstallMessageTracingClientServiceTask)]
	public class UninstallMessageTracingClientService : ManageMessageTracingClientService
	{
		// Token: 0x060029DA RID: 10714 RVA: 0x000A64E7 File Offset: 0x000A46E7
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
