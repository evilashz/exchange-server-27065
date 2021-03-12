using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000D01 RID: 3329
	[Cmdlet("Uninstall", "FrontendTransportService")]
	public sealed class UninstallFrontendTransportService : ManageFrontendTransportService
	{
		// Token: 0x06007FE2 RID: 32738 RVA: 0x0020B0CC File Offset: 0x002092CC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
