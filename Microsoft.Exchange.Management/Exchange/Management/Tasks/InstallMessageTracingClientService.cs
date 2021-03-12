using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000498 RID: 1176
	[Cmdlet("Install", "MessageTracingClientService")]
	[LocDescription(Strings.IDs.InstallMessageTracingClientServiceTask)]
	public class InstallMessageTracingClientService : ManageMessageTracingClientService
	{
		// Token: 0x060029D8 RID: 10712 RVA: 0x000A64CD File Offset: 0x000A46CD
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
