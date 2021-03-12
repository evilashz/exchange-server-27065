using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000149 RID: 329
	[LocDescription(Strings.IDs.UninstallComplianceServiceTask)]
	[Cmdlet("Uninstall", "ComplianceService")]
	public sealed class UninstallComplianceService : ManageComplianceService
	{
		// Token: 0x06000BCF RID: 3023 RVA: 0x00036FA0 File Offset: 0x000351A0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
