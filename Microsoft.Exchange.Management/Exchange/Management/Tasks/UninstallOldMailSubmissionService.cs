using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000460 RID: 1120
	[LocDescription(Strings.IDs.UninstallOldMailSubmissionServiceTask)]
	[Cmdlet("Uninstall", "OldMailSubmissionService")]
	public class UninstallOldMailSubmissionService : ManageOldMailSubmissionService
	{
		// Token: 0x0600278D RID: 10125 RVA: 0x0009C575 File Offset: 0x0009A775
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
