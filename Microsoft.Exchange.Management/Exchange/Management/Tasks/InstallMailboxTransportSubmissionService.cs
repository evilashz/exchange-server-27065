using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000473 RID: 1139
	[Cmdlet("Install", "SubmissionService")]
	[LocDescription(Strings.IDs.InstallMailboxTransportSubmissionServiceTask)]
	public sealed class InstallMailboxTransportSubmissionService : ManageMailboxTransportSubmissionService
	{
		// Token: 0x06002839 RID: 10297 RVA: 0x0009E8BE File Offset: 0x0009CABE
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
