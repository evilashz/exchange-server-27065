using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000631 RID: 1585
	[LocDescription(Strings.IDs.InstallImap4ServiceTask)]
	[Cmdlet("Install", "Imap4Service")]
	public class InstallImap4Service : ManageImap4Service
	{
		// Token: 0x060037E8 RID: 14312 RVA: 0x000E7F5B File Offset: 0x000E615B
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InstallPopImapService();
			base.ReservePort(143);
			base.ReservePort(993);
			TaskLogger.LogExit();
		}
	}
}
