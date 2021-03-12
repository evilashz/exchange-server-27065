using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000638 RID: 1592
	[Cmdlet("Uninstall", "Pop3Service")]
	[LocDescription(Strings.IDs.UninstallPop3ServiceTask)]
	public class UninstallPop3Service : ManagePop3Service
	{
		// Token: 0x06003804 RID: 14340 RVA: 0x000E80B7 File Offset: 0x000E62B7
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.UninstallPopImapService();
			TaskLogger.LogExit();
		}
	}
}
