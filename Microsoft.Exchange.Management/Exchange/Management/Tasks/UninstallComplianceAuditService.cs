using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200013F RID: 319
	[Cmdlet("Uninstall", "ComplianceAuditService")]
	[LocDescription(Strings.IDs.UninstallComplianceAuditServiceTask)]
	public class UninstallComplianceAuditService : ManageComplianceAuditService
	{
		// Token: 0x06000B6E RID: 2926 RVA: 0x0003571C File Offset: 0x0003391C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
