using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002B6 RID: 694
	[Cmdlet("Uninstall", "DiagnosticsService")]
	public class UninstallDiagnosticsService : ManageDiagnosticsService
	{
		// Token: 0x06001860 RID: 6240 RVA: 0x00067437 File Offset: 0x00065637
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
