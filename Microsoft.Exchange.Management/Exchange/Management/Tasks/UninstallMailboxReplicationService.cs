using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000C72 RID: 3186
	[LocDescription(Strings.IDs.UninstallMailboxReplicationServiceTask)]
	[Cmdlet("Uninstall", "MailboxReplicationService")]
	public sealed class UninstallMailboxReplicationService : ManageMailboxReplicationService
	{
		// Token: 0x06007993 RID: 31123 RVA: 0x001EF86E File Offset: 0x001EDA6E
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
