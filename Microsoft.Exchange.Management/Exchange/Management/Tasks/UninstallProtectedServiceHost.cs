using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000768 RID: 1896
	[Cmdlet("Uninstall", "ProtectedServiceHost")]
	public class UninstallProtectedServiceHost : ManageProtectedServiceHost
	{
		// Token: 0x06004348 RID: 17224 RVA: 0x00114372 File Offset: 0x00112572
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
