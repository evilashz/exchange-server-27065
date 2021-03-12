using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000635 RID: 1589
	[LocDescription(Strings.IDs.UninstallImap4BeServiceTask)]
	[Cmdlet("Uninstall", "Imap4BeService")]
	public class UninstallImap4BeService : ManageImap4BeService
	{
		// Token: 0x060037F7 RID: 14327 RVA: 0x000E8014 File Offset: 0x000E6214
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.UninstallPopImapService();
			TaskLogger.LogExit();
		}
	}
}
