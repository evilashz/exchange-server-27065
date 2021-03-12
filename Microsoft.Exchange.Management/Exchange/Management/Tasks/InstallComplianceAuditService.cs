using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200013E RID: 318
	[Cmdlet("Install", "ComplianceAuditService")]
	[LocDescription(Strings.IDs.InstallComplianceAuditServiceTask)]
	public class InstallComplianceAuditService : ManageComplianceAuditService
	{
		// Token: 0x06000B6C RID: 2924 RVA: 0x00035702 File Offset: 0x00033902
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
