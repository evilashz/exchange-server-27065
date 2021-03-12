using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000461 RID: 1121
	[LocDescription(Strings.IDs.UninstallMailSubmissionServiceTask)]
	[Cmdlet("Uninstall", "MailSubmissionService")]
	public class UninstallMailSubmissionService : ManageMailSubmissionService
	{
		// Token: 0x0600278F RID: 10127 RVA: 0x0009C58F File Offset: 0x0009A78F
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
