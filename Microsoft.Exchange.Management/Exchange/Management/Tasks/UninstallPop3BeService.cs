using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200063B RID: 1595
	[Cmdlet("Uninstall", "Pop3BeService")]
	[LocDescription(Strings.IDs.UninstallPop3BeServiceTask)]
	public class UninstallPop3BeService : ManagePop3BeService
	{
		// Token: 0x06003811 RID: 14353 RVA: 0x000E8140 File Offset: 0x000E6340
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.UninstallPopImapService();
			TaskLogger.LogExit();
		}
	}
}
