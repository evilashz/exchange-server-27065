using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000DE RID: 222
	[LocDescription(Strings.IDs.UninstallRecoveryActionArbiterServiceTask)]
	[Cmdlet("Uninstall", "RecoveryActionArbiterService")]
	public class UninstallRecoveryActionArbiterService : ManageRecoveryActionArbiterService
	{
		// Token: 0x06000699 RID: 1689 RVA: 0x0001BDD9 File Offset: 0x00019FD9
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
