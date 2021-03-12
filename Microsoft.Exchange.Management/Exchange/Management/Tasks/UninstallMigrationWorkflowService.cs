using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000C75 RID: 3189
	[Cmdlet("Uninstall", "MigrationWorkflowService")]
	[LocDescription(Strings.IDs.UninstallMigrationWorkflowServiceTask)]
	public sealed class UninstallMigrationWorkflowService : ManageMigrationWorkflowService
	{
		// Token: 0x0600799A RID: 31130 RVA: 0x001EF9B4 File Offset: 0x001EDBB4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
