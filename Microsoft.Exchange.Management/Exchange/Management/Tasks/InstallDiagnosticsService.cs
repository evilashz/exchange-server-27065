using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002B5 RID: 693
	[Cmdlet("Install", "DiagnosticsService")]
	public class InstallDiagnosticsService : ManageDiagnosticsService
	{
		// Token: 0x0600185E RID: 6238 RVA: 0x0006741D File Offset: 0x0006561D
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
