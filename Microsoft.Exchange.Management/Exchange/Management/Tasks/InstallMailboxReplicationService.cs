using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000C71 RID: 3185
	[LocDescription(Strings.IDs.InstallMailboxReplicationServiceTask)]
	[Cmdlet("Install", "MailboxReplicationService")]
	public sealed class InstallMailboxReplicationService : ManageMailboxReplicationService
	{
		// Token: 0x06007991 RID: 31121 RVA: 0x001EF854 File Offset: 0x001EDA54
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
