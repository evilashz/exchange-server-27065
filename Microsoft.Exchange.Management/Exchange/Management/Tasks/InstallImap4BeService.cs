using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000634 RID: 1588
	[Cmdlet("Install", "Imap4BeService")]
	[LocDescription(Strings.IDs.InstallImap4BeServiceTask)]
	public class InstallImap4BeService : ManageImap4BeService
	{
		// Token: 0x060037F5 RID: 14325 RVA: 0x000E7FFA File Offset: 0x000E61FA
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InstallPopImapService();
			TaskLogger.LogExit();
		}
	}
}
