using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000474 RID: 1140
	[Cmdlet("Uninstall", "SubmissionService")]
	[LocDescription(Strings.IDs.UninstallMailboxTransportSubmissionServiceTask)]
	public sealed class UninstallMailboxTransportSubmissionService : ManageMailboxTransportSubmissionService
	{
		// Token: 0x0600283B RID: 10299 RVA: 0x0009E8D8 File Offset: 0x0009CAD8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
