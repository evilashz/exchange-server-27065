using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000632 RID: 1586
	[LocDescription(Strings.IDs.UninstallImap4ServiceTask)]
	[Cmdlet("Uninstall", "Imap4Service")]
	public class UninstallImap4Service : ManageImap4Service
	{
		// Token: 0x060037EA RID: 14314 RVA: 0x000E7F8B File Offset: 0x000E618B
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.UninstallPopImapService();
			TaskLogger.LogExit();
		}
	}
}
