using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000C74 RID: 3188
	[Cmdlet("Install", "MigrationWorkflowService")]
	[LocDescription(Strings.IDs.InstallMigrationWorkflowServiceTask)]
	public sealed class InstallMigrationWorkflowService : ManageMigrationWorkflowService
	{
		// Token: 0x06007998 RID: 31128 RVA: 0x001EF99A File Offset: 0x001EDB9A
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
