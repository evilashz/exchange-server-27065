using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200063A RID: 1594
	[LocDescription(Strings.IDs.InstallPop3BeServiceTask)]
	[Cmdlet("Install", "Pop3BeService")]
	public class InstallPop3BeService : ManagePop3BeService
	{
		// Token: 0x0600380F RID: 14351 RVA: 0x000E8126 File Offset: 0x000E6326
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InstallPopImapService();
			TaskLogger.LogExit();
		}
	}
}
